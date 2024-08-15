
namespace Frontend
{
    partial class IniciarSesion
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
            System.Windows.Forms.PictureBox pcbxLogo;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IniciarSesion));
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtContraseña = new System.Windows.Forms.TextBox();
            this.txtUsuario = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.lblOlvidarContraseña = new System.Windows.Forms.Label();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pcbxVerContraseña = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.btnIniciarSesion = new System.Windows.Forms.PictureBox();
            this.btnRegistrar = new System.Windows.Forms.PictureBox();
            pcbxLogo = new System.Windows.Forms.PictureBox();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(pcbxLogo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcbxVerContraseña)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnIniciarSesion)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnRegistrar)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.SlateBlue;
            this.panel1.Location = new System.Drawing.Point(55, 316);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(363, 3);
            this.panel1.TabIndex = 60;
            // 
            // txtContraseña
            // 
            this.txtContraseña.BackColor = System.Drawing.Color.White;
            this.txtContraseña.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtContraseña.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtContraseña.ForeColor = System.Drawing.Color.Gray;
            this.txtContraseña.Location = new System.Drawing.Point(125, 288);
            this.txtContraseña.MaxLength = 20;
            this.txtContraseña.Name = "txtContraseña";
            this.txtContraseña.Size = new System.Drawing.Size(224, 22);
            this.txtContraseña.TabIndex = 53;
            this.txtContraseña.Text = "Contraseña";
            this.txtContraseña.Enter += new System.EventHandler(this.txtContraseña_Enter_1);
            this.txtContraseña.Leave += new System.EventHandler(this.txtContraseña_Leave_1);
            // 
            // txtUsuario
            // 
            this.txtUsuario.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtUsuario.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUsuario.ForeColor = System.Drawing.Color.Gray;
            this.txtUsuario.Location = new System.Drawing.Point(125, 196);
            this.txtUsuario.MaxLength = 20;
            this.txtUsuario.Name = "txtUsuario";
            this.txtUsuario.Size = new System.Drawing.Size(224, 22);
            this.txtUsuario.TabIndex = 52;
            this.txtUsuario.Text = "Usuario";
            this.txtUsuario.Enter += new System.EventHandler(this.txtUsuario_Enter_1);
            this.txtUsuario.Leave += new System.EventHandler(this.txtUsuario_Leave);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Gray;
            this.label3.Location = new System.Drawing.Point(147, 297);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(163, 18);
            this.label3.TabIndex = 54;
            this.label3.Text = "¿No tienes una cuenta?";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.SlateBlue;
            this.panel2.Location = new System.Drawing.Point(55, 222);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(363, 3);
            this.panel2.TabIndex = 61;
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.lblOlvidarContraseña);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Controls.Add(this.btnIniciarSesion);
            this.panel3.Controls.Add(this.btnRegistrar);
            this.panel3.Location = new System.Drawing.Point(12, 157);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(460, 428);
            this.panel3.TabIndex = 62;
            // 
            // lblOlvidarContraseña
            // 
            this.lblOlvidarContraseña.AutoSize = true;
            this.lblOlvidarContraseña.BackColor = System.Drawing.Color.White;
            this.lblOlvidarContraseña.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOlvidarContraseña.ForeColor = System.Drawing.Color.RoyalBlue;
            this.lblOlvidarContraseña.Location = new System.Drawing.Point(58, 176);
            this.lblOlvidarContraseña.Name = "lblOlvidarContraseña";
            this.lblOlvidarContraseña.Size = new System.Drawing.Size(179, 18);
            this.lblOlvidarContraseña.TabIndex = 59;
            this.lblOlvidarContraseña.Text = "¿Olvidaste tu contraseña?";
            this.lblOlvidarContraseña.Click += new System.EventHandler(this.lblOlvidarContraseña_Click);
            // 
            // pcbxLogo
            // 
            pcbxLogo.Image = global::Frontend.Properties.Resources.Logo_Infini;
            pcbxLogo.Location = new System.Drawing.Point(145, 5);
            pcbxLogo.Name = "pcbxLogo";
            pcbxLogo.Size = new System.Drawing.Size(218, 146);
            pcbxLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            pcbxLogo.TabIndex = 63;
            pcbxLogo.TabStop = false;
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = global::Frontend.Properties.Resources.Perfil;
            this.pictureBox3.Location = new System.Drawing.Point(74, 182);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(45, 36);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox3.TabIndex = 55;
            this.pictureBox3.TabStop = false;
            // 
            // pcbxVerContraseña
            // 
            this.pcbxVerContraseña.Image = global::Frontend.Properties.Resources.ver_contraseña_removebg_preview;
            this.pcbxVerContraseña.Location = new System.Drawing.Point(367, 274);
            this.pcbxVerContraseña.Name = "pcbxVerContraseña";
            this.pcbxVerContraseña.Size = new System.Drawing.Size(41, 36);
            this.pcbxVerContraseña.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pcbxVerContraseña.TabIndex = 59;
            this.pcbxVerContraseña.TabStop = false;
            this.pcbxVerContraseña.Click += new System.EventHandler(this.pcbxVerContraseña_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::Frontend.Properties.Resources.Contraseña_2_removebg_preview;
            this.pictureBox2.Location = new System.Drawing.Point(74, 274);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(46, 36);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 56;
            this.pictureBox2.TabStop = false;
            // 
            // btnIniciarSesion
            // 
            this.btnIniciarSesion.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnIniciarSesion.Image = global::Frontend.Properties.Resources.inciar_sesion_removebg_preview;
            this.btnIniciarSesion.Location = new System.Drawing.Point(132, 176);
            this.btnIniciarSesion.Name = "btnIniciarSesion";
            this.btnIniciarSesion.Size = new System.Drawing.Size(194, 139);
            this.btnIniciarSesion.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnIniciarSesion.TabIndex = 57;
            this.btnIniciarSesion.TabStop = false;
            this.btnIniciarSesion.Click += new System.EventHandler(this.btnIniciarSesion_Click_1);
            // 
            // btnRegistrar
            // 
            this.btnRegistrar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRegistrar.Image = global::Frontend.Properties.Resources.Registrarse_removebg_preview;
            this.btnRegistrar.Location = new System.Drawing.Point(132, 278);
            this.btnRegistrar.Name = "btnRegistrar";
            this.btnRegistrar.Size = new System.Drawing.Size(194, 139);
            this.btnRegistrar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnRegistrar.TabIndex = 58;
            this.btnRegistrar.TabStop = false;
            this.btnRegistrar.Click += new System.EventHandler(this.btnRegistrar_Click_1);
            // 
            // IniciarSesion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(484, 600);
            this.Controls.Add(this.panel2);
            this.Controls.Add(pcbxLogo);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pcbxVerContraseña);
            this.Controls.Add(this.txtUsuario);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.txtContraseña);
            this.Controls.Add(this.panel3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "IniciarSesion";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Iniciar sesión";
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(pcbxLogo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcbxVerContraseña)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnIniciarSesion)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnRegistrar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox btnIniciarSesion;
        private System.Windows.Forms.TextBox txtContraseña;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox btnRegistrar;
        private System.Windows.Forms.TextBox txtUsuario;
        private System.Windows.Forms.PictureBox pcbxVerContraseña;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label lblOlvidarContraseña;
    }
}