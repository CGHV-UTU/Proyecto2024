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
            // Simulación de carga de comentarios
            for (int i = 0; i < commentsPerPage; i++)
            {
                var commentControl = new CommentControl("Usuario", "Este es un comentario de ejemplo.");
                commentControl.Location = new Point(0, (page * commentsPerPage + i) * commentControl.Height);
                PanelComentarios.Controls.Add(commentControl);
            }
        }

        private void Iniciar()
        {
            this.PanelComentarios = new Panel();
            this.PictureBoxSalir = new PictureBox();
            this.SuspendLayout();

            // PanelComentarios
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
