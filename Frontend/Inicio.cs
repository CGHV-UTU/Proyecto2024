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
            cargarLaImagen(); 
        }

        private async void cargarLaImagen()
        {
            string imagenB64 = await conseguirImagenDePerfil(user, token);
            byte[] imagen = Convert.FromBase64String(imagenB64);
            MemoryStream ms = new MemoryStream(imagen);
            Bitmap bitmap = new Bitmap(ms);
            this.PictureBoxUsuario.Image = bitmap;
            redondearPictureBox(bitmap);
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


        public void redondearPictureBox(Image image)
        {
            if (image == null)
            {
                MessageBox.Show("La imagen es nula. No se puede redondear.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            System.Drawing.Drawing2D.GraphicsPath gp = new System.Drawing.Drawing2D.GraphicsPath();
            gp.AddEllipse(0, 0, PictureBoxUsuario.Width, PictureBoxUsuario.Height);
            Region rg = new Region(gp);
            PictureBoxUsuario.Region = rg;
            PictureBoxUsuario.Image = image;
        }



        private void PictureBoxNotificaciones_Click(object sender, EventArgs e)
        {
            this.PanelNotificaciones.AutoScroll = true;
            if (!PanelNotificaciones.Visible)
            {
                PanelNotificaciones.Visible = true;
                Notificaciones notis = new Notificaciones(user, token);
                notis.TopLevel = false;
                notis.FormBorderStyle = FormBorderStyle.None;
                notis.BackColor = Color.LightGray;
                notis.Dock = DockStyle.Fill;
                PanelNotificaciones.Controls.Add(notis);
            } else {
                PanelNotificaciones.Visible = false; // Quitar el panel de notificaciones
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
            PanelMostrarUsuario.Controls.Clear();
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
            paginaDeUsuario.AbrirGrupo += Grupo_EventoParaListar_AbrirGrupo;
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
            PanelMostrarUsuario.Controls.Clear();
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
            PanelMostrarUsuario.Controls.Clear();
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
            PanelMostrarUsuario.Controls.Clear();
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
            comunidad.PostearEnEvento += EventoComunidad_PostearEnEvento;
            comunidad.EventoEliminado += PictureboxLogo_Click;
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
            bool esChatPrivado;
            if (e.arg2.Equals("es chat privado"))
            {
                esChatPrivado = true;
            }
            else
            {
                esChatPrivado = false;
            }
            GruposComunidad comunidad = new GruposComunidad(e.arg, user, token,esChatPrivado);
            comunidad.TopLevel = false;
            comunidad.FormBorderStyle = FormBorderStyle.None;
            comunidad.BackColor = Color.LightGray;
            comunidad.Dock = DockStyle.Fill;
            PanelMostrarUsuario.BackColor = Color.LightGray;
            comunidad.TieneConfiguraciones();
            comunidad.GrupoEliminado += PictureboxLogo_Click;
            comunidad.AbrirUsuario += PostControl_AbrirPaginaUsuario;
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

        private void PictureBoxUsuario_Click(object sender, EventArgs e)
        {
            PanelMostrarUsuario.Controls.Clear();
            PanelComentarios.Visible = false;
            PanelPosts.Visible = false;
            PanelMostrarUsuario.Visible = true;
            PanelMostrarUsuario.Parent = this;
            PanelMostrarUsuario.Location = PanelPosts.Location;
            PaginaDeUsuario paginaDeUsuario = new PaginaDeUsuario(user, modo, user, token);
            paginaDeUsuario.TopLevel = false;
            paginaDeUsuario.FormBorderStyle = FormBorderStyle.None;
            paginaDeUsuario.BackColor = Color.LightGray;
            paginaDeUsuario.Dock = DockStyle.Fill;
            PanelMostrarUsuario.BackColor = Color.LightGray;
            paginaDeUsuario.ReportarPost += PostControl_ReportarPost;
            paginaDeUsuario.AbrirComentarios += PostControl_AbrirComentarios;
            PanelMostrarUsuario.Controls.Add(paginaDeUsuario);
            paginaDeUsuario.Show();
        }
    }
}
