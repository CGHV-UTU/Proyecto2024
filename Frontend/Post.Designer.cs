
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
            this.txtUrl = new System.Windows.Forms.TextBox();
            this.pbxImagen = new System.Windows.Forms.PictureBox();
            this.btnCrear = new System.Windows.Forms.PictureBox();
            this.btnVideo = new System.Windows.Forms.PictureBox();
            this.btnImagen = new System.Windows.Forms.PictureBox();
            this.lblPost = new System.Windows.Forms.Label();
            this.lblEvento = new System.Windows.Forms.Label();
            this.lblGrupo = new System.Windows.Forms.Label();
            this.btnUbicacion = new System.Windows.Forms.PictureBox();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.txtDescripcion = new System.Windows.Forms.TextBox();
            this.dtpFechaInicio = new System.Windows.Forms.DateTimePicker();
            this.dtpFechaFinal = new System.Windows.Forms.DateTimePicker();
            ((System.ComponentModel.ISupportInitialize)(this.pbxImagen)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnCrear)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnVideo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnImagen)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnUbicacion)).BeginInit();
            this.SuspendLayout();
            // 
            // txtTexto
            // 
            this.txtTexto.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.txtTexto.Location = new System.Drawing.Point(12, 44);
            this.txtTexto.Multiline = true;
            this.txtTexto.Name = "txtTexto";
            this.txtTexto.Size = new System.Drawing.Size(381, 135);
            this.txtTexto.TabIndex = 0;
            // 
            // txtUrl
            // 
            this.txtUrl.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.txtUrl.Location = new System.Drawing.Point(12, 244);
            this.txtUrl.Name = "txtUrl";
            this.txtUrl.Size = new System.Drawing.Size(381, 20);
            this.txtUrl.TabIndex = 29;
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
            // btnCrear
            // 
            this.btnCrear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCrear.BackColor = System.Drawing.Color.Transparent;
            this.btnCrear.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCrear.Image = global::Frontend.Properties.Resources.crearPostcrear;
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
            this.btnVideo.BackColor = System.Drawing.Color.Transparent;
            this.btnVideo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnVideo.Image = global::Frontend.Properties.Resources.Video2222;
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
            this.btnImagen.BackColor = System.Drawing.Color.Transparent;
            this.btnImagen.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnImagen.Image = global::Frontend.Properties.Resources.Foto;
            this.btnImagen.Location = new System.Drawing.Point(338, 185);
            this.btnImagen.Name = "btnImagen";
            this.btnImagen.Size = new System.Drawing.Size(50, 50);
            this.btnImagen.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.btnImagen.TabIndex = 25;
            this.btnImagen.TabStop = false;
            this.btnImagen.Click += new System.EventHandler(this.btnImagen_Click);
            // 
            // lblPost
            // 
            this.lblPost.AutoSize = true;
            this.lblPost.Location = new System.Drawing.Point(12, 9);
            this.lblPost.Name = "lblPost";
            this.lblPost.Size = new System.Drawing.Size(28, 13);
            this.lblPost.TabIndex = 30;
            this.lblPost.Text = "Post";
            this.lblPost.Click += new System.EventHandler(this.lblPost_Click);
            // 
            // lblEvento
            // 
            this.lblEvento.AutoSize = true;
            this.lblEvento.Location = new System.Drawing.Point(190, 9);
            this.lblEvento.Name = "lblEvento";
            this.lblEvento.Size = new System.Drawing.Size(41, 13);
            this.lblEvento.TabIndex = 31;
            this.lblEvento.Text = "Evento";
            this.lblEvento.Click += new System.EventHandler(this.lblEvento_Click);
            // 
            // lblGrupo
            // 
            this.lblGrupo.AutoSize = true;
            this.lblGrupo.Location = new System.Drawing.Point(358, 9);
            this.lblGrupo.Name = "lblGrupo";
            this.lblGrupo.Size = new System.Drawing.Size(36, 13);
            this.lblGrupo.TabIndex = 32;
            this.lblGrupo.Text = "Grupo";
            this.lblGrupo.Click += new System.EventHandler(this.lblGrupo_Click);
            // 
            // btnUbicacion
            // 
            this.btnUbicacion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUbicacion.BackColor = System.Drawing.Color.Transparent;
            this.btnUbicacion.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnUbicacion.Image = global::Frontend.Properties.Resources.buscar;
            this.btnUbicacion.Location = new System.Drawing.Point(226, 185);
            this.btnUbicacion.Name = "btnUbicacion";
            this.btnUbicacion.Size = new System.Drawing.Size(50, 50);
            this.btnUbicacion.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.btnUbicacion.TabIndex = 33;
            this.btnUbicacion.TabStop = false;
            this.btnUbicacion.Click += new System.EventHandler(this.btnUbicacion_Click);
            // 
            // txtNombre
            // 
            this.txtNombre.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.txtNombre.Location = new System.Drawing.Point(12, 44);
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(381, 20);
            this.txtNombre.TabIndex = 34;
            // 
            // txtDescripcion
            // 
            this.txtDescripcion.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.txtDescripcion.Location = new System.Drawing.Point(12, 70);
            this.txtDescripcion.Multiline = true;
            this.txtDescripcion.Name = "txtDescripcion";
            this.txtDescripcion.Size = new System.Drawing.Size(381, 86);
            this.txtDescripcion.TabIndex = 35;
            // 
            // dtpFechaInicio
            // 
            this.dtpFechaInicio.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.dtpFechaInicio.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaInicio.Location = new System.Drawing.Point(15, 186);
            this.dtpFechaInicio.Name = "dtpFechaInicio";
            this.dtpFechaInicio.ShowUpDown = true;
            this.dtpFechaInicio.Size = new System.Drawing.Size(200, 20);
            this.dtpFechaInicio.TabIndex = 36;
            // 
            // dtpFechaFinal
            // 
            this.dtpFechaFinal.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.dtpFechaFinal.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaFinal.Location = new System.Drawing.Point(15, 212);
            this.dtpFechaFinal.Name = "dtpFechaFinal";
            this.dtpFechaFinal.ShowUpDown = true;
            this.dtpFechaFinal.Size = new System.Drawing.Size(200, 20);
            this.dtpFechaFinal.TabIndex = 37;
            // 
            // Post
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.ClientSize = new System.Drawing.Size(400, 450);
            this.Controls.Add(this.dtpFechaFinal);
            this.Controls.Add(this.dtpFechaInicio);
            this.Controls.Add(this.txtDescripcion);
            this.Controls.Add(this.txtNombre);
            this.Controls.Add(this.btnUbicacion);
            this.Controls.Add(this.lblGrupo);
            this.Controls.Add(this.lblEvento);
            this.Controls.Add(this.lblPost);
            this.Controls.Add(this.txtUrl);
            this.Controls.Add(this.pbxImagen);
            this.Controls.Add(this.btnCrear);
            this.Controls.Add(this.btnVideo);
            this.Controls.Add(this.btnImagen);
            this.Controls.Add(this.txtTexto);
            this.Name = "Post";
            this.Text = "Post";
            ((System.ComponentModel.ISupportInitialize)(this.pbxImagen)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnCrear)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnVideo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnImagen)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnUbicacion)).EndInit();
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
        private System.Windows.Forms.Label lblPost;
        private System.Windows.Forms.Label lblEvento;
        private System.Windows.Forms.Label lblGrupo;
        private System.Windows.Forms.PictureBox btnUbicacion;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.TextBox txtDescripcion;
        private System.Windows.Forms.DateTimePicker dtpFechaInicio;
        private System.Windows.Forms.DateTimePicker dtpFechaFinal;
    }
}