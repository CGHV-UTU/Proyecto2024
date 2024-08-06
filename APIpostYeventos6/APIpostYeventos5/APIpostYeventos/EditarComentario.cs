using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace APIpostYeventos
{
    public partial class EditarComentario : Form
    {
        private string usuario;
        public EditarComentario(string user)
        {
            InitializeComponent();
            usuario = user;
        }
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Form1 f1 = new Form1(usuario);
            f1.Show();
            this.Close();
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            Form1 f1 = new Form1(usuario);
            f1.Show();
            this.Close();
        }
        private async void CargarTabla()
        {
            dataGridView1.DataSource = await CargarTodosLosComentarios(txtID.Text);
        }
        private void ModificarTabla()
        {
            DataGridViewCellStyle columnHeaderStyle = new DataGridViewCellStyle();
            columnHeaderStyle.BackColor = Color.Beige;
            columnHeaderStyle.Font = new Font("Verdana", 10, FontStyle.Bold);
            dataGridView1.ColumnHeadersDefaultCellStyle = columnHeaderStyle;
        }

        private async void btnBuscar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtID.Text) || !int.TryParse(txtID.Text, out int numero))
            {
                lblErrorID.Show();
                lblErrorID.Text = "Debe ingresar una ID válida";
            }
            else
            {
                var respuesta = await Existe(int.Parse(txtID.Text));
                if (respuesta)
                {
                    var data = await Buscar(int.Parse(txtID.Text));
                    txtTexto.Text = data[0];
                    txtUrl.Text = data[1];
                    if (data[2].Length > 0)
                    {
                        byte[] imagen = Convert.FromBase64String(data[2]);
                        MemoryStream ms = new MemoryStream(imagen);
                        Bitmap bitmap = new Bitmap(ms);
                        pictureBox1.Image = bitmap;
                        pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                    }
                    else
                    {
                        pictureBox1.Image = null;
                    }
                    lblTexto.Show();
                    lblFoto.Show();
                    lblVideo.Show();
                    txtTexto.Show();
                    txtUrl.Show();
                    lblID.Hide();
                    lblErrorID.Hide();
                    txtID.Hide();
                    btnBuscar.Hide();
                    btnVolver.Hide();
                    btnCancelar.Show();
                    dataGridView1.Show();
                    btnEditar.Show();
                    txtIdComentario.Show();
                    lblComentario.Show();
                    CargarTabla();
                    ModificarTabla();
                }
                else
                {
                    lblErrorID.Show();
                    lblErrorID.Text = "No se encontró la ID";
                }
            }
        }
        private async void btnEditar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtIdComentario.Text) || !int.TryParse(txtIdComentario.Text, out int numero))
            {
                lblError2.Show();
                lblError2.Text = "Debe ingresar una ID válida";
            }
            else
            {
                var respuesta = await ExisteComentario(int.Parse(txtIdComentario.Text));
                if (respuesta)
                {
                    var data = await BuscarComentario(int.Parse(txtIdComentario.Text));
                    lblCuenta.Text = data[0];
                    txtComentario.Text = data[1];
                    lblFechayhora.Text = data[2];
                    lblidComentario.Text = txtIdComentario.Text;
                    label2.Show();
                    lblCuenta.Show();
                    lblFechayhora.Show();
                    txtComentario.Show();
                    label1.Show();
                    label3.Show();
                    btnConfirmar.Show();
                    lblidComentario.Show();
                }
                else
                {
                    lblError2.Show();
                    lblError2.Text = "No se encontró la ID";
                }
            }
        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtComentario.Text))
            {
                lblError3.Show();
                lblError3.Text = "Debe ingresar valores en el comentario.";
            }
            else
            {
                lblError3.Text = "Modificación correcta";
                lblError3.Show();
                Modificar(lblidComentario.Text,txtComentario.Text);
            }
            CargarTabla();
            ModificarTabla();
        }

        static async Task<string[]> Buscar(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync($"https://localhost:44340/postPorId?id={id}");
                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();
                    dynamic data = JsonConvert.DeserializeObject(responseBody);
                    return new string[] { data.texto, data.url, data.imagen };
                }
                catch
                {
                    return null;
                }
            }
        }
        static async Task<string[]> BuscarComentario(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync($"https://localhost:44340/conseguirComentario?id={id}");
                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();
                    dynamic data = JsonConvert.DeserializeObject(responseBody);
                    return new string[] { data.NombreDeCuenta, data.texto, data.fechayhora };
                }
                catch
                {
                    return null;
                }
            }
        }
        static async Task<bool> Existe(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync($"https://localhost:44340/existePost?id={id}");
                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();
                    dynamic data = JsonConvert.DeserializeObject<bool>(responseBody);
                    return data;
                }
                catch
                {
                    return false;
                }
            }
        }
        static async Task<dynamic> CargarTodosLosComentarios(string id)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync($"https://localhost:44340/seleccionarTodosLosComentarios?id={id}");
                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();
                    DataTable tabla = JsonConvert.DeserializeObject<DataTable>(responseBody);
                    return tabla;
                }
                catch
                {
                    return "No se pudo cargar la tabla";
                }
            }
        }
        static async Task<bool> ExisteComentario(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync($"https://localhost:44340/existeComentario?id={id}");
                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();
                    dynamic data = JsonConvert.DeserializeObject<bool>(responseBody);
                    return data;
                }
                catch
                {
                    return false;
                }
            }
        }
        static async Task Modificar(string id ,string texto)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var data = new { id = id,texto = texto,};
                    var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PutAsync("https://localhost:44340/modificarComentario", content);
                    response.EnsureSuccessStatusCode();
                }
                catch (Exception)
                {

                }
            }
        }

        //testing
        public dynamic modificarComentario(string id = "" ,string texto = "")
        {
            try
            {
                MySqlConnection conn = new MySqlConnection("Server=localhost; database=base; uID=root; pwd=;");
                conn.Open();
                MySqlCommand cmd;
                if (!string.IsNullOrEmpty(texto))
                {
                    cmd = new MySqlCommand("UPDATE comentarios SET texto=@Texto WHERE id=@id", conn);
                    cmd.Parameters.AddWithValue("@Texto", texto);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    return "Modificacion correcta";
                }
                else
                {
                    return "Modificacion incorrecta";
                }
            }
            catch (Exception)
            {
                return "no se encuentra";
            }
        }
        public dynamic ultimoComentario()
        {
            try
            {
                MySqlConnection conn = new MySqlConnection("Server=localhost; database=base; uID=root; pwd=;");
                conn.Open();
                MySqlCommand command = new MySqlCommand("SELECT id FROM comentarios ORDER BY id DESC LIMIT 1", conn);
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
            catch
            {
                return null;
            }
        }
    }
}
