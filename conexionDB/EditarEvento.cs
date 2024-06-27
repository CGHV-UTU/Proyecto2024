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

namespace BackofficeDeAdministracion
{
   
    public partial class Editar_evento : Form
    {
        static MySqlConnection conn = new MySqlConnection("Server=localhost; database=base; uID=root; pwd=;");
        public Editar_evento()
        {
            InitializeComponent();
            cargarTabla();
            inicializarTablaEventos();
            dtpFecha.MinDate = DateTime.Today;
            int año = DateTime.Now.Year;
            dtpFecha.MaxDate = new DateTime(año, 12, 31);
            this.ActiveControl = txtID;
        }

        //Cargar tabla
        private void inicializarTablaEventos()
        {
            dataGridView1.Columns["id"].ReadOnly = true;
            dataGridView1.Columns["titulo"].ReadOnly = true;
            dataGridView1.Columns["ubicacion"].ReadOnly = true;
            dataGridView1.Columns["descripcion"].ReadOnly = true;
            dataGridView1.Columns["fechayhora"].ReadOnly = true;    
            dataGridView1.ColumnHeadersVisible = true;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
            DataGridViewCellStyle columnHeaderStyle = new DataGridViewCellStyle();
            columnHeaderStyle.BackColor = Color.Beige;
            columnHeaderStyle.Font = new Font("Verdana", 10, FontStyle.Bold);
            dataGridView1.ColumnHeadersDefaultCellStyle = columnHeaderStyle;
            dataGridView1.Columns["id"].Width = 45;
            dataGridView1.Columns["descripcion"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns["fechayhora"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns["fechayhora"].HeaderText = "fecha";
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells;
        }
        private void cargarTabla(/*object sender, EventArgs e*/)
        {
            // Define your connection string
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

        //Buscar Evento
        private void btnBuscar_Click(object sender, EventArgs e)
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
                            MySqlCommand command = new MySqlCommand("SELECT titulo,ubicacion,descripcion,foto,fechayhora FROM eventos WHERE id=@id", conn);
                            command.Parameters.AddWithValue("@id", int.Parse(txtID.Text));
                            MySqlDataReader reader = command.ExecuteReader();
                            if (reader.Read())
                            {
                                txtTitulo.Text = reader["titulo"].ToString();
                                txtDescripcion.Text = reader["descripcion"].ToString();
                                txtUbicacion.Text = reader["ubicacion"].ToString();
                                MemoryStream ms = new MemoryStream((byte[])reader["foto"]);
                                Bitmap bitmap = new Bitmap(ms);
                                pictureBox1.Image = bitmap;
                                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                                lblFechayHora.Text = "Fecha y hora previa del evento: " + reader["fechayhora"];
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

        //Modifico la fila seleccionada en el Datagrid
        private void btnModificar_Click(object sender, EventArgs e)
        {
            string fechayhora = dtpFecha.Text + " " + dtpHora.Text;
            try
            {
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
                selectedRow.Cells[1].Value = txtTitulo.Text;
                selectedRow.Cells[2].Value = txtUbicacion.Text;
                selectedRow.Cells[3].Value = txtDescripcion.Text;
                selectedRow.Cells[4].Value = fechayhora;
            }
            catch (Exception)
            {
                MessageBox.Show("No seleccionó una fila a modificar");
            }
        }

        //Borro la fila del datagrid y registro su id
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                var filaSeleccionada = dataGridView1.SelectedRows[0];
                string id = filaSeleccionada.Cells[0].Value.ToString();
                dataGridView1.Rows.Remove(filaSeleccionada);
                GuardarId(id);
            }
            catch (Exception)
            {
                MessageBox.Show("No seleccionó una fila", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        //Guardo la id de los eventos borrados del datagrid para luego eliminarlos definitivamente
        List<string> eliminarDatos = new List<string>();
        private void GuardarId(string id)
        {          
            eliminarDatos.Add(id);
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {               
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
                    string query = "INSERT INTO eventos (id, titulo, ubicacion, descripcion, fechayhora) " + "VALUES (@id, @titulo, @ubicacion, @descripcion, @fechayhora) " + "ON DUPLICATE KEY UPDATE " + "titulo=@titulo, ubicacion=@ubicacion, descripcion=@descripcion, fechayhora=@fechayhora";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@titulo", titulo);
                    cmd.Parameters.AddWithValue("@ubicacion", ubicacion);
                    cmd.Parameters.AddWithValue("@descripcion", descripcion);
                    cmd.Parameters.AddWithValue("@fechayhora", fechayhora);
                    cmd.ExecuteNonQuery();
                }
                //Para remover de la base de datos los eventos eliminados
                foreach (string id in eliminarDatos)
                {
                    string query = "DELETE FROM eventos WHERE id = @id";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
                eliminarDatos.Clear();
                conn.Close();
                MessageBox.Show("Información guardada con éxito");
                this.Close();
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
        
        private void btnSeleccionar_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.ImageLocation = ofd.FileName;
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            }
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
        private void btnVolver_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
