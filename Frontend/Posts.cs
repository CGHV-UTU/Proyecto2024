﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Frontend
{
    public partial class Posts : Form
    {
        public event EventHandler AbrirComentarios;
        private int currentPage = 0;
        private const int postsPerPage = 10;
        public Posts()
        {
            Iniciar();
            LoadPosts(currentPage);
            panel1.Scroll += PanelPosts_Scroll;
        }

        private void PanelPosts_Scroll(object sender, ScrollEventArgs e)
        {
            if (panel1.VerticalScroll.Value + panel1.ClientSize.Height >= panel1.VerticalScroll.Maximum)
            {
                currentPage++;
                LoadPosts(currentPage);
            }
        }

        private void LoadPosts(int page)
        {
            // Simulación de carga de posts
            for (int i = 0; i < postsPerPage; i++)
            {
                string postType;
                switch (i % 5)
                {
                    case 0:
                        postType = "imageOnly";
                        break;
                    case 1:
                        postType = "textAndUrl";
                        break;
                    case 2:
                        postType = "textOnly";
                        break;
                    case 3:
                        postType = "urlOnly";
                        break;
                    default:
                        postType = "textAndImage";
                        break;
                }
                var postControl = new PostControl($"Post {i + 1}", postType);
                postControl.AbrirComentarios += PostControl_AbrirComentarios;
                postControl.Location = new Point(0, (page * postsPerPage + i) * postControl.Height);
                panel1.Controls.Add(postControl);
            }
        }
        private void PostControl_AbrirComentarios(object sender, EventArgs e)
        {
            // Disparar el evento para que lo maneje quien esté suscrito (en este caso, Inicio)
            AbrirComentarios?.Invoke(this, e);
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
