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


        private string nombreDeCuenta; //Había considerado crear un nombre
        // de cuenta enviador y otro creador, pero creo que va a ser una mejor idea
        // manejar eso desde el front. Ej: Si el nombre de cuenta del mensaje es igual
        // al nombre del que lo envió, entonces el mensaje aparece a la izquierda
        private string nombreVisible;
        private string mensaje;
        private int idPost; // Para poder hacer referencia a un post
        // private string nombreDeCuentaPost //Por si es necesario usarlo para encontrar 
        // el post
        private string token; //Para la autenticación. No tengo ni idea de cómo hacerlo
        private Label lblNombreDeCuenta;
        private TextBox txtMensaje;
        private PictureBox pbxFotoUsuario;
        private Label lblFechaYHora; //Capaz que registro el año también, porque en el DDL es
        private PictureBox pbxImagenCompartida;
        private Panel pnlOpciones;
        private Panel panel1;

        // un DATETIME
        private Panel pnlPost; 


        public MessageControl(dynamic MessageData, string token)
        {
            InitializeComponent();
            txtMensaje.ReadOnly = true;
            aplicarDatos(MessageData);
            
        }
        public async Task aplicarDatos(dynamic MessageData)
        {
            //anda mas o menos aun, no carga las imagenes ni del usuario ni la del mensaje
            MessageBox.Show("" + MessageData);
            this.lblNombreDeCuenta.Text = MessageData.nombreDeCuenta;
            this.txtMensaje.Text = MessageData.texto;
            this.lblFechaYHora.Text = MessageData.fechaYHora;
            MessageBox.Show("" + MessageData.imagen);
            if (!string.IsNullOrEmpty(MessageData.imagen))
            {
                byte[] imagen = Convert.FromBase64String(Convert.ToString(MessageData.imagen));
                MemoryStream ms = new MemoryStream(imagen);
                Bitmap bitmap = new Bitmap(ms);
                this.pbxImagenCompartida.Image = bitmap;
            }
            string imagenB64 = await conseguirImagenDelCreador(MessageData.nombreDeCuenta, token);
            byte[] imagen2 = Convert.FromBase64String(imagenB64);
            MemoryStream ms2 = new MemoryStream(imagen2);
            Bitmap bitmap2 = new Bitmap(ms2);
            this.pbxFotoUsuario.Image = bitmap2;
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

        static async Task<string[]> BuscarPost(int id, string token)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var dato = new { id = id, token = token };
                    var content = new StringContent(JsonConvert.SerializeObject(dato), Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PutAsync("https://localhost:44340/postPorId", content);
                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();
                    dynamic data = JsonConvert.DeserializeObject(responseBody); //sigo sin poder pasar esto a lo que quiero, no me deja acceder a la info del json de nin}guna manera, tengo que hallar alguna forma de pasar los datos
                    return new string[] { data.text, data.link, data.image, data.fechayhora };
                }
                catch
                {
                    return null;
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

        //private async Task ObtenerFotoDelUsuario(string nombreDeCuenta){}

        //private async Task RecibirMensaje(){}

        //private asy



        private void InitializeComponent()
        {
            this.lblNombreDeCuenta = new System.Windows.Forms.Label();
            this.txtMensaje = new System.Windows.Forms.TextBox();
            this.pnlPost = new System.Windows.Forms.Panel();
            this.lblFechaYHora = new System.Windows.Forms.Label();
            this.pbxImagenCompartida = new System.Windows.Forms.PictureBox();
            this.pbxFotoUsuario = new System.Windows.Forms.PictureBox();
            this.pnlOpciones = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.pbxImagenCompartida)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxFotoUsuario)).BeginInit();
            this.SuspendLayout();
            // 
            // lblNombreDeCuenta
            // 
            this.lblNombreDeCuenta.AutoSize = true;
            this.lblNombreDeCuenta.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNombreDeCuenta.Location = new System.Drawing.Point(89, 7);
            this.lblNombreDeCuenta.Name = "lblNombreDeCuenta";
            this.lblNombreDeCuenta.Size = new System.Drawing.Size(140, 20);
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
            this.txtMensaje.Size = new System.Drawing.Size(204, 60);
            this.txtMensaje.TabIndex = 1;
            this.txtMensaje.Text = "Inserte su texto";
            // 
            // pnlPost
            // 
            this.pnlPost.BackColor = System.Drawing.SystemColors.ControlDark;
            this.pnlPost.Location = new System.Drawing.Point(3, 318);
            this.pnlPost.Name = "pnlPost";
            this.pnlPost.Size = new System.Drawing.Size(290, 71);
            this.pnlPost.TabIndex = 4;
            // 
            // lblFechaYHora
            // 
            this.lblFechaYHora.AutoSize = true;
            this.lblFechaYHora.Location = new System.Drawing.Point(228, 12);
            this.lblFechaYHora.Name = "lblFechaYHora";
            this.lblFechaYHora.Size = new System.Drawing.Size(65, 13);
            this.lblFechaYHora.TabIndex = 5;
            this.lblFechaYHora.Text = "DD/MM/YY";
            // 
            // pbxImagenCompartida
            // 
            this.pbxImagenCompartida.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.pbxImagenCompartida.Location = new System.Drawing.Point(3, 94);
            this.pbxImagenCompartida.Name = "pbxImagenCompartida";
            this.pbxImagenCompartida.Size = new System.Drawing.Size(290, 218);
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
            this.pnlOpciones.Size = new System.Drawing.Size(300, 3);
            this.pnlOpciones.TabIndex = 72;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.SlateBlue;
            this.panel1.Location = new System.Drawing.Point(3, 395);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(300, 3);
            this.panel1.TabIndex = 73;
            // 
            // MessageControl
            // 
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pnlOpciones);
            this.Controls.Add(this.pbxImagenCompartida);
            this.Controls.Add(this.lblFechaYHora);
            this.Controls.Add(this.pnlPost);
            this.Controls.Add(this.pbxFotoUsuario);
            this.Controls.Add(this.txtMensaje);
            this.Controls.Add(this.lblNombreDeCuenta);
            this.Name = "MessageControl";
            this.Size = new System.Drawing.Size(312, 409);
            ((System.ComponentModel.ISupportInitialize)(this.pbxImagenCompartida)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxFotoUsuario)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }

      
    }
