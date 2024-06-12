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
using MySql.Data.MySqlClient;

namespace ConexionDB
{
    public partial class Editar_post : Form
    {
        public Editar_post()
        {
            InitializeComponent();        
            cargarTabla();
            inicializarTablaPosts();
        }

        private void inicializarTablaPosts()
        {
            dataGridView1.Columns["id"].ReadOnly = true;
            dataGridView1.Columns["texto"].ReadOnly = true;
            dataGridView1.Columns["url"].ReadOnly = true;
            dataGridView1.Columns["categorias"].ReadOnly = true;
            dataGridView1.Columns["comentarios"].ReadOnly = true;
            dataGridView1.Columns["likes"].ReadOnly = true;


            dataGridView1.ColumnHeadersVisible = true;

            DataGridViewCellStyle columnHeaderStyle = new DataGridViewCellStyle();

            columnHeaderStyle.BackColor = Color.Beige;
            columnHeaderStyle.Font = new Font("Verdana", 10, FontStyle.Bold);
            dataGridView1.ColumnHeadersDefaultCellStyle = columnHeaderStyle;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Close();
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
                    string query = "SELECT id, texto, url, categorias, comentarios, likes FROM posts";

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
                    MessageBox.Show("No se encontró el mundial en el post especificado.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("No se encontró el mundial en el post especificado.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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

       

        private void button1_Click(object sender, EventArgs e)
        {
            if (btnComentarios.Text == "Activar")
            {
                btnComentarios.Text = "Desactivar";
            } else
            {
                btnComentarios.Text = "Activar";
            }
            
        }

        private void button2_Click_1(object sender, EventArgs e)
        {   
            try
            {              
                    DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
                    selectedRow.Cells[0].Value = txtID.Text;
                    selectedRow.Cells[1].Value = txtTexto.Text;
                    selectedRow.Cells[2].Value = txtURL.Text;
                    selectedRow.Cells[3].Value = txtCategorias.Text;
                    selectedRow.Cells[4].Value = estadoComentarios();
                    selectedRow.Cells[5].Value = Int32.Parse(txtLikes.Text);             
            }
            catch (Exception)
            {
                MessageBox.Show("No seleccionó una fila a modificar");
            }
        }

        private Boolean estadoComentarios()
        {
            Boolean estadoComentarios= false ;
            if (btnComentarios.Text == "Desactivar")
            {
                estadoComentarios = true;
            }
            return estadoComentarios;
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
                        string texto = row.Cells["texto"].Value.ToString();
                        string url = row.Cells["url"].Value.ToString();
                        string categorias = row.Cells["categorias"].Value.ToString();
                        bool comentarios = Convert.ToBoolean(row.Cells["comentarios"].Value);
                        int likes = Convert.ToInt32(row.Cells["likes"].Value);

                     
                        string query = "INSERT INTO posts (id, texto, url, categorias, comentarios, likes) " +
                                       "VALUES (@id, @texto, @url, @categorias, @comentarios, @likes) " +
                                       "ON DUPLICATE KEY UPDATE " +
                                       "texto=@texto, url=@url, categorias=@categorias, comentarios=@comentarios, likes=@likes";

                        // Create a MySQL command
                        MySqlCommand cmd = new MySqlCommand(query, conn);

                        // Set query parameters
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.Parameters.AddWithValue("@texto", texto);
                        cmd.Parameters.AddWithValue("@url", url);
                        cmd.Parameters.AddWithValue("@categorias", categorias);
                        cmd.Parameters.AddWithValue("@comentarios", comentarios);
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
                    MessageBox.Show("Ocurrió un error: "  + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {    
                int textoOriginal = Int32.Parse(txtLikes.Text);
                int textoNuevo = textoOriginal + 1;
                txtLikes.Text = textoNuevo.ToString();                      
        }

        private void button10_Click(object sender, EventArgs e)
        {
            int textoOriginal = Int32.Parse(txtLikes.Text);
            int textoNuevo = textoOriginal + 5;
            txtLikes.Text = textoNuevo.ToString();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            int textoOriginal = Int32.Parse(txtLikes.Text);
            int textoNuevo = textoOriginal + 10;
            txtLikes.Text = textoNuevo.ToString();
        }

       
        private void button5_Click(object sender, EventArgs e)
        {
            int textoOriginal = Int32.Parse(txtLikes.Text);
            int textoNuevo = textoOriginal - 1;
            txtLikes.Text = textoNuevo.ToString();
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            int textoOriginal = Int32.Parse(txtLikes.Text);
            int textoNuevo = textoOriginal - 5;
            txtLikes.Text = textoNuevo.ToString();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            int textoOriginal = Int32.Parse(txtLikes.Text);
            int textoNuevo = textoOriginal - 10;
            txtLikes.Text = textoNuevo.ToString();
        }

        

        private void txtLikesPersonalizados_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                txtLikes.Text = "";
                MessageBox.Show("Solo se permiten números", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if(txtLikesPersonalizados.Text != "")
            {
                int textoOriginal = Int32.Parse(txtLikes.Text);
                int textoNuevo = textoOriginal + Int32.Parse(txtLikesPersonalizados.Text);
                txtLikes.Text = textoNuevo.ToString();
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
                    string query = "SELECT imagen FROM posts WHERE id = @id";
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
