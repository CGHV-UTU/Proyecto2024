
namespace Frontend
{
    partial class Busqueda
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
            this.txtBusqueda = new System.Windows.Forms.TextBox();
            this.pnlOpciones = new System.Windows.Forms.Panel();
            this.pnlEvento = new System.Windows.Forms.Panel();
            this.pictureBox5 = new System.Windows.Forms.PictureBox();
            this.label3 = new System.Windows.Forms.Label();
            this.pnlGrupo = new System.Windows.Forms.Panel();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.pnlUsuario = new System.Windows.Forms.Panel();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnOpciones = new System.Windows.Forms.PictureBox();
            this.btnBuscar = new System.Windows.Forms.PictureBox();
            this.pnlMostrar = new System.Windows.Forms.Panel();
            this.pnlOpciones.SuspendLayout();
            this.pnlEvento.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).BeginInit();
            this.pnlGrupo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            this.pnlUsuario.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnOpciones)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnBuscar)).BeginInit();
            this.SuspendLayout();
            // 
            // txtBusqueda
            // 
            this.txtBusqueda.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBusqueda.Location = new System.Drawing.Point(102, 28);
            this.txtBusqueda.Name = "txtBusqueda";
            this.txtBusqueda.Size = new System.Drawing.Size(786, 38);
            this.txtBusqueda.TabIndex = 0;
            // 
            // pnlOpciones
            // 
            this.pnlOpciones.Controls.Add(this.pnlEvento);
            this.pnlOpciones.Controls.Add(this.pnlGrupo);
            this.pnlOpciones.Controls.Add(this.pnlUsuario);
            this.pnlOpciones.Location = new System.Drawing.Point(19, 72);
            this.pnlOpciones.Name = "pnlOpciones";
            this.pnlOpciones.Size = new System.Drawing.Size(223, 183);
            this.pnlOpciones.TabIndex = 3;
            // 
            // pnlEvento
            // 
            this.pnlEvento.Controls.Add(this.pictureBox5);
            this.pnlEvento.Controls.Add(this.label3);
            this.pnlEvento.Location = new System.Drawing.Point(3, 123);
            this.pnlEvento.Name = "pnlEvento";
            this.pnlEvento.Size = new System.Drawing.Size(215, 58);
            this.pnlEvento.TabIndex = 9;
            // 
            // pictureBox5
            // 
            this.pictureBox5.Image = global::Frontend.Properties.Resources.lupa_removebg_preview;
            this.pictureBox5.Location = new System.Drawing.Point(3, 5);
            this.pictureBox5.Name = "pictureBox5";
            this.pictureBox5.Size = new System.Drawing.Size(50, 50);
            this.pictureBox5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox5.TabIndex = 6;
            this.pictureBox5.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(85, 21);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Evento";
            // 
            // pnlGrupo
            // 
            this.pnlGrupo.Controls.Add(this.pictureBox4);
            this.pnlGrupo.Controls.Add(this.label2);
            this.pnlGrupo.Location = new System.Drawing.Point(3, 62);
            this.pnlGrupo.Name = "pnlGrupo";
            this.pnlGrupo.Size = new System.Drawing.Size(215, 58);
            this.pnlGrupo.TabIndex = 9;
            // 
            // pictureBox4
            // 
            this.pictureBox4.Image = global::Frontend.Properties.Resources.Comunidad;
            this.pictureBox4.Location = new System.Drawing.Point(3, 5);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(50, 50);
            this.pictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox4.TabIndex = 5;
            this.pictureBox4.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(86, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(36, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Grupo";
            // 
            // pnlUsuario
            // 
            this.pnlUsuario.Controls.Add(this.pictureBox3);
            this.pnlUsuario.Controls.Add(this.label1);
            this.pnlUsuario.Location = new System.Drawing.Point(3, 3);
            this.pnlUsuario.Name = "pnlUsuario";
            this.pnlUsuario.Size = new System.Drawing.Size(215, 58);
            this.pnlUsuario.TabIndex = 8;
            // 
            // pictureBox3
            // 
            this.pictureBox3.BackgroundImage = global::Frontend.Properties.Resources.lupa_removebg_preview;
            this.pictureBox3.Image = global::Frontend.Properties.Resources.User;
            this.pictureBox3.Location = new System.Drawing.Point(3, 3);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(50, 50);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox3.TabIndex = 4;
            this.pictureBox3.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(83, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Usuario";
            // 
            // btnOpciones
            // 
            this.btnOpciones.Image = global::Frontend.Properties.Resources.mas_opciones;
            this.btnOpciones.Location = new System.Drawing.Point(19, 21);
            this.btnOpciones.Name = "btnOpciones";
            this.btnOpciones.Size = new System.Drawing.Size(50, 50);
            this.btnOpciones.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.btnOpciones.TabIndex = 2;
            this.btnOpciones.TabStop = false;
            this.btnOpciones.Click += new System.EventHandler(this.btnOpciones_Click);
            // 
            // btnBuscar
            // 
            this.btnBuscar.BackgroundImage = global::Frontend.Properties.Resources.lupa_removebg_preview;
            this.btnBuscar.Image = global::Frontend.Properties.Resources.lupa_removebg_preview;
            this.btnBuscar.Location = new System.Drawing.Point(908, 13);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(70, 70);
            this.btnBuscar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.btnBuscar.TabIndex = 1;
            this.btnBuscar.TabStop = false;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // pnlMostrar
            // 
            this.pnlMostrar.Location = new System.Drawing.Point(19, 77);
            this.pnlMostrar.Name = "pnlMostrar";
            this.pnlMostrar.Size = new System.Drawing.Size(869, 214);
            this.pnlMostrar.TabIndex = 4;
            // 
            // Busqueda
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(996, 303);
            this.Controls.Add(this.pnlOpciones);
            this.Controls.Add(this.pnlMostrar);
            this.Controls.Add(this.btnOpciones);
            this.Controls.Add(this.btnBuscar);
            this.Controls.Add(this.txtBusqueda);
            this.Name = "Busqueda";
            this.Text = "Busqueda";
            this.pnlOpciones.ResumeLayout(false);
            this.pnlEvento.ResumeLayout(false);
            this.pnlEvento.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).EndInit();
            this.pnlGrupo.ResumeLayout(false);
            this.pnlGrupo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            this.pnlUsuario.ResumeLayout(false);
            this.pnlUsuario.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnOpciones)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnBuscar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtBusqueda;
        private System.Windows.Forms.PictureBox btnBuscar;
        private System.Windows.Forms.PictureBox btnOpciones;
        private System.Windows.Forms.Panel pnlOpciones;
        private System.Windows.Forms.PictureBox pictureBox5;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.Panel pnlMostrar;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel pnlEvento;
        private System.Windows.Forms.Panel pnlGrupo;
        private System.Windows.Forms.Panel pnlUsuario;
    }
}