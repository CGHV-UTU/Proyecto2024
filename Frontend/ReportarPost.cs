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
        public ReportarPost(string idpost)
        {
            this.idpost = idpost;
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

        private async void pictureBox1_Click(object sender, EventArgs e)
        {
            string creadorPost = await conseguirCreador(int.Parse(idpost));
            await ReportaPost(creadorPost, int.Parse(idpost), cbxRazon.SelectedItem.ToString(), txtDescripcion.Text);
        }
    }
}
