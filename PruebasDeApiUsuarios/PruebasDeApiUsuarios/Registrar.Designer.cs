
namespace PruebasDeApiUsuarios
{
    partial class Registrar
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
            this.btnRegistrar = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtNombreCuenta = new System.Windows.Forms.TextBox();
            this.txtNombreVisible = new System.Windows.Forms.TextBox();
            this.pcbxFotoPerfil = new System.Windows.Forms.PictureBox();
            this.btnSubirImagen = new System.Windows.Forms.Button();
            this.cbxGenero = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtDescripcion = new System.Windows.Forms.TextBox();
            this.txtConfirmarContraseña = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtContraseña = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.lblResultado = new System.Windows.Forms.Label();
            this.dtpNacimiento = new System.Windows.Forms.DateTimePicker();
            this.label9 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pcbxFotoPerfil)).BeginInit();
            this.SuspendLayout();
            // 
            // btnRegistrar
            // 
            this.btnRegistrar.Location = new System.Drawing.Point(12, 470);
            this.btnRegistrar.Name = "btnRegistrar";
            this.btnRegistrar.Size = new System.Drawing.Size(243, 23);
            this.btnRegistrar.TabIndex = 0;
            this.btnRegistrar.Text = "Registrarse";
            this.btnRegistrar.UseVisualStyleBackColor = true;
            this.btnRegistrar.Click += new System.EventHandler(this.btnRegistrar_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 144);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Nombre de cuenta";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 170);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Nombre visible";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 196);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(63, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Descripción";
            // 
            // txtNombreCuenta
            // 
            this.txtNombreCuenta.Location = new System.Drawing.Point(129, 141);
            this.txtNombreCuenta.Name = "txtNombreCuenta";
            this.txtNombreCuenta.Size = new System.Drawing.Size(126, 20);
            this.txtNombreCuenta.TabIndex = 5;
            // 
            // txtNombreVisible
            // 
            this.txtNombreVisible.Location = new System.Drawing.Point(129, 167);
            this.txtNombreVisible.Name = "txtNombreVisible";
            this.txtNombreVisible.Size = new System.Drawing.Size(126, 20);
            this.txtNombreVisible.TabIndex = 6;
            // 
            // pcbxFotoPerfil
            // 
            this.pcbxFotoPerfil.Location = new System.Drawing.Point(71, 259);
            this.pcbxFotoPerfil.Name = "pcbxFotoPerfil";
            this.pcbxFotoPerfil.Size = new System.Drawing.Size(126, 117);
            this.pcbxFotoPerfil.TabIndex = 9;
            this.pcbxFotoPerfil.TabStop = false;
            // 
            // btnSubirImagen
            // 
            this.btnSubirImagen.Location = new System.Drawing.Point(91, 308);
            this.btnSubirImagen.Name = "btnSubirImagen";
            this.btnSubirImagen.Size = new System.Drawing.Size(92, 23);
            this.btnSubirImagen.TabIndex = 10;
            this.btnSubirImagen.Text = "Subir imagen";
            this.btnSubirImagen.UseVisualStyleBackColor = true;
            // 
            // cbxGenero
            // 
            this.cbxGenero.FormattingEnabled = true;
            this.cbxGenero.Items.AddRange(new object[] {
            "Hombre",
            "Mujer",
            "Otro",
            "Prefiero no decirlo"});
            this.cbxGenero.Location = new System.Drawing.Point(129, 443);
            this.cbxGenero.Name = "cbxGenero";
            this.cbxGenero.Size = new System.Drawing.Size(126, 21);
            this.cbxGenero.TabIndex = 11;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 232);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(68, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "Foto de perfil";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(13, 454);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(42, 13);
            this.label6.TabIndex = 13;
            this.label6.Text = "Género";
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(129, 62);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(126, 20);
            this.txtEmail.TabIndex = 15;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 65);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 13);
            this.label3.TabIndex = 14;
            this.label3.Text = "E-mail";
            // 
            // txtDescripcion
            // 
            this.txtDescripcion.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDescripcion.Location = new System.Drawing.Point(129, 193);
            this.txtDescripcion.Name = "txtDescripcion";
            this.txtDescripcion.Size = new System.Drawing.Size(126, 20);
            this.txtDescripcion.TabIndex = 8;
            // 
            // txtConfirmarContraseña
            // 
            this.txtConfirmarContraseña.Location = new System.Drawing.Point(129, 114);
            this.txtConfirmarContraseña.Name = "txtConfirmarContraseña";
            this.txtConfirmarContraseña.Size = new System.Drawing.Size(126, 20);
            this.txtConfirmarContraseña.TabIndex = 17;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(13, 117);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(107, 13);
            this.label7.TabIndex = 16;
            this.label7.Text = "Confirmar contraseña";
            // 
            // txtContraseña
            // 
            this.txtContraseña.Location = new System.Drawing.Point(129, 88);
            this.txtContraseña.Name = "txtContraseña";
            this.txtContraseña.Size = new System.Drawing.Size(126, 20);
            this.txtContraseña.TabIndex = 19;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(13, 91);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(61, 13);
            this.label8.TabIndex = 18;
            this.label8.Text = "Contraseña";
            // 
            // lblResultado
            // 
            this.lblResultado.AutoSize = true;
            this.lblResultado.Location = new System.Drawing.Point(9, 496);
            this.lblResultado.Name = "lblResultado";
            this.lblResultado.Size = new System.Drawing.Size(61, 13);
            this.lblResultado.TabIndex = 20;
            this.lblResultado.Text = "Resultado: ";
            // 
            // dtpNacimiento
            // 
            this.dtpNacimiento.Location = new System.Drawing.Point(129, 396);
            this.dtpNacimiento.Name = "dtpNacimiento";
            this.dtpNacimiento.Size = new System.Drawing.Size(126, 20);
            this.dtpNacimiento.TabIndex = 21;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(17, 402);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(106, 13);
            this.label9.TabIndex = 22;
            this.label9.Text = "Fecha de nacimiento";
            // 
            // Registrar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(272, 518);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.dtpNacimiento);
            this.Controls.Add(this.lblResultado);
            this.Controls.Add(this.txtContraseña);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtConfirmarContraseña);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cbxGenero);
            this.Controls.Add(this.btnSubirImagen);
            this.Controls.Add(this.pcbxFotoPerfil);
            this.Controls.Add(this.txtDescripcion);
            this.Controls.Add(this.txtNombreVisible);
            this.Controls.Add(this.txtNombreCuenta);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnRegistrar);
            this.Name = "Registrar";
            this.Text = "Registrar";
            ((System.ComponentModel.ISupportInitialize)(this.pcbxFotoPerfil)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnRegistrar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtNombreCuenta;
        private System.Windows.Forms.TextBox txtNombreVisible;
        private System.Windows.Forms.PictureBox pcbxFotoPerfil;
        private System.Windows.Forms.Button btnSubirImagen;
        private System.Windows.Forms.ComboBox cbxGenero;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtDescripcion;
        private System.Windows.Forms.TextBox txtConfirmarContraseña;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtContraseña;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblResultado;
        private System.Windows.Forms.DateTimePicker dtpNacimiento;
        private System.Windows.Forms.Label label9;
    }
}