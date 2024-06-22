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
        public EliminarEvento()
        {
            InitializeComponent();
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
                    HttpResponseMessage response = await client.DeleteAsync($"https://localhost:44340/eliminarEvento?id={id}");
                    response.EnsureSuccessStatusCode();
            }
        }
       
        private void btnEliminar_Click_1(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtIdEvento.Text) || !int.TryParse(txtIdEvento.Text, out int numero) )
            {
                lblError.Show();
                lblError.Text = "Debe ingresar una ID válida";
            }
            else
            {
                Eliminar(txtIdEvento.Text);
                lblError.Text = "El Evento se eliminó correctamente";
                lblError.Show();
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
