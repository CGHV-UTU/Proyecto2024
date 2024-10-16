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
        private PictureBox pbxImage;
        private string token;
        private PictureBox pictureBox3;
        private string nombreGrupo;
        public GruposComunidad(dynamic groupData, string user, string token)
        {
            InitializeComponent();
            this.user = user;
            this.token = token;
            this.nombreGrupo = groupData.nombreReal;
            this.pnlAsociarContenido.Visible = false;
            AplicarDatos(groupData);
            AñadirMensajes();
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
            this.panel3 = new System.Windows.Forms.Panel();
            this.lblAsociarImagen = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.lblAsociarPost = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pnlChat = new System.Windows.Forms.Panel();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pbxImage = new System.Windows.Forms.PictureBox();
            this.panel2 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.pbxImagen)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbxEnviar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxAsociarContenido)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBoxConfiguraciones)).BeginInit();
            this.pnlAsociarContenido.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.pnlChat.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxImage)).BeginInit();
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
            this.panel1.Location = new System.Drawing.Point(13, 501);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(971, 65);
            this.panel1.TabIndex = 49;
            // 
            // pbxEnviar
            // 
            this.pbxEnviar.Image = ((System.Drawing.Image)(resources.GetObject("pbxEnviar.Image")));
            this.pbxEnviar.Location = new System.Drawing.Point(915, 10);
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
            this.pbxAsociarContenido.Location = new System.Drawing.Point(3, 10);
            this.pbxAsociarContenido.Name = "pbxAsociarContenido";
            this.pbxAsociarContenido.Size = new System.Drawing.Size(50, 50);
            this.pbxAsociarContenido.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbxAsociarContenido.TabIndex = 9;
            this.pbxAsociarContenido.TabStop = false;
            this.pbxAsociarContenido.Click += new System.EventHandler(this.pbxAsociarContenido_Click);
            // 
            // txtMensajeAEnviar
            // 
            this.txtMensajeAEnviar.Location = new System.Drawing.Point(59, 10);
            this.txtMensajeAEnviar.Multiline = true;
            this.txtMensajeAEnviar.Name = "txtMensajeAEnviar";
            this.txtMensajeAEnviar.Size = new System.Drawing.Size(854, 50);
            this.txtMensajeAEnviar.TabIndex = 8;
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
            this.pnlAsociarContenido.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.pnlAsociarContenido.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlAsociarContenido.Controls.Add(this.panel3);
            this.pnlAsociarContenido.Controls.Add(this.lblAsociarImagen);
            this.pnlAsociarContenido.Controls.Add(this.pictureBox2);
            this.pnlAsociarContenido.Controls.Add(this.lblAsociarPost);
            this.pnlAsociarContenido.Controls.Add(this.pictureBox1);
            this.pnlAsociarContenido.Location = new System.Drawing.Point(12, 378);
            this.pnlAsociarContenido.Name = "pnlAsociarContenido";
            this.pnlAsociarContenido.Size = new System.Drawing.Size(168, 117);
            this.pnlAsociarContenido.TabIndex = 5;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.SlateBlue;
            this.panel3.Location = new System.Drawing.Point(0, 122);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(971, 3);
            this.panel3.TabIndex = 76;
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
            // pictureBox2
            // 
            this.pictureBox2.Image = global::Frontend.Properties.Resources.foto_blanca;
            this.pictureBox2.Location = new System.Drawing.Point(15, 60);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(49, 45);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 60;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Click += new System.EventHandler(this.pictureBox2_Click);
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
            // pnlChat
            // 
            this.pnlChat.AutoScroll = true;
            this.pnlChat.Controls.Add(this.pictureBox3);
            this.pnlChat.Controls.Add(this.pbxImage);
            this.pnlChat.Controls.Add(this.pnlAsociarContenido);
            this.pnlChat.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlChat.Location = new System.Drawing.Point(0, 0);
            this.pnlChat.Name = "pnlChat";
            this.pnlChat.Size = new System.Drawing.Size(996, 574);
            this.pnlChat.TabIndex = 47;
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = global::Frontend.Properties.Resources.Usuario;
            this.pictureBox3.Location = new System.Drawing.Point(17, 12);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(90, 90);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox3.TabIndex = 78;
            this.pictureBox3.Visible = true;
            // 
            // pbxImage
            // 
            this.pbxImage.Image = global::Frontend.Properties.Resources.foto_blanca;
            this.pbxImage.Location = new System.Drawing.Point(186, 443);
            this.pbxImage.Name = "pbxImage";
            this.pbxImage.Size = new System.Drawing.Size(55, 58);
            this.pbxImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbxImage.TabIndex = 77;
            this.pbxImage.TabStop = false;
            this.pbxImage.Visible = false;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.SlateBlue;
            this.panel2.Location = new System.Drawing.Point(13, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(971, 3);
            this.panel2.TabIndex = 76;
            // 
            // GruposComunidad
            // 
            this.ClientSize = new System.Drawing.Size(996, 574);
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
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.pnlChat.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void AplicarDatos(dynamic groupData)
        {
            lblNombre.Text = groupData.nombreVisible;
            byte[] imagen = Convert.FromBase64String(Convert.ToString(groupData.foto));
            MemoryStream ms = new MemoryStream(imagen);
            Bitmap bitmap = new Bitmap(ms);
            this.pictureBox3.Image = bitmap;
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
                    var datos = new { texto = texto, video = video, imagen = Convert.ToBase64String(imagen), nombreDeCuenta = user, nombreReal=nombreGrupo, fechaYHora = fechayhora, token = token };
                    var content = new StringContent(JsonConvert.SerializeObject(datos), Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PostAsync("https://localhost:44304/AñadirMensaje", content);
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
        private async Task<dynamic> ObtenerMensajes(string token)
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

        private async void AñadirMensajes()
        {
            pnlChat.Visible = true;
            var listaDeMensajes = await ObtenerMensajes(token);
            foreach(var mensaje in listaDeMensajes)
            {
                MessageControl messageControl = new MessageControl(mensaje, token);
                messageControl.Size=new Size(312, 409);
                await messageControl.aplicarDatos(mensaje);
                if (pnlChat.Controls.Count > 0)
                {
                    var lastControl = pnlChat.Controls[pnlChat.Controls.Count - 1];
                    messageControl.Location = new Point(50, lastControl.Bottom);
                }
                else
                {
                    messageControl.Location = new Point(50, 0);
                }
                pnlChat.Controls.Add(messageControl);
            }
        }
        private async void pbxEnviar_Click(object sender, EventArgs e)
        {
            DateTime fechayhoraactual = DateTime.Now;
            string fechaHoraString = fechayhoraactual.ToString("yyyy-MM-dd HH:mm:ss");
            byte[] data;
            if (pbxImage.Image == null)
            {
                data = new byte[0];
            }
            else
            {
                MemoryStream ms = new MemoryStream();
                pbxImage.Image.Save(ms, ImageFormat.Jpeg);
                data = ms.ToArray();
            }
            string video;
            string texto;
            if (txtMensajeAEnviar.Text.Contains("https://youtu.be/"))
            {
                video = txtMensajeAEnviar.Text;
                texto = "";
            }
            else
            {
                video = "";
                texto = txtMensajeAEnviar.Text;
            }
            var respuesta=await EnviarMensaje(fechaHoraString, texto, data, video);
            MessageBox.Show(""+respuesta);
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Archivos de imagen|*.png;*.jpg;*.jpeg"; //Para que sólo aparezcan fotos
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                pbxImage.ImageLocation = ofd.FileName;
                pbxImage.SizeMode = PictureBoxSizeMode.StretchImage;
                pbxImage.Visible = true;
            }
        }
    }
}
