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

namespace BackofficeDeAdministracion
{
    public partial class Editar_post : Form
    {
        static MySqlConnection conn = new MySqlConnection("Server=localhost; database=base; uID=root; pwd=;");
        public Editar_post()
        {
            InitializeComponent();        
            cargarTabla();
            inicializarTablaPosts();
            this.ActiveControl = txtID;
        }

        //Cargar tabla
        private void inicializarTablaPosts()
        {
            DataGridViewCellStyle columnHeaderStyle = new DataGridViewCellStyle();
            columnHeaderStyle.BackColor = Color.Beige;
            columnHeaderStyle.Font = new Font("Verdana", 10, FontStyle.Bold);
            dataGridView1.ColumnHeadersDefaultCellStyle = columnHeaderStyle;
            dataGridView1.Columns["id"].Width = 45;
            dataGridView1.Columns["likes"].Width = 65;
            dataGridView1.Columns["comentarios"].HeaderText = "coment";
            dataGridView1.Columns["comentarios"].Width = 75;
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells;
        }
        private void cargarTabla()
        {
            string connectionString = "server = localhost; database = base; uid = root; ";
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT id, texto, url, categorias, comentarios, likes FROM posts";
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

        //Buscar post
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
                            MySqlCommand command = new MySqlCommand("SELECT texto, imagen, url, categorias, likes FROM posts WHERE id=@id", conn);
                            command.Parameters.AddWithValue("@id", int.Parse(txtID.Text));
                            MySqlDataReader reader = command.ExecuteReader();
                            if (reader.Read())
                            {
                                txtTexto.Text = reader["texto"].ToString();
                                try
                                {
                                    lblLikesDeComentario.Text = reader["likes"].ToString();
                                    MemoryStream ms = new MemoryStream((byte[])reader["imagen"]);
                                    Bitmap bitmap = new Bitmap(ms);
                                    pictureBox1.Image = bitmap;
                                    pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                                }
                                catch
                                {

                                }
                                txtURL.Text = reader["url"].ToString();
                                txtCategorias.Text = reader["categorias"].ToString();
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
        private Boolean estadoComentarios()
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
                selectedRow.Cells[1].Value = txtTexto.Text;
                selectedRow.Cells[2].Value = txtURL.Text;
                selectedRow.Cells[3].Value = txtCategorias.Text;
                selectedRow.Cells[4].Value = estadoComentarios();
                selectedRow.Cells[5].Value = Int32.Parse(lblLikesDeComentario.Text);
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
                    int id = Convert.ToInt32(row.Cells["id"].Value);
                    string texto = row.Cells["texto"].Value.ToString();
                    string url = row.Cells["url"].Value.ToString();
                    string categorias = row.Cells["categorias"].Value.ToString();
                    bool comentarios = Convert.ToBoolean(row.Cells["comentarios"].Value);
                    int likes = Convert.ToInt32(row.Cells["likes"].Value);
                    string query = "INSERT INTO posts (id, texto, url, categorias, comentarios, likes) " + "VALUES (@id, @texto, @url, @categorias, @comentarios, @likes) " + "ON DUPLICATE KEY UPDATE " + "texto=@texto, url=@url, categorias=@categorias, comentarios=@comentarios, likes=@likes";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@texto", texto);
                    cmd.Parameters.AddWithValue("@url", url);
                    cmd.Parameters.AddWithValue("@categorias", categorias);
                    cmd.Parameters.AddWithValue("@comentarios", comentarios);
                    cmd.Parameters.AddWithValue("@likes", likes);
                    cmd.ExecuteNonQuery();
                }
                //Para remover de la base de datos los posts eliminados
                foreach (string id in eliminarDatos)
                {
                    string query = "DELETE FROM posts WHERE id = @id";
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
       
        //para evitar likes negativos
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

        //Restar Likes fijo
        private void btnRestar1Like_Click(object sender, EventArgs e)
        {
            int textoOriginal = int.Parse(lblLikesDeComentario.Text);
            int textoNuevo = textoOriginal - 1;
            VerificarMinimo(textoNuevo);       
        }
        private void btnRestar5Likes_Click_1(object sender, EventArgs e)
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
        private void txtLikesPersonalizados_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                MessageBox.Show("Solo se permiten números", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
            }
        }
        private void txtID_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                MessageBox.Show("Solo se permiten números", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
            }
        }
        private void btnVolver_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("¿desea salir del programa? Los datos no guardados se perderán", "Confirmar salida", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Close();
            }
        }




