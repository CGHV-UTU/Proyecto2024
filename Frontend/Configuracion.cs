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
    public partial class Configuracion : Form
    {
        public static string usuario;
        public Configuracion(string user)
        {
            InitializeComponent();
            usuario = user;
            this.BackColor = Color.LightGray;
        }
        public event EventHandler<ConfiguraEventArgs> CambiarModo;
        
        private async void btnCambiar_Click(object sender, EventArgs e)
        {
            var resultado = await CambiarConfig(Convert.ToString(cbxModo.SelectedItem), Convert.ToString(cbxIdioma.SelectedItem));
            if (resultado.Equals("Configuracion correcta"))
            {
                MessageBox.Show("Configuración modificada con éxito", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CambiarModo?.Invoke(this, new ConfiguraEventArgs(Convert.ToString(cbxModo.SelectedItem), Convert.ToString(cbxIdioma.SelectedItem)));
            }
            else
            {
                MessageBox.Show("Hubo un error en backend", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static async Task<string> CambiarConfig(string modo, string idioma)
        {
            try
            {
                string config = $"{modo};{idioma}";
                using (HttpClient client = new HttpClient())
                {
                    var datos = new { nombreDeCuenta = usuario, configuraciones = config };
                    var content = new StringContent(JsonConvert.SerializeObject(datos), Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PostAsync("https://localhost:44383/user/CambiarConfiguracion", content);
                    response.EnsureSuccessStatusCode();
                    var responseBody = await response.Content.ReadAsStringAsync();
                    dynamic data = JsonConvert.DeserializeObject(responseBody);
                    return data;
                }
            }
            catch
            {
                return "fallo";
            }  
        }
    }
}
