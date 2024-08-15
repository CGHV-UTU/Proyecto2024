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
        private Label lblNombre;
        private RichTextBox txtBox;
        private PictureBox PictureBoxLike;
        private PictureBox PictureBoxReportar;
        private PictureBox PictureBoxUsuario;

        public CommentControl(string nombre, string texto)
        {
            InitializeComponent();
            iniciar();
            lblNombre.Text = nombre;
            txtBox.Text = texto;
            txtBox.ReadOnly = true;
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

        private void iniciar()
        {
            this.lblNombre = new Label();
            this.txtBox = new RichTextBox();
            this.PictureBoxLike = new PictureBox();
            this.PictureBoxReportar = new PictureBox();
            this.PictureBoxUsuario = new PictureBox();
            this.SuspendLayout();

            // lblNombre
            this.lblNombre.AutoSize = true;
            this.lblNombre.Location = new System.Drawing.Point(80, 10);
            this.lblNombre.Name = "lblNombre";
            this.lblNombre.Size = new System.Drawing.Size(50, 13);
            this.lblNombre.TabIndex = 0;

            // txtBox
            this.txtBox.Location = new System.Drawing.Point(80, 30); 
            this.txtBox.Name = "txtBox";
            this.txtBox.Size = new System.Drawing.Size(498, 106);
            this.txtBox.TabIndex = 24;

            // PictureBoxUsuario
            this.PictureBoxUsuario.Location = new System.Drawing.Point(10, 10);
            this.PictureBoxUsuario.Name = "PictureBoxUsuario";
            this.PictureBoxUsuario.Size = new System.Drawing.Size(64, 64);
            this.PictureBoxUsuario.SizeMode = PictureBoxSizeMode.StretchImage;
            this.PictureBoxUsuario.Image = Properties.Resources.User;

            // PictureBoxLike
            this.PictureBoxLike.Location = new System.Drawing.Point(600, 40); 
            this.PictureBoxLike.Name = "PictureBoxLike";
            this.PictureBoxLike.Size = new System.Drawing.Size(32, 32);
            this.PictureBoxLike.SizeMode = PictureBoxSizeMode.StretchImage;
            this.PictureBoxLike.Image = Properties.Resources.like_infini;
            this.PictureBoxLike.Click += PictureBoxLike_Click;

            // PictureBoxReportar
            this.PictureBoxReportar.Location = new System.Drawing.Point(600, 80);
            this.PictureBoxReportar.Name = "PictureBoxReportar";
            this.PictureBoxReportar.Size = new System.Drawing.Size(32, 32);
            this.PictureBoxReportar.SizeMode = PictureBoxSizeMode.StretchImage;
            this.PictureBoxReportar.Image = Properties.Resources.reportar;

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
            this.Size = new System.Drawing.Size(650, 150);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
