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
        public EditarPost()
        {
            InitializeComponent();
            pictureBox1.Hide();
            txtTexto.Hide();
            txtUrl.Hide();
            label1.Hide();
            label2.Hide();
            label3.Hide();
            btnModificar.Hide();
            btnSeleccionar.Hide();

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

        private void btnModificar_Click(object sender, EventArgs e)
        {
            MemoryStream ms=new MemoryStream();
            pictureBox1.Image.Save(ms, ImageFormat.Jpeg);
            byte[] imagen = ms.ToArray();
            Modificar(txtID.Text,txtTexto.Text,txtUrl.Text,imagen);
        }

        private async void txtBuscar_Click(object sender, EventArgs e)
        {
            //hay que solucionar esto
            var data= await Buscar(int.Parse(txtID.Text));
            txtTexto.Text = data[0];
            txtUrl.Text = data[1];
            byte[] imagen=Convert.FromBase64String(data[2]);
            MemoryStream ms=new MemoryStream(imagen);
            Bitmap bitmap = new Bitmap(ms);
            pictureBox1.Image = bitmap;
            pictureBox1.SizeMode=PictureBoxSizeMode.StretchImage;
            txtTexto.Show();
            txtUrl.Show();
            pictureBox1.Show();
            label1.Show();
            label2.Show();
            label3.Show();
            btnModificar.Show();
            btnSeleccionar.Show();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Form1 f1 = new Form1();
            f1.Show();
            this.Close();
        }

        public class PostData
        {
            public string texto { get; set; }
            public string imagen { get; set; }
            public string url { get; set; }
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
                    HttpResponseMessage response = await client.PutAsync("https://localhost:7269/modificarPost", content);
                    response.EnsureSuccessStatusCode();
                }
                catch (Exception ex)
                {

                }
            }
        }
    }
}
