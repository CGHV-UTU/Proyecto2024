﻿namespace BackofficeDeAdministracion
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
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // txtTexto
            // 
            this.txtTexto.Location = new System.Drawing.Point(71, 179);
            this.txtTexto.MaxLength = 100;
            this.txtTexto.Multiline = true;
            this.txtTexto.Name = "txtTexto";
            this.txtTexto.Size = new System.Drawing.Size(266, 99);
            this.txtTexto.TabIndex = 46;
            // 
            // lblTexto
            // 
            this.lblTexto.AutoSize = true;
            this.lblTexto.Location = new System.Drawing.Point(31, 182);
            this.lblTexto.Name = "lblTexto";
            this.lblTexto.Size = new System.Drawing.Size(34, 13);
            this.lblTexto.TabIndex = 43;
            this.lblTexto.Text = "Texto";
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(276, 7);
            this.dataGridView1.Name = "dataGridView1";
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
            this.btnBuscar.Location = new System.Drawing.Point(28, 55);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(216, 23);
            this.btnBuscar.TabIndex = 65;
            this.btnBuscar.Text = "🔎 Buscar ";
            this.btnBuscar.UseVisualStyleBackColor = true;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // btnEliminar
            // 
            this.btnEliminar.Location = new System.Drawing.Point(28, 98);
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
            this.lblID.Location = new System.Drawing.Point(25, 26);
            this.lblID.Name = "lblID";
            this.lblID.Size = new System.Drawing.Size(93, 13);
            this.lblID.TabIndex = 63;
            this.lblID.Text = "ID del comentario:";
            // 
            // txtID
            // 
            this.txtID.Location = new System.Drawing.Point(123, 23);
            this.txtID.Name = "txtID";
            this.txtID.Size = new System.Drawing.Size(121, 20);
            this.txtID.TabIndex = 62;
            this.txtID.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtID_KeyPress);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.CausesValidation = false;
            this.label9.Location = new System.Drawing.Point(455, 265);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(76, 13);
            this.label9.TabIndex = 83;
            this.label9.Text = "Personalizado:";
            // 
            // btnAgregar
            // 
            this.btnAgregar.Location = new System.Drawing.Point(445, 293);
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Size = new System.Drawing.Size(102, 23);
            this.btnAgregar.TabIndex = 82;
            this.btnAgregar.Text = "♥ Agregar";
            this.btnAgregar.UseVisualStyleBackColor = true;
            this.btnAgregar.Click += new System.EventHandler(this.btnAgregarLikes_Click);
            // 
            // btnRestar10Likes
            // 
            this.btnRestar10Likes.Location = new System.Drawing.Point(553, 230);
            this.btnRestar10Likes.Name = "btnRestar10Likes";
            this.btnRestar10Likes.Size = new System.Drawing.Size(102, 23);
            this.btnRestar10Likes.TabIndex = 81;
            this.btnRestar10Likes.Text = "♥ -10";
            this.btnRestar10Likes.UseVisualStyleBackColor = true;
            this.btnRestar10Likes.Click += new System.EventHandler(this.btnRestar10Likes_Click);
            // 
            // btnRestar5Likes
            // 
            this.btnRestar5Likes.Location = new System.Drawing.Point(553, 201);
            this.btnRestar5Likes.Name = "btnRestar5Likes";
            this.btnRestar5Likes.Size = new System.Drawing.Size(102, 23);
            this.btnRestar5Likes.TabIndex = 80;
            this.btnRestar5Likes.Text = "♥ -5";
            this.btnRestar5Likes.UseVisualStyleBackColor = true;
            this.btnRestar5Likes.Click += new System.EventHandler(this.btnRestar5Likes_Click);
            // 
            // btnRestar1Like
            // 
            this.btnRestar1Like.Location = new System.Drawing.Point(553, 172);
            this.btnRestar1Like.Name = "btnRestar1Like";
            this.btnRestar1Like.Size = new System.Drawing.Size(102, 23);
            this.btnRestar1Like.TabIndex = 79;
            this.btnRestar1Like.Text = "♥ -1";
            this.btnRestar1Like.UseVisualStyleBackColor = true;
            this.btnRestar1Like.Click += new System.EventHandler(this.btnRestar1Like_Click);
            // 
            // btnSumar10Likes
            // 
            this.btnSumar10Likes.Location = new System.Drawing.Point(445, 230);
            this.btnSumar10Likes.Name = "btnSumar10Likes";
            this.btnSumar10Likes.Size = new System.Drawing.Size(102, 23);
            this.btnSumar10Likes.TabIndex = 78;
            this.btnSumar10Likes.Text = "♥ +10";
            this.btnSumar10Likes.UseVisualStyleBackColor = true;
            this.btnSumar10Likes.Click += new System.EventHandler(this.btnSumar10Likes_Click);
            // 
            // btnSumar5Likes
            // 
            this.btnSumar5Likes.Location = new System.Drawing.Point(445, 201);
            this.btnSumar5Likes.Name = "btnSumar5Likes";
            this.btnSumar5Likes.Size = new System.Drawing.Size(102, 23);
            this.btnSumar5Likes.TabIndex = 77;
            this.btnSumar5Likes.Text = "♥ +5";
            this.btnSumar5Likes.UseVisualStyleBackColor = true;
            this.btnSumar5Likes.Click += new System.EventHandler(this.btnSumar5Likes_Click);
            // 
            // btnSumar1Like
            // 
            this.btnSumar1Like.Location = new System.Drawing.Point(445, 172);
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
            this.label1.Location = new System.Drawing.Point(370, 177);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 13);
            this.label1.TabIndex = 75;
            this.label1.Text = "Likes";
            // 
            // btnGuardar
            // 
            this.btnGuardar.ForeColor = System.Drawing.Color.Black;
            this.btnGuardar.Location = new System.Drawing.Point(12, 392);
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
            this.btnModificar.Location = new System.Drawing.Point(12, 363);
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
            this.lblNumeroDeLikes.Location = new System.Drawing.Point(363, 331);
            this.lblNumeroDeLikes.Name = "lblNumeroDeLikes";
            this.lblNumeroDeLikes.Size = new System.Drawing.Size(86, 13);
            this.lblNumeroDeLikes.TabIndex = 88;
            this.lblNumeroDeLikes.Text = "Número de likes:";
            // 
            // lblLikesDeComentario
            // 
            this.lblLikesDeComentario.AutoSize = true;
            this.lblLikesDeComentario.Location = new System.Drawing.Point(455, 331);
            this.lblLikesDeComentario.Name = "lblLikesDeComentario";
            this.lblLikesDeComentario.Size = new System.Drawing.Size(13, 13);
            this.lblLikesDeComentario.TabIndex = 87;
            this.lblLikesDeComentario.Text = "0";
            // 
            // txtLikesPersonalizados
            // 
            this.txtLikesPersonalizados.Location = new System.Drawing.Point(553, 262);
            this.txtLikesPersonalizados.Name = "txtLikesPersonalizados";
            this.txtLikesPersonalizados.Size = new System.Drawing.Size(102, 20);
            this.txtLikesPersonalizados.TabIndex = 84;
            this.txtLikesPersonalizados.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtLikesPersonalizados_KeyPress);
            // 
            // btnRestar
            // 
            this.btnRestar.Location = new System.Drawing.Point(553, 293);
            this.btnRestar.Name = "btnRestar";
            this.btnRestar.Size = new System.Drawing.Size(102, 23);
            this.btnRestar.TabIndex = 89;
            this.btnRestar.Text = "♥ Restar";
            this.btnRestar.UseVisualStyleBackColor = true;
            this.btnRestar.Click += new System.EventHandler(this.btnRestarLikes_Click);
            // 
            // Editar_comentario
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(739, 421);
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
    }
}