using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Frontend
{
    public partial class NotificacionControl : UserControl
    {
        private string modo;
        private string idioma;
        public NotificacionControl(dynamic notificacion)
        {
            InitializeComponent();
            Iniciar();
            lblNoti.Text = notificacion.texto;
            byte[] imagen = Convert.FromBase64String(Convert.ToString(notificacion.imagen));
            MemoryStream ms = new MemoryStream(imagen);
            Bitmap bitmap = new Bitmap(ms);
            PictureBoxNotificacion.Image = bitmap;
            this.idioma = idioma;
        }


        private void Iniciar()
        {
            this.lblNoti = new Label();
            this.PictureBoxNotificacion = new PictureBox();
            this.SuspendLayout();

            // lblNoti
            this.lblNoti.AutoSize = true;
            this.lblNoti.Location = new System.Drawing.Point(59, 15);
            this.lblNoti.Name = "lblNoti";
            this.lblNoti.Size = new System.Drawing.Size(75, 13);
            this.lblNoti.TabIndex = 0;

            // PictureBoxNotificacion
            this.PictureBoxNotificacion.Location = new System.Drawing.Point(3, 3);
            this.PictureBoxNotificacion.Name = "PictureBoxNotificacion";
            this.PictureBoxNotificacion.Size = new System.Drawing.Size(50, 50);
            this.PictureBoxNotificacion.Image = Properties.Resources.notificacionLike;
            this.PictureBoxNotificacion.SizeMode = PictureBoxSizeMode.StretchImage;
            this.Cursor = Cursors.Hand;

            // Añadir controles al NotificacionControl
            this.Controls.Add(this.lblNoti);
            this.Controls.Add(this.PictureBoxNotificacion);

            // Configuración final del control
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.Name = "NotificacionControl";
            this.Size = new System.Drawing.Size(600, 87); 
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        public void aplicarModoOscuro()
        {
            lblNoti.ForeColor = Color.White;
            this.BackColor = Color.FromArgb(40, 40, 40);
        }
    }
}
