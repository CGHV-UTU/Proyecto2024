using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Frontend
{
    public partial class MessageControl : UserControl
    {
        // Agregar: AplicarDatos();


        private string idMensaje;
        private string token;
        private string user;
        private string creador;
        private Label lblNombreDeCuenta;
        private TextBox txtMensaje;
        private PictureBox pbxFotoUsuario;
        private Label lblFechaYHora; //Capaz que registro el año también, porque en el DDL es
        private PictureBox pbxImagenCompartida;
        private Panel pnlOpciones;
        private TextBox txtURL;
        private PictureBox pbxOpciones;
        private Label btnEliminar;
        private Label btnEditar;
        private Panel panel1;
        public event EventHandler<PersonalizedArgs> EditarMensaje;
        public event EventHandler<PersonalizedArgs> MensajeEliminado;

        public MessageControl(dynamic MessageData, string user, string token)
        {
            InitializeComponent();
            txtMensaje.ReadOnly = true;
            this.token = token;
            this.user = user;
            this.creador = Convert.ToString(MessageData.nombreDeCuenta);
            this.idMensaje = Convert.ToString(MessageData.idMensaje);
            btnEditar.Visible = false;
            btnEliminar.Visible = false;
        }


        public async Task aplicarDatos(dynamic MessageData)
        {
            //anda mas o menos aun, no carga las imagenes ni del usuario ni la del mensaje
            this.lblNombreDeCuenta.Text = MessageData.nombreDeCuenta;
            this.txtMensaje.Text = MessageData.texto;
            this.lblFechaYHora.Text = MessageData.fechaYHora;
            if (!string.IsNullOrEmpty(Convert.ToString(MessageData.imagen)))
            {
                byte[] imagen = Convert.FromBase64String(Convert.ToString(MessageData.imagen));
                MemoryStream ms = new MemoryStream(imagen);
                Bitmap bitmap = new Bitmap(ms);
                this.pbxImagenCompartida.Image = bitmap;
            }
            else
            {
                this.Controls.Remove(this.pbxImagenCompartida);
                this.Size = new Size(473, 92);
            }
            string imagenB64 = await conseguirImagenDelCreador(Convert.ToString(MessageData.nombreDeCuenta), token);
            byte[] imagen2 = Convert.FromBase64String(imagenB64);
            MemoryStream ms2 = new MemoryStream(imagen2);
            Bitmap bitmap2 = new Bitmap(ms2);
            this.pbxFotoUsuario.Image = bitmap2;
            if (user.Equals(creador))
            {
                // pbxOpciones
                // 
                this.pbxOpciones.Image = global::Frontend.Properties.Resources.mas_opciones;
                this.pbxOpciones.Location = new System.Drawing.Point(420, 7);
                this.pbxOpciones.Name = "pbxOpciones";
                this.pbxOpciones.Size = new System.Drawing.Size(50, 20);
                this.pbxOpciones.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                this.pbxOpciones.TabIndex = 75;
                this.pbxOpciones.TabStop = false;
                this.pbxOpciones.Click += new System.EventHandler(this.pbxOpciones_Click);
                // 
                // btnEliminar
                // 
                this.btnEliminar.AutoSize = true;
                this.btnEliminar.Location = new System.Drawing.Point(379, 3);
                this.btnEliminar.Name = "btnEliminar";
                this.btnEliminar.Size = new System.Drawing.Size(43, 13);
                this.btnEliminar.TabIndex = 76;
                this.btnEliminar.Text = "Eliminar";
                this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
                // 
                // btnEditar
                // 
                this.btnEditar.AutoSize = true;
                this.btnEditar.Location = new System.Drawing.Point(380, 16);
                this.btnEditar.Name = "btnEditar";
                this.btnEditar.Size = new System.Drawing.Size(34, 13);
                this.btnEditar.TabIndex = 77;
                this.btnEditar.Text = "Editar";
                this.btnEditar.Click += new System.EventHandler(this.btnEditar_Click);
                this.Controls.Add(this.btnEditar);
                this.Controls.Add(this.btnEliminar);
                this.Controls.Add(this.pbxOpciones);
            }
        }
        static async Task<string> conseguirImagenDelCreador(string creador, string token)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var dato = new { nombreDeCuenta = creador, token = token };
                    var content = new StringContent(JsonConvert.SerializeObject(dato), Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PutAsync($"https://localhost:44383/user/obtenerImagenUsuario", content);
                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();
                    dynamic imagen = JsonConvert.DeserializeObject(responseBody);
                    return imagen;
                }
                catch
                {
                    MessageBox.Show("Error de conexión");
                    return "error";
                }
            }
        }

        public void redondearPictureBox(Image image) //Para la imagen del usuario
        {
            if (image == null)
            {
                MessageBox.Show("La imagen es nula. No se puede redondear.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            System.Drawing.Drawing2D.GraphicsPath gp = new System.Drawing.Drawing2D.GraphicsPath();
            gp.AddEllipse(0, 0, pbxFotoUsuario.Width, pbxFotoUsuario.Height);
            Region rg = new Region(gp);
            pbxFotoUsuario.Region = rg;
            pbxFotoUsuario.Image = image;
        }

            private void InitializeComponent()
        {
            this.lblNombreDeCuenta = new System.Windows.Forms.Label();
            this.txtMensaje = new System.Windows.Forms.TextBox();
            this.lblFechaYHora = new System.Windows.Forms.Label();
            this.pbxImagenCompartida = new System.Windows.Forms.PictureBox();
            this.pbxFotoUsuario = new System.Windows.Forms.PictureBox();
            this.pnlOpciones = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtURL = new System.Windows.Forms.TextBox();
            this.pbxOpciones = new System.Windows.Forms.PictureBox();
            this.btnEliminar = new System.Windows.Forms.Label();
            this.btnEditar = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pbxImagenCompartida)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxFotoUsuario)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxOpciones)).BeginInit();
            this.SuspendLayout();
            // 
            // lblNombreDeCuenta
            // 
            this.lblNombreDeCuenta.AutoSize = true;
            this.lblNombreDeCuenta.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNombreDeCuenta.Location = new System.Drawing.Point(89, 7);
            this.lblNombreDeCuenta.Name = "lblNombreDeCuenta";
            this.lblNombreDeCuenta.Size = new System.Drawing.Size(0, 20);
            this.lblNombreDeCuenta.TabIndex = 0;
            // 
            // txtMensaje
            // 
            this.txtMensaje.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMensaje.ForeColor = System.Drawing.Color.Gray;
            this.txtMensaje.Location = new System.Drawing.Point(89, 28);
            this.txtMensaje.MaxLength = 255;
            this.txtMensaje.Multiline = true;
            this.txtMensaje.Name = "txtMensaje";
            this.txtMensaje.Size = new System.Drawing.Size(381, 60);
            this.txtMensaje.TabIndex = 1;
            this.txtMensaje.Text = "Inserte su texto";
            // 
            // lblFechaYHora
            // 
            this.lblFechaYHora.AutoSize = true;
            this.lblFechaYHora.Location = new System.Drawing.Point(274, 12);
            this.lblFechaYHora.Name = "lblFechaYHora";
            this.lblFechaYHora.Size = new System.Drawing.Size(65, 13);
            this.lblFechaYHora.TabIndex = 5;
            this.lblFechaYHora.Text = "DD/MM/YY";
            // 
            // pbxImagenCompartida
            // 
            this.pbxImagenCompartida.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.pbxImagenCompartida.Location = new System.Drawing.Point(3, 133);
            this.pbxImagenCompartida.Name = "pbxImagenCompartida";
            this.pbxImagenCompartida.Size = new System.Drawing.Size(467, 262);
            this.pbxImagenCompartida.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbxImagenCompartida.TabIndex = 6;
            this.pbxImagenCompartida.TabStop = false;
            // 
            // pbxFotoUsuario
            // 
            this.pbxFotoUsuario.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbxFotoUsuario.Image = global::Frontend.Properties.Resources.User;
            this.pbxFotoUsuario.Location = new System.Drawing.Point(3, 12);
            this.pbxFotoUsuario.Name = "pbxFotoUsuario";
            this.pbxFotoUsuario.Size = new System.Drawing.Size(80, 76);
            this.pbxFotoUsuario.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbxFotoUsuario.TabIndex = 3;
            this.pbxFotoUsuario.TabStop = false;
            // 
            // pnlOpciones
            // 
            this.pnlOpciones.BackColor = System.Drawing.Color.SlateBlue;
            this.pnlOpciones.Location = new System.Drawing.Point(3, 3);
            this.pnlOpciones.Name = "pnlOpciones";
            this.pnlOpciones.Size = new System.Drawing.Size(467, 3);
            this.pnlOpciones.TabIndex = 72;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.SlateBlue;
            this.panel1.Location = new System.Drawing.Point(3, 401);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(467, 3);
            this.panel1.TabIndex = 73;
            // 
            // txtURL
            // 
            this.txtURL.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtURL.ForeColor = System.Drawing.Color.MidnightBlue;
            this.txtURL.Location = new System.Drawing.Point(3, 94);
            this.txtURL.MaxLength = 255;
            this.txtURL.Multiline = true;
            this.txtURL.Name = "txtURL";
            this.txtURL.Size = new System.Drawing.Size(467, 24);
            this.txtURL.TabIndex = 74;
            this.txtURL.Text = "URL";
            // 
            
            // 
            // MessageControl
            // 
            
            
            this.Controls.Add(this.txtURL);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pnlOpciones);
            this.Controls.Add(this.pbxImagenCompartida);
            this.Controls.Add(this.lblFechaYHora);
            this.Controls.Add(this.pbxFotoUsuario);
            this.Controls.Add(this.txtMensaje);
            this.Controls.Add(this.lblNombreDeCuenta);
            this.Name = "MessageControl";
            this.Size = new System.Drawing.Size(473, 409);
            ((System.ComponentModel.ISupportInitialize)(this.pbxImagenCompartida)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxFotoUsuario)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxOpciones)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void pbxOpciones_Click(object sender, EventArgs e)
        {
            if (btnEditar.Visible == false)
            {
                btnEditar.Visible = true;
                btnEliminar.Visible = true;
            }
            else
            {
                btnEditar.Visible = false;
                btnEliminar.Visible = false;
            }
        }

        static async Task<dynamic> eliminar(string idMensaje, string token)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var datos = new { idMensaje=idMensaje, token = token };
                    var content = new StringContent(JsonConvert.SerializeObject(datos), Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PutAsync("https://localhost:44304/EliminarMensaje", content);
                    response.EnsureSuccessStatusCode();
                    var responseBody = await response.Content.ReadAsStringAsync();
                    dynamic data = JsonConvert.DeserializeObject(responseBody);
                    return Convert.ToString(data);
                }
                catch (Exception ex)
                {
                    return "Error de conexión";
                }
            }
        }

        private async void btnEliminar_Click(object sender, EventArgs e)
        {
            string resultado = await eliminar(idMensaje, token);
            MensajeEliminado?.Invoke(this, new PersonalizedArgs("ELIMINADO"));
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            EditarMensaje?.Invoke(this, new PersonalizedArgs(idMensaje));
        }
    }

      
    }
