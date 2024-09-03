using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace APIpostYeventos
{
    public partial class AñadirComentario : Form
    {
        private string usuario;
        public AñadirComentario(string user)
        {
            InitializeComponent();
            usuario = user;
        }
        public class Creador
        {
            public string NombreCreador { get; set; }
        }

        private async void btnBuscar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtID.Text) || !int.TryParse(txtID.Text, out int numero))
            {
                lblErrorID.Show();
                lblErrorID.Text = "Debe ingresar una ID válida";
            }
            else
            {
                var respuesta = await Existe(int.Parse(txtID.Text));
                if (respuesta)
                {
                    var data = await Buscar(int.Parse(txtID.Text));
                    txtTexto.Text = data[0];
                    txtUrl.Text = data[1];
                    if (data[2].Length > 0)
                    {
                        byte[] imagen = Convert.FromBase64String(data[2]);
                        MemoryStream ms = new MemoryStream(imagen);
                        Bitmap bitmap = new Bitmap(ms);
                        pictureBox1.Image = bitmap;
                        pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                    }
                    else
                    {
                        pictureBox1.Image = null;
                    }
                    lblTexto.Show();
                    lblFoto.Show();
                    lblVideo.Show();
                    txtTexto.Show();
                    txtUrl.Show();
                    lblID.Hide();
                    lblErrorID.Hide();
                    txtID.Hide();
                    btnBuscar.Hide();
                    btnVolver.Hide();
                    btnCancelar.Show();
                    txtComentario.Show();
                    btnPublicar.Show();
                    lblNombreDeCuenta.Show();
                    txtNombreDeCuenta.Show();
                }
                else
                {
                    lblErrorID.Show();
                    lblErrorID.Text = "No se encontró la ID";
                }
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Form1 f1 = new Form1(usuario);
            f1.Show();
            this.Close();
        }
        static async Task<string[]> Buscar(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync($"https://localhost:44340/postPorId?id={id}");
                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();
                    dynamic data = JsonConvert.DeserializeObject(responseBody); 
                    return new string[] { data.texto, data.url, data.imagen };
                }
                catch
                {
                    return null;
                }
            }
        }
        static async Task<bool> Existe(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync($"https://localhost:44340/existePost?id={id}");
                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();
                    dynamic data = JsonConvert.DeserializeObject<bool>(responseBody); 
                    return data;
                }
                catch
                {
                    return false;
                }
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
            DateTime now = DateTime.Now;
            string fechayhora = now.ToString("yyyy-MM-dd HH:mm:ss");
            int nombrecreador;
            int.TryParse(txtID.Text, out nombrecreador);
            string nombreCreador = await ConseguirCreador(nombrecreador);

            if (nombreCreador != null)
            {
                await Publicar(usuario, txtID.Text, nombreCreador, txtComentario.Text, fechayhora);
                MessageBox.Show("El comentario se creó correctamente");
            }
            else
            {
                MessageBox.Show("Error al obtener el creador");
            }
            lblError.Show();
        }

        static async Task Publicar(string NombreDeCuenta, string IdPost, string nombreCreador, string texto, string fechayhora)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {

                    var datos = new { NombreDeCuenta = NombreDeCuenta, IdPost = IdPost, NombreCreador = nombreCreador, texto = texto, fechayhora = fechayhora };
                    var content = new StringContent(JsonConvert.SerializeObject(datos), Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PostAsync("https://localhost:44340/hacerComentario", content);
                    response.EnsureSuccessStatusCode();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }
        static async Task<string> ConseguirCreador(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync($"https://localhost:44340/conseguirCreador?id={id}");
                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();
                    return responseBody.Trim('"'); 
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                    return null;
                }
            }
        }


        //testing
        public dynamic hacerComentario(string NombreDeCuenta = "", string IdPost = "", string texto = "", string fechayhora = "")
        {
            MySqlConnection conn = new MySqlConnection("Server=localhost; database=base; uID=root; pwd=;");
            conn.Open();
            MySqlCommand cmd;
            if (!string.IsNullOrEmpty(NombreDeCuenta) &&
                !string.IsNullOrEmpty(IdPost) && !string.IsNullOrEmpty(texto) &&
                !string.IsNullOrEmpty(fechayhora))
            {
                cmd = new MySqlCommand("INSERT INTO comentarios (NombreDeCuenta,IdPost,texto,fechayhora) VALUES (@NombreDeCuenta,@IdPost,@Texto,@FechayHora)", conn);
                cmd.Parameters.AddWithValue("@NombreDeCuenta", NombreDeCuenta);
                cmd.Parameters.AddWithValue("@IdPost", IdPost);
                cmd.Parameters.AddWithValue("@Texto", texto);
                cmd.Parameters.AddWithValue("@FechayHora", fechayhora);
                cmd.ExecuteNonQuery();
                conn.Close();
                return "guardado correcto";
            }
            else
            {
                return "guardado incorrecto";
            }
        }



    }
}
