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
        public event EventHandler<PersonalizedArgs> AbrirEvento;
        public event EventHandler<PersonalizedArgs> AbrirGrupo;
        public event EventHandler<PersonalizedArgs> AbrirUsuario;
        public Grupo_EventoParaListar(string usuario, string token, string nombreRealGrupo = "", int idEvento = 0, dynamic usuariobuscar = null, bool busqueda = false, string idpost="")
        {
            this.nombreReal = nombreRealGrupo;
            this.idevento = idEvento;
            this.token = token;
            this.user = usuario;
            this.datosDelUsuario = usuariobuscar;
            this.idpost = idpost;
            this.busqueda = busqueda;
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
                if (idevento > 0)
                {
                    var data = await BuscarEvento(idevento, token);
                    this.lblNombre.Text = data.titulo;
                    try
                    {
                        byte[] imagen = Convert.FromBase64String(Convert.ToString(data.foto));
                        MemoryStream ms = new MemoryStream(imagen);
                        Bitmap bitmap = new Bitmap(ms);
                        this.PictureBoxImagen.Image = bitmap;
                        if (this.PictureBoxImagen.Image == null)
                        {
                            MessageBox.Show("Imagen nula");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ha ocurrido un error " + ex);
                    }
                    datos = data;
                }
                else
                {
                    if (busqueda == true)
                    {
                        var data = await BuscarGrupo(nombreReal, token);
                        this.lblNombre.Text = data.nombreVisible;
                        try
                        {
                            byte[] imagen = Convert.FromBase64String(Convert.ToString(data.foto));
                            MemoryStream ms = new MemoryStream(imagen);
                            Bitmap bitmap = new Bitmap(ms);
                            this.PictureBoxImagen.Image = bitmap;
                            if (this.PictureBoxImagen.Image == null)
                            {
                                MessageBox.Show("Imagen nula");
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Ha ocurrido un error " + ex);
                        }
                        this.datos = data;
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
                    {
                        var datos = await EsChatPrivado(nombreReal, token);
                        if (datos != null && !Convert.ToString(datos).Equals("No participa") && !Convert.ToString(datos).Equals("Token expirado") && !Convert.ToString(datos).Equals("Hubo un error"))
                        {
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
                            }
                            else
                            {
                                this.lblNombre.Text = user1;
                                imagenB64 = await conseguirImagenDelUsuario(user1, token);
                                byte[] imagen = Convert.FromBase64String(imagenB64);
                                MemoryStream ms = new MemoryStream(imagen);
                                Bitmap bitmap = new Bitmap(ms);
                                this.PictureBoxImagen.Image = bitmap;
                            }
                            var data = await BuscarGrupo(nombreReal, token);
                            data.foto = imagenB64;
                            data.nombreVisible = lblNombre.Text;
                            this.datos = data;
                        }
                        else
                        {
                            var data = await BuscarGrupo(nombreReal, token);
                            this.lblNombre.Text = data.nombreVisible;
                            try
                            {
                                byte[] imagen = Convert.FromBase64String(Convert.ToString(data.foto));
                                MemoryStream ms = new MemoryStream(imagen);
                                Bitmap bitmap = new Bitmap(ms);
                                this.PictureBoxImagen.Image = bitmap;
                                if (this.PictureBoxImagen.Image == null)
                                {
                                    MessageBox.Show("Imagen nula");
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Ha ocurrido un error " + ex);
                            }
                            this.datos = data;
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
                    var rol = await RolEnElGrupo(nombreReal, user, token);
                    if (!string.IsNullOrEmpty(nombreReal) && (Convert.ToString(rol).Equals("admin") || Convert.ToString(rol).Equals("creador")) && !user.Equals(Convert.ToString(this.datosDelUsuario.nombreReal)))
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
            }
        }

        private void Iniciar()
        {
            this.lblNombre = new Label();
            this.PictureBoxImagen = new PictureBox();
            this.SuspendLayout();

            // lblNombre
            this.lblNombre.AutoSize = true;
            this.lblNombre.Location = new System.Drawing.Point(150, 19);
            this.lblNombre.Name = "lblNombre";
            this.lblNombre.Size = new System.Drawing.Size(100, 24);
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
                this.Controls.Add(this.pbxUnirse);
            }
        }

        private void Grupo_EventoParaListar_Click(object sender, EventArgs e)
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
                    AbrirEvento?.Invoke(this, new PersonalizedArgs(datos));
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
                    MessageBox.Show(respuesta);
                }
                else
                {
                    dynamic respuesta = await UnirseAlGrupo(nombreReal, user, token);
                    MessageBox.Show("" + respuesta);
                }
            }
            else
            {
                if (Convert.ToString(datosDelUsuario.rol).Equals("solicitante"))
                {
                    await EliminarUsuarioDelGrupo(nombreReal, Convert.ToString(datosDelUsuario.nombreReal), token);
                    var respuesta = await AñadirUsuarioAlGrupo(nombreReal, Convert.ToString(datosDelUsuario.nombreReal),"usuario" ,token);
                    MessageBox.Show(""+respuesta);
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
            MessageBox.Show(""+respuesta);
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
                    MessageBox.Show("Administrador asignado");
                }
            }
            else
            {
                await EliminarUsuarioDelGrupo(nombreReal, Convert.ToString(datosDelUsuario.nombreReal), token);
                var respuesta = await AñadirUsuarioAlGrupo(nombreReal, Convert.ToString(datosDelUsuario.nombreReal), "usuario", token);
                if (Convert.ToString(respuesta).Equals("Usuario agregado al grupo"))
                {
                    MessageBox.Show("Administrador eliminado");
                }
            }
            this.Controls.Remove(this.lblEliminar);
            this.Controls.Remove(this.lblDarOQuitarAdmin);
        }
    }
}