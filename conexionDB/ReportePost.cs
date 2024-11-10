using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
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
            try
            {
                DataGridViewCellStyle columnHeaderStyle = new DataGridViewCellStyle();
                columnHeaderStyle.Font = new Font("Verdana", 10, FontStyle.Bold);
                dataGridView1.ColumnHeadersDefaultCellStyle = columnHeaderStyle;
                dataGridView1.Columns["numeroDeReporte"].Width = 80;
                dataGridView1.Columns["creadorDelPost"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridView1.Columns["numeroDeReporte"].HeaderText = "Reporte";
                dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells;
            }
            catch
            {
                
            }
        }

        // Buscar post
        private async void btnBuscar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtID.Text))
            {
                MessageBox.Show("Debe ingresar una id", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            int id;
            if (!int.TryParse(txtID.Text, out id))
            {
                MessageBox.Show("ID invalido.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            try
            {
                bool encontrado = await CargarReporte(id);

                if (!encontrado)
                {
                    MessageBox.Show("No se encontro el post especificado.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrio un error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task<bool> CargarReporte(int id)
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
                Controls.OfType<Control>().ToList().ForEach(c => c.Visible = true);
                bool post = await CargarDatosPost(Convert.ToInt32(reader["idPost"]));
                conn.Close();
                return true;
            }
            conn.Close();
            return false;          
        }

        private async Task<bool> CargarDatosPost(int id)
        {
            bool encontrado = false;
            using (var conn = new MySqlConnection("Server=localhost; database=infini; uID=root; pwd=;"))
            {
                await conn.OpenAsync();

                // Consulta del post
                using (var command = new MySqlCommand("SELECT texto, imagen, video, categoria, comentarios FROM Posts WHERE idPost=@id", conn))
                {
                    command.Parameters.AddWithValue("@id", id);
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            txtTexto.Text = reader["texto"].ToString();
                            txtCategorias.Text = reader["categoria"].ToString();
                            txtURL.Text = reader["video"].ToString();
                            await CargarImagen(reader["imagen"].ToString());
                        }
                    }
                }
            }
            return encontrado;
        }

        // Cargar imagen
        private async Task CargarImagen(string urlImagen)
        {
            try
            {
                Controls.OfType<Control>().ToList().ForEach(c => c.Visible = true);
                string imagenBase64 = await CargarImagenDeGitHub(urlImagen);

                if (!string.IsNullOrEmpty(imagenBase64))
                {
                    pictureBox1.Show();
                    byte[] imagenBytes = Convert.FromBase64String(imagenBase64);
                    using (MemoryStream ms = new MemoryStream(imagenBytes))
                    {
                        Bitmap bitmap = new Bitmap(ms);
                        pictureBox1.Image = bitmap;
                        pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                    }
                }
                else
                {
                    pictureBox1.Hide();
                }
            }
            catch
            {
                MessageBox.Show("No se pudo cargar la imagen.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        //Para visualizar las imagenes
        private async Task<string> CargarImagenDeGitHub(string urlImagen)
        {
            using (var client = new HttpClient())
            {
                string token = "no";
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var response = await client.GetAsync(urlImagen);
                if (response.IsSuccessStatusCode)
                {
                    byte[] imagenBytes = await response.Content.ReadAsByteArrayAsync();
                    return Convert.ToBase64String(imagenBytes);
                }
                else
                {
                    throw new Exception("No se pudo descargar la imagen desde GitHub.");
                }
            }
        }

        //Eliminar Post
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                string id = txtID.Text;
                EliminarPost(id);
                cargarTabla();
            }
            catch (Exception)
            {
                MessageBox.Show("No seleccionó una fila", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        private void EliminarPost(string id)
        {
            conn.Open();
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
            conn.Close();
            MessageBox.Show("Información eliminada con éxito.");
            string path = @"C:\Users\emerg\Downloads\elbackoffice\Proyecto2024\Log.txt";
            string mensaje = $"{DateTime.Now}: {Principal.admin} ha eliminado el post de id: {id}";
            using (StreamWriter writer = new StreamWriter(path, true))
            {
                writer.WriteLine(mensaje);
            }
        }
    }
}
