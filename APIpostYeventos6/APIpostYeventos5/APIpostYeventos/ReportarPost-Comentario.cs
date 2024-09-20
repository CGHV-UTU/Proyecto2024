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
        private static string user;
        public ReportarPost_Comentario(string Usuario)
        {
            InitializeComponent();
            user = Usuario;
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
            if (cbxReporte.SelectedItem.Equals("Post"))
            {
                var resultado = await ReportaPost(txtUsuario.Text, int.Parse(txtID.Text), cbxTipo.Text, txtDesc.Text);
                lblResultado.Text = resultado;
            }
            else
            {
                var resultado = await ReportaComentario(txtUsuario.Text, int.Parse(txtID.Text), cbxTipo.Text, txtDesc.Text);
                lblResultado.Text = resultado;
            }
        }
    
        //Reportes
        public static async Task<string> ReportaPost(string usuario, int id, string tipo, string descripcion)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var datos = new {nombreDeCuenta = user, idPost = id, creadorDelPost = usuario, tipo = tipo, descripcion = descripcion};
                    var content = new StringContent(JsonConvert.SerializeObject(datos), Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PostAsync("https://localhost:44383/user/Reportar", content); //Conecta con API usuarios
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
                    var datos = new { nombreDeCuenta = user, idComentario = id, creadorDelComentario = usuario, tipo = tipo, descripcion = descripcion };
                    var content = new StringContent(JsonConvert.SerializeObject(datos), Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PostAsync("https://localhost:44383/Reportar", content); //Conecta con API usuarios
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
