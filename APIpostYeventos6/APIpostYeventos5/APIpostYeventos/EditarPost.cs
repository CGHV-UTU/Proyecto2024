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
                    await Modificar(txtID.Text, txtTexto.Text, txtUrl.Text, imagen);
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
                    var datos = new { id = id };
                    var content = new StringContent(JsonConvert.SerializeObject(datos),Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PutAsync($"https://localhost:44340/postPorId", content);
                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();
                    dynamic data = JsonConvert.DeserializeObject(responseBody); 
                    return new string[] { data.text, data.link, data.image };
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
                    var datos = new { id = id };
                    var content = new StringContent(JsonConvert.SerializeObject(datos), Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PutAsync($"https://localhost:44340/existePost", content);
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
    }
}
