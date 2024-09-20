
namespace APIpostYeventos
{
    partial class EliminarComentario
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
            this.btnVolver = new System.Windows.Forms.Button();
            this.lblErrorID = new System.Windows.Forms.Label();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.txtID = new System.Windows.Forms.TextBox();
            this.lblID = new System.Windows.Forms.Label();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.lblComentario = new System.Windows.Forms.Label();
            this.txtIdComentario = new System.Windows.Forms.TextBox();
            this.lblError2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnVolver
            // 
            this.btnVolver.Location = new System.Drawing.Point(120, 47);
            this.btnVolver.Name = "btnVolver";
            this.btnVolver.Size = new System.Drawing.Size(75, 23);
            this.btnVolver.TabIndex = 86;
            this.btnVolver.Text = "Volver";
            this.btnVolver.UseVisualStyleBackColor = true;
            this.btnVolver.Click += new System.EventHandler(this.btnVolver_Click);
            // 
            // lblErrorID
            // 
            this.lblErrorID.AutoSize = true;
            this.lblErrorID.Location = new System.Drawing.Point(212, 24);
            this.lblErrorID.Name = "lblErrorID";
            this.lblErrorID.Size = new System.Drawing.Size(139, 13);
            this.lblErrorID.TabIndex = 84;
            this.lblErrorID.Text = "Debe ingresar una ID válida";
            this.lblErrorID.Visible = false;
            // 
            // btnBuscar
            // 
            this.btnBuscar.Location = new System.Drawing.Point(20, 47);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(75, 23);
            this.btnBuscar.TabIndex = 83;
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.UseVisualStyleBackColor = true;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // txtID
            // 
            this.txtID.Location = new System.Drawing.Point(95, 21);
            this.txtID.Name = "txtID";
            this.txtID.Size = new System.Drawing.Size(100, 20);
            this.txtID.TabIndex = 81;
            // 
            // lblID
            // 
            this.lblID.AutoSize = true;
            this.lblID.Location = new System.Drawing.Point(17, 24);
            this.lblID.Name = "lblID";
            this.lblID.Size = new System.Drawing.Size(42, 13);
            this.lblID.TabIndex = 80;
            this.lblID.Text = "ID Post";
            // 
            // btnCancelar
            // 
            this.btnCancelar.Location = new System.Drawing.Point(437, 253);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(75, 23);
            this.btnCancelar.TabIndex = 87;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Visible = false;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnEliminar
            // 
            this.btnEliminar.Location = new System.Drawing.Point(8, 253);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(91, 23);
            this.btnEliminar.TabIndex = 88;
            this.btnEliminar.Text = "Eliminar";
            this.btnEliminar.UseVisualStyleBackColor = true;
            this.btnEliminar.Visible = false;
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(16, 86);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(496, 131);
            this.dataGridView1.TabIndex = 89;
            this.dataGridView1.Visible = false;
            // 
            // lblComentario
            // 
            this.lblComentario.AutoSize = true;
            this.lblComentario.Location = new System.Drawing.Point(13, 227);
            this.lblComentario.Name = "lblComentario";
            this.lblComentario.Size = new System.Drawing.Size(71, 13);
            this.lblComentario.TabIndex = 92;
            this.lblComentario.Text = "IDComentario";
            this.lblComentario.Visible = false;
            // 
            // txtIdComentario
            // 
            this.txtIdComentario.Location = new System.Drawing.Point(91, 223);
            this.txtIdComentario.Name = "txtIdComentario";
            this.txtIdComentario.Size = new System.Drawing.Size(100, 20);
            this.txtIdComentario.TabIndex = 93;
            this.txtIdComentario.Visible = false;
            // 
            // lblError2
            // 
            this.lblError2.AutoSize = true;
            this.lblError2.Location = new System.Drawing.Point(197, 227);
            this.lblError2.Name = "lblError2";
            this.lblError2.Size = new System.Drawing.Size(139, 13);
            this.lblError2.TabIndex = 94;
            this.lblError2.Text = "Debe ingresar una ID válida";
            this.lblError2.Visible = false;
            // 
            // EliminarComentario
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(524, 371);
            this.Controls.Add(this.lblError2);
            this.Controls.Add(this.txtIdComentario);
            this.Controls.Add(this.lblComentario);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btnEliminar);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnVolver);
            this.Controls.Add(this.lblErrorID);
            this.Controls.Add(this.btnBuscar);
            this.Controls.Add(this.txtID);
            this.Controls.Add(this.lblID);
            this.Name = "EliminarComentario";
            this.Text = "EliminarComentario";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnVolver;
        private System.Windows.Forms.Label lblErrorID;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.TextBox txtID;
        private System.Windows.Forms.Label lblID;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label lblComentario;
        private System.Windows.Forms.TextBox txtIdComentario;
        private System.Windows.Forms.Label lblError2;
    }
}