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
    public partial class NotificacionControl : UserControl
    {

        public NotificacionControl(string notificacion)
        {
            InitializeComponent();
            Iniciar();
            lblNoti.Text = notificacion;
            AjustarTamaño();
        }

        public Image ImagenNotificacion
        {
            get { return PictureBoxNotificacion.Image; }
            set { PictureBoxNotificacion.Image = value; }
        }

        private void AjustarTamaño()
        {
            lblNoti.Size = new Size(this.Width - 70, this.Height - 20);
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
            this.Size = new System.Drawing.Size(50, 10); // Tamaño ajustado
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
