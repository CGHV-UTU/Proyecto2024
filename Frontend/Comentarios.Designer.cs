
namespace Frontend
{
    partial class Comentarios
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.PanelComentarios = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // PanelComentarios
            // 
            this.PanelComentarios.Location = new System.Drawing.Point(12, 12);
            this.PanelComentarios.Name = "PanelComentarios";
            this.PanelComentarios.Size = new System.Drawing.Size(972, 640);
            this.PanelComentarios.TabIndex = 19;
            this.PanelComentarios.Visible = false;
            // 
            // Comentarios
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(996, 664);
            this.Controls.Add(this.PanelComentarios);
            this.Name = "Comentarios";
            this.Text = "Comentarios";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel PanelComentarios;
        private System.Windows.Forms.PictureBox PictureBoxSalir;
    }
}