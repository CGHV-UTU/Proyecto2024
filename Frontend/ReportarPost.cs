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
    public partial class ReportarPost : Form
    {
        private string idpost;
        private string idcomentario;
        public ReportarPost(string idpost, string idcomentario="")
        {
            this.idpost = idpost;
            this.idcomentario = idcomentario;
            InitializeComponent();
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

        static async Task<dynamic> conseguirCreadorComentario(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync($"https://localhost:44340/conseguirCreadorComentario?id={id}");
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

        public static async Task<string> ReportaPost(string usuario, int id, string tipo, string descripcion)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var datos = new { usuario = usuario, id = id , tipo=tipo, descripcion=descripcion};
                    var content = new StringContent(JsonConvert.SerializeObject(datos), Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PostAsync("https://localhost:44340/ReportarPost", content);
                    response.EnsureSuccessStatusCode();
                    var responseBody = await response.Content.ReadAsStringAsync();
                    dynamic data = JsonConvert.DeserializeObject(responseBody);
                    return data;
                }
                catch
                {
                    return "Falla";
                }
            }
        }

        public static async Task<string> ReportaComentario(string usuario, int id, string tipo, string descripcion)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var datos = new { usuario = usuario, id = id, tipo = tipo, descripcion = descripcion };
                    var content = new StringContent(JsonConvert.SerializeObject(datos), Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PostAsync("https://localhost:44340/ReportarComentario", content);
                    response.EnsureSuccessStatusCode();
                    var responseBody = await response.Content.ReadAsStringAsync();
                    dynamic data = JsonConvert.DeserializeObject(responseBody);
                    return data;
                }
                catch
                {
                    return "Falla";
                }
            }
        }

        private async void pictureBox1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(idcomentario))
            {
                string creadorPost = await conseguirCreador(int.Parse(idpost));
                var respuesta = await ReportaPost(creadorPost, int.Parse(idpost), cbxRazon.SelectedItem.ToString(), txtDescripcion.Text);
                MessageBox.Show(respuesta);
            }
            else
            {
                MessageBox.Show(idcomentario);
                string creadorComentario = await conseguirCreadorComentario(int.Parse(idcomentario));
                MessageBox.Show(creadorComentario);
                var respuesta = await ReportaComentario(creadorComentario, int.Parse(idcomentario), cbxRazon.SelectedItem.ToString(), txtDescripcion.Text);
                MessageBox.Show(respuesta);
            }
        }
    }
}
