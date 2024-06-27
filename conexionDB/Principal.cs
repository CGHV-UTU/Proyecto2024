using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace BackofficeDeAdministracion
{
    public partial class Principal : Form
    {
        
        public Principal()
        {
            InitializeComponent();
            VerificarConexión();
        }

        private void btnCargar_Click(object sender, EventArgs e)
        {
            if (cbxTabla.SelectedItem != null)
            {
                string tabla = cbxTabla.SelectedItem.ToString();
                switch (tabla)
                {
                    case "Post":
                        Editar_post ventanaPost = new Editar_post();
                        ventanaPost.Show();
                        break;

                    case "Evento":
                        Editar_evento ventanaEvento = new Editar_evento();
                        ventanaEvento.Show();
                        break;

                    case "Comentario":
                        Editar_comentario ventanaComentario = new Editar_comentario();
                        ventanaComentario.Show();
                        break;
                }
            }
            else
            {
                MessageBox.Show("Seleccione una tabla", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // conexion con base de datos
        private void VerificarConexión()
        {
            string myConnectionString = "server=localhost;database=base;uid=root;";       
            MySqlConnection conn = new MySqlConnection(myConnectionString);
            try
            {
                conn.Open();
                MessageBox.Show("Conexión realizada con éxito");
                string query = "SELECT * FROM posts";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Console.WriteLine(reader.GetInt32(0));
                    Console.WriteLine(reader.GetString(1));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo abrir la conexión con la base de datos", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(0);
            }
            conn.Close();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
