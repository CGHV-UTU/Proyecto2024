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
        private string idEvento;
        private string token;
        private string nombreReal;
        private string usuarioAReportar;
        private string modo;
        private string idioma;
        public event EventHandler CerrarVentana;
        public ReportarPost(string idpost, string user, string token,string modo, string idcomentario="", string idEvento="", string nombreRealGrupo="", string usuarioAReportar="", string idioma="Español")
        {
            this.idpost = idpost;
            this.idcomentario = idcomentario;
            this.usuario = user;
            this.token = token;
            this.idEvento = idEvento;
            this.nombreReal = nombreRealGrupo;
            this.usuarioAReportar = usuarioAReportar;
            this.modo = modo;
            this.idioma = idioma;
            InitializeComponent();
            this.BackColor = Color.LightGray;
            if (modo.Equals("Oscuro"))
            {
                lblRazon.ForeColor = Color.White;
                lblDescripcion.ForeColor = Color.White;
                txtDescripcion.ForeColor = Color.White;
                txtDescripcion.BackColor = Color.FromArgb(50, 50, 50);
                cbxRazon.ForeColor = Color.White;
                cbxRazon.BackColor = Color.FromArgb(50, 50, 50);
                this.BackColor = Color.FromArgb(40, 40, 40);
            }
            if (idioma.Equals("English"))
            {
                lblRazon.Text = "Reason";
                lblDescripcion.Text = "Description";
                pictureBox1.Image = Frontend.Properties.Resources.report;
                List<string> opciones = new List<string>
                {
                    "Sexual",
                    "Violent or repugnant",
                    "Abusive",
                    "Harassment or bullying",
                    "Harmful or dangerous activities",
                    "Misinformation",
                    "Child abuse",
                    "Terrorism",
                    "Fraud",
                    "Legal issue",
                    "Other"
                };
                cbxRazon.Items.Clear();
                cbxRazon.Items.AddRange(opciones.ToArray());
            }
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
        public static async Task<string> ReportaEvento(string usuario, string id, string tipo, string descripcion, string token)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var datos = new { nombreDeCuenta = usuario, idEvento=id, tipo = tipo, descripcion = descripcion, token = token };
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

        public static async Task<string> ReportaGrupo(string usuario, string id, string tipo, string descripcion, string token)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var datos = new { nombreDeCuenta = usuario, nombreGrupo = id, tipo = tipo, descripcion = descripcion, token = token };
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
        public static async Task<string> ReportaUsuario(string usuario, string usuarioAReportar, string tipo, string descripcion, string token)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var datos = new { nombreDeCuenta = usuario, cuentaReporteUsuario= usuarioAReportar, tipo = tipo, descripcion = descripcion, token = token };
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
            if (!string.IsNullOrEmpty(idpost) && string.IsNullOrEmpty(idcomentario))
            {
                string creadorPost = await obtenerCreador(int.Parse(idpost), token);
                if (!string.IsNullOrEmpty(cbxRazon.Text))
                {
                    if (idioma.Equals("English"))
                    {
                        string razon = cbxRazon.SelectedItem.ToString();
                        string razonEspañol="";
                        switch (razon)
                        {
                            case "Sexual":
                                razonEspañol = "Sexual";
                                break;
                            case "Violent or repugnant":
                                razonEspañol = "Violento o repugnante";
                                break;
                            case "Abusive":
                                razonEspañol = "Vejatorio";
                                break;
                            case "Harassment or bullying":
                                razonEspañol = "Hostigamiento o acoso";
                                break;
                            case "Harmful or dangerous activities":
                                razonEspañol = "Actividades dañinas o peligrosas";
                                break;
                            case "Misinformation":
                                razonEspañol = "Desinformacion";
                                break;
                            case "Child abuse":
                                razonEspañol = "Maltrato infantil";
                                break;
                            case "Terrorism":
                                razonEspañol = "Terrorismo";
                                break;
                            case "Fraud":
                                razonEspañol = "Fraude";
                                break;
                            case "Legal issue":
                                razonEspañol = "Problema legal";
                                break;
                            case "Other":
                                razonEspañol = "Otros";
                                break;
                        }
                        var respuesta = await ReportaPost(usuario, creadorPost, int.Parse(idpost), razonEspañol, txtDescripcion.Text, token);
                        MessageBox.Show("Correct", "Correct", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        var respuesta = await ReportaPost(usuario, creadorPost, int.Parse(idpost), cbxRazon.SelectedItem.ToString(), txtDescripcion.Text, token);
                        MessageBox.Show("Reporte correcto", "Correcto", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    if (idioma.Equals("English"))
                    {
                        MessageBox.Show("A report cant be made without a reason", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        MessageBox.Show("No puede realizar un reporte sin razón", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            if (!string.IsNullOrEmpty(idEvento) && !string.IsNullOrEmpty(idcomentario))
            {
                if (!string.IsNullOrEmpty(cbxRazon.Text))
                {
                    string creadorComentario = await obtenerCreadorComentario(int.Parse(idcomentario), token);
                    var respuesta = await ReportaComentario(usuario, creadorComentario, idcomentario, cbxRazon.SelectedItem.ToString(), txtDescripcion.Text, token);
                    MessageBox.Show(respuesta);
                }
                else
                {
                    if (idioma.Equals("English"))
                    {
                        MessageBox.Show("A report cant be made without a reason", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        MessageBox.Show("No puede realizar un reporte sin razón", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            if (!string.IsNullOrEmpty(idEvento))
            {
                if (!string.IsNullOrEmpty(cbxRazon.Text))
                {
                    var respuesta = await ReportaEvento(usuario, idEvento, cbxRazon.SelectedItem.ToString(), txtDescripcion.Text, token);
                    if (idioma.Equals("English"))
                    {
                        MessageBox.Show("Report successfull", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Reporte realizado con éxito", "Éxiot", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    if (idioma.Equals("English"))
                    {
                        MessageBox.Show("A report cant be made without a reason", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        MessageBox.Show("No puede realizar un reporte sin razón", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            if (!string.IsNullOrEmpty(nombreReal))
            {
                if (!string.IsNullOrEmpty(cbxRazon.Text))
                {
                    var respuesta = await ReportaGrupo(usuario, nombreReal, cbxRazon.SelectedItem.ToString(), txtDescripcion.Text, token);
                    if (idioma.Equals("English"))
                    {
                        MessageBox.Show("Report successfull", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Reporte realizado con éxito", "Éxiot", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    if (idioma.Equals("English"))
                    {
                        MessageBox.Show("A report cant be made without a reason", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        MessageBox.Show("No puede realizar un reporte sin razón", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            if (!string.IsNullOrEmpty(usuarioAReportar))
            {
                if (!string.IsNullOrEmpty(cbxRazon.Text))
                {
                    var respuesta = await ReportaUsuario(usuario, usuarioAReportar, cbxRazon.SelectedItem.ToString(), txtDescripcion.Text, token);
                    if (idioma.Equals("English"))
                    {
                        MessageBox.Show("Report successfull", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Reporte realizado con éxito", "Éxiot", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    if (idioma.Equals("English"))
                    {
                        MessageBox.Show("A report cant be made without a reason", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        MessageBox.Show("No puede realizar un reporte sin razón", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            CerrarVentana?.Invoke(this, EventArgs.Empty);
        }
    }
}
