using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace PruebasAPIGrupos
{
    public partial class Form2 : Form
    {
        private static string user;
        public Form2(string Usuario)
        {
            InitializeComponent();
            user = Usuario;
        }

        public class Grupo
        {
            public string nombreReal { get; set; }
            public string nombreVisible { get; set; }
            public string configuracion { get; set; }
            public string descripcion { get; set; }
            public string imagen { get; set; }
            public char rol { get; set; }
            public string nombreDeCuenta { get; set; }

        }

        private Boolean verificarDatos(string txtNombreVisible, string txtImagen, string configuracion)
        {
            return !string.IsNullOrEmpty(txtNombreVisible)
                && !string.IsNullOrEmpty(txtImagen)
                && !string.IsNullOrEmpty(configuracion);
        }

        private async void btnCrear_Click(object sender, EventArgs e)
        {
            string configuracion = "TodosHablan";
            if (rbtnAdminHabla.Checked)
            {
                configuracion = "AdminHabla";
            }
            MemoryStream ms = new MemoryStream();
            pictureBox1.Image.Save(ms, ImageFormat.Jpeg);
            byte[] imagen = ms.ToArray();
            string foto = Convert.ToBase64String(imagen);
            if (verificarDatos(txtNombre.Text, foto, configuracion))
            {
                if (string.IsNullOrEmpty(txtDescripcion.Text))
                {
                    txtDescripcion.Text = "";
                }
                Grupo grupo = new Grupo
                {
                    nombreVisible = txtNombre.Text,
                    descripcion = txtDescripcion.Text,
                    imagen = foto,
                    configuracion = configuracion,
                    nombreDeCuenta = user

                };
                try
                {
                    string resultadoGrupo = await RegistrarGrupo(grupo);
                    MessageBox.Show($"{resultadoGrupo}");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ocurrió un error: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("Datos inválidos");
            }
        }

        public static async Task<string> RegistrarGrupo(Grupo grupo)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var contentGrupo = new StringContent(JsonConvert.SerializeObject(grupo), Encoding.UTF8, "application/json");
                    HttpResponseMessage responseGrupo = await client.PostAsync("https://localhost:44304/RegistrarGrupo", contentGrupo);
                    responseGrupo.EnsureSuccessStatusCode();

                    return $"Grupo creado correctamente {responseGrupo}";
                }
                catch (Exception ex)
                {
                    return $"No se pudo crear el grupo: {ex.Message}";
                }
            }
        }
        private void btnImagen_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                Image selectedImage = Image.FromFile(ofd.FileName);
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage; 
                pictureBox1.Image = selectedImage;
            }
        }
    }
}
