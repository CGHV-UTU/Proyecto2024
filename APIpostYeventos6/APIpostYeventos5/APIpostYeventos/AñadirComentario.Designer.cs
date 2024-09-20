
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
            this.txtID = new System.Windows.Forms.TextBox();
            this.lblID = new System.Windows.Forms.Label();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnVolver = new System.Windows.Forms.Button();
            this.txtComentario = new System.Windows.Forms.RichTextBox();
            this.lblComentario = new System.Windows.Forms.Label();
            this.btnPublicar = new System.Windows.Forms.Button();
            this.lblError = new System.Windows.Forms.Label();
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
            // btnCancelar
            // 
            this.btnCancelar.Location = new System.Drawing.Point(474, 231);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(75, 23);
            this.btnCancelar.TabIndex = 73;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Visible = false;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
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
            this.txtComentario.Location = new System.Drawing.Point(29, 110);
            this.txtComentario.MaxLength = 99;
            this.txtComentario.Name = "txtComentario";
            this.txtComentario.Size = new System.Drawing.Size(520, 104);
            this.txtComentario.TabIndex = 76;
            this.txtComentario.Text = "";
            this.txtComentario.Visible = false;
            // 
            // lblComentario
            // 
            this.lblComentario.AutoSize = true;
            this.lblComentario.Location = new System.Drawing.Point(42, 82);
            this.lblComentario.Name = "lblComentario";
            this.lblComentario.Size = new System.Drawing.Size(63, 13);
            this.lblComentario.TabIndex = 78;
            this.lblComentario.Text = "Comentario:";
            this.lblComentario.Visible = false;
            // 
            // btnPublicar
            // 
            this.btnPublicar.Location = new System.Drawing.Point(29, 231);
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
            this.lblError.Location = new System.Drawing.Point(126, 236);
            this.lblError.Name = "lblError";
            this.lblError.Size = new System.Drawing.Size(0, 13);
            this.lblError.TabIndex = 80;
            this.lblError.Visible = false;
            // 
            // AñadirComentario
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(581, 264);
            this.Controls.Add(this.lblError);
            this.Controls.Add(this.btnPublicar);
            this.Controls.Add(this.lblComentario);
            this.Controls.Add(this.txtComentario);
            this.Controls.Add(this.btnVolver);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.lblErrorID);
            this.Controls.Add(this.btnBuscar);
            this.Controls.Add(this.txtID);
            this.Controls.Add(this.lblID);
            this.Name = "AñadirComentario";
            this.Text = "AñadirComentario";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lblErrorID;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.TextBox txtID;
        private System.Windows.Forms.Label lblID;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnVolver;
        private System.Windows.Forms.RichTextBox txtComentario;
        private System.Windows.Forms.Label lblComentario;
        private System.Windows.Forms.Button btnPublicar;
        private System.Windows.Forms.Label lblError;
    }
}