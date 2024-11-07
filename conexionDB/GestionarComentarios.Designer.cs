namespace BackofficeDeAdministracion
{
    partial class GestionarComentarios
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
            this.btnBuscar = new System.Windows.Forms.Button();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.lblID = new System.Windows.Forms.Label();
            this.txtID = new System.Windows.Forms.TextBox();
            this.lblNom = new System.Windows.Forms.Label();
            this.lblPost = new System.Windows.Forms.Label();
            this.lblFecha = new System.Windows.Forms.Label();
            this.lblFechayHora = new System.Windows.Forms.Label();
            this.lblIdPost = new System.Windows.Forms.Label();
            this.lblNombreDeCuenta = new System.Windows.Forms.Label();
            this.txtCategorias = new System.Windows.Forms.TextBox();
            this.lblCategorias = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.txtURL = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.lblUrl = new System.Windows.Forms.Label();
            this.lblImagen = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // txtTexto
            // 
            this.txtTexto.Cursor = System.Windows.Forms.Cursors.No;
            this.txtTexto.Font = new System.Drawing.Font("Leelawadee UI", 9.75F);
            this.txtTexto.Location = new System.Drawing.Point(20, 385);
            this.txtTexto.Margin = new System.Windows.Forms.Padding(4);
            this.txtTexto.MaxLength = 100;
            this.txtTexto.Multiline = true;
            this.txtTexto.Name = "txtTexto";
            this.txtTexto.ReadOnly = true;
            this.txtTexto.Size = new System.Drawing.Size(352, 130);
            this.txtTexto.TabIndex = 46;
            this.txtTexto.Visible = false;
            // 
            // lblTexto
            // 
            this.lblTexto.AutoSize = true;
            this.lblTexto.Font = new System.Drawing.Font("Leelawadee UI", 9.75F);
            this.lblTexto.ForeColor = System.Drawing.Color.White;
            this.lblTexto.Location = new System.Drawing.Point(16, 361);
            this.lblTexto.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTexto.Name = "lblTexto";
            this.lblTexto.Size = new System.Drawing.Size(52, 23);
            this.lblTexto.TabIndex = 43;
            this.lblTexto.Text = "Texto";
            this.lblTexto.Visible = false;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(344, 12);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(4);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(755, 209);
            this.dataGridView1.TabIndex = 52;
            this.dataGridView1.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView1_CellMouseDown);
            this.dataGridView1.SelectionChanged += new System.EventHandler(this.dataGridView1_SelectionChanged);
            this.dataGridView1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.dataGridView1_MouseClick);
            // 
            // btnBuscar
            // 
            this.btnBuscar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(101)))), ((int)(((byte)(64)))), ((int)(((byte)(222)))));
            this.btnBuscar.Font = new System.Drawing.Font("Leelawadee UI", 9.75F);
            this.btnBuscar.ForeColor = System.Drawing.Color.White;
            this.btnBuscar.Location = new System.Drawing.Point(35, 101);
            this.btnBuscar.Margin = new System.Windows.Forms.Padding(4);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(288, 33);
            this.btnBuscar.TabIndex = 65;
            this.btnBuscar.Text = "🔎 Buscar ";
            this.btnBuscar.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnBuscar.UseVisualStyleBackColor = false;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // btnEliminar
            // 
            this.btnEliminar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(101)))), ((int)(((byte)(64)))), ((int)(((byte)(222)))));
            this.btnEliminar.Font = new System.Drawing.Font("Leelawadee UI", 9.75F);
            this.btnEliminar.ForeColor = System.Drawing.Color.White;
            this.btnEliminar.Location = new System.Drawing.Point(35, 154);
            this.btnEliminar.Margin = new System.Windows.Forms.Padding(4);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(288, 33);
            this.btnEliminar.TabIndex = 64;
            this.btnEliminar.Text = "❌Eliminar Comentario";
            this.btnEliminar.UseVisualStyleBackColor = false;
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
            // 
            // lblID
            // 
            this.lblID.AutoSize = true;
            this.lblID.Font = new System.Drawing.Font("Leelawadee UI", 9.75F);
            this.lblID.ForeColor = System.Drawing.Color.White;
            this.lblID.Location = new System.Drawing.Point(13, 37);
            this.lblID.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblID.Name = "lblID";
            this.lblID.Size = new System.Drawing.Size(151, 23);
            this.lblID.TabIndex = 63;
            this.lblID.Text = "ID del comentario:";
            // 
            // txtID
            // 
            this.txtID.Font = new System.Drawing.Font("Leelawadee UI", 9.75F);
            this.txtID.Location = new System.Drawing.Point(163, 38);
            this.txtID.Margin = new System.Windows.Forms.Padding(4);
            this.txtID.Name = "txtID";
            this.txtID.Size = new System.Drawing.Size(160, 29);
            this.txtID.TabIndex = 62;
            this.txtID.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtID_KeyPress);
            // 
            // lblNom
            // 
            this.lblNom.AutoSize = true;
            this.lblNom.Font = new System.Drawing.Font("Leelawadee UI", 9.75F);
            this.lblNom.ForeColor = System.Drawing.Color.White;
            this.lblNom.Location = new System.Drawing.Point(16, 255);
            this.lblNom.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblNom.Name = "lblNom";
            this.lblNom.Size = new System.Drawing.Size(159, 23);
            this.lblNom.TabIndex = 90;
            this.lblNom.Text = "Nombre De Cuenta";
            this.lblNom.Visible = false;
            // 
            // lblPost
            // 
            this.lblPost.AutoSize = true;
            this.lblPost.Font = new System.Drawing.Font("Leelawadee UI", 9.75F);
            this.lblPost.ForeColor = System.Drawing.Color.White;
            this.lblPost.Location = new System.Drawing.Point(445, 255);
            this.lblPost.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPost.Name = "lblPost";
            this.lblPost.Size = new System.Drawing.Size(93, 23);
            this.lblPost.TabIndex = 91;
            this.lblPost.Text = "ID de Post:";
            this.lblPost.Visible = false;
            // 
            // lblFecha
            // 
            this.lblFecha.AutoSize = true;
            this.lblFecha.Font = new System.Drawing.Font("Leelawadee UI", 9.75F);
            this.lblFecha.ForeColor = System.Drawing.Color.White;
            this.lblFecha.Location = new System.Drawing.Point(16, 308);
            this.lblFecha.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblFecha.Name = "lblFecha";
            this.lblFecha.Size = new System.Drawing.Size(172, 23);
            this.lblFecha.TabIndex = 92;
            this.lblFecha.Text = "Fecha De Publicación";
            this.lblFecha.Visible = false;
            // 
            // lblFechayHora
            // 
            this.lblFechayHora.AutoSize = true;
            this.lblFechayHora.Font = new System.Drawing.Font("Leelawadee UI", 9.75F);
            this.lblFechayHora.ForeColor = System.Drawing.Color.White;
            this.lblFechayHora.Location = new System.Drawing.Point(224, 308);
            this.lblFechayHora.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblFechayHora.Name = "lblFechayHora";
            this.lblFechayHora.Size = new System.Drawing.Size(54, 23);
            this.lblFechayHora.TabIndex = 95;
            this.lblFechayHora.Text = "Fecha";
            this.lblFechayHora.Visible = false;
            // 
            // lblIdPost
            // 
            this.lblIdPost.AutoSize = true;
            this.lblIdPost.Font = new System.Drawing.Font("Leelawadee UI", 9.75F);
            this.lblIdPost.ForeColor = System.Drawing.Color.White;
            this.lblIdPost.Location = new System.Drawing.Point(544, 255);
            this.lblIdPost.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblIdPost.Name = "lblIdPost";
            this.lblIdPost.Size = new System.Drawing.Size(27, 23);
            this.lblIdPost.TabIndex = 94;
            this.lblIdPost.Text = "ID";
            this.lblIdPost.Visible = false;
            // 
            // lblNombreDeCuenta
            // 
            this.lblNombreDeCuenta.AutoSize = true;
            this.lblNombreDeCuenta.Font = new System.Drawing.Font("Leelawadee UI", 9.75F);
            this.lblNombreDeCuenta.ForeColor = System.Drawing.Color.White;
            this.lblNombreDeCuenta.Location = new System.Drawing.Point(224, 255);
            this.lblNombreDeCuenta.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblNombreDeCuenta.Name = "lblNombreDeCuenta";
            this.lblNombreDeCuenta.Size = new System.Drawing.Size(159, 23);
            this.lblNombreDeCuenta.TabIndex = 93;
            this.lblNombreDeCuenta.Text = "Nombre De Cuenta";
            this.lblNombreDeCuenta.Visible = false;
            // 
            // txtCategorias
            // 
            this.txtCategorias.Cursor = System.Windows.Forms.Cursors.No;
            this.txtCategorias.Font = new System.Drawing.Font("Leelawadee UI", 9.75F);
            this.txtCategorias.Location = new System.Drawing.Point(744, 416);
            this.txtCategorias.Margin = new System.Windows.Forms.Padding(4);
            this.txtCategorias.MaxLength = 100;
            this.txtCategorias.Multiline = true;
            this.txtCategorias.Name = "txtCategorias";
            this.txtCategorias.ReadOnly = true;
            this.txtCategorias.Size = new System.Drawing.Size(353, 34);
            this.txtCategorias.TabIndex = 103;
            this.txtCategorias.Visible = false;
            // 
            // lblCategorias
            // 
            this.lblCategorias.AutoSize = true;
            this.lblCategorias.Font = new System.Drawing.Font("Leelawadee UI", 9.75F);
            this.lblCategorias.ForeColor = System.Drawing.Color.White;
            this.lblCategorias.Location = new System.Drawing.Point(740, 385);
            this.lblCategorias.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCategorias.Name = "lblCategorias";
            this.lblCategorias.Size = new System.Drawing.Size(157, 23);
            this.lblCategorias.TabIndex = 102;
            this.lblCategorias.Text = "Categorías del Post";
            this.lblCategorias.Visible = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(449, 337);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(261, 178);
            this.pictureBox1.TabIndex = 101;
            this.pictureBox1.TabStop = false;
            // 
            // txtURL
            // 
            this.txtURL.Cursor = System.Windows.Forms.Cursors.No;
            this.txtURL.Font = new System.Drawing.Font("Leelawadee UI", 9.75F);
            this.txtURL.Location = new System.Drawing.Point(744, 490);
            this.txtURL.Margin = new System.Windows.Forms.Padding(4);
            this.txtURL.MaxLength = 3227;
            this.txtURL.Multiline = true;
            this.txtURL.Name = "txtURL";
            this.txtURL.ReadOnly = true;
            this.txtURL.Size = new System.Drawing.Size(353, 25);
            this.txtURL.TabIndex = 100;
            this.txtURL.Visible = false;
            // 
            // textBox1
            // 
            this.textBox1.Cursor = System.Windows.Forms.Cursors.No;
            this.textBox1.Font = new System.Drawing.Font("Leelawadee UI", 9.75F);
            this.textBox1.Location = new System.Drawing.Point(744, 279);
            this.textBox1.Margin = new System.Windows.Forms.Padding(4);
            this.textBox1.MaxLength = 100;
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(353, 79);
            this.textBox1.TabIndex = 99;
            this.textBox1.Visible = false;
            // 
            // lblUrl
            // 
            this.lblUrl.AutoSize = true;
            this.lblUrl.Font = new System.Drawing.Font("Leelawadee UI", 9.75F);
            this.lblUrl.ForeColor = System.Drawing.Color.White;
            this.lblUrl.Location = new System.Drawing.Point(740, 466);
            this.lblUrl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblUrl.Name = "lblUrl";
            this.lblUrl.Size = new System.Drawing.Size(176, 23);
            this.lblUrl.TabIndex = 98;
            this.lblUrl.Text = "URL de video del Post";
            this.lblUrl.Visible = false;
            // 
            // lblImagen
            // 
            this.lblImagen.AutoSize = true;
            this.lblImagen.Font = new System.Drawing.Font("Leelawadee UI", 9.75F);
            this.lblImagen.ForeColor = System.Drawing.Color.White;
            this.lblImagen.Location = new System.Drawing.Point(445, 309);
            this.lblImagen.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblImagen.Name = "lblImagen";
            this.lblImagen.Size = new System.Drawing.Size(134, 23);
            this.lblImagen.TabIndex = 97;
            this.lblImagen.Text = "Imagen del Post";
            this.lblImagen.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Leelawadee UI", 9.75F);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(740, 255);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(118, 23);
            this.label1.TabIndex = 96;
            this.label1.Text = "Texto del Post";
            this.label1.Visible = false;
            // 
            // GestionarComentarios
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gray;
            this.ClientSize = new System.Drawing.Size(1115, 543);
            this.Controls.Add(this.txtCategorias);
            this.Controls.Add(this.lblCategorias);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.txtURL);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.lblUrl);
            this.Controls.Add(this.lblImagen);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblFechayHora);
            this.Controls.Add(this.lblIdPost);
            this.Controls.Add(this.lblNombreDeCuenta);
            this.Controls.Add(this.lblFecha);
            this.Controls.Add(this.lblPost);
            this.Controls.Add(this.lblNom);
            this.Controls.Add(this.btnBuscar);
            this.Controls.Add(this.btnEliminar);
            this.Controls.Add(this.lblID);
            this.Controls.Add(this.txtID);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.txtTexto);
            this.Controls.Add(this.lblTexto);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "GestionarComentarios";
            this.Text = "Gestionar Comentarios";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox txtTexto;
        private System.Windows.Forms.Label lblTexto;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.Label lblID;
        private System.Windows.Forms.TextBox txtID;
        private System.Windows.Forms.Label lblNom;
        private System.Windows.Forms.Label lblPost;
        private System.Windows.Forms.Label lblFecha;
        private System.Windows.Forms.Label lblFechayHora;
        private System.Windows.Forms.Label lblIdPost;
        private System.Windows.Forms.Label lblNombreDeCuenta;
        private System.Windows.Forms.TextBox txtCategorias;
        private System.Windows.Forms.Label lblCategorias;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox txtURL;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label lblUrl;
        private System.Windows.Forms.Label lblImagen;
        private System.Windows.Forms.Label label1;
    }
}