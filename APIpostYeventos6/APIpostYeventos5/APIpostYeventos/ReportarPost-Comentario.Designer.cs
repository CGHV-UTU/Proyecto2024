
namespace APIpostYeventos
{
    partial class ReportarPost_Comentario
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
            this.cbxReporte = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblID = new System.Windows.Forms.Label();
            this.txtID = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtDesc = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnEnviar = new System.Windows.Forms.Button();
            this.txtUsuario = new System.Windows.Forms.TextBox();
            this.lblUsuario = new System.Windows.Forms.Label();
            this.lblResultado = new System.Windows.Forms.Label();
            this.cbxTipo = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // cbxReporte
            // 
            this.cbxReporte.FormattingEnabled = true;
            this.cbxReporte.Items.AddRange(new object[] {
            "Post",
            "Comentario"});
            this.cbxReporte.Location = new System.Drawing.Point(160, 16);
            this.cbxReporte.Name = "cbxReporte";
            this.cbxReporte.Size = new System.Drawing.Size(150, 21);
            this.cbxReporte.TabIndex = 0;
            this.cbxReporte.SelectedIndexChanged += new System.EventHandler(this.cbxReporte_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(110, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "¿Qué desea reportar?";
            // 
            // lblID
            // 
            this.lblID.AutoSize = true;
            this.lblID.Location = new System.Drawing.Point(26, 76);
            this.lblID.Name = "lblID";
            this.lblID.Size = new System.Drawing.Size(56, 13);
            this.lblID.TabIndex = 2;
            this.lblID.Text = "Id del post";
            // 
            // txtID
            // 
            this.txtID.Location = new System.Drawing.Point(160, 73);
            this.txtID.Name = "txtID";
            this.txtID.Size = new System.Drawing.Size(150, 20);
            this.txtID.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(26, 187);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Razón";
            // 
            // txtDesc
            // 
            this.txtDesc.Location = new System.Drawing.Point(160, 249);
            this.txtDesc.Multiline = true;
            this.txtDesc.Name = "txtDesc";
            this.txtDesc.Size = new System.Drawing.Size(150, 80);
            this.txtDesc.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(26, 249);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(63, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Descripción";
            // 
            // btnEnviar
            // 
            this.btnEnviar.Location = new System.Drawing.Point(137, 409);
            this.btnEnviar.Name = "btnEnviar";
            this.btnEnviar.Size = new System.Drawing.Size(98, 23);
            this.btnEnviar.TabIndex = 8;
            this.btnEnviar.Text = "Enviar reporte";
            this.btnEnviar.UseVisualStyleBackColor = true;
            this.btnEnviar.Click += new System.EventHandler(this.btnEnviar_Click);
            // 
            // txtUsuario
            // 
            this.txtUsuario.Location = new System.Drawing.Point(160, 134);
            this.txtUsuario.Name = "txtUsuario";
            this.txtUsuario.Size = new System.Drawing.Size(150, 20);
            this.txtUsuario.TabIndex = 10;
            // 
            // lblUsuario
            // 
            this.lblUsuario.AutoSize = true;
            this.lblUsuario.Location = new System.Drawing.Point(26, 134);
            this.lblUsuario.Name = "lblUsuario";
            this.lblUsuario.Size = new System.Drawing.Size(43, 13);
            this.lblUsuario.TabIndex = 9;
            this.lblUsuario.Text = "Usuario";
            // 
            // lblResultado
            // 
            this.lblResultado.AutoSize = true;
            this.lblResultado.Location = new System.Drawing.Point(26, 360);
            this.lblResultado.Name = "lblResultado";
            this.lblResultado.Size = new System.Drawing.Size(55, 13);
            this.lblResultado.TabIndex = 11;
            this.lblResultado.Text = "Resultado";
            // 
            // cbxTipo
            // 
            this.cbxTipo.FormattingEnabled = true;
            this.cbxTipo.Items.AddRange(new object[] {
            "Sexual",
            "Violento o repugnante",
            "Vejatorio",
            "Hostigamiento o acoso",
            "Actividades dañinas o peligrosas",
            "Desinformacion",
            "Maltrato infantil",
            "Terrorismo",
            "Fraude",
            "Problema legal",
            "Otros"});
            this.cbxTipo.Location = new System.Drawing.Point(160, 187);
            this.cbxTipo.Name = "cbxTipo";
            this.cbxTipo.Size = new System.Drawing.Size(150, 21);
            this.cbxTipo.TabIndex = 12;
            // 
            // ReportarPost_Comentario
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(368, 450);
            this.Controls.Add(this.cbxTipo);
            this.Controls.Add(this.lblResultado);
            this.Controls.Add(this.txtUsuario);
            this.Controls.Add(this.lblUsuario);
            this.Controls.Add(this.btnEnviar);
            this.Controls.Add(this.txtDesc);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtID);
            this.Controls.Add(this.lblID);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbxReporte);
            this.Name = "ReportarPost_Comentario";
            this.Text = "ReportarPost_Comentario";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbxReporte;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblID;
        private System.Windows.Forms.TextBox txtID;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtDesc;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnEnviar;
        private System.Windows.Forms.TextBox txtUsuario;
        private System.Windows.Forms.Label lblUsuario;
        private System.Windows.Forms.Label lblResultado;
        private System.Windows.Forms.ComboBox cbxTipo;
    }
}