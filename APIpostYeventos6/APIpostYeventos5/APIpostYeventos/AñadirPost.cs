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
                        await Publicar(txtTexto.Text, txtEnlace.Text, data);
                        lblError.Text = "El post se creó correctamente";
                        lblError.Show();  
                    }
                    else
                    {
                        MemoryStream ms = new MemoryStream();
                        pbxImagen.Image.Save(ms, ImageFormat.Jpeg);
                        byte[] data = ms.ToArray();
                        await Publicar(txtTexto.Text, txtEnlace.Text, data);
                        lblError.Text = "El post se creó correctamente";
                        lblError.Show();
                    }

                }
            }
        }

        public static async Task Publicar(string texto,string url, byte[]imagen)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var datos = new {text= texto, link=url, image=Convert.ToBase64String(imagen), user = usuario};
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

        //para el testing

        public string hacerPost(string link = "", string text = "", string image="")
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
                        cmd = new MySqlCommand("INSERT INTO posts (texto,url) VALUES (@Texto,@url)", conn);
                        cmd.Parameters.AddWithValue("@Texto", texto);
                        cmd.Parameters.AddWithValue("@url", url);
                        cmd.ExecuteNonQuery();
                        conn.Close();
                        return "El post se creó correctamente";
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(image))
                        {
                            byte[] imagen = Convert.FromBase64String(image);
                            cmd = new MySqlCommand("INSERT INTO posts (texto,imagen) VALUES (@Texto,@Imagen)", conn);
                            cmd.Parameters.AddWithValue("@Texto", texto);
                            cmd.Parameters.AddWithValue("@Imagen", imagen);
                            cmd.ExecuteNonQuery();
                            conn.Close();
                            return "El post se creó correctamente";
                        }
                        else
                        {
                            cmd = new MySqlCommand("INSERT INTO posts (texto) VALUES (@Texto)", conn);
                            cmd.Parameters.AddWithValue("@Texto", texto);
                            cmd.ExecuteNonQuery();
                            conn.Close();
                            return "El post se creó correctamente";
                        }
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(image))
                    {
                        byte[] imagen = Convert.FromBase64String(image);
                        cmd = new MySqlCommand("INSERT INTO posts (imagen) VALUES (@Imagen)", conn);
                        cmd.Parameters.AddWithValue("@Imagen", imagen);
                        cmd.ExecuteNonQuery();
                        conn.Close();
                        return "El post se creó correctamente";
                    }
                    else
                    {
                        string url = link;
                        cmd = new MySqlCommand("INSERT INTO posts (url) VALUES (@url)", conn);
                        cmd.Parameters.AddWithValue("@url", url);
                        cmd.ExecuteNonQuery();
                        conn.Close();
                        return "El post se creó correctamente";
                    }
                }
            }
            catch (Exception)
            {
                return "El post no se creó";
            }
        }
    }
}
