using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Frontend
{
    public partial class CrearPostGrupo : Form
    {
        private string user;
        private string token;
        private Panel pnlURL;
        private Panel pnlTexto;
        private TextBox txtUrl;
        private PictureBox pbxImagen;
        private PictureBox btnVideo;
        private PictureBox btnImagen;
        private TextBox txtTexto;
        private PictureBox btnCrear;


        private string nombreReal;


        public event EventHandler Creado;
        public event EventHandler Salir;
        public event EventHandler CambiaTamaño;
        public CrearPostGrupo(string usuario, string nombreReal, string token)//Deberíamos sacar el "" en idevento
        {
            InitializeComponent();
            this.token = token;
            this.nombreReal = nombreReal;
            this.user = usuario;
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CrearPostGrupo));
            this.pnlURL = new System.Windows.Forms.Panel();
            this.pnlTexto = new System.Windows.Forms.Panel();
            this.txtUrl = new System.Windows.Forms.TextBox();
            this.txtTexto = new System.Windows.Forms.TextBox();
            this.pbxImagen = new System.Windows.Forms.PictureBox();
            this.btnVideo = new System.Windows.Forms.PictureBox();
            this.btnImagen = new System.Windows.Forms.PictureBox();
            this.btnCrear = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbxImagen)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnVideo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnImagen)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnCrear)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlURL
            // 
            this.pnlURL.BackColor = System.Drawing.Color.SlateBlue;
            this.pnlURL.Location = new System.Drawing.Point(19, 258);
            this.pnlURL.Name = "pnlURL";
            this.pnlURL.Size = new System.Drawing.Size(381, 3);
            this.pnlURL.TabIndex = 82;
            // 
            // pnlTexto
            // 
            this.pnlTexto.BackColor = System.Drawing.Color.SlateBlue;
            this.pnlTexto.Location = new System.Drawing.Point(19, 167);
            this.pnlTexto.Name = "pnlTexto";
            this.pnlTexto.Size = new System.Drawing.Size(381, 3);
            this.pnlTexto.TabIndex = 81;
            // 
            // txtUrl
            // 
            this.txtUrl.BackColor = System.Drawing.SystemColors.Window;
            this.txtUrl.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUrl.ForeColor = System.Drawing.Color.Gray;
            this.txtUrl.Location = new System.Drawing.Point(19, 235);
            this.txtUrl.MaxLength = 50;
            this.txtUrl.Name = "txtUrl";
            this.txtUrl.Size = new System.Drawing.Size(381, 26);
            this.txtUrl.TabIndex = 80;
            // 
            // txtTexto
            // 
            this.txtTexto.BackColor = System.Drawing.SystemColors.Window;
            this.txtTexto.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTexto.ForeColor = System.Drawing.Color.Gray;
            this.txtTexto.Location = new System.Drawing.Point(19, 35);
            this.txtTexto.MaxLength = 100;
            this.txtTexto.Multiline = true;
            this.txtTexto.Name = "txtTexto";
            this.txtTexto.Size = new System.Drawing.Size(381, 135);
            this.txtTexto.TabIndex = 76;
            this.txtTexto.Text = "Texto";
            this.txtTexto.Enter += new System.EventHandler(this.txtTexto_Enter);
            this.txtTexto.Leave += new System.EventHandler(this.txtTexto_Leave);
            // 
            // pbxImagen
            // 
            this.pbxImagen.Location = new System.Drawing.Point(19, 263);
            this.pbxImagen.Name = "pbxImagen";
            this.pbxImagen.Size = new System.Drawing.Size(381, 212);
            this.pbxImagen.TabIndex = 79;
            this.pbxImagen.TabStop = false;
            this.pbxImagen.Visible = false;
            // 
            // btnVideo
            // 
            this.btnVideo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnVideo.BackColor = System.Drawing.Color.Transparent;
            this.btnVideo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnVideo.Image = global::Frontend.Properties.Resources.Video2222;
            this.btnVideo.Location = new System.Drawing.Point(295, 176);
            this.btnVideo.Name = "btnVideo";
            this.btnVideo.Size = new System.Drawing.Size(50, 50);
            this.btnVideo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.btnVideo.TabIndex = 78;
            this.btnVideo.TabStop = false;
            this.btnVideo.Click += new System.EventHandler(this.btnVideo_Click);
            // 
            // btnImagen
            // 
            this.btnImagen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnImagen.BackColor = System.Drawing.Color.Transparent;
            this.btnImagen.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnImagen.Image = global::Frontend.Properties.Resources.Foto;
            this.btnImagen.Location = new System.Drawing.Point(351, 176);
            this.btnImagen.Name = "btnImagen";
            this.btnImagen.Size = new System.Drawing.Size(50, 50);
            this.btnImagen.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.btnImagen.TabIndex = 77;
            this.btnImagen.TabStop = false;
            this.btnImagen.Click += new System.EventHandler(this.btnImagen_Click);
            // 
            // btnCrear
            // 
            this.btnCrear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCrear.BackColor = System.Drawing.Color.Transparent;
            this.btnCrear.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCrear.Image = ((System.Drawing.Image)(resources.GetObject("btnCrear.Image")));
            this.btnCrear.Location = new System.Drawing.Point(21, 406);
            this.btnCrear.Name = "btnCrear";
            this.btnCrear.Size = new System.Drawing.Size(376, 89);
            this.btnCrear.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnCrear.TabIndex = 83;
            this.btnCrear.TabStop = false;
            this.btnCrear.Click += new System.EventHandler(this.btnCrear_Click);
            // 
            // CrearPostGrupo
            // 
            this.ClientSize = new System.Drawing.Size(420, 511);
            this.Controls.Add(this.btnCrear);
            this.Controls.Add(this.pnlURL);
            this.Controls.Add(this.pnlTexto);
            this.Controls.Add(this.txtUrl);
            this.Controls.Add(this.pbxImagen);
            this.Controls.Add(this.btnVideo);
            this.Controls.Add(this.btnImagen);
            this.Controls.Add(this.txtTexto);
            this.Name = "CrearPostGrupo";
            ((System.ComponentModel.ISupportInitialize)(this.pbxImagen)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnVideo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnImagen)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnCrear)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private async void btnCrear_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtTexto.Text) && pbxImagen.Image == null && string.IsNullOrEmpty(txtUrl.Text))
            {
                MessageBox.Show("No puede realizar un post sin contenido", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                DateTime fechayhoraactual = DateTime.Now;
                string fechaHoraString = fechayhoraactual.ToString("yyyy-MM-dd HH:mm:ss");
                if (pbxImagen.Image == null)
                {
                    byte[] data = new byte[0];
                    await Publicar(user, txtTexto.Text, txtUrl.Text, data, fechaHoraString, token, nombreReal);
                    MessageBox.Show("El post se creó correctamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Creado?.Invoke(this, EventArgs.Empty);
                }
                else
                {
                    MemoryStream ms = new MemoryStream();
                    pbxImagen.Image.Save(ms, ImageFormat.Jpeg);
                    byte[] data = ms.ToArray();
                    await Publicar(user, txtTexto.Text, txtUrl.Text, data, fechaHoraString, token, nombreReal);
                    MessageBox.Show("El post se creó correctamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Creado?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        private void btnVideo_Click(object sender, EventArgs e)
        {
            if (txtUrl.Visible == false)
            {
                if (pbxImagen.Visible == true)
                {
                    MessageBox.Show("No puede crear un post con Imagen y video", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    txtUrl.Visible = true;
                    txtUrl.Text = "Url";
                    pnlURL.Visible = true;
                }
            }
            else
            {
                pnlURL.Visible = false;
                txtUrl.Visible = false;
                txtUrl.Text = "";
            }
        }

        private void btnImagen_Click(object sender, EventArgs e)
        {
            if (pbxImagen.Visible == false)
            {
                if (txtUrl.Visible == true)
                {
                    MessageBox.Show("No puede crear un post con Imagen y video", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    CambiaTamaño?.Invoke(this, EventArgs.Empty);
                    this.Height = 692;
                    btnCrear.Location = new Point(16, 445);
                    OpenFileDialog ofd = new OpenFileDialog();
                    ofd.Filter = "Archivos de imagen|*.png;*.jpg;*.jpeg"; //Para que sólo aparezcan fotos
                    if (ofd.ShowDialog() == DialogResult.OK)
                    {
                        pbxImagen.ImageLocation = ofd.FileName;
                        pbxImagen.SizeMode = PictureBoxSizeMode.StretchImage;
                        pbxImagen.Visible = true;
                    }
                }
            }
            else
            {
                pbxImagen.Visible = true;
            }
        }

        //Por revisar
        public static async Task Publicar(string user, string texto, string url, byte[] imagen, string fechaHora, string token, string nombreReal)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    if (imagen.Length == 0)
                    {
                        var datos = new { text = texto, link = url, user = user, fechayhora = fechaHora, token = token, nombreReal = nombreReal  };
                        var content = new StringContent(JsonConvert.SerializeObject(datos), Encoding.UTF8, "application/json");
                        HttpResponseMessage response = await client.PostAsync("https://localhost:44340/postear", content);
                        response.EnsureSuccessStatusCode();
                    }
                    else
                    {
                        var datos = new { text = texto, link = url, image = Convert.ToBase64String(imagen), user = user, fechayhora = fechaHora, token = token , nombreReal = nombreReal };
                        var content = new StringContent(JsonConvert.SerializeObject(datos), Encoding.UTF8, "application/json");
                        HttpResponseMessage response = await client.PostAsync("https://localhost:44340/postear", content);
                        response.EnsureSuccessStatusCode();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                    Console.ReadLine();
                }
            }
        }

        private void txtTexto_Enter(object sender, EventArgs e)
        {
            if (txtTexto.Text == "Texto")
            {
                txtTexto.Text = "";
            }
        }

        private void txtTexto_Leave(object sender, EventArgs e)
        {
            if (txtTexto.Text == "")
            {
                txtTexto.Text = "Texto";
            }
        }
    }
}
