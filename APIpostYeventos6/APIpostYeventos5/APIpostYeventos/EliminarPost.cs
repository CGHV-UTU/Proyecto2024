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
        public EliminarPost()
        {
            InitializeComponent();
            CargarTabla();
            ModificarTabla();
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
                    Eliminar(txtID.Text);
                    lblError.Show();
                    lblError.Text = "El Post se eliminó correctamente";
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

        private void btnVolver_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();
            this.Close();
        }

        static async Task Eliminar(string id)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.DeleteAsync($"https://localhost:44340/eliminarPost?id={id}");
                    response.EnsureSuccessStatusCode();
                }
                catch (Exception ex)
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
                    HttpResponseMessage response = await client.GetAsync($"https://localhost:44340/existePost?id={id}");
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

        //para testing
        public string probarEliminarPost(string id)
        {
            try
            {
                MySqlConnection conn = new MySqlConnection("Server=localhost; database=base; uID=root; pwd=;");
                conn.Open();
                MySqlCommand command1 = new MySqlCommand("DELETE FROM comentarios WHERE IdPost=@IdPost", conn);
                MySqlCommand command = new MySqlCommand("DELETE FROM posts WHERE id=@Id", conn);
                command1.Parameters.AddWithValue("@IdPost", int.Parse(id));
                command.Parameters.AddWithValue("@Id", int.Parse(id));
                command1.ExecuteNonQuery();
                command.ExecuteNonQuery();
                conn.Close();
                return "El Post se eliminó correctamente";
            }
            catch (Exception ex)
            {
                return "El post no se eliminó";
            }
        }
        public string ultimoPost()
        {
            MySqlConnection conn = new MySqlConnection("Server=localhost; database=base; uID=root; pwd=;");
            conn.Open();
            MySqlCommand command = new MySqlCommand("SELECT id FROM posts ORDER BY id DESC LIMIT 1", conn);
            MySqlDataReader reader = command.ExecuteReader();
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

    }
}
