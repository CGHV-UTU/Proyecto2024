using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace APIpostYeventos
{
    public partial class AñadirEvento : Form
    {
        private static string usuario;
        public AñadirEvento(string user)
        {
            InitializeComponent();
            dtpFecha.MinDate = DateTime.Today;
            int año = DateTime.Now.Year;
            dtpFecha.MaxDate = new DateTime(año, 12, 31);
            int hora = DateTime.Now.Hour;
            int minuto = DateTime.Now.Minute + 5;
            if (minuto >= 60)
            { minuto -= 60;
                hora += 1;
            }
            if (hora >= 24)
            {
                hora = 0;
            }
            dtpHora.MinDate = new DateTime(año, 12, 31, hora, minuto, 0);
            usuario = user;
        }

        private void btnSeleccionarFecha_Click(object sender, EventArgs e)
        {
            lblFechaHora.Text = dtpFecha.Text + " " + dtpHora.Text;
        }

        private void btnSeleccionar_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                pbxImagen.ImageLocation = ofd.FileName;
                pbxImagen.SizeMode = PictureBoxSizeMode.StretchImage;
            }
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            Form1 f1 = new Form1(usuario);
            f1.Show();
            this.Close();
        }

        private async void btnPublicar_Click(object sender, EventArgs e)
        {
            string[] fecha = dtpFecha.Text.Split('/');
            string[] hora = dtpHora.Text.Split(':');
            DateTime datetime = new DateTime(int.Parse(fecha[2]),int.Parse(fecha[1]),int.Parse(fecha[0]),int.Parse(hora[0]),int.Parse(hora[1]),0);
            
            if (string.IsNullOrEmpty(txtTitulo.Text) || pbxImagen.Image == null || datetime<=DateTime.Now )
            {
                lblError.Text = "Debe Rellenar el título, la imagen y la fecha";
                lblError.Show();
            }
            else
            {
                string fechayhora = dtpFecha.Text + " " + dtpHora.Text;
                MemoryStream ms = new MemoryStream();
                pbxImagen.Image.Save(ms, ImageFormat.Jpeg);
                byte[] data = ms.ToArray();
                await Publicar(txtNombreGrupo.Text,usuario,txtTitulo.Text, txtUbicacion.Text, txtDescripcion.Text, data, fechayhora);
                MessageBox.Show("Evento creado correctamente");
            }
        }
        static async Task Publicar(string nombreGrupoVisible, string usuario, string titulo, string ubicacion, string descripcion, byte[] imagen, string fechayhora)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string baseUrl1 = $"https://localhost:44304/ObtenerGruposPorNombreVisibleYUsuario?nombreVisible={nombreGrupoVisible}&nombreDeCuenta={usuario}";
                    string baseUrl2 = "https://localhost:44340/hacerEvento";
                    Console.WriteLine($"Sending GET request to {baseUrl1}");
                    HttpResponseMessage response1 = await client.GetAsync(baseUrl1);
                    if (!response1.IsSuccessStatusCode)
                    {
                        MessageBox.Show($"Error fetching 'nombreReal': {response1.StatusCode} ({response1.ReasonPhrase})");
                        return;
                    }
                    string responseBody1 = await response1.Content.ReadAsStringAsync();
                    Console.WriteLine("Response from ObtenerGruposPorNombreVisibleYUsuario: " + responseBody1);
                    List<dynamic> grupos = JsonConvert.DeserializeObject<List<dynamic>>(responseBody1);

                    if (grupos.Count > 0)
                    {
                        string nombreReal = grupos[0].nombreReal;
                        Console.WriteLine("Nombre Real obtenido: " + nombreReal);
                        var datos2 = new
                        {
                            nombreReal = nombreReal,
                            titulo = titulo,
                            ubicacion = ubicacion,
                            descripcion = descripcion,
                            imagen = Convert.ToBase64String(imagen),
                            fechayhora = fechayhora,
                            user = usuario
                        };
                        var content2 = new StringContent(JsonConvert.SerializeObject(datos2), Encoding.UTF8, "application/json");
                        Console.WriteLine($"Sending POST request to {baseUrl2} with data: {JsonConvert.SerializeObject(datos2)}");
                        HttpResponseMessage response2 = await client.PostAsync(baseUrl2, content2);

                        if (!response2.IsSuccessStatusCode)
                        {
                            MessageBox.Show($"Error creating event: {response2.StatusCode} ({response2.ReasonPhrase})");
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show("No se encontraron Grupos para el usuario especificado.");
                    }
                }
                catch (HttpRequestException ex)
                {
                    MessageBox.Show($"HTTP Request Error: {ex.Message}");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void pbxImagen_Click(object sender, EventArgs e)
        {

        }


        //testing
        public string hacerEvento(string title, string image, string horario, string ubication = "", string description = "")
        {
            string titulo = title;
            string fechayhora = horario;
            byte[] imagen = Convert.FromBase64String(image);
            MySqlConnection conn = new MySqlConnection("Server=localhost; database=base; uID=root; pwd=;");
            conn.Open();
            if (!string.IsNullOrEmpty(ubication))
            {
                if (!string.IsNullOrEmpty(description))
                {
                    string ubicacion = ubication;
                    string descripcion = description;
                    MySqlCommand cmd = new MySqlCommand("INSERT INTO eventos (titulo,ubicacion,descripcion,fechayhora,foto) VALUES (@Titulo,@Ubicacion,@Descripcion,@FechayHora,@Foto)", conn);
                    cmd.Parameters.AddWithValue("@Titulo", titulo);
                    cmd.Parameters.AddWithValue("@Ubicacion", ubicacion);
                    cmd.Parameters.AddWithValue("@Descripcion", descripcion);
                    cmd.Parameters.AddWithValue("@FechayHora", fechayhora);
                    cmd.Parameters.AddWithValue("@Foto", imagen);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    return "guardado correcto";
                }
                else
                {
                    string ubicacion = ubication;
                    MySqlCommand cmd = new MySqlCommand("INSERT INTO eventos (titulo,ubicacion,fechayhora,foto) VALUES (@Titulo,@Ubicacion,@FechayHora,@Foto)", conn);
                    cmd.Parameters.AddWithValue("@Titulo", titulo);
                    cmd.Parameters.AddWithValue("@Ubicacion", ubicacion);
                    cmd.Parameters.AddWithValue("@FechayHora", fechayhora);
                    cmd.Parameters.AddWithValue("@Foto", imagen);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    return "guardado correcto";
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(description))
                {
                    string descripcion = description;
                    MySqlCommand cmd = new MySqlCommand("INSERT INTO eventos (titulo,descripcion,fechayhora,foto) VALUES (@Titulo,@Descripcion,@FechayHora,@Foto)", conn);
                    cmd.Parameters.AddWithValue("@Titulo", titulo);
                    cmd.Parameters.AddWithValue("@Descripcion", descripcion);
                    cmd.Parameters.AddWithValue("@FechayHora", fechayhora);
                    cmd.Parameters.AddWithValue("@Foto", imagen);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    return "guardado correcto";
                }
                else
                {
                    MySqlCommand cmd = new MySqlCommand("INSERT INTO eventos (titulo,fechayhora,foto) VALUES (@Titulo,@FechayHora,@Foto)", conn);
                    cmd.Parameters.AddWithValue("@Titulo", titulo);
                    cmd.Parameters.AddWithValue("@FechayHora", fechayhora);
                    cmd.Parameters.AddWithValue("@Foto", imagen);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    return "guardado correcto";
                }
            }
        }
    }
}
