
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
            this.PictureBoxOpcionesPost = new System.Windows.Forms.PictureBox();
            this.PictureBoxCompartir = new System.Windows.Forms.PictureBox();
            this.PictureBoxComentarios = new System.Windows.Forms.PictureBox();
            this.PictureBoxLike = new System.Windows.Forms.PictureBox();
            this.PictureBoxUsuarioPost = new System.Windows.Forms.PictureBox();
            this.imagen = new System.Windows.Forms.PictureBox();
            this.txtDescripcion = new System.Windows.Forms.Label();
            this.txtUrl = new System.Windows.Forms.Label();
            this.PictureBoxReportar = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBoxOpcionesPost)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBoxCompartir)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBoxComentarios)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBoxLike)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBoxUsuarioPost)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imagen)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBoxReportar)).BeginInit();
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
            // PictureBoxReportar
            // 
            this.PictureBoxReportar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.PictureBoxReportar.BackColor = System.Drawing.Color.Transparent;
            this.PictureBoxReportar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.PictureBoxReportar.Image = global::Frontend.Properties.Resources.reportar;
            this.PictureBoxReportar.Location = new System.Drawing.Point(720, 494);
            this.PictureBoxReportar.Name = "PictureBoxReportar";
            this.PictureBoxReportar.Size = new System.Drawing.Size(50, 50);
            this.PictureBoxReportar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PictureBoxReportar.TabIndex = 33;
            this.PictureBoxReportar.TabStop = false;
            this.PictureBoxReportar.Click += new System.EventHandler(this.PictureBoxReportar_Click_1);
            // 
            // PostControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.PictureBoxReportar);
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
            ((System.ComponentModel.ISupportInitialize)(this.PictureBoxOpcionesPost)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBoxCompartir)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBoxComentarios)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBoxLike)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBoxUsuarioPost)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imagen)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBoxReportar)).EndInit();
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
        private System.Windows.Forms.PictureBox PictureBoxReportar;
    }
}
