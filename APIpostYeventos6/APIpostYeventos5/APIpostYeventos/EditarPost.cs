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
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;

namespace APIpostYeventos
{
    public partial class EditarPost : Form
    {
        static MySqlConnection conn = new MySqlConnection("Server=localhost; database=base; uID=root; pwd=;");
        public static List<Post> posts = new List<Post>();
        private static string usuario;
        public EditarPost(string user)
        {
            InitializeComponent();
            CargarTabla();
            ModificarTabla();
            usuario = user;
        }

        private async void CargarTabla()
        {
            dataGridView1.DataSource = await CargarTodosLosPosts();
        }
        private void ModificarTabla()
        {
            DataGridViewCellStyle columnHeaderStyle = new DataGridViewCellStyle();
            columnHeaderStyle.BackColor = Color.Beige;
            columnHeaderStyle.Font = new Font("Verdana", 10, FontStyle.Bold);
            dataGridView1.ColumnHeadersDefaultCellStyle = columnHeaderStyle;
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
        private void btnRemover_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = null;
        }
        private void btnVolver_Click(object sender, EventArgs e)
        {
            Form1 f1 = new Form1(usuario);
            f1.Show();
            this.Close();
        }
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Form1 f1 = new Form1(usuario);
            f1.Show();
            this.Close();
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
                    btnSeleccionar.Show();
                    btnRemover.Show();
                    btnModificar.Show();
                    btnCancelar.Show();
                    lblID.Hide();
                    lblErrorID.Hide();
                    txtID.Hide();
                    btnBuscar.Hide();
                    btnVolver.Hide();
                }
                else
                {
                    lblErrorID.Show();
                    lblErrorID.Text = "No se encontró la ID";
                }
            }
        }
        private async void btnModificar_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null && !string.IsNullOrEmpty(txtUrl.Text))
            {
                MessageBox.Show("No se puede publicar una imagen y un enlace al mismo tiempo");
            }
            else
            {
                if (pictureBox1.Image == null)
                {
                    byte[] imagen = new byte[0];
                    await Modificar(txtID.Text, txtTexto.Text, txtUrl.Text, imagen);
                    MessageBox.Show("El post se modificó correctamente");
                }
                else
                {
                    var data = await Buscar(int.Parse(txtID.Text));
                    MemoryStream ms = new MemoryStream();
                    pictureBox1.Image.Save(ms, ImageFormat.Jpeg);
                    byte[] imagen = ms.ToArray();
                    Modificar(txtID.Text, txtTexto.Text, txtUrl.Text, imagen);
                    lblErrorModificar.Show();
                    MessageBox.Show("El post se modificó correctamente");
                }
                CargarTabla();
            }
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
                    dynamic data = JsonConvert.DeserializeObject(responseBody); //sigo sin poder pasar esto a lo que quiero, no me deja acceder a la info del json de ninguna manera, tengo que hallar alguna forma de pasar los datos
                    return new string[] { data.texto, data.url, data.imagen };
                }
                catch
                {
                    return null;
                }
            }
        }
        static async Task Modificar(string id, string texto, string url, byte[] imagen)
        {
            using (HttpClient client = new HttpClient()) {
                try
                {
                    var data=new {id=id, text=texto, link=url, image=Convert.ToBase64String(imagen) };
                    var content=new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8,"application/json");
                    HttpResponseMessage response = await client.PutAsync("https://localhost:44340/modificarPost", content);
                    response.EnsureSuccessStatusCode();
                }
                catch (Exception)
                {

                }
            }
        }
        static async Task<dynamic> CargarTodosLosPosts()
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync($"https://localhost:44340/seleccionarTodosLosPost");
                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();
                    DataTable tabla = JsonConvert.DeserializeObject<DataTable>(responseBody);
                    return tabla;
                }
                catch
                {
                    return "No se pudo cargar la tabla";
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
                    dynamic data = JsonConvert.DeserializeObject<bool>(responseBody); //sigo sin poder pasar esto a lo que quiero, no me deja acceder a la info del json de ninguna manera, tengo que hallar alguna forma de pasar los datos
                    return data;
                }
                catch
                {
                    return false;
                }
            }
        }


        //testing de la API
        public string modificarPost(string id,string link = "", string text = "", string image = "")
        {
            try
            {
                MySqlConnection conn = new MySqlConnection("Server=localhost; database=base; uID=root; pwd=;");
                conn.Open();
                MySqlCommand cmd;
                if (!string.IsNullOrEmpty(text))
                {
                    string texto = text;
                    if (!string.IsNullOrEmpty(link))
                    {
                        string url = link;
                        cmd = new MySqlCommand("UPDATE posts SET texto=@texto,imagen=@imagen,url=@url WHERE id=@id", conn);
                        cmd.Parameters.AddWithValue("@Texto", texto);
                        cmd.Parameters.AddWithValue("@url", url);
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.Parameters.AddWithValue("@Imagen", null);
                        cmd.ExecuteNonQuery();
                        conn.Close();
                        return "Modificacion correcta";
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(image))
                        {
                            byte[] imagen = Convert.FromBase64String(image);
                            cmd = new MySqlCommand("UPDATE posts SET texto=@texto,imagen=@imagen,url=@url WHERE id=@id", conn);
                            cmd.Parameters.AddWithValue("@Texto", texto);
                            cmd.Parameters.AddWithValue("@Imagen", imagen);
                            cmd.Parameters.AddWithValue("@id", id);
                            cmd.Parameters.AddWithValue("@url", null);
                            cmd.ExecuteNonQuery();
                            conn.Close();
                            return "Modificacion correcta";
                        }
                        else
                        {
                            cmd = new MySqlCommand("UPDATE posts SET texto=@texto,imagen=@imagen,url=@url WHERE id=@id", conn);
                            cmd.Parameters.AddWithValue("@Texto", texto);
                            cmd.Parameters.AddWithValue("@id", id);
                            cmd.Parameters.AddWithValue("@url", null);
                            cmd.Parameters.AddWithValue("@Imagen", null);
                            cmd.ExecuteNonQuery();
                            conn.Close();
                            return "Modificacion correcta";
                        }
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(image))
                    {
                        byte[] imagen = Convert.FromBase64String(image);
                        cmd = new MySqlCommand("UPDATE posts SET texto=@texto,imagen=@imagen,url=@url WHERE id=@id", conn);
                        cmd.Parameters.AddWithValue("@Imagen", imagen);
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.Parameters.AddWithValue("@Texto", null);
                        cmd.Parameters.AddWithValue("@url", null);
                        cmd.ExecuteNonQuery();
                        conn.Close();
                        return "Modificacion correcta";
                    }
                    else
                    {
                        string url = link;
                        cmd = new MySqlCommand("UPDATE posts SET texto=@texto,imagen=@imagen,url=@url WHERE id=@id", conn);
                        cmd.Parameters.AddWithValue("@url", url);
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.Parameters.AddWithValue("@Texto", null);
                        cmd.Parameters.AddWithValue("@Imagen", null);
                        cmd.ExecuteNonQuery();
                        conn.Close();
                        return "Modificacion correcta";
                    }
                }
            }
            catch (Exception)
            {
                return "Modificación incorrecta";
            }    
        }
        public string ultimoPost()
        {
            MySqlConnection conn = new MySqlConnection("Server=localhost; database=base; uID=root; pwd=;");
            conn.Open();
            MySqlCommand command = new MySqlCommand("SELECT id FROM posts ORDER BY id DESC LIMIT 1", conn);
            MySqlDataReader reader = command.ExecuteReader();
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

        public string conseguirPost(int id)
        {
            try
            {
                MySqlConnection conn = new MySqlConnection("Server=localhost; database=base; uID=root; pwd=;");
                conn.Open();
                MySqlCommand command = new MySqlCommand("SELECT texto,imagen,url FROM posts WHERE id=@Id", conn);
                command.Parameters.AddWithValue("@Id", id);
                MySqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    string texto;
                    if (string.IsNullOrEmpty(reader["texto"].ToString()))
                    {
                        texto = "";
                    }
                    else
                    {
                        texto = reader["texto"].ToString();
                    }
                    string imagen;
                    if (string.IsNullOrEmpty(reader["imagen"].ToString()))
                    {
                        imagen = "";
                    }
                    else
                    {
                        imagen = reader["imagen"].ToString();
                    }
                    string url;
                    if (string.IsNullOrEmpty(reader["url"].ToString()))
                    {
                        url = "";
                    }
                    else
                    {
                        url = reader["url"].ToString();
                    }
                    var data = new { imagen = imagen, url = url, texto = texto };
                    return texto;
                }
                else
                {
                    return "no se encuentra";
                }
            }
            catch (Exception)
            {
                return "no se encuentra";
            }
        }      
    }
}
