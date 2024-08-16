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
        public PostControl(string title, string postType)
        {
            iniciar(postType);
            lblNombre.Text = title;
        }

        private void ConfigurarPostControl(string postType)
        {
            //Tipos de post(puse un texto de ejemplo para que quede claro,
            //los que quedan sin texto son los solo imagen)
            switch (postType)
            {
                case "imageonly":
                    // Solo imagen, ocultar todos los TextBox
                    OcultarTextBoxes();
                    break;

                case "textandurl":
                    // Mostrar ambos TextBox para texto y URL
                    MostrarTextBoxes(true, true);
                    txtDescripcion.Text = "Este post tiene una descripción y una URL.";
                    txtUrl.Text = "Aca va la URL.";
                    txtUrl.Location = new Point(80, 125);
                    break;

                case "textonly":
                    // Mostrar solo el TextBox para texto
                    MostrarTextBoxes(true, false);
                    txtDescripcion.Text = "Este post solo tiene una descripción.";
                    break;

                case "urlonly":
                    // Mostrar solo el TextBox para URL
                    MostrarTextBoxes(false, true);
                    txtUrl.Location = new Point(100, 95);
                    txtUrl.Text = "Este post solo tiene una URL.";
                    break;
            }
        }

        // Mostrar - Ocultar los TextBox
        private void OcultarTextBoxes()
        {
            txtDescripcion.Visible = false;
            txtUrl.Visible = false;
        }
        private void MostrarTextBoxes(bool mostrarTexto, bool mostrarUrl)
        {
            txtDescripcion.Visible = mostrarTexto;
            txtUrl.Visible = mostrarUrl;
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

        //Abrir comentarios
        private void PictureBoxComentarios_Click(object sender, EventArgs e)
        {
            AbrirComentarios?.Invoke(this, EventArgs.Empty);
        }

        private void iniciar(string postType)
        {
            this.lblNombre = new Label();
            this.PictureBoxUsuarioPost = new PictureBox();
            this.imagen = new PictureBox();
            this.PictureBoxLike = new PictureBox();
            this.PictureBoxComentarios = new PictureBox();
            this.PictureBoxCompartir = new PictureBox();
            this.PictureBoxOpcionesPost = new PictureBox();
            this.button1 = new Button();
            this.txtDescripcion = new TextBox();
            this.txtUrl = new TextBox();
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
            this.PictureBoxUsuarioPost.Cursor = Cursors.Hand;

            // like
            this.PictureBoxLike.Location = new System.Drawing.Point(76, 440);
            this.PictureBoxLike.Name = "PictureBoxLike";
            this.PictureBoxLike.Size = new System.Drawing.Size(50, 50);
            this.PictureBoxLike.SizeMode = PictureBoxSizeMode.StretchImage;
            this.PictureBoxLike.Image = Frontend.Properties.Resources.like_infini;
            this.PictureBoxLike.Click += PictureBoxLike_Click;
            this.PictureBoxLike.Cursor = Cursors.Hand;

            // comentarios
            this.PictureBoxComentarios.Location = new System.Drawing.Point(548, 440);
            this.PictureBoxComentarios.Name = "PictureBoxComentarios";
            this.PictureBoxComentarios.Size = new System.Drawing.Size(50, 50);
            this.PictureBoxComentarios.SizeMode = PictureBoxSizeMode.StretchImage;
            this.PictureBoxComentarios.Image = Frontend.Properties.Resources.comentario;
            this.PictureBoxComentarios.Click += PictureBoxComentarios_Click;
            this.PictureBoxComentarios.Cursor = Cursors.Hand;

            // Compartir
            this.PictureBoxCompartir.Location = new System.Drawing.Point(604, 440);
            this.PictureBoxCompartir.Name = "PictureBoxCompartir";
            this.PictureBoxCompartir.Size = new System.Drawing.Size(50, 50);
            this.PictureBoxCompartir.SizeMode = PictureBoxSizeMode.StretchImage;
            this.PictureBoxCompartir.Image = Frontend.Properties.Resources.compartir;
            this.PictureBoxCompartir.Cursor = Cursors.Hand;

            // PictureBoxOpcionesPost
            this.PictureBoxOpcionesPost.Location = new System.Drawing.Point(660, 440);
            this.PictureBoxOpcionesPost.Name = "PictureBoxOpcionesPost";
            this.PictureBoxOpcionesPost.Size = new System.Drawing.Size(50, 50);
            this.PictureBoxOpcionesPost.SizeMode = PictureBoxSizeMode.StretchImage;
            this.PictureBoxOpcionesPost.Image = Frontend.Properties.Resources.mas_opciones;
            this.PictureBoxOpcionesPost.Cursor = Cursors.Hand;

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

            // txtDescripcion
            this.txtDescripcion.Location = new Point(76, 80);
            this.txtDescripcion.Name = "txtDescripcion";
            this.txtDescripcion.Size = new Size(634, 22);

            // txtUrl
            this.txtUrl.Location = new Point(76, 410); // Justo debajo de la imagen
            this.txtUrl.Name = "txtUrl";
            this.txtUrl.Size = new Size(634, 22); // Ajusta el tamaño según necesidad      

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
            this.Controls.Add(this.txtDescripcion);
            this.Controls.Add(this.txtUrl);
            this.Name = "PostControl";
            this.Size = new System.Drawing.Size(787, 578);

            //Tipos de post(puse un texto de ejemplo para que quede claro,
            //los que quedan sin texto son los de imagen)
            switch (postType)
            {
                case "textAndImage":
                    this.txtDescripcion.Visible = true;
                    this.txtDescripcion.Text = "Texto e imagen";
                    this.imagen.Visible = true;
                    this.txtUrl.Visible = false;
                    this.imagen.Location = new System.Drawing.Point(76, 102);
                    this.PictureBoxLike.Location = new Point(76, imagen.Bottom + 10);
                    this.PictureBoxComentarios.Location = new Point(548, imagen.Bottom + 10);
                    this.PictureBoxCompartir.Location = new Point(604, imagen.Bottom + 10);
                    this.PictureBoxOpcionesPost.Location = new Point(660, imagen.Bottom + 10);
                    break;

                case "imageOnly":
                    this.txtDescripcion.Visible = false;
                    this.txtUrl.Visible = false;
                    this.imagen.Visible = true;
                    break;

                case "textAndUrl":
                    this.txtDescripcion.Text = "Este post tiene una descripción y una URL.";
                    this.txtUrl.Text = "Aca va la URL.";
                    this.txtDescripcion.Visible = true;
                    this.txtUrl.Visible = true;
                    this.txtUrl.Location = new Point(76, 115);
                    this.PictureBoxLike.Location = new Point(76, txtUrl.Bottom + 10);
                    this.PictureBoxComentarios.Location = new Point(548, txtUrl.Bottom + 10);
                    this.PictureBoxCompartir.Location = new Point(604, txtUrl.Bottom + 10);
                    this.PictureBoxOpcionesPost.Location = new Point(660, txtUrl.Bottom + 10);
                    this.imagen.Visible = false;
                    break;

                case "textOnly":
                    this.txtDescripcion.Text = "Este post solo tiene una descripción.";
                    this.txtDescripcion.Visible = true;
                    this.txtUrl.Visible = false;
                    this.imagen.Visible = false;
                    this.PictureBoxLike.Location = new Point(76, txtDescripcion.Bottom + 10);
                    this.PictureBoxComentarios.Location = new Point(548, txtDescripcion.Bottom + 10);
                    this.PictureBoxCompartir.Location = new Point(604, txtDescripcion.Bottom + 10);
                    this.PictureBoxOpcionesPost.Location = new Point(660, txtDescripcion.Bottom + 10);
                    break;

                case "urlOnly":
                    this.txtUrl.Location = new Point(76, 80);
                    this.txtUrl.Text = "Este post solo tiene una URL.";
                    this.txtDescripcion.Visible = false;
                    this.txtUrl.Visible = true;
                    this.imagen.Visible = false;
                    this.PictureBoxLike.Location = new Point(76, txtUrl.Bottom + 10);
                    this.PictureBoxComentarios.Location = new Point(548, txtUrl.Bottom + 10);
                    this.PictureBoxCompartir.Location = new Point(604, txtUrl.Bottom + 10);
                    this.PictureBoxOpcionesPost.Location = new Point(660, txtUrl.Bottom + 10);
                    break;
            }
            this.ResumeLayout(false);     
            this.PerformLayout();
        }

        //Boton de prueba para insertar una imagen al post
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
