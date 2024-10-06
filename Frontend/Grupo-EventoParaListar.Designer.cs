
namespace Frontend
{
    partial class Grupo_EventoParaListar
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
            this.PictureBoxImagen = new System.Windows.Forms.PictureBox();
            this.lblNombre = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBoxImagen)).BeginInit();
            this.SuspendLayout();
            // 
            // PictureBoxImagen
            // 
            this.PictureBoxImagen.Location = new System.Drawing.Point(13, 7);
            this.PictureBoxImagen.Name = "PictureBoxImagen";
            this.PictureBoxImagen.Size = new System.Drawing.Size(50, 50);
            this.PictureBoxImagen.TabIndex = 0;
            this.PictureBoxImagen.TabStop = false;
            // 
            // lblNombre
            // 
            this.lblNombre.AutoSize = true;
            this.lblNombre.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNombre.Location = new System.Drawing.Point(137, 19);
            this.lblNombre.Name = "lblNombre";
            this.lblNombre.Size = new System.Drawing.Size(60, 24);
            this.lblNombre.TabIndex = 1;
            this.lblNombre.Text = "label1";
            // 
            // Grupo_EventoParaListar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblNombre);
            this.Controls.Add(this.PictureBoxImagen);
            this.Name = "Grupo_EventoParaListar";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Size = new System.Drawing.Size(300, 67);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBoxImagen)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox PictureBoxImagen;
        private System.Windows.Forms.Label lblNombre;
    }
}
