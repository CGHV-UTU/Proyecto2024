
namespace BackofficeDeAdministracion
{
    partial class GestionarUsuarios
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
            this.lblNom = new System.Windows.Forms.Label();
            this.txtID = new System.Windows.Forms.TextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.lblNombre = new System.Windows.Forms.Label();
            this.lblEstado = new System.Windows.Forms.Label();
            this.lblNombreDeCuenta = new System.Windows.Forms.Label();
            this.lblEstadoDeCuenta = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblFoto = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.lblNombreVisible = new System.Windows.Forms.Label();
            this.lblNomVisible = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.dtpHora = new System.Windows.Forms.DateTimePicker();
            this.dtpFecha = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblDescripcion = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnBuscar
            // 
            this.btnBuscar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(101)))), ((int)(((byte)(64)))), ((int)(((byte)(222)))));
            this.btnBuscar.Font = new System.Drawing.Font("Leelawadee UI", 9.75F);
            this.btnBuscar.ForeColor = System.Drawing.Color.White;
            this.btnBuscar.Location = new System.Drawing.Point(13, 95);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(216, 27);
            this.btnBuscar.TabIndex = 64;
            this.btnBuscar.Text = "🔎 Buscar ";
            this.btnBuscar.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnBuscar.UseVisualStyleBackColor = false;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // lblNom
            // 
            this.lblNom.AutoSize = true;
            this.lblNom.Font = new System.Drawing.Font("Leelawadee UI", 9.75F);
            this.lblNom.ForeColor = System.Drawing.Color.White;
            this.lblNom.Location = new System.Drawing.Point(10, 56);
            this.lblNom.Name = "lblNom";
            this.lblNom.Size = new System.Drawing.Size(121, 17);
            this.lblNom.TabIndex = 63;
            this.lblNom.Text = "Nombre de cuenta:";
            // 
            // txtID
            // 
            this.txtID.Location = new System.Drawing.Point(130, 56);
            this.txtID.Name = "txtID";
            this.txtID.Size = new System.Drawing.Size(121, 20);
            this.txtID.TabIndex = 62;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(257, 12);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(565, 167);
            this.dataGridView1.TabIndex = 65;
            this.dataGridView1.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView1_CellMouseDown);
            this.dataGridView1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dataGridView1_MouseDown);
            // 
            // lblNombre
            // 
            this.lblNombre.AutoSize = true;
            this.lblNombre.Font = new System.Drawing.Font("Leelawadee UI", 9.75F);
            this.lblNombre.ForeColor = System.Drawing.Color.White;
            this.lblNombre.Location = new System.Drawing.Point(10, 216);
            this.lblNombre.Name = "lblNombre";
            this.lblNombre.Size = new System.Drawing.Size(121, 17);
            this.lblNombre.TabIndex = 68;
            this.lblNombre.Text = "Nombre de cuenta:";
            this.lblNombre.Visible = false;
            // 
            // lblEstado
            // 
            this.lblEstado.AutoSize = true;
            this.lblEstado.Font = new System.Drawing.Font("Leelawadee UI", 9.75F);
            this.lblEstado.ForeColor = System.Drawing.Color.White;
            this.lblEstado.Location = new System.Drawing.Point(10, 301);
            this.lblEstado.Name = "lblEstado";
            this.lblEstado.Size = new System.Drawing.Size(51, 17);
            this.lblEstado.TabIndex = 72;
            this.lblEstado.Text = "Estado:";
            this.lblEstado.Visible = false;
            // 
            // lblNombreDeCuenta
            // 
            this.lblNombreDeCuenta.AutoSize = true;
            this.lblNombreDeCuenta.Font = new System.Drawing.Font("Leelawadee UI", 9.75F);
            this.lblNombreDeCuenta.ForeColor = System.Drawing.Color.White;
            this.lblNombreDeCuenta.Location = new System.Drawing.Point(138, 216);
            this.lblNombreDeCuenta.Name = "lblNombreDeCuenta";
            this.lblNombreDeCuenta.Size = new System.Drawing.Size(75, 17);
            this.lblNombreDeCuenta.TabIndex = 75;
            this.lblNombreDeCuenta.Text = "NomVisible";
            this.lblNombreDeCuenta.Visible = false;
            // 
            // lblEstadoDeCuenta
            // 
            this.lblEstadoDeCuenta.AutoSize = true;
            this.lblEstadoDeCuenta.Font = new System.Drawing.Font("Leelawadee UI", 9.75F);
            this.lblEstadoDeCuenta.ForeColor = System.Drawing.Color.White;
            this.lblEstadoDeCuenta.Location = new System.Drawing.Point(138, 301);
            this.lblEstadoDeCuenta.Name = "lblEstadoDeCuenta";
            this.lblEstadoDeCuenta.Size = new System.Drawing.Size(48, 17);
            this.lblEstadoDeCuenta.TabIndex = 76;
            this.lblEstadoDeCuenta.Text = "Estado";
            this.lblEstadoDeCuenta.Visible = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(333, 245);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(201, 164);
            this.pictureBox1.TabIndex = 78;
            this.pictureBox1.TabStop = false;
            // 
            // lblFoto
            // 
            this.lblFoto.AutoSize = true;
            this.lblFoto.Font = new System.Drawing.Font("Leelawadee UI", 9.75F);
            this.lblFoto.ForeColor = System.Drawing.Color.White;
            this.lblFoto.Location = new System.Drawing.Point(330, 216);
            this.lblFoto.Name = "lblFoto";
            this.lblFoto.Size = new System.Drawing.Size(87, 17);
            this.lblFoto.TabIndex = 79;
            this.lblFoto.Text = "Foto de perfil";
            this.lblFoto.Visible = false;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(101)))), ((int)(((byte)(64)))), ((int)(((byte)(222)))));
            this.button1.Font = new System.Drawing.Font("Leelawadee UI", 9.75F);
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(554, 245);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(131, 27);
            this.button1.TabIndex = 80;
            this.button1.Text = "Baneo Permanente";
            this.button1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.btnBaneoPermanente);
            // 
            // lblNombreVisible
            // 
            this.lblNombreVisible.AutoSize = true;
            this.lblNombreVisible.Font = new System.Drawing.Font("Leelawadee UI", 9.75F);
            this.lblNombreVisible.ForeColor = System.Drawing.Color.White;
            this.lblNombreVisible.Location = new System.Drawing.Point(138, 256);
            this.lblNombreVisible.Name = "lblNombreVisible";
            this.lblNombreVisible.Size = new System.Drawing.Size(57, 17);
            this.lblNombreVisible.TabIndex = 82;
            this.lblNombreVisible.Text = "Nombre";
            this.lblNombreVisible.Visible = false;
            // 
            // lblNomVisible
            // 
            this.lblNomVisible.AutoSize = true;
            this.lblNomVisible.Font = new System.Drawing.Font("Leelawadee UI", 9.75F);
            this.lblNomVisible.ForeColor = System.Drawing.Color.White;
            this.lblNomVisible.Location = new System.Drawing.Point(10, 256);
            this.lblNomVisible.Name = "lblNomVisible";
            this.lblNomVisible.Size = new System.Drawing.Size(100, 17);
            this.lblNomVisible.TabIndex = 81;
            this.lblNomVisible.Text = "Nombre visible:";
            this.lblNomVisible.Visible = false;
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(101)))), ((int)(((byte)(64)))), ((int)(((byte)(222)))));
            this.button2.Font = new System.Drawing.Font("Leelawadee UI", 9.75F);
            this.button2.ForeColor = System.Drawing.Color.White;
            this.button2.Location = new System.Drawing.Point(627, 382);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(131, 27);
            this.button2.TabIndex = 83;
            this.button2.Text = "Baneo Temporal";
            this.button2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.btnBaneoTemporal);
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(101)))), ((int)(((byte)(64)))), ((int)(((byte)(222)))));
            this.button3.Font = new System.Drawing.Font("Leelawadee UI", 9.75F);
            this.button3.ForeColor = System.Drawing.Color.White;
            this.button3.Location = new System.Drawing.Point(693, 245);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(131, 27);
            this.button3.TabIndex = 84;
            this.button3.Text = "Desbanear";
            this.button3.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.btnDesbanear);
            // 
            // dtpHora
            // 
            this.dtpHora.CustomFormat = "HH:mm";
            this.dtpHora.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpHora.Location = new System.Drawing.Point(691, 338);
            this.dtpHora.Name = "dtpHora";
            this.dtpHora.Size = new System.Drawing.Size(131, 20);
            this.dtpHora.TabIndex = 86;
            // 
            // dtpFecha
            // 
            this.dtpFecha.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFecha.Location = new System.Drawing.Point(554, 338);
            this.dtpFecha.Name = "dtpFecha";
            this.dtpFecha.Size = new System.Drawing.Size(131, 20);
            this.dtpFecha.TabIndex = 85;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Leelawadee UI", 9.75F);
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(614, 307);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(144, 17);
            this.label4.TabIndex = 87;
            this.label4.Text = "Banear Temporalmente";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Leelawadee UI", 9.75F);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(10, 353);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 17);
            this.label2.TabIndex = 88;
            this.label2.Text = "Descripcion:";
            this.label2.Visible = false;
            // 
            // lblDescripcion
            // 
            this.lblDescripcion.Cursor = System.Windows.Forms.Cursors.No;
            this.lblDescripcion.Font = new System.Drawing.Font("Leelawadee UI", 9.75F);
            this.lblDescripcion.Location = new System.Drawing.Point(95, 350);
            this.lblDescripcion.Multiline = true;
            this.lblDescripcion.Name = "lblDescripcion";
            this.lblDescripcion.ReadOnly = true;
            this.lblDescripcion.Size = new System.Drawing.Size(220, 74);
            this.lblDescripcion.TabIndex = 90;
            this.lblDescripcion.Visible = false;
            // 
            // GestionarUsuarios
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gray;
            this.ClientSize = new System.Drawing.Size(836, 441);
            this.Controls.Add(this.lblDescripcion);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.dtpHora);
            this.Controls.Add(this.dtpFecha);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.lblNombreVisible);
            this.Controls.Add(this.lblNomVisible);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.lblFoto);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.lblEstadoDeCuenta);
            this.Controls.Add(this.lblNombreDeCuenta);
            this.Controls.Add(this.lblEstado);
            this.Controls.Add(this.lblNombre);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btnBuscar);
            this.Controls.Add(this.lblNom);
            this.Controls.Add(this.txtID);
            this.Name = "GestionarUsuarios";
            this.Text = "EditarUsuario";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.Label lblNom;
        private System.Windows.Forms.TextBox txtID;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label lblNombre;
        private System.Windows.Forms.Label lblEstado;
        private System.Windows.Forms.Label lblNombreDeCuenta;
        private System.Windows.Forms.Label lblEstadoDeCuenta;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lblFoto;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label lblNombreVisible;
        private System.Windows.Forms.Label lblNomVisible;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.DateTimePicker dtpHora;
        private System.Windows.Forms.DateTimePicker dtpFecha;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox lblDescripcion;
    }
}