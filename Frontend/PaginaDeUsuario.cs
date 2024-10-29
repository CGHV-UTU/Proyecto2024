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
        public event EventHandler<PersonalizedArgs> AbrirComentarios;
        public event EventHandler<PersonalizedArgs> ReportarPost;
        public PaginaDeUsuario(string nombreCreador, string modo, string user, string token)
        {
            this.nombreDeCreador = nombreCreador;
            this.modo = modo;
            this.user = user;
            this.token = token;
            InitializeComponent();
            Iniciar();
            LoadPosts();
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
                    return new string[] {data.nombreVisible, data.descripcion, data.foto };
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
            DataTable posts = await ConseguirPosts(nombreDeCreador, token);
            if (posts != null)
            {
                for (int i = posts.Rows.Count-1; i >= 0 ; i--)
                {
                    int idpost = Convert.ToInt32(posts.Rows[i]["idPost"]);
                    var postControl = new PostControl(idpost, modo, user, token);
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
                this.lblNombre.Text = nombreDeCreador;
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
        }
        public static async Task Seguir(string user, string aQuienSigue, string tipo, string token)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var datos = new { nombreDeCuenta = user, nombreDeCuenta2=aQuienSigue, tipoInteraccion=tipo, token = token };
                    var content = new StringContent(JsonConvert.SerializeObject(datos), Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PostAsync("https://localhost:44383/user/Interactuar", content);
                    response.EnsureSuccessStatusCode();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("ERROR AL LLAMAR A LA API"+ex.Message);
                }
            }
        }
        private async void btnSeguir_Click(object sender, EventArgs e)
        {
            await Seguir(user, nombreDeCreador, "seguir", token);
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
        private async void pbxChatear_Click(object sender, EventArgs e)
        {
            MemoryStream ms = new MemoryStream();
            PictureBoxUsuario.Image.Save(ms, ImageFormat.Jpeg);
            byte[] data = ms.ToArray();
            var respuesta = await PublicarGrupo("-----------------------------------------","default",data,"",user,token);
            string[] nombreRealDelGrupo=Convert.ToString(respuesta).Split(' ');
            var respuesta2=await AñadirUsuarioAlGrupo(nombreRealDelGrupo[6], nombreDeCreador, "usuario", token);
            MessageBox.Show("" + respuesta2);
        }
    }
}
