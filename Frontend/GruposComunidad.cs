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
        private Label lblAsociarVideo;
        private Panel pnlGruposComunidad;
        private Panel panel2;
        private PictureBox pbxCrearPostGrupo;
       
        private PictureBox pbxFotoGrupo;
        private PictureBox pbxAsociarVideo;
        private Panel pnlChat;
        private Panel panel6;
        private Panel panel4;
        private Label lblPostsGrupo;
        private Label lblChat;
        private Panel pnlPostsGrupo;
        private Panel panel8;
        private Panel panel7;

        private string nombreGrupo;
        private string user;
        private TextBox txtURL;
        private string token;
        private Label lblName;
        private Label lblEditando;
        private string idUltimoMensaje;
        public GruposComunidad(dynamic groupData, string user, string token)
        {
            InitializeComponent();
            this.user = user;
            this.token = token;
            this.nombreGrupo = groupData.nombreReal;
            this.pnlAsociarContenido.Visible = false;
            AplicarDatos(groupData);
            pnlPostsGrupo.Visible = false;
            pnlChat.Visible = true;
            AñadirMensajes();
            lblEditando.Visible = false;
        }
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GruposComunidad));
            this.pbxImagen = new System.Windows.Forms.PictureBox();
            this.lblNombre = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel8 = new System.Windows.Forms.Panel();
            this.panel7 = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.pbxEnviar = new System.Windows.Forms.PictureBox();
            this.pbxAsociarContenido = new System.Windows.Forms.PictureBox();
            this.txtMensajeAEnviar = new System.Windows.Forms.TextBox();
            this.pbxCrearPostGrupo = new System.Windows.Forms.PictureBox();
            this.PictureBoxConfiguraciones = new System.Windows.Forms.PictureBox();
            this.lblMiembros = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.pnlAsociarContenido = new System.Windows.Forms.Panel();
            this.lblAsociarImagen = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.lblAsociarVideo = new System.Windows.Forms.Label();
            this.pbxAsociarVideo = new System.Windows.Forms.PictureBox();
            this.pnlGruposComunidad = new System.Windows.Forms.Panel();
            this.lblName = new System.Windows.Forms.Label();
            this.lblPostsGrupo = new System.Windows.Forms.Label();
            this.lblChat = new System.Windows.Forms.Label();
            this.pbxFotoGrupo = new System.Windows.Forms.PictureBox();
            this.pnlChat = new System.Windows.Forms.Panel();
            this.pnlPostsGrupo = new System.Windows.Forms.Panel();
            this.txtURL = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblEditando = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pbxImagen)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbxEnviar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxAsociarContenido)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxCrearPostGrupo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBoxConfiguraciones)).BeginInit();
            this.pnlAsociarContenido.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxAsociarVideo)).BeginInit();
            this.pnlGruposComunidad.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbxFotoGrupo)).BeginInit();
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
            this.panel1.BackColor = System.Drawing.Color.MediumPurple;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.lblEditando);
            this.panel1.Controls.Add(this.panel8);
            this.panel1.Controls.Add(this.panel7);
            this.panel1.Controls.Add(this.panel6);
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Controls.Add(this.pbxEnviar);
            this.panel1.Controls.Add(this.pbxAsociarContenido);
            this.panel1.Controls.Add(this.txtMensajeAEnviar);
            this.panel1.Location = new System.Drawing.Point(13, 528);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(971, 65);
            this.panel1.TabIndex = 49;
            // 
            // panel8
            // 
            this.panel8.BackColor = System.Drawing.Color.SlateBlue;
            this.panel8.Location = new System.Drawing.Point(967, 3);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(3, 60);
            this.panel8.TabIndex = 79;
            // 
            // panel7
            // 
            this.panel7.BackColor = System.Drawing.Color.SlateBlue;
            this.panel7.Location = new System.Drawing.Point(0, 1);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(3, 60);
            this.panel7.TabIndex = 78;
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.Color.SlateBlue;
            this.panel6.Location = new System.Drawing.Point(0, 61);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(971, 3);
            this.panel6.TabIndex = 77;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.SlateBlue;
            this.panel4.Location = new System.Drawing.Point(-1, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(971, 3);
            this.panel4.TabIndex = 76;
            // 
            // pbxEnviar
            // 
            this.pbxEnviar.Image = ((System.Drawing.Image)(resources.GetObject("pbxEnviar.Image")));
            this.pbxEnviar.Location = new System.Drawing.Point(915, 8);
            this.pbxEnviar.Name = "pbxEnviar";
            this.pbxEnviar.Size = new System.Drawing.Size(50, 50);
            this.pbxEnviar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbxEnviar.TabIndex = 10;
            this.pbxEnviar.TabStop = false;
            this.pbxEnviar.Click += new System.EventHandler(this.pbxEnviar_Click);
            // 
            // pbxAsociarContenido
            // 
            this.pbxAsociarContenido.Image = global::Frontend.Properties.Resources.crear;
            this.pbxAsociarContenido.Location = new System.Drawing.Point(5, 8);
            this.pbxAsociarContenido.Name = "pbxAsociarContenido";
            this.pbxAsociarContenido.Size = new System.Drawing.Size(50, 50);
            this.pbxAsociarContenido.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbxAsociarContenido.TabIndex = 9;
            this.pbxAsociarContenido.TabStop = false;
            this.pbxAsociarContenido.Click += new System.EventHandler(this.pbxAsociarContenido_Click);
            // 
            // txtMensajeAEnviar
            // 
            this.txtMensajeAEnviar.Location = new System.Drawing.Point(59, 8);
            this.txtMensajeAEnviar.MaxLength = 255;
            this.txtMensajeAEnviar.Name = "txtMensajeAEnviar";
            this.txtMensajeAEnviar.Size = new System.Drawing.Size(854, 20);
            this.txtMensajeAEnviar.TabIndex = 8;
            // 
            // pbxCrearPostGrupo
            // 
            this.pbxCrearPostGrupo.Image = global::Frontend.Properties.Resources.crear;
            this.pbxCrearPostGrupo.Location = new System.Drawing.Point(456, 474);
            this.pbxCrearPostGrupo.Name = "pbxCrearPostGrupo";
            this.pbxCrearPostGrupo.Size = new System.Drawing.Size(55, 58);
            this.pbxCrearPostGrupo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbxCrearPostGrupo.TabIndex = 77;
            this.pbxCrearPostGrupo.TabStop = false;
            this.pbxCrearPostGrupo.Visible = false;
            this.pbxCrearPostGrupo.Click += new System.EventHandler(this.pbxCrearPostGrupo_Click);
            // 
            // PictureBoxConfiguraciones
            // 
            this.PictureBoxConfiguraciones.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.PictureBoxConfiguraciones.BackColor = System.Drawing.Color.Transparent;
            this.PictureBoxConfiguraciones.Cursor = System.Windows.Forms.Cursors.Hand;
            this.PictureBoxConfiguraciones.Image = global::Frontend.Properties.Resources.mas_opciones;
            this.PictureBoxConfiguraciones.Location = new System.Drawing.Point(929, 12);
            this.PictureBoxConfiguraciones.Name = "PictureBoxConfiguraciones";
            this.PictureBoxConfiguraciones.Size = new System.Drawing.Size(50, 50);
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
            this.pnlAsociarContenido.BackColor = System.Drawing.Color.MediumPurple;
            this.pnlAsociarContenido.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlAsociarContenido.Controls.Add(this.lblAsociarImagen);
            this.pnlAsociarContenido.Controls.Add(this.pictureBox2);
            this.pnlAsociarContenido.Controls.Add(this.lblAsociarVideo);
            this.pnlAsociarContenido.Controls.Add(this.pbxAsociarVideo);
            this.pnlAsociarContenido.Location = new System.Drawing.Point(9, 399);
            this.pnlAsociarContenido.Name = "pnlAsociarContenido";
            this.pnlAsociarContenido.Size = new System.Drawing.Size(169, 125);
            this.pnlAsociarContenido.TabIndex = 5;
            // 
            // lblAsociarImagen
            // 
            this.lblAsociarImagen.AutoSize = true;
            this.lblAsociarImagen.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAsociarImagen.ForeColor = System.Drawing.SystemColors.Control;
            this.lblAsociarImagen.Location = new System.Drawing.Point(70, 69);
            this.lblAsociarImagen.Name = "lblAsociarImagen";
            this.lblAsociarImagen.Size = new System.Drawing.Size(88, 25);
            this.lblAsociarImagen.TabIndex = 61;
            this.lblAsociarImagen.Text = "Imagen";
            this.lblAsociarImagen.Click += new System.EventHandler(this.pictureBox2_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::Frontend.Properties.Resources.foto_blanca;
            this.pictureBox2.Location = new System.Drawing.Point(15, 60);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(49, 56);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 60;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Click += new System.EventHandler(this.pictureBox2_Click);
            // 
            // lblAsociarVideo
            // 
            this.lblAsociarVideo.AutoSize = true;
            this.lblAsociarVideo.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAsociarVideo.ForeColor = System.Drawing.SystemColors.Control;
            this.lblAsociarVideo.Location = new System.Drawing.Point(70, 18);
            this.lblAsociarVideo.Name = "lblAsociarVideo";
            this.lblAsociarVideo.Size = new System.Drawing.Size(72, 25);
            this.lblAsociarVideo.TabIndex = 59;
            this.lblAsociarVideo.Text = "Video";
            this.lblAsociarVideo.Click += new System.EventHandler(this.pbxAsociarVideo_Click);
            // 
            // pbxAsociarVideo
            // 
            this.pbxAsociarVideo.Image = global::Frontend.Properties.Resources.Video2222;
            this.pbxAsociarVideo.Location = new System.Drawing.Point(15, 9);
            this.pbxAsociarVideo.Name = "pbxAsociarVideo";
            this.pbxAsociarVideo.Size = new System.Drawing.Size(49, 45);
            this.pbxAsociarVideo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbxAsociarVideo.TabIndex = 58;
            this.pbxAsociarVideo.TabStop = false;
            this.pbxAsociarVideo.Click += new System.EventHandler(this.pbxAsociarVideo_Click);
            // 
            // pnlGruposComunidad
            // 
            this.pnlGruposComunidad.AutoScroll = true;
            this.pnlGruposComunidad.Controls.Add(this.lblName);
            this.pnlGruposComunidad.Controls.Add(this.pbxCrearPostGrupo);
            this.pnlGruposComunidad.Controls.Add(this.pnlAsociarContenido);
            this.pnlGruposComunidad.Controls.Add(this.lblPostsGrupo);
            this.pnlGruposComunidad.Controls.Add(this.lblChat);
            this.pnlGruposComunidad.Controls.Add(this.pbxFotoGrupo);
            this.pnlGruposComunidad.Controls.Add(this.panel1);
            this.pnlGruposComunidad.Controls.Add(this.pnlChat);
            this.pnlGruposComunidad.Controls.Add(this.txtURL);
            this.pnlGruposComunidad.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlGruposComunidad.Location = new System.Drawing.Point(0, 0);
            this.pnlGruposComunidad.Name = "pnlGruposComunidad";
            this.pnlGruposComunidad.Size = new System.Drawing.Size(996, 596);
            this.pnlGruposComunidad.TabIndex = 47;
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblName.Location = new System.Drawing.Point(119, 37);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(120, 31);
            this.lblName.TabIndex = 81;
            this.lblName.Text = "lblName";
            // 
            // lblPostsGrupo
            // 
            this.lblPostsGrupo.AutoSize = true;
            this.lblPostsGrupo.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPostsGrupo.ForeColor = System.Drawing.Color.Black;
            this.lblPostsGrupo.Location = new System.Drawing.Point(498, 76);
            this.lblPostsGrupo.Name = "lblPostsGrupo";
            this.lblPostsGrupo.Size = new System.Drawing.Size(66, 24);
            this.lblPostsGrupo.TabIndex = 80;
            this.lblPostsGrupo.Text = "Posts ";
            this.lblPostsGrupo.Click += new System.EventHandler(this.lblPostsGrupo_Click);
            // 
            // lblChat
            // 
            this.lblChat.AutoSize = true;
            this.lblChat.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblChat.ForeColor = System.Drawing.Color.Black;
            this.lblChat.Location = new System.Drawing.Point(407, 76);
            this.lblChat.Name = "lblChat";
            this.lblChat.Size = new System.Drawing.Size(52, 24);
            this.lblChat.TabIndex = 77;
            this.lblChat.Text = "Chat";
            this.lblChat.Click += new System.EventHandler(this.lblChat_Click);
            // 
            // pbxFotoGrupo
            // 
            this.pbxFotoGrupo.Image = global::Frontend.Properties.Resources.Usuario;
            this.pbxFotoGrupo.Location = new System.Drawing.Point(17, 12);
            this.pbxFotoGrupo.Name = "pbxFotoGrupo";
            this.pbxFotoGrupo.Size = new System.Drawing.Size(90, 90);
            this.pbxFotoGrupo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbxFotoGrupo.TabIndex = 78;
            this.pbxFotoGrupo.TabStop = false;
            // 
            // pnlChat
            // 
            this.pnlChat.AutoScroll = true;
            this.pnlChat.BackColor = System.Drawing.Color.LightSteelBlue;
            this.pnlChat.Controls.Add(this.pnlPostsGrupo);
            this.pnlChat.Location = new System.Drawing.Point(13, 113);
            this.pnlChat.Name = "pnlChat";
            this.pnlChat.Size = new System.Drawing.Size(971, 378);
            this.pnlChat.TabIndex = 79;
            // 
            // pnlPostsGrupo
            // 
            this.pnlPostsGrupo.AutoScroll = true;
            this.pnlPostsGrupo.BackColor = System.Drawing.Color.LightSkyBlue;
            this.pnlPostsGrupo.Location = new System.Drawing.Point(0, 0);
            this.pnlPostsGrupo.Name = "pnlPostsGrupo";
            this.pnlPostsGrupo.Size = new System.Drawing.Size(971, 378);
            this.pnlPostsGrupo.TabIndex = 80;
            // 
            // txtURL
            // 
            this.txtURL.Location = new System.Drawing.Point(85, 497);
            this.txtURL.Multiline = true;
            this.txtURL.Name = "txtURL";
            this.txtURL.Size = new System.Drawing.Size(854, 28);
            this.txtURL.TabIndex = 80;
            this.txtURL.Visible = false;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.SlateBlue;
            this.panel2.Location = new System.Drawing.Point(13, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(971, 3);
            this.panel2.TabIndex = 76;
            // 
            // lblEditando
            // 
            this.lblEditando.AutoSize = true;
            this.lblEditando.Location = new System.Drawing.Point(59, 35);
            this.lblEditando.Name = "lblEditando";
            this.lblEditando.Size = new System.Drawing.Size(63, 13);
            this.lblEditando.TabIndex = 80;
            this.lblEditando.Text = "EDITANDO";
            // 
            // GruposComunidad
            // 
            this.ClientSize = new System.Drawing.Size(996, 596);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.lblMiembros);
            this.Controls.Add(this.PictureBoxConfiguraciones);
            this.Controls.Add(this.pnlGruposComunidad);
            this.Controls.Add(this.lblNombre);
            this.Controls.Add(this.pbxImagen);
            this.Name = "GruposComunidad";
            ((System.ComponentModel.ISupportInitialize)(this.pbxImagen)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbxEnviar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxAsociarContenido)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxCrearPostGrupo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBoxConfiguraciones)).EndInit();
            this.pnlAsociarContenido.ResumeLayout(false);
            this.pnlAsociarContenido.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxAsociarVideo)).EndInit();
            this.pnlGruposComunidad.ResumeLayout(false);
            this.pnlGruposComunidad.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbxFotoGrupo)).EndInit();
            this.pnlChat.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void Iniciar()
        {
            pbxFotoGrupo.Visible = true;
            lblMiembros.Visible = true;
            lblChat.Visible = true;
            lblChat.ForeColor = Color.Black;
            lblPostsGrupo.Visible = true;
            lblPostsGrupo.ForeColor = Color.Gray;
            pnlPostsGrupo.Visible = false;
            pnlChat.Visible = true;
            pbxCrearPostGrupo.Visible = false;
            PictureBoxConfiguraciones.Visible = true;
            pnlAsociarContenido.Visible = false;
            panel1.Visible = true;
            txtURL.Visible = false;
        }

        private void AplicarDatos(dynamic groupData)
        {
            this.lblName.Text = groupData.nombreVisible;
            byte[] imagen = Convert.FromBase64String(Convert.ToString(groupData.foto));
            MemoryStream ms = new MemoryStream(imagen);
            Bitmap bitmap = new Bitmap(ms);
            this.pbxFotoGrupo.Image = bitmap;
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

        private async Task<dynamic> EnviarMensaje(string fechayhora, string texto, byte[] imagen, string video)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    if (imagen.Length == 0)
                    {
                        var datos = new { texto = texto, video = video, nombreDeCuenta = user, nombreReal = nombreGrupo, fechaYHora = fechayhora, token = token };
                        var content = new StringContent(JsonConvert.SerializeObject(datos), Encoding.UTF8, "application/json");
                        HttpResponseMessage response = await client.PostAsync("https://localhost:44304/AñadirMensaje", content);
                        response.EnsureSuccessStatusCode();
                        var responseBody = await response.Content.ReadAsStringAsync();
                        dynamic data = JsonConvert.DeserializeObject(responseBody);
                        return data;
                    }
                    else
                    {
                        var datos = new { texto = texto, video = video, imagen = Convert.ToBase64String(imagen), nombreDeCuenta = user, nombreReal = nombreGrupo, fechaYHora = fechayhora, token = token };
                        var content = new StringContent(JsonConvert.SerializeObject(datos), Encoding.UTF8, "application/json");
                        HttpResponseMessage response = await client.PostAsync("https://localhost:44304/AñadirMensaje", content);
                        response.EnsureSuccessStatusCode();
                        var responseBody = await response.Content.ReadAsStringAsync();
                        dynamic data = JsonConvert.DeserializeObject(responseBody);
                        return data;
                    }
                }
                catch (Exception ex)
                {
                    return "Error de conexión";
                }
            }
        }
        private async Task<dynamic> ObtenerMensajes()
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var datos = new { nombreReal = nombreGrupo, token = token };
                    var content = new StringContent(JsonConvert.SerializeObject(datos), Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PutAsync($"https://localhost:44304/ObtenerMensajes", content);
                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();
                    dynamic data = JsonConvert.DeserializeObject(responseBody);
                    return data;
                }
                catch
                {
                    return null;
                }
            }
        }

        private async Task<dynamic> ObtenerMensajesNuevos()
        {
            using (HttpClient client = new HttpClient())
            {
                await Task.Delay(5000);
                try
                {
                    var datos = new { nombreReal = nombreGrupo, idMensaje=idUltimoMensaje, token = token };
                    var content = new StringContent(JsonConvert.SerializeObject(datos), Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PutAsync($"https://localhost:44304/ObtenerMensajesMayorID", content);
                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();
                    dynamic data = JsonConvert.DeserializeObject(responseBody);
                    return data;
                }
                catch
                {
                    return null;
                }
            }
        }

        public async void MensajesNuevos()
        {
            while (true)
            {
                var salida = await ObtenerMensajesNuevos();
                try
                {
                    if (!Convert.ToString(salida).Equals("No se encontraron Mensajes para el grupo especificado") && !Convert.ToString(salida).Equals("Ocurrió un error al intentar obtener los mensajes del grupo.") && !Convert.ToString(salida).Equals("Token expirado"))
                    {
                        AñadirMensajes(salida);
                    }
                }
                catch
                {
                    MessageBox.Show("ERROR");
                }
            }
        }
        private async void AñadirMensajes(dynamic mensajes = null)
        {
            try
            {
                dynamic listaDeMensajes;
                if (mensajes == null)
                {
                    pnlChat.Visible = true;
                    listaDeMensajes = await ObtenerMensajes();
                }
                else
                {
                    listaDeMensajes = mensajes;
                }
                foreach (var mensaje in listaDeMensajes)
                {
                    MessageControl messageControl = new MessageControl(mensaje, token);
                    messageControl.EditarMensaje += MessageControl_EditarMensaje;
                    await messageControl.aplicarDatos(mensaje);
                    if (pnlChat.Controls.Count - 1 > 0)
                    {
                        var lastControl = pnlChat.Controls[pnlChat.Controls.Count - 1];
                        messageControl.Location = new Point(50, lastControl.Bottom);
                    }
                    else
                    {
                        messageControl.Location = new Point(50, 0);
                    }
                    pnlChat.Controls.Add(messageControl);
                    idUltimoMensaje = Convert.ToString(mensaje.idMensaje);
                }
            }
            catch
            {
                MessageBox.Show("no hay mensaje");
            }

        }

        static async Task<dynamic> EditarMensaje(string texto, string idmensaje, string token)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var datos = new { texto = texto, idMensaje=idmensaje, token = token };
                    var content = new StringContent(JsonConvert.SerializeObject(datos), Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PutAsync("https://localhost:44304/ActualizarMensaje", content);
                    response.EnsureSuccessStatusCode();
                    var responseBody = await response.Content.ReadAsStringAsync();
                    dynamic data = JsonConvert.DeserializeObject(responseBody);
                    return data;
                }
                catch (Exception ex)
                {
                    return "Error de conexión";
                }
            }
        }
        private string idMensajeAModificar;
        private void MessageControl_EditarMensaje(object sender, PersonalizedArgs e)
        {
            lblEditando.Visible = true;
            idMensajeAModificar = e.arg;
        }

        private async void pbxEnviar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMensajeAEnviar.Text) && pbxCrearPostGrupo.Image == null) // pbx crar post grupo no le gusta a santi
            {

            }
            else
            {
                if (lblEditando.Visible==false)
                {
                    DateTime fechayhoraactual = DateTime.Now;
                    string fechaHoraString = fechayhoraactual.ToString("yyyy-MM-dd HH:mm:ss");
                    byte[] data;
                    if (pbxCrearPostGrupo.Image == null)
                    {
                        data = new byte[0];
                    }
                    else
                    {
                        MemoryStream ms = new MemoryStream();
                        pbxCrearPostGrupo.Image.Save(ms, ImageFormat.Jpeg);
                        data = ms.ToArray();
                    }
                    string video;
                    string texto;
                    if (txtURL.Text.Contains("https://youtu.be/"))
                    {
                        video = txtMensajeAEnviar.Text;

                    }
                    else
                    {
                        video = "";

                    }
                    texto = txtMensajeAEnviar.Text;
                    MessageBox.Show(data.ToString());
                    var respuesta = await EnviarMensaje(fechaHoraString, texto, data, video);
                    txtMensajeAEnviar.Text = "";
                }
                else
                {
                    string texto = txtMensajeAEnviar.Text;
                    var respuesta = await EditarMensaje(texto, idMensajeAModificar, token);
                    MessageBox.Show(""+respuesta);
                }
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Archivos de imagen|*.png;*.jpg;*.jpeg"; //Para que sólo aparezcan fotos
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                pbxCrearPostGrupo.ImageLocation = ofd.FileName;
                pbxCrearPostGrupo.SizeMode = PictureBoxSizeMode.StretchImage;
                pbxCrearPostGrupo.Visible = true;
            }
        }

        private void lblChat_Click(object sender, EventArgs e)
        {
            pnlPostsGrupo.Visible = false;
            pnlChat.Visible = true;
        }
        static async Task<dynamic> ConseguirPosts(string nombreGrupo, string token)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var dato = new { nombreReal = nombreGrupo, token = token };
                    var content = new StringContent(JsonConvert.SerializeObject(dato), Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PutAsync("https://localhost:44304/ConseguirPostsDeGrupo", content);
                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();
                    dynamic data = JsonConvert.DeserializeObject<DataTable>(responseBody);
                    return data;
                }
                catch
                {
                    return null;
                }
            }
        }
        private async void lblPostsGrupo_Click(object sender, EventArgs e)
        {
            pnlPostsGrupo.Controls.Clear();
            pnlPostsGrupo.Parent = this;
            pnlPostsGrupo.Location = new Point(13, 113);
            pnlChat.Visible = false;
            pnlPostsGrupo.Visible = true;
            pnlPostsGrupo.BringToFront();
            DataTable posts = await ConseguirPosts(nombreGrupo, token);
            if (posts != null)
            {
                for (int i = posts.Rows.Count - 1; i >= 0; i--)
                {
                    int idpost = Convert.ToInt32(posts.Rows[i]["idPost"]);
                    var postControl = new PostControl(idpost, "Claro", user, token); //donde dice claro hay que poner el modo luego
                    await postControl.aplicarDatos();
                    // Calcula la ubicación Y acumulada
                    int currentYPosition = 0;
                    if (pnlPostsGrupo.Controls.Count > 0)
                    {
                        var lastControl = pnlPostsGrupo.Controls[pnlPostsGrupo.Controls.Count - 1];
                        if (postControl.tipo.Equals("imageOnly") || postControl.tipo.Equals("textAndImage"))
                        {
                            await Task.Delay(300);
                        }
                        currentYPosition = lastControl.Bottom;  // La posición inferior del último control agregado
                    }
                    postControl.Location = new Point(0, currentYPosition);
                    pnlPostsGrupo.Controls.Add(postControl);
                }
            }
        }

        private void pbxCrearPostGrupo_Click(object sender, EventArgs e)
        {
            CrearPostGrupo crearPostGrupo = new CrearPostGrupo(user, nombreGrupo, token);
            crearPostGrupo.TopLevel = false;
            crearPostGrupo.FormBorderStyle = FormBorderStyle.None;
            crearPostGrupo.Dock = DockStyle.Fill;
            pnlPostsGrupo.Controls.Add(crearPostGrupo);
            crearPostGrupo.Show();
        }

        private void pbxAsociarVideo_Click(object sender, EventArgs e)
        {
            if(txtURL.Visible == false)
            {
                txtURL.Visible = true;
            } else
            {
                txtURL.Visible = false;
            }
        }
    }
}
