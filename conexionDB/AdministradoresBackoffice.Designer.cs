
namespace BackofficeDeAdministracion
{
    partial class AdministradoresBackoffice
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
            this.button9 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.cbxOpcion = new System.Windows.Forms.ComboBox();
            this.PanelCrear = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.txtPassE = new System.Windows.Forms.TextBox();
            this.txtUserE = new System.Windows.Forms.TextBox();
            this.btnAcceder = new System.Windows.Forms.Button();
            this.lblCont = new System.Windows.Forms.Label();
            this.lblNom = new System.Windows.Forms.Label();
            this.PanelEliminar = new System.Windows.Forms.Panel();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.label6 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.PassE = new System.Windows.Forms.TextBox();
            this.UserE = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.PanelCrear.SuspendLayout();
            this.PanelEliminar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // button9
            // 
            this.button9.BackColor = System.Drawing.Color.Red;
            this.button9.ForeColor = System.Drawing.Color.White;
            this.button9.Location = new System.Drawing.Point(1, 0);
            this.button9.Margin = new System.Windows.Forms.Padding(4);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(93, 32);
            this.button9.TabIndex = 15;
            this.button9.Text = "🏃 Volver";
            this.button9.UseVisualStyleBackColor = false;
            this.button9.Click += new System.EventHandler(this.btnVolver_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(193, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(148, 18);
            this.label1.TabIndex = 16;
            this.label1.Text = "Gestión de Backoffice";
            // 
            // cbxOpcion
            // 
            this.cbxOpcion.FormattingEnabled = true;
            this.cbxOpcion.Items.AddRange(new object[] {
            "Añadir Admin",
            "Eliminar Admin"});
            this.cbxOpcion.Location = new System.Drawing.Point(196, 63);
            this.cbxOpcion.Margin = new System.Windows.Forms.Padding(4);
            this.cbxOpcion.Name = "cbxOpcion";
            this.cbxOpcion.Size = new System.Drawing.Size(145, 24);
            this.cbxOpcion.TabIndex = 17;
            this.cbxOpcion.SelectedIndexChanged += new System.EventHandler(this.cbxOpcion_SelectedIndexChanged);
            // 
            // PanelCrear
            // 
            this.PanelCrear.Controls.Add(this.label2);
            this.PanelCrear.Controls.Add(this.txtPassE);
            this.PanelCrear.Controls.Add(this.txtUserE);
            this.PanelCrear.Controls.Add(this.btnAcceder);
            this.PanelCrear.Controls.Add(this.lblCont);
            this.PanelCrear.Controls.Add(this.lblNom);
            this.PanelCrear.Location = new System.Drawing.Point(12, 111);
            this.PanelCrear.Name = "PanelCrear";
            this.PanelCrear.Size = new System.Drawing.Size(597, 346);
            this.PanelCrear.TabIndex = 18;
            this.PanelCrear.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(176, 42);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(246, 17);
            this.label2.TabIndex = 27;
            this.label2.Text = "Registrar Administrador en Backoffice";
            // 
            // txtPassE
            // 
            this.txtPassE.Location = new System.Drawing.Point(212, 184);
            this.txtPassE.Margin = new System.Windows.Forms.Padding(4);
            this.txtPassE.Name = "txtPassE";
            this.txtPassE.Size = new System.Drawing.Size(189, 22);
            this.txtPassE.TabIndex = 26;
            // 
            // txtUserE
            // 
            this.txtUserE.Location = new System.Drawing.Point(212, 120);
            this.txtUserE.Margin = new System.Windows.Forms.Padding(4);
            this.txtUserE.Name = "txtUserE";
            this.txtUserE.Size = new System.Drawing.Size(189, 22);
            this.txtUserE.TabIndex = 25;
            // 
            // btnAcceder
            // 
            this.btnAcceder.Location = new System.Drawing.Point(225, 244);
            this.btnAcceder.Margin = new System.Windows.Forms.Padding(4);
            this.btnAcceder.Name = "btnAcceder";
            this.btnAcceder.Size = new System.Drawing.Size(156, 28);
            this.btnAcceder.TabIndex = 24;
            this.btnAcceder.Text = "Registrar";
            this.btnAcceder.UseVisualStyleBackColor = true;
            this.btnAcceder.Click += new System.EventHandler(this.btnRegistrar);
            // 
            // lblCont
            // 
            this.lblCont.AutoSize = true;
            this.lblCont.Location = new System.Drawing.Point(62, 184);
            this.lblCont.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCont.Name = "lblCont";
            this.lblCont.Size = new System.Drawing.Size(81, 17);
            this.lblCont.TabIndex = 23;
            this.lblCont.Text = "Contraseña";
            // 
            // lblNom
            // 
            this.lblNom.AutoSize = true;
            this.lblNom.Location = new System.Drawing.Point(62, 120);
            this.lblNom.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblNom.Name = "lblNom";
            this.lblNom.Size = new System.Drawing.Size(133, 17);
            this.lblNom.TabIndex = 22;
            this.lblNom.Text = "Nombre De Usuario";
            // 
            // PanelEliminar
            // 
            this.PanelEliminar.Controls.Add(this.dataGridView1);
            this.PanelEliminar.Controls.Add(this.label6);
            this.PanelEliminar.Controls.Add(this.label3);
            this.PanelEliminar.Controls.Add(this.PassE);
            this.PanelEliminar.Controls.Add(this.UserE);
            this.PanelEliminar.Controls.Add(this.button1);
            this.PanelEliminar.Controls.Add(this.label4);
            this.PanelEliminar.Controls.Add(this.label5);
            this.PanelEliminar.Location = new System.Drawing.Point(12, 94);
            this.PanelEliminar.Name = "PanelEliminar";
            this.PanelEliminar.Size = new System.Drawing.Size(597, 346);
            this.PanelEliminar.TabIndex = 19;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(344, 62);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dataGridView1.Size = new System.Drawing.Size(240, 232);
            this.dataGridView1.TabIndex = 28;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(341, 30);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(253, 17);
            this.label6.TabIndex = 27;
            this.label6.Text = "Lista de Administradores en Backoffice";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(48, 30);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(238, 17);
            this.label3.TabIndex = 27;
            this.label3.Text = "Eliminar Administrador en Backoffice";
            // 
            // PassE
            // 
            this.PassE.Location = new System.Drawing.Point(148, 184);
            this.PassE.Margin = new System.Windows.Forms.Padding(4);
            this.PassE.Name = "PassE";
            this.PassE.Size = new System.Drawing.Size(189, 22);
            this.PassE.TabIndex = 26;
            // 
            // UserE
            // 
            this.UserE.Location = new System.Drawing.Point(148, 120);
            this.UserE.Margin = new System.Windows.Forms.Padding(4);
            this.UserE.Name = "UserE";
            this.UserE.Size = new System.Drawing.Size(189, 22);
            this.UserE.TabIndex = 25;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(87, 243);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(156, 28);
            this.button1.TabIndex = 24;
            this.button1.Text = "Eliminar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.btnEliminar_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 184);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(81, 17);
            this.label4.TabIndex = 23;
            this.label4.Text = "Contraseña";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(7, 120);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(133, 17);
            this.label5.TabIndex = 22;
            this.label5.Text = "Nombre De Usuario";
            // 
            // AdministradoresBackoffice
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(630, 472);
            this.Controls.Add(this.PanelEliminar);
            this.Controls.Add(this.PanelCrear);
            this.Controls.Add(this.cbxOpcion);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button9);
            this.Name = "AdministradoresBackoffice";
            this.Text = "AdministradoresBackoffice";
            this.PanelCrear.ResumeLayout(false);
            this.PanelCrear.PerformLayout();
            this.PanelEliminar.ResumeLayout(false);
            this.PanelEliminar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbxOpcion;
        private System.Windows.Forms.Panel PanelCrear;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtPassE;
        private System.Windows.Forms.TextBox txtUserE;
        private System.Windows.Forms.Button btnAcceder;
        private System.Windows.Forms.Label lblCont;
        private System.Windows.Forms.Label lblNom;
        private System.Windows.Forms.Panel PanelEliminar;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox PassE;
        private System.Windows.Forms.TextBox UserE;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label6;
    }
}