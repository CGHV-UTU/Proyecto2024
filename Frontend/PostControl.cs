using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
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
        public event EventHandler<PersonalizedArgs> AbrirPaginaUsuario;
        public event EventHandler RecargarFeed;
        private string modo;
        private int idpost;
        public string tipo;
        private string user;
        private string creador;
        public PostControl(int idpost, string modo, string user)
        {
            this.modo = modo;
            this.idpost = idpost;
            this.user = user;
        }

        public async Task aplicarDatos()
        {
            var data = await Buscar(idpost);
            string creador = await obtenerCreador(idpost);
            this.creador = creador;
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
            bool Like = await dioLike(user,idpost,creador);
            if (Like)
            {
                HandleLikeClick();
            }
            this.lblNombre.Text = creador;
            string imagenB64 = await conseguirImagenDelCreador(creador);
            byte[] imagen2 = Convert.FromBase64String(imagenB64);
            MemoryStream ms2 = new MemoryStream(imagen2);
            Bitmap bitmap2 = new Bitmap(ms2);
            this.PictureBoxUsuarioPost.Image = bitmap2;
        }

        static async Task<string> conseguirImagenDelCreador(string creador)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var dato = new { nombreDeCuenta = creador};
                    var content = new StringContent(JsonConvert.SerializeObject(dato), Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PutAsync($"https://localhost:44383/user/obtenerImagenUsuario", content);
                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();
                    dynamic imagen = JsonConvert.DeserializeObject(responseBody);
                    return imagen;
                }
                catch
                {
                    MessageBox.Show("Error de conexión");
                    return "error";
                }
            }
        } 

        static async Task<string[]> Buscar(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var dato = new { id=id };
                    var content = new StringContent(JsonConvert.SerializeObject(dato), Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PutAsync("https://localhost:44340/postPorId",content);
                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();
                    dynamic data = JsonConvert.DeserializeObject(responseBody); //sigo sin poder pasar esto a lo que quiero, no me deja acceder a la info del json de nin}guna manera, tengo que hallar alguna forma de pasar los datos
                    return new string[] { data.text, data.link, data.image };
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
                    var dato = new { id=idpost  };
                    var content = new StringContent(JsonConvert.SerializeObject(dato), Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PutAsync("https://localhost:44340/conseguirCreador",content);
                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();
                    dynamic data = JsonConvert.DeserializeObject(responseBody); 
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
                    HttpResponseMessage response = await client.PutAsync("https://localhost:44340/dioLike", content);
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
        static async Task<string> quitarLike(string NombreDeCuenta, int IdPost, string nombreCreador)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var datos = new { NombreDeCuenta = NombreDeCuenta, idpost = IdPost, nombredeCreador = nombreCreador };
                    var content = new StringContent(JsonConvert.SerializeObject(datos), Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PutAsync("https://localhost:44340/quitarLike", content);
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
            if (!isImage1)
            {
                string respuesta = await quitarLike(user, idpost, creador);
            }
            else
            {
                string respuesta = await darLike(user, idpost, creador);
                MessageBox.Show(respuesta);
            }
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
            this.txtDescripcion = new Label();
            this.txtUrl = new Label();
            this.SuspendLayout();

            if (this.creador.Equals(user))
            {
                //editar
                this.PictureBoxEditar = new PictureBox();
                this.PictureBoxEditar.Location = new System.Drawing.Point(497, 440);
                this.PictureBoxEditar.Name = "PictureBoxEditar";
                this.PictureBoxEditar.Size = new System.Drawing.Size(50, 50);
                this.PictureBoxEditar.SizeMode = PictureBoxSizeMode.StretchImage;
                this.PictureBoxEditar.Image = Frontend.Properties.Resources.editar_removebg_preview;
                this.PictureBoxEditar.Click += PictureBoxEditar_Click;
                this.PictureBoxEditar.Cursor = Cursors.Hand;
                this.Controls.Add(this.PictureBoxEditar);
            }

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
            this.PictureBoxUsuarioPost.Click += PictureBoxUsuarioPost_Click;

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
            this.PictureBoxOpcionesPost.Image = Frontend.Properties.Resources.mas_opciones;
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
                    if (this.creador.Equals(user))
                    {
                        this.PictureBoxEditar.Location = new Point(497, imagen.Bottom + 10);
                    }

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
                    if (this.creador.Equals(user))
                    {
                        this.PictureBoxEditar.Location = new Point(497, txtUrl.Bottom + 10);
                    }
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
                    if (this.creador.Equals(user))
                    {
                        this.PictureBoxEditar.Location = new Point(497, txtDescripcion.Bottom + 10);
                    } 
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
                    if (this.creador.Equals(user))
                    {
                        this.PictureBoxEditar.Location = new Point(497, txtUrl.Bottom + 10);
                    }
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
                this.PictureBoxOpcionesPost.Image = Properties.Resources.mas_opciones;
                this.PictureBoxCompartir.Image = Properties.Resources.compartir;
                this.PictureBoxLike.Image = Properties.Resources.like_infini;
                this.PictureBoxComentarios.Image = Properties.Resources.comentario;
                if (this.creador.Equals(user))
                {
                    this.PictureBoxEditar.Image = Frontend.Properties.Resources.editar_removebg_preview;
                }
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
        }


        public bool opciones = false;
        private void PictureBoxOpcionesPost_Click(object sender, EventArgs e)
        {
            if (!opciones)
            {
                this.imagen.SendToBack();
                this.btnReportar = new Label();
                this.btnReportar.Location = new System.Drawing.Point(this.PictureBoxComentarios.Left-100, this.PictureBoxLike.Location.Y+20);
                this.btnReportar.Name = "btnReportar";
                this.btnReportar.Size = new System.Drawing.Size(48, 20);
                this.btnReportar.TabIndex = 41;
                this.btnReportar.Click += btnReportar_Click;
                this.btnReportar.BackColor = Color.Blue;
                this.btnReportar.Text = "Reportar";
                this.btnReportar.BringToFront();
                this.Controls.Add(this.btnReportar);
                if (this.creador.Equals(user))
                {
                    this.btnEliminar = new Label();
                    this.btnEliminar.Location = new System.Drawing.Point(this.PictureBoxComentarios.Left - 100, this.btnReportar.Top - 20);
                    this.btnEliminar.Name = "btnEliminar";
                    this.btnEliminar.Size = new System.Drawing.Size(43, 20);
                    this.btnEliminar.TabIndex = 41;
                    this.btnEliminar.Click += btnEliminar_Click;
                    this.btnEliminar.BackColor = Color.Red;
                    this.btnEliminar.BringToFront();
                    this.btnEliminar.Text = "Eliminar";
                    this.Controls.Add(this.btnEliminar);
                }
                opciones = true;
            }
            else
            {
                this.Controls.Remove(this.btnReportar);
                if (this.creador.Equals(user))
                {
                    this.Controls.Remove(this.btnEliminar);
                }
                opciones = false;
            }
        }

        public bool editando = false;
        private void PictureBoxEditar_Click(object sender, EventArgs e)
        {
            if (editando==false)
            {
                editando = true;
                this.btnConfirmarCambios = new PictureBox();
                this.btnConfirmarCambios.Name = "btnConfirmarCambios"; //falta añadir las cosas para imagen
                this.btnConfirmarCambios.Size = new Size(50, 50);
                this.btnConfirmarCambios.Image = Frontend.Properties.Resources.Usuario;
                this.btnConfirmarCambios.Click += btnConfirmarCambios_Click;
                this.btnConfirmarCambios.SizeMode = PictureBoxSizeMode.StretchImage;
                this.Controls.Add(this.btnConfirmarCambios);
                switch (tipo)
                {
                    case "textAndImage":
                        this.txtDescripcion.Visible = false;
                        this.txtDescripcionEditar = new TextBox(); 
                        this.Controls.Add(this.txtDescripcionEditar);

                        // txtDescripcion
                        this.txtDescripcionEditar.Location = new Point(76, 80);
                        this.txtDescripcionEditar.Name = "txtDescripcionEditar"; 
                        this.txtDescripcionEditar.Size = new Size(634, 22);
                        this.txtDescripcionEditar.Text = this.txtDescripcion.Text;

                        // seleccionarimagen
                        this.btnSeleccionarImagen = new PictureBox();
                        this.btnSeleccionarImagen.Location = new Point(260, imagen.Bottom + 10);
                        this.btnSeleccionarImagen.Name = "btnSeleccionarImagen"; 
                        this.btnSeleccionarImagen.Size = new Size(50, 50);
                        this.btnSeleccionarImagen.Image = Frontend.Properties.Resources.buscar;
                        this.btnSeleccionarImagen.Click += btnSeleccionarImagen_Click;
                        this.btnSeleccionarImagen.SizeMode = PictureBoxSizeMode.StretchImage;
                        this.Controls.Add(this.btnSeleccionarImagen);

                        this.btnConfirmarCambios.Location = new Point(350, imagen.Bottom + 10);
                        //imagenEditar
                        this.imagen.Visible = false;
                        this.imagenEditar = new PictureBox();
                        this.imagenEditar.Location = new System.Drawing.Point(76, 69);
                        this.imagenEditar.Name = "imagenEditar";
                        this.imagenEditar.Size = new System.Drawing.Size(634, 365);
                        this.imagenEditar.SizeMode = PictureBoxSizeMode.StretchImage;
                        this.Controls.Add(this.imagenEditar);
                        break;
                    case "imageOnly": //falta esto
                        // seleccionarimagen
                        this.btnSeleccionarImagen = new PictureBox();
                        this.btnSeleccionarImagen.Location = new Point(260, imagen.Bottom + 10);
                        this.btnSeleccionarImagen.Name = "btnSeleccionarImagen"; 
                        this.btnSeleccionarImagen.Size = new Size(50, 50);
                        this.btnSeleccionarImagen.Image = Frontend.Properties.Resources.buscar;
                        this.btnSeleccionarImagen.SizeMode = PictureBoxSizeMode.StretchImage;
                        this.btnSeleccionarImagen.Click += btnSeleccionarImagen_Click;
                        this.Controls.Add(this.btnSeleccionarImagen);
                        this.btnConfirmarCambios.Location = new Point(350, imagen.Bottom + 10);

                        //imagenEditar
                        this.imagen.Visible = false;
                        this.imagenEditar = new PictureBox();
                        this.imagenEditar.Location = new System.Drawing.Point(76, 69);
                        this.imagenEditar.Name = "imagenEditar";
                        this.imagenEditar.Size = new System.Drawing.Size(634, 365);
                        this.imagenEditar.SizeMode = PictureBoxSizeMode.StretchImage;
                        this.Controls.Add(this.imagenEditar);
                        break;
                    case "textAndUrl":
                        this.txtDescripcion.Visible = false;
                        this.txtDescripcionEditar = new TextBox(); 
                        this.Controls.Add(this.txtDescripcionEditar);
                        this.txtUrl.Visible = false;
                        this.txtUrlEditar = new TextBox(); 
                        this.Controls.Add(this.txtUrlEditar);

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

                        this.btnConfirmarCambios.Location = new Point(350, txtUrlEditar.Bottom + 10);
                        break;
                    case "textOnly":
                        this.txtDescripcion.Visible = false;
                        this.txtDescripcionEditar = new TextBox();
                        this.Controls.Add(this.txtDescripcionEditar);
                        // txtDescripcion
                        this.txtDescripcionEditar.Location = new Point(76, 80);
                        this.txtDescripcionEditar.Name = "txtDescripcionEditar";
                        this.txtDescripcionEditar.Size = new Size(634, 22);
                        this.txtDescripcionEditar.Text = this.txtDescripcion.Text;
                        this.btnConfirmarCambios.Location = new Point(350, txtDescripcion.Bottom + 10);
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
                        this.btnConfirmarCambios.Location = new Point(350, txtUrl.Bottom + 10);
                        break;
                }
            }
            else
            {
                editando = false;
                switch (tipo)
                {
                    case "textAndImage":
                        this.Controls.Remove(this.txtDescripcionEditar); 
                        this.Controls.Remove(this.btnConfirmarCambios);
                        this.Controls.Remove(this.btnSeleccionarImagen);
                        this.Controls.Remove(this.imagenEditar);
                        this.txtDescripcion.Visible = true;
                        this.imagen.Visible = true;
                        break;
                    case "imageOnly":
                        this.Controls.Remove(this.btnSeleccionarImagen);
                        this.Controls.Remove(this.btnConfirmarCambios);
                        this.Controls.Remove(this.imagenEditar);
                        this.imagen.Visible = true;
                        break;
                    case "textAndUrl":
                        this.Controls.Remove(this.txtDescripcionEditar);
                        this.Controls.Remove(this.txtUrlEditar);
                        this.Controls.Remove(this.btnConfirmarCambios);
                        this.txtDescripcion.Visible = true;
                        this.txtUrl.Visible = true;
                        break;
                    case "textOnly":
                        this.Controls.Remove(this.txtDescripcionEditar);
                        this.Controls.Remove(this.btnConfirmarCambios);
                        this.txtDescripcion.Visible = true;
                        break;
                    case "urlOnly":
                        this.Controls.Remove(this.txtUrlEditar);
                        this.Controls.Remove(this.btnConfirmarCambios);
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
                    imagenEditar.Image = Image.FromFile(ofd.FileName);
                    imagenEditar.SizeMode = PictureBoxSizeMode.StretchImage;
                }
            }
        }

        static async Task<dynamic> Modificar(string id, string texto, string url, byte[] imagen)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var data = new { id = id, text = texto, link = url, image = Convert.ToBase64String(imagen) };
                    var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PutAsync("https://localhost:44340/modificarPost", content);
                    response.EnsureSuccessStatusCode();
                    var responseBody = await response.Content.ReadAsStringAsync();
                    dynamic resultado = JsonConvert.DeserializeObject(responseBody);
                    return resultado;
                }
                catch (Exception)
                {
                    MessageBox.Show("ERROR DE CONEXIÓN");
                    return "MAL";
                }
            }
        }

        private async void btnConfirmarCambios_Click(object sender, EventArgs e)
        {
            byte[] imagenfalsa = new byte[0];
            MemoryStream ms = new MemoryStream();
            string resultado;
            switch (tipo)
            {
                case "textAndImage":
                    this.imagenEditar.Image.Save(ms, ImageFormat.Jpeg);
                    byte[] imagen = ms.ToArray();
                    resultado = await Modificar(Convert.ToString(idpost), txtDescripcionEditar.Text, "", imagen);
                    this.imagen.Image = this.imagenEditar.Image;
                    this.txtDescripcion.Text = txtDescripcionEditar.Text;
                    MessageBox.Show(resultado);
                    this.Controls.Remove(this.txtDescripcionEditar);
                    this.Controls.Remove(this.btnConfirmarCambios);
                    this.Controls.Remove(this.btnSeleccionarImagen);
                    this.Controls.Remove(this.imagenEditar);
                    this.txtDescripcion.Visible = true;
                    this.imagen.Visible = true;
                    break;
                case "imageOnly": //falta esto
                    this.imagenEditar.Image.Save(ms, ImageFormat.Jpeg);
                    byte[] image = ms.ToArray();
                    resultado = await Modificar(Convert.ToString(idpost), "", "", image);
                    this.imagen.Image = this.imagenEditar.Image;
                    MessageBox.Show(resultado);
                    this.Controls.Remove(this.btnConfirmarCambios);
                    this.Controls.Remove(this.btnSeleccionarImagen);
                    this.Controls.Remove(this.imagenEditar);
                    this.txtDescripcion.Visible = true;
                    this.imagen.Visible = true;
                    break;
                case "textAndUrl":
                    resultado = await Modificar(Convert.ToString(idpost), txtDescripcionEditar.Text, txtUrlEditar.Text, imagenfalsa);
                    this.txtDescripcion.Text = txtDescripcionEditar.Text;
                    this.txtUrl.Text = txtUrlEditar.Text;
                    MessageBox.Show(resultado);
                    this.Controls.Remove(this.txtDescripcionEditar);
                    this.Controls.Remove(this.txtUrlEditar);
                    this.Controls.Remove(this.btnConfirmarCambios);
                    this.txtDescripcion.Visible = true;
                    this.txtUrl.Visible = true;
                    break;
                case "textOnly":
                    resultado = await Modificar(Convert.ToString(idpost), txtDescripcionEditar.Text, "", imagenfalsa);
                    this.txtDescripcion.Text = txtDescripcionEditar.Text;
                    MessageBox.Show(resultado);
                    this.Controls.Remove(this.txtDescripcionEditar);
                    this.Controls.Remove(this.btnConfirmarCambios);
                    this.txtDescripcion.Visible = true;
                    break;
                case "urlOnly":
                    resultado=await Modificar(Convert.ToString(idpost), "", txtUrlEditar.Text, imagenfalsa);
                    this.txtUrl.Text = txtUrlEditar.Text;
                    MessageBox.Show(resultado);
                    this.Controls.Remove(this.txtUrlEditar);
                    this.Controls.Remove(this.btnConfirmarCambios);
                    this.txtUrl.Visible = true;
                    break;
            }
        }

        static async Task<string> Eliminar(string id)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var datos = new { id=id };
                    var content = new StringContent(JsonConvert.SerializeObject(datos), Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PutAsync($"https://localhost:44340/eliminarPost",content);
                    response.EnsureSuccessStatusCode();
                    var responseBody = await response.Content.ReadAsStringAsync();
                    dynamic result = JsonConvert.DeserializeObject(responseBody);
                    MessageBox.Show(result);
                    return result;
                }
                catch (Exception)
                {
                    MessageBox.Show("Borrado incorrecto");
                    return "Borrado incorrecto";
                }
            }
        }

        private async void btnEliminar_Click(object sender, EventArgs e)
        {
            string result=await Eliminar(Convert.ToString(idpost));
            if (result.Equals("Post eliminado"))
            {
                //aca hacer que se mande un invoke a post que recargue los post existentes
                RecargarFeed?.Invoke(this, EventArgs.Empty);
            }
        }

        private void PictureBoxCompartir_Click(object sender, EventArgs e)
        {

        }

        private void btnReportar_Click(object sender, EventArgs e)
        {
            ReportarPost?.Invoke(this, new PersonalizedArgs(Convert.ToString(idpost)));
        }

        private void PictureBoxUsuarioPost_Click(object sender, EventArgs e)
        {
            AbrirPaginaUsuario?.Invoke(this, new PersonalizedArgs(creador));
        }
    }
}
