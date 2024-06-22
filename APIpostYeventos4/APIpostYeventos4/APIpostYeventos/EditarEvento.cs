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
        public EditarEvento()
        {
            InitializeComponent();
            dtpFecha.MinDate = DateTime.Today;
            int año = DateTime.Now.Year;
            dtpFecha.MaxDate = new DateTime(año,12,31);
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
                string fechayhora = dtpFecha.Text + " " + dtpHora.Text;
                var data = await Buscar(int.Parse(txtID.Text));
                txtTitulo.Text = data[0];
                txtUbicacion.Text = data[1];
                txtDescripcion.Text = data[2];
                byte[] imagen = Convert.FromBase64String(data[3]);
                lblFechayHora.Text = "Fecha y hora previa del evento: " + data[4];
                MemoryStream ms = new MemoryStream(imagen);
                Bitmap bitmap = new Bitmap(ms);
                pictureBox1.Image = bitmap;
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                lblErrorID.Hide();
                lblTitulo.Show();
                lblUbicacion.Show();
                lblDescripcion.Show();
                lblFoto.Show();
                lblFechayHora.Show();
                lblFecha.Show();
                lblHora.Show();
                txtTitulo.Show();
                txtDescripcion.Show();
                txtUbicacion.Show();
                btnSeleccionar.Show();
                btnModificar.Show();
                dtpFecha.Show();
                dtpHora.Show();
                label1.Hide();
                txtID.Hide();
                btnBuscar.Hide();
            }
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

        private async void btnModificar_Click(object sender, EventArgs e)
        {
            string fechayhora = dtpFecha.Text + " " + dtpHora.Text;
            var data = await Buscar(int.Parse(txtID.Text));
            if (txtTitulo.Text == data[0] && txtUbicacion.Text == data[1] && txtDescripcion.Text == data[2] && fechayhora == data[4])
            {
                lblErrorModificar.Show();
                lblErrorModificar.Text = "Debe modificar al menos un valor";
            }
            else
            {
                lblErrorModificar.Show();
                lblErrorModificar.Text = "Se modificó correctamente";
                MemoryStream ms = new MemoryStream();
                pictureBox1.Image.Save(ms, ImageFormat.Jpeg);
                byte[] imagen = ms.ToArray();
                Modificar(txtID.Text, txtTitulo.Text, txtUbicacion.Text, txtDescripcion.Text, imagen, fechayhora);
                lblFechayHora.Text = "Fecha y hora previa del evento: " + fechayhora;
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Form1 f1 = new Form1();
            f1.Show();
            this.Close();
        }

 
        static async Task<string[]> Buscar(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync($"https://localhost:44340/eventoPorId?id={id}");
                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();
                    dynamic data = JsonConvert.DeserializeObject(responseBody); //sigo sin poder pasar esto a lo que quiero, no me deja acceder a la info del json de ninguna manera, tengo que hallar alguna forma de pasar los datos
                    return new string[] { data.titulo, data.ubicacion, data.descripcion, data.foto, data.fechayhora };
                }
                catch
                {
                    return null;
                }
            }
        }

        static async Task Modificar(string id, string titulo, string ubicacion, string descripcion, byte[] imagen, string fechayhora)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var data = new { id = id, titulo = titulo, ubicacion = ubicacion, descripcion = descripcion, imagen = Convert.ToBase64String(imagen), fechayhora = fechayhora };
                    var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PutAsync("https://localhost:44340/modificarEvento", content);
                    response.EnsureSuccessStatusCode();
                }
                catch (Exception ex)
                {

                }
            }
        }
    }

}
