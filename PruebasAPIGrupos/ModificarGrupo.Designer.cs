namespace PruebasAPIGrupos
{
    partial class ModificarGrupo
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
            this.btnModificar = new System.Windows.Forms.Button();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.dataGridViewGrupos = new System.Windows.Forms.DataGridView();
            this.clmnNombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmnDescripcion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmnConfig = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.rbtnAdminHabla = new System.Windows.Forms.RadioButton();
            this.rbtnTodosHablan = new System.Windows.Forms.RadioButton();
            this.txtImagen = new System.Windows.Forms.TextBox();
            this.txtDescripcion = new System.Windows.Forms.TextBox();
            this.txtNombreVisible = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblNombre = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewGrupos)).BeginInit();
            this.SuspendLayout();
            // 
            // btnModificar
            // 
            this.btnModificar.Location = new System.Drawing.Point(15, 447);
            this.btnModificar.Name = "btnModificar";
            this.btnModificar.Size = new System.Drawing.Size(348, 23);
            this.btnModificar.TabIndex = 11;
            this.btnModificar.Text = "Modificar";
            this.btnModificar.UseVisualStyleBackColor = true;
            this.btnModificar.Click += new System.EventHandler(this.btnModificar_Click);
            // 
            // btnBuscar
            // 
            this.btnBuscar.Location = new System.Drawing.Point(15, 37);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(348, 23);
            this.btnBuscar.TabIndex = 10;
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.UseVisualStyleBackColor = true;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // txtNombre
            // 
            this.txtNombre.Location = new System.Drawing.Point(125, 11);
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(238, 20);
            this.txtNombre.TabIndex = 9;
            // 
            // dataGridViewGrupos
            // 
            this.dataGridViewGrupos.AllowUserToAddRows = false;
            this.dataGridViewGrupos.AllowUserToDeleteRows = false;
            this.dataGridViewGrupos.AllowUserToResizeRows = false;
            this.dataGridViewGrupos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewGrupos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.clmnNombre,
            this.clmnDescripcion,
            this.clmnConfig});
            this.dataGridViewGrupos.Location = new System.Drawing.Point(15, 66);
            this.dataGridViewGrupos.Name = "dataGridViewGrupos";
            this.dataGridViewGrupos.Size = new System.Drawing.Size(348, 152);
            this.dataGridViewGrupos.TabIndex = 8;
            // 
            // clmnNombre
            // 
            this.clmnNombre.HeaderText = "Nombre";
            this.clmnNombre.Name = "clmnNombre";
            // 
            // clmnDescripcion
            // 
            this.clmnDescripcion.HeaderText = "Descripción";
            this.clmnDescripcion.Name = "clmnDescripcion";
            // 
            // clmnConfig
            // 
            this.clmnConfig.HeaderText = "Configuración";
            this.clmnConfig.Name = "clmnConfig";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Nombre del grupo";
            // 
            // rbtnAdminHabla
            // 
            this.rbtnAdminHabla.AutoSize = true;
            this.rbtnAdminHabla.Location = new System.Drawing.Point(113, 424);
            this.rbtnAdminHabla.Name = "rbtnAdminHabla";
            this.rbtnAdminHabla.Size = new System.Drawing.Size(209, 17);
            this.rbtnAdminHabla.TabIndex = 20;
            this.rbtnAdminHabla.TabStop = true;
            this.rbtnAdminHabla.Text = "Sólo los administradores pueden hablar";
            this.rbtnAdminHabla.UseVisualStyleBackColor = true;
            // 
            // rbtnTodosHablan
            // 
            this.rbtnTodosHablan.AutoSize = true;
            this.rbtnTodosHablan.Location = new System.Drawing.Point(113, 389);
            this.rbtnTodosHablan.Name = "rbtnTodosHablan";
            this.rbtnTodosHablan.Size = new System.Drawing.Size(205, 17);
            this.rbtnTodosHablan.TabIndex = 19;
            this.rbtnTodosHablan.TabStop = true;
            this.rbtnTodosHablan.Text = "Todos los participantes pueden hablar";
            this.rbtnTodosHablan.UseVisualStyleBackColor = true;
            // 
            // txtImagen
            // 
            this.txtImagen.Location = new System.Drawing.Point(125, 347);
            this.txtImagen.MaxLength = 200;
            this.txtImagen.Name = "txtImagen";
            this.txtImagen.Size = new System.Drawing.Size(238, 20);
            this.txtImagen.TabIndex = 18;
            // 
            // txtDescripcion
            // 
            this.txtDescripcion.Location = new System.Drawing.Point(125, 249);
            this.txtDescripcion.MaxLength = 255;
            this.txtDescripcion.Multiline = true;
            this.txtDescripcion.Name = "txtDescripcion";
            this.txtDescripcion.Size = new System.Drawing.Size(238, 92);
            this.txtDescripcion.TabIndex = 17;
            // 
            // txtNombreVisible
            // 
            this.txtNombreVisible.Location = new System.Drawing.Point(125, 224);
            this.txtNombreVisible.MaxLength = 100;
            this.txtNombreVisible.Name = "txtNombreVisible";
            this.txtNombreVisible.Size = new System.Drawing.Size(238, 20);
            this.txtNombreVisible.TabIndex = 16;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(21, 393);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(72, 13);
            this.label4.TabIndex = 15;
            this.label4.Text = "Configuración";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(21, 350);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(92, 13);
            this.label3.TabIndex = 14;
            this.label3.Text = "URL de la imagen";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 249);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "Descripción";
            // 
            // lblNombre
            // 
            this.lblNombre.AutoSize = true;
            this.lblNombre.Location = new System.Drawing.Point(21, 223);
            this.lblNombre.Name = "lblNombre";
            this.lblNombre.Size = new System.Drawing.Size(44, 13);
            this.lblNombre.TabIndex = 12;
            this.lblNombre.Text = "Nombre";
            // 
            // ModificarGrupo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(396, 498);
            this.Controls.Add(this.rbtnAdminHabla);
            this.Controls.Add(this.rbtnTodosHablan);
            this.Controls.Add(this.txtImagen);
            this.Controls.Add(this.txtDescripcion);
            this.Controls.Add(this.txtNombreVisible);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblNombre);
            this.Controls.Add(this.btnModificar);
            this.Controls.Add(this.btnBuscar);
            this.Controls.Add(this.txtNombre);
            this.Controls.Add(this.dataGridViewGrupos);
            this.Controls.Add(this.label1);
            this.Name = "ModificarGrupo";
            this.Text = "Modificar grupo";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewGrupos)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnModificar;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.DataGridView dataGridViewGrupos;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmnNombre;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmnDescripcion;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmnConfig;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton rbtnAdminHabla;
        private System.Windows.Forms.RadioButton rbtnTodosHablan;
        private System.Windows.Forms.TextBox txtImagen;
        private System.Windows.Forms.TextBox txtDescripcion;
        private System.Windows.Forms.TextBox txtNombreVisible;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblNombre;
    }
}