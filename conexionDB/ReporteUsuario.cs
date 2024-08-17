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
    public partial class ReporteUsuario : Form
    {
        static MySqlConnection conn = new MySqlConnection("Server=localhost; database=base; uID=root; pwd=;");
        public ReporteUsuario()
        {
            InitializeComponent();
            cargarTabla();
            inicializarTablaUsuarios();
            dtpFecha.MinDate = DateTime.Today;
            int año = DateTime.Now.Year;
            dtpFecha.MaxDate = new DateTime(año, 12, 31);
            this.ActiveControl = txtID;
        }

        //Cargar tabla      
        private void cargarTabla()
        {
            string connectionString = "server = localhost; database = base; uid = root; ";
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT NumeroDeReporte, NombreDeUsuario FROM base.Reporte";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    foreach (DataRow row in dataTable.Rows)
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
            dataGridView1.Columns["NumeroDeReporte"].Width = 80;
            dataGridView1.Columns["NombreDeUsuario"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtID.Text))
            {
                try
                {
                    Boolean encontrado = false;
                    string nombre = txtID.Text;
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        if (row.Cells[0].Value != null && row.Cells[0].Value.ToString() == nombre)
                        {
                            conn.Open();
                            MySqlCommand command = new MySqlCommand("SELECT NombreDeUsuario, descripcion FROM base.Reporte WHERE id=@id", conn);
                            command.Parameters.AddWithValue("@id", txtID.Text);
                            MySqlDataReader reader = command.ExecuteReader();
                            if (reader.Read())
                            {
                                lblNombreDeCuenta.Text = reader["NombreDeUsuario"].ToString();
                                lblDescripcionReporte.Text = reader["descripcion"].ToString();                           
                                lblNombre.Show();
                                lblDescripcionReporte.Show();
                                lblDescripcion.Show();
                            }
                            conn.Close();
                            dataGridView1.ClearSelection();
                            row.Selected = true;
                            encontrado = true;
                            return;
                        }
                    }
                    if (!encontrado)
                    {
                        MessageBox.Show("No se encontró el reporte especificado.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("No se encontró el reporte especificado.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            else
            {
                MessageBox.Show("Debe ingresar una id", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnBaneoPermanente(object sender, EventArgs e)
        {
            if (lblNombreDeCuenta.Text == "")
            {
                MessageBox.Show("Debe ingresar un usuario a banear");
            }
            else
            {
                string connectionString = "server = localhost; database = base; uid = root; ";
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    MySqlCommand consultaEstado = new MySqlCommand("SELECT estado_de_cuenta FROM base.usuarios WHERE NombreDeCuenta = @NombreDeCuenta", conn);
                    consultaEstado.Parameters.AddWithValue("@NombreDeCuenta", lblNombreDeCuenta.Text);
                    MySqlDataReader reader = consultaEstado.ExecuteReader();
                    if (reader.Read())
                    {
                        if (reader["estado_de_cuenta"].ToString() == "baneado")
                        {
                            MessageBox.Show("El usuario ya se encuentra baneado permanentemente");
                        }
                        else
                        {
                            conn.Close();
                            MySqlCommand command = new MySqlCommand("UPDATE base.usuarios SET estado_de_cuenta = 'baneado', fechaBaneoTemporal = NULL WHERE NombreDeCuenta = @NombreDeCuenta", conn);
                            command.Parameters.AddWithValue("@NombreDeCuenta", lblNombreDeCuenta.Text);
                            try
                            {
                                conn.Open();
                                int rowsAffected = command.ExecuteNonQuery();
                                if (rowsAffected > 0)
                                {
                                    MessageBox.Show("Se ha baneado al usuario correctamente.");
                                    cargarTabla();
                                }
                                else
                                {
                                    MessageBox.Show("No se encontró el usuario en la base de datos.", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                            catch (Exception)
                            {
                                MessageBox.Show("Error al intentar banear al usuario.");
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("No se encontró el usuario en la base de datos.", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
        private void btnBaneoTemporal(object sender, EventArgs e)
        {
            if (lblNombreDeCuenta.Text == "")
            {
                MessageBox.Show("Debe ingresar un usuario a banear");
            }
            else
            {
                string fechayhora = dtpFecha.Text + " " + dtpHora.Text;
                DateTime fechayhora1 = Convert.ToDateTime(fechayhora);
                Console.WriteLine(fechayhora1);
                string connectionString = "server=localhost; database=base; uid=root;";
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    MySqlCommand command = new MySqlCommand("UPDATE base.usuarios SET estado_de_cuenta = 'temporal', fechaBaneoTemporal = @FechaBaneoTemporal WHERE NombreDeCuenta = @NombreDeCuenta", conn);
                    command.Parameters.AddWithValue("@NombreDeCuenta", lblNombreDeCuenta.Text);
                    command.Parameters.AddWithValue("@FechaBaneoTemporal", fechayhora1);
                    try
                    {
                        conn.Open();
                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Se ha baneado temporalmente al usuario.");
                            cargarTabla();
                        }
                        else
                        {
                            MessageBox.Show("No se encontró el usuario en la base de datos.", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Error al intentar banear temporalmente al usuario.");
                    }
                }
            }
        }

        private void btnDesbanear(object sender, EventArgs e)
        {
            if (lblNombreDeCuenta.Text == "")
            {
                MessageBox.Show("Debe ingresar un usuario a banear");
            }
            else
            {
                string connectionString = "server=localhost; database=base; uid=root;";
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    MySqlCommand consultaEstado = new MySqlCommand("SELECT estado_de_cuenta FROM base.usuarios WHERE NombreDeCuenta = @NombreDeCuenta", conn);
                    consultaEstado.Parameters.AddWithValue("@NombreDeCuenta", lblNombreDeCuenta.Text);
                    MySqlDataReader reader = consultaEstado.ExecuteReader();
                    if (reader.Read())
                    {
                        if (reader["estado_de_cuenta"].ToString() == "activo")
                        {
                            MessageBox.Show("El usuario se encuentra activo");
                        }
                        else
                        {
                            conn.Close();
                            MySqlCommand command = new MySqlCommand("UPDATE usuarios SET estado_de_cuenta = 'activo', fechaBaneoTemporal = NULL WHERE NombreDeCuenta = @NombreDeCuenta", conn);
                            command.Parameters.AddWithValue("@NombreDeCuenta", lblNombreDeCuenta.Text);
                            try
                            {
                                conn.Open();
                                int rowsAffected = command.ExecuteNonQuery();
                                if (rowsAffected > 0)
                                {
                                    MessageBox.Show("Se ha desbaneado al usuario.");
                                    cargarTabla();
                                }
                                else
                                {
                                    MessageBox.Show("No se encontró el usuario en la base de datos.", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                            catch (Exception)
                            {
                                MessageBox.Show("Error al intentar desbanear al usuario.");
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("No se encontró el usuario en la base de datos.", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
        private void btnVolver_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
