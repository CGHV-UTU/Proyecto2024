﻿using Newtonsoft.Json;
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
        private int currentPage = 0;
        private const int commentsPerPage = 10;
        private const int margin = 10; // margen para los comentarios
        private string modo;
        private string idpost;
        public Comentarios(string modo,string idpost)
        {
            this.modo = modo;
            this.idpost = idpost;
            Iniciar();
            LoadComments(currentPage);
            PanelComentarios.Scroll += PanelComments_Scroll;
        }

        static async Task<dynamic> ConseguirComentarios(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync($"https://localhost:44340/seleccionarTodosLosComentarios?id={id}");
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

        private void PanelComments_Scroll(object sender, ScrollEventArgs e)
        {
            if (PanelComentarios.VerticalScroll.Value + PanelComentarios.ClientSize.Height >= PanelComentarios.VerticalScroll.Maximum)
            {
                currentPage++;
                LoadComments(currentPage);
            }
        }

        private async void LoadComments(int page)
        {
            DataTable comentarios=await ConseguirComentarios(int.Parse(idpost));
            if (comentarios != null)
            {
                for (int i = 0; i <= comentarios.Rows.Count; i++)
                {
                    int idcomentario = Convert.ToInt32(comentarios.Rows[i]["id"]);
                    var commentControl = new CommentControl(modo, idpost, idcomentario);
                    commentControl.Size = new Size(465 + margin * 2, 171 + margin * 2);
                    commentControl.Location = new Point(margin, (page * commentsPerPage + i) * (commentControl.Height + margin));
                    PanelComentarios.Controls.Add(commentControl);
                }
            }
        }


        private void Iniciar()
        {
            this.PanelComentarios = new Panel();
            this.PictureBoxSalir = new PictureBox();
            this.SuspendLayout();

            // PanelComentarios
            this.PanelComentarios.HorizontalScroll.Enabled = false;
            this.PanelComentarios.HorizontalScroll.Visible = false;
            this.PanelComentarios.AutoScroll = true;
            this.PanelComentarios.AutoScroll = true;
            this.PanelComentarios.Dock = DockStyle.Fill;
            this.PanelComentarios.Location = new System.Drawing.Point(0, 0);
            this.PanelComentarios.Name = "PanelComentarios";
            this.PanelComentarios.Size = new System.Drawing.Size(800, 450);
            this.PanelComentarios.TabIndex = 0;


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
