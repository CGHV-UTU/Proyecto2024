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
    public partial class Grupo_EventoParaListar : UserControl
    {
        private string nombreReal;
        private int idevento;
        private string token;
        private string user;
        private dynamic datos;
        private dynamic datosDelUsuario;
        private bool busqueda;
        private string idpost;
        private dynamic grupo;
        private dynamic evento;
        private string modo;
        private string idioma;
        public event EventHandler<PersonalizedArgs> AbrirEvento;
        public event EventHandler<PersonalizedArgs> AbrirGrupo;
        public event EventHandler<PersonalizedArgs> AbrirUsuario;
        public Grupo_EventoParaListar(string usuario, string token, dynamic grupo=null, dynamic evento=null, dynamic usuariobuscar = null, bool busqueda = false, string idpost="",string nombreGrupo="", int idevento=0, string modo="Claro", string idioma="Español")
        {
            if (grupo!=null)
            {
                this.nombreReal = Convert.ToString(grupo.nombreReal);
                this.grupo = grupo;
            }
            else
            {
                this.nombreReal = nombreGrupo;
            }
            if (evento != null)
            {
                this.idevento = int.Parse(Convert.ToString(evento.idEvento));
                this.evento = evento;
            }
            else
            {
                this.idevento = idevento;
            }
            this.token = token;
            this.user = usuario;
            this.datosDelUsuario = usuariobuscar;
            this.idpost = idpost;
            this.busqueda = busqueda;
            this.modo = modo;
            this.idioma = idioma;
            InitializeComponent();
            Iniciar();
            AplicarDatos();
        }
        static async Task<dynamic> BuscarEvento(int id, string token)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var datos = new { id = id, token = token };
                    var content = new StringContent(JsonConvert.SerializeObject(datos), Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PutAsync($"https://localhost:44340/eventoPorId", content);
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

        static async Task<dynamic> BuscarGrupo(string nombreReal, string token)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var datos = new { nombreReal = nombreReal, token = token };
                    var content = new StringContent(JsonConvert.SerializeObject(datos), Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PutAsync($"https://localhost:44304/ObtenerGrupo", content);
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
        static async Task<dynamic> EsChatPrivado(string nombreReal, string token)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var datos = new { nombreReal = nombreReal, token = token };
                    var content = new StringContent(JsonConvert.SerializeObject(datos), Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PutAsync($"https://localhost:44304/EsChatPrivado", content);
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
        static async Task<dynamic> UnirseAlGrupo(string nombreReal,string nombre, string token)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var datos = new { nombreDeCuenta = nombre, nombreReal = nombreReal, rol= "solicitante", token = token };
                    var content = new StringContent(JsonConvert.SerializeObject(datos), Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PostAsync($"https://localhost:44304/EnviarSolicitudParaUnirseAlGrupo", content);
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

        static async Task<dynamic> ParticipaDelGrupo(string nombreReal, string nombre, string token)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var datos = new { nombreDeCuenta = nombre, nombreReal = nombreReal, token = token };
                    var content = new StringContent(JsonConvert.SerializeObject(datos), Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PutAsync($"https://localhost:44304/ParticipaDelGrupo", content);
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
        static async Task<string> conseguirImagenDelUsuario(string creador, string token)
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
        private bool esChatPrivado=false;
        private async void AplicarDatos()
        {
            if (this.datosDelUsuario == null)
            {
                if (evento!=null)
                {
                    this.lblNombre.Text = Convert.ToString(evento.titulo);
                    try
                    {
                        byte[] imagen = Convert.FromBase64String(Convert.ToString(evento.foto));
                        MemoryStream ms = new MemoryStream(imagen);
                        Bitmap bitmap = new Bitmap(ms);
                        this.PictureBoxImagen.Image = bitmap;
                        redondearPictureBox(bitmap);
                        if (this.PictureBoxImagen.Image == null)
                        {
                            MessageBox.Show("Imagen nula");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ha ocurrido un error " + ex);
                    }
                    datos = evento;
                }
                else
                {
                    if (busqueda == true)
                    {
                        this.lblNombre.Text = Convert.ToString(grupo.nombreVisible);
                        try
                        {
                            byte[] imagen = Convert.FromBase64String(Convert.ToString(grupo.foto));
                            MemoryStream ms = new MemoryStream(imagen);
                            Bitmap bitmap = new Bitmap(ms);
                            this.PictureBoxImagen.Image = bitmap;
                            redondearPictureBox(bitmap);
                            if (this.PictureBoxImagen.Image == null)
                            {
                                MessageBox.Show("Imagen nula");
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Ha ocurrido un error " + ex);
                        }
                        this.datos = grupo;
                        var respuesta = await ParticipaDelGrupo(nombreReal, user, token);
                        if (this.busqueda && !this.nombreReal.Equals("") && Convert.ToString(respuesta).Equals("No participa"))
                        {
                            //pbxUnirse
                            this.pbxUnirse = new PictureBox();
                            this.pbxUnirse.Location = new System.Drawing.Point(247, 7);
                            this.pbxUnirse.Name = "pbxUnirse";
                            this.pbxUnirse.Size = new System.Drawing.Size(50, 50);
                            this.pbxUnirse.SizeMode = PictureBoxSizeMode.StretchImage;
                            this.pbxUnirse.Image = Properties.Resources.grupos_removebg_preview;
                            this.pbxUnirse.Cursor = Cursors.Hand;
                            this.pbxUnirse.Visible = true;
                            this.pbxUnirse.Click += pbxUnirse_Click;
                            this.Controls.Add(this.pbxUnirse);
                        }
                    }
                    else
                    {
                        if (Convert.ToString(grupo.nombreVisible).Equals("--------------------"))
                        {
                            var datos = await EsChatPrivado(nombreReal, token);
                            esChatPrivado = true;
                            string[] lista = Convert.ToString(datos).Split('"');
                            string user1 = "";
                            string user2 = "";
                            int x = 1;
                            foreach (string palabra in lista)
                            {
                                if (!palabra.Equals("{") && !palabra.Equals("}") && !palabra.Equals(",") && !palabra.Equals(":") && !palabra.Equals("nombreDeCuenta1") && !palabra.Equals("nombreDeCuenta2"))
                                {
                                    if (x == 3)
                                    {
                                        user1 = palabra;
                                    }
                                    if (x == 6)
                                    {
                                        user2 = palabra;
                                    }
                                    x++;
                                }
                            }
                            string imagenB64;
                            if (user1.Equals(user))
                            {
                                this.lblNombre.Text = user2;
                                imagenB64 = await conseguirImagenDelUsuario(user2, token);
                                byte[] imagen = Convert.FromBase64String(imagenB64);
                                MemoryStream ms = new MemoryStream(imagen);
                                Bitmap bitmap = new Bitmap(ms);
                                this.PictureBoxImagen.Image = bitmap;
                                redondearPictureBox(bitmap);
                            }
                            else
                            {
                                this.lblNombre.Text = user1;
                                imagenB64 = await conseguirImagenDelUsuario(user1, token);
                                byte[] imagen = Convert.FromBase64String(imagenB64);
                                MemoryStream ms = new MemoryStream(imagen);
                                Bitmap bitmap = new Bitmap(ms);
                                this.PictureBoxImagen.Image = bitmap;
                                redondearPictureBox(bitmap);
                            }
                            var data = grupo;
                            data.foto = imagenB64;
                            data.nombreVisible = lblNombre.Text;
                            this.datos = data;
                        }
                        else
                        {
                            this.lblNombre.Text = Convert.ToString(grupo.nombreVisible);
                            try
                            {
                                byte[] imagen = Convert.FromBase64String(Convert.ToString(grupo.foto));
                                MemoryStream ms = new MemoryStream(imagen);
                                Bitmap bitmap = new Bitmap(ms);
                                this.PictureBoxImagen.Image = bitmap;
                                redondearPictureBox(bitmap);
                                if (this.PictureBoxImagen.Image == null)
                                {
                                    MessageBox.Show("Imagen nula");
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Ha ocurrido un error " + ex);
                            }
                            this.datos = grupo;
                        }
                    }
                }
            }
            else
            {
                this.pnlTop.Visible = false;
                this.pnlBot.BackColor = Color.Black;
                this.lblNombre.Text = this.datosDelUsuario.nombreVisible;
                try
                {
                    byte[] imagen = Convert.FromBase64String(Convert.ToString(this.datosDelUsuario.foto));
                    MemoryStream ms = new MemoryStream(imagen);
                    Bitmap bitmap = new Bitmap(ms);
                    this.PictureBoxImagen.Image = bitmap;
                    redondearPictureBox(bitmap);
                    if (this.PictureBoxImagen.Image == null)
                    {
                        MessageBox.Show("Imagen nula");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ha ocurrido un error " + ex);
                }
                if (!nombreReal.Equals(""))
                {
                    if (!busqueda)
                    {
                        if (!string.IsNullOrEmpty(nombreReal) && (Convert.ToString(grupo.rol).Equals("admin") || Convert.ToString(grupo.rol).Equals("creador")) && !user.Equals(Convert.ToString(this.datosDelUsuario.nombreReal)))
                        {
                            //pbxUnirse acá se usa para eliminar al usuario del grupo o darle admin
                            this.pbxUnirse = new PictureBox();
                            this.pbxUnirse.Location = new System.Drawing.Point(247, 7);
                            this.pbxUnirse.Name = "pbxUnirse";
                            this.pbxUnirse.Size = new System.Drawing.Size(50, 50);
                            this.pbxUnirse.SizeMode = PictureBoxSizeMode.StretchImage;
                            if (Convert.ToString(datosDelUsuario.rol).Equals("solicitante"))
                            {
                                this.pbxUnirse.Image = Frontend.Properties.Resources.aceptar;
                            }
                            else
                            {
                                this.pbxUnirse.Image = Properties.Resources.mas_opciones;
                            }
                            this.pbxUnirse.Cursor = Cursors.Hand;
                            this.pbxUnirse.Visible = true;
                            this.pbxUnirse.Click += pbxUnirse_Click;
                            this.Controls.Add(this.pbxUnirse);
                        }
                    }
                    else
                    {
                        var rol = await RolEnElGrupo(nombreReal, Convert.ToString(this.datosDelUsuario.nombreDeCuenta), token);
                        if(!Convert.ToString(rol).Equals("usuario") && !Convert.ToString(rol).Equals("creador") && !Convert.ToString(rol).Equals("admin") && !Convert.ToString(rol).Equals("solicitante"))
                        {
                            this.pbxUnirse = new PictureBox();
                            this.pbxUnirse.Location = new System.Drawing.Point(247, 7);
                            this.pbxUnirse.Name = "pbxUnirse";
                            this.pbxUnirse.Size = new System.Drawing.Size(50, 50);
                            this.pbxUnirse.SizeMode = PictureBoxSizeMode.StretchImage;
                            this.pbxUnirse.Image = Frontend.Properties.Resources.aceptar;
                            this.pbxUnirse.Cursor = Cursors.Hand;
                            this.pbxUnirse.Visible = true;
                            this.pbxUnirse.Click += pbxUnirse_Click;
                            this.Controls.Add(this.pbxUnirse);
                        }
                    }
                }
                if (idevento != 0)
                {
                    this.pbxUnirse = new PictureBox();
                    this.pbxUnirse.Location = new System.Drawing.Point(247, 7);
                    this.pbxUnirse.Name = "pbxUnirse";
                    this.pbxUnirse.Size = new System.Drawing.Size(50, 50);
                    this.pbxUnirse.SizeMode = PictureBoxSizeMode.StretchImage;
                    this.pbxUnirse.Image = Frontend.Properties.Resources.aceptar;
                    this.pbxUnirse.Cursor = Cursors.Hand;
                    this.pbxUnirse.Visible = true;
                    this.pbxUnirse.Click += pbxUnirse_Click;
                    this.Controls.Add(this.pbxUnirse);
                }
            }
        }
        public void redondearPictureBox(Image image)
        {
            if (image == null)
            {
                MessageBox.Show("La imagen es nula. No se puede redondear.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            System.Drawing.Drawing2D.GraphicsPath gp = new System.Drawing.Drawing2D.GraphicsPath();
            gp.AddEllipse(0, 0, this.PictureBoxImagen.Width, this.PictureBoxImagen.Height);
            Region rg = new Region(gp);
            this.PictureBoxImagen.Region = rg;
            this.PictureBoxImagen.Image = image;
        }
        private async void Iniciar()
        {
            this.lblNombre = new Label();
            this.PictureBoxImagen = new PictureBox();
            this.SuspendLayout();

            // lblNombre
            this.lblNombre.AutoSize = true;
            this.lblNombre.Location = new System.Drawing.Point(150, 19);
            this.lblNombre.Name = "lblNombre";
            this.lblNombre.Size = new System.Drawing.Size(70, 24);
            this.lblNombre.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.lblNombre.TabIndex = 0;

            // PictureBoxImagen
            this.PictureBoxImagen.Location = new System.Drawing.Point(77, 7);
            this.PictureBoxImagen.Name = "PictureBoxImagen";
            this.PictureBoxImagen.Size = new System.Drawing.Size(50, 50);
            this.PictureBoxImagen.SizeMode = PictureBoxSizeMode.StretchImage;
            this.PictureBoxImagen.Image = Properties.Resources.reportar;
            this.Cursor = Cursors.Hand;

            //pnlTop
            this.pnlTop.Location = new System.Drawing.Point(30, 3);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(300, 3);


            //pnlBot
            this.pnlBot.Location = new System.Drawing.Point(30, 61);
            this.pnlBot.Name = "pnlBot";
            this.pnlBot.Size = new System.Drawing.Size(300, 3);

            // Añadir controles
            this.Controls.Add(this.lblNombre);
            this.Controls.Add(this.PictureBoxImagen);
            this.Controls.Add(this.pnlTop);

            // Configuración final del control
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.Name = "GroupEventControl";
            this.Size = new System.Drawing.Size(350, 67);
            this.ResumeLayout(false);
            this.PerformLayout();

            if (!string.IsNullOrEmpty(this.idpost))
            {
                this.pbxUnirse = new PictureBox();
                this.pbxUnirse.Location = new System.Drawing.Point(247, 7);
                this.pbxUnirse.Name = "pbxUnirse";
                this.pbxUnirse.Size = new System.Drawing.Size(50, 50);
                this.pbxUnirse.SizeMode = PictureBoxSizeMode.StretchImage;
                this.pbxUnirse.Image = Properties.Resources.compartir;
                this.pbxUnirse.Cursor = Cursors.Hand;
                this.pbxUnirse.Visible = true;
                this.pbxUnirse.Click += pbxUnirse_Click;
                if (modo.Equals("Oscuro"))
                {
                    this.pbxUnirse.Image = Properties.Resources.compartir_claro;
                }
                this.Controls.Add(this.pbxUnirse);
            }
            if (!string.IsNullOrEmpty(nombreReal) && datosDelUsuario!=null && busqueda)
            {
                this.pbxUnirse = new PictureBox();
                this.pbxUnirse.Location = new System.Drawing.Point(247, 7);
                this.pbxUnirse.Name = "pbxUnirse";
                this.pbxUnirse.Size = new System.Drawing.Size(50, 50);
                this.pbxUnirse.SizeMode = PictureBoxSizeMode.StretchImage;
                this.pbxUnirse.Image = Properties.Resources.aceptar;
                this.pbxUnirse.Cursor = Cursors.Hand;
                this.pbxUnirse.Visible = true;
                this.pbxUnirse.Click += pbxUnirse_Click;
                this.Controls.Add(this.pbxUnirse);
            }

            if(datosDelUsuario==null && string.IsNullOrEmpty(idpost) && busqueda)
            {
                var rol = await RolEnElGrupo(nombreReal, user, token);
                if (!Convert.ToString(rol).Equals("solicitante") && !Convert.ToString(rol).Equals("creador") && !Convert.ToString(rol).Equals("usuario") && !Convert.ToString(rol).Equals("admin"))
                {
                    this.pbxUnirse = new PictureBox();
                    this.pbxUnirse.Location = new System.Drawing.Point(247, 7);
                    this.pbxUnirse.Name = "pbxUnirse";
                    this.pbxUnirse.Size = new System.Drawing.Size(50, 50);
                    this.pbxUnirse.SizeMode = PictureBoxSizeMode.StretchImage;
                    this.pbxUnirse.Image = Properties.Resources.crearPost_removebg_preview;
                    this.pbxUnirse.Cursor = Cursors.Hand;
                    this.pbxUnirse.Visible = true;
                    this.pbxUnirse.Click += pbxUnirse_Click;
                    if (modo.Equals("Oscuro"))
                    {
                        this.pbxUnirse.Image = Properties.Resources.crear_claro;
                    }
                    this.Controls.Add(this.pbxUnirse);
                }
            }
            if (modo.Equals("Oscuro"))
            {
                lblNombre.ForeColor = Color.White;
            }
        }

        private async void Grupo_EventoParaListar_Click(object sender, EventArgs e)
        {
            if (!busqueda)
            {
                if (this.datosDelUsuario!=null)
                {
                    AbrirUsuario?.Invoke(this, new PersonalizedArgs(Convert.ToString(this.datosDelUsuario.nombreReal)));
                }
                else
                {
                    if (idevento > 0)
                    {
                        AbrirEvento?.Invoke(this, new PersonalizedArgs(datos));
                    }
                    else
                    {
                        if (esChatPrivado)
                        {
                            AbrirGrupo?.Invoke(this, new PersonalizedArgs(datos, "es chat privado"));
                        }
                        else
                        {
                            AbrirGrupo?.Invoke(this, new PersonalizedArgs(datos));
                        }
                    }
                }
            }
            else
            {
                if (idevento > 0)
                {
                    var eventoCompleto = await BuscarEvento(int.Parse(Convert.ToString(evento.idEvento)), token);
                    AbrirEvento?.Invoke(this, new PersonalizedArgs(eventoCompleto));
                }
                else
                {
                    AbrirUsuario?.Invoke(this, new PersonalizedArgs(Convert.ToString(this.datosDelUsuario.nombreDeCuenta)));
                }
            }
        }

        static async Task<dynamic> CompartirPost(string usuario, string idPost, string grupo, string token)
        {
            using (HttpClient client=new HttpClient())
            {
                try
                {
                    var datos = new { user = usuario, nombreReal = grupo, id=idPost, token = token };
                    var content = new StringContent(JsonConvert.SerializeObject(datos), Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PutAsync($"https://localhost:44340/CompartirPost", content);
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
        static async Task<dynamic> AñadirUsuarioAlGrupo(string nombreReal, string nombreDeCuenta,string rol, string token)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var datos = new { nombreDeCuenta = nombreDeCuenta, nombreReal = nombreReal, rol=rol, token = token };
                    var content = new StringContent(JsonConvert.SerializeObject(datos), Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PostAsync($"https://localhost:44304/AgregarUsuarioAGrupo", content);
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
        private async void pbxUnirse_Click(object sender, EventArgs e)
        {
            if (datosDelUsuario==null)
            {
                if (!string.IsNullOrEmpty(this.idpost))
                {
                    string respuesta = await CompartirPost(user, idpost, nombreReal, token);
                    this.Controls.Remove(pbxUnirse);
                }
                else
                {
                    dynamic respuesta = await UnirseAlGrupo(nombreReal, user, token);
                    this.Controls.Remove(pbxUnirse);
                }
            }
            else
            {
                if (idevento == 0)
                {
                    if (!string.IsNullOrEmpty(nombreReal) && datosDelUsuario != null && busqueda)
                    {
                        await AñadirUsuarioAlGrupo(nombreReal, Convert.ToString(datosDelUsuario.nombreDeCuenta), "usuario", token);
                        this.Controls.Remove(pbxUnirse);
                    }
                    else
                    {
                        if (Convert.ToString(datosDelUsuario.rol).Equals("solicitante"))
                        {
                            await EliminarUsuarioDelGrupo(nombreReal, Convert.ToString(datosDelUsuario.nombreReal), token);
                            var respuesta = await AñadirUsuarioAlGrupo(nombreReal, Convert.ToString(datosDelUsuario.nombreReal), "usuario", token);
                            if (idioma.Equals("English"))
                            {
                                MessageBox.Show("Role changed successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show("Rol cambiado correctamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            this.Controls.Remove(pbxUnirse);
                        }
                        else
                        {
                            if (!this.Controls.Contains(this.lblEliminar))
                            {
                                this.lblEliminar = new Label();
                                this.lblDarOQuitarAdmin = new Label();
                                // Eliminar
                                this.lblEliminar.AutoSize = true;
                                this.lblEliminar.Location = new System.Drawing.Point(200, 15);
                                this.lblEliminar.Name = "lblEliminar";
                                this.lblEliminar.Size = new System.Drawing.Size(100, 24);
                                this.lblEliminar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
                                this.lblEliminar.TabIndex = 0;
                                this.lblEliminar.Text = "Eliminar";
                                this.lblEliminar.Click += lblEliminar_Click;

                                // DarOQuitarAdmin
                                this.lblDarOQuitarAdmin.AutoSize = true;
                                this.lblDarOQuitarAdmin.Location = new System.Drawing.Point(200, lblEliminar.Bottom + 10);
                                this.lblDarOQuitarAdmin.Name = "lblDarOQuitarAdmin";
                                this.lblDarOQuitarAdmin.Size = new System.Drawing.Size(100, 24);
                                this.lblDarOQuitarAdmin.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
                                this.lblDarOQuitarAdmin.TabIndex = 0;
                                this.lblDarOQuitarAdmin.Text = "Dar admin";
                                this.lblDarOQuitarAdmin.Click += lblDarOQuitarAdmin_Click;
                                if (modo.Equals("Oscuro"))
                                {
                                    lblEliminar.ForeColor = Color.White;
                                    lblDarOQuitarAdmin.ForeColor = Color.White;
                                }
                                this.Controls.Add(lblEliminar);
                                this.Controls.Add(lblDarOQuitarAdmin);
                            }
                            else
                            {
                                this.Controls.Remove(this.lblEliminar);
                                this.Controls.Remove(this.lblDarOQuitarAdmin);
                            }
                        }
                    }   
                }
                else
                {
                    string rol = await RolDelEvento(Convert.ToString(idevento), Convert.ToString(datosDelUsuario.nombreDeCuenta), token);
                    if (rol.Equals("Seguidor"))
                    {
                        await DarRolEvento(Convert.ToString(idevento), Convert.ToString(datosDelUsuario.nombreDeCuenta),"admin",token);
                        if (modo.Equals("Oscuro"))
                        {
                            this.pbxUnirse.Image = Frontend.Properties.Resources.salirBlanco;
                        }
                        else
                        {
                            this.pbxUnirse.Image = Frontend.Properties.Resources.salir;
                        }
                    }
                    else
                    {
                        await DarRolEvento(Convert.ToString(idevento), Convert.ToString(datosDelUsuario.nombreDeCuenta), "Seguidor", token);
                        this.pbxUnirse.Image = Frontend.Properties.Resources.aceptar;
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
        static async Task<dynamic> DarRolEvento(string idevento, string usuario, string rol, string token)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var dato = new { id = idevento, user = usuario, rol = rol, token = token };
                    var content = new StringContent(JsonConvert.SerializeObject(dato), Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PutAsync("https://localhost:44340/DarRolEvento", content);
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

        static async Task<dynamic> EliminarUsuarioDelGrupo(string nombreReal, string nombreDeCuenta, string token)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var datos = new { nombreDeCuenta = nombreDeCuenta, nombreReal = nombreReal, token = token };
                    var content = new StringContent(JsonConvert.SerializeObject(datos), Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PutAsync($"https://localhost:44304/EliminarUsuarioDeGrupo", content);
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
        private async void lblEliminar_Click(object sender, EventArgs e)
        {
            var respuesta = await EliminarUsuarioDelGrupo(nombreReal, Convert.ToString(datosDelUsuario.nombreReal), token);
            if (idioma.Equals("English"))
            {
                MessageBox.Show("User deleted from the group successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Usuario eliminado del grupo de forma correcta", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            this.Controls.Remove(this.lblEliminar);
            this.Controls.Remove(this.lblDarOQuitarAdmin);
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
        private async void lblDarOQuitarAdmin_Click(object sender, EventArgs e)
        {
            var rol = await RolEnElGrupo(nombreReal, Convert.ToString(datosDelUsuario.nombreReal), token);
            if (Convert.ToString(rol).Equals("usuario"))
            {
                await EliminarUsuarioDelGrupo(nombreReal, Convert.ToString(datosDelUsuario.nombreReal), token);
                var respuesta = await AñadirUsuarioAlGrupo(nombreReal, Convert.ToString(datosDelUsuario.nombreReal), "admin", token);
                if (Convert.ToString(respuesta).Equals("Usuario agregado al grupo"))
                {
                    if (idioma.Equals("English"))
                    {
                        MessageBox.Show("Administrator assigned", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Administrador asignado", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            else
            {
                await EliminarUsuarioDelGrupo(nombreReal, Convert.ToString(datosDelUsuario.nombreReal), token);
                var respuesta = await AñadirUsuarioAlGrupo(nombreReal, Convert.ToString(datosDelUsuario.nombreReal), "usuario", token);
                if (Convert.ToString(respuesta).Equals("Usuario agregado al grupo"))
                {
                    if (idioma.Equals("English"))
                    {
                        MessageBox.Show("Administrator removed", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Administrador eliminado", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            this.Controls.Remove(this.lblEliminar);
            this.Controls.Remove(this.lblDarOQuitarAdmin);
        }
    }
}