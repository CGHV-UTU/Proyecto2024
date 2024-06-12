using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConexionDB
{
    public partial class Editar_evento : Form
    {
        public Editar_evento()
        {
            InitializeComponent();
            cargarTabla();
            inicializarTablaEventos();
            cargarHoras();
           
        }

        private void inicializarTablaEventos()
        {
            dataGridView1.Columns["id"].ReadOnly = true;
            dataGridView1.Columns["titulo"].ReadOnly = true;
            dataGridView1.Columns["ubicacion"].ReadOnly = true;
            dataGridView1.Columns["descripcion"].ReadOnly = true;
            dataGridView1.Columns["fechayhora"].ReadOnly = true;
          
            dataGridView1.ColumnHeadersVisible = true;

            DataGridViewCellStyle columnHeaderStyle = new DataGridViewCellStyle();

            columnHeaderStyle.BackColor = Color.Beige;
            columnHeaderStyle.Font = new Font("Verdana", 10, FontStyle.Bold);
            dataGridView1.ColumnHeadersDefaultCellStyle = columnHeaderStyle;
        }


        private void cargarTabla(/*object sender, EventArgs e*/)
        {
            // Define your connection string (adjust the parameters as needed)
            string connectionString = "server = localhost; database = base; uid = root; ";

            // Create a new MySQL connection
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    // Open the connection
                    conn.Open();

                    // Define your query
                    string query = "SELECT id, titulo, ubicacion, descripcion, fechayhora FROM eventos";

                    // Create a MySQL command
                    MySqlCommand cmd = new MySqlCommand(query, conn);

                    // Create a data adapter
                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);

                    // Create a DataTable to hold the query results
                    DataTable dataTable = new DataTable();

                    // Fill the DataTable with the query results
                    adapter.Fill(dataTable);

                    // Bind the DataTable to the DataGridView
                    dataGridView1.DataSource = dataTable;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message);
                }
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                Boolean encontrado = false;
                int id = int.Parse(txtID.Text);
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Cells[0].Value != null && int.Parse(row.Cells[0].Value.ToString()) == id)
                    {
                        row.Selected = true;
                        encontrado = true;
                        buttonCargarImagen();
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

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                var FilaSeleccionada = dataGridView1.CurrentRow;
                dataGridView1.Rows.Remove(FilaSeleccionada);
            }
            catch (Exception)
            {
                MessageBox.Show("No seleccionó una fila", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            // Define your connection string (adjust the parameters as needed)
            string connectionString = "server=localhost;database=base;uid=root;";

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    //Abro la conexión
                    conn.Open();

                    //Me fijo en todas las filas del DataGridView por información para después modificarla en la base de datos
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {

                        if (row.IsNewRow || !row.Cells[0].Value.ToString().All(char.IsDigit))
                        {
                            continue;
                        }


                        int id = Convert.ToInt32(row.Cells["id"].Value);
                        string titulo = row.Cells["titulo"].Value.ToString();
                        string ubicacion = row.Cells["ubicacion"].Value.ToString();
                        string descripcion = row.Cells["descripcion"].Value.ToString();
                        DateTime fechayhora = Convert.ToDateTime(row.Cells["fechayhora"].Value);



                        string query = "INSERT INTO eventos (id, titulo, ubicacion, descripcion, fechayhora) " +
                                       "VALUES (@id, @titulo, @ubicacion, @descripcion, @fechayhora) " +
                                       "ON DUPLICATE KEY UPDATE " +
                                       "titulo=@titulo, ubicacion=@ubicacion, descripcion=@descripcion, fechayhora=@fechayhora";

                        // Create a MySQL command
                        MySqlCommand cmd = new MySqlCommand(query, conn);

                        // Set query parameters
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.Parameters.AddWithValue("@titulo", titulo);
                        cmd.Parameters.AddWithValue("@ubicacion", ubicacion);
                        cmd.Parameters.AddWithValue("@descripcion", descripcion);
                        cmd.Parameters.AddWithValue("@fechayhora", fechayhora);

                        // Execute the query
                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("Información guardada con éxito");
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show("Ocurrió un error: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ocurrió un error: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        private void cargarHoras()
        {
            for (int hora = 0; hora < 24; hora++)
            {
                cbxHora.Items.Add($"{hora:00}:00");
                cbxHora.Items.Add($"{hora:00}:30");
            }

            // Establecer el valor predeterminado en el ComboBox
            cbxHora.SelectedIndex = 0;
        }

        private DateTime juntarFechayHora()
        {
            DateTime selectedDate = dtpFecha.Value.Date;
            string selectedTime = cbxHora.SelectedItem.ToString();
            TimeSpan time = TimeSpan.Parse(selectedTime);
            DateTime selectedDateTime = selectedDate.Add(time);
            return selectedDateTime;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
                selectedRow.Cells[1].Value = txtTitulo.Text;
                selectedRow.Cells[2].Value = txtUbicacion.Text;
                selectedRow.Cells[3].Value = txtDescripcion.Text;
                selectedRow.Cells[4].Value = juntarFechayHora();
            }
            catch (Exception)
            {
                MessageBox.Show("No seleccionó una fila a modificar");
            }


        }

        private void cargarImagen(int eventoId)
        {
            string connectionString = "server = localhost; database = base; uid = root;";

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT foto FROM eventos WHERE id = @id";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@id", eventoId);

                    byte[] imageData = (byte[])cmd.ExecuteScalar();

                    if (imageData != null && imageData.Length > 0)
                    {
                        using (MemoryStream ms = new MemoryStream(imageData))
                        {
                            pictureBox1.Image = Image.FromStream(ms);
                        }
                    }
                    else
                    {
                        MessageBox.Show("No se encontró la imagen para el evento especificado.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ocurrió un error: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        private void buttonCargarImagen()
        {
            try
            {
                int eventoId = int.Parse(txtID.Text);
                cargarImagen(eventoId);
            }
            catch (FormatException)
            {
                MessageBox.Show("Por favor, ingrese un ID válido.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

    }
}
