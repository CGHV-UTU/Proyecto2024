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
    public partial class Editar_comentario : Form
    {
        static MySqlConnection conn = new MySqlConnection("Server=localhost; database=base; uID=root; pwd=;");
        public Editar_comentario()
        {
            InitializeComponent();
            cargarTabla();
            inicializarTablaComentarios();
            this.ActiveControl = txtID;
        }

        private void inicializarTablaComentarios()
        {
            DataGridViewCellStyle columnHeaderStyle = new DataGridViewCellStyle();
            columnHeaderStyle.BackColor = Color.Beige;
            columnHeaderStyle.Font = new Font("Verdana", 10, FontStyle.Bold);
            dataGridView1.ColumnHeadersDefaultCellStyle = columnHeaderStyle;
            dataGridView1.Columns["id"].Width = 45;
            dataGridView1.Columns["Texto"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns["likes"].Width = 65;
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells;
        }
        private void cargarTabla()
        {
            // Define your connection string (adjust the parameters as needed)
            string connectionString = "server = localhost; database = base; uid = root; ";
            // Create a new MySQL connection
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT id, texto, likes FROM comentarios";
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

        //Buscar comentario
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if(!string.IsNullOrEmpty(txtID.Text))
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
                            MySqlCommand command = new MySqlCommand("SELECT texto, likes FROM comentarios WHERE id=@id", conn);
                            command.Parameters.AddWithValue("@id", int.Parse(txtID.Text));
                            MySqlDataReader reader = command.ExecuteReader();
                            if (reader.Read())
                            {
                                txtTexto.Text = reader["texto"].ToString();
                                lblLikesDeComentario.Text = reader["likes"].ToString();
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
                        MessageBox.Show("No se encontró el comentario especificado.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("No se encontró el comentario especificado.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            else
            {
                MessageBox.Show("Debe ingresar una id", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        //Modifico la fila seleccionada en el Datagrid
        private void btnModificarClick(object sender, EventArgs e)
        {
            try
            {
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
                selectedRow.Cells[1].Value = txtTexto.Text;
                selectedRow.Cells[2].Value = Int32.Parse(lblLikesDeComentario.Text);
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
                var filaSeleccionada = dataGridView1.CurrentRow;
                string id = filaSeleccionada.Cells[0].Value.ToString();
                dataGridView1.Rows.Remove(filaSeleccionada);
                GuardarId(id);
            }
            catch (Exception)
            {
                MessageBox.Show("No seleccionó una fila", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            }
        }
        //Guardo la id de los comentarios borrados del datagrid para luego eliminarlos definitivamente
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
                    string texto = row.Cells["texto"].Value.ToString();
                    int likes = Convert.ToInt32(row.Cells["likes"].Value);
                    string query = "INSERT INTO comentarios (id, texto, likes) " + "VALUES (@id, @texto, @likes) " + "ON DUPLICATE KEY UPDATE " + "texto=@texto, likes=@likes";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@texto", texto);
                    cmd.Parameters.AddWithValue("@likes", likes);
                    cmd.ExecuteNonQuery();
                }
                //Para remover de la base de datos los comentarios eliminados
                foreach (string id in eliminarDatos)
                {
                    string query = "DELETE FROM comentarios WHERE id = @id";
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
 
        //Botones para gestión de likes  
        //Sumar Likes fijo
        private void btnSumar1Like_Click(object sender, EventArgs e)
        {
            int textoOriginal = int.Parse(lblLikesDeComentario.Text);
            int textoNuevo = textoOriginal + 1;
            lblLikesDeComentario.Text = textoNuevo.ToString();
        }
        private void btnSumar5Likes_Click(object sender, EventArgs e)
        {
            int textoOriginal = int.Parse(lblLikesDeComentario.Text);
            int textoNuevo = textoOriginal + 5;
            lblLikesDeComentario.Text = textoNuevo.ToString();
        }
        private void btnSumar10Likes_Click(object sender, EventArgs e)
        {
            int textoOriginal = int.Parse(lblLikesDeComentario.Text);
            int textoNuevo = textoOriginal + 10;
            lblLikesDeComentario.Text = textoNuevo.ToString();
        }

        //Restar Likes fijo
        private void VerificarMinimo(int nuevoValor)
        {
            if (nuevoValor >= 0)
            {
                lblLikesDeComentario.Text = nuevoValor.ToString();
            }
            else
            {
                lblLikesDeComentario.Text = "0";
            }
        }
        private void btnRestar1Like_Click(object sender, EventArgs e)
        {
            int textoOriginal = int.Parse(lblLikesDeComentario.Text);
            int textoNuevo = textoOriginal - 1;
            VerificarMinimo(textoNuevo);
        }
        private void btnRestar5Likes_Click(object sender, EventArgs e)
        {
            int textoOriginal = int.Parse(lblLikesDeComentario.Text);
            int textoNuevo = textoOriginal - 5;
            VerificarMinimo(textoNuevo);
        }
        private void btnRestar10Likes_Click(object sender, EventArgs e)
        {
            int textoOriginal = int.Parse(lblLikesDeComentario.Text);
            int textoNuevo = textoOriginal - 10;
            VerificarMinimo(textoNuevo);
        }
    
        //Cambiar likes Personalizado
        private void btnAgregarLikes_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtLikesPersonalizados.Text))
            {
                int textoOriginal = int.Parse(lblLikesDeComentario.Text);
                int textoNuevo = textoOriginal + int.Parse(txtLikesPersonalizados.Text);
                lblLikesDeComentario.Text = textoNuevo.ToString();
            }
        }
        private void btnRestarLikes_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtLikesPersonalizados.Text))
            {
                int textoOriginal = int.Parse(lblLikesDeComentario.Text);
                int textoNuevo = textoOriginal - int.Parse(txtLikesPersonalizados.Text);
                VerificarMinimo(textoNuevo);
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
        private void txtLikesPersonalizados_KeyPress(object sender, KeyPressEventArgs e)
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
