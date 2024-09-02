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
    public partial class CrearComentario : Form
    {
        private string idpost;
        private string user;
        public CrearComentario(string nombreCuenta, string idpost)
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
                    var datos = new { NombreDeCuenta = NombreDeCuenta, IdPost = IdPost, NombreCreador=nombreCreador, texto = texto, fechayhora = fechayhora };
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

        static async Task<dynamic> conseguirCreador(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync($"https://localhost:44340/conseguirCreador?id={id}");
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
            var creadorPost = await conseguirCreador(int.Parse(idpost));
            await Publicar(user,idpost,creadorPost,txtBox.Text,fechayhora);
        }
    }
}
