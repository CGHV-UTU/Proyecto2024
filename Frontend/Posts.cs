using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Frontend
{
    public partial class Posts : Form
    {
        public event EventHandler<PersonalizedArgs> AbrirComentarios;
        public event EventHandler<PersonalizedArgs> ReportarPost;
        public event EventHandler<PersonalizedArgs> AbrirPaginaUsuario;
        public event EventHandler<PersonalizedArgs> Compartir;
        private int currentPage = 1;
        private string modo;
        private string user;
        private string token;
        private string idioma;
        private int postsCargados;
        public Posts(string modo,string user, string token, string idioma)
        {
            this.modo = modo;
            this.user = user;
            this.token = token;
            this.idioma = idioma;
            Iniciar();
            panel1.Visible = false;
            LoadPosts(currentPage);
        }

        static async Task<dynamic> ConseguirPostsPublicos(int id1, int id2,string token)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var dato = new {id=id1, idEvento=id2, token = token };
                    var content = new StringContent(JsonConvert.SerializeObject(dato), Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PutAsync($"https://localhost:44340/conseguir40PostMasPopulares", content);
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

        private void PanelPosts_Scroll(object sender, ScrollEventArgs e)
        {
            if (this.panel1.VerticalScroll.Value >= this.panel1.VerticalScroll.Maximum-2000)
            {
                currentPage++;
                LoadPosts(currentPage);
            }
        }

        private async void LoadPosts(int page)
        {
            progressBar1.Maximum = 100;
            progressBar1.Value = 1;
            var postPublicos = await ConseguirPostsPublicos(40 * page - 40, 40*page,token);
            if (postPublicos == null || Convert.ToString(postPublicos).Equals("no se encuentra"))
            {
                MessageBox.Show("No se encontraron posts");
                return;
            }
            int count = 0;
            foreach(var post in postPublicos)
            {
                count++;
            }
            progressBar1.Maximum = count;
            progressBar1.Value = 0;
            // carga de posts
            foreach (var post in postPublicos)
            {
                var postControl = new PostControl(post, modo, user, token, idioma);
                postControl.AbrirComentarios += PostControl_AbrirComentarios;
                postControl.ReportarPost += PostControl_ReportarPost;
                postControl.RecargarFeed += PostControl_RecargarFeed;
                postControl.AbrirPaginaUsuario += PostControl_AbrirPaginaUsuario;
                postControl.Compartir += PostControl_Compartir;
                await postControl.aplicarDatos();
                // Calcula la ubicación Y acumulada
                int currentYPosition = 0;
                if (panel1.Controls.Count > 0)
                {
                    var lastControl = panel1.Controls[panel1.Controls.Count - 1];
                    if (postControl.tipo.Equals("imageOnly") || postControl.tipo.Equals("textAndImage"))
                    {
                        await Task.Delay(300);
                    }
                    currentYPosition = lastControl.Bottom;  // La posición inferior del último control agregado
                }
                postControl.Location = new Point(0, currentYPosition);
                panel1.Controls.Add(postControl);
                progressBar1.Value += 1;
                Application.DoEvents();
            }
            this.progressBar1.Visible = false;
            this.panel1.Visible = true;
        }
        private void PostControl_RecargarFeed(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            LoadPosts(1);
        }
        private void PostControl_AbrirComentarios(object sender, PersonalizedArgs e)
        {
            // Disparar el evento para que lo maneje quien esté suscrito (en este caso, Inicio)
            AbrirComentarios?.Invoke(this, new PersonalizedArgs(e.arg));
        }
        private void PostControl_ReportarPost(object sender, PersonalizedArgs e)
        {
            // Disparar el evento para que lo maneje quien esté suscrito (en este caso, Inicio)
            ReportarPost?.Invoke(this, new PersonalizedArgs(e.arg));
        }
        private void PostControl_AbrirPaginaUsuario(object sender, PersonalizedArgs e)
        {
            AbrirPaginaUsuario?.Invoke(this, new PersonalizedArgs(e.arg));
        }
        private void PostControl_Compartir(object sender, PersonalizedArgs e)
        {
            Compartir?.Invoke(this, new PersonalizedArgs(e.arg));
        }
        private void Iniciar()
        {
            this.panel1 = new Panel();
            this.progressBar1 = new ProgressBar();
            this.SuspendLayout();

            // panelPosts
            this.panel1.AutoScroll = true;
            this.panel1.Dock = DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(800, 450);
            this.panel1.Scroll += PanelPosts_Scroll;
            this.panel1.TabIndex = 0;

            this.progressBar1.Location = new Point(250, 219);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new Size(516, 23);
            this.progressBar1.Parent=this;

            // Form1
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panel1);
            this.Name = "Form1";
            this.Text = "Infinite Scroll Posts";
            this.ResumeLayout(false);
        }
    }
}
