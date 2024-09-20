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
    public partial class AñadirEvento : Form
    {
        private static string usuario;
        public AñadirEvento(string user)
        {
            InitializeComponent();
            dtpFecha.MinDate = DateTime.Today;
            dtpFecha2.MinDate = DateTime.Today.AddDays(1);
            int año = DateTime.Now.Year;
            dtpFecha.MaxDate = new DateTime(año, 12, 31);
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
        // Fecha y hora. La fecha de fin de evento es siempre un dia mas que la de inicio
        private void dtpFecha_ValueChanged(object sender, EventArgs e)
        {
            dtpFecha2.MinDate = dtpFecha.Value.AddDays(1); 
        }
        private void btnSeleccionarFecha_Click(object sender, EventArgs e)
        {
            lblFechaHora.Text = dtpFecha.Text + " " + dtpHora.Text;
        }
        private void btnSeleccionarFecha2_Click(object sender, EventArgs e)
        {
            lblFechaHora2.Text = dtpFecha2.Text + " " + dtpHora2.Text;
        }

        // Agregar imagen
        private void btnSeleccionar_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                pbxImagen.ImageLocation = ofd.FileName;
                pbxImagen.SizeMode = PictureBoxSizeMode.StretchImage;
            }
        }

        //Cerrar Form
        private void btnVolver_Click(object sender, EventArgs e)
        {
            Form1 f1 = new Form1(usuario);
            f1.Show();
            this.Close();
        }

        //Publicar Evento
        private async void btnPublicar_Click(object sender, EventArgs e)
        {
            string[] fecha = dtpFecha.Text.Split('/');
            string[] hora = dtpHora.Text.Split(':');
            DateTime datetime = new DateTime(int.Parse(fecha[2]),int.Parse(fecha[1]),int.Parse(fecha[0]),int.Parse(hora[0]),int.Parse(hora[1]),0);
            
            if (string.IsNullOrEmpty(txtTitulo.Text) || pbxImagen.Image == null || datetime<=DateTime.Now )
            {
                lblError.Text = "Debe Rellenar el título, la imagen y la fecha";
                lblError.Show();
            }
            else
            {
                string fechayhora = datetime.ToString("yyyy-MM-dd HH:mm:ss");
                DateTime datetime2 = dtpFecha2.Value.Date + dtpHora2.Value.TimeOfDay;
                string fechayhora2 = datetime2.ToString("yyyy-MM-dd HH:mm:ss");
                MemoryStream ms = new MemoryStream();
                pbxImagen.Image.Save(ms, ImageFormat.Jpeg);
                byte[] data = ms.ToArray();
                await Publicar(txtTitulo.Text, txtUbicacion.Text, txtDescripcion.Text, data, fechayhora, fechayhora2);
                MessageBox.Show("Evento creado correctamente");
            }
        }

        
        //Conexion con API
        static async Task Publicar(string titulo, string ubicacion, string descripcion, byte[] imagen, string fechayhora, string fechayhora2)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var datos = new { titulo = titulo, ubicacion = ubicacion, descripcion = descripcion, foto = Convert.ToBase64String(imagen), fechaYhora_Inicio = fechayhora, fechaYhora_Final = fechayhora2, user = usuario };
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
    }
}
