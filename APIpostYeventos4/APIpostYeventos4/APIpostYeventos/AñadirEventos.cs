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
    public partial class AñadirEvento : Form
    {
        public AñadirEvento()
        {
            InitializeComponent();
            dtpFecha.MinDate = DateTime.Today;
        }

        private void btnSeleccionarFecha_Click(object sender, EventArgs e)
        {
            lblFechaHora.Text = dtpFecha.Text + " " + dtpHora.Text;
        }

        private void btnSeleccionar_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                pbxImagen.ImageLocation = ofd.FileName;
                pbxImagen.SizeMode = PictureBoxSizeMode.StretchImage;
            }
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            Form1 f1 = new Form1();
            f1.Show();
            this.Close();
        }

        private void btnPublicar_Click(object sender, EventArgs e)
        {
            string[] fecha = dtpFecha.Text.Split('/');
            string[] hora = dtpHora.Text.Split(':');
            DateTime datetime = new DateTime(int.Parse(fecha[2]),int.Parse(fecha[1]),int.Parse(fecha[0]),int.Parse(hora[0]),int.Parse(hora[1]),0);
            
            if (string.IsNullOrEmpty(txtTitulo.Text) || int.TryParse(txtTitulo.Text, out int numero) || string.IsNullOrEmpty(txtUbicacion.Text) || int.TryParse(txtUbicacion.Text, out int numero2) || string.IsNullOrEmpty(txtDescripcion.Text) || int.TryParse(txtDescripcion.Text, out int numero3) || pbxImagen.Image == null || datetime<=DateTime.Now )
            {
                lblError.Text = "Debe Rellenar todos los campos";
                lblError.Show();
            }
            else
            {
                string fechayhora = dtpFecha.Text + " " + dtpHora.Text;
                MemoryStream ms = new MemoryStream();
                pbxImagen.Image.Save(ms, ImageFormat.Jpeg);
                byte[] data = ms.ToArray();
                Publicar(txtTitulo.Text, txtUbicacion.Text, txtDescripcion.Text, data, fechayhora);
                lblError.Text = "El evento se creó correctamente";
                lblError.Show();
            }
        }
        static async Task Publicar(string titulo, string ubicacion, string descripcion, byte[] imagen, string fechayhora)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var datos = new { titulo = titulo, ubicacion = ubicacion, descripcion = descripcion  ,imagen = Convert.ToBase64String(imagen), fechayhora = fechayhora};
                    var content = new StringContent(JsonConvert.SerializeObject(datos), Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PostAsync("https://localhost:44340/hacerEvento", content);
                    response.EnsureSuccessStatusCode();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                    Console.ReadLine();
                }
            }
        }

        private void pbxImagen_Click(object sender, EventArgs e)
        {

        }
    }
}
