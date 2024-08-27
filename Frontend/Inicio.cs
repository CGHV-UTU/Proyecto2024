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

        private void PictureBoxNotificaciones_Click(object sender, EventArgs e)
        {
            PanelNotificaciones.Visible = true;
            Notificaciones notificaciones = new Notificaciones(user);
            notificaciones.TopLevel = false;
            notificaciones.FormBorderStyle = FormBorderStyle.None;
            notificaciones.BackColor = Color.LightGray;
            notificaciones.Dock = DockStyle.Fill;
            PanelNotificaciones.Controls.Add(notificaciones);
            notificaciones.Show();
            
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

        
    }
}
