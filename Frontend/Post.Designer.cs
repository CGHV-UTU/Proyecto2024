
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Post));
            this.txtTexto = new System.Windows.Forms.TextBox();
            this.txtUrl = new System.Windows.Forms.TextBox();
            this.lblPost = new System.Windows.Forms.Label();
            this.lblEvento = new System.Windows.Forms.Label();
            this.lblGrupo = new System.Windows.Forms.Label();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.txtDescripcion = new System.Windows.Forms.TextBox();
            this.dtpFechaInicio = new System.Windows.Forms.DateTimePicker();
            this.dtpFechaFinal = new System.Windows.Forms.DateTimePicker();
            this.pnlOpciones = new System.Windows.Forms.Panel();
            this.pnlNombre = new System.Windows.Forms.Panel();
            this.pnlTexto = new System.Windows.Forms.Panel();
            this.pnlDescripcion = new System.Windows.Forms.Panel();
            this.pnlURL = new System.Windows.Forms.Panel();
            this.pnlOpcionPost = new System.Windows.Forms.Panel();
            this.pnlOpcionEvento = new System.Windows.Forms.Panel();
            this.pnlOpcionGrupo = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.btnUbicacion = new System.Windows.Forms.PictureBox();
            this.pbxImagen = new System.Windows.Forms.PictureBox();
            this.btnCrear = new System.Windows.Forms.PictureBox();
            this.btnVideo = new System.Windows.Forms.PictureBox();
            this.btnImagen = new System.Windows.Forms.PictureBox();
            this.txtCategorias = new System.Windows.Forms.TextBox();
            this.pnlOpcionGrupo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnUbicacion)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxImagen)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnCrear)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnVideo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnImagen)).BeginInit();
            this.SuspendLayout();
            // 
            // txtTexto
            // 
            this.txtTexto.BackColor = System.Drawing.SystemColors.Window;
            this.txtTexto.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTexto.ForeColor = System.Drawing.Color.Gray;
            this.txtTexto.Location = new System.Drawing.Point(17, 44);
            this.txtTexto.MaxLength = 100;
            this.txtTexto.Multiline = true;
            this.txtTexto.Name = "txtTexto";
            this.txtTexto.Size = new System.Drawing.Size(381, 135);
            this.txtTexto.TabIndex = 0;
            this.txtTexto.Text = "Texto";
            this.txtTexto.Enter += new System.EventHandler(this.txtTexto_Enter);
            this.txtTexto.Leave += new System.EventHandler(this.txtTexto_Leave);
            // 
            // txtUrl
            // 
            this.txtUrl.BackColor = System.Drawing.SystemColors.Window;
            this.txtUrl.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUrl.ForeColor = System.Drawing.Color.Gray;
            this.txtUrl.Location = new System.Drawing.Point(17, 244);
            this.txtUrl.MaxLength = 50;
            this.txtUrl.Name = "txtUrl";
            this.txtUrl.Size = new System.Drawing.Size(381, 26);
            this.txtUrl.TabIndex = 29;
            this.txtUrl.Enter += new System.EventHandler(this.txtUrl_Enter);
            this.txtUrl.Leave += new System.EventHandler(this.txtUrl_Leave);
            // 
            // lblPost
            // 
            this.lblPost.AutoSize = true;
            this.lblPost.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.lblPost.ForeColor = System.Drawing.SystemColors.Desktop;
            this.lblPost.Location = new System.Drawing.Point(35, 3);
            this.lblPost.Name = "lblPost";
            this.lblPost.Size = new System.Drawing.Size(46, 24);
            this.lblPost.TabIndex = 30;
            this.lblPost.Text = "Post";
            this.lblPost.Click += new System.EventHandler(this.lblPost_Click);
            // 
            // lblEvento
            // 
            this.lblEvento.AutoSize = true;
            this.lblEvento.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.lblEvento.ForeColor = System.Drawing.SystemColors.Desktop;
            this.lblEvento.Location = new System.Drawing.Point(176, 3);
            this.lblEvento.Name = "lblEvento";
            this.lblEvento.Size = new System.Drawing.Size(69, 24);
            this.lblEvento.TabIndex = 31;
            this.lblEvento.Text = "Evento";
            this.lblEvento.Click += new System.EventHandler(this.lblEvento_Click);
            // 
            // lblGrupo
            // 
            this.lblGrupo.AutoSize = true;
            this.lblGrupo.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.lblGrupo.ForeColor = System.Drawing.SystemColors.Desktop;
            this.lblGrupo.Location = new System.Drawing.Point(323, 3);
            this.lblGrupo.Name = "lblGrupo";
            this.lblGrupo.Size = new System.Drawing.Size(63, 24);
            this.lblGrupo.TabIndex = 32;
            this.lblGrupo.Text = "Grupo";
            this.lblGrupo.Click += new System.EventHandler(this.lblGrupo_Click);
            // 
            // txtNombre
            // 
            this.txtNombre.BackColor = System.Drawing.SystemColors.Window;
            this.txtNombre.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNombre.ForeColor = System.Drawing.Color.Gray;
            this.txtNombre.Location = new System.Drawing.Point(17, 44);
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(381, 26);
            this.txtNombre.TabIndex = 34;
            this.txtNombre.Text = "Nombre";
            this.txtNombre.Enter += new System.EventHandler(this.txtNombre_Enter);
            this.txtNombre.Leave += new System.EventHandler(this.txtNombre_Leave);
            // 
            // txtDescripcion
            // 
            this.txtDescripcion.BackColor = System.Drawing.SystemColors.Window;
            this.txtDescripcion.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDescripcion.ForeColor = System.Drawing.Color.Gray;
            this.txtDescripcion.Location = new System.Drawing.Point(17, 76);
            this.txtDescripcion.MaxLength = 100;
            this.txtDescripcion.Multiline = true;
            this.txtDescripcion.Name = "txtDescripcion";
            this.txtDescripcion.Size = new System.Drawing.Size(381, 86);
            this.txtDescripcion.TabIndex = 35;
            this.txtDescripcion.Text = "Descripción";
            this.txtDescripcion.Enter += new System.EventHandler(this.txtDescripcion_Enter);
            this.txtDescripcion.Leave += new System.EventHandler(this.txtDescripcion_Leave);
            // 
            // dtpFechaInicio
            // 
            this.dtpFechaInicio.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFechaInicio.CalendarForeColor = System.Drawing.SystemColors.GrayText;
            this.dtpFechaInicio.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.dtpFechaInicio.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaInicio.Location = new System.Drawing.Point(18, 186);
            this.dtpFechaInicio.Name = "dtpFechaInicio";
            this.dtpFechaInicio.ShowUpDown = true;
            this.dtpFechaInicio.Size = new System.Drawing.Size(200, 20);
            this.dtpFechaInicio.TabIndex = 36;
            this.dtpFechaInicio.ValueChanged += new System.EventHandler(this.dtpFechaInicio_ValueChanged);
            // 
            // dtpFechaFinal
            // 
            this.dtpFechaFinal.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFechaFinal.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.dtpFechaFinal.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaFinal.Location = new System.Drawing.Point(19, 212);
            this.dtpFechaFinal.Name = "dtpFechaFinal";
            this.dtpFechaFinal.ShowUpDown = true;
            this.dtpFechaFinal.Size = new System.Drawing.Size(200, 20);
            this.dtpFechaFinal.TabIndex = 37;
            this.dtpFechaFinal.ValueChanged += new System.EventHandler(this.dtpFechaFinal_ValueChanged);
            // 
            // pnlOpciones
            // 
            this.pnlOpciones.BackColor = System.Drawing.Color.SlateBlue;
            this.pnlOpciones.Location = new System.Drawing.Point(26, 33);
            this.pnlOpciones.Name = "pnlOpciones";
            this.pnlOpciones.Size = new System.Drawing.Size(363, 3);
            this.pnlOpciones.TabIndex = 71;
            // 
            // pnlNombre
            // 
            this.pnlNombre.BackColor = System.Drawing.Color.SlateBlue;
            this.pnlNombre.Location = new System.Drawing.Point(17, 68);
            this.pnlNombre.Name = "pnlNombre";
            this.pnlNombre.Size = new System.Drawing.Size(381, 3);
            this.pnlNombre.TabIndex = 73;
            this.pnlNombre.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlNombre_Paint);
            // 
            // pnlTexto
            // 
            this.pnlTexto.BackColor = System.Drawing.Color.SlateBlue;
            this.pnlTexto.Location = new System.Drawing.Point(17, 176);
            this.pnlTexto.Name = "pnlTexto";
            this.pnlTexto.Size = new System.Drawing.Size(381, 3);
            this.pnlTexto.TabIndex = 74;
            // 
            // pnlDescripcion
            // 
            this.pnlDescripcion.BackColor = System.Drawing.Color.SlateBlue;
            this.pnlDescripcion.Location = new System.Drawing.Point(17, 159);
            this.pnlDescripcion.Name = "pnlDescripcion";
            this.pnlDescripcion.Size = new System.Drawing.Size(381, 3);
            this.pnlDescripcion.TabIndex = 75;
            this.pnlDescripcion.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlDescripcion_Paint);
            // 
            // pnlURL
            // 
            this.pnlURL.BackColor = System.Drawing.Color.SlateBlue;
            this.pnlURL.Location = new System.Drawing.Point(17, 267);
            this.pnlURL.Name = "pnlURL";
            this.pnlURL.Size = new System.Drawing.Size(381, 3);
            this.pnlURL.TabIndex = 75;
            // 
            // pnlOpcionPost
            // 
            this.pnlOpcionPost.BackColor = System.Drawing.Color.SlateBlue;
            this.pnlOpcionPost.Location = new System.Drawing.Point(26, 26);
            this.pnlOpcionPost.Name = "pnlOpcionPost";
            this.pnlOpcionPost.Size = new System.Drawing.Size(70, 3);
            this.pnlOpcionPost.TabIndex = 72;
            // 
            // pnlOpcionEvento
            // 
            this.pnlOpcionEvento.BackColor = System.Drawing.Color.SlateBlue;
            this.pnlOpcionEvento.Location = new System.Drawing.Point(175, 26);
            this.pnlOpcionEvento.Name = "pnlOpcionEvento";
            this.pnlOpcionEvento.Size = new System.Drawing.Size(70, 3);
            this.pnlOpcionEvento.TabIndex = 74;
            // 
            // pnlOpcionGrupo
            // 
            this.pnlOpcionGrupo.BackColor = System.Drawing.Color.SlateBlue;
            this.pnlOpcionGrupo.Controls.Add(this.panel5);
            this.pnlOpcionGrupo.Location = new System.Drawing.Point(319, 26);
            this.pnlOpcionGrupo.Name = "pnlOpcionGrupo";
            this.pnlOpcionGrupo.Size = new System.Drawing.Size(70, 3);
            this.pnlOpcionGrupo.TabIndex = 75;
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.SlateBlue;
            this.panel5.Location = new System.Drawing.Point(53, -7);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(10, 10);
            this.panel5.TabIndex = 73;
            // 
            // btnUbicacion
            // 
            this.btnUbicacion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUbicacion.BackColor = System.Drawing.Color.Transparent;
            this.btnUbicacion.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnUbicacion.Image = global::Frontend.Properties.Resources.buscar;
            this.btnUbicacion.Location = new System.Drawing.Point(238, 185);
            this.btnUbicacion.Name = "btnUbicacion";
            this.btnUbicacion.Size = new System.Drawing.Size(50, 50);
            this.btnUbicacion.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.btnUbicacion.TabIndex = 33;
            this.btnUbicacion.TabStop = false;
            this.btnUbicacion.Click += new System.EventHandler(this.btnUbicacion_Click);
            // 
            // pbxImagen
            // 
            this.pbxImagen.Location = new System.Drawing.Point(17, 272);
            this.pbxImagen.Name = "pbxImagen";
            this.pbxImagen.Size = new System.Drawing.Size(381, 212);
            this.pbxImagen.TabIndex = 28;
            this.pbxImagen.TabStop = false;
            this.pbxImagen.Visible = false;
            this.pbxImagen.Click += new System.EventHandler(this.pbxImagen_Click);
            // 
            // btnCrear
            // 
            this.btnCrear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCrear.BackColor = System.Drawing.Color.Transparent;
            this.btnCrear.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCrear.Image = ((System.Drawing.Image)(resources.GetObject("btnCrear.Image")));
            this.btnCrear.Location = new System.Drawing.Point(22, 404);
            this.btnCrear.Name = "btnCrear";
            this.btnCrear.Size = new System.Drawing.Size(372, 87);
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
            this.btnVideo.Location = new System.Drawing.Point(293, 185);
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
            this.btnImagen.Location = new System.Drawing.Point(349, 185);
            this.btnImagen.Name = "btnImagen";
            this.btnImagen.Size = new System.Drawing.Size(50, 50);
            this.btnImagen.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.btnImagen.TabIndex = 25;
            this.btnImagen.TabStop = false;
            this.btnImagen.Click += new System.EventHandler(this.btnImagen_Click);
            // 
            // txtCategorias
            // 
            this.txtCategorias.BackColor = System.Drawing.SystemColors.Window;
            this.txtCategorias.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCategorias.ForeColor = System.Drawing.Color.Gray;
            this.txtCategorias.Location = new System.Drawing.Point(16, 184);
            this.txtCategorias.Multiline = true;
            this.txtCategorias.Name = "txtCategorias";
            this.txtCategorias.Size = new System.Drawing.Size(216, 54);
            this.txtCategorias.TabIndex = 76;
            this.txtCategorias.Text = "Categorías";
            // 
            // Post
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(420, 511);
            this.Controls.Add(this.txtCategorias);
            this.Controls.Add(this.pnlOpcionGrupo);
            this.Controls.Add(this.pnlOpcionEvento);
            this.Controls.Add(this.pnlOpcionPost);
            this.Controls.Add(this.pnlURL);
            this.Controls.Add(this.pnlDescripcion);
            this.Controls.Add(this.pnlTexto);
            this.Controls.Add(this.pnlNombre);
            this.Controls.Add(this.pnlOpciones);
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
            this.pnlOpcionGrupo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.btnUbicacion)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxImagen)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnCrear)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnVideo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnImagen)).EndInit();
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
        private System.Windows.Forms.Panel pnlOpciones;
        private System.Windows.Forms.Panel pnlNombre;
        private System.Windows.Forms.Panel pnlTexto;
        private System.Windows.Forms.Panel pnlDescripcion;
        private System.Windows.Forms.Panel pnlURL;
        private System.Windows.Forms.Panel pnlOpcionPost;
        private System.Windows.Forms.Panel pnlOpcionEvento;
        private System.Windows.Forms.Panel pnlOpcionGrupo;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.TextBox txtCategorias;
    }
}