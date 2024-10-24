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
        private string idevento;
        private string nombreReal;
        public Post(string usuario, string token, string idevento = "", string nombreReal = "")//Deberíamos sacar el "" en idevento
        {
            InitializeComponent();
            txtUrl.Visible = false;
            user = usuario;
            this.idevento = idevento;
            this.token = token;
            this.nombreReal = nombreReal;
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
            this.txtCategorias.Visible = true;
            lblEvento.ForeColor = Color.Gray;
            lblGrupo.ForeColor = Color.Gray;
            pnlOpcionPost.Visible = true;
            if (!idevento.Equals("") || !nombreReal.Equals(""))
            {
                lblEvento.Visible = false;
                pnlOpcionEvento.Visible = false;
                lblGrupo.Visible = false;
                pnlOpcionGrupo.Visible = false;
                lblPost.Visible = false;
                pnlOpcionPost.Visible = false;
                this.txtCategorias.Visible = false;
            }
            Console.WriteLine(nombreReal);
        }



        public event EventHandler Creado;
        public event EventHandler Salir;
        public event EventHandler CambiaTamaño;
        private async void btnCrear_Click(object sender, EventArgs e)
        {
            if (nombreReal != "" && idevento != "")
            {
                MessageBox.Show("Se ha enviado un id de grupo y evento", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!idevento.Equals(""))
            {
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
                        await Publicar(txtTexto.Text, txtUrl.Text, data, fechaHoraString, token, idevento);
                        MessageBox.Show("El post se creó correctamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Creado?.Invoke(this, EventArgs.Empty);
                    }
                    else
                    {
                        MemoryStream ms = new MemoryStream();
                        pbxImagen.Image.Save(ms, ImageFormat.Jpeg);
                        byte[] data = ms.ToArray();
                        await Publicar(txtTexto.Text, txtUrl.Text, data, fechaHoraString, token, idevento);
                        MessageBox.Show("El post se creó correctamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Creado?.Invoke(this, EventArgs.Empty);
                    }
                }
                return;
            }

            if (!nombreReal.Equals(""))
            {
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
                        await Publicar(txtTexto.Text, txtUrl.Text, data, fechaHoraString, token, "", "", nombreReal);
                        MessageBox.Show("El post se creó correctamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Creado?.Invoke(this, EventArgs.Empty);
                    }
                    else
                    {
                        MemoryStream ms = new MemoryStream();
                        pbxImagen.Image.Save(ms, ImageFormat.Jpeg);
                        byte[] data = ms.ToArray();
                        await Publicar(txtTexto.Text, txtUrl.Text, data, fechaHoraString, token, idevento);
                        MessageBox.Show("El post se creó correctamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Creado?.Invoke(this, EventArgs.Empty);
                    }
                }
                return;
            }


            switch (menuActual)
                {
                    case "post":
                        if (string.IsNullOrEmpty(txtTexto.Text) && pbxImagen.Image == null && string.IsNullOrEmpty(txtUrl.Text))
                        {
                            MessageBox.Show("No puede realizar un post sin contenido", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        }
                        else
                        {
                            DateTime fechayhoraactual = DateTime.Now;
                            string fechaHoraString = fechayhoraactual.ToString("yyyy-MM-dd HH:mm:ss");
                            if (pbxImagen.Image == null)
                            {
                                byte[] data = new byte[0];
                                var respuesta =await Publicar(txtTexto.Text, txtUrl.Text, data, fechaHoraString, token, categoria: txtCategorias.Text);
                                MessageBox.Show("" + respuesta);
                                MessageBox.Show("El post se creó correctamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                Creado?.Invoke(this, EventArgs.Empty);
                            }
                            else
                            {
                                MemoryStream ms = new MemoryStream();
                                pbxImagen.Image.Save(ms, ImageFormat.Jpeg);
                                byte[] data = ms.ToArray();
                                var respuesta = await Publicar(txtTexto.Text, txtUrl.Text, data, fechaHoraString, token, categoria:txtCategorias.Text);
                                MessageBox.Show("" + respuesta);
                                MessageBox.Show("El post se creó correctamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                Creado?.Invoke(this, EventArgs.Empty);
                            }
                        }
                        break;
                    case "evento":
                        if (string.IsNullOrEmpty(txtNombre.Text) || dtpFechaFinal.Value < DateTime.Now)
                        {
                            MessageBox.Show("No puede realizar un evento sin título o fecha", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        }
                        if (pbxImagen.Image == null)
                        {

                            byte[] data = new byte[0];
                            await PublicarEvento(txtNombre.Text, txtUrl.Text, data, txtDescripcion.Text, dtpFechaInicio.Text, dtpFechaFinal.Text, token);
                            MessageBox.Show("El evento se creó correctamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            Creado?.Invoke(this, EventArgs.Empty);
                        }
                        else
                        {
                            MemoryStream ms = new MemoryStream();
                            pbxImagen.Image.Save(ms, ImageFormat.Jpeg);
                            byte[] data = ms.ToArray();
                            await PublicarEvento(txtNombre.Text, txtUrl.Text, data, txtDescripcion.Text, dtpFechaInicio.Text, dtpFechaFinal.Text, token);
                            MessageBox.Show("El evento se creó correctamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            Creado?.Invoke(this, EventArgs.Empty);
                        }
                        break;
                    case "grupo":
                        if (string.IsNullOrEmpty(txtNombre.Text))
                        {
                            MessageBox.Show("No puede realizar un grupo sin nombre", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        }
                        if (pbxImagen.Image == null)
                        {
                            byte[] data = new byte[0];
                            await PublicarGrupo(txtNombre.Text, "default", data, txtDescripcion.Text, token);
                            MessageBox.Show("El grupo se creó correctamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            Creado?.Invoke(this, EventArgs.Empty);
                        }
                        else
                        {
                            MemoryStream ms = new MemoryStream();
                            pbxImagen.Image.Save(ms, ImageFormat.Jpeg);
                            byte[] data = ms.ToArray();
                            await PublicarGrupo(txtNombre.Text, "default", data, txtDescripcion.Text, token);
                            MessageBox.Show("El grupo se creó correctamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            Creado?.Invoke(this, EventArgs.Empty);

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
                    txtUrl.Text = "Url de video";
                    pnlURL.Visible = true;
                }
            }
            else
            {
                pnlURL.Visible = false;
                txtUrl.Visible = false;
                txtUrl.Text = "";
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
                    ofd.Filter = "Archivos de imagen|*.png;*.jpg;*.jpeg"; //Para que sólo aparezcan fotos
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

        public static async Task<dynamic> Publicar(string texto, string url, byte[] imagen, string fechaHora, string token, string idevento="", string categoria="", string nombreReal="")
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    if (imagen.Length == 0)
                    {
                        var datos = new { text = texto, link = url, user = user, fechayhora = fechaHora, idEvento=idevento,token = token, categoria=categoria, nombreReal = nombreReal};
                        var content = new StringContent(JsonConvert.SerializeObject(datos), Encoding.UTF8, "application/json");
                        HttpResponseMessage response = await client.PostAsync("https://localhost:44340/postear", content);
                        response.EnsureSuccessStatusCode();
                        var responseBody = await response.Content.ReadAsStringAsync();
                        dynamic data = JsonConvert.DeserializeObject(responseBody);
                        return data;
                    }
                    else
                    {
                        var datos = new { text = texto, link = url, image = Convert.ToBase64String(imagen), user = user, fechayhora = fechaHora, idEvento = idevento, token = token , categoria = categoria };
                        var content = new StringContent(JsonConvert.SerializeObject(datos), Encoding.UTF8, "application/json");
                        HttpResponseMessage response = await client.PostAsync("https://localhost:44340/postear", content);
                        response.EnsureSuccessStatusCode();
                        var responseBody = await response.Content.ReadAsStringAsync();
                        dynamic data = JsonConvert.DeserializeObject(responseBody);
                        return data;
                    }
                }
                catch (Exception ex)
                {
                    return "ERROR"+ex.Message;
                }
            }
        }
        public static async Task PublicarEvento(string titulo, string ubicacion, byte[] imagen, string descripcion, string fechainicio, string fechafin, string token)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    if (imagen.Length == 0)
                    {
                        var datos = new { titulo = titulo, ubicacion = ubicacion, user = user, fechaYhora_Inicio = fechainicio, fechaYhora_Final = fechafin, token = token, descripcion = descripcion };
                        var content = new StringContent(JsonConvert.SerializeObject(datos), Encoding.UTF8, "application/json");
                        HttpResponseMessage response = await client.PostAsync("https://localhost:44340/hacerEvento", content);
                        response.EnsureSuccessStatusCode();
                    }
                    else
                    {
                        var datos = new { titulo = titulo, ubicacion = ubicacion, foto = Convert.ToBase64String(imagen), user = user, fechaYhora_Inicio = fechainicio, fechaYhora_Final = fechafin, token = token, descripcion = descripcion };
                        var content = new StringContent(JsonConvert.SerializeObject(datos), Encoding.UTF8, "application/json");
                        HttpResponseMessage response = await client.PostAsync("https://localhost:44340/hacerEvento", content);
                        response.EnsureSuccessStatusCode();
                    }
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
                    if (imagen.Length == 0)
                    {
                        var datos = new { nombreVisible = nombreVisible, configuracion = configuracion, nombreDeCuenta = user, token = token, descripcion = descripcion };
                        var content = new StringContent(JsonConvert.SerializeObject(datos), Encoding.UTF8, "application/json");
                        HttpResponseMessage response = await client.PostAsync("https://localhost:44304/RegistrarGrupo", content);
                        response.EnsureSuccessStatusCode();
                    }
                    else
                    {
                        var datos = new { nombreVisible = nombreVisible, configuracion = configuracion, imagen = Convert.ToBase64String(imagen), nombreDeCuenta = user, token = token, descripcion = descripcion };
                        var content = new StringContent(JsonConvert.SerializeObject(datos), Encoding.UTF8, "application/json");
                        HttpResponseMessage response = await client.PostAsync("https://localhost:44304/RegistrarGrupo", content);
                        response.EnsureSuccessStatusCode();
                    }
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
            this.txtCategorias.Visible = true;
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
            this.txtCategorias.Visible = false;
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
            this.txtCategorias.Visible = false;
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

        private void txtCategorias_Enter(object sender, EventArgs e)
        {
            if (txtCategorias.Text== "Categorías")
            {
                txtUrl.Text = "";
            }
        }
    }
}   