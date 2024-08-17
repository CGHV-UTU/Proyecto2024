using System;
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
    public partial class Comentarios : Form
    {
        private int currentPage = 0;
        private const int commentsPerPage = 10;
        private const int margin = 10; // margen para los comentarios

        public Comentarios()
        {
            Iniciar();
            LoadComments(currentPage);
            PanelComentarios.Scroll += PanelComments_Scroll;
        }

        private void PanelComments_Scroll(object sender, ScrollEventArgs e)
        {
            if (PanelComentarios.VerticalScroll.Value + PanelComentarios.ClientSize.Height >= PanelComentarios.VerticalScroll.Maximum)
            {
                currentPage++;
                LoadComments(currentPage);
            }
        }

        private void LoadComments(int page)
        {
            for (int i = 0; i < commentsPerPage; i++)
            {
                var nombreUsuario = $"Usuario {page * commentsPerPage + i + 1}";
                var comentarioTexto = $"Este es el comentario número {page * commentsPerPage + i + 1}.";
                var commentControl = new CommentControl(nombreUsuario, comentarioTexto);
                commentControl.Size = new Size(465 + margin * 2, 171 + margin * 2);
                commentControl.Location = new Point(margin, (page * commentsPerPage + i) * (commentControl.Height + margin));

                PanelComentarios.Controls.Add(commentControl);
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
