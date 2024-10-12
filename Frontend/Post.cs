﻿using Newtonsoft.Json;
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
            this.pnlNombre.Visible = false;
            this.txtDescripcion.Visible = false;
            this.pnlDescripcion.Visible = false;
            this.dtpFechaInicio.MinDate = DateTime.Now;
            this.dtpFechaFinal.MinDate = DateTime.Now;
            this.dtpFechaFinal.Visible = false;
            this.dtpFechaInicio.Visible = false;
            this.pnlOpcionEvento.Visible = false;
            this.pnlOpcionGrupo.Visible = false;
            this.pnlURL.Visible = false;
            lblEvento.ForeColor = Color.Gray;
            lblGrupo.ForeColor = Color.Gray;
            pnlOpcionPost.Visible = true;
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
                    if (string.IsNullOrEmpty(txtNombre.Text))
                    {
                        MessageBox.Show("No puede realizar un grupo sin nombre", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        MemoryStream ms = new MemoryStream();
                        pbxImagen.Image.Save(ms, ImageFormat.Jpeg);
                        byte[] data = ms.ToArray();
                        PublicarGrupo(txtNombre.Text, "default", data, txtDescripcion.Text, token);
                    }
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
                    pnlURL.Visible = true;
                }
            }
            else
            {
                pnlURL.Visible = false;
                txtUrl.Visible = false;
            }
        }

        private void btnImagen_Click(object sender, EventArgs e)
        {
            if (pbxImagen.Visible == false)
            {
                if (txtUrl.Visible == true && menuActual.Equals("post"))
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
        public static async Task PublicarGrupo(string nombreVisible, string configuracion, byte[] imagen, string descripcion, string token)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var datos = new { nombreVisible = nombreVisible, configuracion = configuracion, imagen = Convert.ToBase64String(imagen), nombreDeCuenta = user, token = token, descripcion = descripcion };
                    var content = new StringContent(JsonConvert.SerializeObject(datos), Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PostAsync("https://localhost:44304/RegistrarGrupo", content);
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
            lblPost.ForeColor = Color.Black;
            lblEvento.ForeColor = Color.Gray;
            lblGrupo.ForeColor = Color.Gray;
            this.pnlOpcionPost.Visible = true;
            this.pnlOpcionEvento.Visible = false;
            this.pnlOpcionGrupo.Visible = false;
            this.btnUbicacion.Visible = false;
            this.btnVideo.Visible = true;
            this.txtNombre.Visible = false;
            this.pnlNombre.Visible = false;
            this.txtDescripcion.Visible = false;
            this.pnlDescripcion.Visible = false;
            this.txtTexto.Visible = true;
            this.pnlTexto.Visible = true;
            this.dtpFechaFinal.Visible = false;
            this.dtpFechaInicio.Visible = false;
            menuActual = "post";
        }

        private void lblEvento_Click(object sender, EventArgs e)
        {
            lblEvento.ForeColor = Color.Black;
            lblPost.ForeColor = Color.Gray;
            lblGrupo.ForeColor = Color.Gray;
            this.pnlOpcionPost.Visible = false;
            this.pnlOpcionEvento.Visible = true;
            this.pnlOpcionGrupo.Visible = false;
            this.btnUbicacion.Visible = true;
            this.btnVideo.Visible = false;
            this.btnUbicacion.Location = new Point(282, 185);
            this.txtNombre.Visible = true;
            this.pnlNombre.Visible = true;
            this.txtDescripcion.Visible = true;
            this.pnlDescripcion.Visible = true;
            this.txtTexto.Visible = false;
            this.pnlTexto.Visible = false;
            this.dtpFechaFinal.Visible = true;
            this.dtpFechaInicio.Visible = true;
            menuActual = "evento";
        }

        private void lblGrupo_Click(object sender, EventArgs e)
        {
            lblGrupo.ForeColor = Color.Black;
            lblPost.ForeColor = Color.Gray;
            lblEvento.ForeColor = Color.Gray;
            this.pnlOpcionPost.Visible = false;
            this.pnlOpcionEvento.Visible = false;
            this.pnlOpcionGrupo.Visible = true;
            this.btnUbicacion.Visible = false;
            this.btnVideo.Visible = false;
            this.txtNombre.Visible = true;
            this.pnlNombre.Visible = true;
            this.txtDescripcion.Visible = true;
            this.pnlDescripcion.Visible = true;
            this.txtTexto.Visible = false;
            this.pnlTexto.Visible = false;
            this.dtpFechaFinal.Visible = false;
            this.dtpFechaInicio.Visible = false;
            this.txtUrl.Visible = false;
            this.pnlURL.Visible = false;
            menuActual = "grupo";
        }

        private void btnUbicacion_Click(object sender, EventArgs e)
        {
            if (txtUrl.Visible == false)
            {
                txtUrl.Visible = true;
                pnlURL.Visible = true;
            }
            else
            {
                txtUrl.Visible = false;
                pnlURL.Visible = false;
            }

            
        }

        private void pbxImagen_Click(object sender, EventArgs e)
        {

        }

        private void dtpFechaInicio_ValueChanged(object sender, EventArgs e)
        {

        }

        private void dtpFechaFinal_ValueChanged(object sender, EventArgs e)
        {

        }

        private void txtNombre_Enter(object sender, EventArgs e)
        {
            if (txtNombre.Text == "Nombre")
            {
                txtNombre.Text = "";
            }   
        }

        private void txtNombre_Leave(object sender, EventArgs e)
        {
            if (txtNombre.Text == "")
            {
                txtNombre.Text = "Nombre";
            }
        }

        private void txtDescripcion_Enter(object sender, EventArgs e)
        {
            if(txtDescripcion.Text == "Descripción")
            {
                txtDescripcion.Text = "";
            }
        }

        private void txtDescripcion_Leave(object sender, EventArgs e)
        {
            if (txtDescripcion.Text == "")
            {
                txtDescripcion.Text = "Descripción";
            }
        }

        private void txtTexto_Enter(object sender, EventArgs e)
        {
            if (txtTexto.Text == "Texto")
            {
                txtTexto.Text = "";
            }
        }

        private void txtTexto_Leave(object sender, EventArgs e)
        {
            if (txtTexto.Text == "")
            {
                txtTexto.Text = "Texto";
            }
        }

        private void txtUrl_Enter(object sender, EventArgs e)
        {
            if (txtUrl.Text == "URL del video")
            {
                txtUrl.Text = "";
            }
        }

        private void txtUrl_Leave(object sender, EventArgs e)
        {
            if (txtUrl.Text == "")
            {
                txtUrl.Text = "URL del video";
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pnlNombre_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pnlDescripcion_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}   