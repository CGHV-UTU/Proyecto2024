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
        public event EventHandler<PersonalizedArgs> AbrirEvento;
        public event EventHandler<PersonalizedArgs> AbrirGrupo;
        public event EventHandler<PersonalizedArgs> AbrirUsuario;
        public Grupo_EventoParaListar(string usuario, string token, string nombreRealGrupo = "", int idEvento = 0, dynamic usuariobuscar = null, bool busqueda = false)
        {
            this.nombreReal = nombreRealGrupo;
            this.idevento = idEvento;
            this.token = token;
            this.user = usuario;
            this.datosDelUsuario = usuariobuscar;
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

        static async Task<dynamic> UnirseAlGrupo(string nombreReal,string nombre, string token)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var datos = new { nombreDeCuenta = nombre, nombreReal = nombreReal, token = token };
                    var content = new StringContent(JsonConvert.SerializeObject(datos), Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PutAsync($"https://localhost:44304/EnviarSolicitudParaUnirseAlGrupo", content);
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
                    datos = data;
                }
            }
            else
            {
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

            if (this.busqueda && !this.nombreReal.Equals(""))
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
                this.Controls.Add(this.pbxUnirse);
            }
        }

        private void Grupo_EventoParaListar_Click(object sender, EventArgs e)
        {
            if (!busqueda)
            {
                if (idevento > 0)
                {
                    AbrirEvento?.Invoke(this, new PersonalizedArgs(datos));
                }
                else
                {
                    AbrirGrupo?.Invoke(this, new PersonalizedArgs(datos));
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

        private async void pbxUnirse_Click(object sender, EventArgs e)
        {
            dynamic respuesta = await UnirseAlGrupo(nombreReal, user, token);
        }
    }
}
