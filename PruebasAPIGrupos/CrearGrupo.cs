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
        }

        private Boolean verificarDatos(string txtNombreVisible, string txtImagen, string configuracion)
        {
            Boolean correcto = false;
            if (!string.IsNullOrEmpty(txtNombreVisible)
                && !string.IsNullOrEmpty(txtImagen)
                && !string.IsNullOrEmpty(configuracion))
            {
                correcto = true;
            }
            return correcto;
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
                    //nombreReal = user, // Asegúrate de tener un campo para el nombre real del grupo
                    nombreVisible = txtNombre.Text,
                    descripcion = txtDescripcion.Text,
                    imagen = foto,
                    configuracion = configuracion
                };

                try
                {
                    string resultadoGrupo = await RegistrarGrupo(grupo);
                    string resultadoUsuarioGrupo = await RegistrarParticipar(grupo.nombreReal, user);

                    MessageBox.Show($"{resultadoGrupo}\n{resultadoUsuarioGrupo}");
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
                    // Registrar el grupo en la tabla "grupos"
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

        public static async Task<string> RegistrarParticipar(string nombreReal, string usuario)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var usuarioGrupo = new
                    {
                        nombreReal = nombreReal,
                        nombreDeCuenta = usuario,
                    };

                    var contentUsuarioGrupo = new StringContent(JsonConvert.SerializeObject(usuarioGrupo), Encoding.UTF8, "application/json");
                    HttpResponseMessage responseUsuarioGrupo = await client.PostAsync("https://localhost:44304/RegistrarGrupoUG", contentUsuarioGrupo);

                    // Verifica si la respuesta es exitosa
                    if (responseUsuarioGrupo.IsSuccessStatusCode)
                    {
                        return $"Relación Participa creada correctamente {responseUsuarioGrupo}";
                    }
                    else
                    {
                        // Lee el contenido de la respuesta en caso de error
                        var errorContent = await responseUsuarioGrupo.Content.ReadAsStringAsync();
                        return $"Error en la creación: {responseUsuarioGrupo.StatusCode} - {errorContent}";
                    }
                }
                catch (Exception ex)
                {
                    return $"No se pudo crear la relación Participa: {ex.Message}";
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
