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
    public partial class EliminarComentario : Form
    {
        public EliminarComentario()
        {
            InitializeComponent();
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
                    btnEliminar.Show();
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

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Form1 f1 = new Form1();
            f1.Show();
            this.Close();
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            Form1 f1 = new Form1();
            f1.Show();
            this.Close();
        }
        private async void btnEliminar_Click(object sender, EventArgs e)
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
                    Eliminar(txtIdComentario.Text);
                    lblError2.Show();
                    lblError2.Text = "El Comentario se eliminó correctamente";
                    CargarTabla();
                    ModificarTabla();
                }
                else
                {
                    lblError2.Show();
                    lblError2.Text = "No se encontró la ID";
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
        static async Task Eliminar(string id)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.DeleteAsync($"https://localhost:44340/eliminarComentario?id={id}");
                    response.EnsureSuccessStatusCode();
                }
                catch (Exception ex)
                {

                }
            }
        }
    }
}
