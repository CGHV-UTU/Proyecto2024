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
   
    public partial class GestionarEventos : Form
    {
        static MySqlConnection conn = new MySqlConnection("Server=localhost; database=infini; uID=root; pwd=;");
        public GestionarEventos()
        {
            InitializeComponent();
            CargarTabla();
            InicializarTablaEventos();
            this.ActiveControl = txtID;
            dataGridView1.CellMouseDown += dataGridView1_CellMouseDown;
            dataGridView1.MouseClick += dataGridView1_MouseClick;
            dataGridView1.SelectionChanged += dataGridView1_SelectionChanged;
            dataGridView1.ClearSelection();
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

        //Cargar tabla
        private void InicializarTablaEventos()
        {              
            DataGridViewCellStyle columnHeaderStyle = new DataGridViewCellStyle();
            columnHeaderStyle.BackColor = Color.Beige;
            columnHeaderStyle.Font = new Font("Verdana", 10, FontStyle.Bold);
            dataGridView1.ColumnHeadersDefaultCellStyle = columnHeaderStyle;
            dataGridView1.Columns["idEvento"].Width = 45;
            dataGridView1.Columns["idEvento"].HeaderText = "id";
            dataGridView1.Columns["descripcion"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells;
        }
        private void CargarTabla()
        {
            string connectionString = "server = localhost; database = infini; uid = root; ";
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT idEvento, titulo, ubicacion, descripcion FROM Eventos";
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

        //Buscar Evento
        private async void btnBuscar_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtID.Text))
            {
                try
                {
                    Boolean encontrado = false;
                    int id = int.Parse(txtID.Text);
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        if (row.Cells[0].Value != null && int.Parse(row.Cells[0].Value.ToString()) == id)
                        {
                            conn.Open();
                            MySqlCommand command = new MySqlCommand("SELECT titulo,ubicacion,descripcion,foto,fechaYhora_Inicio, fechaYhora_Final FROM Eventos WHERE idEvento=@id", conn);
                            command.Parameters.AddWithValue("@id", int.Parse(txtID.Text));
                            MySqlDataReader reader = command.ExecuteReader();
                            if (reader.Read())
                            {
                                txtTitulo.Text = reader["titulo"].ToString();
                                txtDescripcion.Text = reader["descripcion"].ToString();
                                txtUbicacion.Text = reader["ubicacion"].ToString();
                                lblInicio.Text = reader["fechaYhora_Inicio"].ToString();
                                lblFin.Text = reader["fechaYhora_Final"].ToString();
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
                            conn.Close();
                            dataGridView1.ClearSelection();
                            row.Selected = true;
                            encontrado = true;
                            return;
                        }
                    }
                    if (!encontrado)
                    {
                        MessageBox.Show("No se encontró el evento especificado.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("No se encontró el evento especificado.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            else
            {
                MessageBox.Show("Debe ingresar una id", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
                
        }

        //Borro la fila del datagrid y registro su id
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                var filaSeleccionada = dataGridView1.CurrentRow;
                string id = txtID.Text;
                EliminarEvento(id);
                CargarTabla();
                InicializarTablaEventos();
            }
            catch (Exception)
            {
                MessageBox.Show("No seleccionó una fila", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            }
        }
        private void EliminarEvento(string id)
        {
            conn.Open();
            MySqlCommand command = new MySqlCommand("DELETE FROM PostEvento WHERE idEvento=@Id", conn);
            MySqlCommand command1 = new MySqlCommand("DELETE FROM Eventos WHERE idEvento=@Id", conn);
            MySqlCommand command2 = new MySqlCommand("DELETE FROM ParticipaEvento WHERE idEvento=@Id", conn);
            command2.Parameters.AddWithValue("@Id", int.Parse(id));
            command2.ExecuteNonQuery();
            command.Parameters.AddWithValue("@Id", int.Parse(id));
            command.ExecuteNonQuery();
            command1.Parameters.AddWithValue("@Id", int.Parse(id));
            command1.ExecuteNonQuery();
            conn.Close();
            MessageBox.Show("Información eliminada con éxito");
        }

        //Para limitar la escritura de los txt a solo numeros
        private void txtID_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                MessageBox.Show("Solo se permiten números", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
            }
        }   
    }
}
