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
    public partial class ReporteGrupo : Form
    {
        static MySqlConnection conn = new MySqlConnection("Server=192.168.5.50; database=cghv; uID=federico.gonzalez; pwd=56983793;");
        public ReporteGrupo()
        {
            InitializeComponent();
            CargarTabla();
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

        //Cargar tabla      
        private void CargarTabla()
        {
            string connectionString = "Server=192.168.5.50; database=cghv; uID=federico.gonzalez; pwd=56983793;";
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT numeroDeReporte, nombreGrupo, tipo FROM Reportes WHERE nombreGrupo IS NOT NULL";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    dataGridView1.DataSource = dataTable;
                    EsteticaTabla();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message);
                }
            }
        }
        private void EsteticaTabla()
        {
            DataGridViewCellStyle columnHeaderStyle = new DataGridViewCellStyle();
            columnHeaderStyle.Font = new Font("Verdana", 10, FontStyle.Bold);
            dataGridView1.ColumnHeadersDefaultCellStyle = columnHeaderStyle;
            dataGridView1.Columns["numeroDeReporte"].Width = 80;
            dataGridView1.Columns["tipo"].Width = 150;
            dataGridView1.Columns["numeroDeReporte"].HeaderText = "Reporte";
            dataGridView1.Columns["nombreGrupo"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns["nombreGrupo"].HeaderText = "Grupo";
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells;
        }

        // Buscar grupo
        private async void btnBuscar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtID.Text))
            {
                MessageBox.Show("Debe ingresar un nombre de grupo", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            string nombre = txtID.Text;
            bool encontrado = await BuscarGrupo(nombre);

            if (!encontrado)
            {
                MessageBox.Show("No se encontró el grupo especificado.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        // Buscar grupo en la base de datos
        private async Task<bool> BuscarGrupo(string nombre)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection("Server=192.168.5.50; database=cghv; uID=federico.gonzalez; pwd=56983793;"))
                {
                    await conn.OpenAsync();
                    MySqlCommand command = new MySqlCommand("SELECT nombreReal, nombreVisible, foto, descripcion FROM Grupos WHERE nombreReal=@nombreReal", conn);
                    command.Parameters.AddWithValue("@nombreReal", nombre);

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (await reader.ReadAsync())
                        {
                            lblNombreDeGrupo.Text = reader["nombreReal"].ToString();
                            lblNombreVisible.Text = reader["nombreVisible"].ToString();
                            txtDescripcionDeGrupo.Text = reader["descripcion"].ToString();

                            try
                            {
                                string imagenUrl = reader["foto"].ToString();
                                await CargarYMostrarImagen(imagenUrl);
                            }
                            catch (Exception)
                            {
                                pictureBox1.Hide();
                            }

                            // Mostrar controles
                            Controls.OfType<Control>().ToList().ForEach(c => c.Visible = true);
                            return true; // Grupo encontrado
                        }
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error al buscar el grupo.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return false; // Grupo no encontrado
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

        //Eliiminar Grupo
        private void btnEliminar(object sender, EventArgs e)
        {
            try
            {
                string Nombre = lblNombreDeGrupo.Text;
                EliminarGrupo(Nombre);
                CargarTabla();
            }
            catch (Exception)
            {
                MessageBox.Show("No seleccionó una fila", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            }
        }

        //Eliminar informacion de la base de datos
        private void EliminarGrupo(string Nombre)
        {
            conn.Open();
            MySqlCommand cmd = new MySqlCommand("DELETE FROM Reportes WHERE nombreGrupo = @nombreReal;" +
                "DELETE FROM Participa WHERE nombreReal = @nombreReal;" +
                "DELETE FROM PostGrupo WHERE nombreReal = @nombreReal;" +
                "DELETE FROM Grupos WHERE nombreReal = @nombreReal", conn);
            cmd.Parameters.AddWithValue("@nombreReal", Nombre);
            cmd.ExecuteNonQuery();
            conn.Close();
            //Log
            MessageBox.Show("Información eliminada con éxito.");
            string path = @"C:\Users\emerg\Downloads\elbackoffice\Proyecto2024\Log.txt";
            string mensaje = $"{DateTime.Now}: {Principal.admin} ha eliminado el grupo {Nombre}";
            using (StreamWriter writer = new StreamWriter(path, true))
            {
                writer.WriteLine(mensaje);
            }
        }
    }
}
