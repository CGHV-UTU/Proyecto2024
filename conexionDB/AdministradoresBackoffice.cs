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
    public partial class AdministradoresBackoffice : Form
    {
        static MySqlConnection conn = new MySqlConnection("Server=localhost; database=base; uID=root; pwd=;");
        public AdministradoresBackoffice()
        {
            InitializeComponent();
            PanelCrear.Visible = false;
            PanelEliminar.Visible = false;
        }

        //Selección de Panel
        private void cbxOpcion_SelectedIndexChanged(object sender, EventArgs e)
        {
            int seleccionado = cbxOpcion.SelectedIndex;
            PanelCrear.Visible = false;
            PanelEliminar.Visible = false;

            switch (seleccionado)
            {
                case 0:
                    PanelCrear.Visible = true;
                    break;
                case 1:
                    PanelEliminar.Visible = true;
                    cargarTabla();
                    inicializarTablaUsuarios();
                    break;
            }
        }

        // Panel Para Eliminar
        //Cargar tabla      
        private void cargarTabla()
        {
            string connectionString = "server = localhost; database = base; uid = root; ";
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT Nombre FROM base.AdministradorBackoffice";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    dataGridView1.DataSource = dataTable;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message);
                }
            }
        }
        private void inicializarTablaUsuarios()
        {
            DataGridViewCellStyle columnHeaderStyle = new DataGridViewCellStyle();
            columnHeaderStyle.BackColor = Color.Beige;
            columnHeaderStyle.Font = new Font("Verdana", 10, FontStyle.Bold);
            dataGridView1.ColumnHeadersDefaultCellStyle = columnHeaderStyle;
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells;
            dataGridView1.Columns["Nombre"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        //Panel para registrar un nuevo administrador
        private void btnRegistrar(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtUserE.Text) || string.IsNullOrEmpty(txtPassE.Text))
            {
                MessageBox.Show("Debe ingresar un usuario y contraseña", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            try
            {
                conn.Open();
                MySqlCommand command1 = new MySqlCommand("SELECT * FROM base.AdministradorBackoffice WHERE Nombre = @User;", conn);
                command1.Parameters.AddWithValue("@User", txtUserE.Text);
                MySqlDataReader reader = command1.ExecuteReader();
                if (reader.HasRows)
                {
                    MessageBox.Show("El usuario ya existe.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    reader.Close();
                    MySqlCommand command = new MySqlCommand("INSERT INTO base.AdministradorBackoffice(Nombre,Contraseña) VALUES (@User, @Pass);", conn);
                    command.Parameters.AddWithValue("@User", txtUserE.Text);
                    command.Parameters.AddWithValue("@Pass", txtPassE.Text);
                    command.ExecuteNonQuery();
                }
                conn.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("No se encontró el usuario especificado.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        
        //Eliminar
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtUserE.Text))
            {
                MessageBox.Show("Debe ingresar un usuario", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            try
            {
                conn.Open();
                MySqlCommand command1 = new MySqlCommand("SELECT * FROM base.AdministradorBackoffice WHERE Nombre = @User;", conn);
                command1.Parameters.AddWithValue("@User", txtUserE.Text);
                MySqlDataReader reader = command1.ExecuteReader();

                if (!reader.HasRows)
                {
                    MessageBox.Show("El usuario no existe.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    reader.Close();
                    MySqlCommand command = new MySqlCommand("DELETE FROM base.AdministradorBackoffice WHERE Nombre = @User;", conn);
                    command.Parameters.AddWithValue("@User", txtUserE.Text);
                    command.ExecuteNonQuery();
                    MessageBox.Show("Usuario eliminado con éxito.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                conn.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Ocurrió un error al intentar eliminar el usuario.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Volver
        private void btnVolver_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
