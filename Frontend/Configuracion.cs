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
        private string token;
        private string idioma;
        public Configuracion(string user, string token,string modo, string idioma)
        {
            InitializeComponent();
            usuario = user;
            this.token = token;
            this.idioma = idioma;
            if (!modo.Equals("Oscuro"))
            {
                this.BackColor = Color.LightGray;
            }
            else
            {
                this.BackColor = Color.FromArgb(40, 40, 40);
                label1.ForeColor = Color.White;
                label2.ForeColor = Color.White;
                cbxIdioma.ForeColor = Color.White;
                cbxModo.ForeColor = Color.White;
                cbxIdioma.BackColor= Color.FromArgb(50, 50, 50);
                cbxModo.BackColor= Color.FromArgb(50, 50, 50);
            }
            if (idioma.Equals("English"))
            {
                label1.Text = "Mode";
                label2.Text = "Language";
                List<string> opciones = new List<string>
                {
                    "Bright",
                    "Dark"
                };
                cbxModo.Items.Clear();
                cbxModo.Items.AddRange(opciones.ToArray());
                btnCambiar.Text = "Change Configuration";
            }
        }
        public event EventHandler<ConfiguraEventArgs> CambiarModo;
        
        private async void btnCambiar_Click(object sender, EventArgs e)
        {
            if (idioma.Equals("English"))
            {
                string modoEspañol="Claro";
                string modo = Convert.ToString(cbxModo.SelectedItem);
                switch (modo)
                {
                    case "Bright":
                        modoEspañol = "Claro";
                        break;
                    case "Dark":
                        modoEspañol = "Oscuro";
                        break;
                }
                var resultado = await CambiarConfig(modoEspañol, Convert.ToString(cbxIdioma.SelectedItem), token);
                if (resultado.Equals("Configuracion correcta"))
                {
                    MessageBox.Show("Configuration changed succesfully, leaving the program", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CambiarModo?.Invoke(this, new ConfiguraEventArgs(Convert.ToString(cbxModo.SelectedItem), Convert.ToString(cbxIdioma.SelectedItem)));
                    Application.Exit();
                }
                else
                {
                    MessageBox.Show("Backend ERROR", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                var resultado = await CambiarConfig(Convert.ToString(cbxModo.SelectedItem), Convert.ToString(cbxIdioma.SelectedItem), token);
                if (resultado.Equals("Configuracion correcta"))
                {
                    MessageBox.Show("Configuración modificada con éxito, saliendo del programa", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CambiarModo?.Invoke(this, new ConfiguraEventArgs(Convert.ToString(cbxModo.SelectedItem), Convert.ToString(cbxIdioma.SelectedItem)));
                    Application.Exit();
                }
                else
                {
                    MessageBox.Show("Hubo un error en backend", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        public static async Task<string> CambiarConfig(string modo, string idioma, string token)
        {
            try
            {
                string config = $"{modo};{idioma}";
                using (HttpClient client = new HttpClient())
                {
                    var datos = new { nombreDeCuenta = usuario, configuraciones = config, token=token };
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
