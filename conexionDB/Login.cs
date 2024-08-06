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
        static MySqlConnection conn = new MySqlConnection("Server=localhost; database=base; uID=root; pwd=;");
        public Login()
        {
            InitializeComponent();
            VerificarConexión();
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
                    conn.Open();
                    MySqlCommand command = new MySqlCommand("SELECT Contraseña FROM AdministradorBackoffice WHERE Nombre=@Nombre", conn);
                    command.Parameters.AddWithValue("@Nombre", txtUser.Text);
                    MySqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        string contReal = reader["Contraseña"].ToString();
                        if (contReal == txtPass.Text)
                        {
                            MessageBox.Show("Acceso concedido.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            Principal inicio = new Principal(txtUser.Text);                      
                            inicio.Show();
                            this.Hide();
                        }
                        else
                        {
                            lblError2.Show();
                        }                    
                    }
                    else
                    {
                        MessageBox.Show("No se encontró el usuario especificado.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    conn.Close();                                   
                }
                catch (Exception)
                {
                    MessageBox.Show("No se encontró el usuario especificado.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                     
        }

        //Verificar conexión con la Base de Datos
        private void VerificarConexión()
        {       
            MySqlConnection conn = new MySqlConnection("server=localhost;database=base;uid=root;");
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

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
