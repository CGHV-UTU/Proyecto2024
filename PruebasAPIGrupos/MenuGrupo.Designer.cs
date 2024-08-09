namespace PruebasAPIGrupos
{
    partial class MenuGrupo
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
            this.cbxMenu = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cbxMenu
            // 
            this.cbxMenu.FormattingEnabled = true;
            this.cbxMenu.Items.AddRange(new object[] {
            "Crear grupo",
            "Eliminar grupo",
            "Ver grupos de un usuario",
            "Modificar grupo"});
            this.cbxMenu.Location = new System.Drawing.Point(94, 33);
            this.cbxMenu.Name = "cbxMenu";
            this.cbxMenu.Size = new System.Drawing.Size(121, 21);
            this.cbxMenu.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(94, 60);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(121, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Aceptar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // MenuGrupo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(290, 150);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.cbxMenu);
            this.Name = "MenuGrupo";
            this.Text = "MenuGrupo";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cbxMenu;
        private System.Windows.Forms.Button button1;
    }
}