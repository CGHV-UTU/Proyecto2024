using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Frontend
{
    public partial class Notificaciones : Form
    {
        private string user;
        private string token;
        public event EventHandler NuevasNotificaciones;
        public Notificaciones(string usuario, string token)
        {
            InitializeComponent();
            this.user = usuario;
            this.token = token;
            Iniciar();
            notificaciones(); // Carga todas las notificaciones
        }
        private int cantNotificaciones=0;
        public async void notificaciones()
        {
            // Obtener las notificaciones desde la API o la fuente de datos
            while (true)
            {
                var notificaciones = await conseguirNotificaciones(user, token);
                if (!Convert.ToString(notificaciones).Equals($"No se encontraron notificaciones para el usuario: {user}"))
                {
                    PanelNotificaciones.Controls.Clear();
                    foreach (var notificacion in notificaciones)
                    {
                        // Crear el control de notificación o contenido y añadirlo al panel contenedor
                        var notiControl = new NotificacionControl(notificacion);
                        if (PanelNotificaciones.Controls.Count == 0)
                        {
                            notiControl.Location = new Point(0, 0);
                        }
                        else
                        {
                            var lastControl = PanelNotificaciones.Controls[PanelNotificaciones.Controls.Count - 1];
                            notiControl.Location = new Point(0, lastControl.Bottom);
                        }
                        this.PanelNotificaciones.Controls.Add(notiControl);
                    }
                    if (cantNotificaciones == 0)
                    {
                        cantNotificaciones = this.PanelNotificaciones.Controls.Count;
                    }
                    else
                    {
                        if (cantNotificaciones < this.PanelNotificaciones.Controls.Count)
                        {
                            NuevasNotificaciones?.Invoke(this, EventArgs.Empty);
                        }
                    }
                }
                else
                {
                    cantNotificaciones = 0;
                }
            }
        }

        public void setearCantidadDeNotificaciones()
        {
            cantNotificaciones = this.PanelNotificaciones.Controls.Count;
        }
        public static async Task<dynamic> conseguirNotificaciones(string usuario, string token)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    await Task.Delay(10000);
                    var datos = new { nombreDeCuenta = usuario, token=token };
                    var content = new StringContent(JsonConvert.SerializeObject(datos), Encoding.UTF8, "application/json");
                    var response = await client.PostAsync("https://localhost:44383/user/ConseguirNotificaciones", content);
                    var responseBody = await response.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject(responseBody);
                    return data; // Devuelve la cadena completa de notificaciones
                }
            }
            catch
            {
                return "fallido"; // Retorna un valor de error si ocurre una excepción
            }
        }
        private void Iniciar()
        {
            this.SuspendLayout();

            // PanelNotificaciones
            this.PanelNotificaciones.HorizontalScroll.Enabled = false;
            this.PanelNotificaciones.HorizontalScroll.Visible = false;
            this.PanelNotificaciones.AutoScroll = true;

            // Notificaciones Form
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Name = "Notificaciones";
            this.Text = "Notificaciones";
            this.ResumeLayout(false);
        }
    }
}
