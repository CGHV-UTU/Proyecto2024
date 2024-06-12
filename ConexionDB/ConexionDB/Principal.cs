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

namespace ConexionDB
{
    public partial class Principal : Form
    {
        
        public Principal()
        {
            InitializeComponent();
            VerificarConexión();
        }

        private void button6_Click(object sender, EventArgs e)
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
                
            } else
            {
                MessageBox.Show("Seleccione una tabla", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            
            
          
        }

        private void VerificarConexión()
        {
            string myConnectionString = "server=localhost;database=base;uid=root;";
            // conexion con base de datos
            MySqlConnection cnn = new MySqlConnection(myConnectionString);
            try
            {
                cnn.Open();
                MessageBox.Show("Conexión realizada con éxito");
                //cnn.Close();

                // hago consulta en la base
                string query = "SELECT * FROM posts";

                //create command and assign the query and connection from the constructor
                MySqlCommand cmd = new MySqlCommand(query, cnn);

                //Execute command
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    //Use GetString etc depending on the column datatypes.
                    Console.WriteLine(reader.GetInt32(0));
                    Console.WriteLine(reader.GetString(1));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo abrir la conexión con la base de datos", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            //close connection
            cnn.Close();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
