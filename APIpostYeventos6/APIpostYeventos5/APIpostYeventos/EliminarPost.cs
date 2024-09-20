using System;
using System.Collections;
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
using System.Xml.Serialization;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;

namespace APIpostYeventos
{
    public partial class EliminarPost : Form
    {
        private static string usuario;
        public EliminarPost(string user)
        {
            InitializeComponent();
            CargarTabla();
            ModificarTabla();
            usuario = user;
        }

        private async void CargarTabla()
        {
            dataGridView1.DataSource = await CargarTodosLosPosts();
        }
        private void ModificarTabla()
        {
            DataGridViewCellStyle columnHeaderStyle = new DataGridViewCellStyle();
            columnHeaderStyle.BackColor = Color.Beige;
            columnHeaderStyle.Font = new Font("Verdana", 10, FontStyle.Bold);
            dataGridView1.ColumnHeadersDefaultCellStyle = columnHeaderStyle;
        }
        private async void btnEliminar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtID.Text) || !int.TryParse(txtID.Text, out int numero))
            {
                lblError.Show();
                lblError.Text = "Debe ingresar una ID válida";
            }
            else
            {
                var respuesta = await Existe(int.Parse(txtID.Text));
                if (respuesta)
                {
                    await Eliminar(txtID.Text);
                    lblError.Show();
                    MessageBox.Show("El Post se eliminó correctamente");
                    CargarTabla();
                    ModificarTabla();
                }
                else
                {
                    MessageBox.Show("No se encontro la ID");
                }
            }
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1(usuario);
            form1.Show();
            this.Close();
        }

        static async Task Eliminar(string id)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var datos = new { id = id };
                    var content = new StringContent(JsonConvert.SerializeObject(datos), Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PutAsync($"https://localhost:44340/eliminarPost", content);
                    response.EnsureSuccessStatusCode();
                }
                catch (Exception)
                {

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

        static async Task<dynamic> CargarTodosLosPosts()
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync($"https://localhost:44340/seleccionarTodosLosPost");
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
    }
}
