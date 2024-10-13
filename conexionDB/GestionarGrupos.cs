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
    public partial class GestionarGrupos : Form
    {
        static MySqlConnection conn = new MySqlConnection("Server=localhost; database=infini; uID=root; pwd=;");
        public GestionarGrupos()
        {
            InitializeComponent();
            CargarTabla();
            InicializarTablaGrupos();
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
                    string query = "SELECT nombreReal, nombreVisible FROM Grupos";
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
        private void InicializarTablaGrupos()
        {
            DataGridViewCellStyle columnHeaderStyle = new DataGridViewCellStyle();
            columnHeaderStyle.BackColor = Color.Beige;
            columnHeaderStyle.Font = new Font("Verdana", 10, FontStyle.Bold);
            dataGridView1.ColumnHeadersDefaultCellStyle = columnHeaderStyle;
            dataGridView1.Columns["nombreReal"].Width = 140;
            dataGridView1.Columns["nombreVisible"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns["nombreReal"].HeaderText = "Nombre";
            dataGridView1.Columns["nombreVisible"].HeaderText = "Nombre Visible";
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells;
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

        private async void btnBuscar_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtID.Text))
            {
                try
                {
                    Boolean encontrado = false;
                    string nombre = txtID.Text;
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        if (row.Cells[0].Value != null && row.Cells[0].Value.ToString() == nombre)
                        {
                            conn.Open();
                            MySqlCommand command = new MySqlCommand("SELECT nombreReal, nombreVisible, foto, descripcion FROM Grupos WHERE nombreReal=@nombreReal", conn);
                            command.Parameters.AddWithValue("@nombreReal", nombre);
                            MySqlDataReader reader = command.ExecuteReader();
                            if (reader.Read())
                            {
                                lblNombreDeGrupo.Text = reader["nombreReal"].ToString();
                                lblNombreVisible.Text = reader["nombreVisible"].ToString();
                                txtDescripcionDeGrupo.Text = reader["descripcion"].ToString();

                                try
                                {
                                    string imagen = await CargarImagenDeGitHub(reader["foto"].ToString());
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
                                lblNombre.Show();
                                lblNombreVisible.Show();
                                lblNomVisible.Show();
                                txtDescripcionDeGrupo.Show();
                                lblDesc.Show();
                                lblFoto.Show();
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
                        MessageBox.Show("No se encontró el grupo especificado.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("No se encontró el grupo especificado.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            else
            {
                MessageBox.Show("Debe ingresar un nombre de grupo", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }


        private void btnEliminar(object sender, EventArgs e)
        {

            if (lblNombreDeGrupo.Text == "")
            {
                MessageBox.Show("Debe seleccionar un grupo");
            }
            else
            {
                MessageBox.Show("Grupo eliminado correctamente.");
                string Nombre = lblNombreDeGrupo.Text;
                GuardarNombre(Nombre);
                lblNombreDeGrupo.Text = "";
                pictureBox1.Image = null;
                //Codigo Para Ocultar Todos los Label
                // Recorre todos los controles del formulario
                foreach (Control control in this.Controls)
                {
                    // Verifica si el control es de tipo Label y no es "lblNom"(El Principal Para Buscar grupos)
                    if (control is Label && control.Name != "lblNom")
                    {
                        control.Visible = false;
                    }
                }

            }
        }

        //Guardo la id de los eventos borrados del datagrid para luego eliminarlos definitivamente
        List<string> eliminarDatos = new List<string>();
        private void GuardarNombre(string Nombre)
        {
            eliminarDatos.Add(Nombre);
        }
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            conn.Open();
            foreach (string Nombre in eliminarDatos)
            {
                MySqlCommand cmd = new MySqlCommand("DELETE FROM Reportes WHERE nombreGrupo = @nombreReal;" +
                    "DELETE FROM Participa WHERE nombreReal = @nombreReal;" +
                    "DELETE FROM PostGrupo WHERE nombreReal = @nombreReal;" +
                    "DELETE FROM Grupos WHERE nombreReal = @nombreReal", conn);
                cmd.Parameters.AddWithValue("@nombreReal", Nombre);
                cmd.ExecuteNonQuery();
            }
            eliminarDatos.Clear();
            conn.Close();
            MessageBox.Show("Información guardada con éxito");
            this.Close();
        }
    }
}
