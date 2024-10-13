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
        private int currentPage = 0;
        private string modo;
        private string user;
        private string token;
        public Posts(string modo,string user, string token)
        {
            this.modo = modo;
            this.user = user;
            this.token = token;
            Iniciar();
            LoadPosts(currentPage);
            panel1.Scroll += PanelPosts_Scroll;
        }

        static async Task<bool> Existe(int id, string token)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var dato = new { id=id , token = token };
                    var content = new StringContent(JsonConvert.SerializeObject(dato), Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PutAsync("https://localhost:44340/existePost", content);
                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();
                    dynamic data = JsonConvert.DeserializeObject<bool>(responseBody); //sigo sin poder pasar esto a lo que quiero, no me deja acceder a la info del json de ninguna manera, tengo que hallar alguna forma de pasar los datos
                    return data;
                }
                catch
                {
                    return false;
                }
            }
        }

        static async Task<dynamic> ConseguirPostsPublicos(string token)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var dato = new {token = token };
                    var content = new StringContent(JsonConvert.SerializeObject(dato), Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PutAsync($"https://localhost:44340/seleccionarTodosLosPostPublicos", content);
                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();
                    dynamic data = JsonConvert.DeserializeObject<DataTable>(responseBody);
                    return data;
                }
                catch
                {
                    return null;
                }
            }
        }

        static async Task<string> CuantosPost(string token)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var dato = new { token = token };
                    var content = new StringContent(JsonConvert.SerializeObject(dato), Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PutAsync($"https://localhost:44340/ultimoPost", content);
                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();
                    dynamic data = JsonConvert.DeserializeObject(responseBody); //sigo sin poder pasar esto a lo que quiero, no me deja acceder a la info del json de ninguna manera, tengo que hallar alguna forma de pasar los datos
                    Console.WriteLine("Respuesta http: " + response);
                    Console.WriteLine("Respuesta body: " + responseBody);
                    Console.WriteLine("data: " + data);
                    return data;
                }
                catch
                {
                    return "0";
                }
            }
        }
        private void PanelPosts_Scroll(object sender, ScrollEventArgs e)
        {
            if (panel1.VerticalScroll.Value + panel1.ClientSize.Height >= panel1.VerticalScroll.Maximum)
            {
                currentPage++;
                LoadPosts(currentPage);
            }
        }

        private async void LoadPosts(int page)
        {
            DataTable postPublicos = await ConseguirPostsPublicos(token);
            
            if (postPublicos == null)
            {
                MessageBox.Show("No se encontraron posts");
                return;
            }

            // carga de posts
            for (int i = postPublicos.Rows.Count; i > 0; i--)
            {
                    var postControl = new PostControl(Convert.ToInt32(postPublicos.Rows[i-1]["idPost"]), modo, user, token);
                    postControl.AbrirComentarios += PostControl_AbrirComentarios;
                    postControl.ReportarPost += PostControl_ReportarPost;
                    postControl.RecargarFeed += PostControl_RecargarFeed;
                    postControl.AbrirPaginaUsuario += PostControl_AbrirPaginaUsuario;
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
            }
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
        private void Iniciar()
        {
            this.panel1 = new Panel();
            this.SuspendLayout();

            // panelPosts
            this.panel1.AutoScroll = true;
            this.panel1.Dock = DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panelPosts";
            this.panel1.Size = new System.Drawing.Size(800, 450);
            this.panel1.TabIndex = 0;

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
