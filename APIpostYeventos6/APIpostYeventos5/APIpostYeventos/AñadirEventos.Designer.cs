
namespace APIpostYeventos
{
    partial class AñadirEvento
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
            this.btnPublicar = new System.Windows.Forms.Button();
            this.btnSeleccionarFecha = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.lblFechaHora = new System.Windows.Forms.Label();
            this.txtUbicacion = new System.Windows.Forms.TextBox();
            this.txtTitulo = new System.Windows.Forms.TextBox();
            this.lblHora = new System.Windows.Forms.Label();
            this.lblFecha = new System.Windows.Forms.Label();
            this.dtpHora = new System.Windows.Forms.DateTimePicker();
            this.dtpFecha = new System.Windows.Forms.DateTimePicker();
            this.btnVolver = new System.Windows.Forms.Button();
            this.btnSeleccionar = new System.Windows.Forms.Button();
            this.pbxImagen = new System.Windows.Forms.PictureBox();
            this.label3 = new System.Windows.Forms.Label();
            this.lblUbicacion = new System.Windows.Forms.Label();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.lblDescripcion = new System.Windows.Forms.Label();
            this.txtDescripcion = new System.Windows.Forms.TextBox();
            this.lblError = new System.Windows.Forms.Label();
            this.btnSeleccionarFecha2 = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.lblFechaHora2 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.dtpHora2 = new System.Windows.Forms.DateTimePicker();
            this.dtpFecha2 = new System.Windows.Forms.DateTimePicker();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pbxImagen)).BeginInit();
            this.SuspendLayout();
            // 
            // btnPublicar
            // 
            this.btnPublicar.Location = new System.Drawing.Point(18, 386);
            this.btnPublicar.Name = "btnPublicar";
            this.btnPublicar.Size = new System.Drawing.Size(91, 23);
            this.btnPublicar.TabIndex = 31;
            this.btnPublicar.Text = "Publicar Evento";
            this.btnPublicar.UseVisualStyleBackColor = true;
            this.btnPublicar.Click += new System.EventHandler(this.btnPublicar_Click);
            // 
            // btnSeleccionarFecha
            // 
            this.btnSeleccionarFecha.Location = new System.Drawing.Point(410, 144);
            this.btnSeleccionarFecha.Name = "btnSeleccionarFecha";
            this.btnSeleccionarFecha.Size = new System.Drawing.Size(200, 23);
            this.btnSeleccionarFecha.TabIndex = 30;
            this.btnSeleccionarFecha.Text = "Seleccionar Fecha y Hora";
            this.btnSeleccionarFecha.UseVisualStyleBackColor = true;
            this.btnSeleccionarFecha.Click += new System.EventHandler(this.btnSeleccionarFecha_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(402, 180);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(129, 13);
            this.label2.TabIndex = 29;
            this.label2.Text = "Fecha y hora del Evento :";
            // 
            // lblFechaHora
            // 
            this.lblFechaHora.AutoSize = true;
            this.lblFechaHora.Location = new System.Drawing.Point(542, 180);
            this.lblFechaHora.Name = "lblFechaHora";
            this.lblFechaHora.Size = new System.Drawing.Size(0, 13);
            this.lblFechaHora.TabIndex = 28;
            // 
            // txtUbicacion
            // 
            this.txtUbicacion.Location = new System.Drawing.Point(95, 63);
            this.txtUbicacion.Name = "txtUbicacion";
            this.txtUbicacion.Size = new System.Drawing.Size(150, 20);
            this.txtUbicacion.TabIndex = 27;
            // 
            // txtTitulo
            // 
            this.txtTitulo.Location = new System.Drawing.Point(95, 32);
            this.txtTitulo.Name = "txtTitulo";
            this.txtTitulo.Size = new System.Drawing.Size(150, 20);
            this.txtTitulo.TabIndex = 26;
            // 
            // lblHora
            // 
            this.lblHora.AutoSize = true;
            this.lblHora.Location = new System.Drawing.Point(407, 102);
            this.lblHora.Name = "lblHora";
            this.lblHora.Size = new System.Drawing.Size(30, 13);
            this.lblHora.TabIndex = 25;
            this.lblHora.Text = "Hora";
            // 
            // lblFecha
            // 
            this.lblFecha.AutoSize = true;
            this.lblFecha.Location = new System.Drawing.Point(407, 54);
            this.lblFecha.Name = "lblFecha";
            this.lblFecha.Size = new System.Drawing.Size(37, 13);
            this.lblFecha.TabIndex = 24;
            this.lblFecha.Text = "Fecha";
            // 
            // dtpHora
            // 
            this.dtpHora.CustomFormat = "HH:mm";
            this.dtpHora.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpHora.Location = new System.Drawing.Point(410, 118);
            this.dtpHora.MinDate = new System.DateTime(2024, 6, 4, 0, 0, 0, 0);
            this.dtpHora.Name = "dtpHora";
            this.dtpHora.Size = new System.Drawing.Size(200, 20);
            this.dtpHora.TabIndex = 23;
            // 
            // dtpFecha
            // 
            this.dtpFecha.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFecha.Location = new System.Drawing.Point(410, 70);
            this.dtpFecha.Name = "dtpFecha";
            this.dtpFecha.Size = new System.Drawing.Size(200, 20);
            this.dtpFecha.TabIndex = 22;
            this.dtpFecha.ValueChanged += new System.EventHandler(this.dtpFecha_ValueChanged);
            // 
            // btnVolver
            // 
            this.btnVolver.Location = new System.Drawing.Point(18, 415);
            this.btnVolver.Name = "btnVolver";
            this.btnVolver.Size = new System.Drawing.Size(91, 23);
            this.btnVolver.TabIndex = 21;
            this.btnVolver.Text = "Volver";
            this.btnVolver.UseVisualStyleBackColor = true;
            this.btnVolver.Click += new System.EventHandler(this.btnVolver_Click);
            // 
            // btnSeleccionar
            // 
            this.btnSeleccionar.Location = new System.Drawing.Point(92, 287);
            this.btnSeleccionar.Name = "btnSeleccionar";
            this.btnSeleccionar.Size = new System.Drawing.Size(132, 23);
            this.btnSeleccionar.TabIndex = 20;
            this.btnSeleccionar.Text = "Seleccionar imagen";
            this.btnSeleccionar.UseVisualStyleBackColor = true;
            this.btnSeleccionar.Click += new System.EventHandler(this.btnSeleccionar_Click);
            // 
            // pbxImagen
            // 
            this.pbxImagen.Location = new System.Drawing.Point(80, 146);
            this.pbxImagen.Name = "pbxImagen";
            this.pbxImagen.Size = new System.Drawing.Size(159, 135);
            this.pbxImagen.TabIndex = 19;
            this.pbxImagen.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(115, 130);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 13);
            this.label3.TabIndex = 18;
            this.label3.Text = "Foto del Evento";
            // 
            // lblUbicacion
            // 
            this.lblUbicacion.AutoSize = true;
            this.lblUbicacion.Location = new System.Drawing.Point(17, 66);
            this.lblUbicacion.Name = "lblUbicacion";
            this.lblUbicacion.Size = new System.Drawing.Size(55, 13);
            this.lblUbicacion.TabIndex = 17;
            this.lblUbicacion.Text = "Ubicación";
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Location = new System.Drawing.Point(17, 35);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(33, 13);
            this.lblTitulo.TabIndex = 16;
            this.lblTitulo.Text = "Titulo";
            // 
            // lblDescripcion
            // 
            this.lblDescripcion.AutoSize = true;
            this.lblDescripcion.Location = new System.Drawing.Point(17, 92);
            this.lblDescripcion.Name = "lblDescripcion";
            this.lblDescripcion.Size = new System.Drawing.Size(63, 13);
            this.lblDescripcion.TabIndex = 32;
            this.lblDescripcion.Text = "Descripcion";
            // 
            // txtDescripcion
            // 
            this.txtDescripcion.Location = new System.Drawing.Point(95, 89);
            this.txtDescripcion.Name = "txtDescripcion";
            this.txtDescripcion.Size = new System.Drawing.Size(150, 20);
            this.txtDescripcion.TabIndex = 33;
            // 
            // lblError
            // 
            this.lblError.AutoSize = true;
            this.lblError.Location = new System.Drawing.Point(115, 391);
            this.lblError.Name = "lblError";
            this.lblError.Size = new System.Drawing.Size(160, 13);
            this.lblError.TabIndex = 34;
            this.lblError.Text = "Debe Rellenar todos los campos";
            this.lblError.Visible = false;
            // 
            // btnSeleccionarFecha2
            // 
            this.btnSeleccionarFecha2.Location = new System.Drawing.Point(410, 362);
            this.btnSeleccionarFecha2.Name = "btnSeleccionarFecha2";
            this.btnSeleccionarFecha2.Size = new System.Drawing.Size(200, 23);
            this.btnSeleccionarFecha2.TabIndex = 44;
            this.btnSeleccionarFecha2.Text = "Seleccionar Fecha y Hora";
            this.btnSeleccionarFecha2.UseVisualStyleBackColor = true;
            this.btnSeleccionarFecha2.Click += new System.EventHandler(this.btnSeleccionarFecha2_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(402, 398);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(129, 13);
            this.label5.TabIndex = 43;
            this.label5.Text = "Fecha y hora del Evento :";
            // 
            // lblFechaHora2
            // 
            this.lblFechaHora2.AutoSize = true;
            this.lblFechaHora2.Location = new System.Drawing.Point(542, 398);
            this.lblFechaHora2.Name = "lblFechaHora2";
            this.lblFechaHora2.Size = new System.Drawing.Size(0, 13);
            this.lblFechaHora2.TabIndex = 42;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(407, 320);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(30, 13);
            this.label7.TabIndex = 41;
            this.label7.Text = "Hora";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(407, 272);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(37, 13);
            this.label8.TabIndex = 40;
            this.label8.Text = "Fecha";
            // 
            // dtpHora2
            // 
            this.dtpHora2.CustomFormat = "HH:mm";
            this.dtpHora2.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpHora2.Location = new System.Drawing.Point(410, 336);
            this.dtpHora2.MinDate = new System.DateTime(2024, 6, 4, 0, 0, 0, 0);
            this.dtpHora2.Name = "dtpHora2";
            this.dtpHora2.Size = new System.Drawing.Size(200, 20);
            this.dtpHora2.TabIndex = 39;
            // 
            // dtpFecha2
            // 
            this.dtpFecha2.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFecha2.Location = new System.Drawing.Point(410, 288);
            this.dtpFecha2.Name = "dtpFecha2";
            this.dtpFecha2.Size = new System.Drawing.Size(200, 20);
            this.dtpFecha2.TabIndex = 38;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(407, 18);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(85, 13);
            this.label9.TabIndex = 45;
            this.label9.Text = "Inicio del evento";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(407, 245);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(71, 13);
            this.label10.TabIndex = 46;
            this.label10.Text = "fin del evento";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(281, 336);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 35;
            this.label1.Text = "label1";
            this.label1.Visible = false;
            // 
            // AñadirEvento
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(722, 459);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.btnSeleccionarFecha2);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.lblFechaHora2);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.dtpHora2);
            this.Controls.Add(this.dtpFecha2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblError);
            this.Controls.Add(this.txtDescripcion);
            this.Controls.Add(this.lblDescripcion);
            this.Controls.Add(this.btnPublicar);
            this.Controls.Add(this.btnSeleccionarFecha);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblFechaHora);
            this.Controls.Add(this.txtUbicacion);
            this.Controls.Add(this.txtTitulo);
            this.Controls.Add(this.lblHora);
            this.Controls.Add(this.lblFecha);
            this.Controls.Add(this.dtpHora);
            this.Controls.Add(this.dtpFecha);
            this.Controls.Add(this.btnVolver);
            this.Controls.Add(this.btnSeleccionar);
            this.Controls.Add(this.pbxImagen);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblUbicacion);
            this.Controls.Add(this.lblTitulo);
            this.Name = "AñadirEvento";
            this.Text = "AñadirEvento";
            ((System.ComponentModel.ISupportInitialize)(this.pbxImagen)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnPublicar;
        private System.Windows.Forms.Button btnSeleccionarFecha;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblFechaHora;
        private System.Windows.Forms.TextBox txtUbicacion;
        private System.Windows.Forms.TextBox txtTitulo;
        private System.Windows.Forms.Label lblHora;
        private System.Windows.Forms.Label lblFecha;
        private System.Windows.Forms.DateTimePicker dtpHora;
        private System.Windows.Forms.DateTimePicker dtpFecha;
        private System.Windows.Forms.Button btnVolver;
        private System.Windows.Forms.Button btnSeleccionar;
        private System.Windows.Forms.PictureBox pbxImagen;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblUbicacion;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblDescripcion;
        private System.Windows.Forms.TextBox txtDescripcion;
        private System.Windows.Forms.Label lblError;
        private System.Windows.Forms.Button btnSeleccionarFecha2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblFechaHora2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.DateTimePicker dtpHora2;
        private System.Windows.Forms.DateTimePicker dtpFecha2;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label1;
    }
}