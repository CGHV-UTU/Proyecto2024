
namespace Frontend
{
    partial class PaginaDeUsuario
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
            this.PictureBoxUsuario = new System.Windows.Forms.PictureBox();
            this.lblNombre = new System.Windows.Forms.Label();
            this.lblDescripcion = new System.Windows.Forms.Label();
            this.panelPosts = new System.Windows.Forms.Panel();
            this.btnSeguir = new System.Windows.Forms.PictureBox();
            this.pbxChatear = new System.Windows.Forms.PictureBox();
            this.pbxReportar = new System.Windows.Forms.PictureBox();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.txtDescripcion = new System.Windows.Forms.TextBox();
            this.btnConfirmar = new System.Windows.Forms.PictureBox();
            this.pbxImagenEditar = new System.Windows.Forms.PictureBox();
            this.pbxBloquear = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBoxUsuario)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSeguir)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxChatear)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxReportar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnConfirmar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxImagenEditar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxBloquear)).BeginInit();
            this.SuspendLayout();
            // 
            // PictureBoxUsuario
            // 
            this.PictureBoxUsuario.Cursor = System.Windows.Forms.Cursors.Hand;
            this.PictureBoxUsuario.Image = global::Frontend.Properties.Resources.User;
            this.PictureBoxUsuario.Location = new System.Drawing.Point(12, 12);
            this.PictureBoxUsuario.Name = "PictureBoxUsuario";
            this.PictureBoxUsuario.Size = new System.Drawing.Size(120, 120);
            this.PictureBoxUsuario.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PictureBoxUsuario.TabIndex = 23;
            this.PictureBoxUsuario.TabStop = false;
            // 
            // lblNombre
            // 
            this.lblNombre.AutoSize = true;
            this.lblNombre.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNombre.Location = new System.Drawing.Point(154, 22);
            this.lblNombre.Name = "lblNombre";
            this.lblNombre.Size = new System.Drawing.Size(76, 25);
            this.lblNombre.TabIndex = 24;
            this.lblNombre.Text = "label1";
            // 
            // lblDescripcion
            // 
            this.lblDescripcion.AutoSize = true;
            this.lblDescripcion.Location = new System.Drawing.Point(168, 58);
            this.lblDescripcion.Name = "lblDescripcion";
            this.lblDescripcion.Size = new System.Drawing.Size(35, 13);
            this.lblDescripcion.TabIndex = 25;
            this.lblDescripcion.Text = "label2";
            // 
            // panelPosts
            // 
            this.panelPosts.Location = new System.Drawing.Point(12, 138);
            this.panelPosts.Name = "panelPosts";
            this.panelPosts.Size = new System.Drawing.Size(972, 424);
            this.panelPosts.TabIndex = 26;
            // 
            // btnSeguir
            // 
            this.btnSeguir.Image = global::Frontend.Properties.Resources.seguir_removebg_preview;
            this.btnSeguir.Location = new System.Drawing.Point(138, 87);
            this.btnSeguir.Name = "btnSeguir";
            this.btnSeguir.Size = new System.Drawing.Size(188, 41);
            this.btnSeguir.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.btnSeguir.TabIndex = 52;
            this.btnSeguir.TabStop = false;
            this.btnSeguir.Click += new System.EventHandler(this.btnSeguir_Click);
            // 
            // pbxChatear
            // 
            this.pbxChatear.Image = global::Frontend.Properties.Resources.Comunidad;
            this.pbxChatear.Location = new System.Drawing.Point(362, 82);
            this.pbxChatear.Name = "pbxChatear";
            this.pbxChatear.Size = new System.Drawing.Size(50, 50);
            this.pbxChatear.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbxChatear.TabIndex = 53;
            this.pbxChatear.TabStop = false;
            this.pbxChatear.Click += new System.EventHandler(this.pbxChatear_Click);
            // 
            // pbxReportar
            // 
            this.pbxReportar.Image = global::Frontend.Properties.Resources.reportar;
            this.pbxReportar.Location = new System.Drawing.Point(934, 12);
            this.pbxReportar.Name = "pbxReportar";
            this.pbxReportar.Size = new System.Drawing.Size(50, 50);
            this.pbxReportar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbxReportar.TabIndex = 54;
            this.pbxReportar.TabStop = false;
            this.pbxReportar.Click += new System.EventHandler(this.pbxReportar_Click);
            // 
            // txtNombre
            // 
            this.txtNombre.Location = new System.Drawing.Point(159, 22);
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(100, 20);
            this.txtNombre.TabIndex = 55;
            // 
            // txtDescripcion
            // 
            this.txtDescripcion.Location = new System.Drawing.Point(159, 55);
            this.txtDescripcion.Name = "txtDescripcion";
            this.txtDescripcion.Size = new System.Drawing.Size(684, 20);
            this.txtDescripcion.TabIndex = 56;
            // 
            // btnConfirmar
            // 
            this.btnConfirmar.Image = global::Frontend.Properties.Resources.aceptar;
            this.btnConfirmar.Location = new System.Drawing.Point(473, 1);
            this.btnConfirmar.Name = "btnConfirmar";
            this.btnConfirmar.Size = new System.Drawing.Size(50, 50);
            this.btnConfirmar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.btnConfirmar.TabIndex = 57;
            this.btnConfirmar.TabStop = false;
            this.btnConfirmar.Click += new System.EventHandler(this.btnConfirmar_Click);
            // 
            // pbxImagenEditar
            // 
            this.pbxImagenEditar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbxImagenEditar.Image = global::Frontend.Properties.Resources.User;
            this.pbxImagenEditar.Location = new System.Drawing.Point(12, 8);
            this.pbxImagenEditar.Name = "pbxImagenEditar";
            this.pbxImagenEditar.Size = new System.Drawing.Size(120, 120);
            this.pbxImagenEditar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbxImagenEditar.TabIndex = 58;
            this.pbxImagenEditar.TabStop = false;
            this.pbxImagenEditar.Click += new System.EventHandler(this.pbxImagenEditar_Click);
            // 
            // pbxBloquear
            // 
            this.pbxBloquear.Image = global::Frontend.Properties.Resources.salir;
            this.pbxBloquear.Location = new System.Drawing.Point(866, 12);
            this.pbxBloquear.Name = "pbxBloquear";
            this.pbxBloquear.Size = new System.Drawing.Size(50, 50);
            this.pbxBloquear.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbxBloquear.TabIndex = 59;
            this.pbxBloquear.TabStop = false;
            this.pbxBloquear.Click += new System.EventHandler(this.pbxBloquear_Click);
            // 
            // PaginaDeUsuario
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(996, 574);
            this.Controls.Add(this.pbxBloquear);
            this.Controls.Add(this.pbxImagenEditar);
            this.Controls.Add(this.btnConfirmar);
            this.Controls.Add(this.txtDescripcion);
            this.Controls.Add(this.txtNombre);
            this.Controls.Add(this.pbxReportar);
            this.Controls.Add(this.pbxChatear);
            this.Controls.Add(this.btnSeguir);
            this.Controls.Add(this.panelPosts);
            this.Controls.Add(this.lblDescripcion);
            this.Controls.Add(this.lblNombre);
            this.Controls.Add(this.PictureBoxUsuario);
            this.Name = "PaginaDeUsuario";
            this.Text = "PaginaDeUsuario";
            ((System.ComponentModel.ISupportInitialize)(this.PictureBoxUsuario)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSeguir)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxChatear)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxReportar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnConfirmar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxImagenEditar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxBloquear)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox PictureBoxUsuario;
        private System.Windows.Forms.Label lblNombre;
        private System.Windows.Forms.Label lblDescripcion;
        private System.Windows.Forms.Panel panelPosts;
        private System.Windows.Forms.PictureBox btnSeguir;
        private System.Windows.Forms.PictureBox pbxChatear;
        private System.Windows.Forms.PictureBox pbxReportar;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.TextBox txtDescripcion;
        private System.Windows.Forms.PictureBox btnConfirmar;
        private System.Windows.Forms.PictureBox pbxImagenEditar;
        private System.Windows.Forms.PictureBox pbxBloquear;
    }
}