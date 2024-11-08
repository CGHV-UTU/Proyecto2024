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
    public partial class Comentarios : Form
    {
        private const int margin = 10; // margen para los comentarios
        private string modo;
        private string idioma;
        private string idpost;
        private string user;
        private string token;
        public event EventHandler<PersonalizedArgs> ReportarComentario;
        public event EventHandler<PersonalizedArgs> AbrirPaginaDelUsuario;
        public Comentarios(string modo,string idpost, string user, string token, string idioma)
        {
            this.modo = modo;
            this.idioma = idioma;
            this.idpost = idpost;
            this.user = user;
            this.token = token;
            Iniciar();
            CreadorComentarios comentario = new CreadorComentarios(user,idpost, token, modo, idioma);
            comentario.Location = new Point(margin, 0);
            PanelComentarios.Controls.Add(comentario);
            LoadComments();
            if (modo.Equals("Oscuro"))
            {
                this.PanelComentarios.BackColor = Color.FromArgb(40, 40, 40);
            }
        }

        static async Task<dynamic> ConseguirComentarios(string id, string token)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var dato = new { id = id , token=token};
                    var content = new StringContent(JsonConvert.SerializeObject(dato), Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PutAsync($"https://localhost:44340/seleccionarTodosLosComentarios",content);
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


        private async void LoadComments()
        {
            var comentarios=await ConseguirComentarios(idpost, token);
            if (comentarios != null && !Convert.ToString(comentarios).Equals("Error al cargar Datagrid"))
            {
                foreach(var comentario in comentarios)
                {
                    var commentControl = new CommentControl(modo, idpost, comentario,user, token, idioma);
                    commentControl.Size = new Size(465 + margin * 2, 171 + margin * 2);
                    var lastControl = PanelComentarios.Controls[PanelComentarios.Controls.Count - 1];
                    commentControl.Location = new Point(margin, lastControl.Bottom);
                    commentControl.ReportarComentario += CommentControl_ReportarComentario;
                    commentControl.AbrirPaginaDelUsuario += CommentControl_AbrirPaginaUsuario;
                    PanelComentarios.Controls.Add(commentControl);
                }
            }
        }
        private void CommentControl_AbrirPaginaUsuario(object sender, PersonalizedArgs e)
        {
            AbrirPaginaDelUsuario?.Invoke(this, new PersonalizedArgs(e.arg));
        }
        private void CommentControl_ReportarComentario(object sender, PersonalizedArgs e)
        {
            ReportarComentario?.Invoke(this, new PersonalizedArgs(e.arg,e.arg2));
        }

        private void Iniciar()
        {
            this.PanelComentarios = new Panel();
            this.PictureBoxSalir = new PictureBox();
            this.SuspendLayout();

            // PanelComentarios
            this.PanelComentarios.HorizontalScroll.Enabled = false;
            this.PanelComentarios.AutoScroll = true;
            this.PanelComentarios.Dock = DockStyle.Fill;
            this.PanelComentarios.Location = new System.Drawing.Point(0, 0);
            this.PanelComentarios.Name = "PanelComentarios";
            this.PanelComentarios.Size = new System.Drawing.Size(800, 450);
            this.PanelComentarios.TabIndex = 0;
            this.PanelComentarios.BackColor = Color.FromArgb(190, 190, 190);


            this.Controls.Add(this.PanelComentarios);

            // Comentarios Form
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Name = "Comentarios";
            this.Text = "Infinite Scroll Comentarios";
            this.ResumeLayout(false);
        }
    }
}
