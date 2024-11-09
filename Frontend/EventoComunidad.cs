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
        private Label lblNombre;
        private Panel panelPosts;
        private PictureBox btnSeguir;
        private string token;
        private string user;
        private PictureBox btnCrear;
        private string idEvento;
        private PictureBox pbxEditar;
        private TextBox txtNombre;
        private TextBox txtDesc;
        private TextBox txtUbicacion;
        private Label lblEditar;
        private Label lblEliminar;
        private PictureBox pbxConfirmarCambios;
        private PictureBox pbxSeleccionarImagen;
        private Label lblCancelar;
        private PictureBox pbxImagenEditar;
        private Label label1;
        private Label label2;
        private Label lblAdministradores;
        private string modo;
        private Label lblFechaInicio;
        private Label lblFechaFinal;
        private dynamic evento;
        private string idioma;
        public event EventHandler<PersonalizedArgs> PostearEnEvento;
        public event EventHandler<PersonalizedArgs> AbrirComentarios;
        public event EventHandler<PersonalizedArgs> ReportarPost;
        public event EventHandler<PersonalizedArgs> EventoEliminado;
        public event EventHandler<PersonalizedArgs> ReportarEvento;
        public EventoComunidad(dynamic EventData,string user, string token, string modo, string idioma)
        {
            InitializeComponent();
            this.token = token;
            this.user = user;
            this.idEvento = Convert.ToString(EventData.idEvento);
            this.modo = modo;
            this.idioma = idioma;
            evento = EventData;
            AplicarDatos(EventData);
            if (idioma.Equals("English"))
            {
                lblCancelar.Text = "Cancel";
                label1.Text = "Start";
                label2.Text = "Finish";
                lblEliminar.Text = "Delete";
                lblEditar.Text = "Edit";
                lblAdministradores.Text = "Administrators";
            }
            LoadPosts();
            CompararCreador();
            txtNombre.Visible = false;
            txtDesc.Visible = false;
            txtUbicacion.Visible = false;
            dtpFechaInicio.Enabled = false;
            dtpFechaFinal.Enabled = false;
            pbxConfirmarCambios.Visible = false;
            lblEditar.Visible = false;
            lblEliminar.Visible = false;
            lblCancelar.Visible = false;
            pbxImagenEditar.Visible = false;
            pbxSeleccionarImagen.Visible = false;
            lblAdministradores.Visible = false;
            if (modo.Equals("Oscuro"))
            {
                lblNombre.ForeColor = Color.White;
                lblDescripcion.ForeColor = Color.White;
                lblUbicacion.ForeColor = Color.White;
                lblCancelar.ForeColor = Color.White;
                label1.ForeColor = Color.White;
                label2.ForeColor = Color.White;
                lblFechaInicio.ForeColor = Color.White;
                lblFechaFinal.ForeColor = Color.White;
                lblEditar.ForeColor = Color.White;
                lblEliminar.ForeColor = Color.White;
                lblCancelar.ForeColor = Color.White;
                pbxSeleccionarImagen.Image = Frontend.Properties.Resources.Foto_negra;
                btnUbicacion.Image = Frontend.Properties.Resources.buscar_claro;
                btnCrear.Image = Frontend.Properties.Resources.crear_claro;
                pbxEditar.Image = Frontend.Properties.Resources.mas_opciones_claro_relleno;
                panelPosts.BackColor= Color.FromArgb(50, 50, 50);
                this.BackColor= Color.FromArgb(40, 40, 40);
            }
            if (idioma.Equals("English"))
            {
                btnSeguir.Image = Frontend.Properties.Resources.Follow_removebg_preview;
            }
        }
        
        private void CompararCreador()
        {
            string rol = Convert.ToString(evento.rol);
            if (rol.Equals("creador"))
            {
                btnSeguir.Visible = false;
                if (modo.Equals("Oscuro"))
                {
                    lblAdministradores.ForeColor = Color.White;
                }
            }
            else
            {
                lblEditar.Text = "Reportar";
                lblEliminar.Text = "Salir";
                if (idioma.Equals("English"))
                {
                    lblEditar.Text = "Report";
                    lblEliminar.Text = "Exit";
                }
                if (rol.Equals("Seguidor"))
                {
                    btnSeguir.Visible = false;
                    btnCrear.Visible = false;
                    this.Controls.Remove(lblAdministradores);
                }
                else
                {
                    if (rol.Equals("admin"))
                    {
                        btnSeguir.Visible = false;
                        btnCrear.Visible = true;
                        if (modo.Equals("Oscuro"))
                        {
                            lblAdministradores.ForeColor = Color.White;
                        }
                    }
                    else
                    {
                        btnSeguir.Visible = true;
                        btnCrear.Visible = false;
                        lblAdministradores.Visible = false;
                    }
                }
            }
        }

        static async Task<dynamic> RolDelEvento(string idevento, string usuario, string token)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var dato = new { id = idevento, user = usuario, token = token };
                    var content = new StringContent(JsonConvert.SerializeObject(dato), Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PutAsync("https://localhost:44340/RolDelEvento", content);
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
        private void InitializeComponent()
        {
            this.dtpFechaInicio = new System.Windows.Forms.DateTimePicker();
            this.dtpFechaFinal = new System.Windows.Forms.DateTimePicker();
            this.lblNombre = new System.Windows.Forms.Label();
            this.lblDescripcion = new System.Windows.Forms.Label();
            this.lblUbicacion = new System.Windows.Forms.Label();
            this.panelPosts = new System.Windows.Forms.Panel();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.txtDesc = new System.Windows.Forms.TextBox();
            this.txtUbicacion = new System.Windows.Forms.TextBox();
            this.lblEditar = new System.Windows.Forms.Label();
            this.lblEliminar = new System.Windows.Forms.Label();
            this.lblCancelar = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblAdministradores = new System.Windows.Forms.Label();
            this.lblFechaInicio = new System.Windows.Forms.Label();
            this.lblFechaFinal = new System.Windows.Forms.Label();
            this.pbxImagenEditar = new System.Windows.Forms.PictureBox();
            this.pbxSeleccionarImagen = new System.Windows.Forms.PictureBox();
            this.pbxConfirmarCambios = new System.Windows.Forms.PictureBox();
            this.pbxEditar = new System.Windows.Forms.PictureBox();
            this.btnCrear = new System.Windows.Forms.PictureBox();
            this.btnSeguir = new System.Windows.Forms.PictureBox();
            this.btnUbicacion = new System.Windows.Forms.PictureBox();
            this.pbxImagen = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbxImagenEditar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxSeleccionarImagen)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxConfirmarCambios)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxEditar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnCrear)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSeguir)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnUbicacion)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxImagen)).BeginInit();
            this.SuspendLayout();
            // 
            // dtpFechaInicio
            // 
            this.dtpFechaInicio.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpFechaInicio.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.dtpFechaInicio.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaInicio.Location = new System.Drawing.Point(760, 108);
            this.dtpFechaInicio.Name = "dtpFechaInicio";
            this.dtpFechaInicio.ShowUpDown = true;
            this.dtpFechaInicio.Size = new System.Drawing.Size(200, 20);
            this.dtpFechaInicio.TabIndex = 42;
            // 
            // dtpFechaFinal
            // 
            this.dtpFechaFinal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpFechaFinal.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.dtpFechaFinal.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaFinal.Location = new System.Drawing.Point(760, 134);
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
            // panelPosts
            // 
            this.panelPosts.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelPosts.AutoScroll = true;
            this.panelPosts.Location = new System.Drawing.Point(12, 228);
            this.panelPosts.Name = "panelPosts";
            this.panelPosts.Size = new System.Drawing.Size(972, 334);
            this.panelPosts.TabIndex = 50;
            // 
            // txtNombre
            // 
            this.txtNombre.Location = new System.Drawing.Point(159, 17);
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(100, 20);
            this.txtNombre.TabIndex = 54;
            // 
            // txtDesc
            // 
            this.txtDesc.Location = new System.Drawing.Point(159, 47);
            this.txtDesc.Name = "txtDesc";
            this.txtDesc.Size = new System.Drawing.Size(100, 20);
            this.txtDesc.TabIndex = 55;
            // 
            // txtUbicacion
            // 
            this.txtUbicacion.Location = new System.Drawing.Point(101, 142);
            this.txtUbicacion.Name = "txtUbicacion";
            this.txtUbicacion.Size = new System.Drawing.Size(100, 20);
            this.txtUbicacion.TabIndex = 56;
            // 
            // lblEditar
            // 
            this.lblEditar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblEditar.AutoSize = true;
            this.lblEditar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEditar.Location = new System.Drawing.Point(853, 9);
            this.lblEditar.Name = "lblEditar";
            this.lblEditar.Size = new System.Drawing.Size(51, 20);
            this.lblEditar.TabIndex = 57;
            this.lblEditar.Text = "Editar";
            this.lblEditar.Click += new System.EventHandler(this.lblEditar_Click);
            // 
            // lblEliminar
            // 
            this.lblEliminar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblEliminar.AutoSize = true;
            this.lblEliminar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEliminar.Location = new System.Drawing.Point(839, 35);
            this.lblEliminar.Name = "lblEliminar";
            this.lblEliminar.Size = new System.Drawing.Size(65, 20);
            this.lblEliminar.TabIndex = 58;
            this.lblEliminar.Text = "Eliminar";
            this.lblEliminar.Click += new System.EventHandler(this.lblEliminar_Click);
            // 
            // lblCancelar
            // 
            this.lblCancelar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCancelar.AutoSize = true;
            this.lblCancelar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCancelar.Location = new System.Drawing.Point(542, 35);
            this.lblCancelar.Name = "lblCancelar";
            this.lblCancelar.Size = new System.Drawing.Size(72, 20);
            this.lblCancelar.TabIndex = 61;
            this.lblCancelar.Text = "Cancelar";
            this.lblCancelar.Click += new System.EventHandler(this.lblCancelar_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(660, 108);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 20);
            this.label1.TabIndex = 63;
            this.label1.Text = "Inicia";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(660, 134);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 20);
            this.label2.TabIndex = 64;
            this.label2.Text = "Finaliza";
            // 
            // lblAdministradores
            // 
            this.lblAdministradores.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblAdministradores.AutoSize = true;
            this.lblAdministradores.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAdministradores.Location = new System.Drawing.Point(780, 55);
            this.lblAdministradores.Name = "lblAdministradores";
            this.lblAdministradores.Size = new System.Drawing.Size(124, 20);
            this.lblAdministradores.TabIndex = 65;
            this.lblAdministradores.Text = "Administradores";
            this.lblAdministradores.Click += new System.EventHandler(this.lblAdministradores_Click);
            // 
            // lblFechaInicio
            // 
            this.lblFechaInicio.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblFechaInicio.AutoSize = true;
            this.lblFechaInicio.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFechaInicio.Location = new System.Drawing.Point(756, 108);
            this.lblFechaInicio.Name = "lblFechaInicio";
            this.lblFechaInicio.Size = new System.Drawing.Size(51, 20);
            this.lblFechaInicio.TabIndex = 66;
            this.lblFechaInicio.Text = "label3";
            // 
            // lblFechaFinal
            // 
            this.lblFechaFinal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblFechaFinal.AutoSize = true;
            this.lblFechaFinal.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFechaFinal.Location = new System.Drawing.Point(756, 134);
            this.lblFechaFinal.Name = "lblFechaFinal";
            this.lblFechaFinal.Size = new System.Drawing.Size(51, 20);
            this.lblFechaFinal.TabIndex = 67;
            this.lblFechaFinal.Text = "label4";
            // 
            // pbxImagenEditar
            // 
            this.pbxImagenEditar.Location = new System.Drawing.Point(12, 12);
            this.pbxImagenEditar.Name = "pbxImagenEditar";
            this.pbxImagenEditar.Size = new System.Drawing.Size(90, 90);
            this.pbxImagenEditar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbxImagenEditar.TabIndex = 62;
            this.pbxImagenEditar.TabStop = false;
            // 
            // pbxSeleccionarImagen
            // 
            this.pbxSeleccionarImagen.BackColor = System.Drawing.Color.Transparent;
            this.pbxSeleccionarImagen.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbxSeleccionarImagen.Image = global::Frontend.Properties.Resources.Foto;
            this.pbxSeleccionarImagen.Location = new System.Drawing.Point(103, 12);
            this.pbxSeleccionarImagen.Name = "pbxSeleccionarImagen";
            this.pbxSeleccionarImagen.Size = new System.Drawing.Size(50, 50);
            this.pbxSeleccionarImagen.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbxSeleccionarImagen.TabIndex = 60;
            this.pbxSeleccionarImagen.TabStop = false;
            this.pbxSeleccionarImagen.Click += new System.EventHandler(this.pbxSeleccionarImagen_Click);
            // 
            // pbxConfirmarCambios
            // 
            this.pbxConfirmarCambios.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pbxConfirmarCambios.BackColor = System.Drawing.Color.Transparent;
            this.pbxConfirmarCambios.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbxConfirmarCambios.Image = global::Frontend.Properties.Resources.aceptar;
            this.pbxConfirmarCambios.Location = new System.Drawing.Point(474, 5);
            this.pbxConfirmarCambios.Name = "pbxConfirmarCambios";
            this.pbxConfirmarCambios.Size = new System.Drawing.Size(50, 50);
            this.pbxConfirmarCambios.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbxConfirmarCambios.TabIndex = 59;
            this.pbxConfirmarCambios.TabStop = false;
            this.pbxConfirmarCambios.Click += new System.EventHandler(this.pbxConfirmarCambios_Click);
            // 
            // pbxEditar
            // 
            this.pbxEditar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pbxEditar.BackColor = System.Drawing.Color.Transparent;
            this.pbxEditar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbxEditar.Image = global::Frontend.Properties.Resources.mas_opciones;
            this.pbxEditar.Location = new System.Drawing.Point(910, 5);
            this.pbxEditar.Name = "pbxEditar";
            this.pbxEditar.Size = new System.Drawing.Size(50, 50);
            this.pbxEditar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbxEditar.TabIndex = 53;
            this.pbxEditar.TabStop = false;
            this.pbxEditar.Click += new System.EventHandler(this.pbxEditar_Click);
            // 
            // btnCrear
            // 
            this.btnCrear.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
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
            // btnUbicacion
            // 
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
            // EventoComunidad
            // 
            this.ClientSize = new System.Drawing.Size(996, 574);
            this.Controls.Add(this.lblFechaFinal);
            this.Controls.Add(this.lblFechaInicio);
            this.Controls.Add(this.lblAdministradores);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pbxImagenEditar);
            this.Controls.Add(this.lblCancelar);
            this.Controls.Add(this.pbxSeleccionarImagen);
            this.Controls.Add(this.pbxConfirmarCambios);
            this.Controls.Add(this.lblEliminar);
            this.Controls.Add(this.lblEditar);
            this.Controls.Add(this.lblUbicacion);
            this.Controls.Add(this.lblNombre);
            this.Controls.Add(this.lblDescripcion);
            this.Controls.Add(this.txtUbicacion);
            this.Controls.Add(this.txtDesc);
            this.Controls.Add(this.txtNombre);
            this.Controls.Add(this.pbxEditar);
            this.Controls.Add(this.btnCrear);
            this.Controls.Add(this.btnSeguir);
            this.Controls.Add(this.panelPosts);
            this.Controls.Add(this.btnUbicacion);
            this.Controls.Add(this.dtpFechaFinal);
            this.Controls.Add(this.dtpFechaInicio);
            this.Controls.Add(this.pbxImagen);
            this.Name = "EventoComunidad";
            ((System.ComponentModel.ISupportInitialize)(this.pbxImagenEditar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxSeleccionarImagen)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxConfirmarCambios)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxEditar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnCrear)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSeguir)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnUbicacion)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxImagen)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void AplicarDatos(dynamic EventData)
        {
            lblNombre.Text = EventData.titulo;
            lblDescripcion.Text = EventData.descripcion;
            lblFechaInicio.Text = EventData.fechaYhora_Inicio;
            lblFechaFinal.Text = EventData.fechaYhora_Final;
            lblUbicacion.Text = EventData.ubicacion;
            idEvento = Convert.ToString(EventData.idEvento);
            //Creo que está bien?? 
            byte[] imagen = Convert.FromBase64String(Convert.ToString(EventData.foto));
            MemoryStream ms = new MemoryStream(imagen);
            Bitmap bitmap = new Bitmap(ms);
            this.pbxImagen.Image = bitmap;
            dtpFechaInicio.Visible = false;
            dtpFechaFinal.Visible = false;
        }
        // lo de acá aún no hay forma de probarlo, recien cuando esté el menú de búsqueda se va a poder
        public static async Task<dynamic> Seguir(string user, string idevento, string rol, string token)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var datos = new { user = user, id = idevento, rol = rol, token = token };
                    var content = new StringContent(JsonConvert.SerializeObject(datos), Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PostAsync("https://localhost:44340/participarDelEvento", content);
                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();
                    dynamic data = JsonConvert.DeserializeObject(responseBody);
                    return data;
                }
                catch (Exception ex)
                {
                    return "ERROR AL LLAMAR A LA API";
                }
            }
        }
        private async void btnSeguir_Click(object sender, EventArgs e)
        {
            await Seguir(user, idEvento, "Seguidor", token);
            btnSeguir.Visible = false;
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
                    dynamic data = JsonConvert.DeserializeObject(responseBody);
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
            var posts = await ConseguirPosts(idEvento, token);
            if (posts != null && !Convert.ToString(posts).Equals("Error al cargar Datagrid"))
            {
                foreach(var post in posts)
                {
                    var postControl = new PostControl(post, modo, user, token,idioma);
                    postControl.AbrirComentarios += PostControl_AbrirComentarios;
                    postControl.ReportarPost += PostControl_ReportarPost;
                    await postControl.aplicarDatos();
                    postControl.quitarLike();
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

        private void pbxEditar_Click(object sender, EventArgs e)
        {
            if (lblEditar.Visible == false)
            {
                lblEditar.Visible = true;
                lblEliminar.Visible = true;
                lblAdministradores.Visible = true;
            }
            else
            {
                lblEditar.Visible = false;
                lblEliminar.Visible = false;
                lblAdministradores.Visible = false;
            }
        }

        private void lblEditar_Click(object sender, EventArgs e)
        {
            lblEditar.Visible = false;
            lblAdministradores.Visible = false;
            lblEliminar.Visible = false;
            if (lblEditar.Text.Equals("Editar") || lblEditar.Text.Equals("Edit"))
            {
                txtNombre.Visible = true;
                txtDesc.Visible = true;
                txtUbicacion.Visible = true;
                dtpFechaInicio.Text = lblFechaInicio.Text;
                dtpFechaFinal.Text = lblFechaFinal.Text;
                dtpFechaInicio.Enabled = true;
                dtpFechaFinal.Enabled = true;
                pbxConfirmarCambios.Visible = true;
                txtNombre.Text = lblNombre.Text;
                txtDesc.Text = lblDescripcion.Text;
                txtUbicacion.Text = lblUbicacion.Text;
                lblCancelar.Visible = true;
                pbxImagenEditar.Visible = true;
                pbxImagenEditar.Image = pbxImagen.Image;
                pbxSeleccionarImagen.Visible = true;
                lblNombre.Visible = false;
                lblDescripcion.Visible = false;
                lblUbicacion.Visible = false;
                lblFechaFinal.Visible = false;
                lblFechaInicio.Visible = false;
            }
            else
            {
                ReportarEvento?.Invoke(this, new PersonalizedArgs(Convert.ToString(idEvento)));
            }
        }

        static async Task<dynamic> EliminarEvento(string id, string token)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var data = new { id = id, token = token };
                    var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PutAsync("https://localhost:44340/eliminarEvento", content);
                    response.EnsureSuccessStatusCode();
                    var responseBody = await response.Content.ReadAsStringAsync();
                    dynamic resultado = JsonConvert.DeserializeObject(responseBody);
                    return resultado;
                }
                catch (Exception)
                {
                    MessageBox.Show("ERROR DE CONEXIÓN");
                    return "MAL";
                }
            }
        }
        public static async Task Salir(string user, string idevento, string token)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var datos = new { user = user, id = idevento, token = token };
                    var content = new StringContent(JsonConvert.SerializeObject(datos), Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PutAsync("https://localhost:44340/EliminarDelEvento", content);
                    response.EnsureSuccessStatusCode();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("ERROR AL LLAMAR A LA API");
                }
            }
        }
        private async void lblEliminar_Click(object sender, EventArgs e)
        {
            lblEditar.Visible = false;
            lblAdministradores.Visible = false;
            lblEliminar.Visible = false;
            if (lblEliminar.Text.Equals("Eliminar") || lblEliminar.Text.Equals("Delete"))
            {
                var resultado = await EliminarEvento(idEvento, token);
                if (idioma.Equals("English"))
                {
                    MessageBox.Show("Event deleted successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Evento eliminado correctamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                EventoEliminado?.Invoke(this, new PersonalizedArgs("Eliminado"));
            }
            else
            {
                await Salir(user, idEvento, token);
                EventoEliminado?.Invoke(this, new PersonalizedArgs("Eliminado"));
            }
        }

        static async Task<dynamic> Modificar(string id, string titulo, string fechaYhoraInicio, string fechaYhoraFinal, byte[] imagen, string ubicacion, string descripcion, string token)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var data = new { id = id, titulo = titulo, fechaYhora_Inicio = fechaYhoraInicio, fechaYhora_Final=fechaYhoraFinal, ubicacion=ubicacion, descripcion=descripcion, foto = Convert.ToBase64String(imagen), token = token };
                    var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PutAsync("https://localhost:44340/modificarEvento", content);
                    response.EnsureSuccessStatusCode();
                    var responseBody = await response.Content.ReadAsStringAsync();
                    dynamic resultado = JsonConvert.DeserializeObject(responseBody);
                    return resultado;
                }
                catch (Exception)
                {
                    MessageBox.Show("ERROR DE CONEXIÓN");
                    return "MAL";
                }
            }
        }
        private async void pbxConfirmarCambios_Click(object sender, EventArgs e)
        {
            MemoryStream ms = new MemoryStream();
            this.pbxImagenEditar.Image.Save(ms, ImageFormat.Jpeg);
            byte[] imagen = ms.ToArray();
            var respuesta = await Modificar(idEvento,txtNombre.Text,dtpFechaInicio.Text,dtpFechaFinal.Text, imagen, txtUbicacion.Text, txtDesc.Text, token);
            if (idioma.Equals("English"))
            {
                MessageBox.Show("Event modified successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Evento modificado correctamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            lblNombre.Text = txtNombre.Text;
            lblDescripcion.Text= txtDesc.Text;
            lblUbicacion.Text = txtUbicacion.Text;
            pbxImagen.Image = pbxImagenEditar.Image;
            pbxImagenEditar.Visible = false;
            txtNombre.Visible = false;
            txtDesc.Visible = false;
            txtUbicacion.Visible = false;
            dtpFechaInicio.Enabled = false;
            dtpFechaFinal.Enabled = false;
            pbxConfirmarCambios.Visible = false;
            lblCancelar.Visible = false;
            pbxSeleccionarImagen.Visible = false;
            lblNombre.Visible = true;
            lblDescripcion.Visible = true;
            lblUbicacion.Visible = true;
            lblFechaInicio.Text = dtpFechaInicio.Text;
            lblFechaFinal.Text = dtpFechaFinal.Text;
            lblFechaInicio.Visible = true;
            lblFechaFinal.Visible = true;
            dtpFechaInicio.Visible = false;
            dtpFechaFinal.Visible = false;
        }

        private void pbxSeleccionarImagen_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    pbxImagenEditar.Image = Image.FromFile(ofd.FileName);
                    pbxImagenEditar.SizeMode = PictureBoxSizeMode.StretchImage;
                }
            }
        }

        private void lblCancelar_Click(object sender, EventArgs e)
        {
            txtNombre.Visible = false;
            txtDesc.Visible = false;
            txtUbicacion.Visible = false;
            dtpFechaInicio.Enabled = false;
            dtpFechaFinal.Enabled = false;
            pbxConfirmarCambios.Visible = false;
            lblCancelar.Visible = false;
            pbxImagenEditar.Visible = false;
            pbxSeleccionarImagen.Visible = false;
            lblNombre.Visible = true;
            lblDescripcion.Visible = true;
            lblUbicacion.Visible = true;
        }
        static async Task<dynamic> Miembros(string idevento, string token)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var datos = new { id = idevento, token = token };
                    var content = new StringContent(JsonConvert.SerializeObject(datos), Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PutAsync($"https://localhost:44340/ObtenerParticipantesDelEvento", content);
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

        private async void lblAdministradores_Click(object sender, EventArgs e)
        {
            lblEditar.Visible = false;
            lblAdministradores.Visible = false;
            lblEliminar.Visible = false;
            panelPosts.Controls.Clear();
            panelPosts.Location = new Point(12, 228);
            var listaDeMiembros = await Miembros(idEvento, token);
            foreach (var elemento in listaDeMiembros)
            {
                if (!Convert.ToString(elemento.nombreDeCuenta).Equals(user))
                {
                    var groupControl = new Grupo_EventoParaListar(user, token, idevento: int.Parse(idEvento), usuariobuscar: elemento, modo: modo, idioma:idioma);
                    if (panelPosts.Controls.Count > 0)
                    {
                        var lastControl = panelPosts.Controls[panelPosts.Controls.Count - 1];
                        groupControl.Location = new Point(0, lastControl.Bottom);
                    }
                    else
                    {
                        groupControl.Location = new Point(100, 0);
                    }
                    panelPosts.Controls.Add(groupControl);
                }
            }
        }
    }
}
