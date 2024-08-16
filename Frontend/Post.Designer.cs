
namespace Frontend
{
    partial class Post
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
            this.txtTexto = new System.Windows.Forms.TextBox();
            this.btnCrear = new System.Windows.Forms.PictureBox();
            this.btnVideo = new System.Windows.Forms.PictureBox();
            this.btnImagen = new System.Windows.Forms.PictureBox();
            this.pbxImagen = new System.Windows.Forms.PictureBox();
            this.txtUrl = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.btnCrear)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnVideo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnImagen)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxImagen)).BeginInit();
            this.SuspendLayout();
            // 
            // txtTexto
            // 
            this.txtTexto.Location = new System.Drawing.Point(12, 37);
            this.txtTexto.Multiline = true;
            this.txtTexto.Name = "txtTexto";
            this.txtTexto.Size = new System.Drawing.Size(381, 142);
            this.txtTexto.TabIndex = 0;
            // 
            // btnCrear
            // 
            this.btnCrear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCrear.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCrear.Image = global::Frontend.Properties.Resources.crearPost_removebg_preview;
            this.btnCrear.Location = new System.Drawing.Point(12, 270);
            this.btnCrear.Name = "btnCrear";
            this.btnCrear.Size = new System.Drawing.Size(372, 219);
            this.btnCrear.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnCrear.TabIndex = 27;
            this.btnCrear.TabStop = false;
            this.btnCrear.Click += new System.EventHandler(this.btnCrear_Click);
            // 
            // btnVideo
            // 
            this.btnVideo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnVideo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnVideo.Image = global::Frontend.Properties.Resources.video;
            this.btnVideo.Location = new System.Drawing.Point(282, 185);
            this.btnVideo.Name = "btnVideo";
            this.btnVideo.Size = new System.Drawing.Size(50, 50);
            this.btnVideo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.btnVideo.TabIndex = 26;
            this.btnVideo.TabStop = false;
            this.btnVideo.Click += new System.EventHandler(this.btnVideo_Click);
            // 
            // btnImagen
            // 
            this.btnImagen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnImagen.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnImagen.Image = global::Frontend.Properties.Resources.foto_blanca;
            this.btnImagen.Location = new System.Drawing.Point(338, 185);
            this.btnImagen.Name = "btnImagen";
            this.btnImagen.Size = new System.Drawing.Size(50, 50);
            this.btnImagen.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.btnImagen.TabIndex = 25;
            this.btnImagen.TabStop = false;
            this.btnImagen.Click += new System.EventHandler(this.btnImagen_Click);
            // 
            // pbxImagen
            // 
            this.pbxImagen.Location = new System.Drawing.Point(12, 241);
            this.pbxImagen.Name = "pbxImagen";
            this.pbxImagen.Size = new System.Drawing.Size(381, 212);
            this.pbxImagen.TabIndex = 28;
            this.pbxImagen.TabStop = false;
            this.pbxImagen.Visible = false;
            // 
            // txtUrl
            // 
            this.txtUrl.Location = new System.Drawing.Point(12, 244);
            this.txtUrl.Name = "txtUrl";
            this.txtUrl.Size = new System.Drawing.Size(381, 20);
            this.txtUrl.TabIndex = 29;
            // 
            // Post
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(400, 450);
            this.Controls.Add(this.txtUrl);
            this.Controls.Add(this.pbxImagen);
            this.Controls.Add(this.btnCrear);
            this.Controls.Add(this.btnVideo);
            this.Controls.Add(this.btnImagen);
            this.Controls.Add(this.txtTexto);
            this.Name = "Post";
            this.Text = "Post";
            ((System.ComponentModel.ISupportInitialize)(this.btnCrear)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnVideo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnImagen)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxImagen)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtTexto;
        private System.Windows.Forms.PictureBox btnImagen;
        private System.Windows.Forms.PictureBox btnVideo;
        private System.Windows.Forms.PictureBox btnCrear;
        private System.Windows.Forms.PictureBox pbxImagen;
        private System.Windows.Forms.TextBox txtUrl;
    }
}