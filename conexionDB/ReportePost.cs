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
    public partial class ReportePost : Form
    {
        static MySqlConnection conn = new MySqlConnection("Server=localhost; database=infini; uID=root; pwd=;");
        public ReportePost()
        {
            InitializeComponent();
            cargarTabla();
            inicializarTablaPosts();
        }

        //Cargar tabla      
        private void cargarTabla()
        {
            string connectionString = "server = localhost; database = infini; uid = root; ";
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT numeroDeReporte, idPost, creadorDelPost FROM Reportes WHERE idPost IS NOT NULL";
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
        private void inicializarTablaPosts()
        {
            try
            {
                DataGridViewCellStyle columnHeaderStyle = new DataGridViewCellStyle();
                columnHeaderStyle.BackColor = Color.Beige;
                columnHeaderStyle.Font = new Font("Verdana", 10, FontStyle.Bold);
                dataGridView1.ColumnHeadersDefaultCellStyle = columnHeaderStyle;
                dataGridView1.Columns["numeroDeReporte"].Width = 80;
                dataGridView1.Columns["creadorDelPost"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells;
            }
            catch
            {

            }
        }

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
                            MySqlCommand command = new MySqlCommand("SELECT creadorDelPost, idPost, tipo, descripcion FROM Reportes WHERE numeroDeReporte=@id", conn);
                            command.Parameters.AddWithValue("@id", int.Parse(txtID.Text));
                            MySqlDataReader reader = command.ExecuteReader();
                            if (reader.Read())
                            {
                                lblCuenta.Text = reader["creadorDelPost"].ToString();
                                lblDescripcionReporte.Text = reader["descripcion"].ToString();
                                lblTipo.Text = reader["tipo"].ToString();
                                lblIdPost.Text = reader["idPost"].ToString();
                                lblNombre.Show();
                                lblCuenta.Show();
                                label2.Show();
                                lblIdPost.Show();
                                label1.Show();
                                lblTipo.Show();
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
                        MessageBox.Show("No se encontró el post especificado.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("No se encontró el post especificado.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
                string id = lblIdPost.Text;
                GuardarId(id);
            }
            catch (Exception)
            {
                MessageBox.Show("No seleccionó una fila", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            }
        }
        //Guardo la id de los post borrados del datagrid para luego eliminarlos definitivamente
        List<string> eliminarDatos = new List<string>();
        private void GuardarId(string id)
        {
            eliminarDatos.Add(id);
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            conn.Open();
            foreach (string id in eliminarDatos)
            {
                MySqlCommand command = new MySqlCommand("DELETE FROM Reportes WHERE idPost=@Id;", conn);
                MySqlCommand command8 = new MySqlCommand("DELETE FROM Comentarios WHERE idPost=@Id", conn);
                MySqlCommand command2 = new MySqlCommand("DELETE FROM DaLike WHERE idPost = @Id", conn);
                MySqlCommand command3 = new MySqlCommand("DELETE FROM PostPublico WHERE idPost = @Id", conn);
                MySqlCommand command4 = new MySqlCommand("DELETE FROM PostGrupo WHERE idPost = @Id", conn);
                MySqlCommand command5 = new MySqlCommand("DELETE FROM PostEvento WHERE idPost = @Id", conn);
                MySqlCommand command6 = new MySqlCommand("DELETE FROM Posts WHERE idPost = @Id", conn);
                MySqlCommand command7 = new MySqlCommand("DELETE FROM DaLikeComentario WHERE idComentario=(SELECT id FROM Comentarios WHERE idPost=@id)", conn);
                command.Parameters.AddWithValue("@Id", id);
                command8.Parameters.AddWithValue("@Id", id);
                command2.Parameters.AddWithValue("@Id", id);
                command3.Parameters.AddWithValue("@Id", id);
                command4.Parameters.AddWithValue("@Id", id);
                command5.Parameters.AddWithValue("@Id", id);
                command6.Parameters.AddWithValue("@Id", id);
                command7.Parameters.AddWithValue("@Id", id);
                command7.ExecuteNonQuery();
                command.ExecuteNonQuery();
                command2.ExecuteNonQuery();
                command3.ExecuteNonQuery();
                command4.ExecuteNonQuery();
                command5.ExecuteNonQuery();
                command6.ExecuteNonQuery();
            }
            eliminarDatos.Clear();
            conn.Close();
            MessageBox.Show("Información guardada con éxito");
            this.Close();
        }
    }
}
