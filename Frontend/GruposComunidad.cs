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
        private TextBox txtDescripcion;
        private TextBox txtNombre;
        private PictureBox pbxImagen;
        public class GroupData
        {
            public string nombreReal { get; set; }
            public string nombreVisible { get; set; }
            public string configuracion { get; set; }
            public string descripcion { get; set; }
            public string foto { get; set; }

        }

        public GruposComunidad(GroupData groupData)
        {
            InitializeComponent();
            AplicarDatos(groupData);
        }
        private void InitializeComponent()
        {
            this.txtDescripcion = new System.Windows.Forms.TextBox();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.pbxImagen = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbxImagen)).BeginInit();
            this.SuspendLayout();
            // 
            // txtDescripcion
            // 
            this.txtDescripcion.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.txtDescripcion.Location = new System.Drawing.Point(10, 47);
            this.txtDescripcion.Multiline = true;
            this.txtDescripcion.Name = "txtDescripcion";
            this.txtDescripcion.Size = new System.Drawing.Size(381, 86);
            this.txtDescripcion.TabIndex = 38;
            // 
            // txtNombre
            // 
            this.txtNombre.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.txtNombre.Location = new System.Drawing.Point(10, 21);
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(381, 20);
            this.txtNombre.TabIndex = 37;
            // 
            // pbxImagen
            // 
            this.pbxImagen.Location = new System.Drawing.Point(10, 218);
            this.pbxImagen.Name = "pbxImagen";
            this.pbxImagen.Size = new System.Drawing.Size(381, 212);
            this.pbxImagen.TabIndex = 36;
            this.pbxImagen.TabStop = false;
            this.pbxImagen.Visible = false;
            // 
            // GruposComunidad
            // 
            this.ClientSize = new System.Drawing.Size(400, 450);
            this.Controls.Add(this.txtDescripcion);
            this.Controls.Add(this.txtNombre);
            this.Controls.Add(this.pbxImagen);
            this.Name = "GruposComunidad";
            ((System.ComponentModel.ISupportInitialize)(this.pbxImagen)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void AplicarDatos(GroupData groupData)
        {
            txtNombre.Text = groupData.nombreVisible;
            txtDescripcion.Text = groupData.descripcion;
            byte[] imagen = Convert.FromBase64String(Convert.ToString(groupData.foto));
            MemoryStream ms = new MemoryStream(imagen);
            Bitmap bitmap = new Bitmap(ms);
            this.pbxImagen.Image = bitmap;
        }
    }
}
