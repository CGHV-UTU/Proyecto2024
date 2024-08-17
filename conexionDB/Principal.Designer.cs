
namespace BackofficeDeAdministracion
{
    partial class Principal
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
            this.label1 = new System.Windows.Forms.Label();
            this.cbxTabla = new System.Windows.Forms.ComboBox();
            this.button9 = new System.Windows.Forms.Button();
            this.btnCargarTabla = new System.Windows.Forms.Button();
            this.lblUsuarioBackoffice = new System.Windows.Forms.Label();
            this.btnGestionAdmin = new System.Windows.Forms.Button();
            this.btnCargarReportes = new System.Windows.Forms.Button();
            this.cbxReportes = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(44, 63);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(147, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Gestión de contenido:";
            // 
            // cbxTabla
            // 
            this.cbxTabla.FormattingEnabled = true;
            this.cbxTabla.Items.AddRange(new object[] {
            "Post",
            "Evento",
            "Comentario",
            "Usuario",
            "Grupo"});
            this.cbxTabla.Location = new System.Drawing.Point(199, 60);
            this.cbxTabla.Margin = new System.Windows.Forms.Padding(4);
            this.cbxTabla.Name = "cbxTabla";
            this.cbxTabla.Size = new System.Drawing.Size(160, 24);
            this.cbxTabla.TabIndex = 3;
            // 
            // button9
            // 
            this.button9.BackColor = System.Drawing.Color.Red;
            this.button9.ForeColor = System.Drawing.Color.White;
            this.button9.Location = new System.Drawing.Point(3, 2);
            this.button9.Margin = new System.Windows.Forms.Padding(4);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(75, 28);
            this.button9.TabIndex = 13;
            this.button9.Text = "🏃 Salir";
            this.button9.UseVisualStyleBackColor = false;
            this.button9.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // btnCargarTabla
            // 
            this.btnCargarTabla.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCargarTabla.Location = new System.Drawing.Point(72, 94);
            this.btnCargarTabla.Margin = new System.Windows.Forms.Padding(4);
            this.btnCargarTabla.Name = "btnCargarTabla";
            this.btnCargarTabla.Size = new System.Drawing.Size(288, 28);
            this.btnCargarTabla.TabIndex = 15;
            this.btnCargarTabla.Text = "📂 Cargar ";
            this.btnCargarTabla.UseVisualStyleBackColor = true;
            this.btnCargarTabla.Click += new System.EventHandler(this.btnCargar_Click);
            // 
            // lblUsuarioBackoffice
            // 
            this.lblUsuarioBackoffice.AutoSize = true;
            this.lblUsuarioBackoffice.Location = new System.Drawing.Point(303, 9);
            this.lblUsuarioBackoffice.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblUsuarioBackoffice.Name = "lblUsuarioBackoffice";
            this.lblUsuarioBackoffice.Size = new System.Drawing.Size(57, 17);
            this.lblUsuarioBackoffice.TabIndex = 16;
            this.lblUsuarioBackoffice.Text = "Usuario";
            // 
            // btnGestionAdmin
            // 
            this.btnGestionAdmin.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGestionAdmin.Location = new System.Drawing.Point(12, 234);
            this.btnGestionAdmin.Name = "btnGestionAdmin";
            this.btnGestionAdmin.Size = new System.Drawing.Size(409, 23);
            this.btnGestionAdmin.TabIndex = 17;
            this.btnGestionAdmin.Text = "Gestión de Administradores";
            this.btnGestionAdmin.UseVisualStyleBackColor = true;
            this.btnGestionAdmin.Click += new System.EventHandler(this.btnGestionAdmin_Click);
            // 
            // btnCargarReportes
            // 
            this.btnCargarReportes.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCargarReportes.Location = new System.Drawing.Point(72, 183);
            this.btnCargarReportes.Margin = new System.Windows.Forms.Padding(4);
            this.btnCargarReportes.Name = "btnCargarReportes";
            this.btnCargarReportes.Size = new System.Drawing.Size(288, 28);
            this.btnCargarReportes.TabIndex = 20;
            this.btnCargarReportes.Text = "📂 Cargar ";
            this.btnCargarReportes.UseVisualStyleBackColor = true;
            this.btnCargarReportes.Click += new System.EventHandler(this.btnCargarReportes_Click);
            // 
            // cbxReportes
            // 
            this.cbxReportes.FormattingEnabled = true;
            this.cbxReportes.Items.AddRange(new object[] {
            "Post",
            "Evento",
            "Comentario",
            "Usuario",
            "Grupo"});
            this.cbxReportes.Location = new System.Drawing.Point(199, 149);
            this.cbxReportes.Margin = new System.Windows.Forms.Padding(4);
            this.cbxReportes.Name = "cbxReportes";
            this.cbxReportes.Size = new System.Drawing.Size(160, 24);
            this.cbxReportes.TabIndex = 19;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(44, 152);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(139, 17);
            this.label2.TabIndex = 18;
            this.label2.Text = "Gestión de Reportes";
            // 
            // Principal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(433, 269);
            this.Controls.Add(this.btnCargarReportes);
            this.Controls.Add(this.cbxReportes);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnGestionAdmin);
            this.Controls.Add(this.lblUsuarioBackoffice);
            this.Controls.Add(this.btnCargarTabla);
            this.Controls.Add(this.button9);
            this.Controls.Add(this.cbxTabla);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Principal";
            this.Text = "Backoffice";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbxTabla;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.Button btnCargarTabla;
        private System.Windows.Forms.Label lblUsuarioBackoffice;
        private System.Windows.Forms.Button btnGestionAdmin;
        private System.Windows.Forms.Button btnCargarReportes;
        private System.Windows.Forms.ComboBox cbxReportes;
        private System.Windows.Forms.Label label2;
    }
}

