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
    public partial class EventoComunidad : Form
    {
        //Estos tres ya están con el EventData
        //  private static string user;
        //  private string token;
        //  private string evento;
        private DateTimePicker dtpFechaFinal;
        private DateTimePicker dtpFechaInicio;
        private TextBox txtDescripcion;
        private TextBox txtNombre;
        private PictureBox btnUbicacion;
        private PictureBox pbxImagen;
        //Todo esto está basado en la ventana "Post" que te deja crear
        //publicaciones, eventos y grupos.
        //Claramente no probé nada.
//⡴⠒⣄⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣼⠉⠳⡆⠀
//⣇⠰⠉⢙⡄⠀⠀⣴⠖⢦⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠘⣆⠁⠙⡆
//⡇⢠⠞⠉⠙⣾⠃⢀⡼⠀⠀⠀⠀⠀⠀⠀⢀⣼⡀⠄⢷⣄⣀⠀⠀⠀⠀⠀⠀⠀⠰⠒⠲⡄⠀⣏⣆⣀⡍
//⢠⡏⠀⡤⠒⠃⠀⡜⠀⠀⠀⠀⠀⢀⣴⠾⠛⡁⠀⠀⢀⣈⡉⠙⠳⣤⡀⠀⠀⠀⠘⣆⠀⣇⡼⢋⠀⠀⢱
//⠀⠘⣇⠀⠀⠀⠀⠀⡇⠀⠀⠀⠀⡴⢋⡣⠊⡩⠋⠀⠀⠀⠣⡉⠲⣄⠀⠙⢆⠀⠀⠀⣸⠀⢉⠀⢀⠿⠀⢸
//⠀⠀⠸⡄⠀⠈⢳⣄⡇⠀⠀⢀⡞⠀⠈⠀⢀⣴⣾⣿⣿⣿⣿⣦⡀⠀⠀⠀⠈⢧⠀⠀⢳⣰⠁⠀⠀⠀⣠⠃
//⠀⠀⠀⠘⢄⣀⣸⠃⠀⠀⠀⡸⠀⠀⠀⢠⣿⣿⣿⣿⣿⣿⣿⣿⣿⣆⠀⠀⠀⠈⣇⠀⠀⠙⢄⣀⠤⠚⠁⠀
//⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⡇⠀⠀⢠⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡄⠀⠀⠀⢹⠀⠀⠀⠀⠀⠀⠀⠀⠀
//⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⡀⠀⠀⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡀⠀⠀⢘⠀⠀⠀⠀⠀⠀⠀⠀⠀
//⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⡇⠀⢰⣿⣿⣿⡿⠛⠁⠀⠉⠛⢿⣿⣿⣿⣧⠀⠀⣼⠀⠀⠀⠀⠀⠀⠀⠀⠀
//⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢠⡀⣸⣿⣿⠟⠀⠀⠀⠀⠀⠀⠀⢻⣿⣿⣿⡀⢀⠇⠀⠀⠀⠀⠀⠀⠀⠀⠀
//⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠘⡇⠹⠿⠋⠀⠀⠀⠀⠀⠀⠀⠀⠀⠙⢿⡿⠁⡏⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
//⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠻⣤⣞⠁⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢢⣀⣠⠇⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
//⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠙⠲⢤⣀⣀⠀⢀⣀⣀⠤⠒⠉⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
        public class EventData
        {
            public string id { get; set; }
            public string titulo { get; set; } = "";
            public string ubicacion { get; set; } = "";
            public string descripcion { get; set; } = "";
            public string foto { get; set; } = "";
            public string fechaYhora_Inicio { get; set; } = "";
            public string fechaYhora_Final { get; set; } = "";
            public string user { get; set; }
            public string token { get; set; }
        }

        //Recibo los datos de evento desde Grupo-EventoParaListar.
        //No tiene sentido buscarlos de nuevo acá si ya en la
        //ventana anterior los tenemos guardados en un datatable.
        public EventoComunidad(EventData EventData)
        {
            InitializeComponent();
            AplicarDatos(EventData);
        }
        
       //Tuqui
        private void InitializeComponent()
        {
            this.dtpFechaFinal = new System.Windows.Forms.DateTimePicker();
            this.dtpFechaInicio = new System.Windows.Forms.DateTimePicker();
            this.txtDescripcion = new System.Windows.Forms.TextBox();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.pbxImagen = new System.Windows.Forms.PictureBox();
            this.btnUbicacion = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbxImagen)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnUbicacion)).BeginInit();
            this.SuspendLayout();
            // 
            // dtpFechaFinal
            // 
            this.dtpFechaFinal.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.dtpFechaFinal.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaFinal.Location = new System.Drawing.Point(13, 189);
            this.dtpFechaFinal.Name = "dtpFechaFinal";
            this.dtpFechaFinal.ShowUpDown = true;
            this.dtpFechaFinal.Size = new System.Drawing.Size(200, 20);
            this.dtpFechaFinal.TabIndex = 43;
            // 
            // dtpFechaInicio
            // 
            this.dtpFechaInicio.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.dtpFechaInicio.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaInicio.Location = new System.Drawing.Point(13, 163);
            this.dtpFechaInicio.Name = "dtpFechaInicio";
            this.dtpFechaInicio.ShowUpDown = true;
            this.dtpFechaInicio.Size = new System.Drawing.Size(200, 20);
            this.dtpFechaInicio.TabIndex = 42;
            // 
            // txtDescripcion
            // 
            this.txtDescripcion.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.txtDescripcion.Location = new System.Drawing.Point(10, 47);
            this.txtDescripcion.Multiline = true;
            this.txtDescripcion.Name = "txtDescripcion";
            this.txtDescripcion.Size = new System.Drawing.Size(381, 86);
            this.txtDescripcion.TabIndex = 41;
            // 
            // txtNombre
            // 
            this.txtNombre.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.txtNombre.Location = new System.Drawing.Point(10, 21);
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(381, 20);
            this.txtNombre.TabIndex = 40;
            // 
            // pbxImagen
            // 
            this.pbxImagen.Location = new System.Drawing.Point(10, 218);
            this.pbxImagen.Name = "pbxImagen";
            this.pbxImagen.Size = new System.Drawing.Size(381, 212);
            this.pbxImagen.TabIndex = 38;
            this.pbxImagen.TabStop = false;
            this.pbxImagen.Visible = false;
            // 
            // btnUbicacion
            // 
            this.btnUbicacion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUbicacion.BackColor = System.Drawing.Color.Transparent;
            this.btnUbicacion.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnUbicacion.Image = global::Frontend.Properties.Resources.buscar;
            this.btnUbicacion.Location = new System.Drawing.Point(219, 159);
            this.btnUbicacion.Name = "btnUbicacion";
            this.btnUbicacion.Size = new System.Drawing.Size(50, 50);
            this.btnUbicacion.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.btnUbicacion.TabIndex = 44;
            this.btnUbicacion.TabStop = false;
            // 
            // Evento
            // 
            this.ClientSize = new System.Drawing.Size(400, 450);
            this.Controls.Add(this.btnUbicacion);
            this.Controls.Add(this.dtpFechaFinal);
            this.Controls.Add(this.dtpFechaInicio);
            this.Controls.Add(this.txtDescripcion);
            this.Controls.Add(this.txtNombre);
            this.Controls.Add(this.pbxImagen);
            this.Name = "Evento";
            ((System.ComponentModel.ISupportInitialize)(this.pbxImagen)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnUbicacion)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        //El nombre lo dice todo.
        private void AplicarDatos(EventData EventData)
        {
            txtNombre.Text = EventData.titulo;
            txtDescripcion.Text = EventData.descripcion;
            dtpFechaInicio.Text = EventData.fechaYhora_Inicio;
            dtpFechaFinal.Text = EventData.fechaYhora_Final;

            //Creo que está bien?? 
            byte[] imagen = Convert.FromBase64String(Convert.ToString(EventData.foto));
            MemoryStream ms = new MemoryStream(imagen);
            Bitmap bitmap = new Bitmap(ms);
            this.pbxImagen.Image = bitmap;
        }


        
    }
}
