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
        private string configuracion;
        private string user;
        private TextBox txtURL;
        private string token;
        private Label lblName;
        private Label lblEditando;
        private Panel pnlCrear;
        private Label lblEliminar;
        private Label lblEditar;
        private PictureBox pbxSeleccionarImagen;
        private TextBox txtNombre;
        private PictureBox pbxFotoGrupoEditar;
        private Label lblCancelar;
        private PictureBox pbxConfirmarCambios;
        private Label lblAñadir;
        private string idUltimoMensaje;
        public event EventHandler GrupoEliminado;
        public event EventHandler<PersonalizedArgs> AbrirUsuario;
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
            pnlCrear.Visible = false;
            pbxConfirmarCambios.Visible = false;
            pbxSeleccionarImagen.Visible = false;
            txtNombre.Visible = false;
            lblCancelar.Visible = false;
            lblEditar.Visible = false;
            lblEliminar.Visible = false;
            pbxFotoGrupoEditar.Visible = false;
            lblAñadir.Visible = false;
        }
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GruposComunidad));
            this.pbxImagen = new System.Windows.Forms.PictureBox();
            this.lblNombre = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblEditando = new System.Windows.Forms.Label();
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
            this.lblAñadir = new System.Windows.Forms.Label();
            this.lblCancelar = new System.Windows.Forms.Label();
            this.pbxConfirmarCambios = new System.Windows.Forms.PictureBox();
            this.pbxSeleccionarImagen = new System.Windows.Forms.PictureBox();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.pbxFotoGrupoEditar = new System.Windows.Forms.PictureBox();
            this.lblEliminar = new System.Windows.Forms.Label();
            this.lblEditar = new System.Windows.Forms.Label();
            this.pnlCrear = new System.Windows.Forms.Panel();
            this.pnlPostsGrupo = new System.Windows.Forms.Panel();
            this.lblName = new System.Windows.Forms.Label();
            this.lblPostsGrupo = new System.Windows.Forms.Label();
            this.lblChat = new System.Windows.Forms.Label();
            this.pbxFotoGrupo = new System.Windows.Forms.PictureBox();
            this.pnlChat = new System.Windows.Forms.Panel();
            this.txtURL = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
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
            ((System.ComponentModel.ISupportInitialize)(this.pbxConfirmarCambios)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxSeleccionarImagen)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxFotoGrupoEditar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxFotoGrupo)).BeginInit();
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
            this.panel1.Location = new System.Drawing.Point(8, 649);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(971, 65);
            this.panel1.TabIndex = 49;
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
            this.pbxCrearPostGrupo.Location = new System.Drawing.Point(450, 615);
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
            this.PictureBoxConfiguraciones.Click += new System.EventHandler(this.PictureBoxConfiguraciones_Click);
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
            this.lblMiembros.Click += new System.EventHandler(this.lblMiembros_Click);
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
            this.pnlAsociarContenido.Location = new System.Drawing.Point(3, 520);
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
            this.pnlGruposComunidad.Controls.Add(this.lblAñadir);
            this.pnlGruposComunidad.Controls.Add(this.lblCancelar);
            this.pnlGruposComunidad.Controls.Add(this.pbxConfirmarCambios);
            this.pnlGruposComunidad.Controls.Add(this.pbxSeleccionarImagen);
            this.pnlGruposComunidad.Controls.Add(this.txtNombre);
            this.pnlGruposComunidad.Controls.Add(this.pbxFotoGrupoEditar);
            this.pnlGruposComunidad.Controls.Add(this.lblEliminar);
            this.pnlGruposComunidad.Controls.Add(this.lblEditar);
            this.pnlGruposComunidad.Controls.Add(this.pbxCrearPostGrupo);
            this.pnlGruposComunidad.Controls.Add(this.pnlCrear);
            this.pnlGruposComunidad.Controls.Add(this.pnlAsociarContenido);
            this.pnlGruposComunidad.Controls.Add(this.pnlPostsGrupo);
            this.pnlGruposComunidad.Controls.Add(this.lblName);
            this.pnlGruposComunidad.Controls.Add(this.lblPostsGrupo);
            this.pnlGruposComunidad.Controls.Add(this.lblChat);
            this.pnlGruposComunidad.Controls.Add(this.pbxFotoGrupo);
            this.pnlGruposComunidad.Controls.Add(this.panel1);
            this.pnlGruposComunidad.Controls.Add(this.pnlChat);
            this.pnlGruposComunidad.Controls.Add(this.txtURL);
            this.pnlGruposComunidad.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlGruposComunidad.Location = new System.Drawing.Point(0, 0);
            this.pnlGruposComunidad.Name = "pnlGruposComunidad";
            this.pnlGruposComunidad.Size = new System.Drawing.Size(996, 717);
            this.pnlGruposComunidad.TabIndex = 47;
            // 
            // lblAñadir
            // 
            this.lblAñadir.AutoSize = true;
            this.lblAñadir.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAñadir.Location = new System.Drawing.Point(801, 47);
            this.lblAñadir.Name = "lblAñadir";
            this.lblAñadir.Size = new System.Drawing.Size(122, 20);
            this.lblAñadir.TabIndex = 89;
            this.lblAñadir.Text = "Añadir Usuarios";
            this.lblAñadir.Click += new System.EventHandler(this.lblAñadir_Click);
            // 
            // lblCancelar
            // 
            this.lblCancelar.AutoSize = true;
            this.lblCancelar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCancelar.Location = new System.Drawing.Point(537, 48);
            this.lblCancelar.Name = "lblCancelar";
            this.lblCancelar.Size = new System.Drawing.Size(72, 20);
            this.lblCancelar.TabIndex = 88;
            this.lblCancelar.Text = "Cancelar";
            // 
            // pbxConfirmarCambios
            // 
            this.pbxConfirmarCambios.Image = global::Frontend.Properties.Resources.aceptar;
            this.pbxConfirmarCambios.Location = new System.Drawing.Point(465, 10);
            this.pbxConfirmarCambios.Name = "pbxConfirmarCambios";
            this.pbxConfirmarCambios.Size = new System.Drawing.Size(50, 50);
            this.pbxConfirmarCambios.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbxConfirmarCambios.TabIndex = 87;
            this.pbxConfirmarCambios.TabStop = false;
            this.pbxConfirmarCambios.Click += new System.EventHandler(this.pbxConfirmarCambios_Click);
            // 
            // pbxSeleccionarImagen
            // 
            this.pbxSeleccionarImagen.Image = global::Frontend.Properties.Resources.Foto;
            this.pbxSeleccionarImagen.Location = new System.Drawing.Point(122, 7);
            this.pbxSeleccionarImagen.Name = "pbxSeleccionarImagen";
            this.pbxSeleccionarImagen.Size = new System.Drawing.Size(50, 50);
            this.pbxSeleccionarImagen.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbxSeleccionarImagen.TabIndex = 86;
            this.pbxSeleccionarImagen.TabStop = false;
            this.pbxSeleccionarImagen.Click += new System.EventHandler(this.pbxSeleccionarImagen_Click);
            // 
            // txtNombre
            // 
            this.txtNombre.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNombre.Location = new System.Drawing.Point(122, 59);
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(161, 38);
            this.txtNombre.TabIndex = 85;
            // 
            // pbxFotoGrupoEditar
            // 
            this.pbxFotoGrupoEditar.Image = global::Frontend.Properties.Resources.Usuario;
            this.pbxFotoGrupoEditar.Location = new System.Drawing.Point(17, 12);
            this.pbxFotoGrupoEditar.Name = "pbxFotoGrupoEditar";
            this.pbxFotoGrupoEditar.Size = new System.Drawing.Size(90, 90);
            this.pbxFotoGrupoEditar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbxFotoGrupoEditar.TabIndex = 84;
            this.pbxFotoGrupoEditar.TabStop = false;
            // 
            // lblEliminar
            // 
            this.lblEliminar.AutoSize = true;
            this.lblEliminar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEliminar.Location = new System.Drawing.Point(858, 27);
            this.lblEliminar.Name = "lblEliminar";
            this.lblEliminar.Size = new System.Drawing.Size(65, 20);
            this.lblEliminar.TabIndex = 83;
            this.lblEliminar.Text = "Eliminar";
            this.lblEliminar.Click += new System.EventHandler(this.lblEliminar_Click);
            // 
            // lblEditar
            // 
            this.lblEditar.AutoSize = true;
            this.lblEditar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEditar.Location = new System.Drawing.Point(872, 7);
            this.lblEditar.Name = "lblEditar";
            this.lblEditar.Size = new System.Drawing.Size(51, 20);
            this.lblEditar.TabIndex = 82;
            this.lblEditar.Text = "Editar";
            this.lblEditar.Click += new System.EventHandler(this.lblEditar_Click);
            // 
            // pnlCrear
            // 
            this.pnlCrear.Location = new System.Drawing.Point(281, 18);
            this.pnlCrear.Name = "pnlCrear";
            this.pnlCrear.Size = new System.Drawing.Size(436, 10);
            this.pnlCrear.TabIndex = 0;
            // 
            // pnlPostsGrupo
            // 
            this.pnlPostsGrupo.AutoScroll = true;
            this.pnlPostsGrupo.BackColor = System.Drawing.Color.LightSkyBlue;
            this.pnlPostsGrupo.Location = new System.Drawing.Point(12, 110);
            this.pnlPostsGrupo.Name = "pnlPostsGrupo";
            this.pnlPostsGrupo.Size = new System.Drawing.Size(971, 499);
            this.pnlPostsGrupo.TabIndex = 80;
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
            this.pnlChat.Location = new System.Drawing.Point(13, 113);
            this.pnlChat.Name = "pnlChat";
            this.pnlChat.Size = new System.Drawing.Size(971, 499);
            this.pnlChat.TabIndex = 79;
            // 
            // txtURL
            // 
            this.txtURL.Location = new System.Drawing.Point(71, 618);
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
            // GruposComunidad
            // 
            this.ClientSize = new System.Drawing.Size(996, 717);
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
            ((System.ComponentModel.ISupportInitialize)(this.pbxConfirmarCambios)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxSeleccionarImagen)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxFotoGrupoEditar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxFotoGrupo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        static async Task<dynamic> RolEnElGrupo(string nombreReal, string nombre, string token)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var datos = new { nombreDeCuenta = nombre, nombreReal = nombreReal, token = token };
                    var content = new StringContent(JsonConvert.SerializeObject(datos), Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PutAsync($"https://localhost:44304/ObtenerRolDelUsuarioEnElGrupo", content);
                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();
                    dynamic data = JsonConvert.DeserializeObject(responseBody);
                    return data;
                }
                catch
                {
                    return "ERROR";
                }
            }
        }
        private string rol;
        public async void TieneConfiguraciones()
        {
            var respuesta = await RolEnElGrupo(nombreGrupo, user, token);
            rol = Convert.ToString(respuesta);
            if (rol.Equals("admin") || rol.Equals("creador"))
            {
                PictureBoxConfiguraciones.Visible = true;
            }
            else
            {
                PictureBoxConfiguraciones.Visible = false;
            }
        }

        private void AplicarDatos(dynamic groupData)
        {
            this.lblName.Text = groupData.nombreVisible;
            byte[] imagen = Convert.FromBase64String(Convert.ToString(groupData.foto));
            MemoryStream ms = new MemoryStream(imagen);
            Bitmap bitmap = new Bitmap(ms);
            this.pbxFotoGrupo.Image = bitmap;
            configuracion = groupData.configuracion;
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
                    MessageBox.Show("Nuevo ciclo");
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
                    MessageControl messageControl = new MessageControl(mensaje, user,token);
                    messageControl.EditarMensaje += MessageControl_EditarMensaje;
                    messageControl.MensajeEliminado+= MessageControl_RefrescarMensajes;
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

        private void MessageControl_RefrescarMensajes(object sender, PersonalizedArgs e)
        {
            pnlChat.Controls.Clear();
            AñadirMensajes();
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
                    if (pictureBox2.Image == null)
                    {
                        data = new byte[0];
                    }
                    else
                    {
                        MemoryStream ms = new MemoryStream();
                        pictureBox2.Image.Save(ms, ImageFormat.Jpeg);
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
                    pnlChat.Controls.Clear();
                    AñadirMensajes();
                }
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Archivos de imagen|*.png;*.jpg;*.jpeg"; //Para que sólo aparezcan fotos
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                pictureBox2.ImageLocation = ofd.FileName;
                pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
                pictureBox2.Visible = true;
            }
        }

        private void lblChat_Click(object sender, EventArgs e)
        {
            pnlPostsGrupo.Visible = false;
            pnlChat.Visible = true;
            panel1.Visible = true;
            pbxCrearPostGrupo.Visible = false;
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
            if (pnlCrear.Visible == true)
            {
                pnlCrear.Visible = false;
                pnlCrear.Controls.Clear();
            }
            pnlPostsGrupo.Controls.Clear();
            pnlPostsGrupo.Parent = this;
            pnlPostsGrupo.Location = new Point(13, 113);
            pnlChat.Visible = false;
            pnlPostsGrupo.Visible = true;
            pnlPostsGrupo.BringToFront();
            pbxCrearPostGrupo.Visible = true;
            pnlAsociarContenido.Visible = false;
            panel1.Visible = false;
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
            pnlCrear.Visible = true;
            pnlCrear.Height = 692;
            Post crearPostGrupo = new Post(user, token, "", nombreGrupo);
            crearPostGrupo.TopLevel = false;
            crearPostGrupo.FormBorderStyle = FormBorderStyle.None;
            crearPostGrupo.Creado += lblPostsGrupo_Click;
            pnlCrear.Controls.Add(crearPostGrupo);
            crearPostGrupo.Show();
            pnlCrear.BringToFront();
            pnlPostsGrupo.Visible = false;
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

        private void PictureBoxConfiguraciones_Click(object sender, EventArgs e)
        {
            if (lblEditar.Visible == false)
            {
                lblEditar.Visible = true;
                if (rol.Equals("creador"))
                {
                    lblEliminar.Visible = true;
                }
                lblAñadir.Visible = true;
            }
            else
            {
                lblEditar.Visible = false;
                lblEliminar.Visible = false;
                lblAñadir.Visible = false;
            }
        }
        static async Task<dynamic> Miembros(string grupo, string token)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var datos = new { nombreReal = grupo, token = token };
                    var content = new StringContent(JsonConvert.SerializeObject(datos), Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PutAsync($"https://localhost:44304/ObtenerUsuariosDelGrupo", content);
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

        private async void lblMiembros_Click(object sender, EventArgs e)
        {
            if (pnlCrear.Visible == true)
            {
                pnlCrear.Visible = false;
                pnlCrear.Controls.Clear();
            }
            pnlPostsGrupo.Controls.Clear();
            pnlPostsGrupo.Parent = this;
            pnlPostsGrupo.Location = new Point(13, 113);
            pnlChat.Visible = false;
            pnlPostsGrupo.Visible = true;
            pnlPostsGrupo.BringToFront();
            pbxCrearPostGrupo.Visible = true;
            pnlAsociarContenido.Visible = false;
            panel1.Visible = false;
            var listaDeUsuarios = await Miembros(nombreGrupo, token);
            if (listaDeUsuarios !=null)
            {
                foreach (var elemento in listaDeUsuarios)
                {
                    if (!Convert.ToString(elemento.rol).Equals("solicitante"))
                    {
                        var groupControl = new Grupo_EventoParaListar(user, token, nombreRealGrupo: nombreGrupo, usuariobuscar: elemento);
                        groupControl.AbrirUsuario += Grupo_EventoParaListar_AbrirUsuario;
                        if (pnlPostsGrupo.Controls.Count > 0)
                        {
                            var lastControl = pnlPostsGrupo.Controls[pnlPostsGrupo.Controls.Count - 1];
                            groupControl.Location = new Point(0, lastControl.Bottom);
                        }
                        else
                        {
                            groupControl.Location = new Point(0, 52);
                        }
                        pnlPostsGrupo.Controls.Add(groupControl);
                    }
                }
            }
        }
        private void Grupo_EventoParaListar_AbrirUsuario(object sender, PersonalizedArgs e)
        {
            AbrirUsuario?.Invoke(this, new PersonalizedArgs(e.arg));
        }
        private void lblEditar_Click(object sender, EventArgs e)
        {
            pbxSeleccionarImagen.Visible = true;
            pbxFotoGrupoEditar.Visible = true;
            txtNombre.Visible = true;
            lblName.Visible = false;
            lblMiembros.Visible = false;
            txtNombre.Text = lblName.Text;
            pbxConfirmarCambios.Visible = true;
            lblCancelar.Visible = true;
            pbxFotoGrupoEditar.Image = pbxFotoGrupo.Image;
            lblEditar.Visible = false;
            lblEliminar.Visible = false;
        }
        static async Task<dynamic> EliminarGrupo(string nombreReal, string token)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var data = new { nombreReal = nombreReal, token = token };
                    var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PutAsync("https://localhost:44304/EliminarGrupo", content);
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

        private async void lblEliminar_Click(object sender, EventArgs e)
        {
            var respuesta = await EliminarGrupo(nombreGrupo,token);
            GrupoEliminado?.Invoke(this, EventArgs.Empty);
        }

        static async Task<dynamic> Modificar(string nombrereal, string nombreVisible, string configuracion, byte[] imagen, string token)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var data = new { nombreReal=nombrereal, nombreVisible=nombreVisible, configuracion=configuracion, imagen = Convert.ToBase64String(imagen), token = token };
                    var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PutAsync("https://localhost:44304/EditarGrupo", content);
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
            pbxSeleccionarImagen.Visible = false;
            pbxFotoGrupoEditar.Visible = false;
            txtNombre.Visible = false;
            lblName.Visible = true;
            lblMiembros.Visible = true;
            lblName.Text = txtNombre.Text;
            pbxConfirmarCambios.Visible = false;
            lblCancelar.Visible = false;
            pbxFotoGrupo.Image = pbxFotoGrupoEditar.Image;
            MemoryStream ms = new MemoryStream();
            this.pbxFotoGrupo.Image.Save(ms, ImageFormat.Jpeg);
            byte[] imagen = ms.ToArray();
            var resultado = await Modificar(nombreGrupo,lblName.Text,configuracion,imagen,token);
            MessageBox.Show(""+resultado);
        }

        private void pbxSeleccionarImagen_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    pbxFotoGrupoEditar.Image = Image.FromFile(ofd.FileName);
                    pbxFotoGrupoEditar.SizeMode = PictureBoxSizeMode.StretchImage;
                }
            }
        }

        private async void lblAñadir_Click(object sender, EventArgs e)
        {
            if (pnlCrear.Visible == true)
            {
                pnlCrear.Visible = false;
                pnlCrear.Controls.Clear();
            }
            pnlPostsGrupo.Controls.Clear();
            pnlPostsGrupo.Parent = this;
            pnlPostsGrupo.Location = new Point(13, 113);
            pnlChat.Visible = false;
            pnlPostsGrupo.Visible = true;
            pnlPostsGrupo.BringToFront();
            pbxCrearPostGrupo.Visible = true;
            pnlAsociarContenido.Visible = false;
            panel1.Visible = false;
            var listaDeUsuarios = await Miembros(nombreGrupo, token);
            if (listaDeUsuarios != null)
            {
                foreach (var elemento in listaDeUsuarios)
                {
                    if (Convert.ToString(elemento.rol).Equals("solicitante"))
                    {
                        var groupControl = new Grupo_EventoParaListar(user, token, nombreRealGrupo: nombreGrupo,usuariobuscar: elemento);
                        groupControl.AbrirUsuario +=Grupo_EventoParaListar_AbrirUsuario;
                        if (pnlPostsGrupo.Controls.Count > 0)
                        {
                            var lastControl = pnlPostsGrupo.Controls[pnlPostsGrupo.Controls.Count - 1];
                            groupControl.Location = new Point(0, lastControl.Bottom);
                        }
                        else
                        {
                            groupControl.Location = new Point(0, 52);
                        }
                        pnlPostsGrupo.Controls.Add(groupControl);
                    }
                }
            }
        }
    }
}
