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
        public string usuario;
        public Reportar(string user)
        {
            InitializeComponent();
            usuario = user;
            lblUser.Text = "Usuario reportando = " + usuario;
        }

        private async void btnReportar_Click(object sender, EventArgs e)
        {
            bool userExists = await ExisteUser(txtUsuario.Text);
            Console.WriteLine(userExists);
            if (userExists == true)
            {
                var resultado = await Reporta(usuario,txtUsuario.Text, txtTipo.Text, txtDesc.Text);
                MessageBox.Show(resultado, "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else { MessageBox.Show("Ha ocurrido un error. No existe alguien con ese nombre", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        public static async Task<string> Reporta(string nombreDeCuenta,string cuentaReporteUsuario, string tipo, string descripcion)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var datos = new { nombreDeCuenta = nombreDeCuenta, cuentaReporteUsuario = cuentaReporteUsuario, tipo = tipo, descripcion=descripcion };
                    var content = new StringContent(JsonConvert.SerializeObject(datos), Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PostAsync("https://localhost:44383/user/Reportar", content);
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

        private async Task<bool> ExisteUser(string nombreCuenta)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var datos = new { nombreDeCuenta = nombreCuenta };
                    var content = new StringContent(JsonConvert.SerializeObject(datos), Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PutAsync($"https://localhost:44383/user/ExisteUsuario", content);
                    response.EnsureSuccessStatusCode(); // Lanza una excepción si el código de estado no es exitoso
                    var responseBody = await response.Content.ReadAsStringAsync();
                    dynamic result = JsonConvert.DeserializeObject(responseBody);

                    return result.existe;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al conectarse al servidor: " + ex.Message);
                    return false;
                }
            }
        }
    }
}
