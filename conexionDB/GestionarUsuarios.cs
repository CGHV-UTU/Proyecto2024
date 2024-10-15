using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BackofficeDeAdministracion
{
    public partial class GestionarUsuarios : Form
    {
        static MySqlConnection conn = new MySqlConnection("Server=localhost; database=infini; uID=root; pwd=;");
        public GestionarUsuarios()
        {
            InitializeComponent();
            cargarTabla();
            inicializarTablaUsuarios();
            dtpFecha.MinDate = DateTime.Today;
            int año = DateTime.Now.Year;
            dtpFecha.MaxDate = new DateTime(año, 12, 31);
            this.ActiveControl = txtID;
            dataGridView1.CellMouseDown += dataGridView1_CellMouseDown;
            dataGridView1.MouseClick += dataGridView1_MouseClick;
            dataGridView1.SelectionChanged += dataGridView1_SelectionChanged;
            dataGridView1.ClearSelection();
        }    
        private void cargarTabla()
        {
            string connectionString = "server = localhost; database = infini; uid = root; ";
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT nombreDeCuenta, nombreVisible, email FROM Usuarios";
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
            dataGridView1.Columns["nombreDeCuenta"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells;
        }
        private async Task<string> CargarImagenDeGitHub(string urlImagen)
        {
            using (var client = new HttpClient())
            {
                string token = "11BKZVKOQ0DjsNNMCl27pG_bWGpU4CD8HpcEIQooMyAsLtedjVMN7kzcrz1WrYLmA9NOKBAL3W9WQKb76D"; // Token para repositorio privado. Cambiar por el token real
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var response = await client.GetAsync(urlImagen);
                if (response.IsSuccessStatusCode)
                {
                    byte[] imagenBytes = await response.Content.ReadAsByteArrayAsync();
                    return Convert.ToBase64String(imagenBytes);
                }
                else
                {
                    throw new Exception("No se pudo descargar la imagen desde GitHub.");
                }
            }
        }
        private void dataGridView1_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            dataGridView1.ClearSelection(); // Evita la selección con el mouse
        }

        // Evitar la selección cuando se hace clic en cualquier lugar del DataGridView
        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            dataGridView1.ClearSelection(); // Limpia la selección
        }

        // Evitar que cualquier selección se mantenga
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            dataGridView1.ClearSelection(); // Siempre limpia la selección si algo intenta seleccionarse
        }

        private void dataGridView1_MouseDown(object sender, MouseEventArgs e)
        {
            // Cancela cualquier selección cuando se hace clic en el DataGridView
            dataGridView1.ClearSelection();
        }

        private async void btnBuscar_Click(object sender, EventArgs e)
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
                            MySqlCommand command = new MySqlCommand("SELECT nombreDeCuenta, nombreVisible, foto, estadoDeCuenta, descripcion FROM Usuarios WHERE nombreDeCuenta=@NombreDeCuenta", conn);
                            command.Parameters.AddWithValue("@NombreDeCuenta", txtID.Text);
                            MySqlDataReader reader = command.ExecuteReader();
                            if (reader.Read())
                            {
                                lblNombreDeCuenta.Text = reader["nombreDeCuenta"].ToString();
                                lblNombreVisible.Text = reader["nombreVisible"].ToString();
                                lblEstadoDeCuenta.Text = reader["estadoDeCuenta"].ToString();
                                lblDescripcion.Text = reader["descripcion"].ToString();
                                try
                                {
                                    
                                    string imagen = await CargarImagenDeGitHub(reader["foto"].ToString());
                                    if (!string.IsNullOrEmpty(imagen))
                                    {
                                        pictureBox1.Show();
                                        byte[] imagenBytes = Convert.FromBase64String(imagen);
                                        using (MemoryStream ms = new MemoryStream(imagenBytes))
                                        {
                                            Bitmap bitmap = new Bitmap(ms);
                                            pictureBox1.Image = bitmap;
                                            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                                        }
                                    }
                                    else
                                    {
                                        pictureBox1.Hide();
                                    }
                                }
                                catch
                                {

                                }
                                label2.Show();
                                lblDescripcion.Show();
                                lblNombre.Show();
                                lblNombreVisible.Show();
                                lblNomVisible.Show();
                                lblEstadoDeCuenta.Show();
                                lblEstado.Show();
                                lblFoto.Show();
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
                        MessageBox.Show("No se encontró el usuario especificado.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("No se encontró el usuario especificado.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
                return;
            }

            string connectionString = "server=localhost; database=infini; uid=root;";
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    MySqlCommand consultaEstado = new MySqlCommand("SELECT idBan FROM Ban WHERE nombreDeUsuario = @NombreDeCuenta", conn);
                    consultaEstado.Parameters.AddWithValue("@NombreDeCuenta", lblNombreDeCuenta.Text);
                    MySqlDataReader reader = consultaEstado.ExecuteReader();

                    if (reader.Read())
                    {
                        MessageBox.Show("El usuario ya se encuentra baneado permanentemente");
                        return;
                    }
                    reader.Close();
                    MySqlCommand command = new MySqlCommand("INSERT INTO Ban (nombreDeUsuario, fechaInicio, fechaFinalizacion) VALUES (@NombreDeCuenta, NOW(), '3024-12-24 23:59:59')", conn);
                    command.Parameters.AddWithValue("@NombreDeCuenta", lblNombreDeCuenta.Text);

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
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al intentar banear al usuario: {ex.Message}");
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
                string connectionString = "server=localhost; database=infini; uid=root;";
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    MySqlCommand command = new MySqlCommand("INSERT INTO Ban(nombreDeUsuario, fechaInicio, fechaFinalizacion) VALUES(@NombreDeCuenta, NOW(), @FechaBaneoTemporal)", conn);
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
                string connectionString = "server=localhost; database=infini; uid=root;";
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    MySqlCommand consultaEstado = new MySqlCommand("SELECT nombreDeUsuario FROM Ban WHERE nombreDeUsuario = @NombreDeCuenta", conn);
                    consultaEstado.Parameters.AddWithValue("@NombreDeCuenta", lblNombreDeCuenta.Text);
                    MySqlDataReader reader = consultaEstado.ExecuteReader();
                    if (reader.Read())
                    {
                        conn.Close();
                        MySqlCommand command = new MySqlCommand("DELETE FROM Ban WHERE nombreDeUsuario = @NombreDeCuenta", conn);
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
                    else
                    {
                        MessageBox.Show("El usuario se encuentra activo");
                        conn.Close();
                    }
                }
            }
        }
    }
}
