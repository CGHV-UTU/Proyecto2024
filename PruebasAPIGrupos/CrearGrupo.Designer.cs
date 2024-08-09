namespace PruebasAPIGrupos
{
    partial class Form2
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
            this.lblNombre = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.txtDescripcion = new System.Windows.Forms.TextBox();
            this.txtImagen = new System.Windows.Forms.TextBox();
            this.rbtnTodosHablan = new System.Windows.Forms.RadioButton();
            this.rbtnAdminHabla = new System.Windows.Forms.RadioButton();
            this.btnCrear = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblNombre
            // 
            this.lblNombre.AutoSize = true;
            this.lblNombre.Location = new System.Drawing.Point(23, 36);
            this.lblNombre.Name = "lblNombre";
            this.lblNombre.Size = new System.Drawing.Size(44, 13);
            this.lblNombre.TabIndex = 0;
            this.lblNombre.Text = "Nombre";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(23, 62);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Descripción";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(23, 163);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(92, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "URL de la imagen";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(23, 206);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(72, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Configuración";
            // 
            // txtNombre
            // 
            this.txtNombre.Location = new System.Drawing.Point(127, 36);
            this.txtNombre.MaxLength = 100;
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(193, 20);
            this.txtNombre.TabIndex = 4;
            // 
            // txtDescripcion
            // 
            this.txtDescripcion.Location = new System.Drawing.Point(127, 62);
            this.txtDescripcion.MaxLength = 255;
            this.txtDescripcion.Multiline = true;
            this.txtDescripcion.Name = "txtDescripcion";
            this.txtDescripcion.Size = new System.Drawing.Size(193, 92);
            this.txtDescripcion.TabIndex = 5;
            // 
            // txtImagen
            // 
            this.txtImagen.Location = new System.Drawing.Point(127, 160);
            this.txtImagen.MaxLength = 200;
            this.txtImagen.Name = "txtImagen";
            this.txtImagen.Size = new System.Drawing.Size(193, 20);
            this.txtImagen.TabIndex = 6;
            // 
            // rbtnTodosHablan
            // 
            this.rbtnTodosHablan.AutoSize = true;
            this.rbtnTodosHablan.Location = new System.Drawing.Point(115, 202);
            this.rbtnTodosHablan.Name = "rbtnTodosHablan";
            this.rbtnTodosHablan.Size = new System.Drawing.Size(205, 17);
            this.rbtnTodosHablan.TabIndex = 7;
            this.rbtnTodosHablan.TabStop = true;
            this.rbtnTodosHablan.Text = "Todos los participantes pueden hablar";
            this.rbtnTodosHablan.UseVisualStyleBackColor = true;
            // 
            // rbtnAdminHabla
            // 
            this.rbtnAdminHabla.AutoSize = true;
            this.rbtnAdminHabla.Location = new System.Drawing.Point(115, 237);
            this.rbtnAdminHabla.Name = "rbtnAdminHabla";
            this.rbtnAdminHabla.Size = new System.Drawing.Size(209, 17);
            this.rbtnAdminHabla.TabIndex = 8;
            this.rbtnAdminHabla.TabStop = true;
            this.rbtnAdminHabla.Text = "Sólo los administradores pueden hablar";
            this.rbtnAdminHabla.UseVisualStyleBackColor = true;
            // 
            // btnCrear
            // 
            this.btnCrear.Location = new System.Drawing.Point(26, 289);
            this.btnCrear.Name = "btnCrear";
            this.btnCrear.Size = new System.Drawing.Size(294, 23);
            this.btnCrear.TabIndex = 9;
            this.btnCrear.Text = "Crear grupo";
            this.btnCrear.UseVisualStyleBackColor = true;
            this.btnCrear.Click += new System.EventHandler(this.btnCrear_Click);
            // 
            // Form2
            // 
            this.ClientSize = new System.Drawing.Size(523, 318);
            this.Controls.Add(this.btnCrear);
            this.Controls.Add(this.rbtnAdminHabla);
            this.Controls.Add(this.rbtnTodosHablan);
            this.Controls.Add(this.txtImagen);
            this.Controls.Add(this.txtDescripcion);
            this.Controls.Add(this.txtNombre);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblNombre);
            this.Name = "Form2";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

       
        private System.Windows.Forms.Label lblNombre;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.TextBox txtDescripcion;
        private System.Windows.Forms.TextBox txtImagen;
        private System.Windows.Forms.RadioButton rbtnTodosHablan;
        private System.Windows.Forms.RadioButton rbtnAdminHabla;
        private System.Windows.Forms.Button btnCrear;
    }
}