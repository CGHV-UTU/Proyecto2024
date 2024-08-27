
namespace Frontend
{
    partial class NotificacionControl
    {
        /// <summary> 
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur de composants

        /// <summary> 
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas 
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblNoti = new System.Windows.Forms.Label();
            this.PictureBoxNotificacion = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBoxNotificacion)).BeginInit();
            this.SuspendLayout();
            // 
            // lblNoti
            // 
            this.lblNoti.AutoSize = true;
            this.lblNoti.Location = new System.Drawing.Point(77, 37);
            this.lblNoti.Name = "lblNoti";
            this.lblNoti.Size = new System.Drawing.Size(91, 13);
            this.lblNoti.TabIndex = 0;
            this.lblNoti.Text = "Texto notificación";
            // 
            // PictureBoxNotificacion
            // 
            this.PictureBoxNotificacion.Cursor = System.Windows.Forms.Cursors.Hand;
            this.PictureBoxNotificacion.Image = global::Frontend.Properties.Resources.notificacionLike;
            this.PictureBoxNotificacion.Location = new System.Drawing.Point(21, 18);
            this.PictureBoxNotificacion.Name = "PictureBoxNotificacion";
            this.PictureBoxNotificacion.Size = new System.Drawing.Size(50, 50);
            this.PictureBoxNotificacion.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PictureBoxNotificacion.TabIndex = 24;
            this.PictureBoxNotificacion.TabStop = false;
            this.PictureBoxNotificacion.Visible = false;
            // 
            // NotificacionControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.PictureBoxNotificacion);
            this.Controls.Add(this.lblNoti);
            this.Name = "NotificacionControl";
            this.Size = new System.Drawing.Size(600, 89);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBoxNotificacion)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblNoti;
        private System.Windows.Forms.PictureBox PictureBoxNotificacion;
    }
}
