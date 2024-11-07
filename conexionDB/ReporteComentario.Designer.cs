
namespace BackofficeDeAdministracion
{
    partial class ReporteComentario
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
            this.btnGuardar = new System.Windows.Forms.Button();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.lblDescripcionReporte = new System.Windows.Forms.Label();
            this.lblDescripcion = new System.Windows.Forms.Label();
            this.lblNombreDeCuenta = new System.Windows.Forms.Label();
            this.lblNombre = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.lblNom = new System.Windows.Forms.Label();
            this.txtID = new System.Windows.Forms.TextBox();
            this.lblTipo = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.Comentario = new System.Windows.Forms.Label();
            this.txtComentario = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnGuardar
            // 
            this.btnGuardar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(101)))), ((int)(((byte)(64)))), ((int)(((byte)(222)))));
            this.btnGuardar.ForeColor = System.Drawing.Color.White;
            this.btnGuardar.Location = new System.Drawing.Point(17, 407);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(811, 23);
            this.btnGuardar.TabIndex = 132;
            this.btnGuardar.Text = "💾 Guardar y Salir";
            this.btnGuardar.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnGuardar.UseVisualStyleBackColor = false;
            // 
            // btnEliminar
            // 
            this.btnEliminar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(101)))), ((int)(((byte)(64)))), ((int)(((byte)(222)))));
            this.btnEliminar.ForeColor = System.Drawing.Color.White;
            this.btnEliminar.Location = new System.Drawing.Point(12, 141);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(216, 23);
            this.btnEliminar.TabIndex = 131;
            this.btnEliminar.Text = "❌Eliminar ";
            this.btnEliminar.UseVisualStyleBackColor = false;
            // 
            // lblDescripcionReporte
            // 
            this.lblDescripcionReporte.AutoSize = true;
            this.lblDescripcionReporte.ForeColor = System.Drawing.Color.White;
            this.lblDescripcionReporte.Location = new System.Drawing.Point(117, 320);
            this.lblDescripcionReporte.Name = "lblDescripcionReporte";
            this.lblDescripcionReporte.Size = new System.Drawing.Size(44, 13);
            this.lblDescripcionReporte.TabIndex = 130;
            this.lblDescripcionReporte.Text = "Nombre";
            this.lblDescripcionReporte.Visible = false;
            // 
            // lblDescripcion
            // 
            this.lblDescripcion.AutoSize = true;
            this.lblDescripcion.ForeColor = System.Drawing.Color.White;
            this.lblDescripcion.Location = new System.Drawing.Point(32, 320);
            this.lblDescripcion.Name = "lblDescripcion";
            this.lblDescripcion.Size = new System.Drawing.Size(66, 13);
            this.lblDescripcion.TabIndex = 129;
            this.lblDescripcion.Text = "Descripcion:";
            this.lblDescripcion.Visible = false;
            // 
            // lblNombreDeCuenta
            // 
            this.lblNombreDeCuenta.AutoSize = true;
            this.lblNombreDeCuenta.ForeColor = System.Drawing.Color.White;
            this.lblNombreDeCuenta.Location = new System.Drawing.Point(133, 320);
            this.lblNombreDeCuenta.Name = "lblNombreDeCuenta";
            this.lblNombreDeCuenta.Size = new System.Drawing.Size(0, 13);
            this.lblNombreDeCuenta.TabIndex = 128;
            // 
            // lblNombre
            // 
            this.lblNombre.AutoSize = true;
            this.lblNombre.ForeColor = System.Drawing.Color.White;
            this.lblNombre.Location = new System.Drawing.Point(32, 221);
            this.lblNombre.Name = "lblNombre";
            this.lblNombre.Size = new System.Drawing.Size(98, 13);
            this.lblNombre.TabIndex = 127;
            this.lblNombre.Text = "Nombre de cuenta:";
            this.lblNombre.Visible = false;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(256, 13);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(566, 170);
            this.dataGridView1.TabIndex = 125;
            // 
            // btnBuscar
            // 
            this.btnBuscar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(101)))), ((int)(((byte)(64)))), ((int)(((byte)(222)))));
            this.btnBuscar.ForeColor = System.Drawing.Color.White;
            this.btnBuscar.Location = new System.Drawing.Point(11, 96);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(216, 23);
            this.btnBuscar.TabIndex = 124;
            this.btnBuscar.Text = "🔎 Buscar ";
            this.btnBuscar.UseVisualStyleBackColor = false;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // lblNom
            // 
            this.lblNom.AutoSize = true;
            this.lblNom.ForeColor = System.Drawing.Color.White;
            this.lblNom.Location = new System.Drawing.Point(14, 56);
            this.lblNom.Name = "lblNom";
            this.lblNom.Size = new System.Drawing.Size(75, 13);
            this.lblNom.TabIndex = 123;
            this.lblNom.Text = "Id de Reporte:";
            // 
            // txtID
            // 
            this.txtID.Location = new System.Drawing.Point(106, 54);
            this.txtID.Name = "txtID";
            this.txtID.Size = new System.Drawing.Size(121, 20);
            this.txtID.TabIndex = 122;
            // 
            // lblTipo
            // 
            this.lblTipo.AutoSize = true;
            this.lblTipo.ForeColor = System.Drawing.Color.White;
            this.lblTipo.Location = new System.Drawing.Point(76, 268);
            this.lblTipo.Name = "lblTipo";
            this.lblTipo.Size = new System.Drawing.Size(22, 13);
            this.lblTipo.TabIndex = 133;
            this.lblTipo.Text = "Tip";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(32, 268);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(28, 13);
            this.label1.TabIndex = 134;
            this.label1.Text = "Tipo";
            // 
            // Comentario
            // 
            this.Comentario.AutoSize = true;
            this.Comentario.ForeColor = System.Drawing.Color.White;
            this.Comentario.Location = new System.Drawing.Point(360, 218);
            this.Comentario.Name = "Comentario";
            this.Comentario.Size = new System.Drawing.Size(63, 13);
            this.Comentario.TabIndex = 135;
            this.Comentario.Text = "Comentario:";
            this.Comentario.Visible = false;
            // 
            // txtComentario
            // 
            this.txtComentario.Cursor = System.Windows.Forms.Cursors.No;
            this.txtComentario.Location = new System.Drawing.Point(429, 218);
            this.txtComentario.Multiline = true;
            this.txtComentario.Name = "txtComentario";
            this.txtComentario.ReadOnly = true;
            this.txtComentario.Size = new System.Drawing.Size(370, 115);
            this.txtComentario.TabIndex = 136;
            this.txtComentario.Visible = false;
            // 
            // ReporteComentario
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gray;
            this.ClientSize = new System.Drawing.Size(836, 441);
            this.Controls.Add(this.txtComentario);
            this.Controls.Add(this.Comentario);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblTipo);
            this.Controls.Add(this.btnGuardar);
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
            this.Name = "ReporteComentario";
            this.Text = "ReporteComentario";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.Label lblDescripcionReporte;
        private System.Windows.Forms.Label lblDescripcion;
        private System.Windows.Forms.Label lblNombreDeCuenta;
        private System.Windows.Forms.Label lblNombre;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.Label lblNom;
        private System.Windows.Forms.TextBox txtID;
        private System.Windows.Forms.Label lblTipo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label Comentario;
        private System.Windows.Forms.TextBox txtComentario;
    }
}