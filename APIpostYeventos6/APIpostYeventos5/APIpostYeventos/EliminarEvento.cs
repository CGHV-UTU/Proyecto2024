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
                HttpResponseMessage response = await client.DeleteAsync($"https://localhost:44340/eliminarEvento?id={id}");
                response.EnsureSuccessStatusCode();
            }
        }

        static async Task<bool> Existe(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync($"https://localhost:44340/existeEvento?id={id}");
                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();
                    dynamic data = JsonConvert.DeserializeObject<bool>(responseBody); //sigo sin poder pasar esto a lo que quiero, no me deja acceder a la info del json de ninguna manera, tengo que hallar alguna forma de pasar los datos
                    return data;
                }
                catch
                {
                    return false;
                }
            }
        }

        //testing 
        public string eliminarEvento(string id)
        {
            try
            {
                MySqlConnection conn = new MySqlConnection("Server=localhost; database=base; uID=root; pwd=;");
                conn.Open();
                MySqlCommand command = new MySqlCommand("DELETE FROM eventos WHERE id=@Id", conn);
                command.Parameters.AddWithValue("@Id", int.Parse(id));
                command.ExecuteNonQuery();
                conn.Close();
                return "Evento eliminado";
            }
            catch
            {
                return "Evento no eliminado";
            }       
        }
        public string ultimoEvento()
        {
            try
            {
                MySqlConnection conn = new MySqlConnection("Server=localhost; database=base; uID=root; pwd=;");
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT id FROM eventos ORDER BY id DESC LIMIT 1", conn);
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    string id = reader["id"].ToString();
                    return id;
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }
        }
    }
}
