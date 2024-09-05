using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Frontend
{
    public partial class PostControl : UserControl
    {
        public event EventHandler<PersonalizedArgs> AbrirComentarios;
        public event EventHandler<PersonalizedArgs> ReportarPost;
        private string modo;
        private int idpost;
        public string tipo;
        private string user;
        public PostControl(int idpost, string modo, string user)
        {
            this.modo = modo;
            this.idpost = idpost;
            this.user = user;
        }

        public async Task aplicarDatos()
        {
            var data = await Buscar(idpost);
            if (string.IsNullOrEmpty(data[2]))
            {
                if (string.IsNullOrEmpty(data[1]))
                {
                    this.tipo = "textOnly";
                    iniciar("textOnly");
                    txtDescripcion.Text = data[0];
                }
                else
                {
                    if (string.IsNullOrEmpty(data[0]))
                    {
                        this.tipo = "urlOnly";
                        iniciar("urlOnly");
                        txtUrl.Text = data[1];
                    }
                    else
                    {
                        this.tipo = "textAndUrl";
                        iniciar("textAndUrl");
                        txtDescripcion.Text = data[0];
                        txtUrl.Text = data[1];
                    }
                }
            }
            else
            {
                if (string.IsNullOrEmpty(data[0]))
                {
                    this.tipo = "imageOnly";
                    iniciar("imageOnly");
                    byte[] imagen = Convert.FromBase64String(data[2]);
                    MemoryStream ms = new MemoryStream(imagen);
                    Bitmap bitmap = new Bitmap(ms);
                    this.imagen.Image = bitmap;
                    this.imagen.SizeMode = PictureBoxSizeMode.StretchImage;
                }
                else
                {
                    this.tipo = "textAndImage";
                    iniciar("textAndImage");
                    txtDescripcion.Text = data[0];
                    byte[] imagen = Convert.FromBase64String(data[2]);
                    MemoryStream ms = new MemoryStream(imagen);
                    Bitmap bitmap = new Bitmap(ms);
                    this.imagen.Image = bitmap;
                    this.imagen.SizeMode = PictureBoxSizeMode.StretchImage;
                }
            }
            string creador = await obtenerCreador(idpost);
            bool Like = await dioLike(user,idpost,creador);
            if (Like)
            {
                HandleLikeClick();
            }
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
                    dynamic data = JsonConvert.DeserializeObject(responseBody); //sigo sin poder pasar esto a lo que quiero, no me deja acceder a la info del json de nin}guna manera, tengo que hallar alguna forma de pasar los datos
                    return new string[] { data.texto, data.url, data.imagen };
                }
                catch
                {
                    return null;
                }
            }
        }

        public static async Task<string> obtenerCreador(int idpost)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync($"https://localhost:44340/conseguirCreador?id={idpost}");
                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();
                    dynamic data = JsonConvert.DeserializeObject(responseBody); //sigo sin poder pasar esto a lo que quiero, no me deja acceder a la info del json de nin}guna manera, tengo que hallar alguna forma de pasar los datos
                    return data;
                }
                catch
                {
                    return null;
                }
            }
        }

        public static async Task<dynamic> AgregarNotificaciones(string user, string notificaciones)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                { 
                    var payload = new
                    {
                        user = user,
                        notificaciones = notificaciones
                    };

                    // Serializar el objeto a JSON
                    string jsonPayload = JsonConvert.SerializeObject(payload);
                    var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PostAsync("https://localhost:44340/agregarNotificaciones", content);
                    response.EnsureSuccessStatusCode();

                    return response;
                }
                catch (Exception ex)
                {
                    return $"Error al enviar la solicitud: {ex.Message}";
                }
            }
        }

        public async Task EnviarNotificacion()
        {
            try
            {
                dynamic creador = await obtenerCreador(idpost);
                if (creador != null)
                {
                    string notificacion = $"Like:Usuario le ha dado like a tu publicación";
                    dynamic response = await AgregarNotificaciones(creador, notificacion);
                    if (response != null && response.success)
                    {
                        MessageBox.Show("Like enviado con éxito a " + creador);
                    }
                    else
                    {
                        MessageBox.Show("Error al enviar la notificación.");
                    }
                }
                else
                {
                    MessageBox.Show("No se pudo obtener el creador del post.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ha ocurrido un error: " + ex.Message);
            }
        }

        static async Task<string> darLike(string NombreDeCuenta, int IdPost, string nombreCreador)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var datos = new { NombreDeCuenta = NombreDeCuenta, idpost = IdPost, nombredeCreador = nombreCreador};
                    var content = new StringContent(JsonConvert.SerializeObject(datos), Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PostAsync("https://localhost:44340/darLike", content);
                    response.EnsureSuccessStatusCode();
                    var responseBody = await response.Content.ReadAsStringAsync();
                    dynamic data = JsonConvert.DeserializeObject(responseBody);
                    return data;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("ERROR" + ex);
                    return "like erroneo";
                }
            }
        }
        static async Task<bool> dioLike(string NombreDeCuenta, int IdPost, string nombreCreador)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var datos = new { NombreDeCuenta = NombreDeCuenta, idpost = IdPost, nombredeCreador = nombreCreador };
                    var content = new StringContent(JsonConvert.SerializeObject(datos), Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PostAsync("https://localhost:44340/dioLike", content);
                    response.EnsureSuccessStatusCode();
                    var responseBody = await response.Content.ReadAsStringAsync();
                    dynamic data = JsonConvert.DeserializeObject(responseBody);
                    return data;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }

        private void ConfigurarPostControl(string postType)
        {
            //Tipos de post(puse un texto de ejemplo para que quede claro,
            //los que quedan sin texto son los solo imagen)
            switch (postType)
            {
                case "imageonly":
                    // Solo imagen, ocultar todos los TextBox
                    OcultarTextBoxes();
                    break;

                case "textandurl":
                    // Mostrar ambos TextBox para texto y URL
                    MostrarTextBoxes(true, true);
                    txtDescripcion.Text = "Este post tiene una descripción y una URL.";
                    txtUrl.Text = "Aca va la URL.";
                    txtUrl.Location = new Point(80, 125);
                    break;

                case "textonly":
                    // Mostrar solo el TextBox para texto
                    MostrarTextBoxes(true, false);
                    txtDescripcion.Text = "Este post solo tiene una descripción.";
                    break;

                case "urlonly":
                    // Mostrar solo el TextBox para URL
                    MostrarTextBoxes(false, true);
                    txtUrl.Location = new Point(100, 95);
                    txtUrl.Text = "Este post solo tiene una URL.";
                    break;
            }
        }

        // Mostrar - Ocultar los TextBox
        private void OcultarTextBoxes()
        {
            txtDescripcion.Visible = false;
            txtUrl.Visible = false;
        }
        private void MostrarTextBoxes(bool mostrarTexto, bool mostrarUrl)
        {
            txtDescripcion.Visible = mostrarTexto;
            txtUrl.Visible = mostrarUrl;
        }


        // Dar like. Puse lo de isImage porque no me andaba normal
        private bool isImage1 = true;
        private async void PictureBoxLike_Click(object sender, EventArgs e)
        {
            string creador = await obtenerCreador(idpost);
            string respuesta=await darLike(user, idpost, creador);
            HandleLikeClick();
        }

        private async Task HandleLikeClick()
        {
            if (modo.Equals("Oscuro"))
            {
                if (isImage1)
                {
                    PictureBoxLike.Image = Properties.Resources.like_claro_relleno;
                    isImage1 = false;
                    await EnviarNotificacion();
                }
                else
                {
                    PictureBoxLike.Image = Properties.Resources.like_claro;
                    isImage1 = true;
                }
            }
            else
            {
                if (isImage1)
                {
                    PictureBoxLike.Image = Properties.Resources.Like_Relleno;
                    isImage1 = false;
                }
                else
                {
                    PictureBoxLike.Image = Properties.Resources.like_infini;
                    isImage1 = true;
                }
            }
        }

        //Abrir comentarios
        private void PictureBoxComentarios_Click(object sender, EventArgs e)
        {
            AbrirComentarios?.Invoke(this, new PersonalizedArgs(""+idpost));
        }

        private async void iniciar(string postType)
        {
            this.lblNombre = new Label();
            this.PictureBoxUsuarioPost = new PictureBox();
            this.imagen = new PictureBox();
            this.PictureBoxLike = new PictureBox();
            this.PictureBoxComentarios = new PictureBox();
            this.PictureBoxCompartir = new PictureBox();
            this.PictureBoxOpcionesPost = new PictureBox();
            this.PictureBoxEditar = new PictureBox();
            this.txtDescripcion = new Label();
            this.txtUrl = new Label();
            this.SuspendLayout();

            //editar
            this.PictureBoxEditar.Location = new System.Drawing.Point(497, 440);
            this.PictureBoxEditar.Name = "PictureBoxEditar";
            this.PictureBoxEditar.Size = new System.Drawing.Size(50, 50);
            this.PictureBoxEditar.SizeMode = PictureBoxSizeMode.StretchImage;
            this.PictureBoxEditar.Image = Frontend.Properties.Resources.editar_removebg_preview;
            this.PictureBoxEditar.Click += PictureBoxEditar_Click;
            this.PictureBoxEditar.Cursor = Cursors.Hand;

            // lblTitle
            this.lblNombre.AutoSize = true;
            this.lblNombre.Location = new System.Drawing.Point(132, 50);
            this.lblNombre.Name = "lblNombre";
            this.lblNombre.Size = new System.Drawing.Size(44, 13);
            this.lblNombre.TabIndex = 28;

            // picImage
            this.PictureBoxUsuarioPost.Location = new System.Drawing.Point(76, 13);
            this.PictureBoxUsuarioPost.Name = "PictureBoxUsuarioPost";
            this.PictureBoxUsuarioPost.Size = new System.Drawing.Size(50, 50);
            this.PictureBoxUsuarioPost.SizeMode = PictureBoxSizeMode.StretchImage;
            this.PictureBoxUsuarioPost.Image = Frontend.Properties.Resources.User;
            this.PictureBoxUsuarioPost.Cursor = Cursors.Hand;

            // like
            this.PictureBoxLike.Location = new System.Drawing.Point(76, 440);
            this.PictureBoxLike.Name = "PictureBoxLike";
            this.PictureBoxLike.Size = new System.Drawing.Size(50, 50);
            this.PictureBoxLike.SizeMode = PictureBoxSizeMode.StretchImage;
            this.PictureBoxLike.Image = Frontend.Properties.Resources.like_infini;
            this.PictureBoxLike.Click += PictureBoxLike_Click; //Acá salta error cuando cambio el evento a async
            this.PictureBoxLike.Cursor = Cursors.Hand;

            // comentarios
            this.PictureBoxComentarios.Location = new System.Drawing.Point(548, 440);
            this.PictureBoxComentarios.Name = "PictureBoxComentarios";
            this.PictureBoxComentarios.Size = new System.Drawing.Size(50, 50);
            this.PictureBoxComentarios.SizeMode = PictureBoxSizeMode.StretchImage;
            this.PictureBoxComentarios.Image = Frontend.Properties.Resources.comentario;
            this.PictureBoxComentarios.Click += PictureBoxComentarios_Click;
            this.PictureBoxComentarios.Cursor = Cursors.Hand;

            // Compartir
            this.PictureBoxCompartir.Location = new System.Drawing.Point(604, 440);
            this.PictureBoxCompartir.Name = "PictureBoxCompartir";
            this.PictureBoxCompartir.Size = new System.Drawing.Size(50, 50);
            this.PictureBoxCompartir.SizeMode = PictureBoxSizeMode.StretchImage;
            this.PictureBoxCompartir.Image = Frontend.Properties.Resources.compartir;
            this.PictureBoxCompartir.Cursor = Cursors.Hand;

            // PictureBoxOpcionesPost
            this.PictureBoxOpcionesPost.Location = new System.Drawing.Point(660, 440);
            this.PictureBoxOpcionesPost.Name = "PictureBoxOpcionesPost";
            this.PictureBoxOpcionesPost.Size = new System.Drawing.Size(50, 50);
            this.PictureBoxOpcionesPost.SizeMode = PictureBoxSizeMode.StretchImage;
            this.PictureBoxOpcionesPost.Image = Frontend.Properties.Resources.reportar;
            this.PictureBoxOpcionesPost.Click += PictureBoxOpcionesPost_Click;
            this.PictureBoxOpcionesPost.Cursor = Cursors.Hand;

            // imagen
            this.imagen.Location = new System.Drawing.Point(76, 69);
            this.imagen.Name = "imagen";
            this.imagen.Size = new System.Drawing.Size(634, 365);
            this.imagen.SizeMode = PictureBoxSizeMode.StretchImage;


            // txtDescripcion
            this.txtDescripcion.Location = new Point(76, 80);
            this.txtDescripcion.Name = "txtDescripcion";
            this.txtDescripcion.Size = new Size(634, 22);

            // txtUrl
            this.txtUrl.Location = new Point(76, 410); // Justo debajo de la imagen
            this.txtUrl.Name = "txtUrl";
            this.txtUrl.Size = new Size(634, 22); // Ajusta el tamaño según necesidad      

            // PostControl
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.Controls.Add(this.PictureBoxUsuarioPost);
            this.Controls.Add(this.lblNombre);
            this.Controls.Add(this.imagen);
            this.Controls.Add(this.PictureBoxLike);
            this.Controls.Add(this.PictureBoxComentarios);
            this.Controls.Add(this.PictureBoxCompartir);
            this.Controls.Add(this.PictureBoxOpcionesPost);
            this.Controls.Add(this.txtDescripcion);
            this.Controls.Add(this.txtUrl);
            this.Controls.Add(this.PictureBoxEditar);
            this.Name = "PostControl";
            this.Size = new System.Drawing.Size(787, 578);

            //Tipos de post(puse un texto de ejemplo para que quede claro,
            //los que quedan sin texto son los de imagen)
            switch (postType)
            {
                case "textAndImage":
                    this.txtDescripcion.Visible = true;
                    this.txtDescripcion.Text = "Texto e imagen";
                    this.imagen.Visible = true;
                    this.txtUrl.Visible = false;
                    this.imagen.Location = new System.Drawing.Point(76, 102);
                    this.PictureBoxLike.Location = new Point(76, imagen.Bottom + 10);
                    this.PictureBoxComentarios.Location = new Point(548, imagen.Bottom + 10);
                    this.PictureBoxCompartir.Location = new Point(604, imagen.Bottom + 10);
                    this.PictureBoxOpcionesPost.Location = new Point(660, imagen.Bottom + 10);
                    this.PictureBoxEditar.Location = new Point(497, imagen.Bottom + 10);
                    break;

                case "imageOnly":
                    this.txtDescripcion.Visible = false;
                    this.txtUrl.Visible = false;
                    this.imagen.Visible = true;
                    break;

                case "textAndUrl":
                    this.txtDescripcion.Text = "Este post tiene una descripción y una URL.";
                    this.txtUrl.Text = "Aca va la URL.";
                    this.txtDescripcion.Visible = true;
                    this.txtUrl.Visible = true;
                    this.txtDescripcion.Location = new Point(76, 85);
                    this.txtUrl.Location = new Point(76, txtDescripcion.Bottom + 10);
                    this.PictureBoxLike.Location = new Point(76, txtUrl.Bottom + 10);
                    this.PictureBoxComentarios.Location = new Point(548, txtUrl.Bottom + 10);
                    this.PictureBoxCompartir.Location = new Point(604, txtUrl.Bottom + 10);
                    this.PictureBoxOpcionesPost.Location = new Point(660, txtUrl.Bottom + 10);
                    this.PictureBoxEditar.Location = new Point(497, txtUrl.Bottom + 10);
                    this.imagen.Visible = false;
                    this.Height = PictureBoxLike.Bottom + 10;
                    break;

                case "textOnly":
                    this.txtDescripcion.Text = "Este post solo tiene una descripción.";
                    this.txtDescripcion.Visible = true;
                    this.txtUrl.Visible = false;
                    this.imagen.Visible = false;
                    this.PictureBoxLike.Location = new Point(76, txtDescripcion.Bottom + 10);
                    this.PictureBoxComentarios.Location = new Point(548, txtDescripcion.Bottom + 10);
                    this.PictureBoxCompartir.Location = new Point(604, txtDescripcion.Bottom + 10);
                    this.PictureBoxOpcionesPost.Location = new Point(660, txtDescripcion.Bottom + 10);
                    this.PictureBoxEditar.Location = new Point(497, txtDescripcion.Bottom + 10);
                    this.Height = PictureBoxLike.Bottom + 10;
                    break;

                case "urlOnly":
                    this.txtUrl.Location = new Point(76, 80);
                    this.txtUrl.Text = "Este post solo tiene una URL.";
                    this.txtDescripcion.Visible = false;
                    this.txtUrl.Visible = true;
                    this.imagen.Visible = false;
                    this.PictureBoxLike.Location = new Point(76, txtUrl.Bottom + 10);
                    this.PictureBoxComentarios.Location = new Point(548, txtUrl.Bottom + 10);
                    this.PictureBoxCompartir.Location = new Point(604, txtUrl.Bottom + 10);
                    this.PictureBoxOpcionesPost.Location = new Point(660, txtUrl.Bottom + 10);
                    this.PictureBoxEditar.Location = new Point(497, txtUrl.Bottom + 10);
                    break;
            }
            if (modo.Equals("Oscuro"))
            {
                this.PictureBoxOpcionesPost.Image = Properties.Resources.mas_opciones_claro_relleno;
                this.PictureBoxCompartir.Image = Properties.Resources.compartir_claro;
                this.PictureBoxLike.Image = Properties.Resources.like_claro;
                this.PictureBoxComentarios.Image = Properties.Resources.comentario_claro;
                this.PictureBoxEditar.Image = Frontend.Properties.Resources.editar_removebg_preview;
                this.txtUrl.ForeColor = Color.White;
                this.txtDescripcion.ForeColor = Color.White;
                this.lblNombre.ForeColor = Color.White;
            }
            else
            {
                this.PictureBoxOpcionesPost.Image = Properties.Resources.reportar;
                this.PictureBoxCompartir.Image = Properties.Resources.compartir;
                this.PictureBoxLike.Image = Properties.Resources.like_infini;
                this.PictureBoxComentarios.Image = Properties.Resources.comentario;
                this.PictureBoxEditar.Image = Frontend.Properties.Resources.editar_removebg_preview;
                this.txtUrl.ForeColor = Color.Black;
                this.txtDescripcion.ForeColor = Color.Black;
                this.lblNombre.ForeColor = Color.Black;
            }
            this.ResumeLayout(false);
            this.PerformLayout();
            string creador = await obtenerCreador(idpost);
            if (creador.Equals(user))
            {
                this.PictureBoxEditar.Visible = true;
            }
            else
            {
                this.PictureBoxEditar.Visible = false;
            }
        }



        private void PictureBoxOpcionesPost_Click(object sender, EventArgs e)
        {
            ReportarPost?.Invoke(this, new PersonalizedArgs("" + idpost));
        }

        public bool editando = false;
        private void PictureBoxEditar_Click(object sender, EventArgs e)
        {
            if (editando==false)
            {
                editando = true;
                this.btnConfirmarCambios = new PictureBox();
                
                switch (tipo)
                {
                    case "textAndImage":
                        this.txtDescripcion.Visible = false;
                        this.txtDescripcionEditar = new TextBox(); //falla acá
                        this.Controls.Add(this.txtDescripcionEditar);
                        // txtDescripcion
                        this.txtDescripcionEditar.Location = new Point(76, 80);
                        this.txtDescripcionEditar.Name = "txtDescripcionEditar"; //falta añadir las cosas para imagen
                        this.txtDescripcionEditar.Size = new Size(634, 22);
                        this.txtDescripcionEditar.Text = this.txtDescripcion.Text;
                        // seleccionarimagen
                        this.btnSeleccionarImagen = new PictureBox();
                        this.Controls.Add(this.btnSeleccionarImagen);
                        this.btnSeleccionarImagen.Location = new Point(260, imagen.Bottom + 10);
                        this.btnSeleccionarImagen.Name = "btnSeleccionarImagen"; //falta añadir las cosas para imagen
                        this.btnSeleccionarImagen.Size = new Size(50, 50);
                        this.btnSeleccionarImagen.Image = Frontend.Properties.Resources.editar_removebg_preview;
                        this.btnSeleccionarImagen.Click += btnSeleccionarImagen_Click;
                        break;
                    case "imageOnly": //falta esto
                        break;
                    case "textAndUrl":
                        this.txtDescripcionEditar.Visible = true;
                        this.txtDescripcion.Visible = false;
                        this.txtUrlEditar.Visible = true;
                        this.txtUrl.Visible = false;
                        this.txtDescripcionEditar.Location = new Point(76, 85);
                        this.txtUrlEditar.Location = new Point(76, txtDescripcion.Bottom + 10);
                        this.txtDescripcionEditar.Text = this.txtDescripcion.Text;
                        this.txtUrlEditar.Text = this.txtUrl.Text;
                        this.Controls.Add(this.txtDescripcionEditar);
                        this.Controls.Add(this.txtUrlEditar);
                        break;
                    case "textOnly":
                        this.txtDescripcion.Visible = false;
                        this.txtDescripcionEditar = new TextBox();
                        this.Controls.Add(this.txtDescripcionEditar);
                        // txtDescripcion
                        this.txtDescripcionEditar.Location = new Point(76, 80);
                        this.txtDescripcionEditar.Name = "txtDescripcionEditar";
                        this.txtDescripcionEditar.Size = new Size(634, 22);
                        break;
                    case "urlOnly":
                        this.txtUrl.Visible = false;
                        this.txtUrlEditar = new TextBox();
                        this.Controls.Add(this.txtUrlEditar);
                        // txtDescripcion
                        this.txtUrlEditar.Location = new Point(76, 80);
                        this.txtUrlEditar.Name = "txtUrlEditar";
                        this.txtUrlEditar.Size = new Size(634, 22);
                        this.txtUrlEditar.Text = this.txtUrl.Text;
                        break;
                }
            }
            else
            {
                editando = false;
                switch (tipo)
                {
                    case "textAndImage":
                        this.Controls.Remove(this.txtDescripcionEditar); //falta imagen
                        this.txtDescripcion.Visible = true;
                        break;
                    case "imageOnly": //falta esto
                        break;
                    case "textAndUrl":
                        this.Controls.Remove(this.txtDescripcionEditar);
                        this.Controls.Remove(this.txtUrlEditar);
                        this.txtDescripcion.Visible = true;
                        this.txtUrl.Visible = true;
                        break;
                    case "textOnly":
                        this.Controls.Remove(this.txtDescripcionEditar);
                        this.txtDescripcion.Visible = true;
                        break;
                    case "urlOnly":
                        this.Controls.Remove(this.txtUrlEditar);
                        this.txtUrl.Visible = true;
                        break;
                }
            }
        }

        private void btnSeleccionarImagen_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    imagen.Image = Image.FromFile(ofd.FileName);
                    imagen.SizeMode = PictureBoxSizeMode.StretchImage;
                }
            }
        }

        private void btnConfirmarCambios_Click(object sender, EventArgs e)
        {

        }
    }
}
