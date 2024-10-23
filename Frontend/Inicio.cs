using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Frontend
{
    public partial class Inicio : Form
    {
        private string user;
        private string token;
        private string idioma;
        private string modo;
        public Inicio(string usuario, string token)
        {
            InitializeComponent();
            user = usuario;
            this.token = token;
            VerPosts();
            PanelComentarios.Visible = false;
            PictureBoxSalir.Visible = false;
            PanelPostear.Visible = false;
            PanelNotificaciones.Visible = false;
            PanelMostrarUsuario.Visible = false;
            panelBusqueda.Visible = false;
            cargarLaImagen(); //hacer que se muestre circular
        }

        private async void cargarLaImagen()
        {
            string imagenB64 = await conseguirImagenDePerfil(user, token);
            byte[] imagen = Convert.FromBase64String(imagenB64);
            MemoryStream ms = new MemoryStream(imagen);
            Bitmap bitmap = new Bitmap(ms);
            this.PictureBoxUsuario.Image = bitmap;
        }
        static async Task<string> conseguirImagenDePerfil(string creador, string token)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var dato = new { nombreDeCuenta = creador, token = token };
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

        // Método para inicializar componentes adicionales, incluido el PanelNotificaciones
        private void IniciarComponentesAdicionales()
        {
            // Inicializar el PanelNotificaciones
            this.PanelNotificaciones = new Panel();
            this.PanelNotificaciones.AutoScroll = true;
            this.PanelNotificaciones.Dock = DockStyle.Fill;
            this.PanelNotificaciones.Location = new Point(0, 0);
            this.PanelNotificaciones.Name = "PanelNotificaciones";
            this.PanelNotificaciones.Size = new Size(500, 100);
            this.PanelNotificaciones.TabIndex = 0;
            this.PanelNotificaciones.Visible = false; 
            this.Controls.Add(this.PanelNotificaciones);
        }

        private void PictureBoxNotificaciones_Click(object sender, EventArgs e)
        {
            if (!PanelNotificaciones.Visible)
            {
                //MostrarNotificacionesEjemplo(); // Método para mostrar las notificaciones de ejemplo
                MostrarNotificacionesReales();
                PanelNotificaciones.Visible = true; // Mostrar el panel de notificaciones
            } else {
                PanelNotificaciones.Visible = false; // Quitar el panel de notificaciones
            }
           
        }

        private void MostrarNotificacionesEjemplo()
        {
            PanelNotificaciones.Controls.Clear(); // Limpiar cualquier notificación existente
            int margin = 10;
            for (int i = 0; i < 5; i++)
            {
                NotificacionControl notificacionControl = new NotificacionControl($"Notificación ejemplo {i + 1}");
                notificacionControl.Size = new Size(500 - margin * 2, 100);
                notificacionControl.Location = new Point(margin, i * (notificacionControl.Height + margin));
                PanelNotificaciones.Controls.Add(notificacionControl);
            }
        }

        // Las notificaciones son un string largo. Cada una se va a separar con un ";" 
        // y luego se va a separar en otros dos campos con ":": Tipo y el texto.
        // El tipo quiero usarlo para mostrar diferentes íconos de notificación (like, etiquetado,
        // algo con usuario, cosas así).
        // 
        // EJ:
        //          Like : Texto ; Etiquetado : Texto
        //          
        // Cuando nos pasemos de las 4-5 notificaciones, frenamos.
        // Rellenar el panel de notificaciones con un for cuando tenemos menos de 4-5 notificaciones
        // podría dar error. 

        private async void MostrarNotificacionesReales()
        {
            PanelNotificaciones.Controls.Clear(); // Limpio el panel
            int margin = 10;
            string notificaciones = await conseguirNotificaciones(user, token);
            string[] notificacionesArray = notificaciones.Split(';');

            // Me fijo si tiene algo
            if (notificacionesArray.Length > 0 && !string.IsNullOrEmpty(notificacionesArray[0]))
            {
                for (int i = 0; i < notificacionesArray.Length && i < 10; i++)
                {
                    // Cada notificación tiene este formato  "Tipo:Texto". Lo dividimos con el ":"
                    string[] partes = notificacionesArray[i].Split(':');

                    // Verificamos que haya exactamente 2 partes, tipo y texto
                    if (partes.Length == 2)
                    {
                        string tipo = partes[0];  // Tipo de la notificación
                        string texto = partes[1]; // Texto de la notificación

                        // Crear control de notificación con el texto
                        NotificacionControl notificacionControl = new NotificacionControl(texto);
                        notificacionControl.Size = new Size(500 - margin * 2, 100);
                        notificacionControl.Location = new Point(margin, i * (notificacionControl.Height + margin));
                        cambiarFotoNotificaciones(tipo, notificacionControl);
                        PanelNotificaciones.Controls.Add(notificacionControl);
                    }
                }
            }
            else
            {
                NotificacionControl notificacionControl = new NotificacionControl("No tienes notificaciones");
                notificacionControl.Size = new Size(500 - margin * 2, 100);
                notificacionControl.ImagenNotificacion = Properties.Resources.buscar;
               // notificacionControl.Location = new Point(margin, i * (notificacionControl.Height + margin));
                PanelNotificaciones.Controls.Add(notificacionControl);
            }
            
        }

        // cargar form de posts. -Puse un fondo gris para distinguirlo    
        private async void VerPosts()
        {
            string config = await conseguirConfig(user, token);
            string[] configure = config.Split(';');    
            idioma = configure[1];
            this.modo = configure[0];
            Posts post = new Posts(configure[0],user, token);
            if (configure[0].Equals("Oscuro"))
            {
                BackColor = Color.FromArgb(20, 20, 20);
                post.BackColor = Color.FromArgb(40, 40, 40);
            }
            else
            {
                post.BackColor = Color.LightGray;
            }
            post.TopLevel = false;
            post.FormBorderStyle = FormBorderStyle.None;
            post.Dock = DockStyle.Fill;
            post.AbrirComentarios += PostControl_AbrirComentarios;
            post.ReportarPost += PostControl_ReportarPost;
            post.AbrirPaginaUsuario += PostControl_AbrirPaginaUsuario;
            post.Compartir += PostControl_Compartir;
            PanelPosts.Controls.Add(post);
            post.Show();
        }
        private void PostControl_AbrirComentarios(object sender, PersonalizedArgs e)
        {
            VerComentarios(e.arg);
            PanelComentarios.Visible = true;
            PictureBoxSalir.Visible = true;
        }
        private void PostControl_ReportarPost(object sender, PersonalizedArgs e)
        {
            ReportarPost(e.arg, token);
        }
        private void PostControl_AbrirPaginaUsuario(object sender, PersonalizedArgs e)
        {
            PanelComentarios.Visible = false;
            PanelPosts.Visible = false;
            PanelMostrarUsuario.Visible = true;
            PanelMostrarUsuario.Parent = this;
            PanelMostrarUsuario.Location = PanelPosts.Location;
            PaginaDeUsuario paginaDeUsuario = new PaginaDeUsuario(e.arg, modo, user, token);
            paginaDeUsuario.TopLevel = false;
            paginaDeUsuario.FormBorderStyle = FormBorderStyle.None;
            paginaDeUsuario.BackColor = Color.LightGray;
            paginaDeUsuario.Dock = DockStyle.Fill;
            PanelMostrarUsuario.BackColor = Color.LightGray;
          //  paginaDeUsuario.BackColor = Color.FromArgb(34, 67, 220);
            paginaDeUsuario.ReportarPost += PostControl_ReportarPost;
            paginaDeUsuario.AbrirComentarios += PostControl_AbrirComentarios;
            PanelMostrarUsuario.Controls.Add(paginaDeUsuario);
            paginaDeUsuario.Show();
        }

        private void PostControl_Compartir(object sender, PersonalizedArgs e)
        {
            PanelComentarios.Visible = false;
            PanelPosts.Visible = false;
            PanelMostrarUsuario.Visible = true;
            PanelMostrarUsuario.Parent = this;
            PanelMostrarUsuario.Location = PanelPosts.Location;
            Comunidad comunidad = new Comunidad(modo, user, token, Convert.ToString(e.arg));
            comunidad.TopLevel = false;
            comunidad.FormBorderStyle = FormBorderStyle.None;
            comunidad.BackColor = Color.LightGray;
            comunidad.Dock = DockStyle.Fill;
            PanelMostrarUsuario.BackColor = Color.LightGray;
            PanelMostrarUsuario.Controls.Add(comunidad);
            comunidad.Show();
        }

        private void ReportarPost(string idpost, string token, string idcomentario="")
        {
            PanelComentarios.Visible = false;
            PanelPostear.Controls.Clear();
            PanelPostear.Visible = true;
            PanelPostear.Parent = this;
            PanelPosts.Visible = false;
            ReportarPost post = new ReportarPost(idpost, user, token, idcomentario);
            post.TopLevel = false;
            post.FormBorderStyle = FormBorderStyle.None;
            post.BackColor = Color.LightGray;
            post.Dock = DockStyle.Fill;
           // post.BackColor = Color.FromArgb(34, 67, 220);
            PanelPostear.Controls.Add(post);
            post.Show();
        }
        private async void VerComentarios(string idpost)
        {
            string config = await conseguirConfig(user, token);
            string[] configure = config.Split(';');
            Comentarios comentario = new Comentarios(configure[0],idpost,user, token);
            comentario.TopLevel = false;
            comentario.FormBorderStyle = FormBorderStyle.None;
            comentario.BackColor = Color.LightGray;
            comentario.Dock = DockStyle.Fill;
            comentario.ReportarComentario += CommentControl_ReportarComentario;
            PanelComentarios.Controls.Add(comentario);
            comentario.Show();
        }

        private void CommentControl_ReportarComentario(object sender, PersonalizedArgs e)
        {
            ReportarPost(e.arg, token, e.arg2);
        }

        private void PictureBoxSalir_Click(object sender, EventArgs e)
        {
            PanelComentarios.Visible = false;
            PictureBoxSalir.Visible = false;
            PanelComentarios.Controls.Clear();
        }

        private void PictureBoxCrear_Click(object sender, EventArgs e)
        {
            VerPost();
        }
        private void VerPost(string idevento="")
        {
            PanelPostear.Controls.Clear();
            PanelPostear.Visible = true;
            PanelPostear.Parent = this;
            PanelPosts.Visible = false; 
            Post post = new Post(user, token, idevento);
            post.TopLevel = false;
            post.FormBorderStyle = FormBorderStyle.None;
            post.BackColor = Color.White;
            post.Dock = DockStyle.Fill;
            post.Creado += Post_Creado;
            post.Salir += Post_Salir;
            post.CambiaTamaño += Post_CambiaTamaño;
            // post.BackColor = Color.FromArgb(34, 67, 220);
            PanelPostear.BackColor = Color.LightGray;
            PanelPostear.Controls.Add(post);
            post.Show();
        }
        private void Post_Creado(object sender, EventArgs e)
        {
            PanelPostear.Visible = false;
            PanelPosts.Controls.Clear();
            VerPosts();
            PanelPosts.Visible = true;
        }
        private void Post_Salir(object sender,EventArgs e)
        {
            PanelPostear.Visible = false;
            PanelPosts.Visible = true;
        }
        private void Post_CambiaTamaño(object sender, EventArgs e)
        {
            PanelPostear.Height = 692;
        }

        private void PictureBoxConfiguraciones_Click(object sender, EventArgs e)
        {
            PanelPostear.Controls.Clear();
            PanelPostear.Visible = true;
            PanelPostear.Parent = this;
            PanelPosts.Visible = false;
            Configuracion config = new Configuracion(user, token);
            config.TopLevel = false;
            config.FormBorderStyle = FormBorderStyle.None;
            config.BackColor = Color.LightGray;
            config.Dock = DockStyle.Fill;
            config.CambiarModo += CambiarModo;
            PanelPostear.Controls.Add(config);
            config.Show();
        }
      
        private void CambiarModo(object sender, ConfiguraEventArgs e)
        {
            if (e.Modo.Equals("Claro"))
            {
                BackColor = Color.White;
            }
            else
            {
                BackColor = Color.FromArgb(20, 20, 20);
            }
            idioma = e.Idioma;
            PanelPosts.Controls.Clear();
            VerPosts();
        }

        private void PictureboxLogo_Click(object sender, EventArgs e)
        {
            PanelPostear.Visible = false;
            PanelPosts.Visible = true;
            panelBusqueda.Visible = false;
            PanelMostrarUsuario.Controls.Clear();
        }
         
        private void cambiarFotoNotificaciones(string tipo, NotificacionControl control)
        {
           // string[] tipo = notificacion.Split(':');
            switch (tipo)
            {
                case ("Like"):
                    control.ImagenNotificacion = Properties.Resources.notificacionLike;
                    break;

                case ("Etiquetado"):
                    control.ImagenNotificacion = Properties.Resources.mas_opciones;
                    break;

                default:
                    control.ImagenNotificacion = Properties.Resources.campana;
                    break;
        
            }

        }

        public static async Task<string> conseguirConfig(string usuario, string token)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var datos = new { nombreDeCuenta = usuario, token=token };
                    var content = new StringContent(JsonConvert.SerializeObject(datos), Encoding.UTF8, "application/json");
                    var response = await client.PostAsync("https://localhost:44383/user/ConseguirConfiguracion", content);
                    var responseBody = await response.Content.ReadAsStringAsync();
                    dynamic data = JsonConvert.DeserializeObject(responseBody);
                    return data;
                }
            }
            catch
            {
                return "fallido";
            }
            
        }

        public static async Task<string> conseguirNotificaciones(string usuario,string token)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var datos = new { nombreDeCuenta = usuario, token };
                    var content = new StringContent(JsonConvert.SerializeObject(datos), Encoding.UTF8, "application/json");
                    var response = await client.PostAsync("https://localhost:44383/user/ConseguirNotificaciones", content);
                    var responseBody = await response.Content.ReadAsStringAsync();
                    dynamic data = JsonConvert.DeserializeObject(responseBody);
                    return data;
                }
            }
            catch (Exception ex)
            {
                return "fallido: " + ex.Message;
            }
        }

       

        private void PictureBoxComunidad_Click(object sender, EventArgs e)
        {
            PanelComentarios.Visible = false;
            PanelPosts.Visible = false;
            PanelMostrarUsuario.Visible = true;
            PanelMostrarUsuario.Parent = this;
            PanelMostrarUsuario.Location = PanelPosts.Location;
            Comunidad comunidad = new Comunidad(modo, user, token);
            comunidad.TopLevel = false;
            comunidad.FormBorderStyle = FormBorderStyle.None;
            comunidad.BackColor = Color.LightGray;
            comunidad.Dock = DockStyle.Fill;
            PanelMostrarUsuario.BackColor = Color.LightGray;
            //comunidad.BackColor = Color.FromArgb(34, 67, 220);
            //comunidad.ReportarPost += PostControl_ReportarPost;
            comunidad.AbrirEvento += Grupo_EventoParaListar_AbrirEvento;
            comunidad.AbrirGrupo += Grupo_EventoParaListar_AbrirGrupo;
            
            PanelMostrarUsuario.Controls.Add(comunidad);
            comunidad.Show();
        }
        private void Grupo_EventoParaListar_AbrirEvento(object sender, PersonalizedArgs e)
        {
            PanelMostrarUsuario.Controls.Clear();
            PanelComentarios.Visible = false;
            PanelPosts.Visible = false;
            PanelMostrarUsuario.Visible = true;
            PanelMostrarUsuario.Parent = this;
            PanelMostrarUsuario.Location = PanelPosts.Location;
            EventoComunidad comunidad = new EventoComunidad(e.arg, user, token, modo);
            comunidad.TopLevel = false;
            comunidad.FormBorderStyle = FormBorderStyle.None;
            comunidad.BackColor = Color.LightGray;
            comunidad.Dock = DockStyle.Fill;
            PanelMostrarUsuario.BackColor = Color.LightGray;
            //comunidad.BackColor = Color.FromArgb(34, 67, 220);
            comunidad.PostearEnEvento+= EventoComunidad_PostearEnEvento;
            //comunidad.AbrirEvento += PostControl_AbrirComentarios;
            PanelMostrarUsuario.Controls.Add(comunidad);
            comunidad.Show();
        }

        private void Grupo_EventoParaListar_AbrirGrupo(object sender, PersonalizedArgs e)
        {
            PanelMostrarUsuario.Controls.Clear();
            PanelComentarios.Visible = false;
            PanelPosts.Visible = false;
            PanelMostrarUsuario.Visible = true;
            PanelMostrarUsuario.Parent = this;
            PanelMostrarUsuario.Location = PanelPosts.Location;
            GruposComunidad comunidad = new GruposComunidad(e.arg, user, token);
            comunidad.TopLevel = false;
            comunidad.FormBorderStyle = FormBorderStyle.None;
            comunidad.BackColor = Color.LightGray;
            comunidad.Dock = DockStyle.Fill;
            PanelMostrarUsuario.BackColor = Color.LightGray;
            PanelMostrarUsuario.Controls.Add(comunidad);
            comunidad.Show();
            comunidad.MensajesNuevos();
        }

        private void EventoComunidad_PostearEnEvento(object sender, PersonalizedArgs e)
        {
            VerPost(e.arg);
        }
        private void pbxBuscar_Click(object sender, EventArgs e)
        {

            if (panelBusqueda.Visible == false)
            {
                if (panelBusqueda.Controls.Count == 0)
                {
                    panelBusqueda.Visible = true;
                    PanelMostrarUsuario.Parent = this;
                    PanelMostrarUsuario.Location = PanelPosts.Location;
                    Busqueda busqueda = new Busqueda(user,token);
                    busqueda.TopLevel = false;
                    busqueda.FormBorderStyle = FormBorderStyle.None;
                    busqueda.BackColor = Color.LightGray;
                    busqueda.Dock = DockStyle.Fill;
                    busqueda.AbrirUsuario += PostControl_AbrirPaginaUsuario;
                    busqueda.AbrirEvento += Grupo_EventoParaListar_AbrirEvento;
                    panelBusqueda.BackColor = Color.LightGray;
                    panelBusqueda.Controls.Add(busqueda);
                    busqueda.Show();
                }
                else
                {
                    panelBusqueda.Visible = true;
                }
            }
            else
            {
                panelBusqueda.Visible = false;
            }
        }
    }
}
