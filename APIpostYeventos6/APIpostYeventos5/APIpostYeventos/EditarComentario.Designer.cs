
namespace APIpostYeventos
{
    partial class EditarComentario
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
            this.lblError2 = new System.Windows.Forms.Label();
            this.txtIdComentario = new System.Windows.Forms.TextBox();
            this.lblComentario = new System.Windows.Forms.Label();
            this.txtTexto = new System.Windows.Forms.Label();
            this.txtUrl = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btnConfirmar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnVolver = new System.Windows.Forms.Button();
            this.lblFoto = new System.Windows.Forms.Label();
            this.lblErrorID = new System.Windows.Forms.Label();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.txtID = new System.Windows.Forms.TextBox();
            this.lblID = new System.Windows.Forms.Label();
            this.lblVideo = new System.Windows.Forms.Label();
            this.lblTexto = new System.Windows.Forms.Label();
            this.btnEditar = new System.Windows.Forms.Button();
            this.txtComentario = new System.Windows.Forms.RichTextBox();
            this.lblCuenta = new System.Windows.Forms.Label();
            this.lblFechayhora = new System.Windows.Forms.Label();
            this.lblError3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblidComentario = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // lblError2
            // 
            this.lblError2.AutoSize = true;
            this.lblError2.Location = new System.Drawing.Point(326, 308);
            this.lblError2.Name = "lblError2";
            this.lblError2.Size = new System.Drawing.Size(139, 13);
            this.lblError2.TabIndex = 111;
            this.lblError2.Text = "Debe ingresar una ID válida";
            this.lblError2.Visible = false;
            // 
            // txtIdComentario
            // 
            this.txtIdComentario.Location = new System.Drawing.Point(90, 305);
            this.txtIdComentario.Name = "txtIdComentario";
            this.txtIdComentario.Size = new System.Drawing.Size(100, 20);
            this.txtIdComentario.TabIndex = 110;
            this.txtIdComentario.Visible = false;
            // 
            // lblComentario
            // 
            this.lblComentario.AutoSize = true;
            this.lblComentario.Location = new System.Drawing.Point(12, 308);
            this.lblComentario.Name = "lblComentario";
            this.lblComentario.Size = new System.Drawing.Size(71, 13);
            this.lblComentario.TabIndex = 109;
            this.lblComentario.Text = "IDComentario";
            this.lblComentario.Visible = false;
            // 
            // txtTexto
            // 
            this.txtTexto.AutoSize = true;
            this.txtTexto.Location = new System.Drawing.Point(58, 81);
            this.txtTexto.Name = "txtTexto";
            this.txtTexto.Size = new System.Drawing.Size(0, 13);
            this.txtTexto.TabIndex = 108;
            // 
            // txtUrl
            // 
            this.txtUrl.AutoSize = true;
            this.txtUrl.Location = new System.Drawing.Point(84, 119);
            this.txtUrl.Name = "txtUrl";
            this.txtUrl.Size = new System.Drawing.Size(0, 13);
            this.txtUrl.TabIndex = 107;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 168);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(496, 131);
            this.dataGridView1.TabIndex = 106;
            this.dataGridView1.Visible = false;
            // 
            // btnConfirmar
            // 
            this.btnConfirmar.Location = new System.Drawing.Point(12, 511);
            this.btnConfirmar.Name = "btnConfirmar";
            this.btnConfirmar.Size = new System.Drawing.Size(91, 23);
            this.btnConfirmar.TabIndex = 105;
            this.btnConfirmar.Text = "Confirmar";
            this.btnConfirmar.UseVisualStyleBackColor = true;
            this.btnConfirmar.Visible = false;
            this.btnConfirmar.Click += new System.EventHandler(this.btnConfirmar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Location = new System.Drawing.Point(433, 511);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(75, 23);
            this.btnCancelar.TabIndex = 104;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Visible = false;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnVolver
            // 
            this.btnVolver.Location = new System.Drawing.Point(112, 50);
            this.btnVolver.Name = "btnVolver";
            this.btnVolver.Size = new System.Drawing.Size(75, 23);
            this.btnVolver.TabIndex = 103;
            this.btnVolver.Text = "Volver";
            this.btnVolver.UseVisualStyleBackColor = true;
            this.btnVolver.Click += new System.EventHandler(this.btnVolver_Click);
            // 
            // lblFoto
            // 
            this.lblFoto.AutoSize = true;
            this.lblFoto.Location = new System.Drawing.Point(414, 11);
            this.lblFoto.Name = "lblFoto";
            this.lblFoto.Size = new System.Drawing.Size(28, 13);
            this.lblFoto.TabIndex = 102;
            this.lblFoto.Text = "Foto";
            this.lblFoto.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblFoto.Visible = false;
            // 
            // lblErrorID
            // 
            this.lblErrorID.AutoSize = true;
            this.lblErrorID.Location = new System.Drawing.Point(204, 27);
            this.lblErrorID.Name = "lblErrorID";
            this.lblErrorID.Size = new System.Drawing.Size(139, 13);
            this.lblErrorID.TabIndex = 101;
            this.lblErrorID.Text = "Debe ingresar una ID válida";
            this.lblErrorID.Visible = false;
            // 
            // btnBuscar
            // 
            this.btnBuscar.Location = new System.Drawing.Point(12, 50);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(75, 23);
            this.btnBuscar.TabIndex = 100;
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.UseVisualStyleBackColor = true;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(349, 27);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(159, 135);
            this.pictureBox1.TabIndex = 99;
            this.pictureBox1.TabStop = false;
            // 
            // txtID
            // 
            this.txtID.Location = new System.Drawing.Point(87, 24);
            this.txtID.Name = "txtID";
            this.txtID.Size = new System.Drawing.Size(100, 20);
            this.txtID.TabIndex = 98;
            // 
            // lblID
            // 
            this.lblID.AutoSize = true;
            this.lblID.Location = new System.Drawing.Point(9, 27);
            this.lblID.Name = "lblID";
            this.lblID.Size = new System.Drawing.Size(42, 13);
            this.lblID.TabIndex = 97;
            this.lblID.Text = "ID Post";
            // 
            // lblVideo
            // 
            this.lblVideo.AutoSize = true;
            this.lblVideo.Location = new System.Drawing.Point(9, 119);
            this.lblVideo.Name = "lblVideo";
            this.lblVideo.Size = new System.Drawing.Size(60, 13);
            this.lblVideo.TabIndex = 96;
            this.lblVideo.Text = "Video de yt";
            this.lblVideo.Visible = false;
            // 
            // lblTexto
            // 
            this.lblTexto.AutoSize = true;
            this.lblTexto.Location = new System.Drawing.Point(9, 81);
            this.lblTexto.Name = "lblTexto";
            this.lblTexto.Size = new System.Drawing.Size(34, 13);
            this.lblTexto.TabIndex = 95;
            this.lblTexto.Text = "Texto";
            this.lblTexto.Visible = false;
            // 
            // btnEditar
            // 
            this.btnEditar.Location = new System.Drawing.Point(207, 303);
            this.btnEditar.Name = "btnEditar";
            this.btnEditar.Size = new System.Drawing.Size(91, 23);
            this.btnEditar.TabIndex = 112;
            this.btnEditar.Text = "Editar";
            this.btnEditar.UseVisualStyleBackColor = true;
            this.btnEditar.Visible = false;
            this.btnEditar.Click += new System.EventHandler(this.btnEditar_Click);
            // 
            // txtComentario
            // 
            this.txtComentario.Location = new System.Drawing.Point(12, 373);
            this.txtComentario.Name = "txtComentario";
            this.txtComentario.Size = new System.Drawing.Size(495, 132);
            this.txtComentario.TabIndex = 113;
            this.txtComentario.Text = "";
            this.txtComentario.Visible = false;
            // 
            // lblCuenta
            // 
            this.lblCuenta.AutoSize = true;
            this.lblCuenta.Location = new System.Drawing.Point(59, 357);
            this.lblCuenta.Name = "lblCuenta";
            this.lblCuenta.Size = new System.Drawing.Size(0, 13);
            this.lblCuenta.TabIndex = 114;
            this.lblCuenta.Visible = false;
            // 
            // lblFechayhora
            // 
            this.lblFechayhora.AutoSize = true;
            this.lblFechayhora.Location = new System.Drawing.Point(326, 357);
            this.lblFechayhora.Name = "lblFechayhora";
            this.lblFechayhora.Size = new System.Drawing.Size(0, 13);
            this.lblFechayhora.TabIndex = 115;
            this.lblFechayhora.Visible = false;
            // 
            // lblError3
            // 
            this.lblError3.AutoSize = true;
            this.lblError3.Location = new System.Drawing.Point(109, 516);
            this.lblError3.Name = "lblError3";
            this.lblError3.Size = new System.Drawing.Size(139, 13);
            this.lblError3.TabIndex = 116;
            this.lblError3.Text = "Debe ingresar una ID válida";
            this.lblError3.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 334);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(165, 13);
            this.label2.TabIndex = 117;
            this.label2.Text = "ID del comentario siendo editado:";
            this.label2.Visible = false;
            // 
            // lblidComentario
            // 
            this.lblidComentario.AutoSize = true;
            this.lblidComentario.Location = new System.Drawing.Point(184, 334);
            this.lblidComentario.Name = "lblidComentario";
            this.lblidComentario.Size = new System.Drawing.Size(0, 13);
            this.lblidComentario.TabIndex = 118;
            this.lblidComentario.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 357);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 119;
            this.label1.Text = "Cuenta:";
            this.label1.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(252, 357);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 13);
            this.label3.TabIndex = 120;
            this.label3.Text = "Fecha/Hora:";
            this.label3.Visible = false;
            // 
            // EditarComentario
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(519, 546);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblidComentario);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblError3);
            this.Controls.Add(this.lblFechayhora);
            this.Controls.Add(this.lblCuenta);
            this.Controls.Add(this.txtComentario);
            this.Controls.Add(this.btnEditar);
            this.Controls.Add(this.lblError2);
            this.Controls.Add(this.txtIdComentario);
            this.Controls.Add(this.lblComentario);
            this.Controls.Add(this.txtTexto);
            this.Controls.Add(this.txtUrl);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btnConfirmar);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnVolver);
            this.Controls.Add(this.lblFoto);
            this.Controls.Add(this.lblErrorID);
            this.Controls.Add(this.btnBuscar);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.txtID);
            this.Controls.Add(this.lblID);
            this.Controls.Add(this.lblVideo);
            this.Controls.Add(this.lblTexto);
            this.Name = "EditarComentario";
            this.Text = "EditarComentario";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblError2;
        private System.Windows.Forms.TextBox txtIdComentario;
        private System.Windows.Forms.Label lblComentario;
        private System.Windows.Forms.Label txtTexto;
        private System.Windows.Forms.Label txtUrl;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnConfirmar;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnVolver;
        private System.Windows.Forms.Label lblFoto;
        private System.Windows.Forms.Label lblErrorID;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox txtID;
        private System.Windows.Forms.Label lblID;
        private System.Windows.Forms.Label lblVideo;
        private System.Windows.Forms.Label lblTexto;
        private System.Windows.Forms.Button btnEditar;
        private System.Windows.Forms.RichTextBox txtComentario;
        private System.Windows.Forms.Label lblCuenta;
        private System.Windows.Forms.Label lblFechayhora;
        private System.Windows.Forms.Label lblError3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblidComentario;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
    }
}