using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net.Http;
using System.Windows.Forms;

namespace Frontend
{
    //Misma lógica que en EventoComunidad: Recibo los datos
    //de la ventana anterior y los muestro.
    class GruposComunidad : Form
    {
        private PictureBox pbxImagen;
        private string user;
        private Label lblNombre;
        private Panel panel1;
        private PictureBox pbxEnviar;
        private PictureBox pbxAsociarContenido;
        private TextBox txtMensajeAEnviar;
        private PictureBox PictureBoxConfiguraciones;
        private Label lblMiembros;
        private Panel panel5;
        private Panel pnlAsociarContenido;
        private Label lblAsociarImagen;
        private PictureBox pictureBox2;
        private Label lblAsociarPost;
        private PictureBox pictureBox1;
        private Panel pnlChat;
        private Panel panel2;
        private Panel panel3;
        private string token;
        public GruposComunidad(dynamic groupData, string user, string token)
        {
            InitializeComponent();
            this.user = user;
            this.token = token;
            AplicarDatos(groupData);
        }
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GruposComunidad));
            this.pbxImagen = new System.Windows.Forms.PictureBox();
            this.lblNombre = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pbxEnviar = new System.Windows.Forms.PictureBox();
            this.pbxAsociarContenido = new System.Windows.Forms.PictureBox();
            this.txtMensajeAEnviar = new System.Windows.Forms.TextBox();
            this.PictureBoxConfiguraciones = new System.Windows.Forms.PictureBox();
            this.lblMiembros = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.pnlAsociarContenido = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblAsociarPost = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.lblAsociarImagen = new System.Windows.Forms.Label();
            this.pnlChat = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.pbxImagen)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbxEnviar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxAsociarContenido)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBoxConfiguraciones)).BeginInit();
            this.pnlAsociarContenido.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.pnlChat.SuspendLayout();
            this.SuspendLayout();
            // 
            // pbxImagen
            // 
            this.pbxImagen.Image = global::Frontend.Properties.Resources.User;
            this.pbxImagen.Location = new System.Drawing.Point(17, 12);
            this.pbxImagen.Name = "pbxImagen";
            this.pbxImagen.Size = new System.Drawing.Size(90, 90);
            this.pbxImagen.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbxImagen.TabIndex = 36;
            this.pbxImagen.TabStop = false;
            // 
            // lblNombre
            // 
            this.lblNombre.AutoSize = true;
            this.lblNombre.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNombre.Location = new System.Drawing.Point(116, 22);
            this.lblNombre.Name = "lblNombre";
            this.lblNombre.Size = new System.Drawing.Size(343, 31);
            this.lblNombre.TabIndex = 46;
            this.lblNombre.Text = "NombreDeUsuario/Grupo";
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.pbxEnviar);
            this.panel1.Controls.Add(this.pbxAsociarContenido);
            this.panel1.Controls.Add(this.txtMensajeAEnviar);
            this.panel1.Location = new System.Drawing.Point(13, 473);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(971, 93);
            this.panel1.TabIndex = 49;
            // 
            // pbxEnviar
            // 
            this.pbxEnviar.Image = ((System.Drawing.Image)(resources.GetObject("pbxEnviar.Image")));
            this.pbxEnviar.Location = new System.Drawing.Point(866, 3);
            this.pbxEnviar.Name = "pbxEnviar";
            this.pbxEnviar.Size = new System.Drawing.Size(99, 85);
            this.pbxEnviar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbxEnviar.TabIndex = 10;
            this.pbxEnviar.TabStop = false;
            // 
            // pbxAsociarContenido
            // 
            this.pbxAsociarContenido.Image = global::Frontend.Properties.Resources.crear;
            this.pbxAsociarContenido.Location = new System.Drawing.Point(3, 3);
            this.pbxAsociarContenido.Name = "pbxAsociarContenido";
            this.pbxAsociarContenido.Size = new System.Drawing.Size(99, 85);
            this.pbxAsociarContenido.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbxAsociarContenido.TabIndex = 9;
            this.pbxAsociarContenido.TabStop = false;
            this.pbxAsociarContenido.Click += new System.EventHandler(this.pbxAsociarContenido_Click);
            // 
            // txtMensajeAEnviar
            // 
            this.txtMensajeAEnviar.Location = new System.Drawing.Point(108, 3);
            this.txtMensajeAEnviar.Multiline = true;
            this.txtMensajeAEnviar.Name = "txtMensajeAEnviar";
            this.txtMensajeAEnviar.Size = new System.Drawing.Size(752, 85);
            this.txtMensajeAEnviar.TabIndex = 8;
            // 
            // PictureBoxConfiguraciones
            // 
            this.PictureBoxConfiguraciones.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.PictureBoxConfiguraciones.BackColor = System.Drawing.Color.Transparent;
            this.PictureBoxConfiguraciones.Cursor = System.Windows.Forms.Cursors.Hand;
            this.PictureBoxConfiguraciones.Image = global::Frontend.Properties.Resources.mas_opciones;
            this.PictureBoxConfiguraciones.Location = new System.Drawing.Point(900, 12);
            this.PictureBoxConfiguraciones.Name = "PictureBoxConfiguraciones";
            this.PictureBoxConfiguraciones.Size = new System.Drawing.Size(79, 74);
            this.PictureBoxConfiguraciones.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.PictureBoxConfiguraciones.TabIndex = 50;
            this.PictureBoxConfiguraciones.TabStop = false;
            // 
            // lblMiembros
            // 
            this.lblMiembros.AutoSize = true;
            this.lblMiembros.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMiembros.ForeColor = System.Drawing.Color.Gray;
            this.lblMiembros.Location = new System.Drawing.Point(118, 66);
            this.lblMiembros.Name = "lblMiembros";
            this.lblMiembros.Size = new System.Drawing.Size(157, 20);
            this.lblMiembros.TabIndex = 51;
            this.lblMiembros.Text = "Miembro1, Miembro2";
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.SlateBlue;
            this.panel5.Location = new System.Drawing.Point(13, 103);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(971, 3);
            this.panel5.TabIndex = 75;
            // 
            // pnlAsociarContenido
            // 
            this.pnlAsociarContenido.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.pnlAsociarContenido.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlAsociarContenido.Controls.Add(this.lblAsociarImagen);
            this.pnlAsociarContenido.Controls.Add(this.pictureBox2);
            this.pnlAsociarContenido.Controls.Add(this.lblAsociarPost);
            this.pnlAsociarContenido.Controls.Add(this.pictureBox1);
            this.pnlAsociarContenido.Location = new System.Drawing.Point(4, 235);
            this.pnlAsociarContenido.Name = "pnlAsociarContenido";
            this.pnlAsociarContenido.Size = new System.Drawing.Size(168, 117);
            this.pnlAsociarContenido.TabIndex = 5;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Frontend.Properties.Resources.crear;
            this.pictureBox1.Location = new System.Drawing.Point(15, 9);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(49, 45);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 58;
            this.pictureBox1.TabStop = false;
            // 
            // lblAsociarPost
            // 
            this.lblAsociarPost.AutoSize = true;
            this.lblAsociarPost.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAsociarPost.Location = new System.Drawing.Point(70, 18);
            this.lblAsociarPost.Name = "lblAsociarPost";
            this.lblAsociarPost.Size = new System.Drawing.Size(55, 25);
            this.lblAsociarPost.TabIndex = 59;
            this.lblAsociarPost.Text = "Post";
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::Frontend.Properties.Resources.foto_blanca;
            this.pictureBox2.Location = new System.Drawing.Point(15, 60);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(49, 45);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 60;
            this.pictureBox2.TabStop = false;
            // 
            // lblAsociarImagen
            // 
            this.lblAsociarImagen.AutoSize = true;
            this.lblAsociarImagen.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAsociarImagen.Location = new System.Drawing.Point(70, 69);
            this.lblAsociarImagen.Name = "lblAsociarImagen";
            this.lblAsociarImagen.Size = new System.Drawing.Size(82, 25);
            this.lblAsociarImagen.TabIndex = 61;
            this.lblAsociarImagen.Text = "Imagen";
            // 
            // pnlChat
            // 
            this.pnlChat.Controls.Add(this.pnlAsociarContenido);
            this.pnlChat.Location = new System.Drawing.Point(13, 112);
            this.pnlChat.Name = "pnlChat";
            this.pnlChat.Size = new System.Drawing.Size(971, 355);
            this.pnlChat.TabIndex = 47;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.SlateBlue;
            this.panel2.Location = new System.Drawing.Point(13, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(971, 3);
            this.panel2.TabIndex = 76;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.SlateBlue;
            this.panel3.Location = new System.Drawing.Point(13, 468);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(971, 3);
            this.panel3.TabIndex = 76;
            // 
            // GruposComunidad
            // 
            this.ClientSize = new System.Drawing.Size(996, 574);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.lblMiembros);
            this.Controls.Add(this.PictureBoxConfiguraciones);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pnlChat);
            this.Controls.Add(this.lblNombre);
            this.Controls.Add(this.pbxImagen);
            this.Name = "GruposComunidad";
            ((System.ComponentModel.ISupportInitialize)(this.pbxImagen)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbxEnviar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxAsociarContenido)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBoxConfiguraciones)).EndInit();
            this.pnlAsociarContenido.ResumeLayout(false);
            this.pnlAsociarContenido.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.pnlChat.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void AplicarDatos(dynamic groupData)
        {
            lblNombre.Text = groupData.nombreVisible;
            byte[] imagen = Convert.FromBase64String(Convert.ToString(groupData.foto));
            MemoryStream ms = new MemoryStream(imagen);
            Bitmap bitmap = new Bitmap(ms);
            this.pbxImagen.Image = bitmap;
        }

        private void pbxAsociarContenido_Click(object sender, EventArgs e)
        {
            if (pnlAsociarContenido.Visible)
            {
                pnlAsociarContenido.Visible = false;
            } else
            {
                pnlAsociarContenido.Visible = true;
            }
        }
    }
}
