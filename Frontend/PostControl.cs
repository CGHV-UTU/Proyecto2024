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
    public partial class PostControl : UserControl
    {
        public event EventHandler AbrirComentarios;
        public PostControl(string title)
        {
            iniciar();
            lblNombre.Text = title;
        }

        // Dar like. Puse lo de isImage porque no me andaba normal
        private bool isImage1 = true;
        private void PictureBoxLike_Click(object sender, EventArgs e)
        {
            if (isImage1)
            {
                PictureBoxLike.Image = Properties.Resources.Like_Relleno;
                isImage1 = false;
            }
            else
            {
                PictureBoxLike.Image = Properties.Resources.like_infini;
                isImage1 = true;
            }
        }
        private void PictureBoxComentarios_Click(object sender, EventArgs e)
        {
            AbrirComentarios?.Invoke(this, EventArgs.Empty);
        }

        private void iniciar()
        {
            this.lblNombre = new Label();
            this.PictureBoxUsuarioPost = new PictureBox();
            this.imagen = new PictureBox();
            this.PictureBoxLike = new PictureBox();
            this.PictureBoxComentarios = new PictureBox();
            this.PictureBoxCompartir = new PictureBox();
            this.PictureBoxOpcionesPost = new PictureBox();
            this.button1 = new Button();
            this.SuspendLayout();

            // lblTitle
            this.lblNombre.AutoSize = true;
            this.lblNombre.Location = new System.Drawing.Point(132, 50);
            this.lblNombre.Name = "lblNombre";
            this.lblNombre.Size = new System.Drawing.Size(44, 13);
            this.lblNombre.TabIndex = 28;

            // picImage
            this.PictureBoxUsuarioPost.Location = new System.Drawing.Point(76, 13);
            this.PictureBoxUsuarioPost.Name = "PictureBoxUsuarioPost";
            this.PictureBoxUsuarioPost.Size = new System.Drawing.Size(50, 50);
            this.PictureBoxUsuarioPost.SizeMode = PictureBoxSizeMode.StretchImage;
            this.PictureBoxUsuarioPost.Image = Frontend.Properties.Resources.User;

            // like
            this.PictureBoxLike.Location = new System.Drawing.Point(76, 440);
            this.PictureBoxLike.Name = "PictureBoxLike";
            this.PictureBoxLike.Size = new System.Drawing.Size(50, 50);
            this.PictureBoxLike.SizeMode = PictureBoxSizeMode.StretchImage;
            this.PictureBoxLike.Image = Frontend.Properties.Resources.like_infini;
            this.PictureBoxLike.Click += PictureBoxLike_Click;

            // comentarios
            this.PictureBoxComentarios.Location = new System.Drawing.Point(548, 440);
            this.PictureBoxComentarios.Name = "PictureBoxComentarios";
            this.PictureBoxComentarios.Size = new System.Drawing.Size(50, 50);
            this.PictureBoxComentarios.SizeMode = PictureBoxSizeMode.StretchImage;
            this.PictureBoxComentarios.Image = Frontend.Properties.Resources.comentario;
            this.PictureBoxComentarios.Click += PictureBoxComentarios_Click;

            // Compartir
            this.PictureBoxCompartir.Location = new System.Drawing.Point(604, 440);
            this.PictureBoxCompartir.Name = "PictureBoxCompartir";
            this.PictureBoxCompartir.Size = new System.Drawing.Size(50, 50);
            this.PictureBoxCompartir.SizeMode = PictureBoxSizeMode.StretchImage;
            this.PictureBoxCompartir.Image = Frontend.Properties.Resources.compartir;

            // PictureBoxOpcionesPost
            this.PictureBoxOpcionesPost.Location = new System.Drawing.Point(660, 440);
            this.PictureBoxOpcionesPost.Name = "PictureBoxOpcionesPost";
            this.PictureBoxOpcionesPost.Size = new System.Drawing.Size(50, 50);
            this.PictureBoxOpcionesPost.SizeMode = PictureBoxSizeMode.StretchImage;
            this.PictureBoxOpcionesPost.Image = Frontend.Properties.Resources.mas_opciones;

            // imagen
            this.imagen.Location = new System.Drawing.Point(76, 69);
            this.imagen.Name = "imagen";
            this.imagen.Size = new System.Drawing.Size(634, 365);
            this.imagen.SizeMode = PictureBoxSizeMode.StretchImage;

            // button1
            this.button1.Location = new System.Drawing.Point(247, 45);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 27;
            this.button1.Text = "Añadir imagen";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += button1_Click;


            // PostControl
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.Controls.Add(this.PictureBoxUsuarioPost);
            this.Controls.Add(this.lblNombre);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.imagen);
            this.Controls.Add(this.PictureBoxLike);
            this.Controls.Add(this.PictureBoxComentarios);
            this.Controls.Add(this.PictureBoxCompartir);
            this.Controls.Add(this.PictureBoxOpcionesPost);
            this.Name = "PostControl";
            this.Size = new System.Drawing.Size(787, 578);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                imagen.ImageLocation = ofd.FileName;
                imagen.SizeMode = PictureBoxSizeMode.StretchImage;
            }
        }
    }
}
