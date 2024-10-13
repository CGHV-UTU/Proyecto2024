
namespace BackofficeDeAdministracion
{
    partial class GestionarGrupos
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
            this.lblNombreVisible = new System.Windows.Forms.Label();
            this.lblNomVisible = new System.Windows.Forms.Label();
            this.lblFoto = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblNombreDeGrupo = new System.Windows.Forms.Label();
            this.lblDesc = new System.Windows.Forms.Label();
            this.lblNombre = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.lblNom = new System.Windows.Forms.Label();
            this.txtID = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.txtDescripcionDeGrupo = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // lblNombreVisible
            // 
            this.lblNombreVisible.AutoSize = true;
            this.lblNombreVisible.Location = new System.Drawing.Point(119, 255);
            this.lblNombreVisible.Name = "lblNombreVisible";
            this.lblNombreVisible.Size = new System.Drawing.Size(44, 13);
            this.lblNombreVisible.TabIndex = 110;
            this.lblNombreVisible.Text = "Nombre";
            this.lblNombreVisible.Visible = false;
            // 
            // lblNomVisible
            // 
            this.lblNomVisible.AutoSize = true;
            this.lblNomVisible.Location = new System.Drawing.Point(15, 255);
            this.lblNomVisible.Name = "lblNomVisible";
            this.lblNomVisible.Size = new System.Drawing.Size(79, 13);
            this.lblNomVisible.TabIndex = 109;
            this.lblNomVisible.Text = "Nombre visible:";
            this.lblNomVisible.Visible = false;
            // 
            // lblFoto
            // 
            this.lblFoto.AutoSize = true;
            this.lblFoto.Location = new System.Drawing.Point(215, 218);
            this.lblFoto.Name = "lblFoto";
            this.lblFoto.Size = new System.Drawing.Size(68, 13);
            this.lblFoto.TabIndex = 108;
            this.lblFoto.Text = "Foto de perfil";
            this.lblFoto.Visible = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(303, 209);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(196, 145);
            this.pictureBox1.TabIndex = 107;
            this.pictureBox1.TabStop = false;
            // 
            // lblNombreDeGrupo
            // 
            this.lblNombreDeGrupo.AutoSize = true;
            this.lblNombreDeGrupo.Location = new System.Drawing.Point(119, 209);
            this.lblNombreDeGrupo.Name = "lblNombreDeGrupo";
            this.lblNombreDeGrupo.Size = new System.Drawing.Size(0, 13);
            this.lblNombreDeGrupo.TabIndex = 105;
            // 
            // lblDesc
            // 
            this.lblDesc.AutoSize = true;
            this.lblDesc.Location = new System.Drawing.Point(520, 215);
            this.lblDesc.Name = "lblDesc";
            this.lblDesc.Size = new System.Drawing.Size(66, 13);
            this.lblDesc.TabIndex = 104;
            this.lblDesc.Text = "Descripción:";
            this.lblDesc.Visible = false;
            // 
            // lblNombre
            // 
            this.lblNombre.AutoSize = true;
            this.lblNombre.Location = new System.Drawing.Point(15, 209);
            this.lblNombre.Name = "lblNombre";
            this.lblNombre.Size = new System.Drawing.Size(94, 13);
            this.lblNombre.TabIndex = 103;
            this.lblNombre.Text = "Nombre de Grupo:";
            this.lblNombre.Visible = false;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(255, 12);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(569, 170);
            this.dataGridView1.TabIndex = 101;
            // 
            // btnBuscar
            // 
            this.btnBuscar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(101)))), ((int)(((byte)(64)))), ((int)(((byte)(222)))));
            this.btnBuscar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBuscar.ForeColor = System.Drawing.Color.White;
            this.btnBuscar.Location = new System.Drawing.Point(11, 95);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(216, 23);
            this.btnBuscar.TabIndex = 100;
            this.btnBuscar.Text = "🔎 Buscar ";
            this.btnBuscar.UseVisualStyleBackColor = false;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // lblNom
            // 
            this.lblNom.AutoSize = true;
            this.lblNom.Location = new System.Drawing.Point(8, 56);
            this.lblNom.Name = "lblNom";
            this.lblNom.Size = new System.Drawing.Size(94, 13);
            this.lblNom.TabIndex = 99;
            this.lblNom.Text = "Nombre de Grupo:";
            // 
            // txtID
            // 
            this.txtID.Location = new System.Drawing.Point(106, 53);
            this.txtID.Name = "txtID";
            this.txtID.Size = new System.Drawing.Size(121, 20);
            this.txtID.TabIndex = 98;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(101)))), ((int)(((byte)(64)))), ((int)(((byte)(222)))));
            this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(11, 137);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(216, 23);
            this.button1.TabIndex = 111;
            this.button1.Text = "❌Eliminar Grupo";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.btnEliminar);
            // 
            // btnGuardar
            // 
            this.btnGuardar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(101)))), ((int)(((byte)(64)))), ((int)(((byte)(222)))));
            this.btnGuardar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGuardar.ForeColor = System.Drawing.Color.White;
            this.btnGuardar.Location = new System.Drawing.Point(11, 407);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(813, 22);
            this.btnGuardar.TabIndex = 112;
            this.btnGuardar.Text = "💾 Guardar";
            this.btnGuardar.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnGuardar.UseVisualStyleBackColor = false;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // txtDescripcionDeGrupo
            // 
            this.txtDescripcionDeGrupo.Cursor = System.Windows.Forms.Cursors.No;
            this.txtDescripcionDeGrupo.Location = new System.Drawing.Point(592, 209);
            this.txtDescripcionDeGrupo.Multiline = true;
            this.txtDescripcionDeGrupo.Name = "txtDescripcionDeGrupo";
            this.txtDescripcionDeGrupo.Size = new System.Drawing.Size(232, 145);
            this.txtDescripcionDeGrupo.TabIndex = 113;
            // 
            // GestionarGrupos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gray;
            this.ClientSize = new System.Drawing.Size(836, 441);
            this.Controls.Add(this.txtDescripcionDeGrupo);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.lblNombreVisible);
            this.Controls.Add(this.lblNomVisible);
            this.Controls.Add(this.lblFoto);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.lblNombreDeGrupo);
            this.Controls.Add(this.lblDesc);
            this.Controls.Add(this.lblNombre);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btnBuscar);
            this.Controls.Add(this.lblNom);
            this.Controls.Add(this.txtID);
            this.Name = "GestionarGrupos";
            this.Text = "GestionarUsuarios";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblNombreVisible;
        private System.Windows.Forms.Label lblNomVisible;
        private System.Windows.Forms.Label lblFoto;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lblNombreDeGrupo;
        private System.Windows.Forms.Label lblDesc;
        private System.Windows.Forms.Label lblNombre;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.Label lblNom;
        private System.Windows.Forms.TextBox txtID;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.TextBox txtDescripcionDeGrupo;
    }
}