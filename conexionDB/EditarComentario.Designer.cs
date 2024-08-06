namespace BackofficeDeAdministracion
{
    partial class Editar_comentario
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
            this.lblTexto = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.button9 = new System.Windows.Forms.Button();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.lblID = new System.Windows.Forms.Label();
            this.txtID = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.btnAgregar = new System.Windows.Forms.Button();
            this.btnRestar10Likes = new System.Windows.Forms.Button();
            this.btnRestar5Likes = new System.Windows.Forms.Button();
            this.btnRestar1Like = new System.Windows.Forms.Button();
            this.btnSumar10Likes = new System.Windows.Forms.Button();
            this.btnSumar5Likes = new System.Windows.Forms.Button();
            this.btnSumar1Like = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.btnModificar = new System.Windows.Forms.Button();
            this.lblNumeroDeLikes = new System.Windows.Forms.Label();
            this.lblLikesDeComentario = new System.Windows.Forms.Label();
            this.txtLikesPersonalizados = new System.Windows.Forms.TextBox();
            this.btnRestar = new System.Windows.Forms.Button();
            this.lblNom = new System.Windows.Forms.Label();
            this.lblPost = new System.Windows.Forms.Label();
            this.lblFecha = new System.Windows.Forms.Label();
            this.lblFechayHora = new System.Windows.Forms.Label();
            this.lblIdPost = new System.Windows.Forms.Label();
            this.lblNombreDeCuenta = new System.Windows.Forms.Label();
            this.btnVerPost = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.lblPostCategorias = new System.Windows.Forms.Label();
            this.lblPostUrl = new System.Windows.Forms.Label();
            this.lblPostTexto = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblPostLike = new System.Windows.Forms.Label();
            this.lblCategorias = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblUrl = new System.Windows.Forms.Label();
            this.lblImagen = new System.Windows.Forms.Label();
            this.lblPostText = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // txtTexto
            // 
            this.txtTexto.Location = new System.Drawing.Point(63, 293);
            this.txtTexto.MaxLength = 100;
            this.txtTexto.Multiline = true;
            this.txtTexto.Name = "txtTexto";
            this.txtTexto.Size = new System.Drawing.Size(266, 99);
            this.txtTexto.TabIndex = 46;
            this.txtTexto.Visible = false;
            // 
            // lblTexto
            // 
            this.lblTexto.AutoSize = true;
            this.lblTexto.Location = new System.Drawing.Point(23, 296);
            this.lblTexto.Name = "lblTexto";
            this.lblTexto.Size = new System.Drawing.Size(34, 13);
            this.lblTexto.TabIndex = 43;
            this.lblTexto.Text = "Texto";
            this.lblTexto.Visible = false;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(276, 7);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(450, 150);
            this.dataGridView1.TabIndex = 52;
            // 
            // button9
            // 
            this.button9.BackColor = System.Drawing.Color.Red;
            this.button9.ForeColor = System.Drawing.Color.White;
            this.button9.Location = new System.Drawing.Point(0, 0);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(89, 23);
            this.button9.TabIndex = 53;
            this.button9.Text = " ⬅️ Volver";
            this.button9.UseVisualStyleBackColor = false;
            this.button9.Click += new System.EventHandler(this.btnVolver_Click);
            // 
            // btnBuscar
            // 
            this.btnBuscar.Location = new System.Drawing.Point(26, 63);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(216, 23);
            this.btnBuscar.TabIndex = 65;
            this.btnBuscar.Text = "🔎 Buscar ";
            this.btnBuscar.UseVisualStyleBackColor = true;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // btnEliminar
            // 
            this.btnEliminar.Location = new System.Drawing.Point(26, 106);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(216, 23);
            this.btnEliminar.TabIndex = 64;
            this.btnEliminar.Text = "❌Eliminar ";
            this.btnEliminar.UseVisualStyleBackColor = true;
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
            // 
            // lblID
            // 
            this.lblID.AutoSize = true;
            this.lblID.Location = new System.Drawing.Point(23, 34);
            this.lblID.Name = "lblID";
            this.lblID.Size = new System.Drawing.Size(93, 13);
            this.lblID.TabIndex = 63;
            this.lblID.Text = "ID del comentario:";
            // 
            // txtID
            // 
            this.txtID.Location = new System.Drawing.Point(121, 31);
            this.txtID.Name = "txtID";
            this.txtID.Size = new System.Drawing.Size(121, 20);
            this.txtID.TabIndex = 62;
            this.txtID.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtID_KeyPress);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.CausesValidation = false;
            this.label9.Location = new System.Drawing.Point(496, 308);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(76, 13);
            this.label9.TabIndex = 83;
            this.label9.Text = "Personalizado:";
            // 
            // btnAgregar
            // 
            this.btnAgregar.Location = new System.Drawing.Point(486, 336);
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Size = new System.Drawing.Size(102, 23);
            this.btnAgregar.TabIndex = 82;
            this.btnAgregar.Text = "♥ Agregar";
            this.btnAgregar.UseVisualStyleBackColor = true;
            this.btnAgregar.Click += new System.EventHandler(this.btnAgregarLikes_Click);
            // 
            // btnRestar10Likes
            // 
            this.btnRestar10Likes.Location = new System.Drawing.Point(594, 273);
            this.btnRestar10Likes.Name = "btnRestar10Likes";
            this.btnRestar10Likes.Size = new System.Drawing.Size(102, 23);
            this.btnRestar10Likes.TabIndex = 81;
            this.btnRestar10Likes.Text = "♥ -10";
            this.btnRestar10Likes.UseVisualStyleBackColor = true;
            this.btnRestar10Likes.Click += new System.EventHandler(this.btnRestar10Likes_Click);
            // 
            // btnRestar5Likes
            // 
            this.btnRestar5Likes.Location = new System.Drawing.Point(594, 244);
            this.btnRestar5Likes.Name = "btnRestar5Likes";
            this.btnRestar5Likes.Size = new System.Drawing.Size(102, 23);
            this.btnRestar5Likes.TabIndex = 80;
            this.btnRestar5Likes.Text = "♥ -5";
            this.btnRestar5Likes.UseVisualStyleBackColor = true;
            this.btnRestar5Likes.Click += new System.EventHandler(this.btnRestar5Likes_Click);
            // 
            // btnRestar1Like
            // 
            this.btnRestar1Like.Location = new System.Drawing.Point(594, 215);
            this.btnRestar1Like.Name = "btnRestar1Like";
            this.btnRestar1Like.Size = new System.Drawing.Size(102, 23);
            this.btnRestar1Like.TabIndex = 79;
            this.btnRestar1Like.Text = "♥ -1";
            this.btnRestar1Like.UseVisualStyleBackColor = true;
            this.btnRestar1Like.Click += new System.EventHandler(this.btnRestar1Like_Click);
            // 
            // btnSumar10Likes
            // 
            this.btnSumar10Likes.Location = new System.Drawing.Point(486, 273);
            this.btnSumar10Likes.Name = "btnSumar10Likes";
            this.btnSumar10Likes.Size = new System.Drawing.Size(102, 23);
            this.btnSumar10Likes.TabIndex = 78;
            this.btnSumar10Likes.Text = "♥ +10";
            this.btnSumar10Likes.UseVisualStyleBackColor = true;
            this.btnSumar10Likes.Click += new System.EventHandler(this.btnSumar10Likes_Click);
            // 
            // btnSumar5Likes
            // 
            this.btnSumar5Likes.Location = new System.Drawing.Point(486, 244);
            this.btnSumar5Likes.Name = "btnSumar5Likes";
            this.btnSumar5Likes.Size = new System.Drawing.Size(102, 23);
            this.btnSumar5Likes.TabIndex = 77;
            this.btnSumar5Likes.Text = "♥ +5";
            this.btnSumar5Likes.UseVisualStyleBackColor = true;
            this.btnSumar5Likes.Click += new System.EventHandler(this.btnSumar5Likes_Click);
            // 
            // btnSumar1Like
            // 
            this.btnSumar1Like.Location = new System.Drawing.Point(486, 215);
            this.btnSumar1Like.Name = "btnSumar1Like";
            this.btnSumar1Like.Size = new System.Drawing.Size(102, 23);
            this.btnSumar1Like.TabIndex = 76;
            this.btnSumar1Like.Text = "♥ +1";
            this.btnSumar1Like.UseVisualStyleBackColor = true;
            this.btnSumar1Like.Click += new System.EventHandler(this.btnSumar1Like_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(400, 220);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 13);
            this.label1.TabIndex = 75;
            this.label1.Text = "Gestionar likes";
            // 
            // btnGuardar
            // 
            this.btnGuardar.ForeColor = System.Drawing.Color.Black;
            this.btnGuardar.Location = new System.Drawing.Point(12, 434);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(714, 22);
            this.btnGuardar.TabIndex = 86;
            this.btnGuardar.Text = "💾 Guardar y Salir";
            this.btnGuardar.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnGuardar.UseVisualStyleBackColor = true;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // btnModificar
            // 
            this.btnModificar.Location = new System.Drawing.Point(12, 405);
            this.btnModificar.Name = "btnModificar";
            this.btnModificar.Size = new System.Drawing.Size(714, 23);
            this.btnModificar.TabIndex = 85;
            this.btnModificar.Text = "♻️ Modificar";
            this.btnModificar.UseVisualStyleBackColor = true;
            this.btnModificar.Click += new System.EventHandler(this.btnModificarClick);
            // 
            // lblNumeroDeLikes
            // 
            this.lblNumeroDeLikes.AutoSize = true;
            this.lblNumeroDeLikes.CausesValidation = false;
            this.lblNumeroDeLikes.Location = new System.Drawing.Point(400, 171);
            this.lblNumeroDeLikes.Name = "lblNumeroDeLikes";
            this.lblNumeroDeLikes.Size = new System.Drawing.Size(86, 13);
            this.lblNumeroDeLikes.TabIndex = 88;
            this.lblNumeroDeLikes.Text = "Número de likes:";
            // 
            // lblLikesDeComentario
            // 
            this.lblLikesDeComentario.AutoSize = true;
            this.lblLikesDeComentario.Location = new System.Drawing.Point(492, 171);
            this.lblLikesDeComentario.Name = "lblLikesDeComentario";
            this.lblLikesDeComentario.Size = new System.Drawing.Size(13, 13);
            this.lblLikesDeComentario.TabIndex = 87;
            this.lblLikesDeComentario.Text = "0";
            // 
            // txtLikesPersonalizados
            // 
            this.txtLikesPersonalizados.Location = new System.Drawing.Point(594, 305);
            this.txtLikesPersonalizados.Name = "txtLikesPersonalizados";
            this.txtLikesPersonalizados.Size = new System.Drawing.Size(102, 20);
            this.txtLikesPersonalizados.TabIndex = 84;
            this.txtLikesPersonalizados.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtLikesPersonalizados_KeyPress);
            // 
            // btnRestar
            // 
            this.btnRestar.Location = new System.Drawing.Point(594, 336);
            this.btnRestar.Name = "btnRestar";
            this.btnRestar.Size = new System.Drawing.Size(102, 23);
            this.btnRestar.TabIndex = 89;
            this.btnRestar.Text = "♥ Restar";
            this.btnRestar.UseVisualStyleBackColor = true;
            this.btnRestar.Click += new System.EventHandler(this.btnRestarLikes_Click);
            // 
            // lblNom
            // 
            this.lblNom.AutoSize = true;
            this.lblNom.Location = new System.Drawing.Point(22, 162);
            this.lblNom.Name = "lblNom";
            this.lblNom.Size = new System.Drawing.Size(98, 13);
            this.lblNom.TabIndex = 90;
            this.lblNom.Text = "Nombre De Cuenta";
            this.lblNom.Visible = false;
            // 
            // lblPost
            // 
            this.lblPost.AutoSize = true;
            this.lblPost.Location = new System.Drawing.Point(25, 204);
            this.lblPost.Name = "lblPost";
            this.lblPost.Size = new System.Drawing.Size(57, 13);
            this.lblPost.TabIndex = 91;
            this.lblPost.Text = "ID de Post";
            this.lblPost.Visible = false;
            // 
            // lblFecha
            // 
            this.lblFecha.AutoSize = true;
            this.lblFecha.Location = new System.Drawing.Point(25, 248);
            this.lblFecha.Name = "lblFecha";
            this.lblFecha.Size = new System.Drawing.Size(112, 13);
            this.lblFecha.TabIndex = 92;
            this.lblFecha.Text = "Fecha De Publicación";
            this.lblFecha.Visible = false;
            // 
            // lblFechayHora
            // 
            this.lblFechayHora.AutoSize = true;
            this.lblFechayHora.Location = new System.Drawing.Point(149, 248);
            this.lblFechayHora.Name = "lblFechayHora";
            this.lblFechayHora.Size = new System.Drawing.Size(37, 13);
            this.lblFechayHora.TabIndex = 95;
            this.lblFechayHora.Text = "Fecha";
            this.lblFechayHora.Visible = false;
            // 
            // lblIdPost
            // 
            this.lblIdPost.AutoSize = true;
            this.lblIdPost.Location = new System.Drawing.Point(149, 204);
            this.lblIdPost.Name = "lblIdPost";
            this.lblIdPost.Size = new System.Drawing.Size(18, 13);
            this.lblIdPost.TabIndex = 94;
            this.lblIdPost.Text = "ID";
            this.lblIdPost.Visible = false;
            // 
            // lblNombreDeCuenta
            // 
            this.lblNombreDeCuenta.AutoSize = true;
            this.lblNombreDeCuenta.Location = new System.Drawing.Point(146, 162);
            this.lblNombreDeCuenta.Name = "lblNombreDeCuenta";
            this.lblNombreDeCuenta.Size = new System.Drawing.Size(98, 13);
            this.lblNombreDeCuenta.TabIndex = 93;
            this.lblNombreDeCuenta.Text = "Nombre De Cuenta";
            this.lblNombreDeCuenta.Visible = false;
            // 
            // btnVerPost
            // 
            this.btnVerPost.Location = new System.Drawing.Point(216, 199);
            this.btnVerPost.Name = "btnVerPost";
            this.btnVerPost.Size = new System.Drawing.Size(75, 23);
            this.btnVerPost.TabIndex = 96;
            this.btnVerPost.Text = "Ver";
            this.btnVerPost.UseVisualStyleBackColor = true;
            this.btnVerPost.Visible = false;
            this.btnVerPost.Click += new System.EventHandler(this.btnVerPost_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.lblPostCategorias);
            this.panel1.Controls.Add(this.lblPostUrl);
            this.panel1.Controls.Add(this.lblPostTexto);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.lblPostLike);
            this.panel1.Controls.Add(this.lblCategorias);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.lblUrl);
            this.panel1.Controls.Add(this.lblImagen);
            this.panel1.Controls.Add(this.lblPostText);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Location = new System.Drawing.Point(2, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(478, 287);
            this.panel1.TabIndex = 97;
            this.panel1.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(42, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(168, 13);
            this.label2.TabIndex = 102;
            this.label2.Text = "Post donde se creó el comentario:";
            // 
            // lblPostCategorias
            // 
            this.lblPostCategorias.AutoSize = true;
            this.lblPostCategorias.Location = new System.Drawing.Point(116, 132);
            this.lblPostCategorias.Name = "lblPostCategorias";
            this.lblPostCategorias.Size = new System.Drawing.Size(59, 13);
            this.lblPostCategorias.TabIndex = 101;
            this.lblPostCategorias.Text = "Categorías";
            // 
            // lblPostUrl
            // 
            this.lblPostUrl.AutoSize = true;
            this.lblPostUrl.Location = new System.Drawing.Point(118, 191);
            this.lblPostUrl.Name = "lblPostUrl";
            this.lblPostUrl.Size = new System.Drawing.Size(75, 13);
            this.lblPostUrl.TabIndex = 100;
            this.lblPostUrl.Text = "URL del video";
            // 
            // lblPostTexto
            // 
            this.lblPostTexto.AutoSize = true;
            this.lblPostTexto.Location = new System.Drawing.Point(116, 89);
            this.lblPostTexto.Name = "lblPostTexto";
            this.lblPostTexto.Size = new System.Drawing.Size(34, 13);
            this.lblPostTexto.TabIndex = 99;
            this.lblPostTexto.Text = "Texto";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.CausesValidation = false;
            this.label3.Location = new System.Drawing.Point(26, 245);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(86, 13);
            this.label3.TabIndex = 91;
            this.label3.Text = "Número de likes:";
            // 
            // lblPostLike
            // 
            this.lblPostLike.AutoSize = true;
            this.lblPostLike.Location = new System.Drawing.Point(123, 245);
            this.lblPostLike.Name = "lblPostLike";
            this.lblPostLike.Size = new System.Drawing.Size(13, 13);
            this.lblPostLike.TabIndex = 90;
            this.lblPostLike.Text = "0";
            // 
            // lblCategorias
            // 
            this.lblCategorias.AutoSize = true;
            this.lblCategorias.Location = new System.Drawing.Point(27, 132);
            this.lblCategorias.Name = "lblCategorias";
            this.lblCategorias.Size = new System.Drawing.Size(59, 13);
            this.lblCategorias.TabIndex = 88;
            this.lblCategorias.Text = "Categorías";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(295, 84);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(160, 120);
            this.pictureBox1.TabIndex = 81;
            this.pictureBox1.TabStop = false;
            // 
            // lblUrl
            // 
            this.lblUrl.AutoSize = true;
            this.lblUrl.Location = new System.Drawing.Point(22, 191);
            this.lblUrl.Name = "lblUrl";
            this.lblUrl.Size = new System.Drawing.Size(75, 13);
            this.lblUrl.TabIndex = 78;
            this.lblUrl.Text = "URL del video";
            // 
            // lblImagen
            // 
            this.lblImagen.AutoSize = true;
            this.lblImagen.Location = new System.Drawing.Point(235, 90);
            this.lblImagen.Name = "lblImagen";
            this.lblImagen.Size = new System.Drawing.Size(42, 13);
            this.lblImagen.TabIndex = 77;
            this.lblImagen.Text = "Imagen";
            // 
            // lblPostText
            // 
            this.lblPostText.AutoSize = true;
            this.lblPostText.Location = new System.Drawing.Point(27, 89);
            this.lblPostText.Name = "lblPostText";
            this.lblPostText.Size = new System.Drawing.Size(34, 13);
            this.lblPostText.TabIndex = 76;
            this.lblPostText.Text = "Texto";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(380, 248);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Volver";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.btnVolverPost);
            // 
            // Editar_comentario
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(739, 461);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnVerPost);
            this.Controls.Add(this.lblFechayHora);
            this.Controls.Add(this.lblIdPost);
            this.Controls.Add(this.lblNombreDeCuenta);
            this.Controls.Add(this.lblFecha);
            this.Controls.Add(this.lblPost);
            this.Controls.Add(this.lblNom);
            this.Controls.Add(this.btnRestar);
            this.Controls.Add(this.lblNumeroDeLikes);
            this.Controls.Add(this.lblLikesDeComentario);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.btnModificar);
            this.Controls.Add(this.txtLikesPersonalizados);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.btnAgregar);
            this.Controls.Add(this.btnRestar10Likes);
            this.Controls.Add(this.btnRestar5Likes);
            this.Controls.Add(this.btnRestar1Like);
            this.Controls.Add(this.btnSumar10Likes);
            this.Controls.Add(this.btnSumar5Likes);
            this.Controls.Add(this.btnSumar1Like);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnBuscar);
            this.Controls.Add(this.btnEliminar);
            this.Controls.Add(this.lblID);
            this.Controls.Add(this.txtID);
            this.Controls.Add(this.button9);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.txtTexto);
            this.Controls.Add(this.lblTexto);
            this.Name = "Editar_comentario";
            this.Text = "Editar comentario";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox txtTexto;
        private System.Windows.Forms.Label lblTexto;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.Label lblID;
        private System.Windows.Forms.TextBox txtID;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btnAgregar;
        private System.Windows.Forms.Button btnRestar10Likes;
        private System.Windows.Forms.Button btnRestar5Likes;
        private System.Windows.Forms.Button btnRestar1Like;
        private System.Windows.Forms.Button btnSumar10Likes;
        private System.Windows.Forms.Button btnSumar5Likes;
        private System.Windows.Forms.Button btnSumar1Like;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Button btnModificar;
        private System.Windows.Forms.Label lblNumeroDeLikes;
        private System.Windows.Forms.Label lblLikesDeComentario;
        private System.Windows.Forms.TextBox txtLikesPersonalizados;
        private System.Windows.Forms.Button btnRestar;
        private System.Windows.Forms.Label lblNom;
        private System.Windows.Forms.Label lblPost;
        private System.Windows.Forms.Label lblFecha;
        private System.Windows.Forms.Label lblFechayHora;
        private System.Windows.Forms.Label lblIdPost;
        private System.Windows.Forms.Label lblNombreDeCuenta;
        private System.Windows.Forms.Button btnVerPost;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblPostLike;
        private System.Windows.Forms.Label lblCategorias;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lblUrl;
        private System.Windows.Forms.Label lblImagen;
        private System.Windows.Forms.Label lblPostText;
        private System.Windows.Forms.Label lblPostCategorias;
        private System.Windows.Forms.Label lblPostUrl;
        private System.Windows.Forms.Label lblPostTexto;
        private System.Windows.Forms.Label label2;
    }
}