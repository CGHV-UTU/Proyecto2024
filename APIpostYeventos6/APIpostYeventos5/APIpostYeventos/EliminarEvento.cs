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
using MySql.Data.MySqlClient;
using Newtonsoft.Json;

namespace APIpostYeventos
{
    public partial class EliminarEvento : Form
    {
        private string usuario;
        public EliminarEvento(string user)
        {
            InitializeComponent();
            CargarTabla();
            ModificarTabla();
            usuario = user;
        }
        private async void CargarTabla()
        {
            dataGridView1.DataSource = await CargarTodosLosEventos();
        }
        private void ModificarTabla()
        {
            DataGridViewCellStyle columnHeaderStyle = new DataGridViewCellStyle();
            columnHeaderStyle.BackColor = Color.Beige;
            columnHeaderStyle.Font = new Font("Verdana", 10, FontStyle.Bold);
            dataGridView1.ColumnHeadersDefaultCellStyle = columnHeaderStyle;
        }
        private void btnVolver_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1(usuario);
            form1.Show();
            this.Close();
        } 
        
       
        private async void btnEliminar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtIdEvento.Text) || !int.TryParse(txtIdEvento.Text, out int numero))
            {
                lblError.Show();
                lblError.Text = "Debe ingresar una ID válida";
            }
            else
            {
                var respuesta = await Existe(int.Parse(txtIdEvento.Text));
                if (respuesta)
                {
                    Eliminar(txtIdEvento.Text);
                    lblError.Text = "El Evento se eliminó correctamente";
                    lblError.Show();
                    CargarTabla();
                    ModificarTabla();
                }
                else
                {
                    lblError.Show();
                    lblError.Text = "No se encontró la ID";
                }
            }
        }

        static async Task<dynamic> CargarTodosLosEventos()
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync($"https://localhost:44340/seleccionarTodosLosEventos");
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
        static async Task Eliminar(string id)
        {
            using (HttpClient client = new HttpClient())
            {
                var datos = new { id = id };
                var content = new StringContent(JsonConvert.SerializeObject(datos), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PutAsync($"https://localhost:44340/eliminarEvento", content);
                response.EnsureSuccessStatusCode();
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
                    HttpResponseMessage response = await client.PutAsync($"https://localhost:44340/existeEvento", content);
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
    }
}
