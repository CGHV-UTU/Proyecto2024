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
    public partial class GestionarComentarios : Form
    {
        static MySqlConnection conn = new MySqlConnection("Server=localhost; database=infini; uID=root; pwd=;");
        public GestionarComentarios()
        {
            InitializeComponent();
            CargarTabla();
            InicializarTablaComentarios();
            this.ActiveControl = txtID;
        }
        
        private void CargarTabla()
        {
            string connectionString = "server = localhost; database = infini; uid = root; ";
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT id, texto, idPost, nombreCreador FROM Comentarios";
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
        private void InicializarTablaComentarios()
        {
            DataGridViewCellStyle columnHeaderStyle = new DataGridViewCellStyle();
            columnHeaderStyle.BackColor = Color.Beige;
            columnHeaderStyle.Font = new Font("Verdana", 10, FontStyle.Bold);
            dataGridView1.ColumnHeadersDefaultCellStyle = columnHeaderStyle;
            dataGridView1.Columns["id"].Width = 45;
            dataGridView1.Columns["idPost"].Width = 55;
            dataGridView1.Columns["texto"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells;
        }
        private void dataGridView1_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                dataGridView1.ClearSelection(); // Anula la selección
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
                            MySqlCommand command = new MySqlCommand("SELECT nombreDeCuenta, idPost, texto, fechaYhora FROM Comentarios WHERE id=@id", conn);
                            command.Parameters.AddWithValue("@id", int.Parse(txtID.Text));
                            MySqlDataReader reader = command.ExecuteReader();
                            if (reader.Read())
                            {
                                lblNombreDeCuenta.Text = reader["nombreDeCuenta"].ToString();
                                lblIdPost.Text = reader["idPost"].ToString();
                                txtTexto.Text = reader["texto"].ToString();
                                lblFechayHora.Text = reader["fechaYhora"].ToString();                               
                                // Mostrar todo lo oculto
                                foreach (Control control in this.Controls)
                                {                                                                    
                                        control.Visible = true;
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

        //Borro la fila del datagrid y registro su id
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                var filaSeleccionada = dataGridView1.CurrentRow;
                string id = txtID.Text;
                EliminarComentario(id);
                CargarTabla();
                InicializarTablaComentarios();
            }
            catch (Exception)
            {
                MessageBox.Show("No seleccionó una fila", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            }
        }
        private void EliminarComentario(string id)
        {
            conn.Open();
            MySqlCommand command = new MySqlCommand("DELETE FROM Comentarios WHERE id = @id", conn);
            MySqlCommand command1 = new MySqlCommand("DELETE FROM DaLikeComentario WHERE idComentario=@id", conn);
            MySqlCommand command2 = new MySqlCommand("DELETE FROM ParticipaEvento WHERE idEvento=@Id", conn);
            command2.Parameters.AddWithValue("@id", int.Parse(id));
            command2.ExecuteNonQuery();
            command.Parameters.AddWithValue("@id", int.Parse(id));
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
