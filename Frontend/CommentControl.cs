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
    public partial class CommentControl : UserControl
    {

        public CommentControl(string nombre, string texto)
        {
            InitializeComponent();
            Iniciar();

            lblNombre.Text = nombre;
            txtBox.Text = texto;
            txtBox.ReadOnly = true;

            AjustarTamaño();
        }

        private void AjustarTamaño()
        {
            txtBox.Size = new Size(this.Width - 70, this.Height - 65);
        }


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

        private void Iniciar()
        {
            this.lblNombre = new Label();
            this.txtBox = new RichTextBox();
            this.PictureBoxLike = new PictureBox();
            this.PictureBoxReportar = new PictureBox();
            this.PictureBoxUsuario = new PictureBox();
            this.SuspendLayout();

            // lblNombre
            this.lblNombre.AutoSize = true;
            this.lblNombre.Location = new System.Drawing.Point(59, 37);
            this.lblNombre.Name = "lblNombre";
            this.lblNombre.Size = new System.Drawing.Size(44, 13);
            this.lblNombre.TabIndex = 0;

            // txtBox
            this.txtBox.Location = new System.Drawing.Point(3, 53);
            this.txtBox.Name = "txtBox";
            this.txtBox.Size = new System.Drawing.Size(this.Width - 70, this.Height - 65);
            this.txtBox.TabIndex = 31;

            // PictureBoxUsuario
            this.PictureBoxUsuario.Location = new System.Drawing.Point(3, 3);
            this.PictureBoxUsuario.Name = "PictureBoxUsuario";
            this.PictureBoxUsuario.Size = new System.Drawing.Size(50, 50);
            this.PictureBoxUsuario.SizeMode = PictureBoxSizeMode.StretchImage;
            this.PictureBoxUsuario.Image = Properties.Resources.User;
            this.Cursor = Cursors.Hand;

            // PictureBoxLike
            this.PictureBoxLike.Location = new System.Drawing.Point(410, 53);
            this.PictureBoxLike.Name = "PictureBoxLike";
            this.PictureBoxLike.Size = new System.Drawing.Size(50, 50);
            this.PictureBoxLike.SizeMode = PictureBoxSizeMode.StretchImage;
            this.PictureBoxLike.Image = Properties.Resources.like_infini;
            this.PictureBoxLike.Click += PictureBoxLike_Click;
            this.Cursor = Cursors.Hand;

            // PictureBoxReportar
            this.PictureBoxReportar.Location = new System.Drawing.Point(410, 109);
            this.PictureBoxReportar.Name = "PictureBoxReportar";
            this.PictureBoxReportar.Size = new System.Drawing.Size(50,50);
            this.PictureBoxReportar.SizeMode = PictureBoxSizeMode.StretchImage;
            this.PictureBoxReportar.Image = Properties.Resources.reportar;
            this.Cursor = Cursors.Hand;

            // Añadir controles al CommentControl
            this.Controls.Add(this.lblNombre);
            this.Controls.Add(this.txtBox);
            this.Controls.Add(this.PictureBoxLike);
            this.Controls.Add(this.PictureBoxReportar);
            this.Controls.Add(this.PictureBoxUsuario);

            // Configuración final del control
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.Name = "CommentControl";
            this.Size = new System.Drawing.Size(465, 171);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
