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
using MySql.Data.MySqlClient;

namespace BackofficeDeAdministracion
{
    public partial class GestionarPosts : Form
    {
        static MySqlConnection conn = new MySqlConnection("Server=localhost; database=infini; uID=root; pwd=;");
        public GestionarPosts()
        {
            InitializeComponent();        
            CargarTabla();
            InicializarTablaPosts();
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
                    string query = "SELECT Posts.idPost,Posts.texto,Posts.video,Posts.categoria,Posts.comentarios,COUNT(DaLike.idPost) AS cantidadLikes FROM Posts LEFT JOIN DaLike ON Posts.idPost=DaLike.idPost GROUP BY Posts.idPost,Posts.texto,Posts.video,Posts.categoria,Posts.comentarios;";
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

        //Cargar tabla
        private void InicializarTablaPosts()
        {
            DataGridViewCellStyle columnHeaderStyle = new DataGridViewCellStyle();
            columnHeaderStyle.BackColor = Color.Beige;
            columnHeaderStyle.Font = new Font("Verdana", 10, FontStyle.Bold);
            dataGridView1.ColumnHeadersDefaultCellStyle = columnHeaderStyle;        
            dataGridView1.BorderStyle = BorderStyle.None;
            dataGridView1.EnableHeadersVisualStyles = false;         
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells;
            dataGridView1.Columns["cantidadLikes"].Width = 75;
            dataGridView1.Columns["idPost"].HeaderText = "likes";
            dataGridView1.Columns["idPost"].Width = 51;
            dataGridView1.Columns["idPost"].HeaderText = "id";         
            dataGridView1.Columns["texto"].Width = 147;
            dataGridView1.Columns["comentarios"].Width = 75;
            dataGridView1.Columns["comentarios"].HeaderText = "Coment";
        }


        private async Task<string> CargarImagenDeGitHub(string urlImagen)
        {
            using (var client = new HttpClient())
            {
                string token = "11BKZVKOQ0DjsNNMCl27pG_bWGpU4CD8HpcEIQooMyAsLtedjVMN7kzcrz1WrYLmA9NOKBAL3W9WQKb76D"; // Token para repositorio privado. Cambiar por el token real
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

        //Buscar post
        private async void btnBuscar_Click(object sender, EventArgs e)
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
                            MySqlCommand command = new MySqlCommand("SELECT texto, imagen, video, categoria FROM Posts WHERE idPost=@id", conn);
                            command.Parameters.AddWithValue("@id", int.Parse(txtID.Text));
                            MySqlDataReader reader = command.ExecuteReader();
                            if (reader.Read())
                            {
                                txtTexto.Text = reader["texto"].ToString();
                                try
                                {
                                    string imagen = await CargarImagenDeGitHub(reader["imagen"].ToString());
                                    byte[] imagenBytes = Convert.FromBase64String(imagen);
                                    using (MemoryStream ms = new MemoryStream(imagenBytes))
                                    {
                                        Bitmap bitmap = new Bitmap(ms);
                                        pictureBox1.Image = bitmap;
                                        pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                                    }
                                }
                                catch
                                {

                                }
                            }
                            reader.Close();
                            MySqlCommand commandLikes = new MySqlCommand("SELECT COUNT(*) AS cantidadLikes FROM DaLike WHERE idPost=@id", conn);
                            commandLikes.Parameters.AddWithValue("@id", id);
                            MySqlDataReader readerLikes = commandLikes.ExecuteReader();
                            if (readerLikes.Read())
                            {
                                lblLikesDePost.Text = readerLikes["cantidadLikes"].ToString();
                            }
                            readerLikes.Close();
                            conn.Close();
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

        //Activar y Desactivar comentarios
        private void btnComentarios_Click(object sender, EventArgs e)
        {
            if (btnComentarios.Text == "Activar")
            {
                btnComentarios.Text = "Desactivar";
            }
            else
            {
                btnComentarios.Text = "Activar";
            }
        }
        private Boolean EstadoComentarios()
        {
            Boolean estadoComentarios = false;
            if (btnComentarios.Text == "Desactivar")
            {
                estadoComentarios = true;
            }
            return estadoComentarios;
        }

        //Modifico la fila seleccionada en el Datagrid
        private void btnModificar_Click(object sender, EventArgs e)
        {
            try
            {
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
                selectedRow.Cells[4].Value = EstadoComentarios();
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
                string id = txtID.Text;
                dataGridView1.Rows.Remove(filaSeleccionada);
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
                //Me fijo en todas las filas del DataGridView por información para después modificarla en la base de datos
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.IsNewRow || !row.Cells[0].Value.ToString().All(char.IsDigit))
                    {
                        continue;
                    }
                    int id = Convert.ToInt32(row.Cells["idPost"].Value);
                    bool comentarios = Convert.ToBoolean(row.Cells["comentarios"].Value);
                    string query = "INSERT INTO Posts (idPost, comentarios) " + "VALUES (@id, @comentarios) " + "ON DUPLICATE KEY UPDATE " + "comentarios=@comentarios";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@comentarios", comentarios);
                    cmd.ExecuteNonQuery();
                }
                //Para remover de la base de datos los posts eliminados
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