        //testing 
        public string modificarPost(string id, string link = "", string text = "", string image = "")
        {
            try
            {
                MySqlConnection conn = new MySqlConnection("Server=localhost; database=base; uID=root; pwd=;");
                conn.Open();
                MySqlCommand cmd;
                if (!string.IsNullOrEmpty(text))
                {
                    string texto = text;
                    if (!string.IsNullOrEmpty(link))
                    {
                        string url = link;
                        cmd = new MySqlCommand("UPDATE posts SET texto=@texto,url=@url WHERE id=@id", conn);
                        cmd.Parameters.AddWithValue("@Texto", texto);
                        cmd.Parameters.AddWithValue("@url", link);
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.ExecuteNonQuery();
                        conn.Close();
                        return "Modificacion correcta";
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(image))
                        {
                            byte[] imagen = Convert.FromBase64String(image);
                            cmd = new MySqlCommand("UPDATE posts SET texto=@texto,imagen=@imagen WHERE id=@id", conn);
                            cmd.Parameters.AddWithValue("@Texto", texto);
                            cmd.Parameters.AddWithValue("@Imagen", imagen);
                            cmd.Parameters.AddWithValue("@id", id);
                            cmd.ExecuteNonQuery();
                            conn.Close();
                            return "Modificacion correcta";
                        }
                        else
                        {
                            cmd = new MySqlCommand("UPDATE posts SET texto=@texto WHERE id=@id", conn);
                            cmd.Parameters.AddWithValue("@Texto", texto);
                            cmd.Parameters.AddWithValue("@id", id);
                            cmd.ExecuteNonQuery();
                            conn.Close();
                            return "Modificacion correcta";
                        }
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(image))
                    {
                        byte[] imagen = Convert.FromBase64String(image);
                        cmd = new MySqlCommand("UPDATE posts SET imagen=@imagen WHERE id=@id", conn);
                        cmd.Parameters.AddWithValue("@Imagen", imagen);
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.ExecuteNonQuery();
                        conn.Close();
                        return "Modificacion correcta";
                    }
                    else
                    {
                        string url = link;
                        cmd = new MySqlCommand("UPDATE posts SET url=@url WHERE id=@id", conn);
                        cmd.Parameters.AddWithValue("@url", url);
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.ExecuteNonQuery();
                        conn.Close();
                        return "Modificacion correcta";
                    }
                }
            }
            catch (Exception ex)
            {
                return "Modificación incorrecta";
            }
        }
        public string ultimoPost()
        {
            MySqlConnection conn = new MySqlConnection("Server=localhost; database=base; uID=root; pwd=;");
            conn.Open();
            MySqlCommand command = new MySqlCommand("SELECT id FROM posts ORDER BY id DESC LIMIT 1", conn);
            MySqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                string id = reader["id"].ToString();
                return id;
            }
            else
            {
                return null;
            }
        }

        public string conseguirPost(int id)
        {
            try
            {
                MySqlConnection conn = new MySqlConnection("Server=localhost; database=base; uID=root; pwd=;");
                conn.Open();
                MySqlCommand command = new MySqlCommand("SELECT texto,imagen,url FROM posts WHERE id=@Id", conn);
                command.Parameters.AddWithValue("@Id", id);
                MySqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    string texto;
                    if (string.IsNullOrEmpty(reader["texto"].ToString()))
                    {
                        texto = "";
                    }
                    else
                    {
                        texto = reader["texto"].ToString();
                    }
                    string imagen;
                    if (string.IsNullOrEmpty(reader["imagen"].ToString()))
                    {
                        imagen = "";
                    }
                    else
                    {
                        imagen = reader["imagen"].ToString();
                    }
                    string url;
                    if (string.IsNullOrEmpty(reader["url"].ToString()))
                    {
                        url = "";
                    }
                    else
                    {
                        url = reader["url"].ToString();
                    }
                    var data = new { imagen = imagen, url = url, texto = texto };
                    return texto;
                }
                else
                {
                    return "no se encuentra";
                }
            }
            catch (Exception ex)
            {
                return "no se encuentra";
            }
        }
    }  
}
