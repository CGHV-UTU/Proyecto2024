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
    public partial class ReporteComentario : Form
    {
        static MySqlConnection conn = new MySqlConnection("Server=localhost; database=infini; uID=root; pwd=;");
        public ReporteComentario()
        {
            InitializeComponent();
            cargarTabla();          
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
                    string query = "SELECT numeroDeReporte, creadorDelComentario, tipo FROM Reportes WHERE idComentario IS NOT NULL";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);                   
                    dataGridView1.DataSource = dataTable;
                    inicializarTablaPosts();
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
            columnHeaderStyle.Font = new Font("Verdana", 10, FontStyle.Bold);
            dataGridView1.ColumnHeadersDefaultCellStyle = columnHeaderStyle;
            dataGridView1.Columns["numeroDeReporte"].Width = 80;
            dataGridView1.Columns["tipo"].Width = 150;
            dataGridView1.Columns["numeroDeReporte"].HeaderText = "Reporte";
            dataGridView1.Columns["creadorDelComentario"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns["creadorDelComentario"].HeaderText = "Creador";
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtID.Text))
            {
                MessageBox.Show("Debe ingresar una id", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            try
            {
                //Buscar comentario y reporte
                int id = int.Parse(txtID.Text);
                if (buscarReporte(id))
                {
                    MessageBox.Show("Se encontro el comentario especificado.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
        // Busca un reporte en la base de datos usando el ID
        private bool buscarReporte(int id)
        {
            conn.Open();
            MySqlCommand command = new MySqlCommand("SELECT idComentario, creadorDelComentario, tipo, descripcion FROM Reportes WHERE numeroDeReporte=@id", conn);
            command.Parameters.AddWithValue("@id", id);
            MySqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                lblNombreDeCuenta.Text = reader["creadorDelComentario"].ToString();
                txtDescripcionReporte.Text = reader["descripcion"].ToString();
                lblTipo.Text = reader["tipo"].ToString();
                //Si encuentra el reporte carga el comentario
                if (BuscarComentario(Convert.ToInt32(reader["idComentario"].ToString())))
                {
                    return true; // Comentario encontrado
                }
                else
                {
                    return false; // Comentario no encontrado
                }
            }
            return false; // Error
        }

        //Borro la fila del datagrid y registro su id
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                string id = txtID.Text;
                EliminarComentario(id);
                cargarTabla();
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
            string folderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads", "Backoffice_CGHV_Log");
            Directory.CreateDirectory(folderPath);
            string path = Path.Combine(folderPath, "Log.txt");
            string mensaje = $"{DateTime.Now}: {Principal.admin} ha eliminado el comentario de id {id} por un reporte";
            using (StreamWriter writer = new StreamWriter(path, true))
            {
                writer.WriteLine(mensaje);
            }
        }
    }   
}
