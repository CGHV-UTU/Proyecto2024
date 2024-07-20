
namespace APIpostYeventos
{
    partial class AñadirComentario
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
            this.lblErrorID = new System.Windows.Forms.Label();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.txtID = new System.Windows.Forms.TextBox();
            this.lblID = new System.Windows.Forms.Label();
            this.txtUrl = new System.Windows.Forms.TextBox();
            this.txtTexto = new System.Windows.Forms.TextBox();
            this.lblVideo = new System.Windows.Forms.Label();
            this.lblTexto = new System.Windows.Forms.Label();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.lblFoto = new System.Windows.Forms.Label();
            this.btnVolver = new System.Windows.Forms.Button();
            this.txtComentario = new System.Windows.Forms.RichTextBox();
            this.txtNombreDeCuenta = new System.Windows.Forms.TextBox();
            this.lblNombreDeCuenta = new System.Windows.Forms.Label();
            this.btnPublicar = new System.Windows.Forms.Button();
            this.lblError = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // lblErrorID
            // 
            this.lblErrorID.AutoSize = true;
            this.lblErrorID.Location = new System.Drawing.Point(232, 18);
            this.lblErrorID.Name = "lblErrorID";
            this.lblErrorID.Size = new System.Drawing.Size(139, 13);
            this.lblErrorID.TabIndex = 70;
            this.lblErrorID.Text = "Debe ingresar una ID válida";
            this.lblErrorID.Visible = false;
            // 
            // btnBuscar
            // 
            this.btnBuscar.Location = new System.Drawing.Point(40, 41);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(75, 23);
            this.btnBuscar.TabIndex = 69;
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.UseVisualStyleBackColor = true;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(377, 18);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(159, 135);
            this.pictureBox1.TabIndex = 68;
            this.pictureBox1.TabStop = false;
            // 
            // txtID
            // 
            this.txtID.Location = new System.Drawing.Point(115, 15);
            this.txtID.Name = "txtID";
            this.txtID.Size = new System.Drawing.Size(100, 20);
            this.txtID.TabIndex = 66;
            // 
            // lblID
            // 
            this.lblID.AutoSize = true;
            this.lblID.Location = new System.Drawing.Point(37, 18);
            this.lblID.Name = "lblID";
            this.lblID.Size = new System.Drawing.Size(42, 13);
            this.lblID.TabIndex = 65;
            this.lblID.Text = "ID Post";
            // 
            // txtUrl
            // 
            this.txtUrl.Location = new System.Drawing.Point(40, 126);
            this.txtUrl.Name = "txtUrl";
            this.txtUrl.Size = new System.Drawing.Size(183, 20);
            this.txtUrl.TabIndex = 64;
            this.txtUrl.Visible = false;
            // 
            // txtTexto
            // 
            this.txtTexto.Location = new System.Drawing.Point(40, 87);
            this.txtTexto.Name = "txtTexto";
            this.txtTexto.Size = new System.Drawing.Size(100, 20);
            this.txtTexto.TabIndex = 63;
            this.txtTexto.Visible = false;
            // 
            // lblVideo
            // 
            this.lblVideo.AutoSize = true;
            this.lblVideo.Location = new System.Drawing.Point(37, 110);
            this.lblVideo.Name = "lblVideo";
            this.lblVideo.Size = new System.Drawing.Size(60, 13);
            this.lblVideo.TabIndex = 62;
            this.lblVideo.Text = "Video de yt";
            this.lblVideo.Visible = false;
            // 
            // lblTexto
            // 
            this.lblTexto.AutoSize = true;
            this.lblTexto.Location = new System.Drawing.Point(37, 72);
            this.lblTexto.Name = "lblTexto";
            this.lblTexto.Size = new System.Drawing.Size(34, 13);
            this.lblTexto.TabIndex = 60;
            this.lblTexto.Text = "Texto";
            this.lblTexto.Visible = false;
            // 
            // btnCancelar
            // 
            this.btnCancelar.Location = new System.Drawing.Point(485, 347);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(75, 23);
            this.btnCancelar.TabIndex = 73;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Visible = false;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // lblFoto
            // 
            this.lblFoto.AutoSize = true;
            this.lblFoto.Location = new System.Drawing.Point(442, 2);
            this.lblFoto.Name = "lblFoto";
            this.lblFoto.Size = new System.Drawing.Size(28, 13);
            this.lblFoto.TabIndex = 74;
            this.lblFoto.Text = "Foto";
            this.lblFoto.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblFoto.Visible = false;
            // 
            // btnVolver
            // 
            this.btnVolver.Location = new System.Drawing.Point(140, 41);
            this.btnVolver.Name = "btnVolver";
            this.btnVolver.Size = new System.Drawing.Size(75, 23);
            this.btnVolver.TabIndex = 75;
            this.btnVolver.Text = "Volver";
            this.btnVolver.UseVisualStyleBackColor = true;
            this.btnVolver.Click += new System.EventHandler(this.btnVolver_Click);
            // 
            // txtComentario
            // 
            this.txtComentario.Location = new System.Drawing.Point(40, 200);
            this.txtComentario.Name = "txtComentario";
            this.txtComentario.Size = new System.Drawing.Size(520, 132);
            this.txtComentario.TabIndex = 76;
            this.txtComentario.Text = "";
            this.txtComentario.Visible = false;
            // 
            // txtNombreDeCuenta
            // 
            this.txtNombreDeCuenta.Location = new System.Drawing.Point(165, 169);
            this.txtNombreDeCuenta.Name = "txtNombreDeCuenta";
            this.txtNombreDeCuenta.Size = new System.Drawing.Size(100, 20);
            this.txtNombreDeCuenta.TabIndex = 77;
            this.txtNombreDeCuenta.Visible = false;
            // 
            // lblNombreDeCuenta
            // 
            this.lblNombreDeCuenta.AutoSize = true;
            this.lblNombreDeCuenta.Location = new System.Drawing.Point(53, 172);
            this.lblNombreDeCuenta.Name = "lblNombreDeCuenta";
            this.lblNombreDeCuenta.Size = new System.Drawing.Size(96, 13);
            this.lblNombreDeCuenta.TabIndex = 78;
            this.lblNombreDeCuenta.Text = "Nombre de Cuenta";
            this.lblNombreDeCuenta.Visible = false;
            // 
            // btnPublicar
            // 
            this.btnPublicar.Location = new System.Drawing.Point(40, 347);
            this.btnPublicar.Name = "btnPublicar";
            this.btnPublicar.Size = new System.Drawing.Size(91, 23);
            this.btnPublicar.TabIndex = 79;
            this.btnPublicar.Text = "Publicar";
            this.btnPublicar.UseVisualStyleBackColor = true;
            this.btnPublicar.Visible = false;
            this.btnPublicar.Click += new System.EventHandler(this.btnPublicar_Click);
            // 
            // lblError
            // 
            this.lblError.AutoSize = true;
            this.lblError.Location = new System.Drawing.Point(137, 352);
            this.lblError.Name = "lblError";
            this.lblError.Size = new System.Drawing.Size(0, 13);
            this.lblError.TabIndex = 80;
            this.lblError.Visible = false;
            // 
            // AñadirComentario
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(581, 382);
            this.Controls.Add(this.lblError);
            this.Controls.Add(this.btnPublicar);
            this.Controls.Add(this.lblNombreDeCuenta);
            this.Controls.Add(this.txtNombreDeCuenta);
            this.Controls.Add(this.txtComentario);
            this.Controls.Add(this.btnVolver);
            this.Controls.Add(this.lblFoto);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.lblErrorID);
            this.Controls.Add(this.btnBuscar);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.txtID);
            this.Controls.Add(this.lblID);
            this.Controls.Add(this.txtUrl);
            this.Controls.Add(this.txtTexto);
            this.Controls.Add(this.lblVideo);
            this.Controls.Add(this.lblTexto);
            this.Name = "AñadirComentario";
            this.Text = "AñadirComentario";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lblErrorID;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox txtID;
        private System.Windows.Forms.Label lblID;
        private System.Windows.Forms.TextBox txtUrl;
        private System.Windows.Forms.TextBox txtTexto;
        private System.Windows.Forms.Label lblVideo;
        private System.Windows.Forms.Label lblTexto;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Label lblFoto;
        private System.Windows.Forms.Button btnVolver;
        private System.Windows.Forms.RichTextBox txtComentario;
        private System.Windows.Forms.TextBox txtNombreDeCuenta;
        private System.Windows.Forms.Label lblNombreDeCuenta;
        private System.Windows.Forms.Button btnPublicar;
        private System.Windows.Forms.Label lblError;
    }
}