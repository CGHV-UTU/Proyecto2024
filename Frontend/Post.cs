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
        private string token;
        public Post(string usuario,string token)
        {
            InitializeComponent();
            txtUrl.Visible = false;
            user = usuario;
            this.token = token;
            this.BackColor = Color.LightGray;
            this.btnUbicacion.Visible = false;
            this.txtNombre.Visible = false;
            this.txtDescripcion.Visible = false;
            this.dtpFechaInicio.MinDate = DateTime.Now;
            this.dtpFechaFinal.MinDate = DateTime.Now;
            this.dtpFechaFinal.Visible = false;
            this.dtpFechaInicio.Visible = false;
        }
        public event EventHandler Creado;
        public event EventHandler Salir;
        public event EventHandler CambiaTamaño;
        private void btnCrear_Click(object sender, EventArgs e)
        {
            switch (menuActual)
            {
                case "post":
                    if (string.IsNullOrEmpty(txtTexto.Text) && pbxImagen.Image == null && string.IsNullOrEmpty(txtUrl.Text))
                    {
                        MessageBox.Show("No puede realizar un post sin contenido", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        DateTime fechayhoraactual = DateTime.Now;
                        string fechaHoraString = fechayhoraactual.ToString("yyyy-MM-dd HH:mm:ss");
                        if (pbxImagen.Image == null)
                        {
                            byte[] data = new byte[0];
                            Publicar(txtTexto.Text, txtUrl.Text, data, fechaHoraString, token);
                            MessageBox.Show("El post se creó correctamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            Creado?.Invoke(this, EventArgs.Empty);
                        }
                        else
                        {
                            MemoryStream ms = new MemoryStream();
                            pbxImagen.Image.Save(ms, ImageFormat.Jpeg);
                            byte[] data = ms.ToArray();
                            Publicar(txtTexto.Text, txtUrl.Text, data, fechaHoraString, token);
                            MessageBox.Show("El post se creó correctamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            Creado?.Invoke(this, EventArgs.Empty);
                        }
                    }
                    break;
                case "evento":
                    if (string.IsNullOrEmpty(txtNombre.Text) && dtpFechaFinal.Value == DateTime.Now)
                    {
                        MessageBox.Show("No puede realizar un evento sin título o fecha", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        MemoryStream ms = new MemoryStream();
                        pbxImagen.Image.Save(ms, ImageFormat.Jpeg);
                        byte[] data = ms.ToArray();
                        PublicarEvento(txtNombre.Text, txtUrl.Text, data, txtDescripcion.Text, dtpFechaInicio.Text, dtpFechaFinal.Text, token);
                    }
                    break;
                case "grupo":
                    break;
            }
        }

        private void btnVideo_Click(object sender, EventArgs e)
        {
            if (txtUrl.Visible==false)
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
            else
            {
                txtUrl.Visible = false;
            }
        }

        private void btnImagen_Click(object sender, EventArgs e)
        {
            if (pbxImagen.Visible == false)
            {
                if (txtUrl.Visible == true)
                {
                    MessageBox.Show("No puede crear un post con Imagen y video", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    CambiaTamaño?.Invoke(this, EventArgs.Empty);
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
            else
            {
                pbxImagen.Visible = true;
            }
        }

        public static async Task Publicar(string texto, string url, byte[] imagen, string fechaHora, string token)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var datos = new { text = texto, link = url, image = Convert.ToBase64String(imagen), user = user , fechayhora =fechaHora, token=token};
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
        public static async Task PublicarEvento(string titulo, string ubicacion, byte[] imagen, string descripcion, string fechainicio, string fechafin, string token)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var datos = new { titulo = titulo, ubicacion= ubicacion, foto = Convert.ToBase64String(imagen), user = user, fechaYhora_Inicio = fechainicio, fechaYhora_Final=fechafin, token = token, descripcion=descripcion };
                    var content = new StringContent(JsonConvert.SerializeObject(datos), Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PostAsync("https://localhost:44340/hacerEvento", content);
                    response.EnsureSuccessStatusCode();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                    Console.ReadLine();
                }
            }
        }

        private void X_Click(object sender, EventArgs e)
        {
            Salir?.Invoke(this, EventArgs.Empty);
        }
        private string menuActual="post";
        private void lblPost_Click(object sender, EventArgs e)
        {
            this.btnUbicacion.Visible = false;
            this.btnVideo.Visible = true;
            this.txtNombre.Visible = false;
            this.txtDescripcion.Visible = false;
            this.txtTexto.Visible = true;
            this.dtpFechaFinal.Visible = false;
            this.dtpFechaInicio.Visible = false;
            menuActual = "post";
        }

        private void lblEvento_Click(object sender, EventArgs e)
        {
            this.btnUbicacion.Visible = true;
            this.btnVideo.Visible = false;
            this.btnUbicacion.Location = new Point(282, 185);
            this.txtNombre.Visible = true;
            this.txtDescripcion.Visible = true;
            this.txtTexto.Visible = false;
            this.dtpFechaFinal.Visible = true;
            this.dtpFechaInicio.Visible = true;
            menuActual = "evento";
        }

        private void lblGrupo_Click(object sender, EventArgs e)
        {
            this.btnUbicacion.Visible = false;
            this.btnVideo.Visible = false;
            this.txtNombre.Visible = true;
            this.txtDescripcion.Visible = true;
            this.txtTexto.Visible = false;
            this.dtpFechaFinal.Visible = false;
            this.dtpFechaInicio.Visible = false;
            menuActual = "grupo";
        }

        private void btnUbicacion_Click(object sender, EventArgs e)
        {
            if (txtUrl.Visible == false)
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
            else
            {
                txtUrl.Visible = false;
            }
        }
    }
}   