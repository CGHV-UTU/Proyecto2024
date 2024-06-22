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
    public partial class EditarEvento : Form
    {
        public EditarEvento()
        {
            InitializeComponent();
            dtpFecha.MinDate = DateTime.Today;
            int año = DateTime.Now.Year;
            dtpFecha.MaxDate = new DateTime(año,12,31);
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
                string fechayhora = dtpFecha.Text + " " + dtpHora.Text;
                var data = await Buscar(int.Parse(txtID.Text));
                txtTitulo.Text = data[0];
                txtUbicacion.Text = data[1];
                txtDescripcion.Text = data[2];
                if (data[3].Length>0)
                {
                    byte[] imagen = Convert.FromBase64String(data[3]);
                    MemoryStream ms = new MemoryStream(imagen);
                    Bitmap bitmap = new Bitmap(ms);
                    pictureBox1.Image = bitmap;
                    pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                }
                else
                {
                    pictureBox1.Image = null;
                }
                lblFechayHora.Text = "Fecha y hora previa del evento: " + data[4];
                dtpFecha.Text = data[4];
                dtpHora.Text = data[4];
                lblErrorID.Hide();
                lblTitulo.Show();
                lblUbicacion.Show();
                lblDescripcion.Show();
                lblFoto.Show();
                lblFechayHora.Show();
                lblFecha.Show();
                lblHora.Show();
                txtTitulo.Show();
                txtDescripcion.Show();
                txtUbicacion.Show();
                btnSeleccionar.Show();
                btnModificar.Show();
                dtpFecha.Show();
                dtpHora.Show();
                label1.Hide();
                txtID.Hide();
                btnBuscar.Hide();
            }
        }

        private void btnSeleccionar_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.ImageLocation = ofd.FileName;
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            }
        }

        private async void btnModificar_Click(object sender, EventArgs e)
        {
            string fechayhora = dtpFecha.Text + " " + dtpHora.Text;
            var data = await Buscar(int.Parse(txtID.Text));
            if (txtTitulo.Text == data[0] && txtUbicacion.Text == data[1] && txtDescripcion.Text == data[2] && fechayhora == data[4])
            {
                lblErrorModificar.Show();
                lblErrorModificar.Text = "Debe modificar al menos un valor";
            }
            else
            {
                if (string.IsNullOrEmpty(txtID.Text) || string.IsNullOrEmpty(txtTitulo.Text) || pictureBox1.Image==null || string.IsNullOrEmpty(fechayhora))
                {
                    lblErrorModificar.Show();
                    lblErrorModificar.Text = "Eliminó uno de los valores obligatorios";
                }
                else
                {
                    lblErrorModificar.Show();
                    lblErrorModificar.Text = "Se modificó correctamente";
                    MemoryStream ms = new MemoryStream();
                    pictureBox1.Image.Save(ms, ImageFormat.Jpeg);
                    byte[] imagen = ms.ToArray();
                    Modificar(txtID.Text, txtTitulo.Text, txtUbicacion.Text, txtDescripcion.Text, imagen, fechayhora);
                    lblFechayHora.Text = "Fecha y hora previa del evento: " + fechayhora;
                }
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Form1 f1 = new Form1();
            f1.Show();
            this.Close();
        }

 
        static async Task<string[]> Buscar(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync($"https://localhost:44340/eventoPorId?id={id}");
                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();
                    dynamic data = JsonConvert.DeserializeObject(responseBody); //sigo sin poder pasar esto a lo que quiero, no me deja acceder a la info del json de ninguna manera, tengo que hallar alguna forma de pasar los datos
                    return new string[] { data.titulo, data.ubicacion, data.descripcion, data.foto, data.fechayhora };
                }
                catch
                {
                    return null;
                }
            }
        }

        static async Task Modificar(string id, string titulo, string ubicacion, string descripcion, byte[] imagen, string fechayhora)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var data = new { id = id, titulo = titulo, ubicacion = ubicacion, descripcion = descripcion, imagen = Convert.ToBase64String(imagen), fechayhora = fechayhora };
                    var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PutAsync("https://localhost:44340/modificarEvento", content);
                    response.EnsureSuccessStatusCode();
                }
                catch (Exception ex)
                {

                }
            }
        }

        //testing
        public string modificarEvento(string idE, string title = "", string image = "", string horario = "", string ubication = "", string description = "")
        {
            string id = idE;        
            MySqlConnection conn = new MySqlConnection("Server=localhost; database=base; uID=root; pwd=;");
            conn.Open();
            try
            {

                byte[] imagen = Convert.FromBase64String(image);
                string titulo = title, fechayhora = horario, ubicacion = ubication, descripcion = description;
                MySqlCommand cmd = new MySqlCommand("UPDATE eventos SET ubicacion=@Ubicacion, titulo=@Titulo, descripcion=@Descripcion, foto=@Foto, fechayhora=@FechayHora WHERE id=@id", conn);
                if (!string.IsNullOrEmpty(ubication))
                {
                    cmd.Parameters.AddWithValue("@Ubicacion", ubicacion);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Ubicacion", null);
                }
                if (!string.IsNullOrEmpty(title))
                {
                    cmd.Parameters.AddWithValue("@Titulo", titulo);
                }
                else
                {
                    return "modificacion erronea";
                }
                if (!string.IsNullOrEmpty(description))
                {
                    cmd.Parameters.AddWithValue("@Descripcion", descripcion);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Descripcion", null);
                }
                if (!string.IsNullOrEmpty(image))
                {
                    cmd.Parameters.AddWithValue("@Foto", imagen);
                }
                else
                {
                    return "modificacion erronea";
                }
                if (!string.IsNullOrEmpty(horario))
                {
                    cmd.Parameters.AddWithValue("@FechayHora", fechayhora);
                }
                else
                {
                    return "modificacion erronea";
                }

                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
                conn.Close();
                return "modificacion correcta";
            }
            catch
            {
                return "modificación incorrecta";
            }
        }

        public string ultimoEvento()
        {
            try
            {
                MySqlConnection conn = new MySqlConnection("Server=localhost; database=base; uID=root; pwd=;");
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT id FROM eventos ORDER BY id DESC LIMIT 1", conn);
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    string id = reader["id"].ToString();
                    return id;
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }
        }

        public string conseguirEvento(int id)
        {
            try
            {
                MySqlConnection conn = new MySqlConnection("Server=localhost; database=base; uID=root; pwd=;");
                conn.Open();
                MySqlCommand command = new MySqlCommand("SELECT titulo,ubicacion,descripcion,foto,fechayhora FROM eventos WHERE id=@Id", conn);
                command.Parameters.AddWithValue("@Id", id);
                MySqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    string ubicacion;
                    if (string.IsNullOrEmpty(reader["ubicacion"].ToString()))
                    {
                        ubicacion = "";
                    }
                    else
                    {
                        ubicacion = reader["ubicacion"].ToString();
                    }
                    string descripcion;
                    if (string.IsNullOrEmpty(reader["descripcion"].ToString()))
                    {
                        descripcion = "";
                    }
                    else
                    {
                        descripcion = reader["descripcion"].ToString();
                    }
                    var data = new { titulo = reader["titulo"].ToString(), ubicacion = ubicacion, descripcion = reader["descripcion"].ToString(), foto = Convert.ToBase64String((byte[])reader["foto"]), fechayhora = reader["fechayhora"].ToString() };
                    return data.titulo;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }

}
