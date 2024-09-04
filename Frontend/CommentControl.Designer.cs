
namespace Frontend
{
    partial class CommentControl
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
            this.txtBox = new System.Windows.Forms.RichTextBox();
            this.PictureBoxReportar = new System.Windows.Forms.PictureBox();
            this.PictureBoxLike = new System.Windows.Forms.PictureBox();
            this.PictureBoxUsuario = new System.Windows.Forms.PictureBox();
            this.lblNombre = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBoxReportar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBoxLike)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBoxUsuario)).BeginInit();
            this.SuspendLayout();
            // 
            // txtBox
            // 
            this.txtBox.Location = new System.Drawing.Point(3, 53);
            this.txtBox.Name = "txtBox";
            this.txtBox.Size = new System.Drawing.Size(401, 106);
            this.txtBox.TabIndex = 31;
            this.txtBox.Text = "";
            this.txtBox.Visible = false;
            // 
            // PictureBoxReportar
            // 
            this.PictureBoxReportar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.PictureBoxReportar.Image = global::Frontend.Properties.Resources.reportar;
            this.PictureBoxReportar.Location = new System.Drawing.Point(410, 109);
            this.PictureBoxReportar.Name = "PictureBoxReportar";
            this.PictureBoxReportar.Size = new System.Drawing.Size(50, 50);
            this.PictureBoxReportar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PictureBoxReportar.TabIndex = 32;
            this.PictureBoxReportar.TabStop = false;
            this.PictureBoxReportar.Visible = false;
            this.PictureBoxReportar.Click += new System.EventHandler(this.PictureBoxReportar_Click);
            // 
            // PictureBoxLike
            // 
            this.PictureBoxLike.Cursor = System.Windows.Forms.Cursors.Hand;
            this.PictureBoxLike.Image = global::Frontend.Properties.Resources.like_infini;
            this.PictureBoxLike.Location = new System.Drawing.Point(410, 53);
            this.PictureBoxLike.Name = "PictureBoxLike";
            this.PictureBoxLike.Size = new System.Drawing.Size(50, 50);
            this.PictureBoxLike.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PictureBoxLike.TabIndex = 30;
            this.PictureBoxLike.TabStop = false;
            this.PictureBoxLike.Visible = false;
            // 
            // PictureBoxUsuario
            // 
            this.PictureBoxUsuario.Cursor = System.Windows.Forms.Cursors.Hand;
            this.PictureBoxUsuario.Image = global::Frontend.Properties.Resources.User;
            this.PictureBoxUsuario.Location = new System.Drawing.Point(3, 3);
            this.PictureBoxUsuario.Name = "PictureBoxUsuario";
            this.PictureBoxUsuario.Size = new System.Drawing.Size(50, 50);
            this.PictureBoxUsuario.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PictureBoxUsuario.TabIndex = 23;
            this.PictureBoxUsuario.TabStop = false;
            this.PictureBoxUsuario.Visible = false;
            // 
            // lblNombre
            // 
            this.lblNombre.AutoSize = true;
            this.lblNombre.Location = new System.Drawing.Point(59, 37);
            this.lblNombre.Name = "lblNombre";
            this.lblNombre.Size = new System.Drawing.Size(44, 13);
            this.lblNombre.TabIndex = 29;
            this.lblNombre.Text = "Nombre";
            this.lblNombre.Visible = false;
            // 
            // CommentControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.PictureBoxReportar);
            this.Controls.Add(this.txtBox);
            this.Controls.Add(this.PictureBoxLike);
            this.Controls.Add(this.lblNombre);
            this.Controls.Add(this.PictureBoxUsuario);
            this.Name = "CommentControl";
            this.Size = new System.Drawing.Size(463, 163);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBoxReportar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBoxLike)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBoxUsuario)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox PictureBoxUsuario;
        private System.Windows.Forms.PictureBox PictureBoxLike;
        private System.Windows.Forms.RichTextBox txtBox;
        private System.Windows.Forms.PictureBox PictureBoxReportar;
        private System.Windows.Forms.Label lblNombre;
    }
}