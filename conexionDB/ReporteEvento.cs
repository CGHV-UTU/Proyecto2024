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
    public partial class ReporteEvento : Form
    {
        static MySqlConnection conn = new MySqlConnection("Server=192.168.5.50; database=cghv; uID=federico.gonzalez; pwd=56983793;");
        public ReporteEvento()
        {
            InitializeComponent();
            CargarTabla();
            this.ActiveControl = txtID;
            dataGridView1.CellMouseDown += dataGridView1_CellMouseDown;
            dataGridView1.MouseClick += dataGridView1_MouseClick;
            dataGridView1.SelectionChanged += dataGridView1_SelectionChanged;
            dataGridView1.ClearSelection();
        }

        //Limitar la escritura de txtID a solo numeros
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

        // Conseguir imagenes de github
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

        //Cargar lista de Reportes
        private void CargarTabla()
        {
            // vuelvo a abrir una conexion porque la principal esta en uso y genera errores
            string connectionString = "Server=192.168.5.50; database=cghv; uID=federico.gonzalez; pwd=56983793;";
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT numeroDeReporte, idEvento, tipo, descripcion FROM Reportes WHERE idEvento IS NOT NULL";
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
            dataGridView1.Columns["numeroDeReporte"].Width = 70;
            dataGridView1.Columns["numeroDeReporte"].HeaderText = "Reporte";
            dataGridView1.Columns["descripcion"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells;
        }

        // Buscar Reporte y evento
        private async void btnBuscar_Click(object sender, EventArgs e)
        {

            //Comprobar que el campo para id no sea vacio
            if (string.IsNullOrEmpty(txtID.Text))
            {
                MessageBox.Show("Debe ingresar una id", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            //Buscar el evento
            int id = int.Parse(txtID.Text);
            bool encontrado = await CargarReporte(id);

            // Si no se encuentra da error
            if (!encontrado)
            {
                MessageBox.Show("No se encontró el evento especificado.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        private async Task<bool> CargarReporte(int id)
        {
            conn.Open();
            MySqlCommand command = new MySqlCommand("SELECT nombreDeCuenta, idEvento, tipo, descripcion FROM Reportes WHERE numeroDeReporte=@id", conn);
            command.Parameters.AddWithValue("@id", int.Parse(txtID.Text));
            MySqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                lblNombreDeCuenta.Text = reader["nombreDeCuenta"].ToString();
                txtDescripcionReporte.Text = reader["descripcion"].ToString();
                lblTipo.Text = reader["tipo"].ToString();
                lblIdPost.Text = reader["idEvento"].ToString();
                Controls.OfType<Control>().ToList().ForEach(c => c.Visible = true);
                bool post = await CargarDatosEvento(Convert.ToInt32(reader["idEvento"]));
                conn.Close();
                return true;
            }
            conn.Close();
            return false;
        }

        //Cargar un evento con su id
        private async Task<bool> CargarDatosEvento(int id)
        {
            try
            {
                using (var conn2 = new MySqlConnection("Server=192.168.5.50; database=cghv; uID=federico.gonzalez; pwd=56983793;"))
                {
                    await conn2.OpenAsync();
                    MySqlCommand command = new MySqlCommand("SELECT titulo, ubicacion, descripcion, foto, fechaYhora_Inicio, fechaYhora_Final FROM Eventos WHERE idEvento=@id", conn2);
                    command.Parameters.AddWithValue("@id", id);

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Asignar los datos conseguidos
                            txtTitulo.Text = reader["titulo"].ToString();
                            txtDescripcion.Text = reader["descripcion"].ToString();
                            txtUbicacion.Text = reader["ubicacion"].ToString();
                            lblInicio.Text = reader["fechaYhora_Inicio"].ToString();
                            lblFin.Text = reader["fechaYhora_Final"].ToString();

                            // Cargar y mostrar imagen
                            string imagen = await CargarImagenDeGitHub(reader["foto"].ToString());
                            pictureBox1.Image = !string.IsNullOrEmpty(imagen) ? ConvertirImagen(imagen) : null;
                            pictureBox1.Visible = !string.IsNullOrEmpty(imagen);

                            // Mostrar controles
                            Controls.OfType<Control>().ToList().ForEach(c => c.Visible = true);

                            return true; // Evento encontrado
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error al cargar el evento: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return false; // Evento no encontrado
        }

        // Convertir imagen
        private Bitmap ConvertirImagen(string imagenBase64)
        {
            byte[] imagenBytes = Convert.FromBase64String(imagenBase64);
            using (MemoryStream ms = new MemoryStream(imagenBytes))
            {
                return new Bitmap(ms);
            }
        }

        //Eliminar un evento
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtID.Text))
            {
                MessageBox.Show("Debe ingresar una id", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            try
            {
                string id = txtID.Text;
                EliminarEvento(id); //Enviar id del evento para eliminarlo de la base de datos
                CargarTabla();
                MessageBox.Show("Evento eliminado correctamente.", "Informacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error al eliminar el evento: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Eliminar informacion de la base de datos
        private void EliminarEvento(string id)
        {
            conn.Open();
            MySqlCommand command = new MySqlCommand("DELETE FROM PostEvento WHERE idEvento=@Id", conn);
            MySqlCommand command1 = new MySqlCommand("DELETE FROM Eventos WHERE idEvento=@Id", conn);
            MySqlCommand command2 = new MySqlCommand("DELETE FROM ParticipaEvento WHERE idEvento=@Id", conn);
            command2.Parameters.AddWithValue("@Id", int.Parse(id));
            command2.ExecuteNonQuery();
            command.Parameters.AddWithValue("@Id", int.Parse(id));
            command.ExecuteNonQuery();
            command1.Parameters.AddWithValue("@Id", int.Parse(id));
            command1.ExecuteNonQuery();
            conn.Close();
            //Log
            string folderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads", "Backoffice_CGHV_Log");
            Directory.CreateDirectory(folderPath);
            string path = Path.Combine(folderPath, "Log.txt");
            string mensaje = $"{DateTime.Now}: {Principal.admin} ha eliminado el evento de id por un reporte {id}";
            using (StreamWriter writer = new StreamWriter(path, true))
            {
                writer.WriteLine(mensaje);
            }
        }
    }
}
