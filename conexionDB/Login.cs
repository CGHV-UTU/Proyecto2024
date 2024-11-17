using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BackofficeDeAdministracion
{
    public partial class Login : Form
    {
        static MySqlConnection conn = new MySqlConnection("Server=192.168.5.50; database=cghv; uID=federico.gonzalez; pwd=56983793;");
        public Login()
        {
            InitializeComponent();
            VerificarConexión();
            VerificarAdmin();
        }

        //Crear usuario si no existe
        private void VerificarAdmin()
        {
            try
            {
                conn.Open();
                string query = "SELECT COUNT(*) FROM Login WHERE nombreDeCuenta = 'admin' AND contrasena = 'admincghv'";
                MySqlCommand command = new MySqlCommand(query, conn);
                int count = Convert.ToInt32(command.ExecuteScalar());
                if (count == 0)
                {
                    string insertQuery = "INSERT INTO Login (nombreDeCuenta, contrasena) VALUES ('admin', 'admincghv')";
                    MySqlCommand insertCommand = new MySqlCommand(insertQuery, conn);
                    insertCommand.ExecuteNonQuery();
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al verificar o crear el usuario admin: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAcceder_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtUser.Text) || string.IsNullOrEmpty(txtPass.Text))
            {
                MessageBox.Show("Debe ingresar un usuario y contraseña", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }            
                try
                {
                    string nombre = txtUser.Text;
                    conn.Open();
                    MySqlCommand command = new MySqlCommand("SELECT contrasena FROM Login WHERE nombreDeCuenta=@Nombre", conn);
                    command.Parameters.AddWithValue("@Nombre", nombre);
                    MySqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        string contReal = reader["contrasena"].ToString();
                        if (contReal == txtPass.Text)
                        {
                            MessageBox.Show("Acceso concedido.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            Principal inicio = new Principal(nombre);
                            inicio.FormClosed += (s, args) => this.Close();
                            inicio.Show();
                            this.Hide();
                        }
                        else
                        {
                            MessageBox.Show("No encontrado.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }                    
                    }
                    else
                    {
                        MessageBox.Show("No encontrado.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    conn.Close();                                   
                }
                catch (Exception)
                {
                    MessageBox.Show("Error.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                     
        }

        //Verificar conexión con la Base de Datos
        private void VerificarConexión()
        {       
            MySqlConnection conn = new MySqlConnection("Server=192.168.5.50; database=cghv; uID=federico.gonzalez; pwd=56983793;");
            try
            {
                conn.Open();
                MessageBox.Show("Conexión realizada con éxito");             
            }
            catch (Exception)
            {
                MessageBox.Show("No se pudo abrir la conexión con la base de datos", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(0);
            }
            conn.Close();
        }
    }
}
