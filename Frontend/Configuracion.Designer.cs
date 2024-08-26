
namespace Frontend
{
    partial class Configuracion
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
            this.label1 = new System.Windows.Forms.Label();
            this.cbxModo = new System.Windows.Forms.ComboBox();
            this.cbxIdioma = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnCambiar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(44, 76);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Modo";
            // 
            // cbxModo
            // 
            this.cbxModo.FormattingEnabled = true;
            this.cbxModo.Items.AddRange(new object[] {
            "Claro",
            "Oscuro"});
            this.cbxModo.Location = new System.Drawing.Point(159, 76);
            this.cbxModo.Name = "cbxModo";
            this.cbxModo.Size = new System.Drawing.Size(121, 21);
            this.cbxModo.TabIndex = 2;
            // 
            // cbxIdioma
            // 
            this.cbxIdioma.FormattingEnabled = true;
            this.cbxIdioma.Items.AddRange(new object[] {
            "Español",
            "Inglés"});
            this.cbxIdioma.Location = new System.Drawing.Point(159, 131);
            this.cbxIdioma.Name = "cbxIdioma";
            this.cbxIdioma.Size = new System.Drawing.Size(121, 21);
            this.cbxIdioma.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(44, 131);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Idioma";
            // 
            // btnCambiar
            // 
            this.btnCambiar.BackColor = System.Drawing.Color.Indigo;
            this.btnCambiar.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnCambiar.Location = new System.Drawing.Point(108, 202);
            this.btnCambiar.Name = "btnCambiar";
            this.btnCambiar.Size = new System.Drawing.Size(133, 23);
            this.btnCambiar.TabIndex = 5;
            this.btnCambiar.Text = "Cambiar configuracion";
            this.btnCambiar.UseVisualStyleBackColor = false;
            this.btnCambiar.Click += new System.EventHandler(this.btnCambiar_Click);
            // 
            // Configuracion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SlateBlue;
            this.ClientSize = new System.Drawing.Size(319, 259);
            this.Controls.Add(this.btnCambiar);
            this.Controls.Add(this.cbxIdioma);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cbxModo);
            this.Controls.Add(this.label1);
            this.Name = "Configuracion";
            this.Text = "Configuracion";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbxModo;
        private System.Windows.Forms.ComboBox cbxIdioma;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnCambiar;
    }
}