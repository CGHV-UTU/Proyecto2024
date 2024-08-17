
namespace BackofficeDeAdministracion
{
    partial class GestionarUsuario
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
            this.btnVolver = new System.Windows.Forms.Button();
            this.lblNombre = new System.Windows.Forms.Label();
            this.lblEstado = new System.Windows.Forms.Label();
            this.lblReportes = new System.Windows.Forms.Label();
            this.lblNombreDeCuenta = new System.Windows.Forms.Label();
            this.lblEstadoDeCuenta = new System.Windows.Forms.Label();
            this.lblReportesDeCuenta = new System.Windows.Forms.Label();
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
            this.btnVerReportes = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnBuscar
            // 
            this.btnBuscar.Location = new System.Drawing.Point(17, 117);
            this.btnBuscar.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(288, 28);
            this.btnBuscar.TabIndex = 64;
            this.btnBuscar.Text = "🔎 Buscar ";
            this.btnBuscar.UseVisualStyleBackColor = true;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // lblNom
            // 
            this.lblNom.AutoSize = true;
            this.lblNom.Location = new System.Drawing.Point(13, 69);
            this.lblNom.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblNom.Name = "lblNom";
            this.lblNom.Size = new System.Drawing.Size(129, 17);
            this.lblNom.TabIndex = 63;
            this.lblNom.Text = "Nombre de cuenta:";
            // 
            // txtID
            // 
            this.txtID.Location = new System.Drawing.Point(144, 65);
            this.txtID.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtID.Name = "txtID";
            this.txtID.Size = new System.Drawing.Size(160, 22);
            this.txtID.TabIndex = 62;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(343, 15);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(727, 209);
            this.dataGridView1.TabIndex = 65;
            // 
            // btnVolver
            // 
            this.btnVolver.BackColor = System.Drawing.Color.Red;
            this.btnVolver.ForeColor = System.Drawing.Color.White;
            this.btnVolver.Location = new System.Drawing.Point(0, 0);
            this.btnVolver.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnVolver.Name = "btnVolver";
            this.btnVolver.Size = new System.Drawing.Size(119, 28);
            this.btnVolver.TabIndex = 67;
            this.btnVolver.Text = " ⬅️ Volver";
            this.btnVolver.UseVisualStyleBackColor = false;
            this.btnVolver.Click += new System.EventHandler(this.btnVolver_Click);
            // 
            // lblNombre
            // 
            this.lblNombre.AutoSize = true;
            this.lblNombre.Location = new System.Drawing.Point(13, 293);
            this.lblNombre.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblNombre.Name = "lblNombre";
            this.lblNombre.Size = new System.Drawing.Size(129, 17);
            this.lblNombre.TabIndex = 68;
            this.lblNombre.Text = "Nombre de cuenta:";
            this.lblNombre.Visible = false;
            // 
            // lblEstado
            // 
            this.lblEstado.AutoSize = true;
            this.lblEstado.Location = new System.Drawing.Point(13, 405);
            this.lblEstado.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblEstado.Name = "lblEstado";
            this.lblEstado.Size = new System.Drawing.Size(56, 17);
            this.lblEstado.TabIndex = 72;
            this.lblEstado.Text = "Estado:";
            this.lblEstado.Visible = false;
            // 
            // lblReportes
            // 
            this.lblReportes.AutoSize = true;
            this.lblReportes.Location = new System.Drawing.Point(13, 466);
            this.lblReportes.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblReportes.Name = "lblReportes";
            this.lblReportes.Size = new System.Drawing.Size(135, 17);
            this.lblReportes.TabIndex = 73;
            this.lblReportes.Text = "Número de reportes";
            this.lblReportes.Visible = false;
            // 
            // lblNombreDeCuenta
            // 
            this.lblNombreDeCuenta.AutoSize = true;
            this.lblNombreDeCuenta.Location = new System.Drawing.Point(184, 293);
            this.lblNombreDeCuenta.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblNombreDeCuenta.Name = "lblNombreDeCuenta";
            this.lblNombreDeCuenta.Size = new System.Drawing.Size(0, 17);
            this.lblNombreDeCuenta.TabIndex = 75;
            // 
            // lblEstadoDeCuenta
            // 
            this.lblEstadoDeCuenta.AutoSize = true;
            this.lblEstadoDeCuenta.Location = new System.Drawing.Point(184, 405);
            this.lblEstadoDeCuenta.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblEstadoDeCuenta.Name = "lblEstadoDeCuenta";
            this.lblEstadoDeCuenta.Size = new System.Drawing.Size(52, 17);
            this.lblEstadoDeCuenta.TabIndex = 76;
            this.lblEstadoDeCuenta.Text = "Estado";
            this.lblEstadoDeCuenta.Visible = false;
            // 
            // lblReportesDeCuenta
            // 
            this.lblReportesDeCuenta.AutoSize = true;
            this.lblReportesDeCuenta.Location = new System.Drawing.Point(184, 466);
            this.lblReportesDeCuenta.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblReportesDeCuenta.Name = "lblReportesDeCuenta";
            this.lblReportesDeCuenta.Size = new System.Drawing.Size(58, 17);
            this.lblReportesDeCuenta.TabIndex = 77;
            this.lblReportesDeCuenta.Text = "Numero";
            this.lblReportesDeCuenta.Visible = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(427, 293);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(213, 148);
            this.pictureBox1.TabIndex = 78;
            this.pictureBox1.TabStop = false;
            // 
            // lblFoto
            // 
            this.lblFoto.AutoSize = true;
            this.lblFoto.Location = new System.Drawing.Point(309, 308);
            this.lblFoto.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblFoto.Name = "lblFoto";
            this.lblFoto.Size = new System.Drawing.Size(91, 17);
            this.lblFoto.TabIndex = 79;
            this.lblFoto.Text = "Foto de perfil";
            this.lblFoto.Visible = false;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(700, 302);
            this.button1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(160, 28);
            this.button1.TabIndex = 80;
            this.button1.Text = "Baneo Permanente";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.btnBaneoPermanente);
            // 
            // lblNombreVisible
            // 
            this.lblNombreVisible.AutoSize = true;
            this.lblNombreVisible.Location = new System.Drawing.Point(184, 350);
            this.lblNombreVisible.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblNombreVisible.Name = "lblNombreVisible";
            this.lblNombreVisible.Size = new System.Drawing.Size(58, 17);
            this.lblNombreVisible.TabIndex = 82;
            this.lblNombreVisible.Text = "Nombre";
            this.lblNombreVisible.Visible = false;
            // 
            // lblNomVisible
            // 
            this.lblNomVisible.AutoSize = true;
            this.lblNomVisible.Location = new System.Drawing.Point(13, 350);
            this.lblNomVisible.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblNomVisible.Name = "lblNomVisible";
            this.lblNomVisible.Size = new System.Drawing.Size(105, 17);
            this.lblNomVisible.TabIndex = 81;
            this.lblNomVisible.Text = "Nombre visible:";
            this.lblNomVisible.Visible = false;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(812, 466);
            this.button2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(160, 28);
            this.button2.TabIndex = 83;
            this.button2.Text = "Baneo Temporal";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.btnBaneoTemporal);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(897, 302);
            this.button3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(160, 28);
            this.button3.TabIndex = 84;
            this.button3.Text = "Desbanear";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.btnDesbanear);
            // 
            // dtpHora
            // 
            this.dtpHora.CustomFormat = "HH:mm";
            this.dtpHora.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpHora.Location = new System.Drawing.Point(883, 416);
            this.dtpHora.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dtpHora.Name = "dtpHora";
            this.dtpHora.Size = new System.Drawing.Size(173, 22);
            this.dtpHora.TabIndex = 86;
            // 
            // dtpFecha
            // 
            this.dtpFecha.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFecha.Location = new System.Drawing.Point(700, 416);
            this.dtpFecha.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dtpFecha.Name = "dtpFecha";
            this.dtpFecha.Size = new System.Drawing.Size(173, 22);
            this.dtpFecha.TabIndex = 85;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(791, 377);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(157, 17);
            this.label4.TabIndex = 87;
            this.label4.Text = "Banear Temporalmente";
            // 
            // btnVerReportes
            // 
            this.btnVerReportes.Location = new System.Drawing.Point(312, 469);
            this.btnVerReportes.Name = "btnVerReportes";
            this.btnVerReportes.Size = new System.Drawing.Size(75, 23);
            this.btnVerReportes.TabIndex = 88;
            this.btnVerReportes.Text = "Ver";
            this.btnVerReportes.UseVisualStyleBackColor = true;
            // 
            // GestionarUsuario
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1089, 545);
            this.Controls.Add(this.btnVerReportes);
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
            this.Controls.Add(this.lblReportesDeCuenta);
            this.Controls.Add(this.lblEstadoDeCuenta);
            this.Controls.Add(this.lblNombreDeCuenta);
            this.Controls.Add(this.lblReportes);
            this.Controls.Add(this.lblEstado);
            this.Controls.Add(this.lblNombre);
            this.Controls.Add(this.btnVolver);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btnBuscar);
            this.Controls.Add(this.lblNom);
            this.Controls.Add(this.txtID);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "GestionarUsuario";
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
        private System.Windows.Forms.Button btnVolver;
        private System.Windows.Forms.Label lblNombre;
        private System.Windows.Forms.Label lblEstado;
        private System.Windows.Forms.Label lblReportes;
        private System.Windows.Forms.Label lblNombreDeCuenta;
        private System.Windows.Forms.Label lblEstadoDeCuenta;
        private System.Windows.Forms.Label lblReportesDeCuenta;
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
        private System.Windows.Forms.Button btnVerReportes;
    }
}