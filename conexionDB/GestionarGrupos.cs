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
    public partial class GestionarGrupos : Form
    {
        static MySqlConnection conn = new MySqlConnection("Server=localhost; database=base; uID=root; pwd=;");
        public GestionarGrupos()
        {
            InitializeComponent();
            CargarTabla();
            InicializarTablaGrupos();
        }

        //Cargar tabla      
        private void CargarTabla()
        {
            string connectionString = "server = localhost; database = base; uid = root; ";
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT nombreReal, nombreVisible FROM grupos";
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

        private void btnBuscar_Click(object sender, EventArgs e)
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
                            MySqlCommand command = new MySqlCommand("SELECT nombreReal, nombreVisible, foto, descripcion FROM grupos WHERE nombreReal=@nombreReal", conn);
                            command.Parameters.AddWithValue("@nombreReal", nombre);
                            MySqlDataReader reader = command.ExecuteReader();
                            if (reader.Read())
                            {
                                lblNombreDeGrupo.Text = reader["nombreReal"].ToString();
                                lblNombreVisible.Text = reader["nombreVisible"].ToString();
                                lblDescripcionDeGrupo.Text = reader["descripcion"].ToString();

                                try
                                {
                                    MemoryStream ms = new MemoryStream((byte[])reader["foto"]);
                                    Bitmap bitmap = new Bitmap(ms);
                                    pictureBox1.Image = bitmap;
                                    pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                                }
                                catch
                                {

                                }
                                lblNombre.Show();
                                lblNombreVisible.Show();
                                lblNomVisible.Show();
                                lblDescripcionDeGrupo.Show();
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
                var filaSeleccionada = dataGridView1.SelectedRows[0];             
                dataGridView1.Rows.Remove(filaSeleccionada);
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
                        // Oculta el control
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
                MySqlCommand command = new MySqlCommand("DELETE FROM base.grupos WHERE nombreReal = @nombreReal", conn);
                command.Parameters.AddWithValue("@nombreReal", Nombre);                            
                command.ExecuteNonQuery();
            }
            eliminarDatos.Clear();
            conn.Close();
            MessageBox.Show("Información guardada con éxito");
            this.Close();
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
        public string EliminarGrupo(string nombre)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection("Server=localhost; database=base; uID=root; pwd=;"))
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("DELETE FROM grupos WHERE nombreReal=@nombreReal", conn);
                    cmd.Parameters.AddWithValue("@nombreReal", nombre);
                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        return "Grupo eliminado";
                    }
                    else
                    {
                        return "No se encontró el grupo a eliminar";
                    }
                }
            }
            catch (Exception)
            {
                return "Error al intentar eliminar grupo";
            }
        }
        public string UltimoGrupo()
        {
            MySqlConnection conn = new MySqlConnection("Server=localhost; database=base; uID=root; pwd=;");
            conn.Open();
            MySqlCommand command = new MySqlCommand("SELECT nombreReal FROM grupos ORDER BY nombreReal DESC LIMIT 1", conn);
            MySqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                string nombre = reader["nombreReal"].ToString();
                return nombre;
            }
            else
            {
                return null;
            }
        }
    }
}
