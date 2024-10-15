namespace BackofficeDeAdministracion
{
    partial class GestionarPosts
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.txtURL = new System.Windows.Forms.TextBox();
            this.txtTexto = new System.Windows.Forms.TextBox();
            this.lblUrl = new System.Windows.Forms.Label();
            this.lblImagen = new System.Windows.Forms.Label();
            this.lblTexto = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.lblEstadoDeComentarios = new System.Windows.Forms.Label();
            this.btnComentarios = new System.Windows.Forms.Button();
            this.lblLikes = new System.Windows.Forms.Label();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.lblIdPost = new System.Windows.Forms.Label();
            this.txtID = new System.Windows.Forms.TextBox();
            this.txtCategorias = new System.Windows.Forms.TextBox();
            this.lblCategorias = new System.Windows.Forms.Label();
            this.btnModificar = new System.Windows.Forms.Button();
            this.lblLikesDePost = new System.Windows.Forms.Label();
            this.lblNumeroDeLikes = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(468, 191);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(196, 145);
            this.pictureBox1.TabIndex = 39;
            this.pictureBox1.TabStop = false;
            // 
            // txtURL
            // 
            this.txtURL.Cursor = System.Windows.Forms.Cursors.No;
            this.txtURL.Location = new System.Drawing.Point(95, 307);
            this.txtURL.MaxLength = 3227;
            this.txtURL.Multiline = true;
            this.txtURL.Name = "txtURL";
            this.txtURL.ReadOnly = true;
            this.txtURL.Size = new System.Drawing.Size(266, 21);
            this.txtURL.TabIndex = 38;
            // 
            // txtTexto
            // 
            this.txtTexto.Cursor = System.Windows.Forms.Cursors.No;
            this.txtTexto.Location = new System.Drawing.Point(95, 191);
            this.txtTexto.MaxLength = 100;
            this.txtTexto.Multiline = true;
            this.txtTexto.Name = "txtTexto";
            this.txtTexto.ReadOnly = true;
            this.txtTexto.Size = new System.Drawing.Size(266, 65);
            this.txtTexto.TabIndex = 37;
            // 
            // lblUrl
            // 
            this.lblUrl.AutoSize = true;
            this.lblUrl.ForeColor = System.Drawing.Color.White;
            this.lblUrl.Location = new System.Drawing.Point(15, 307);
            this.lblUrl.Name = "lblUrl";
            this.lblUrl.Size = new System.Drawing.Size(73, 13);
            this.lblUrl.TabIndex = 35;
            this.lblUrl.Text = "URL de video";
            // 
            // lblImagen
            // 
            this.lblImagen.AutoSize = true;
            this.lblImagen.ForeColor = System.Drawing.Color.White;
            this.lblImagen.Location = new System.Drawing.Point(389, 194);
            this.lblImagen.Name = "lblImagen";
            this.lblImagen.Size = new System.Drawing.Size(42, 13);
            this.lblImagen.TabIndex = 34;
            this.lblImagen.Text = "Imagen";
            // 
            // lblTexto
            // 
            this.lblTexto.AutoSize = true;
            this.lblTexto.ForeColor = System.Drawing.Color.White;
            this.lblTexto.Location = new System.Drawing.Point(20, 194);
            this.lblTexto.Name = "lblTexto";
            this.lblTexto.Size = new System.Drawing.Size(34, 13);
            this.lblTexto.TabIndex = 33;
            this.lblTexto.Text = "Texto";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(258, 12);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(566, 170);
            this.dataGridView1.StandardTab = true;
            this.dataGridView1.TabIndex = 31;
            this.dataGridView1.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView1_CellMouseDown);
            // 
            // lblEstadoDeComentarios
            // 
            this.lblEstadoDeComentarios.AutoSize = true;
            this.lblEstadoDeComentarios.ForeColor = System.Drawing.Color.White;
            this.lblEstadoDeComentarios.Location = new System.Drawing.Point(20, 354);
            this.lblEstadoDeComentarios.Name = "lblEstadoDeComentarios";
            this.lblEstadoDeComentarios.Size = new System.Drawing.Size(131, 13);
            this.lblEstadoDeComentarios.TabIndex = 41;
            this.lblEstadoDeComentarios.Text = "Estado de los comentarios";
            // 
            // btnComentarios
            // 
            this.btnComentarios.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(101)))), ((int)(((byte)(64)))), ((int)(((byte)(222)))));
            this.btnComentarios.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnComentarios.ForeColor = System.Drawing.Color.White;
            this.btnComentarios.Location = new System.Drawing.Point(160, 349);
            this.btnComentarios.Name = "btnComentarios";
            this.btnComentarios.Size = new System.Drawing.Size(201, 23);
            this.btnComentarios.TabIndex = 42;
            this.btnComentarios.Text = "Desactivar";
            this.btnComentarios.UseVisualStyleBackColor = false;
            this.btnComentarios.Click += new System.EventHandler(this.btnComentarios_Click);
            // 
            // lblLikes
            // 
            this.lblLikes.AutoSize = true;
            this.lblLikes.ForeColor = System.Drawing.Color.White;
            this.lblLikes.Location = new System.Drawing.Point(389, 353);
            this.lblLikes.Name = "lblLikes";
            this.lblLikes.Size = new System.Drawing.Size(32, 13);
            this.lblLikes.TabIndex = 52;
            this.lblLikes.Text = "Likes";
            // 
            // btnBuscar
            // 
            this.btnBuscar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(101)))), ((int)(((byte)(64)))), ((int)(((byte)(222)))));
            this.btnBuscar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBuscar.ForeColor = System.Drawing.Color.White;
            this.btnBuscar.Location = new System.Drawing.Point(12, 79);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(216, 23);
            this.btnBuscar.TabIndex = 61;
            this.btnBuscar.Text = "🔎 Buscar ";
            this.btnBuscar.UseVisualStyleBackColor = false;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // btnEliminar
            // 
            this.btnEliminar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(101)))), ((int)(((byte)(64)))), ((int)(((byte)(222)))));
            this.btnEliminar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEliminar.ForeColor = System.Drawing.Color.White;
            this.btnEliminar.Location = new System.Drawing.Point(12, 124);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(216, 23);
            this.btnEliminar.TabIndex = 60;
            this.btnEliminar.Text = "❌Eliminar ";
            this.btnEliminar.UseVisualStyleBackColor = false;
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
            // 
            // lblIdPost
            // 
            this.lblIdPost.AutoSize = true;
            this.lblIdPost.ForeColor = System.Drawing.Color.White;
            this.lblIdPost.Location = new System.Drawing.Point(12, 40);
            this.lblIdPost.Name = "lblIdPost";
            this.lblIdPost.Size = new System.Drawing.Size(61, 13);
            this.lblIdPost.TabIndex = 59;
            this.lblIdPost.Text = "ID del post:";
            // 
            // txtID
            // 
            this.txtID.Location = new System.Drawing.Point(107, 37);
            this.txtID.Name = "txtID";
            this.txtID.Size = new System.Drawing.Size(121, 20);
            this.txtID.TabIndex = 58;
            this.txtID.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtID_KeyPress);
            // 
            // txtCategorias
            // 
            this.txtCategorias.Cursor = System.Windows.Forms.Cursors.No;
            this.txtCategorias.Location = new System.Drawing.Point(95, 262);
            this.txtCategorias.MaxLength = 100;
            this.txtCategorias.Multiline = true;
            this.txtCategorias.Name = "txtCategorias";
            this.txtCategorias.ReadOnly = true;
            this.txtCategorias.Size = new System.Drawing.Size(266, 28);
            this.txtCategorias.TabIndex = 63;
            // 
            // lblCategorias
            // 
            this.lblCategorias.AutoSize = true;
            this.lblCategorias.ForeColor = System.Drawing.Color.White;
            this.lblCategorias.Location = new System.Drawing.Point(20, 265);
            this.lblCategorias.Name = "lblCategorias";
            this.lblCategorias.Size = new System.Drawing.Size(59, 13);
            this.lblCategorias.TabIndex = 62;
            this.lblCategorias.Text = "Categorías";
            // 
            // btnModificar
            // 
            this.btnModificar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(101)))), ((int)(((byte)(64)))), ((int)(((byte)(222)))));
            this.btnModificar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnModificar.ForeColor = System.Drawing.Color.White;
            this.btnModificar.Location = new System.Drawing.Point(13, 378);
            this.btnModificar.Name = "btnModificar";
            this.btnModificar.Size = new System.Drawing.Size(811, 23);
            this.btnModificar.TabIndex = 65;
            this.btnModificar.Text = "♻️ Modificar";
            this.btnModificar.UseVisualStyleBackColor = false;
            this.btnModificar.Click += new System.EventHandler(this.btnModificar_Click);
            // 
            // lblLikesDePost
            // 
            this.lblLikesDePost.AutoSize = true;
            this.lblLikesDePost.ForeColor = System.Drawing.Color.White;
            this.lblLikesDePost.Location = new System.Drawing.Point(544, 353);
            this.lblLikesDePost.Name = "lblLikesDePost";
            this.lblLikesDePost.Size = new System.Drawing.Size(13, 13);
            this.lblLikesDePost.TabIndex = 67;
            this.lblLikesDePost.Text = "0";
            // 
            // lblNumeroDeLikes
            // 
            this.lblNumeroDeLikes.AutoSize = true;
            this.lblNumeroDeLikes.CausesValidation = false;
            this.lblNumeroDeLikes.ForeColor = System.Drawing.Color.White;
            this.lblNumeroDeLikes.Location = new System.Drawing.Point(452, 353);
            this.lblNumeroDeLikes.Name = "lblNumeroDeLikes";
            this.lblNumeroDeLikes.Size = new System.Drawing.Size(86, 13);
            this.lblNumeroDeLikes.TabIndex = 68;
            this.lblNumeroDeLikes.Text = "Número de likes:";
            // 
            // GestionarPosts
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gray;
            this.ClientSize = new System.Drawing.Size(836, 441);
            this.Controls.Add(this.lblNumeroDeLikes);
            this.Controls.Add(this.lblLikesDePost);
            this.Controls.Add(this.btnModificar);
            this.Controls.Add(this.txtCategorias);
            this.Controls.Add(this.lblCategorias);
            this.Controls.Add(this.btnBuscar);
            this.Controls.Add(this.btnEliminar);
            this.Controls.Add(this.lblIdPost);
            this.Controls.Add(this.txtID);
            this.Controls.Add(this.lblLikes);
            this.Controls.Add(this.btnComentarios);
            this.Controls.Add(this.lblEstadoDeComentarios);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.txtURL);
            this.Controls.Add(this.txtTexto);
            this.Controls.Add(this.lblUrl);
            this.Controls.Add(this.lblImagen);
            this.Controls.Add(this.lblTexto);
            this.Controls.Add(this.dataGridView1);
            this.Name = "GestionarPosts";
            this.Text = "Editar post";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox txtURL;
        private System.Windows.Forms.TextBox txtTexto;
        private System.Windows.Forms.Label lblUrl;
        private System.Windows.Forms.Label lblImagen;
        private System.Windows.Forms.Label lblTexto;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label lblEstadoDeComentarios;
        private System.Windows.Forms.Button btnComentarios;
        private System.Windows.Forms.Label lblLikes;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.Label lblIdPost;
        private System.Windows.Forms.TextBox txtID;
        private System.Windows.Forms.TextBox txtCategorias;
        private System.Windows.Forms.Label lblCategorias;
        private System.Windows.Forms.Button btnModificar;
        private System.Windows.Forms.Label lblLikesDePost;
        private System.Windows.Forms.Label lblNumeroDeLikes;
    }
}