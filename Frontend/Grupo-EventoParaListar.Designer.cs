
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
            this.pnlTop = new System.Windows.Forms.Panel();
            this.pnlBot = new System.Windows.Forms.Panel();
            this.pbxUnirse = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBoxImagen)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxUnirse)).BeginInit();
            this.SuspendLayout();
            // 
            // PictureBoxImagen
            // 
            this.PictureBoxImagen.Location = new System.Drawing.Point(3, 3);
            this.PictureBoxImagen.Name = "PictureBoxImagen";
            this.PictureBoxImagen.Size = new System.Drawing.Size(10, 10);
            this.PictureBoxImagen.TabIndex = 0;
            this.PictureBoxImagen.TabStop = false;
            // 
            // lblNombre
            // 
            this.lblNombre.AutoSize = true;
            this.lblNombre.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNombre.Location = new System.Drawing.Point(137, 19);
            this.lblNombre.Name = "lblNombre";
            this.lblNombre.Size = new System.Drawing.Size(0, 24);
            this.lblNombre.TabIndex = 1;
            // 
            // pnlTop
            // 
            this.pnlTop.BackColor = System.Drawing.Color.SlateBlue;
            this.pnlTop.Location = new System.Drawing.Point(47, 3);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(200, 3);
            this.pnlTop.TabIndex = 73;
            // 
            // pnlBot
            // 
            this.pnlBot.BackColor = System.Drawing.Color.SlateBlue;
            this.pnlBot.Location = new System.Drawing.Point(47, 61);
            this.pnlBot.Name = "pnlBot";
            this.pnlBot.Size = new System.Drawing.Size(200, 3);
            this.pnlBot.TabIndex = 74;
            // 
            // pbxUnirse
            // 
            this.pbxUnirse.Location = new System.Drawing.Point(253, 57);
            this.pbxUnirse.Name = "pbxUnirse";
            this.pbxUnirse.Size = new System.Drawing.Size(38, 10);
            this.pbxUnirse.TabIndex = 75;
            this.pbxUnirse.TabStop = false;
            this.pbxUnirse.Click += new System.EventHandler(this.pbxUnirse_Click);
            // 
            // Grupo_EventoParaListar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pbxUnirse);
            this.Controls.Add(this.pnlBot);
            this.Controls.Add(this.pnlTop);
            this.Controls.Add(this.lblNombre);
            this.Controls.Add(this.PictureBoxImagen);
            this.Name = "Grupo_EventoParaListar";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Size = new System.Drawing.Size(300, 67);
            this.Click += new System.EventHandler(this.Grupo_EventoParaListar_Click);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBoxImagen)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxUnirse)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox PictureBoxImagen;
        private System.Windows.Forms.Label lblNombre;
        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Panel pnlBot;
        private System.Windows.Forms.PictureBox pbxUnirse;
    }
}
