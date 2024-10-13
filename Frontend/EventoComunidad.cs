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
        private PictureBox pbxImagen;
        private DateTimePicker dtpFechaInicio;
        private DateTimePicker dtpFechaFinal;
        private PictureBox btnUbicacion;
        private Label lblDescripcion;
        private Label lblUbicacion;
        private PictureBox pictureBox1;
        private Label lblHorario;
        private Label lblNombre;
        private Panel panelPosts;
        private PictureBox btnSeguir;

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


        //Recibo los datos de evento desde Grupo-EventoParaListar.
        //No tiene sentido buscarlos de nuevo acá si ya en la
        //ventana anterior los tenemos guardados en un datatable.
        private string token;
        private string user;
        private PictureBox btnCrear;
        private string idEvento;
        private string modo;
        public event EventHandler<PersonalizedArgs> PostearEnEvento;
        public event EventHandler<PersonalizedArgs> AbrirComentarios;
        public event EventHandler<PersonalizedArgs> ReportarPost;
        public EventoComunidad(dynamic EventData,string user, string token, string modo)
        {
            InitializeComponent();
            this.token = token;
            this.user = user;
            this.idEvento = Convert.ToString(EventData.id);
            this.modo = modo;
            AplicarDatos(EventData);
            LoadPosts();
        }
        
       //Tuqui
        private void InitializeComponent()
        {
            this.dtpFechaInicio = new System.Windows.Forms.DateTimePicker();
            this.dtpFechaFinal = new System.Windows.Forms.DateTimePicker();
            this.lblNombre = new System.Windows.Forms.Label();
            this.lblDescripcion = new System.Windows.Forms.Label();
            this.lblUbicacion = new System.Windows.Forms.Label();
            this.lblHorario = new System.Windows.Forms.Label();
            this.panelPosts = new System.Windows.Forms.Panel();
            this.btnSeguir = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnUbicacion = new System.Windows.Forms.PictureBox();
            this.pbxImagen = new System.Windows.Forms.PictureBox();
            this.btnCrear = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.btnSeguir)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnUbicacion)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxImagen)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnCrear)).BeginInit();
            this.SuspendLayout();
            // 
            // dtpFechaInicio
            // 
            this.dtpFechaInicio.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.dtpFechaInicio.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaInicio.Location = new System.Drawing.Point(531, 47);
            this.dtpFechaInicio.Name = "dtpFechaInicio";
            this.dtpFechaInicio.ShowUpDown = true;
            this.dtpFechaInicio.Size = new System.Drawing.Size(200, 20);
            this.dtpFechaInicio.TabIndex = 42;
            // 
            // dtpFechaFinal
            // 
            this.dtpFechaFinal.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.dtpFechaFinal.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaFinal.Location = new System.Drawing.Point(760, 47);
            this.dtpFechaFinal.Name = "dtpFechaFinal";
            this.dtpFechaFinal.ShowUpDown = true;
            this.dtpFechaFinal.Size = new System.Drawing.Size(200, 20);
            this.dtpFechaFinal.TabIndex = 43;
            // 
            // lblNombre
            // 
            this.lblNombre.AutoSize = true;
            this.lblNombre.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNombre.Location = new System.Drawing.Point(154, 12);
            this.lblNombre.Name = "lblNombre";
            this.lblNombre.Size = new System.Drawing.Size(70, 25);
            this.lblNombre.TabIndex = 45;
            this.lblNombre.Text = "label1";
            // 
            // lblDescripcion
            // 
            this.lblDescripcion.AutoSize = true;
            this.lblDescripcion.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDescripcion.Location = new System.Drawing.Point(156, 51);
            this.lblDescripcion.Name = "lblDescripcion";
            this.lblDescripcion.Size = new System.Drawing.Size(45, 16);
            this.lblDescripcion.TabIndex = 46;
            this.lblDescripcion.Text = "label1";
            // 
            // lblUbicacion
            // 
            this.lblUbicacion.AutoSize = true;
            this.lblUbicacion.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUbicacion.Location = new System.Drawing.Point(100, 142);
            this.lblUbicacion.Name = "lblUbicacion";
            this.lblUbicacion.Size = new System.Drawing.Size(83, 16);
            this.lblUbicacion.TabIndex = 47;
            this.lblUbicacion.Text = "lblUbicacion";
            // 
            // lblHorario
            // 
            this.lblHorario.AutoSize = true;
            this.lblHorario.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHorario.Location = new System.Drawing.Point(618, 142);
            this.lblHorario.Name = "lblHorario";
            this.lblHorario.Size = new System.Drawing.Size(35, 16);
            this.lblHorario.TabIndex = 49;
            this.lblHorario.Text = "hora";
            // 
            // panelPosts
            // 
            this.panelPosts.AutoScroll = true;
            this.panelPosts.Location = new System.Drawing.Point(12, 228);
            this.panelPosts.Name = "panelPosts";
            this.panelPosts.Size = new System.Drawing.Size(972, 334);
            this.panelPosts.TabIndex = 50;
            // 
            // btnSeguir
            // 
            this.btnSeguir.Image = global::Frontend.Properties.Resources.seguir_removebg_preview;
            this.btnSeguir.Location = new System.Drawing.Point(108, 70);
            this.btnSeguir.Name = "btnSeguir";
            this.btnSeguir.Size = new System.Drawing.Size(188, 41);
            this.btnSeguir.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.btnSeguir.TabIndex = 51;
            this.btnSeguir.TabStop = false;
            this.btnSeguir.Click += new System.EventHandler(this.btnSeguir_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox1.Image = global::Frontend.Properties.Resources.buscar;
            this.pictureBox1.Location = new System.Drawing.Point(531, 108);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(50, 50);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 48;
            this.pictureBox1.TabStop = false;
            // 
            // btnUbicacion
            // 
            this.btnUbicacion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUbicacion.BackColor = System.Drawing.Color.Transparent;
            this.btnUbicacion.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnUbicacion.Image = global::Frontend.Properties.Resources.buscar;
            this.btnUbicacion.Location = new System.Drawing.Point(12, 108);
            this.btnUbicacion.Name = "btnUbicacion";
            this.btnUbicacion.Size = new System.Drawing.Size(50, 50);
            this.btnUbicacion.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.btnUbicacion.TabIndex = 44;
            this.btnUbicacion.TabStop = false;
            // 
            // pbxImagen
            // 
            this.pbxImagen.Location = new System.Drawing.Point(12, 12);
            this.pbxImagen.Name = "pbxImagen";
            this.pbxImagen.Size = new System.Drawing.Size(90, 90);
            this.pbxImagen.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbxImagen.TabIndex = 38;
            this.pbxImagen.TabStop = false;
            // 
            // btnCrear
            // 
            this.btnCrear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCrear.BackColor = System.Drawing.Color.Transparent;
            this.btnCrear.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCrear.Image = global::Frontend.Properties.Resources.crear;
            this.btnCrear.Location = new System.Drawing.Point(474, 172);
            this.btnCrear.Name = "btnCrear";
            this.btnCrear.Size = new System.Drawing.Size(50, 50);
            this.btnCrear.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.btnCrear.TabIndex = 52;
            this.btnCrear.TabStop = false;
            this.btnCrear.Click += new System.EventHandler(this.btnCrear_Click);
            // 
            // EventoComunidad
            // 
            this.ClientSize = new System.Drawing.Size(996, 574);
            this.Controls.Add(this.btnCrear);
            this.Controls.Add(this.btnSeguir);
            this.Controls.Add(this.panelPosts);
            this.Controls.Add(this.lblHorario);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.lblUbicacion);
            this.Controls.Add(this.lblDescripcion);
            this.Controls.Add(this.lblNombre);
            this.Controls.Add(this.btnUbicacion);
            this.Controls.Add(this.dtpFechaFinal);
            this.Controls.Add(this.dtpFechaInicio);
            this.Controls.Add(this.pbxImagen);
            this.Name = "EventoComunidad";
            ((System.ComponentModel.ISupportInitialize)(this.btnSeguir)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnUbicacion)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxImagen)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnCrear)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        //El nombre lo dice todo.
        private void AplicarDatos(dynamic EventData)
        {
            lblNombre.Text = EventData.titulo;
            lblDescripcion.Text = EventData.descripcion;
            dtpFechaInicio.Text = EventData.fechaYhora_Inicio;
            dtpFechaFinal.Text = EventData.fechaYhora_Final;
            lblUbicacion.Text = EventData.ubicacion;
            MessageBox.Show(Convert.ToString(EventData.id));
            //Creo que está bien?? 
            byte[] imagen = Convert.FromBase64String(Convert.ToString(EventData.foto));
            MemoryStream ms = new MemoryStream(imagen);
            Bitmap bitmap = new Bitmap(ms);
            this.pbxImagen.Image = bitmap;
        }
        // lo de acá aún no hay forma de probarlo, recien cuando esté el menú de búsqueda se va a poder
        public static async Task Seguir(string user, string idevento, string rol, string token)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                        var datos = new { user = user, id= idevento, rol=rol, token = token };
                        var content = new StringContent(JsonConvert.SerializeObject(datos), Encoding.UTF8, "application/json");
                        HttpResponseMessage response = await client.PostAsync("https://localhost:44340/participarDelEvento", content);
                        response.EnsureSuccessStatusCode();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("ERROR AL LLAMAR A LA API");
                }
            }
        }
        private async void btnSeguir_Click(object sender, EventArgs e)
        {
            await Seguir(user, idEvento, "Seguidor", token);
        }

        private void btnCrear_Click(object sender, EventArgs e)
        {
            PostearEnEvento?.Invoke(this, new PersonalizedArgs(idEvento));
        }

        static async Task<dynamic> ConseguirPosts(string idevento, string token)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var dato = new { idEvento=idevento, token = token };
                    var content = new StringContent(JsonConvert.SerializeObject(dato), Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PutAsync("https://localhost:44340/seleccionarTodosLosPostDelEvento", content);
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

        private async void LoadPosts()
        {
            DataTable posts = await ConseguirPosts(idEvento, token);
            if (posts != null)
            {
                for (int i = posts.Rows.Count - 1; i >= 0; i--)
                {
                    int idpost = Convert.ToInt32(posts.Rows[i]["idPost"]);
                    var postControl = new PostControl(idpost, modo, user, token);
                    postControl.AbrirComentarios += PostControl_AbrirComentarios;
                    postControl.ReportarPost += PostControl_ReportarPost;
                    await postControl.aplicarDatos();
                    // Calcula la ubicación Y acumulada
                    int currentYPosition = 0;
                    if (panelPosts.Controls.Count > 0)
                    {
                        var lastControl = panelPosts.Controls[panelPosts.Controls.Count - 1];
                        if (postControl.tipo.Equals("imageOnly") || postControl.tipo.Equals("textAndImage"))
                        {
                            await Task.Delay(300);
                        }
                        currentYPosition = lastControl.Bottom;  // La posición inferior del último control agregado
                    }
                    postControl.Location = new Point(0, currentYPosition);
                    MessageBox.Show(""+postControl.Location);
                    panelPosts.Controls.Add(postControl);
                }
            }
        }
        private void PostControl_AbrirComentarios(object sender, PersonalizedArgs e)
        {
            // Disparar el evento para que lo maneje quien esté suscrito (en este caso, Inicio)
            AbrirComentarios?.Invoke(this, new PersonalizedArgs(e.arg));
        }
        private void PostControl_ReportarPost(object sender, PersonalizedArgs e)
        {
            // Disparar el evento para que lo maneje quien esté suscrito (en este caso, Inicio)
            ReportarPost?.Invoke(this, new PersonalizedArgs(e.arg));
        }
    }
}
