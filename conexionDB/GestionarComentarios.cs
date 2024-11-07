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
            this.ActiveControl = txtID;
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

        //Evitar cualquier seleccion en el datagrid
        private void dataGridView1_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            dataGridView1.ClearSelection();
        }
        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            dataGridView1.ClearSelection();
        }
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            dataGridView1.ClearSelection();
        }

        //Cargar lista de Comentarios
        private void CargarTabla()
        {
            // vuelvo a abrir una conexion porque la principal esta en uso y genera errores
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
                    EsteticaDeLaTabla();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message);
                }
            }
        }
        // Modificar aspectos visuales
        private void EsteticaDeLaTabla()
        {
            DataGridViewCellStyle columnHeaderStyle = new DataGridViewCellStyle();
            columnHeaderStyle.Font = new Font("Verdana", 10, FontStyle.Bold);
            dataGridView1.ColumnHeadersDefaultCellStyle = columnHeaderStyle;
            dataGridView1.Columns["id"].Width = 45;
            dataGridView1.Columns["idPost"].Width = 75;
            dataGridView1.Columns["nombreCreador"].Width = 105;
            dataGridView1.Columns["nombreCreador"].HeaderText = "Creador";
            dataGridView1.Columns["texto"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells;
        }

        // Boton para buscar Comentario
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtID.Text))
            {
                MessageBox.Show("Debe ingresar una id", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            try
            {
                int id = int.Parse(txtID.Text);
                if (BuscarComentario(id))
                {
                    Controls.OfType<Control>().ToList().ForEach(c => c.Visible = true);
                }
                else
                {
                    MessageBox.Show("No se encontro el comentario especificado.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Ocurrio un error al buscar el comentario.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Busca un comentario en la base de datos usando el ID
        private bool BuscarComentario(int id)
        {
            try
            {
                //Conexion con la base de datos
                conn.Open();
                MySqlCommand command = new MySqlCommand("SELECT nombreDeCuenta, idPost, texto, fechaYhora FROM Comentarios WHERE id=@id", conn);
                command.Parameters.AddWithValue("@id", id);
                MySqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    // Cargar los datos conseguidos
                    lblNombreDeCuenta.Text = reader["nombreDeCuenta"].ToString();
                    lblIdPost.Text = reader["idPost"].ToString();
                    txtTexto.Text = reader["texto"].ToString();
                    lblFechayHora.Text = reader["fechaYhora"].ToString();
                    conn.Close();
                    return true; // Comentario encontrado
                }
                conn.Close();
                return false; // Comentario no encontrado
            }
            catch (Exception)
            {
                conn.Close();
                return false; // Error
            }
        }

        //Eliminar comentario
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                string id = txtID.Text;
                EliminarComentario(id);
                CargarTabla();
            }
            catch (Exception)
            {
                MessageBox.Show("Ocurrio un error", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            }
        }

        //Eliminar informacion de la base de datos
        private void EliminarComentario(string id)
        {
            conn.Open();
            MySqlCommand command2 = new MySqlCommand("DELETE FROM Reportes WHERE idComentario=@id", conn);
            MySqlCommand command1 = new MySqlCommand("DELETE FROM DaLikeComentario WHERE idComentario=@id", conn);
            MySqlCommand command = new MySqlCommand("DELETE FROM Comentarios WHERE id = @id", conn);
            command2.Parameters.AddWithValue("@id", int.Parse(id));
            command2.ExecuteNonQuery();
            command1.Parameters.AddWithValue("@id", int.Parse(id));
            command1.ExecuteNonQuery();
            command.Parameters.AddWithValue("@id", int.Parse(id));
            command.ExecuteNonQuery();
            conn.Close();
            MessageBox.Show("Información eliminada con éxito");

            //Registro en logs
            string path = @"C:\Users\emerg\Downloads\elbackoffice\Proyecto2024\Log.txt";
            string mensaje = $"{DateTime.Now}: {Principal.admin} ha eliminado el comentario de id {id}";
            using (StreamWriter writer = new StreamWriter(path, true))
            {
                writer.WriteLine(mensaje);
            }
        }
    }
}
