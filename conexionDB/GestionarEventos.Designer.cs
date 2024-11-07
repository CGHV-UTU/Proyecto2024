namespace BackofficeDeAdministracion
{
    partial class GestionarEventos
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.txtTitulo = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtDescripcion = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtUbicacion = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblFechayHora = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblFin = new System.Windows.Forms.Label();
            this.lblInicio = new System.Windows.Forms.Label();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.lblIdPost = new System.Windows.Forms.Label();
            this.txtID = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
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
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(566, 170);
            this.dataGridView1.TabIndex = 66;
            this.dataGridView1.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView1_CellMouseDown);
            // 
            // txtTitulo
            // 
            this.txtTitulo.Cursor = System.Windows.Forms.Cursors.No;
            this.txtTitulo.Font = new System.Drawing.Font("Leelawadee UI", 9.75F);
            this.txtTitulo.Location = new System.Drawing.Point(107, 220);
            this.txtTitulo.MaxLength = 100;
            this.txtTitulo.Multiline = true;
            this.txtTitulo.Name = "txtTitulo";
            this.txtTitulo.ReadOnly = true;
            this.txtTitulo.Size = new System.Drawing.Size(266, 21);
            this.txtTitulo.TabIndex = 68;
            this.txtTitulo.Visible = false;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Leelawadee UI", 9.75F);
            this.label10.ForeColor = System.Drawing.Color.White;
            this.label10.Location = new System.Drawing.Point(38, 223);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(40, 17);
            this.label10.TabIndex = 67;
            this.label10.Text = "Título";
            this.label10.Visible = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(107, 272);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(196, 145);
            this.pictureBox1.TabIndex = 70;
            this.pictureBox1.TabStop = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Leelawadee UI", 9.75F);
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(38, 272);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(51, 17);
            this.label4.TabIndex = 69;
            this.label4.Text = "Imagen";
            this.label4.Visible = false;
            // 
            // txtDescripcion
            // 
            this.txtDescripcion.Cursor = System.Windows.Forms.Cursors.No;
            this.txtDescripcion.Font = new System.Drawing.Font("Leelawadee UI", 9.75F);
            this.txtDescripcion.Location = new System.Drawing.Point(532, 220);
            this.txtDescripcion.MaxLength = 100;
            this.txtDescripcion.Multiline = true;
            this.txtDescripcion.Name = "txtDescripcion";
            this.txtDescripcion.ReadOnly = true;
            this.txtDescripcion.Size = new System.Drawing.Size(266, 65);
            this.txtDescripcion.TabIndex = 72;
            this.txtDescripcion.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Leelawadee UI", 9.75F);
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(433, 223);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(76, 17);
            this.label3.TabIndex = 71;
            this.label3.Text = "Descripción";
            this.label3.Visible = false;
            // 
            // txtUbicacion
            // 
            this.txtUbicacion.Cursor = System.Windows.Forms.Cursors.No;
            this.txtUbicacion.Font = new System.Drawing.Font("Leelawadee UI", 9.75F);
            this.txtUbicacion.Location = new System.Drawing.Point(532, 303);
            this.txtUbicacion.MaxLength = 100;
            this.txtUbicacion.Multiline = true;
            this.txtUbicacion.Name = "txtUbicacion";
            this.txtUbicacion.ReadOnly = true;
            this.txtUbicacion.Size = new System.Drawing.Size(266, 21);
            this.txtUbicacion.TabIndex = 74;
            this.txtUbicacion.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Leelawadee UI", 9.75F);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(433, 306);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 17);
            this.label1.TabIndex = 73;
            this.label1.Text = "Ubicación";
            this.label1.Visible = false;
            // 
            // lblFechayHora
            // 
            this.lblFechayHora.AutoSize = true;
            this.lblFechayHora.Font = new System.Drawing.Font("Leelawadee UI", 9.75F);
            this.lblFechayHora.ForeColor = System.Drawing.Color.White;
            this.lblFechayHora.Location = new System.Drawing.Point(433, 358);
            this.lblFechayHora.Name = "lblFechayHora";
            this.lblFechayHora.Size = new System.Drawing.Size(119, 17);
            this.lblFechayHora.TabIndex = 78;
            this.lblFechayHora.Text = "Fecha y hora Inicio:";
            this.lblFechayHora.Visible = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Leelawadee UI", 9.75F);
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(433, 388);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(105, 17);
            this.label5.TabIndex = 79;
            this.label5.Text = "Fecha y hora Fin:";
            this.label5.Visible = false;
            // 
            // lblFin
            // 
            this.lblFin.AutoSize = true;
            this.lblFin.Font = new System.Drawing.Font("Leelawadee UI", 9.75F);
            this.lblFin.ForeColor = System.Drawing.Color.White;
            this.lblFin.Location = new System.Drawing.Point(569, 388);
            this.lblFin.Name = "lblFin";
            this.lblFin.Size = new System.Drawing.Size(35, 17);
            this.lblFin.TabIndex = 81;
            this.lblFin.Text = "F fin:";
            this.lblFin.Visible = false;
            // 
            // lblInicio
            // 
            this.lblInicio.AutoSize = true;
            this.lblInicio.Font = new System.Drawing.Font("Leelawadee UI", 9.75F);
            this.lblInicio.ForeColor = System.Drawing.Color.White;
            this.lblInicio.Location = new System.Drawing.Point(569, 358);
            this.lblInicio.Name = "lblInicio";
            this.lblInicio.Size = new System.Drawing.Size(35, 17);
            this.lblInicio.TabIndex = 80;
            this.lblInicio.Text = "F fin:";
            this.lblInicio.Visible = false;
            // 
            // btnBuscar
            // 
            this.btnBuscar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(101)))), ((int)(((byte)(64)))), ((int)(((byte)(222)))));
            this.btnBuscar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBuscar.Font = new System.Drawing.Font("Leelawadee UI", 9.75F);
            this.btnBuscar.ForeColor = System.Drawing.Color.White;
            this.btnBuscar.Location = new System.Drawing.Point(16, 95);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(216, 27);
            this.btnBuscar.TabIndex = 85;
            this.btnBuscar.Text = "🔎 Buscar ";
            this.btnBuscar.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnBuscar.UseVisualStyleBackColor = false;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // btnEliminar
            // 
            this.btnEliminar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(101)))), ((int)(((byte)(64)))), ((int)(((byte)(222)))));
            this.btnEliminar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEliminar.Font = new System.Drawing.Font("Leelawadee UI", 9.75F);
            this.btnEliminar.ForeColor = System.Drawing.Color.White;
            this.btnEliminar.Location = new System.Drawing.Point(16, 140);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(216, 27);
            this.btnEliminar.TabIndex = 84;
            this.btnEliminar.Text = "❌Eliminar ";
            this.btnEliminar.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnEliminar.UseVisualStyleBackColor = false;
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
            // 
            // lblIdPost
            // 
            this.lblIdPost.AutoSize = true;
            this.lblIdPost.Font = new System.Drawing.Font("Leelawadee UI", 9.75F);
            this.lblIdPost.ForeColor = System.Drawing.Color.White;
            this.lblIdPost.Location = new System.Drawing.Point(13, 48);
            this.lblIdPost.Name = "lblIdPost";
            this.lblIdPost.Size = new System.Drawing.Size(88, 17);
            this.lblIdPost.TabIndex = 83;
            this.lblIdPost.Text = "ID del Evento:";
            // 
            // txtID
            // 
            this.txtID.Font = new System.Drawing.Font("Leelawadee UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtID.Location = new System.Drawing.Point(111, 48);
            this.txtID.Name = "txtID";
            this.txtID.Size = new System.Drawing.Size(121, 22);
            this.txtID.TabIndex = 82;
            this.txtID.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtID_KeyPress);
            // 
            // GestionarEventos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gray;
            this.ClientSize = new System.Drawing.Size(836, 441);
            this.Controls.Add(this.btnBuscar);
            this.Controls.Add(this.btnEliminar);
            this.Controls.Add(this.lblIdPost);
            this.Controls.Add(this.txtID);
            this.Controls.Add(this.lblFin);
            this.Controls.Add(this.lblInicio);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.lblFechayHora);
            this.Controls.Add(this.txtUbicacion);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtDescripcion);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtTitulo);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.dataGridView1);
            this.Name = "GestionarEventos";
            this.Text = "Editar evento";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TextBox txtTitulo;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtDescripcion;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtUbicacion;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblFechayHora;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblFin;
        private System.Windows.Forms.Label lblInicio;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.Label lblIdPost;
        private System.Windows.Forms.TextBox txtID;
    }
}