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
    public partial class ReporteComentario : Form
    {
        static MySqlConnection conn = new MySqlConnection("Server=localhost; database=infini; uID=root; pwd=;");
        public ReporteComentario()
        {
            InitializeComponent();
            cargarTabla();
            inicializarTablaPosts();
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
                    string query = "SELECT numeroDeReporte, creadorDelComentario, tipo FROM Reportes WHERE idComentario IS NOT NULL";
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
            DataGridViewCellStyle columnHeaderStyle = new DataGridViewCellStyle();
            columnHeaderStyle.BackColor = Color.Beige;
            columnHeaderStyle.Font = new Font("Verdana", 10, FontStyle.Bold);
            dataGridView1.ColumnHeadersDefaultCellStyle = columnHeaderStyle;
            dataGridView1.Columns["numeroDeReporte"].Width = 60;
            dataGridView1.Columns["numeroDeReporte"].HeaderText = "Reporte";
            dataGridView1.Columns["creadorDelComentario"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns["creadorDelComentario"].HeaderText = "Creador";
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells;
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
                            MySqlCommand command = new MySqlCommand("SELECT idComentario, creadorDelComentario, tipo, descripcion FROM Reportes WHERE numeroDeReporte=@id", conn);
                            command.Parameters.AddWithValue("@id", int.Parse(txtID.Text));
                            MySqlDataReader reader = command.ExecuteReader();
                            if (reader.Read())
                            {
                                lblNombreDeCuenta.Text = reader["creadorDelComentario"].ToString();
                                lblDescripcionReporte.Text = reader["descripcion"].ToString();
                                lblTipo.Text = reader["tipo"].ToString();
                                lblNombre.Show();
                                lblDescripcionReporte.Show();
                                lblDescripcion.Show();
                                reader.Close();
                                MySqlCommand command2 = new MySqlCommand("SELECT texto FROM comentarios WHERE id = @idComentario");
                                command2.Parameters.AddWithValue("@id", int.Parse(reader["idComentario"].ToString()));

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
                string id = txtID.Text;
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
            try
            {
                conn.Open();
                //Para remover de la base de datos los comentarios eliminados
                foreach (string id in eliminarDatos)
                {
                    string query = "DELETE FROM Comentarios WHERE id = @id";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@id", id);
                    try
                    {
                        string query2 = "DELETE FROM DaLikeComentario WHERE idComentario=@id";
                        MySqlCommand cmd2 = new MySqlCommand(query2, conn);
                        cmd2.Parameters.AddWithValue("@id", id);
                        cmd2.ExecuteNonQuery();
                    }
                    catch
                    {

                    }
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
    }   
}
