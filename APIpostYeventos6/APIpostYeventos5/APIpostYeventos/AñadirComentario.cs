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
        public AñadirComentario()
        {
            InitializeComponent();
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
            Form1 f1 = new Form1();
            f1.Show();
            this.Close();
        }

        private void btnPublicar_Click(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;
            string fechayhora = now.ToString("yyyy-MM-dd HH:mm:ss");
            if (string.IsNullOrEmpty(txtNombreDeCuenta.Text) || string.IsNullOrEmpty(txtComentario.Text))
            {
                lblError.Text = "Debe ingresar un nombre de usuario y contenido en el comentario";
                lblError.Show();
            }
            else
            {
                Publicar(txtNombreDeCuenta.Text, txtID.Text, txtComentario.Text, fechayhora);
                lblError.Text = "El comentario se creó correctamente";
                lblError.Show();
            }
        }
        static async Task Publicar(string NombreDeCuenta, string IdPost, string texto, string fechayhora)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var datos = new { NombreDeCuenta = NombreDeCuenta, IdPost = IdPost, texto = texto, fechayhora = fechayhora };
                    var content = new StringContent(JsonConvert.SerializeObject(datos), Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PostAsync("https://localhost:44340/hacerComentario", content);
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
