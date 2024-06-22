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
using System.Text;
using System.Threading.Tasks;


namespace APIpostYeventos
{
    public partial class AñadirPost : Form
    {
        public AñadirPost()
        {
            InitializeComponent();
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            Form1 f1=new Form1();
            f1.Show();
            this.Close();
        }

        private void btnSeleccionar_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd=new OpenFileDialog();

            if (ofd.ShowDialog()== DialogResult.OK)
            {
                pbxImagen.ImageLocation = ofd.FileName;
                pbxImagen.SizeMode=PictureBoxSizeMode.StretchImage;
            }
        }

        private void btnPublicar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtTexto.Text) && pbxImagen.Image == null && string.IsNullOrEmpty(txtEnlace.Text))
            {
                lblError.Show();
                lblError.Text = "Todos los campos no pueden estar vacíos";
            }
            else
            {
                if (pbxImagen.Image != null && !string.IsNullOrEmpty(txtEnlace.Text))
                {
                    lblError.Show();
                    lblError.Text = "No se puede publicar una imagen y un enlace al mismo tiempo";
                }
                else
                {
                    if(pbxImagen.Image == null)
                    {
                        byte[] data = new byte[0];
                        Publicar(txtTexto.Text, txtEnlace.Text, data);
                        lblError.Text = "El post se creó correctamente";
                        lblError.Show();
                        
                    }
                    else
                    {
                        MemoryStream ms = new MemoryStream();
                        pbxImagen.Image.Save(ms, ImageFormat.Jpeg);
                        byte[] data = ms.ToArray();
                        Publicar(txtTexto.Text, txtEnlace.Text, data);
                        lblError.Text = "El post se creó correctamente";
                        lblError.Show();
                    }

                }
            }
        }

        static async Task Publicar(string texto,string url, byte[]imagen)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var datos = new {text= texto, link=url, image=Convert.ToBase64String(imagen)};
                    var content = new StringContent(JsonConvert.SerializeObject(datos),Encoding.UTF8,"application/json");
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
