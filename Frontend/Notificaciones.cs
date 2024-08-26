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
        public Notificaciones(string usuario)
        {
            InitializeComponent();
            this.user = usuario;
            
        }

        public async void notificaciones()
        {
            string notificaciones = await conseguirNotificaciones(user);
            string[] notisarray = notificaciones.Split(';');
            int i=notisarray.Length;
            for(int x=0; x<i; x++)
            {
                var noti = new NotificacionControl(notisarray[x]);
                noti.Location = new Point(0, (x * noti.Height));
                panel1.Controls.Add(noti);
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
                    dynamic data = JsonConvert.DeserializeObject(responseBody);
                    return data;
                }
            }
            catch
            {
                return "fallido";
            }
        }
    }
}
