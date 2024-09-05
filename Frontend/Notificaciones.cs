﻿using Newtonsoft.Json;
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
        private int currentPage = 0;
        private const int notificationsPerPage = 5; // Número de notificaciones por página
        private const int margin = 10; // margen para las notificaciones

        public Notificaciones(string usuario)
        {
            InitializeComponent();
            this.user = usuario;
            Iniciar();
            notificaciones(); // Carga todas las notificaciones
        }

        public async void notificaciones()
        {
            // Obtener las notificaciones desde la API o la fuente de datos
            string notificaciones = await conseguirNotificaciones(user);
            string[] notisarray = notificaciones.Split(';');
            int i = notisarray.Length;

            // Ajuste de la posición vertical inicial
            int currentY = 10; // Comenzar a 10 píxeles desde la parte superior

            for (int x = 0; x < i; x++)
            {
                // Crear un panel contenedor para cada notificación
                Panel panelContenedor = new Panel();
                panelContenedor.Size = new Size(PanelNotificaciones.Width - 20, 100); // Ancho del panel de notificación menos márgenes
                panelContenedor.Location = new Point(10, currentY); // Ajusta la posición para cada panel
                panelContenedor.BorderStyle = BorderStyle.FixedSingle; // Borde para diferenciar
                panelContenedor.BackColor = Color.White; // Fondo blanco para contraste

                // Crear el control de notificación o contenido y añadirlo al panel contenedor
                var notiControl = new NotificacionControl(notisarray[x]);
                notiControl.Dock = DockStyle.Fill; // Llenar el panel contenedor
                panelContenedor.Controls.Add(notiControl);

                // Añadir el panel contenedor al PanelNotificaciones
                PanelNotificaciones.Controls.Add(panelContenedor);

                // Incrementar la posición Y para el siguiente panel
                currentY += panelContenedor.Height + 10; // Dejar un margen de 10 píxeles entre paneles
            }
        }

        public static async Task<string> conseguirNotificaciones(string usuario)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var datos = new { nombreDeCuenta = usuario };
                    var content = new StringContent(JsonConvert.SerializeObject(datos), Encoding.UTF8, "application/json");
                    var response = await client.PostAsync("https://localhost:44383/user/ConseguirNotificaciones", content);
                    var responseBody = await response.Content.ReadAsStringAsync();
                    return responseBody; // Devuelve la cadena completa de notificaciones
                }
            }
            catch
            {
                return "fallido"; // Retorna un valor de error si ocurre una excepción
            }
        }

        private void Iniciar()
        {
            this.PanelNotificaciones = new Panel();
            this.SuspendLayout();

            // PanelNotificaciones
            this.PanelNotificaciones.HorizontalScroll.Enabled = false;
            this.PanelNotificaciones.HorizontalScroll.Visible = false;
            this.PanelNotificaciones.AutoScroll = true;
            this.PanelNotificaciones.Dock = DockStyle.Fill;
            this.PanelNotificaciones.Location = new System.Drawing.Point(0, 0);
            this.PanelNotificaciones.Name = "PanelNotificaciones";
            this.PanelNotificaciones.Size = new System.Drawing.Size(800, 450);
            this.PanelNotificaciones.TabIndex = 0;

            this.Controls.Add(this.PanelNotificaciones);

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