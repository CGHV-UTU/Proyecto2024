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

namespace APIpostYeventos
{
    public partial class ReportarPost_Comentario : Form
    {
        public ReportarPost_Comentario()
        {
            InitializeComponent();
        }

        private void cbxReporte_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxReporte.SelectedItem.Equals("Post")) 
            {
                lblID.Text = "Id del post";
            }
            else
            {
                lblID.Text = "Id del comentario";
            }
        }

        private async void btnEnviar_Click(object sender, EventArgs e)
        {
            var respuesta = await Reporta(txtUsuario.Text,txtTipo.Text,txtDesc.Text);
            if (respuesta.Equals("Reporte correcto"))
            {
                var ultimo = await UltimoReporte();
                lblResultado.Text = "" + ultimo;
                if (int.Parse(ultimo) > 0)
                {
                    lblResultado.Text = "" + ultimo;
                    if (cbxReporte.SelectedItem.Equals("Post"))
                    {
                        lblResultado.Text = "LLega";
                        var resultado = await ReportaPost(int.Parse(ultimo), txtUsuario.Text, int.Parse(txtID.Text));
                        lblResultado.Text = resultado;
                    }
                    else
                    {
                        lblResultado.Text = "LLega";
                        var resultado = await ReportaComentario(int.Parse(ultimo), txtUsuario.Text, int.Parse(txtID.Text));
                        lblResultado.Text = resultado;
                    }
                }        
            }
            else
            {
                lblResultado.Text = "Reporte incorrecto";
            }
        }

        public static async Task<string> Reporta(string usuario, string tipo, string descripcion)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var datos = new { usuario = usuario, tipo = tipo, descripcion = descripcion };
                    var content = new StringContent(JsonConvert.SerializeObject(datos), Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PostAsync("https://localhost:44383/user/ReportarUsuario", content);
                    response.EnsureSuccessStatusCode();
                    var responseBody = await response.Content.ReadAsStringAsync();
                    dynamic data = JsonConvert.DeserializeObject(responseBody);
                    return data;
                }
                catch
                {
                    return "Reporte fallido";
                }
            }
        }

        public static async Task<string> UltimoReporte()
        {
            using (HttpClient client = new HttpClient())
            {
                
                    HttpResponseMessage response = await client.GetAsync("https://localhost:44383/user/UltimoReporte");
                    response.EnsureSuccessStatusCode();
                    var responseBody = await response.Content.ReadAsStringAsync();
                    dynamic data = JsonConvert.DeserializeObject(responseBody);
                    return data;
                
            }
        }

        public static async Task<string> ReportaPost(int numeroReporte,string usuario, int id)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var datos = new { numeroReporte = numeroReporte, usuario = usuario, id = id };
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
        public static async Task<string> ReportaComentario(int numeroReporte, string usuario, int id)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var datos = new { numeroReporte = numeroReporte, usuario = usuario, id = id };
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
    }
}
