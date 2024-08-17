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
    public partial class RegistrarAdministrador : Form
    {
        static MySqlConnection conn = new MySqlConnection("Server=localhost; database=base; uID=root; pwd=;");
        public RegistrarAdministrador()
        {
            InitializeComponent();
        }
        private void btnRegistrar(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtUser.Text) || string.IsNullOrEmpty(txtPass.Text))
            {
                MessageBox.Show("Debe ingresar un usuario y contraseña", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            try
            {
                conn.Open();
                MySqlCommand command1 = new MySqlCommand("SELECT * FROM base.AdministradorBackoffice WHERE Nombre = @User;", conn);
                command1.Parameters.AddWithValue("@User", txtUser.Text);
                MySqlDataReader reader = command1.ExecuteReader();
                if (reader.HasRows)
                {
                    MessageBox.Show("El usuario ya existe.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    reader.Close();
                    MySqlCommand command = new MySqlCommand("INSERT INTO base.AdministradorBackoffice(Nombre,Contraseña) VALUES (@User, @Pass);", conn);
                    command.Parameters.AddWithValue("@User", txtUser.Text);
                    command.Parameters.AddWithValue("@Pass", txtPass.Text);
                    command.ExecuteNonQuery();
                }
                conn.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("No se encontró el usuario especificado.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
    }
}
