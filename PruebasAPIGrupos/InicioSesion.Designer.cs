using System;

namespace PruebasAPIGrupos
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnVerContraseña = new System.Windows.Forms.Button();
            this.btnRegistrarse = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.lblResultado = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtContraseña = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtUsuario = new System.Windows.Forms.TextBox();
            this.btnIniciarSesion = new System.Windows.Forms.Button();
            this.btnMenuGrupos = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnVerContraseña
            // 
            this.btnVerContraseña.Location = new System.Drawing.Point(211, 50);
            this.btnVerContraseña.Name = "btnVerContraseña";
            this.btnVerContraseña.Size = new System.Drawing.Size(28, 20);
            this.btnVerContraseña.TabIndex = 33;
            this.btnVerContraseña.Text = "👁️";
            this.btnVerContraseña.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnVerContraseña.UseVisualStyleBackColor = true;
            this.btnVerContraseña.Click += new System.EventHandler(this.btnVerContraseña_Click);
            // 
            // btnRegistrarse
            // 
            this.btnRegistrarse.Location = new System.Drawing.Point(96, 170);
            this.btnRegistrarse.Name = "btnRegistrarse";
            this.btnRegistrarse.Size = new System.Drawing.Size(90, 23);
            this.btnRegistrarse.TabIndex = 32;
            this.btnRegistrarse.Text = "Registrarse";
            this.btnRegistrarse.UseVisualStyleBackColor = true;
            this.btnRegistrarse.Click += new System.EventHandler(this.btnRegistrarse_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 141);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(121, 13);
            this.label3.TabIndex = 31;
            this.label3.Text = "¿No tienes una cuenta?";
            // 
            // lblResultado
            // 
            this.lblResultado.AutoSize = true;
            this.lblResultado.Location = new System.Drawing.Point(12, 225);
            this.lblResultado.Name = "lblResultado";
            this.lblResultado.Size = new System.Drawing.Size(61, 13);
            this.lblResultado.TabIndex = 30;
            this.lblResultado.Text = "Resultado: ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 13);
            this.label2.TabIndex = 29;
            this.label2.Text = "Contraseña";
            // 
            // txtContraseña
            // 
            this.txtContraseña.Location = new System.Drawing.Point(85, 50);
            this.txtContraseña.Name = "txtContraseña";
            this.txtContraseña.Size = new System.Drawing.Size(120, 20);
            this.txtContraseña.TabIndex = 28;
            this.txtContraseña.UseSystemPasswordChar = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 27;
            this.label1.Text = "Nombre";
            // 
            // txtUsuario
            // 
            this.txtUsuario.Location = new System.Drawing.Point(85, 5);
            this.txtUsuario.Name = "txtUsuario";
            this.txtUsuario.Size = new System.Drawing.Size(120, 20);
            this.txtUsuario.TabIndex = 26;
            // 
            // btnIniciarSesion
            // 
            this.btnIniciarSesion.Location = new System.Drawing.Point(96, 89);
            this.btnIniciarSesion.Name = "btnIniciarSesion";
            this.btnIniciarSesion.Size = new System.Drawing.Size(90, 23);
            this.btnIniciarSesion.TabIndex = 25;
            this.btnIniciarSesion.Text = "Iniciar Sesión";
            this.btnIniciarSesion.UseVisualStyleBackColor = true;
            this.btnIniciarSesion.Click += new System.EventHandler(this.btnIniciarSesion_Click);
            // 
            // btnMenuGrupos
            // 
            this.btnMenuGrupos.Location = new System.Drawing.Point(42, 262);
            this.btnMenuGrupos.Name = "btnMenuGrupos";
            this.btnMenuGrupos.Size = new System.Drawing.Size(197, 23);
            this.btnMenuGrupos.TabIndex = 34;
            this.btnMenuGrupos.Text = "Menú de grupos";
            this.btnMenuGrupos.UseVisualStyleBackColor = true;
            this.btnMenuGrupos.Click += new System.EventHandler(this.btnMenuGrupos_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnMenuGrupos);
            this.Controls.Add(this.btnVerContraseña);
            this.Controls.Add(this.btnRegistrarse);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblResultado);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtContraseña);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtUsuario);
            this.Controls.Add(this.btnIniciarSesion);
            this.Name = "Form1";
            this.Text = "LOGIN para API  grupos";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void btnIniciarSesion_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        #endregion

        private System.Windows.Forms.Button btnVerContraseña;
        private System.Windows.Forms.Button btnRegistrarse;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblResultado;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtContraseña;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtUsuario;
        private System.Windows.Forms.Button btnIniciarSesion;
        private System.Windows.Forms.Button btnMenuGrupos;
    }
}

