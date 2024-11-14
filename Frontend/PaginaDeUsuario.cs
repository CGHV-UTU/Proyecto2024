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
    public partial class PaginaDeUsuario : Form
    {
        private string nombreDeCreador;
        private string modo;
        private string user;
        private string token;
        private string interaccion;
        private string idioma;
        public event EventHandler<PersonalizedArgs> AbrirComentarios;
        public event EventHandler<PersonalizedArgs> ReportarPost;
        public event EventHandler<PersonalizedArgs> AbrirGrupo;
        public event EventHandler<PersonalizedArgs> ReportarUsuario;
        public event EventHandler<PersonalizedArgs> NuevaImagen;
        public PaginaDeUsuario(string nombreCreador, string modo, string user, string token, string idioma)
        {
            this.nombreDeCreador = nombreCreador;
            this.modo = modo;
            this.user = user;
            this.token = token;
            this.idioma = idioma;
            InitializeComponent();
            Iniciar();
            LoadPosts();
            txtDescripcion.Visible = false;
            txtNombre.Visible = false;
            pbxImagenEditar.Visible = false;
            btnConfirmar.Visible = false;
        }
        static async Task<dynamic> obtenerImagenNombreVyDescUsuario(string creador, string token)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var datos = new { nombreDeCuenta = creador, token=token };
                    var content = new StringContent(JsonConvert.SerializeObject(datos), Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PutAsync($"https://localhost:44383/user/obtenerImagenNombreVyDescUsuario", content);
                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();
                    dynamic data = JsonConvert.DeserializeObject(responseBody);
                    return new string[] {data.nombreVisible, data.descripcion, data.foto, data.seguidores };
                }
                catch
                {
                    MessageBox.Show("Error de conexión");
                    return null;
                }
            }
        }
        static async Task<dynamic> ConseguirPosts(string nombreDeCreador, string token)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var dato = new { user = nombreDeCreador, token=token};
                    var content = new StringContent(JsonConvert.SerializeObject(dato), Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PutAsync("https://localhost:44340/seleccionarTodosLosPostDelUsuario",content);
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
            var posts = await ConseguirPosts(nombreDeCreador, token);
            if (posts != null && !Convert.ToString(posts).Equals("Error al cargar Datagrid"))
            {
                foreach (var post in posts)
                {
                    var postControl = new PostControl(post, modo, user, token,idioma);
                    postControl.AbrirComentarios += PostControl_AbrirComentarios;
                    postControl.ReportarPost += PostControl_ReportarPost;
                    await postControl.aplicarDatos();
                    // Calcula la ubicación Y acumulada
                    int currentYPosition = PictureBoxUsuario.Bottom+10;
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

        private async void Iniciar()
        {   
            var datos=await obtenerImagenNombreVyDescUsuario(nombreDeCreador,token);
            if (datos != null)
            {
                string descripcion = datos[1];
                this.lblDescripcion.Text = descripcion;
                string imagenB64 = datos[2];
                byte[] imagen = Convert.FromBase64String(imagenB64);
                MemoryStream ms = new MemoryStream(imagen);
                Bitmap bitmap = new Bitmap(ms);
                this.PictureBoxUsuario.Image = bitmap;
                this.PictureBoxUsuario.SizeMode = PictureBoxSizeMode.StretchImage;
                this.lblNombre.Text = datos[0];
                if (datos[3] != null)
                {
                    if (int.Parse(datos[3]) > 0)
                    {
                        this.lblSeguidores.Text = datos[3];
                    }
                    else
                    {
                        this.lblSeguidores.Visible = false;
                    }
                }
                else
                {
                    this.lblSeguidores.Text = "";
                }
            }
            this.SuspendLayout();
            // panelPosts
            this.panelPosts.AutoScroll = true;
            this.panelPosts.Location = new System.Drawing.Point(12, 200);
            this.panelPosts.Name = "panelPosts";
            this.panelPosts.Size = new System.Drawing.Size(972, 424);
            this.panelPosts.TabIndex = 0;
            this.BackColor = Color.LightGray;
            // Form1
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1012, 613);
            this.Controls.Add(this.panelPosts);
            this.Name = "Form1";
            this.Text = "Infinite Scroll Posts";
            this.ResumeLayout(false);
            if (modo.Equals("Oscuro"))
            {
                this.BackColor = Color.FromArgb(40, 40, 40);
                lblNombre.ForeColor = Color.White;
                lblDescripcion.ForeColor = Color.White;
                lblSiguiendo.ForeColor = Color.White;
                pbxBloquear.Image = Frontend.Properties.Resources.salirBlanco;
                pbxReportar.Image = Frontend.Properties.Resources.reportarBlanco;
                pbxChatear.Image = Frontend.Properties.Resources.chat_blanco_removebg_preview;
                panelPosts.BackColor= Color.FromArgb(50, 50, 50);
                lblSeguidores.ForeColor = Color.White;
            }
            if (nombreDeCreador.Equals(user))
            {
                btnSeguir.Visible = false;
                pbxChatear.Visible = false;
                pbxReportar.Image = Frontend.Properties.Resources.editar_removebg_preview;
                if (modo.Equals("Oscuro"))
                {
                    pbxReportar.Image = Frontend.Properties.Resources.editarClaro;
                }
                this.Controls.Remove(pbxBloquear);
            }
            else
            {
                var respuesta = await LoSigue(user, nombreDeCreador, token);
                if (Convert.ToString(respuesta).Equals("seguir"))
                {
                    interaccion = "seguir";
                    lblSiguiendo.Visible = true;
                    btnSeguir.Visible = false;
                }
            }
            if (idioma.Equals("English"))
            {
                btnSeguir.Image = Frontend.Properties.Resources.Follow_removebg_preview;
                lblSiguiendo.Text = "Following";
            }
        }
        public static async Task<dynamic> Interactuar(string user, string aQuienSigue, string tipo, string token)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var datos = new { nombreDeCuenta = user, nombreDeCuenta2=aQuienSigue, tipoInteraccion=tipo, token = token };
                    var content = new StringContent(JsonConvert.SerializeObject(datos), Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PostAsync("https://localhost:44383/user/Interactuar", content);
                    response.EnsureSuccessStatusCode();
                    var responseBody = await response.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject(responseBody);
                    return data;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("ERROR AL LLAMAR A LA API"+ex.Message);
                    return "ERROR";
                }
            }
        }

        public static async Task<dynamic> LoSigue(string user, string aQuienSigue, string token)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var datos = new { nombreDeCuenta = user, nombreDeCuenta2 = aQuienSigue, token = token };
                    var content = new StringContent(JsonConvert.SerializeObject(datos), Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PutAsync("https://localhost:44383/user/ConseguirInteraccion", content);
                    response.EnsureSuccessStatusCode();
                    var responseBody = await response.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject(responseBody);
                    return data;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("ERROR AL LLAMAR A LA API" + ex.Message);
                    return "ERROR";
                }
            }
        }
        public static async Task<dynamic> EliminarInteraccion(string user, string aQuienSigue, string tipoInteraccion, string token)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var datos = new { nombreDeCuenta = user, nombreDeCuenta2 = aQuienSigue, tipoInteraccion= tipoInteraccion, token = token };
                    var content = new StringContent(JsonConvert.SerializeObject(datos), Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PutAsync("https://localhost:44383/user/EliminarInteraccion", content);
                    response.EnsureSuccessStatusCode();
                    var responseBody = await response.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject(responseBody);
                    return data;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("ERROR AL LLAMAR A LA API" + ex.Message);
                    return "ERROR";
                }
            }
        }
        public async Task EnviarNotificacion()
        {
            try
            {
                string texto = $"{user} ha empezado a seguirte";
                MemoryStream ms = new MemoryStream();
                PictureBoxUsuario.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                string b64 = Convert.ToBase64String(ms.ToArray());
                dynamic response = await AgregarNotificaciones(nombreDeCreador, texto, "seguir", b64, token);
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR " + ex.Message);
            }
        }
        public static async Task<dynamic> AgregarNotificaciones(string user, string texto, string tipo, string imagen, string token)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var payload = new
                    {
                        nombreDeCuenta = user,
                        texto = texto,
                        tipo = tipo,
                        imagen = imagen,
                        token = token
                    };

                    // Serializar el objeto a JSON
                    string jsonPayload = JsonConvert.SerializeObject(payload);
                    var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PostAsync("https://localhost:44383/user/agregarNotificaciones", content);
                    response.EnsureSuccessStatusCode();
                    var responseBody = await response.Content.ReadAsStringAsync();
                    dynamic data = JsonConvert.DeserializeObject(responseBody);
                    return data;
                }
                catch (Exception ex)
                {
                    return $"Error al enviar la solicitud: {ex.Message}";
                }
            }
        }
        private async void btnSeguir_Click(object sender, EventArgs e)
        {
            await Interactuar(user, nombreDeCreador, "seguir", token);
            await EnviarNotificacion();
            interaccion = "seguir";
            btnSeguir.Visible = false;
            lblSiguiendo.Visible = true;
        }
        public static async Task<dynamic> PublicarGrupo(string nombreVisible, string configuracion, byte[] imagen, string descripcion, string user, string token)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    if (imagen.Length == 0)
                    {
                        var datos = new { nombreVisible = nombreVisible, configuracion = configuracion, nombreDeCuenta = user, rol= "usuario", token = token, descripcion = descripcion };
                        var content = new StringContent(JsonConvert.SerializeObject(datos), Encoding.UTF8, "application/json");
                        HttpResponseMessage response = await client.PostAsync("https://localhost:44304/RegistrarGrupo", content);
                        response.EnsureSuccessStatusCode();
                        string responseBody = await response.Content.ReadAsStringAsync();
                        dynamic data = JsonConvert.DeserializeObject(responseBody);
                        return data;
                    }
                    else
                    {
                        var datos = new { nombreVisible = nombreVisible, configuracion = configuracion, imagen = Convert.ToBase64String(imagen), nombreDeCuenta = user, rol = "usuario", token = token, descripcion = descripcion };
                        var content = new StringContent(JsonConvert.SerializeObject(datos), Encoding.UTF8, "application/json");
                        HttpResponseMessage response = await client.PostAsync("https://localhost:44304/RegistrarGrupo", content);
                        response.EnsureSuccessStatusCode();
                        string responseBody = await response.Content.ReadAsStringAsync();
                        dynamic data = JsonConvert.DeserializeObject(responseBody);
                        return data;
                    }
                }
                catch (Exception ex)
                {
                    return "Error: " + ex.Message;
                }
            }
        }
        static async Task<dynamic> AñadirUsuarioAlGrupo(string nombreReal, string nombreDeCuenta, string rol, string token)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var datos = new { nombreDeCuenta = nombreDeCuenta, nombreReal = nombreReal, rol = rol, token = token };
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
        static async Task<dynamic> ExisteChatPrivado(string nombreDeCuenta1, string nombreDeCuenta2, string token)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var datos = new { nombreDeCuenta1 = nombreDeCuenta1, nombreDeCuenta2 = nombreDeCuenta2, token = token };
                    var content = new StringContent(JsonConvert.SerializeObject(datos), Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PutAsync($"https://localhost:44304/ExisteChatPrivado", content);
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
        private async void pbxChatear_Click(object sender, EventArgs e)
        {
            if (!user.Equals(nombreDeCreador))
            {
                var bloqueado = await LoSigue(user, nombreDeCreador, token);
                var bloqueadoAlreves = await LoSigue(nombreDeCreador, user, token);
                if (!Convert.ToString(bloqueado).Equals("bloquear") && !Convert.ToString(bloqueadoAlreves).Equals("bloquear"))
                {
                    dynamic existe = await ExisteChatPrivado(user, nombreDeCreador, token);
                    if (Convert.ToString(existe).Equals("false") || Convert.ToString(existe).Equals("False"))
                    {
                        MemoryStream ms = new MemoryStream();
                        PictureBoxUsuario.Image.Save(ms, ImageFormat.Jpeg);
                        byte[] data = ms.ToArray();
                        var respuesta = await PublicarGrupo("-----------------------------------------", "default", data, "", user, token);
                        string[] nombreRealDelGrupo = Convert.ToString(respuesta).Split(' ');
                        var respuesta2 = await AñadirUsuarioAlGrupo(nombreRealDelGrupo[6], nombreDeCreador, "usuario", token);
                        string imagenB64 = await conseguirImagenDelUsuario(nombreDeCreador, token);
                        dynamic existe2 = await ExisteChatPrivado(user, nombreDeCreador, token);
                        var data1 = await BuscarGrupo(Convert.ToString(existe2), token);
                        data1.foto = imagenB64;
                        data1.nombreVisible = nombreDeCreador;
                        AbrirGrupo?.Invoke(this, new PersonalizedArgs(data1, "es chat privado"));
                    }
                    else
                    {
                        string imagenB64 = await conseguirImagenDelUsuario(nombreDeCreador, token);
                        var data2 = await BuscarGrupo(Convert.ToString(existe), token);
                        data2.foto = imagenB64;
                        data2.nombreVisible = nombreDeCreador;
                        AbrirGrupo?.Invoke(this, new PersonalizedArgs(data2, "es chat privado"));
                    }
                }
            }
        }

        private void pbxReportar_Click(object sender, EventArgs e)
        {
            if (nombreDeCreador.Equals(user))
            {
                if (txtNombre.Visible==false)
                {
                    txtDescripcion.Visible = true;
                    txtNombre.Visible = true;
                    pbxImagenEditar.Visible = true;
                    btnConfirmar.Visible = true;
                    PictureBoxUsuario.Visible = false;
                    pbxImagenEditar.Image = PictureBoxUsuario.Image;
                    txtDescripcion.Text = lblDescripcion.Text;
                    txtNombre.Text = lblNombre.Text;

                }
                else
                {
                    txtDescripcion.Visible = false;
                    txtNombre.Visible = false;
                    pbxImagenEditar.Visible = false;
                    btnConfirmar.Visible = false;
                    PictureBoxUsuario.Visible = true;
                }
            }
            else
            {
                ReportarUsuario?.Invoke(this, new PersonalizedArgs(nombreDeCreador));
            }      
        }

        private async void btnConfirmar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtNombre.Text))
            {
                if (idioma.Equals("English"))
                {
                    MessageBox.Show("The name can´t be empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show("El nombre no puede ser vacio.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                lblDescripcion.Text = txtDescripcion.Text;
                lblNombre.Text = txtNombre.Text;
                PictureBoxUsuario.Image = pbxImagenEditar.Image;
                MemoryStream ms = new MemoryStream();
                pbxImagenEditar.Image.Save(ms, ImageFormat.Jpeg);
                byte[] imagen = ms.ToArray();
                await EditarUsuario(user, txtNombre.Text, imagen, txtDescripcion.Text, token);
                txtDescripcion.Visible = false;
                txtNombre.Visible = false;
                pbxImagenEditar.Visible = false;
                PictureBoxUsuario.Visible = true;
                btnConfirmar.Visible = false;
                NuevaImagen?.Invoke(this, new PersonalizedArgs(imagen));
            }
        }

        static async Task<dynamic> EditarUsuario(string creador, string nombreVisible, byte[] imagen, string descripcion, string token)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var dato = new { nombreDeCuenta = creador, nombreVisible=nombreVisible, descripcion=descripcion, foto=Convert.ToBase64String(imagen), token = token };
                    var content = new StringContent(JsonConvert.SerializeObject(dato), Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PutAsync($"https://localhost:44383/user/EditarUsuario", content);
                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();
                    dynamic data = JsonConvert.DeserializeObject(responseBody);
                    return data;
                }
                catch(Exception ex)
                {
                    MessageBox.Show("Error de conexión"+ex.Message);
                    return "error";
                }
            }
        }
        private void pbxImagenEditar_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Archivos de imagen|*.png;*.jpg;*.jpeg"; //Para que sólo aparezcan fotos
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                pbxImagenEditar.ImageLocation = ofd.FileName;
                pbxImagenEditar.SizeMode = PictureBoxSizeMode.StretchImage;
            }
        }

        private async void pbxBloquear_Click(object sender, EventArgs e)
        {
            if (interaccion.Equals("seguir"))
            {
                await EliminarInteraccion(user, nombreDeCreador, "seguir", token); //hacer que cambie el boton
                await Interactuar(user, nombreDeCreador, "bloquear", token);
                btnSeguir.Visible = false;
                pbxChatear.Visible = false;
                interaccion = "bloquear";
            }
            else
            {
                if (interaccion.Equals("bloquear"))
                {
                    await EliminarInteraccion(user, nombreDeCreador, "bloquear", token);
                    btnSeguir.Visible = true;
                    pbxChatear.Visible = true;
                    interaccion = "";
                }
                else
                {
                    await Interactuar(user, nombreDeCreador, "bloquear", token);
                    btnSeguir.Visible = false;
                    pbxChatear.Visible = false;
                    interaccion = "bloquear";
                }
            }
        }

        private async void lblSiguiendo_Click(object sender, EventArgs e)
        {
            await EliminarInteraccion(user, nombreDeCreador, "seguir", token); //hacer que cambie el boton
            interaccion = "";
            btnSeguir.Visible = true;
            lblSiguiendo.Visible = false;
        }
    }
}
