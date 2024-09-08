
namespace Frontend
{
    partial class PostControl
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
            this.lblNombre = new System.Windows.Forms.Label();
            this.txtDescripcion = new System.Windows.Forms.Label();
            this.txtUrl = new System.Windows.Forms.Label();
            this.PictureBoxEditar = new System.Windows.Forms.PictureBox();
            this.PictureBoxOpcionesPost = new System.Windows.Forms.PictureBox();
            this.PictureBoxCompartir = new System.Windows.Forms.PictureBox();
            this.PictureBoxComentarios = new System.Windows.Forms.PictureBox();
            this.PictureBoxLike = new System.Windows.Forms.PictureBox();
            this.PictureBoxUsuarioPost = new System.Windows.Forms.PictureBox();
            this.imagen = new System.Windows.Forms.PictureBox();
            this.txtDescripcionEditar = new System.Windows.Forms.TextBox();
            this.txtUrlEditar = new System.Windows.Forms.TextBox();
            this.btnConfirmarCambios = new System.Windows.Forms.PictureBox();
            this.btnSeleccionarImagen = new System.Windows.Forms.PictureBox();
            this.imagenEditar = new System.Windows.Forms.PictureBox();
            this.btnEliminar = new System.Windows.Forms.Label();
            this.btnReportar = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBoxEditar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBoxOpcionesPost)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBoxCompartir)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBoxComentarios)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBoxLike)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBoxUsuarioPost)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imagen)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnConfirmarCambios)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSeleccionarImagen)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imagenEditar)).BeginInit();
            this.SuspendLayout();
            // 
            // lblNombre
            // 
            this.lblNombre.AutoSize = true;
            this.lblNombre.Location = new System.Drawing.Point(136, 50);
            this.lblNombre.Name = "lblNombre";
            this.lblNombre.Size = new System.Drawing.Size(44, 13);
            this.lblNombre.TabIndex = 28;
            this.lblNombre.Text = "Nombre";
            // 
            // txtDescripcion
            // 
            this.txtDescripcion.AutoSize = true;
            this.txtDescripcion.Location = new System.Drawing.Point(86, 77);
            this.txtDescripcion.Name = "txtDescripcion";
            this.txtDescripcion.Size = new System.Drawing.Size(44, 13);
            this.txtDescripcion.TabIndex = 31;
            this.txtDescripcion.Text = "Nombre";
            // 
            // txtUrl
            // 
            this.txtUrl.AutoSize = true;
            this.txtUrl.Location = new System.Drawing.Point(86, 469);
            this.txtUrl.Name = "txtUrl";
            this.txtUrl.Size = new System.Drawing.Size(44, 13);
            this.txtUrl.TabIndex = 32;
            this.txtUrl.Text = "Nombre";
            // 
            // PictureBoxEditar
            // 
            this.PictureBoxEditar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.PictureBoxEditar.BackColor = System.Drawing.Color.Transparent;
            this.PictureBoxEditar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.PictureBoxEditar.Image = global::Frontend.Properties.Resources.editar_removebg_preview;
            this.PictureBoxEditar.Location = new System.Drawing.Point(497, 494);
            this.PictureBoxEditar.Name = "PictureBoxEditar";
            this.PictureBoxEditar.Size = new System.Drawing.Size(50, 50);
            this.PictureBoxEditar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PictureBoxEditar.TabIndex = 33;
            this.PictureBoxEditar.TabStop = false;
            this.PictureBoxEditar.Click += new System.EventHandler(this.PictureBoxEditar_Click);
            // 
            // PictureBoxOpcionesPost
            // 
            this.PictureBoxOpcionesPost.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.PictureBoxOpcionesPost.BackColor = System.Drawing.Color.Transparent;
            this.PictureBoxOpcionesPost.Cursor = System.Windows.Forms.Cursors.Hand;
            this.PictureBoxOpcionesPost.Image = global::Frontend.Properties.Resources.mas_opciones;
            this.PictureBoxOpcionesPost.Location = new System.Drawing.Point(664, 494);
            this.PictureBoxOpcionesPost.Name = "PictureBoxOpcionesPost";
            this.PictureBoxOpcionesPost.Size = new System.Drawing.Size(50, 50);
            this.PictureBoxOpcionesPost.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PictureBoxOpcionesPost.TabIndex = 26;
            this.PictureBoxOpcionesPost.TabStop = false;
            this.PictureBoxOpcionesPost.Click += new System.EventHandler(this.PictureBoxOpcionesPost_Click);
            // 
            // PictureBoxCompartir
            // 
            this.PictureBoxCompartir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.PictureBoxCompartir.BackColor = System.Drawing.Color.Transparent;
            this.PictureBoxCompartir.Cursor = System.Windows.Forms.Cursors.Hand;
            this.PictureBoxCompartir.Image = global::Frontend.Properties.Resources.compartir;
            this.PictureBoxCompartir.Location = new System.Drawing.Point(608, 494);
            this.PictureBoxCompartir.Name = "PictureBoxCompartir";
            this.PictureBoxCompartir.Size = new System.Drawing.Size(50, 50);
            this.PictureBoxCompartir.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PictureBoxCompartir.TabIndex = 25;
            this.PictureBoxCompartir.TabStop = false;
            this.PictureBoxCompartir.Click += new System.EventHandler(this.PictureBoxCompartir_Click);
            // 
            // PictureBoxComentarios
            // 
            this.PictureBoxComentarios.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.PictureBoxComentarios.BackColor = System.Drawing.Color.Transparent;
            this.PictureBoxComentarios.Cursor = System.Windows.Forms.Cursors.Hand;
            this.PictureBoxComentarios.Image = global::Frontend.Properties.Resources.comentario;
            this.PictureBoxComentarios.Location = new System.Drawing.Point(553, 494);
            this.PictureBoxComentarios.Name = "PictureBoxComentarios";
            this.PictureBoxComentarios.Size = new System.Drawing.Size(50, 50);
            this.PictureBoxComentarios.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PictureBoxComentarios.TabIndex = 24;
            this.PictureBoxComentarios.TabStop = false;
            // 
            // PictureBoxLike
            // 
            this.PictureBoxLike.Cursor = System.Windows.Forms.Cursors.Hand;
            this.PictureBoxLike.Image = global::Frontend.Properties.Resources.like_infini;
            this.PictureBoxLike.Location = new System.Drawing.Point(80, 494);
            this.PictureBoxLike.Name = "PictureBoxLike";
            this.PictureBoxLike.Size = new System.Drawing.Size(50, 50);
            this.PictureBoxLike.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PictureBoxLike.TabIndex = 23;
            this.PictureBoxLike.TabStop = false;
            this.PictureBoxLike.Click += new System.EventHandler(this.PictureBoxLike_Click);
            // 
            // PictureBoxUsuarioPost
            // 
            this.PictureBoxUsuarioPost.Cursor = System.Windows.Forms.Cursors.Hand;
            this.PictureBoxUsuarioPost.Image = global::Frontend.Properties.Resources.User;
            this.PictureBoxUsuarioPost.Location = new System.Drawing.Point(80, 13);
            this.PictureBoxUsuarioPost.Name = "PictureBoxUsuarioPost";
            this.PictureBoxUsuarioPost.Size = new System.Drawing.Size(50, 50);
            this.PictureBoxUsuarioPost.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PictureBoxUsuarioPost.TabIndex = 22;
            this.PictureBoxUsuarioPost.TabStop = false;
            // 
            // imagen
            // 
            this.imagen.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.imagen.Location = new System.Drawing.Point(80, 101);
            this.imagen.Name = "imagen";
            this.imagen.Size = new System.Drawing.Size(634, 365);
            this.imagen.TabIndex = 21;
            this.imagen.TabStop = false;
            // 
            // txtDescripcionEditar
            // 
            this.txtDescripcionEditar.Location = new System.Drawing.Point(194, 74);
            this.txtDescripcionEditar.Name = "txtDescripcionEditar";
            this.txtDescripcionEditar.Size = new System.Drawing.Size(520, 20);
            this.txtDescripcionEditar.TabIndex = 34;
            // 
            // txtUrlEditar
            // 
            this.txtUrlEditar.Location = new System.Drawing.Point(194, 47);
            this.txtUrlEditar.Name = "txtUrlEditar";
            this.txtUrlEditar.Size = new System.Drawing.Size(520, 20);
            this.txtUrlEditar.TabIndex = 35;
            // 
            // btnConfirmarCambios
            // 
            this.btnConfirmarCambios.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnConfirmarCambios.BackColor = System.Drawing.Color.Transparent;
            this.btnConfirmarCambios.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnConfirmarCambios.Image = global::Frontend.Properties.Resources.editar_removebg_preview;
            this.btnConfirmarCambios.Location = new System.Drawing.Point(355, 494);
            this.btnConfirmarCambios.Name = "btnConfirmarCambios";
            this.btnConfirmarCambios.Size = new System.Drawing.Size(50, 50);
            this.btnConfirmarCambios.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.btnConfirmarCambios.TabIndex = 37;
            this.btnConfirmarCambios.TabStop = false;
            this.btnConfirmarCambios.Click += new System.EventHandler(this.btnConfirmarCambios_Click);
            // 
            // btnSeleccionarImagen
            // 
            this.btnSeleccionarImagen.Location = new System.Drawing.Point(179, 494);
            this.btnSeleccionarImagen.Name = "btnSeleccionarImagen";
            this.btnSeleccionarImagen.Size = new System.Drawing.Size(61, 50);
            this.btnSeleccionarImagen.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.btnSeleccionarImagen.TabIndex = 38;
            this.btnSeleccionarImagen.TabStop = false;
            this.btnSeleccionarImagen.Click += new System.EventHandler(this.btnSeleccionarImagen_Click);
            // 
            // imagenEditar
            // 
            this.imagenEditar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.imagenEditar.Location = new System.Drawing.Point(80, 123);
            this.imagenEditar.Name = "imagenEditar";
            this.imagenEditar.Size = new System.Drawing.Size(634, 365);
            this.imagenEditar.TabIndex = 39;
            this.imagenEditar.TabStop = false;
            // 
            // btnEliminar
            // 
            this.btnEliminar.AutoSize = true;
            this.btnEliminar.Location = new System.Drawing.Point(666, 453);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(43, 13);
            this.btnEliminar.TabIndex = 41;
            this.btnEliminar.Text = "Eliminar";
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
            // 
            // btnReportar
            // 
            this.btnReportar.AutoSize = true;
            this.btnReportar.Location = new System.Drawing.Point(666, 475);
            this.btnReportar.Name = "btnReportar";
            this.btnReportar.Size = new System.Drawing.Size(48, 13);
            this.btnReportar.TabIndex = 41;
            this.btnReportar.Text = "Reportar";
            this.btnReportar.Click += new System.EventHandler(this.btnReportar_Click);
            // 
            // PostControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnReportar);
            this.Controls.Add(this.btnEliminar);
            this.Controls.Add(this.imagenEditar);
            this.Controls.Add(this.btnSeleccionarImagen);
            this.Controls.Add(this.btnConfirmarCambios);
            this.Controls.Add(this.txtUrlEditar);
            this.Controls.Add(this.txtDescripcionEditar);
            this.Controls.Add(this.PictureBoxEditar);
            this.Controls.Add(this.txtUrl);
            this.Controls.Add(this.txtDescripcion);
            this.Controls.Add(this.lblNombre);
            this.Controls.Add(this.PictureBoxOpcionesPost);
            this.Controls.Add(this.PictureBoxCompartir);
            this.Controls.Add(this.PictureBoxComentarios);
            this.Controls.Add(this.PictureBoxLike);
            this.Controls.Add(this.PictureBoxUsuarioPost);
            this.Controls.Add(this.imagen);
            this.Name = "PostControl";
            this.Size = new System.Drawing.Size(787, 578);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBoxEditar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBoxOpcionesPost)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBoxCompartir)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBoxComentarios)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBoxLike)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBoxUsuarioPost)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imagen)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnConfirmarCambios)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSeleccionarImagen)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imagenEditar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblNombre;
        private System.Windows.Forms.PictureBox PictureBoxOpcionesPost;
        private System.Windows.Forms.PictureBox PictureBoxCompartir;
        private System.Windows.Forms.PictureBox PictureBoxComentarios;
        private System.Windows.Forms.PictureBox PictureBoxLike;
        private System.Windows.Forms.PictureBox PictureBoxUsuarioPost;
        private System.Windows.Forms.PictureBox imagen;
        private System.Windows.Forms.Label txtDescripcion;
        private System.Windows.Forms.Label txtUrl;
        private System.Windows.Forms.PictureBox PictureBoxEditar;
        private System.Windows.Forms.TextBox txtDescripcionEditar;
        private System.Windows.Forms.TextBox txtUrlEditar;
        private System.Windows.Forms.PictureBox btnConfirmarCambios;
        private System.Windows.Forms.PictureBox btnSeleccionarImagen;
        private System.Windows.Forms.PictureBox imagenEditar;
        private System.Windows.Forms.Label btnEliminar;
        private System.Windows.Forms.Label btnReportar;
    }
}
