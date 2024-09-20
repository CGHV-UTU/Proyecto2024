using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace APIpostYeventos
{
    public partial class EditarEvento : Form
    {
        private string usuario;
        public EditarEvento(string user)
        {
            InitializeComponent();
            CargarTabla();
            ModificarTabla();
            dtpFecha.MinDate = DateTime.Today;
            dtpFecha2.MinDate = DateTime.Today.AddDays(1);
            int año = DateTime.Now.Year;
            dtpFecha.MaxDate = new DateTime(año,12,31);
            dtpFecha2.MaxDate = new DateTime(año, 12, 31);

            int hora = DateTime.Now.Hour;
            int minuto = DateTime.Now.Minute + 5;
            if (minuto >= 60)
            {
                minuto -= 60;
                hora += 1;
            }
            if (hora >= 24)
            {
                hora = 0;
            }
            dtpHora.MinDate = new DateTime(año, 12, 31, hora, minuto, 0);
            dtpHora2.MinDate = new DateTime(año, 12, 31, hora, minuto, 0);
            usuario = user;
        }
        private void dtpFecha_ValueChanged(object sender, EventArgs e)
        {
            dtpFecha2.MinDate = dtpFecha.Value.AddDays(1);
        }
        private async void CargarTabla()
        {
            dataGridView1.DataSource = await CargarTodosLosEventos();
        }

        private void btnSeleccionar_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.ImageLocation = ofd.FileName;
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            }
        }
        private void ModificarTabla()
        {          
            DataGridViewCellStyle columnHeaderStyle = new DataGridViewCellStyle();
            columnHeaderStyle.BackColor = Color.Beige;
            columnHeaderStyle.Font = new Font("Verdana", 10, FontStyle.Bold);
            dataGridView1.ColumnHeadersDefaultCellStyle = columnHeaderStyle;          
        }
        private void btnVolver_Click(object sender, EventArgs e)
        {
            Form1 f1 = new Form1(usuario);
            f1.Show();
            this.Close();
        }
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Form1 f1 = new Form1(usuario);
            f1.Show();
            this.Close();
        }
        private void btnSeleccionarFecha_Click(object sender, EventArgs e)
        {
            lblFechaHora.Text = dtpFecha.Text + " " + dtpHora.Text;
        }
        private void btnSeleccionarFecha2_Click(object sender, EventArgs e)
        {
            lblFechaHora2.Text = dtpFecha2.Text + " " + dtpHora2.Text;
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
                if(respuesta)
                {
                    var data = await Buscar(int.Parse(txtID.Text));
                    txtTitulo.Text = data[0];
                    txtUbicacion.Text = data[1];
                    txtDescripcion.Text = data[2];
                    if (data[3].Length > 0)
                    {
                        byte[] imagen = Convert.FromBase64String(data[3]);
                        MemoryStream ms = new MemoryStream(imagen);
                        Bitmap bitmap = new Bitmap(ms);
                        pictureBox1.Image = bitmap;
                        pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                    }
                    else
                    {
                        pictureBox1.Image = null;
                    }
                    lblFechayHora.Text = "Fecha y hora previa del evento: " + data[4];
                    lblErrorID.Hide();
                    lblTitulo.Show();
                    lblUbicacion.Show();
                    lblDescripcion.Show();
                    lblFoto.Show();
                    lblFechayHora.Show();
                    txtTitulo.Show();
                    txtDescripcion.Show();
                    txtUbicacion.Show();
                    btnSeleccionar.Show();
                    btnModificar.Show();
                    btnCancelar.Show();
                    label1.Hide();
                    txtID.Hide();
                    btnBuscar.Hide();
                    btnVolver.Hide();
                }
                else
                {
                    lblErrorID.Show();
                    lblErrorID.Text = "No se encontró la ID";
                }
                
            }
        }

        private async void btnModificar_Click(object sender, EventArgs e)
        {
            var data = await Buscar(int.Parse(txtID.Text));
            string[] fecha = dtpFecha.Text.Split('/');
            string[] hora = dtpHora.Text.Split(':');
            DateTime datetime = new DateTime(int.Parse(fecha[2]), int.Parse(fecha[1]), int.Parse(fecha[0]), int.Parse(hora[0]), int.Parse(hora[1]), 0);
            string fechayhora = datetime.ToString("yyyy-MM-dd HH:mm:ss");
            DateTime datetime2 = dtpFecha2.Value.Date + dtpHora2.Value.TimeOfDay;
            string fechayhora2 = datetime2.ToString("yyyy-MM-dd HH:mm:ss");
            if (txtTitulo.Text == data[0] && txtUbicacion.Text == data[1] && txtDescripcion.Text == data[2] && fechayhora == data[4])
            {
                lblErrorModificar.Show();
                lblErrorModificar.Text = "Debe modificar al menos un valor";
            }
            else
            {
                if (string.IsNullOrEmpty(txtID.Text) || string.IsNullOrEmpty(txtTitulo.Text) || pictureBox1.Image==null || string.IsNullOrEmpty(fechayhora))
                {
                    lblErrorModificar.Show();
                    lblErrorModificar.Text = "Eliminó uno de los valores obligatorios";
                }
                else
                {
                    lblErrorModificar.Show();
                    lblErrorModificar.Text = "Se modificó correctamente";                   
                    MemoryStream ms = new MemoryStream();
                    pictureBox1.Image.Save(ms, ImageFormat.Jpeg);
                    byte[] imagen = ms.ToArray();
                    await Modificar(txtID.Text, txtTitulo.Text, txtUbicacion.Text, txtDescripcion.Text, imagen, fechayhora, fechayhora2);
                    lblFechayHora.Text = "Fecha y hora previa del evento: " + fechayhora;
                }
                CargarTabla();
            }
        }

        static async Task<string[]> Buscar(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var datos = new { id = id };
                    var content = new StringContent(JsonConvert.SerializeObject(datos), Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PutAsync($"https://localhost:44340/eventoPorId", content);
                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();
                    dynamic data = JsonConvert.DeserializeObject(responseBody); 
                    return new string[] { data.titulo, data.ubicacion, data.descripcion, data.foto, data.fechayhora };
                }
                catch
                {
                    return null;
                }
            }
        }
        static async Task Modificar(string id, string titulo, string ubicacion, string descripcion, byte[] imagen, string fechayhora, string fechayhora2)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var data = new { id = id, titulo = titulo, ubicacion = ubicacion, descripcion = descripcion, foto = Convert.ToBase64String(imagen), fechaYhora_Inicio = fechayhora, fechaYhora_Final = fechayhora2 };
                    var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PutAsync("https://localhost:44340/modificarEvento", content);
                    response.EnsureSuccessStatusCode();
                }
                catch (Exception)
                {

                }
            }
        }
        static async Task<dynamic> CargarTodosLosEventos()
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync($"https://localhost:44340/seleccionarTodosLosEventos");
                    response.EnsureSuccessStatusCode();
                    string ResponseBody = await response.Content.ReadAsStringAsync();
                    DataTable tabla = JsonConvert.DeserializeObject<DataTable>(ResponseBody);
                    return tabla;
                }
                catch
                {
                    return "No se pudo cargar la tabla";
                }
            }
        }
        static async Task<bool> Existe(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var datos = new { id = id };
                    var content = new StringContent(JsonConvert.SerializeObject(datos), Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PutAsync($"https://localhost:44340/existeEvento", content);
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
    }
}
