
namespace ConexionDB
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
            this.btnGuardar = new System.Windows.Forms.Button();
            this.btnCargarTabla = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(51, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Tabla a editar:";
            // 
            // cbxTabla
            // 
            this.cbxTabla.FormattingEnabled = true;
            this.cbxTabla.Items.AddRange(new object[] {
            "Post",
            "Evento",
            "Comentario"});
            this.cbxTabla.Location = new System.Drawing.Point(149, 49);
            this.cbxTabla.Name = "cbxTabla";
            this.cbxTabla.Size = new System.Drawing.Size(121, 21);
            this.cbxTabla.TabIndex = 3;
            // 
            // button9
            // 
            this.button9.BackColor = System.Drawing.Color.Red;
            this.button9.ForeColor = System.Drawing.Color.White;
            this.button9.Location = new System.Drawing.Point(2, 2);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(56, 23);
            this.button9.TabIndex = 13;
            this.button9.Text = "🏃 Salir";
            this.button9.UseVisualStyleBackColor = false;
            this.button9.Click += new System.EventHandler(this.button9_Click);
            // 
            // btnGuardar
            // 
            this.btnGuardar.ForeColor = System.Drawing.Color.Black;
            this.btnGuardar.Location = new System.Drawing.Point(64, 2);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(87, 22);
            this.btnGuardar.TabIndex = 14;
            this.btnGuardar.Text = "💾 Guardar";
            this.btnGuardar.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnGuardar.UseVisualStyleBackColor = true;
            // 
            // btnCargarTabla
            // 
            this.btnCargarTabla.Location = new System.Drawing.Point(54, 76);
            this.btnCargarTabla.Name = "btnCargarTabla";
            this.btnCargarTabla.Size = new System.Drawing.Size(216, 23);
            this.btnCargarTabla.TabIndex = 15;
            this.btnCargarTabla.Text = "📂 Cargar ";
            this.btnCargarTabla.UseVisualStyleBackColor = true;
            this.btnCargarTabla.Click += new System.EventHandler(this.button6_Click);
            // 
            // Principal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(325, 129);
            this.Controls.Add(this.btnCargarTabla);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.button9);
            this.Controls.Add(this.cbxTabla);
            this.Controls.Add(this.label1);
            this.Name = "Principal";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbxTabla;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Button btnCargarTabla;
    }
}

