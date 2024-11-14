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
            dataGridView1.CellMouseDown += dataGridView1_CellMouseDown;
            dataGridView1.MouseClick += dataGridView1_MouseClick;
            dataGridView1.SelectionChanged += dataGridView1_SelectionChanged;
            dataGridView1.ClearSelection();
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

        //Para limitar la escritura de txtID a solo numeros
        private void txtID_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                MessageBox.Show("Solo se permiten números", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
            }
        }

        //Cargar tabla
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
            columnHeaderStyle.Font = new Font("Verdana", 10, FontStyle.Bold);
            dataGridView1.ColumnHeadersDefaultCellStyle = columnHeaderStyle;        
            dataGridView1.BorderStyle = BorderStyle.None;
            dataGridView1.EnableHeadersVisualStyles = false;         
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells;
            dataGridView1.Columns["cantidadLikes"].Width = 70;
            dataGridView1.Columns["cantidadLikes"].HeaderText = "likes";
            dataGridView1.Columns["idPost"].Width = 51;
            dataGridView1.Columns["idPost"].HeaderText = "id";
            dataGridView1.Columns["texto"].Width = 191;
            dataGridView1.Columns["categoria"].Width = 93;
            dataGridView1.Columns["video"].Width = 85;
            dataGridView1.Columns["comentarios"].Width = 75;
            dataGridView1.Columns["comentarios"].HeaderText = "Coment";
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
                bool encontrado = await CargarDatosPost(id);

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
                            try
                            {
                                string imagenUrl = reader["imagen"].ToString();
                                await CargarYMostrarImagen(imagenUrl);
                            }
                            catch (Exception)
                            {
                                pictureBox1.Hide();
                            }
                            Controls.OfType<Control>().ToList().ForEach(c => c.Visible = true);
                            if (Convert.ToBoolean(reader["comentarios"]))
                            {
                                btnComentarios.Text = "Desactivar";
                            }
                            else
                            {
                                btnComentarios.Text = "Activar";
                            }
                            encontrado = true;
                        }
                    }
                }

                // Consulta de likes
                using (var commandLikes = new MySqlCommand("SELECT COUNT(*) AS cantidadLikes FROM DaLike WHERE idPost=@id", conn))
                {
                    commandLikes.Parameters.AddWithValue("@id", id);
                    lblLikesDePost.Text = (await commandLikes.ExecuteScalarAsync()).ToString();
                }
            }
            return encontrado;
        }

        // Cargar imagen
        private async Task CargarYMostrarImagen(string urlImagen)
        {
            string imagenBase64 = await CargarImagen.CargarImagenDeGitHub(urlImagen);
            if (!string.IsNullOrEmpty(imagenBase64))
            {
                byte[] imagenBytes = Convert.FromBase64String(imagenBase64);
                using (MemoryStream ms = new MemoryStream(imagenBytes))
                {
                    Bitmap bitmap = new Bitmap(ms);
                    pictureBox1.Image = bitmap;
                    pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                    pictureBox1.Show();
                }
            }
            else
            {
                pictureBox1.Hide();
            }
        }

        //Activar y Desactivar comentarios
        private void btnComentarios_Click(object sender, EventArgs e)
        {
            //Comprobar id
            if (string.IsNullOrEmpty(txtID.Text))
            {
                MessageBox.Show("Por favor, ingrese un ID.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            bool comentarios;
            //Modificar boton
            if (btnComentarios.Text == "Activar")
            {
                btnComentarios.Text = "Desactivar";
                comentarios = true;
            }
            else
            {
                btnComentarios.Text = "Activar";
                comentarios = false;
            }
         
            //Aplicar cambio
            try
            {
                conn.Open();
                // Verificar si el ID existe en la base de datos
                string checkQuery = "SELECT COUNT(*) FROM Posts WHERE idPost = @idPost;";
                MySqlCommand checkCmd = new MySqlCommand(checkQuery, conn);
                checkCmd.Parameters.AddWithValue("@idPost", txtID.Text);
                int count = Convert.ToInt32(checkCmd.ExecuteScalar());

                if (count > 0)
                {

                    // Actualizar la base de datos
                    string updateQuery = "UPDATE Posts SET comentarios = @comentarios WHERE idPost = @idPost;";
                    MySqlCommand updateCmd = new MySqlCommand(updateQuery, conn);
                    updateCmd.Parameters.AddWithValue("@comentarios", comentarios);
                    updateCmd.Parameters.AddWithValue("@idPost", txtID.Text);
                    updateCmd.ExecuteNonQuery();
                    MessageBox.Show("Informacion guardada con éxito.");
                    CargarTabla();
                    //Log
                    string folderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads", "Backoffice_CGHV_Log");
                    Directory.CreateDirectory(folderPath);
                    string path = Path.Combine(folderPath, "Log.txt");
                    string mensaje = $"{DateTime.Now}: {Principal.admin} ha desactivado los comentarios del post {txtID.Text}";
                    if (comentarios == true)
                    {
                        mensaje = $"{DateTime.Now}: {Principal.admin} ha reactivado los comentarios del post {txtID.Text}";
                    }
                    using (StreamWriter writer = new StreamWriter(path, true))
                    {
                        writer.WriteLine(mensaje);
                    }

                }
                else
                {
                    MessageBox.Show("No se encontro el post especificado.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Ocurrio un error: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }


        private void btnEliminar_Click(object sender, EventArgs e)
        {           
            try
            {
                string id = txtID.Text;
                EliminarPost(txtID.Text);
                CargarTabla();
            }
            catch (Exception)
            {
                MessageBox.Show("Ocurrio un error", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        private void EliminarPost(string id)
        {
            MySqlConnection eliminar = new MySqlConnection("server=localhost; database=infini; uid=root;");
            eliminar.Open();
            MySqlCommand command8 = new MySqlCommand("DELETE FROM reportes WHERE idComentario IN (SELECT id FROM Comentarios WHERE idPost = @Id)", eliminar);
            MySqlCommand command9 = new MySqlCommand("DELETE FROM reportes WHERE idPost = @Id", eliminar);
            command8.Parameters.AddWithValue("@Id", id);
            command9.Parameters.AddWithValue("@Id", id);
            command8.ExecuteNonQuery();
            command9.ExecuteNonQuery();
            MySqlCommand command = new MySqlCommand("DELETE FROM Comentarios WHERE idPost=@Id", eliminar);
            MySqlCommand command2 = new MySqlCommand("DELETE FROM DaLike WHERE idPost=@Id", eliminar);
            MySqlCommand command3 = new MySqlCommand("DELETE FROM PostPublico WHERE idPost=@Id", eliminar);
            MySqlCommand command4 = new MySqlCommand("DELETE FROM PostGrupo WHERE idPost=@Id", eliminar);
            MySqlCommand command5 = new MySqlCommand("DELETE FROM PostEvento WHERE idPost=@Id", eliminar);
            MySqlCommand command6 = new MySqlCommand("DELETE FROM Posts WHERE idPost=@Id", eliminar);
            MySqlCommand command7 = new MySqlCommand("DELETE FROM DaLikeComentario WHERE idComentario IN (SELECT id FROM Comentarios WHERE idPost=@Id)", eliminar);
            command.Parameters.AddWithValue("@Id", id);
            command2.Parameters.AddWithValue("@Id", id);
            command3.Parameters.AddWithValue("@Id", id);
            command4.Parameters.AddWithValue("@Id", id);
            command5.Parameters.AddWithValue("@Id", id);
            command6.Parameters.AddWithValue("@Id", id);
            command7.Parameters.AddWithValue("@Id", id);
            command.ExecuteNonQuery();
            command2.ExecuteNonQuery();
            command3.ExecuteNonQuery();
            command4.ExecuteNonQuery();
            command5.ExecuteNonQuery();
            command6.ExecuteNonQuery();
            command7.ExecuteNonQuery();
            eliminar.Close();
            MessageBox.Show("Post eliminado con exito.");
            // Log
            string folderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads", "Backoffice_CGHV_Log");
            Directory.CreateDirectory(folderPath);
            string path = Path.Combine(folderPath, "Log.txt");
            string mensaje = $"{DateTime.Now}: {Principal.admin} ha eliminado el post de id: {id}";

            using (StreamWriter writer = new StreamWriter(path, true))
            {
                writer.WriteLine(mensaje);
            }
        }
    }
}
