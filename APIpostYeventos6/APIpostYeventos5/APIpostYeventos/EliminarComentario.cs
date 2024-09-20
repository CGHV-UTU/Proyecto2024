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
    public partial class EliminarComentario : Form
    {
        private string usuario;
        public EliminarComentario(string user)
        {
            InitializeComponent();
            usuario = user;
        }
        private async void CargarTabla()
        {
            dataGridView1.DataSource = await CargarTodosLosComentarios(txtID.Text);
        }
        private void ModificarTabla()
        {
            DataGridViewCellStyle columnHeaderStyle = new DataGridViewCellStyle();
            columnHeaderStyle.BackColor = Color.Beige;
            columnHeaderStyle.Font = new Font("Verdana", 10, FontStyle.Bold);
            dataGridView1.ColumnHeadersDefaultCellStyle = columnHeaderStyle;
        }

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
                    dataGridView1.Show();
                    btnEliminar.Show();
                    txtIdComentario.Show();
                    lblComentario.Show();
                    CargarTabla();
                    ModificarTabla();
                }
                else
                {
                    lblErrorID.Show();
                    lblErrorID.Text = "No se encontró la ID";
                }
            }
        }
        static async Task<string[]> Buscar(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync($"https://localhost:44340/postPorId?id={id}");
                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();
                    dynamic data = JsonConvert.DeserializeObject(responseBody); 
                    return new string[] { data.texto, data.url, data.imagen };
                }
                catch
                {
                    return null;
                }
            }
        }
        static async Task<bool> Existe(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var datos = new { id = id };
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
        static async Task<dynamic> CargarTodosLosComentarios(string id)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var datos = new {id = id};
                    var content = new StringContent(JsonConvert.SerializeObject(datos), Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PutAsync($"https://localhost:44340/seleccionarTodosLosComentarios", content);
                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();
                    DataTable tabla = JsonConvert.DeserializeObject<DataTable>(responseBody);
                    return tabla;
                }
                catch
                {
                    return "No se pudo cargar la tabla";
                }
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Form1 f1 = new Form1(usuario);
            f1.Show();
            this.Close();
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            Form1 f1 = new Form1(usuario);
            f1.Show();
            this.Close();
        }
        private async void btnEliminar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtIdComentario.Text) || !int.TryParse(txtIdComentario.Text, out int numero))
            {
                lblError2.Show();
                lblError2.Text = "Debe ingresar una ID válida";
            }
            else
            {
                var respuesta = await ExisteComentario(int.Parse(txtIdComentario.Text));
                if (respuesta)
                {
                    await Eliminar(txtIdComentario.Text);
                    lblError2.Show();
                    lblError2.Text = "El Comentario se eliminó correctamente";
                    CargarTabla();
                    ModificarTabla();
                }
                else
                {
                    lblError2.Show();
                    lblError2.Text = "No se encontró la ID";
                }
            }
        }
        static async Task<bool> ExisteComentario(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var datos = new { id = id };
                    var content = new StringContent(JsonConvert.SerializeObject(datos), Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PutAsync($"https://localhost:44340/existeComentario", content);
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
        static async Task Eliminar(string id)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var datos = new { id = id };
                    var content = new StringContent(JsonConvert.SerializeObject(datos), Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PutAsync($"https://localhost:44340/eliminarComentario", content);
                    response.EnsureSuccessStatusCode();
                }
                catch (Exception)
                {

                }
            }
        }
    }
}
