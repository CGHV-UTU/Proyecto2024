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
        private int currentPage = 0;
        private string modo;
        public Posts(string modo)
        {
            this.modo = modo;
            Iniciar();
            LoadPosts(currentPage);
            panel1.Scroll += PanelPosts_Scroll;
        }

        static async Task<bool> Existe(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync($"https://localhost:44340/existePost?id={id}");
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

        static async Task<string> CuantosPost()
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync($"https://localhost:44340/ultimoPost");
                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();
                    dynamic data = JsonConvert.DeserializeObject(responseBody); //sigo sin poder pasar esto a lo que quiero, no me deja acceder a la info del json de ninguna manera, tengo que hallar alguna forma de pasar los datos
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
            var cantPost = await CuantosPost();

            // Simulación de carga de posts
            for (int i = 0; i < int.Parse(cantPost); i++)
            {
                var existe = await Existe(i);
                if (existe)
                {
                    var postControl = new PostControl(i + 1, modo);
                    postControl.AbrirComentarios += PostControl_AbrirComentarios;
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
        }
        private void PostControl_AbrirComentarios(object sender, PersonalizedArgs e)
        {
            // Disparar el evento para que lo maneje quien esté suscrito (en este caso, Inicio)
            AbrirComentarios?.Invoke(this, new PersonalizedArgs(e.arg));
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
