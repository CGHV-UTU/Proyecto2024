
namespace BackofficeDeAdministracion
{
    partial class ReporteEvento
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
            this.btnBuscar = new System.Windows.Forms.Button();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.lblIdPost = new System.Windows.Forms.Label();
            this.txtID = new System.Windows.Forms.TextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.lblFin = new System.Windows.Forms.Label();
            this.lblInicio = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblFechayHora = new System.Windows.Forms.Label();
            this.txtUbicacion = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtDescripcion = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtTitulo = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblTipo = new System.Windows.Forms.Label();
            this.lblT = new System.Windows.Forms.Label();
            this.lblDescripcion = new System.Windows.Forms.Label();
            this.lblNombreDeCuenta = new System.Windows.Forms.Label();
            this.lblNombre = new System.Windows.Forms.Label();
            this.txtDescripcionReporte = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
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
            this.btnBuscar.TabIndex = 90;
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
            this.btnEliminar.TabIndex = 89;
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
            this.lblIdPost.Size = new System.Drawing.Size(96, 17);
            this.lblIdPost.TabIndex = 88;
            this.lblIdPost.Text = "ID del Reporte:";
            // 
            // txtID
            // 
            this.txtID.Font = new System.Drawing.Font("Leelawadee UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtID.Location = new System.Drawing.Point(115, 48);
            this.txtID.Name = "txtID";
            this.txtID.Size = new System.Drawing.Size(121, 22);
            this.txtID.TabIndex = 87;
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
            this.dataGridView1.TabIndex = 86;
            // 
            // lblFin
            // 
            this.lblFin.AutoSize = true;
            this.lblFin.Font = new System.Drawing.Font("Leelawadee UI", 9.75F);
            this.lblFin.ForeColor = System.Drawing.Color.White;
            this.lblFin.Location = new System.Drawing.Point(613, 408);
            this.lblFin.Name = "lblFin";
            this.lblFin.Size = new System.Drawing.Size(35, 17);
            this.lblFin.TabIndex = 102;
            this.lblFin.Text = "F fin:";
            this.lblFin.Visible = false;
            // 
            // lblInicio
            // 
            this.lblInicio.AutoSize = true;
            this.lblInicio.Font = new System.Drawing.Font("Leelawadee UI", 9.75F);
            this.lblInicio.ForeColor = System.Drawing.Color.White;
            this.lblInicio.Location = new System.Drawing.Point(613, 378);
            this.lblInicio.Name = "lblInicio";
            this.lblInicio.Size = new System.Drawing.Size(35, 17);
            this.lblInicio.TabIndex = 101;
            this.lblInicio.Text = "F fin:";
            this.lblInicio.Visible = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Leelawadee UI", 9.75F);
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(477, 408);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(105, 17);
            this.label5.TabIndex = 100;
            this.label5.Text = "Fecha y hora Fin:";
            this.label5.Visible = false;
            // 
            // lblFechayHora
            // 
            this.lblFechayHora.AutoSize = true;
            this.lblFechayHora.Font = new System.Drawing.Font("Leelawadee UI", 9.75F);
            this.lblFechayHora.ForeColor = System.Drawing.Color.White;
            this.lblFechayHora.Location = new System.Drawing.Point(477, 378);
            this.lblFechayHora.Name = "lblFechayHora";
            this.lblFechayHora.Size = new System.Drawing.Size(119, 17);
            this.lblFechayHora.TabIndex = 99;
            this.lblFechayHora.Text = "Fecha y hora Inicio:";
            this.lblFechayHora.Visible = false;
            // 
            // txtUbicacion
            // 
            this.txtUbicacion.Cursor = System.Windows.Forms.Cursors.No;
            this.txtUbicacion.Font = new System.Drawing.Font("Leelawadee UI", 9.75F);
            this.txtUbicacion.Location = new System.Drawing.Point(558, 341);
            this.txtUbicacion.MaxLength = 100;
            this.txtUbicacion.Multiline = true;
            this.txtUbicacion.Name = "txtUbicacion";
            this.txtUbicacion.ReadOnly = true;
            this.txtUbicacion.Size = new System.Drawing.Size(266, 21);
            this.txtUbicacion.TabIndex = 98;
            this.txtUbicacion.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Leelawadee UI", 9.75F);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(477, 345);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 17);
            this.label1.TabIndex = 97;
            this.label1.Text = "Ubicación";
            this.label1.Visible = false;
            // 
            // txtDescripcion
            // 
            this.txtDescripcion.Cursor = System.Windows.Forms.Cursors.No;
            this.txtDescripcion.Font = new System.Drawing.Font("Leelawadee UI", 9.75F);
            this.txtDescripcion.Location = new System.Drawing.Point(558, 259);
            this.txtDescripcion.MaxLength = 100;
            this.txtDescripcion.Multiline = true;
            this.txtDescripcion.Name = "txtDescripcion";
            this.txtDescripcion.ReadOnly = true;
            this.txtDescripcion.Size = new System.Drawing.Size(266, 65);
            this.txtDescripcion.TabIndex = 96;
            this.txtDescripcion.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Leelawadee UI", 9.75F);
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(477, 262);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(76, 17);
            this.label3.TabIndex = 95;
            this.label3.Text = "Descripción";
            this.label3.Visible = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(258, 280);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(196, 145);
            this.pictureBox1.TabIndex = 94;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Visible = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Leelawadee UI", 9.75F);
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(268, 250);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(51, 17);
            this.label4.TabIndex = 93;
            this.label4.Text = "Imagen";
            this.label4.Visible = false;
            // 
            // txtTitulo
            // 
            this.txtTitulo.Cursor = System.Windows.Forms.Cursors.No;
            this.txtTitulo.Font = new System.Drawing.Font("Leelawadee UI", 9.75F);
            this.txtTitulo.Location = new System.Drawing.Point(558, 219);
            this.txtTitulo.MaxLength = 100;
            this.txtTitulo.Multiline = true;
            this.txtTitulo.Name = "txtTitulo";
            this.txtTitulo.ReadOnly = true;
            this.txtTitulo.Size = new System.Drawing.Size(266, 21);
            this.txtTitulo.TabIndex = 92;
            this.txtTitulo.Visible = false;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Leelawadee UI", 9.75F);
            this.label10.ForeColor = System.Drawing.Color.White;
            this.label10.Location = new System.Drawing.Point(477, 219);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(40, 17);
            this.label10.TabIndex = 91;
            this.label10.Text = "Título";
            this.label10.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Leelawadee UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(322, 207);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 25);
            this.label2.TabIndex = 103;
            this.label2.Text = "Evento";
            this.label2.Visible = false;
            // 
            // lblTipo
            // 
            this.lblTipo.AutoSize = true;
            this.lblTipo.Font = new System.Drawing.Font("Leelawadee UI", 9.75F);
            this.lblTipo.ForeColor = System.Drawing.Color.White;
            this.lblTipo.Location = new System.Drawing.Point(58, 272);
            this.lblTipo.Name = "lblTipo";
            this.lblTipo.Size = new System.Drawing.Size(103, 17);
            this.lblTipo.TabIndex = 117;
            this.lblTipo.Text = "No Especificado";
            this.lblTipo.Visible = false;
            // 
            // lblT
            // 
            this.lblT.AutoSize = true;
            this.lblT.Font = new System.Drawing.Font("Leelawadee UI", 9.75F);
            this.lblT.ForeColor = System.Drawing.Color.White;
            this.lblT.Location = new System.Drawing.Point(12, 272);
            this.lblT.Name = "lblT";
            this.lblT.Size = new System.Drawing.Size(34, 17);
            this.lblT.TabIndex = 116;
            this.lblT.Text = "Tipo";
            this.lblT.Visible = false;
            // 
            // lblDescripcion
            // 
            this.lblDescripcion.AutoSize = true;
            this.lblDescripcion.Font = new System.Drawing.Font("Leelawadee UI", 9.75F);
            this.lblDescripcion.ForeColor = System.Drawing.Color.White;
            this.lblDescripcion.Location = new System.Drawing.Point(13, 330);
            this.lblDescripcion.Name = "lblDescripcion";
            this.lblDescripcion.Size = new System.Drawing.Size(149, 17);
            this.lblDescripcion.TabIndex = 114;
            this.lblDescripcion.Text = "Descripcion del reporte:";
            this.lblDescripcion.Visible = false;
            // 
            // lblNombreDeCuenta
            // 
            this.lblNombreDeCuenta.AutoSize = true;
            this.lblNombreDeCuenta.Font = new System.Drawing.Font("Leelawadee UI", 9.75F);
            this.lblNombreDeCuenta.ForeColor = System.Drawing.Color.White;
            this.lblNombreDeCuenta.Location = new System.Drawing.Point(138, 215);
            this.lblNombreDeCuenta.Name = "lblNombreDeCuenta";
            this.lblNombreDeCuenta.Size = new System.Drawing.Size(54, 17);
            this.lblNombreDeCuenta.TabIndex = 113;
            this.lblNombreDeCuenta.Text = "nombre";
            this.lblNombreDeCuenta.Visible = false;
            // 
            // lblNombre
            // 
            this.lblNombre.AutoSize = true;
            this.lblNombre.Font = new System.Drawing.Font("Leelawadee UI", 9.75F);
            this.lblNombre.ForeColor = System.Drawing.Color.White;
            this.lblNombre.Location = new System.Drawing.Point(12, 215);
            this.lblNombre.Name = "lblNombre";
            this.lblNombre.Size = new System.Drawing.Size(121, 17);
            this.lblNombre.TabIndex = 112;
            this.lblNombre.Text = "Nombre de cuenta:";
            this.lblNombre.Visible = false;
            // 
            // txtDescripcionReporte
            // 
            this.txtDescripcionReporte.Cursor = System.Windows.Forms.Cursors.No;
            this.txtDescripcionReporte.Font = new System.Drawing.Font("Leelawadee UI", 9.75F);
            this.txtDescripcionReporte.Location = new System.Drawing.Point(16, 360);
            this.txtDescripcionReporte.MaxLength = 100;
            this.txtDescripcionReporte.Multiline = true;
            this.txtDescripcionReporte.Name = "txtDescripcionReporte";
            this.txtDescripcionReporte.ReadOnly = true;
            this.txtDescripcionReporte.Size = new System.Drawing.Size(216, 65);
            this.txtDescripcionReporte.TabIndex = 118;
            this.txtDescripcionReporte.Visible = false;
            // 
            // ReporteEvento
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gray;
            this.ClientSize = new System.Drawing.Size(836, 441);
            this.Controls.Add(this.txtDescripcionReporte);
            this.Controls.Add(this.lblTipo);
            this.Controls.Add(this.lblT);
            this.Controls.Add(this.lblDescripcion);
            this.Controls.Add(this.lblNombreDeCuenta);
            this.Controls.Add(this.lblNombre);
            this.Controls.Add(this.label2);
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
            this.Controls.Add(this.btnBuscar);
            this.Controls.Add(this.btnEliminar);
            this.Controls.Add(this.lblIdPost);
            this.Controls.Add(this.txtID);
            this.Controls.Add(this.dataGridView1);
            this.Name = "ReporteEvento";
            this.Text = "ReporteEvento";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.Label lblIdPost;
        private System.Windows.Forms.TextBox txtID;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label lblFin;
        private System.Windows.Forms.Label lblInicio;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblFechayHora;
        private System.Windows.Forms.TextBox txtUbicacion;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtDescripcion;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtTitulo;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblTipo;
        private System.Windows.Forms.Label lblT;
        private System.Windows.Forms.Label lblDescripcion;
        private System.Windows.Forms.Label lblNombreDeCuenta;
        private System.Windows.Forms.Label lblNombre;
        private System.Windows.Forms.TextBox txtDescripcionReporte;
    }
}