using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace APIpostYeventos
{
    public partial class AñadirComentario : Form
    {
        private string usuario;
        public AñadirComentario(string user)
        {
            InitializeComponent();
            usuario = user;
        }
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Form1 f1 = new Form1(usuario);
            f1.Show();
            this.Close();
        }
        // Buscar Post
        private async void btnBuscar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtID.Text) || !int.TryParse(txtID.Text, out int numero))
            {
                lblErrorID.Show();
                lblErrorID.Text = "Debe ingresar una ID válida";
            }
            else
            {
                var respuesta = await Existe(int.Parse(txtID.Text));
                if (respuesta)
                {
                    lblID.Hide();
                    lblErrorID.Hide();
                    txtID.Hide();
                    btnBuscar.Hide();
                    btnVolver.Hide();
                    btnCancelar.Show();
                    txtComentario.Show();
                    btnPublicar.Show();
                    lblComentario.Show();
                }
                else
                {
                    lblErrorID.Show();
                    lblErrorID.Text = "No se encontró la ID";
                }
            }
        }
        static async Task<bool> Existe(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var datos = new {id = id};
                    var content = new StringContent(JsonConvert.SerializeObject(datos), Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PutAsync($"https://localhost:44340/existePost", content);
                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();
                    dynamic data = JsonConvert.DeserializeObject<bool>(responseBody); 
                    return data;
                }
                catch
                {
                    return false;
                }
            }
        }
        //Salir
        private void btnVolver_Click(object sender, EventArgs e)
        {
            Form1 f1 = new Form1(usuario);
            f1.Show();
            this.Close();
        }
        //Publicar Comentario
        private async void btnPublicar_Click(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;
            string fechayhora = now.ToString("yyyy-MM-dd HH:mm:ss");
            int nombrecreador;
            int.TryParse(txtID.Text, out nombrecreador);
            string nombreCreador = await ConseguirCreador(nombrecreador);

            if (nombreCreador != null)
            {
                await Publicar(usuario, txtID.Text, nombreCreador, txtComentario.Text, fechayhora);
                MessageBox.Show("El comentario se creó correctamente");
            }
            else
            {
                MessageBox.Show("Error al obtener el creador");
            }
            lblError.Show();
        }
        static async Task Publicar(string NombreDeCuenta, string IdPost, string nombreCreador, string texto, string fechayhora)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {

                    var datos = new { NombreDeCuenta = NombreDeCuenta, IdPost = IdPost, NombreCreador = nombreCreador, texto = texto, fechayhora = fechayhora };
                    var content = new StringContent(JsonConvert.SerializeObject(datos), Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PostAsync("https://localhost:44340/hacerComentario", content);
                    response.EnsureSuccessStatusCode();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }
        static async Task<string> ConseguirCreador(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var datos = new { id = id };
                    var content = new StringContent(JsonConvert.SerializeObject(datos), Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PutAsync($"https://localhost:44340/conseguirCreador", content);
                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();
                    return responseBody.Trim('"'); 
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                    return null;
                }
            }
        }
    }
}
