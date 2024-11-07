
namespace BackofficeDeAdministracion
{
    partial class ReportePost
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
            this.lblDescripcionReporte = new System.Windows.Forms.Label();
            this.lblDescripcion = new System.Windows.Forms.Label();
            this.lblNombreDeCuenta = new System.Windows.Forms.Label();
            this.lblNombre = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.lblNom = new System.Windows.Forms.Label();
            this.txtID = new System.Windows.Forms.TextBox();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.lblTipo = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblIdPost = new System.Windows.Forms.Label();
            this.lblCuenta = new System.Windows.Forms.Label();
            this.lblNumeroDeLikes = new System.Windows.Forms.Label();
            this.lblLikesDePost = new System.Windows.Forms.Label();
            this.txtCategorias = new System.Windows.Forms.TextBox();
            this.lblCategorias = new System.Windows.Forms.Label();
            this.lblLikes = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.txtURL = new System.Windows.Forms.TextBox();
            this.txtTexto = new System.Windows.Forms.TextBox();
            this.lblUrl = new System.Windows.Forms.Label();
            this.lblImagen = new System.Windows.Forms.Label();
            this.lblTexto = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // lblDescripcionReporte
            // 
            this.lblDescripcionReporte.AutoSize = true;
            this.lblDescripcionReporte.ForeColor = System.Drawing.Color.White;
            this.lblDescripcionReporte.Location = new System.Drawing.Point(94, 392);
            this.lblDescripcionReporte.Name = "lblDescripcionReporte";
            this.lblDescripcionReporte.Size = new System.Drawing.Size(44, 13);
            this.lblDescripcionReporte.TabIndex = 119;
            this.lblDescripcionReporte.Text = "Nombre";
            this.lblDescripcionReporte.Visible = false;
            // 
            // lblDescripcion
            // 
            this.lblDescripcion.AutoSize = true;
            this.lblDescripcion.ForeColor = System.Drawing.Color.White;
            this.lblDescripcion.Location = new System.Drawing.Point(19, 392);
            this.lblDescripcion.Name = "lblDescripcion";
            this.lblDescripcion.Size = new System.Drawing.Size(66, 13);
            this.lblDescripcion.TabIndex = 118;
            this.lblDescripcion.Text = "Descripcion:";
            this.lblDescripcion.Visible = false;
            // 
            // lblNombreDeCuenta
            // 
            this.lblNombreDeCuenta.AutoSize = true;
            this.lblNombreDeCuenta.ForeColor = System.Drawing.Color.White;
            this.lblNombreDeCuenta.Location = new System.Drawing.Point(129, 392);
            this.lblNombreDeCuenta.Name = "lblNombreDeCuenta";
            this.lblNombreDeCuenta.Size = new System.Drawing.Size(0, 13);
            this.lblNombreDeCuenta.TabIndex = 116;
            this.lblNombreDeCuenta.Visible = false;
            // 
            // lblNombre
            // 
            this.lblNombre.AutoSize = true;
            this.lblNombre.ForeColor = System.Drawing.Color.White;
            this.lblNombre.Location = new System.Drawing.Point(18, 225);
            this.lblNombre.Name = "lblNombre";
            this.lblNombre.Size = new System.Drawing.Size(98, 13);
            this.lblNombre.TabIndex = 115;
            this.lblNombre.Text = "Nombre de cuenta:";
            this.lblNombre.Visible = false;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(258, 12);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(566, 170);
            this.dataGridView1.TabIndex = 113;
            // 
            // btnBuscar
            // 
            this.btnBuscar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(101)))), ((int)(((byte)(64)))), ((int)(((byte)(222)))));
            this.btnBuscar.ForeColor = System.Drawing.Color.White;
            this.btnBuscar.Location = new System.Drawing.Point(15, 86);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(217, 23);
            this.btnBuscar.TabIndex = 112;
            this.btnBuscar.Text = "🔎 Buscar ";
            this.btnBuscar.UseVisualStyleBackColor = false;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // lblNom
            // 
            this.lblNom.AutoSize = true;
            this.lblNom.ForeColor = System.Drawing.Color.White;
            this.lblNom.Location = new System.Drawing.Point(19, 47);
            this.lblNom.Name = "lblNom";
            this.lblNom.Size = new System.Drawing.Size(75, 13);
            this.lblNom.TabIndex = 111;
            this.lblNom.Text = "Id de Reporte:";
            // 
            // txtID
            // 
            this.txtID.Location = new System.Drawing.Point(111, 44);
            this.txtID.Name = "txtID";
            this.txtID.Size = new System.Drawing.Size(121, 20);
            this.txtID.TabIndex = 110;
            // 
            // btnEliminar
            // 
            this.btnEliminar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(101)))), ((int)(((byte)(64)))), ((int)(((byte)(222)))));
            this.btnEliminar.ForeColor = System.Drawing.Color.White;
            this.btnEliminar.Location = new System.Drawing.Point(17, 116);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(215, 23);
            this.btnEliminar.TabIndex = 120;
            this.btnEliminar.Text = "❌Eliminar ";
            this.btnEliminar.UseVisualStyleBackColor = false;
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(19, 329);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 13);
            this.label1.TabIndex = 122;
            this.label1.Text = "Tipo:";
            this.label1.Visible = false;
            // 
            // lblTipo
            // 
            this.lblTipo.AutoSize = true;
            this.lblTipo.ForeColor = System.Drawing.Color.White;
            this.lblTipo.Location = new System.Drawing.Point(94, 329);
            this.lblTipo.Name = "lblTipo";
            this.lblTipo.Size = new System.Drawing.Size(24, 13);
            this.lblTipo.TabIndex = 123;
            this.lblTipo.Text = "tipo";
            this.lblTipo.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(19, 278);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 13);
            this.label2.TabIndex = 124;
            this.label2.Text = "ID Post:";
            this.label2.Visible = false;
            // 
            // lblIdPost
            // 
            this.lblIdPost.AutoSize = true;
            this.lblIdPost.ForeColor = System.Drawing.Color.White;
            this.lblIdPost.Location = new System.Drawing.Point(94, 278);
            this.lblIdPost.Name = "lblIdPost";
            this.lblIdPost.Size = new System.Drawing.Size(15, 13);
            this.lblIdPost.TabIndex = 125;
            this.lblIdPost.Text = "id";
            this.lblIdPost.Visible = false;
            // 
            // lblCuenta
            // 
            this.lblCuenta.AutoSize = true;
            this.lblCuenta.ForeColor = System.Drawing.Color.White;
            this.lblCuenta.Location = new System.Drawing.Point(122, 225);
            this.lblCuenta.Name = "lblCuenta";
            this.lblCuenta.Size = new System.Drawing.Size(44, 13);
            this.lblCuenta.TabIndex = 126;
            this.lblCuenta.Text = "Nombre";
            this.lblCuenta.Visible = false;
            // 
            // lblNumeroDeLikes
            // 
            this.lblNumeroDeLikes.AutoSize = true;
            this.lblNumeroDeLikes.CausesValidation = false;
            this.lblNumeroDeLikes.ForeColor = System.Drawing.Color.White;
            this.lblNumeroDeLikes.Location = new System.Drawing.Point(327, 397);
            this.lblNumeroDeLikes.Name = "lblNumeroDeLikes";
            this.lblNumeroDeLikes.Size = new System.Drawing.Size(86, 13);
            this.lblNumeroDeLikes.TabIndex = 139;
            this.lblNumeroDeLikes.Text = "Número de likes:";
            this.lblNumeroDeLikes.Visible = false;
            // 
            // lblLikesDePost
            // 
            this.lblLikesDePost.AutoSize = true;
            this.lblLikesDePost.ForeColor = System.Drawing.Color.White;
            this.lblLikesDePost.Location = new System.Drawing.Point(405, 397);
            this.lblLikesDePost.Name = "lblLikesDePost";
            this.lblLikesDePost.Size = new System.Drawing.Size(13, 13);
            this.lblLikesDePost.TabIndex = 138;
            this.lblLikesDePost.Text = "0";
            this.lblLikesDePost.Visible = false;
            // 
            // txtCategorias
            // 
            this.txtCategorias.Cursor = System.Windows.Forms.Cursors.No;
            this.txtCategorias.Location = new System.Drawing.Point(330, 293);
            this.txtCategorias.MaxLength = 100;
            this.txtCategorias.Multiline = true;
            this.txtCategorias.Name = "txtCategorias";
            this.txtCategorias.ReadOnly = true;
            this.txtCategorias.Size = new System.Drawing.Size(266, 28);
            this.txtCategorias.TabIndex = 137;
            this.txtCategorias.Visible = false;
            // 
            // lblCategorias
            // 
            this.lblCategorias.AutoSize = true;
            this.lblCategorias.ForeColor = System.Drawing.Color.White;
            this.lblCategorias.Location = new System.Drawing.Point(255, 296);
            this.lblCategorias.Name = "lblCategorias";
            this.lblCategorias.Size = new System.Drawing.Size(59, 13);
            this.lblCategorias.TabIndex = 136;
            this.lblCategorias.Text = "Categorías";
            this.lblCategorias.Visible = false;
            // 
            // lblLikes
            // 
            this.lblLikes.AutoSize = true;
            this.lblLikes.ForeColor = System.Drawing.Color.White;
            this.lblLikes.Location = new System.Drawing.Point(255, 397);
            this.lblLikes.Name = "lblLikes";
            this.lblLikes.Size = new System.Drawing.Size(32, 13);
            this.lblLikes.TabIndex = 135;
            this.lblLikes.Text = "Likes";
            this.lblLikes.Visible = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(628, 260);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(196, 145);
            this.pictureBox1.TabIndex = 132;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Visible = false;
            // 
            // txtURL
            // 
            this.txtURL.Cursor = System.Windows.Forms.Cursors.No;
            this.txtURL.Location = new System.Drawing.Point(330, 338);
            this.txtURL.MaxLength = 3227;
            this.txtURL.Multiline = true;
            this.txtURL.Name = "txtURL";
            this.txtURL.ReadOnly = true;
            this.txtURL.Size = new System.Drawing.Size(266, 21);
            this.txtURL.TabIndex = 131;
            this.txtURL.Visible = false;
            // 
            // txtTexto
            // 
            this.txtTexto.Cursor = System.Windows.Forms.Cursors.No;
            this.txtTexto.Location = new System.Drawing.Point(330, 222);
            this.txtTexto.MaxLength = 100;
            this.txtTexto.Multiline = true;
            this.txtTexto.Name = "txtTexto";
            this.txtTexto.ReadOnly = true;
            this.txtTexto.Size = new System.Drawing.Size(266, 65);
            this.txtTexto.TabIndex = 130;
            this.txtTexto.Visible = false;
            // 
            // lblUrl
            // 
            this.lblUrl.AutoSize = true;
            this.lblUrl.ForeColor = System.Drawing.Color.White;
            this.lblUrl.Location = new System.Drawing.Point(255, 341);
            this.lblUrl.Name = "lblUrl";
            this.lblUrl.Size = new System.Drawing.Size(73, 13);
            this.lblUrl.TabIndex = 129;
            this.lblUrl.Text = "URL de video";
            this.lblUrl.Visible = false;
            // 
            // lblImagen
            // 
            this.lblImagen.AutoSize = true;
            this.lblImagen.ForeColor = System.Drawing.Color.White;
            this.lblImagen.Location = new System.Drawing.Point(625, 225);
            this.lblImagen.Name = "lblImagen";
            this.lblImagen.Size = new System.Drawing.Size(42, 13);
            this.lblImagen.TabIndex = 128;
            this.lblImagen.Text = "Imagen";
            this.lblImagen.Visible = false;
            // 
            // lblTexto
            // 
            this.lblTexto.AutoSize = true;
            this.lblTexto.ForeColor = System.Drawing.Color.White;
            this.lblTexto.Location = new System.Drawing.Point(255, 225);
            this.lblTexto.Name = "lblTexto";
            this.lblTexto.Size = new System.Drawing.Size(34, 13);
            this.lblTexto.TabIndex = 127;
            this.lblTexto.Text = "Texto";
            this.lblTexto.Visible = false;
            // 
            // ReportePost
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gray;
            this.ClientSize = new System.Drawing.Size(836, 441);
            this.Controls.Add(this.lblNumeroDeLikes);
            this.Controls.Add(this.lblLikesDePost);
            this.Controls.Add(this.txtCategorias);
            this.Controls.Add(this.lblCategorias);
            this.Controls.Add(this.lblLikes);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.txtURL);
            this.Controls.Add(this.txtTexto);
            this.Controls.Add(this.lblUrl);
            this.Controls.Add(this.lblImagen);
            this.Controls.Add(this.lblTexto);
            this.Controls.Add(this.lblCuenta);
            this.Controls.Add(this.lblIdPost);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblTipo);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnEliminar);
            this.Controls.Add(this.lblDescripcionReporte);
            this.Controls.Add(this.lblDescripcion);
            this.Controls.Add(this.lblNombreDeCuenta);
            this.Controls.Add(this.lblNombre);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btnBuscar);
            this.Controls.Add(this.lblNom);
            this.Controls.Add(this.txtID);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "ReportePost";
            this.Text = "ReportePost";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblDescripcionReporte;
        private System.Windows.Forms.Label lblDescripcion;
        private System.Windows.Forms.Label lblNombreDeCuenta;
        private System.Windows.Forms.Label lblNombre;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.Label lblNom;
        private System.Windows.Forms.TextBox txtID;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblTipo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblIdPost;
        private System.Windows.Forms.Label lblCuenta;
        private System.Windows.Forms.Label lblNumeroDeLikes;
        private System.Windows.Forms.Label lblLikesDePost;
        private System.Windows.Forms.TextBox txtCategorias;
        private System.Windows.Forms.Label lblCategorias;
        private System.Windows.Forms.Label lblLikes;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox txtURL;
        private System.Windows.Forms.TextBox txtTexto;
        private System.Windows.Forms.Label lblUrl;
        private System.Windows.Forms.Label lblImagen;
        private System.Windows.Forms.Label lblTexto;
    }
}