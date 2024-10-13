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
            this.pbxImagen = new System.Windows.Forms.PictureBox();
            this.lblNombre = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.pbxImagen)).BeginInit();
            this.SuspendLayout();
            // 
            // pbxImagen
            // 
            this.pbxImagen.Location = new System.Drawing.Point(12, 12);
            this.pbxImagen.Name = "pbxImagen";
            this.pbxImagen.Size = new System.Drawing.Size(90, 90);
            this.pbxImagen.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbxImagen.TabIndex = 36;
            this.pbxImagen.TabStop = false;
            // 
            // lblNombre
            // 
            this.lblNombre.AutoSize = true;
            this.lblNombre.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNombre.Location = new System.Drawing.Point(148, 12);
            this.lblNombre.Name = "lblNombre";
            this.lblNombre.Size = new System.Drawing.Size(70, 25);
            this.lblNombre.TabIndex = 46;
            this.lblNombre.Text = "label1";
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(13, 108);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(971, 454);
            this.panel1.TabIndex = 47;
            // 
            // GruposComunidad
            // 
            this.ClientSize = new System.Drawing.Size(996, 574);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lblNombre);
            this.Controls.Add(this.pbxImagen);
            this.Name = "GruposComunidad";
            ((System.ComponentModel.ISupportInitialize)(this.pbxImagen)).EndInit();
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
    }
}
