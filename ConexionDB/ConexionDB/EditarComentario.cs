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

namespace ConexionDB
{
    public partial class Editar_comentario : Form
    {
        public Editar_comentario()
        {
            InitializeComponent();
            cargarTabla();
        }

        private void inicializarTablaComentarios()
        {
            dataGridView1.ColumnCount = 3;
            dataGridView1.ColumnHeadersVisible = true;
            DataGridViewCellStyle columnHeaderStyle = new DataGridViewCellStyle();
            columnHeaderStyle.BackColor = Color.Beige;
            columnHeaderStyle.Font = new Font("Verdana", 10, FontStyle.Bold);
            dataGridView1.ColumnHeadersDefaultCellStyle = columnHeaderStyle;
            dataGridView1.Columns[0].Name = "ID";
            dataGridView1.Columns[1].Name = "Texto";
            dataGridView1.Columns[2].Name = "Likes";
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            try
            {
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
                selectedRow.Cells[1].Value = txtTexto.Text;         
                selectedRow.Cells[2].Value = Int32.Parse(txtLikes.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("No seleccionó una fila a modificar");
            }
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
                    string query = "SELECT id, texto, likes FROM comentarios";

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

        private void button15_Click(object sender, EventArgs e)
        {
            int textoOriginal = Int32.Parse(txtLikes.Text);
            int textoNuevo = textoOriginal + 1;
            txtLikes.Text = textoNuevo.ToString();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            int textoOriginal = Int32.Parse(txtLikes.Text);
            int textoNuevo = textoOriginal + 5;
            txtLikes.Text = textoNuevo.ToString();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            int textoOriginal = Int32.Parse(txtLikes.Text);
            int textoNuevo = textoOriginal + 10;
            txtLikes.Text = textoNuevo.ToString();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            int textoOriginal = Int32.Parse(txtLikes.Text);
            int textoNuevo = textoOriginal - 1;
            txtLikes.Text = textoNuevo.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int textoOriginal = Int32.Parse(txtLikes.Text);
            int textoNuevo = textoOriginal - 5;
            txtLikes.Text = textoNuevo.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int textoOriginal = Int32.Parse(txtLikes.Text);
            int textoNuevo = textoOriginal - 10;
            txtLikes.Text = textoNuevo.ToString();
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
                        return;
                    }
                }

                if (!encontrado)
                {
                    MessageBox.Show("No se encontró el comentario especificado.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("No se encontró el comentario especificado.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
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

        private void button5_Click(object sender, EventArgs e)
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
                        string texto = row.Cells["texto"].Value.ToString();
                        int likes = Convert.ToInt32(row.Cells["likes"].Value);


                        string query = "INSERT INTO comentarios (id, texto, likes) " +
                                       "VALUES (@id, @texto, @likes) " +
                                       "ON DUPLICATE KEY UPDATE " +
                                       "texto=@texto, likes=@likes";

                        // Create a MySQL command
                        MySqlCommand cmd = new MySqlCommand(query, conn);

                        // Set query parameters
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.Parameters.AddWithValue("@texto", texto);
                        cmd.Parameters.AddWithValue("@likes", likes);

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

        private void button6_Click(object sender, EventArgs e)
        {
            if (txtLikesPersonalizados.Text != "")
            {
                int textoOriginal = Int32.Parse(txtLikes.Text);
                int textoNuevo = textoOriginal + Int32.Parse(txtLikesPersonalizados.Text);
                txtLikes.Text = textoNuevo.ToString();
            }
        }
    }
}
