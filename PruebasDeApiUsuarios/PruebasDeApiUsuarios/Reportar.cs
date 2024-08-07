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

namespace PruebasDeApiUsuarios
{
    public partial class Reportar : Form
    {
        public Reportar()
        {
            InitializeComponent();
        }

        private async void btnReportar_Click(object sender, EventArgs e)
        {
            var existe = await ExisteUser(txtUsuario.Text);
            if (existe)
            {
                var resultado = await Reporta(txtUsuario.Text, txtTipo.Text, txtDesc.Text);
                lblResultado.Text = resultado;
            }
        }

        public static async Task<string> Reporta(string usuario, string tipo, string descripcion)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var datos = new { usuario = usuario, tipo = tipo, descripcion=descripcion };
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

        public static async Task<bool> ExisteUser(string nombreCuenta)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync($"https://localhost:44383/user/existeUsuario?nombredecuenta={nombreCuenta}");
                    response.EnsureSuccessStatusCode();
                    var responseBody = await response.Content.ReadAsStringAsync();
                    dynamic data = JsonConvert.DeserializeObject(responseBody);
                    return data;
                }
                catch
                {
                    return false;
                }
            }
        }
    }
}
