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
    public partial class CreadorComentarios : UserControl
    {
        private string idpost;
        private string user;
        public CreadorComentarios(string nombreCuenta, string idpost)
        {
            this.idpost = idpost;
            user = nombreCuenta;
            InitializeComponent();
        }
        static async Task Publicar(string NombreDeCuenta, string IdPost, string nombreCreador, string texto, string fechayhora)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var datos = new { NombreDeCuenta = NombreDeCuenta, IdPost = IdPost, NombreCreador = nombreCreador, texto = texto, fechayhora = fechayhora };
                    var content = new StringContent(JsonConvert.SerializeObject(datos), Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PostAsync("https://localhost:44340/hacerComentario", content);
                    response.EnsureSuccessStatusCode();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                    Console.ReadLine();
                }
            }
        }

        public static async Task<string> obtenerCreador(int idpost)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var dato = new { id = idpost };
                    var content = new StringContent(JsonConvert.SerializeObject(dato), Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PutAsync("https://localhost:44340/conseguirCreador", content);
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

        private async void pictureBox1_Click(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;
            string fechayhora = now.ToString("yyyy-MM-dd HH:mm:ss");
            var creadorPost = await obtenerCreador(int.Parse(idpost));
            await Publicar(user, idpost, creadorPost, textBox1.Text, fechayhora);
        }
    }
}
