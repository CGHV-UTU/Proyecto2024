using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
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
        private string idioma;
        public Inicio(string usuario)
        {
            InitializeComponent();
            user = usuario;
            VerPosts();
            PanelComentarios.Visible = false;
            PictureBoxSalir.Visible = false;
            PanelPostear.Visible = false;
            PanelNotificaciones.Visible = false;
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
                MostrarNotificacionesEjemplo(); // Método para mostrar las notificaciones de ejemplo
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
            PanelNotificaciones.Controls.Clear(); // Limpiar cualquier notificación existente
            int margin = 10;
            string notificaciones = await conseguirNotificaciones(user);
            string[] array = notificaciones.Split(';');
            for (int i = 0; i < 5; i++)
            {
                NotificacionControl notificacionControl = new NotificacionControl($"Notificación ejemplo {i + 1}");
                notificacionControl.Size = new Size(500 - margin * 2, 100);
                notificacionControl.Location = new Point(margin, i * (notificacionControl.Height + margin));
                PanelNotificaciones.Controls.Add(notificacionControl);
            }
        }

        // cargar form de posts. -Puse un fondo gris para distinguirlo    
        private async void VerPosts()
        {
            string config = await conseguirConfig(user);
            string[] configure = config.Split(';');    
            idioma = configure[1];
            Posts post = new Posts(configure[0]);
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
            PanelPosts.Controls.Add(post);
            post.Show();
        }
        private void PostControl_AbrirComentarios(object sender, EventArgs e)
        {
            VerComentarios();
            PanelComentarios.Visible = true;
            PictureBoxSalir.Visible = true;
        }
        private void VerComentarios()
        {
            Comentarios comentario = new Comentarios();
            comentario.TopLevel = false;
            comentario.FormBorderStyle = FormBorderStyle.None;
            comentario.BackColor = Color.LightGray;
            comentario.Dock = DockStyle.Fill;
            PanelComentarios.Controls.Add(comentario);
            comentario.Show();
        }

        private void PictureBoxSalir_Click(object sender, EventArgs e)
        {
            PanelComentarios.Visible = false;
            PictureBoxSalir.Visible = false;
        }

        private void PictureBoxCrear_Click(object sender, EventArgs e)
        {
            VerPost();
        }
        private void VerPost()
        {
            PanelPostear.Controls.Clear();
            PanelPostear.Visible = true;
            PanelPostear.Parent = this;
            PanelPosts.Visible = false; 
            Post post = new Post(user);
            post.TopLevel = false;
            post.FormBorderStyle = FormBorderStyle.None;
            post.BackColor = Color.LightGray;
            post.Dock = DockStyle.Fill;
            post.Creado += Post_Creado;
            post.Salir += Post_Salir;
            post.CambiaTamaño += Post_CambiaTamaño;
            post.BackColor = Color.FromArgb(34, 67, 220);
            PanelPostear.Controls.Add(post);
            post.Show();
        }
        private void Post_Creado(object sender, EventArgs e)
        {
            PanelPostear.Visible = false;
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
            Configuracion config = new Configuracion(user);
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
        }

        public static async Task<string> conseguirConfig(string usuario)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var datos = new { nombreDeCuenta = usuario };
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

        public static async Task<string> conseguirNotificaciones(string usuario)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var datos = new { nombreDeCuenta = usuario };
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
    }
}
