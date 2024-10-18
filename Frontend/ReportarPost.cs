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
        private string usuario;
        private string token;
        public ReportarPost(string idpost, string user, string token, string idcomentario="")
        {
            this.idpost = idpost;
            this.idcomentario = idcomentario;
            this.usuario = user;
            this.token = token;
            InitializeComponent();
            this.BackColor = Color.LightGray;
        }

        public static async Task<string> obtenerCreador(int id, string token)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var dato = new { id = id , token=token};
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

        public static async Task<string> obtenerCreadorComentario(int id, string token)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var dato = new { id = id, token=token };
                    var content = new StringContent(JsonConvert.SerializeObject(dato), Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PutAsync("https://localhost:44340/conseguirCreadorComentario", content);
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

        public static async Task<string> ReportaPost(string usuario, string creadorDelPost, int id, string tipo, string descripcion, string token)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var datos = new { nombreDeCuenta = usuario, creadorDelPost = creadorDelPost, idPost = id , tipo=tipo, descripcion=descripcion, token=token};
                    var content = new StringContent(JsonConvert.SerializeObject(datos), Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PostAsync("https://localhost:44383/user/Reportar", content);
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

        public static async Task<string> ReportaComentario(string usuario, string creadorDelComentario, string id, string tipo, string descripcion, string token)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var datos = new { nombreDeCuenta = usuario, creadorDelComentario = creadorDelComentario, idComentario = id, tipo = tipo, descripcion = descripcion, token=token };
                    var content = new StringContent(JsonConvert.SerializeObject(datos), Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PostAsync("https://localhost:44383/user/Reportar", content);
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
                string creadorPost = await obtenerCreador(int.Parse(idpost), token);
                if (!string.IsNullOrEmpty(cbxRazon.Text))
                {
                    var respuesta = await ReportaPost(usuario, creadorPost, int.Parse(idpost), cbxRazon.SelectedItem.ToString(), txtDescripcion.Text, token);
                    MessageBox.Show(respuesta);
                }
                else
                {
                    MessageBox.Show("No puede realizar un reporte sin razón", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(cbxRazon.Text))
                {
                    string creadorComentario = await obtenerCreadorComentario(int.Parse(idcomentario), token);
                    var respuesta = await ReportaComentario(usuario, creadorComentario, idcomentario, cbxRazon.SelectedItem.ToString(), txtDescripcion.Text, token);
                    MessageBox.Show(respuesta);
                }
                else
                {
                    MessageBox.Show("No puede realizar un reporte sin razón", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                }
        }
    }
}
