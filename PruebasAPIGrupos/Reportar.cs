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

namespace PruebasAPIGrupos
{
    public partial class Reportar : Form
    {
        public Reportar()
        {
            InitializeComponent();
        }

        private async void btnReportar_Click(object sender, EventArgs e)
        {
            var respuesta = await Reporta(txtUsuario.Text, txtTipo.Text, txtDesc.Text);
            if (respuesta.Equals("Reporte correcto"))
                lblResultado.Text = "BIEN";
            {
                var ultimo = await UltimoReporte();
                if (int.Parse(ultimo) > 0)
                {
                    var resultado = await ReportaGrupo(int.Parse(ultimo), txtUsuario.Text, txtGrupo.Text);
                    lblResultado.Text = resultado;
                }
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

        public static async Task<string> ReportaGrupo(int numeroReporte, string usuario, string nombreReal)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var datos = new { numeroReporte = numeroReporte, usuario = usuario, nombreReal = nombreReal };
                    var content = new StringContent(JsonConvert.SerializeObject(datos), Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PostAsync("https://localhost:44304/ReportarGrupo", content);
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
