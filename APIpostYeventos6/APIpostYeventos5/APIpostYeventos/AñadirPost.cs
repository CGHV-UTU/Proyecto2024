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
using System.Xml.Serialization;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;



namespace APIpostYeventos
{
    public partial class AñadirPost : Form
    {
        private static string usuario;
        public AñadirPost(string user)
        {
            InitializeComponent();
            usuario = user;
        }
        public void btnVolver_Click(object sender, EventArgs e)
        {
            Form1 f1=new Form1(usuario);
            f1.Show();
            this.Close();
        }
        public void btnSeleccionar_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd=new OpenFileDialog();

            if (ofd.ShowDialog()== DialogResult.OK)
            {
                pbxImagen.ImageLocation = ofd.FileName;
                pbxImagen.SizeMode=PictureBoxSizeMode.StretchImage;
            }
        }
        private void btnRemover_Click(object sender, EventArgs e)
        {
            pbxImagen.Image = null;
        }

        public async void btnPublicar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtTexto.Text) && pbxImagen.Image == null && string.IsNullOrEmpty(txtEnlace.Text))
            {
                MessageBox.Show("Todos los campos no pueden estar vacíos");
            }
            else
            {
                if (pbxImagen.Image != null && !string.IsNullOrEmpty(txtEnlace.Text))
                {
                    MessageBox.Show("No se puede publicar una imagen y un enlace al mismo tiempo");
                }
                else
                {
                    string fecha = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    if(pbxImagen.Image == null)
                    {
                        byte[] data = new byte[0];
                        await Publicar(txtTexto.Text, txtEnlace.Text, data, fecha);
                        MessageBox.Show("El post se creó correctamente");
                    }
                    else
                    {
                        MemoryStream ms = new MemoryStream();
                        pbxImagen.Image.Save(ms, ImageFormat.Jpeg);
                        byte[] data = ms.ToArray();
                        await Publicar(txtTexto.Text, txtEnlace.Text, data, fecha);
                        MessageBox.Show("El post se creó correctamente");
                    }

                }
            }
        }
        public static async Task Publicar(string texto,string url, byte[]imagen, string fechayhora)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var datos = new {text= texto, link=url, image=Convert.ToBase64String(imagen), user = usuario, fechayhora = fechayhora};
                    var content = new StringContent(JsonConvert.SerializeObject(datos),Encoding.UTF8,"application/json");
                    HttpResponseMessage response = await client.PostAsync("https://localhost:44340/postear", content);
                    response.EnsureSuccessStatusCode();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }
    }
}
