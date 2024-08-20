
namespace BackofficeDeAdministracion
{
    partial class AgregarAdmin
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
            this.label2 = new System.Windows.Forms.Label();
            this.txtPassE = new System.Windows.Forms.TextBox();
            this.txtUserE = new System.Windows.Forms.TextBox();
            this.btnRegistrar = new System.Windows.Forms.Button();
            this.lblCont = new System.Windows.Forms.Label();
            this.lblNom = new System.Windows.Forms.Label();
            this.lblAgregado = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(100, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(184, 13);
            this.label2.TabIndex = 33;
            this.label2.Text = "Registrar Administrador en Backoffice";
            // 
            // txtPassE
            // 
            this.txtPassE.Location = new System.Drawing.Point(167, 169);
            this.txtPassE.Name = "txtPassE";
            this.txtPassE.Size = new System.Drawing.Size(143, 20);
            this.txtPassE.TabIndex = 32;
            // 
            // txtUserE
            // 
            this.txtUserE.Location = new System.Drawing.Point(167, 117);
            this.txtUserE.Name = "txtUserE";
            this.txtUserE.Size = new System.Drawing.Size(143, 20);
            this.txtUserE.TabIndex = 31;
            // 
            // btnRegistrar
            // 
            this.btnRegistrar.Location = new System.Drawing.Point(177, 217);
            this.btnRegistrar.Name = "btnRegistrar";
            this.btnRegistrar.Size = new System.Drawing.Size(117, 23);
            this.btnRegistrar.TabIndex = 30;
            this.btnRegistrar.Text = "Registrar";
            this.btnRegistrar.UseVisualStyleBackColor = true;
            this.btnRegistrar.Click += new System.EventHandler(this.btnRegistrar_Click);
            // 
            // lblCont
            // 
            this.lblCont.AutoSize = true;
            this.lblCont.Location = new System.Drawing.Point(54, 169);
            this.lblCont.Name = "lblCont";
            this.lblCont.Size = new System.Drawing.Size(61, 13);
            this.lblCont.TabIndex = 29;
            this.lblCont.Text = "Contraseña";
            // 
            // lblNom
            // 
            this.lblNom.AutoSize = true;
            this.lblNom.Location = new System.Drawing.Point(54, 117);
            this.lblNom.Name = "lblNom";
            this.lblNom.Size = new System.Drawing.Size(100, 13);
            this.lblNom.TabIndex = 28;
            this.lblNom.Text = "Nombre De Usuario";
            // 
            // lblAgregado
            // 
            this.lblAgregado.AutoSize = true;
            this.lblAgregado.Location = new System.Drawing.Point(312, 222);
            this.lblAgregado.Name = "lblAgregado";
            this.lblAgregado.Size = new System.Drawing.Size(161, 13);
            this.lblAgregado.TabIndex = 34;
            this.lblAgregado.Text = "Usuario Agregado Corectamente";
            this.lblAgregado.Visible = false;
            // 
            // AgregarAdmin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(509, 311);
            this.Controls.Add(this.lblAgregado);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtPassE);
            this.Controls.Add(this.txtUserE);
            this.Controls.Add(this.btnRegistrar);
            this.Controls.Add(this.lblCont);
            this.Controls.Add(this.lblNom);
            this.Name = "AgregarAdmin";
            this.Text = "AgregarAdmin";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtPassE;
        private System.Windows.Forms.TextBox txtUserE;
        private System.Windows.Forms.Button btnRegistrar;
        private System.Windows.Forms.Label lblCont;
        private System.Windows.Forms.Label lblNom;
        private System.Windows.Forms.Label lblAgregado;
    }
}