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

namespace Frontend
{
    public partial class Post : Form
    {
        private static string user;
        public Post(string usuario)
        {
            InitializeComponent();
            txtUrl.Visible = false;
            user = usuario;
        }


        private void btnCrear_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtTexto.Text) && pbxImagen.Image == null && string.IsNullOrEmpty(txtUrl.Text))
            {
                MessageBox.Show("No puede realizar un post sin contenido", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (pbxImagen.Image == null)
                {
                    byte[] data = new byte[0];
                    Publicar(txtTexto.Text, txtUrl.Text, data);
                    MessageBox.Show("El post se creó correctamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MemoryStream ms = new MemoryStream();
                    pbxImagen.Image.Save(ms, ImageFormat.Jpeg);
                    byte[] data = ms.ToArray();
                    Publicar(txtTexto.Text, txtUrl.Text, data);
                    MessageBox.Show("El post se creó correctamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void btnVideo_Click(object sender, EventArgs e)
        {
            if (pbxImagen.Visible == true)
            {
                MessageBox.Show("No puede crear un post con Imagen y video", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                txtUrl.Visible = true;
            }
        }

        private void btnImagen_Click(object sender, EventArgs e)
        {
            if (txtUrl.Visible == true)
            {
                MessageBox.Show("No puede crear un post con Imagen y video", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                this.Height = 692;
                btnCrear.Location = new Point(16, 445);
                OpenFileDialog ofd = new OpenFileDialog();
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    pbxImagen.ImageLocation = ofd.FileName;
                    pbxImagen.SizeMode = PictureBoxSizeMode.StretchImage;
                    pbxImagen.Visible = true;
                }
            }
        }

        public static async Task Publicar(string texto, string url, byte[] imagen)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var datos = new { text = texto, link = url, image = Convert.ToBase64String(imagen), user = user };
                    var content = new StringContent(JsonConvert.SerializeObject(datos), Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PostAsync("https://localhost:44340/postear", content);
                    response.EnsureSuccessStatusCode();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                    Console.ReadLine();
                }
            }
        }
    }
}
