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

namespace PruebasDeApiUsuarios
{
    public partial class ModificarUsuario : Form
    {
        public ModificarUsuario()
        {
            InitializeComponent();
            redondearPictureBox(Properties.Resources.perfilVacio);
            cbxGenero.DropDownStyle = ComboBoxStyle.DropDownList;
            cbxEstadoCuenta.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private async void btnModificar_Click(object sender, EventArgs e)
        {
            if (verificarDatos())
            {
                var existe = await ExisteUser(txtNombre.Text);
                Console.WriteLine(existe);

                if (existe)
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        pictureBox1.Image.Save(ms, ImageFormat.Jpeg);
                        byte[] imagen = ms.ToArray();

                        var resultado = await ActualizarUsuario(txtNombre.Text, txtNombreV.Text, txtEmail.Text, txtDescripcion.Text, txtConfiguraciones.Text, cbxGenero.SelectedItem?.ToString(), dtpFecha.Text, cbxEstadoCuenta.SelectedItem?.ToString(), imagen);
                        lblRespuesta.Text = resultado;

                        MessageBox.Show(resultado, "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Ha ocurrido un error. No existe alguien con ese nombre", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void btnSeleccionar_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                Image selectedImage = Image.FromFile(ofd.FileName);
                //pictureBox1.ImageLocation = ofd.FileName;
                pictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;
                redondearPictureBox(selectedImage);
            }
        }
        public void redondearPictureBox(Image image)
        {
            System.Drawing.Drawing2D.GraphicsPath gp = new System.Drawing.Drawing2D.GraphicsPath();
            gp.AddEllipse(10, 5, pictureBox1.Width - 24, pictureBox1.Height - 6);
            Region rg = new Region(gp);
            pictureBox1.Region = rg;
            pictureBox1.Image = image;
        }
        private async Task<bool> ExisteUser(string nombreCuenta)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync($"https://localhost:44383/user/ExisteUsuario?nombreDeCuenta={nombreCuenta}");
                    response.EnsureSuccessStatusCode(); // Lanza una excepción si el código de estado no es exitoso
                    var responseBody = await response.Content.ReadAsStringAsync();
                    dynamic result = JsonConvert.DeserializeObject(responseBody);

                    return result.existe;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al conectarse al servidor: " + ex.Message);
                    return false;
                }
            }
        }

        public Boolean verificarDatos()
        {
            if (
                string.IsNullOrEmpty(txtNombre.Text) ||
                string.IsNullOrEmpty(txtNombreV.Text) ||
                string.IsNullOrEmpty(txtEmail.Text)               
                )
            {
                MessageBox.Show("Ha ocurrido un error. Complete todos los campos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (!txtEmail.Text.Contains("@"))
            {
                MessageBox.Show("Ha ocurrido un error. Su dirección de correo electrónico no es válida", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }
        public static async Task<string> ActualizarUsuario(string nombreCuenta, string nombreVisible, string email, string descripcion, string configuraciones, string genero, string fechaDeNacimiento, string estadoDeCuenta, byte[] imagen)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    // Convertir la imagen a una cadena Base64
                    string imagenBase64 = Convert.ToBase64String(imagen);

                    // Crear el objeto con los datos del usuario
                    var datos = new
                    {
                        nombreDeCuenta = nombreCuenta,
                        nombreVisible = nombreVisible,
                        email = email,
                        descripcion = descripcion,
                        imagen = imagenBase64,
                        configuraciones = configuraciones,
                        genero = genero,
                        fechaDeNacimiento = fechaDeNacimiento,
                        estadoDeCuenta = estadoDeCuenta
                    };

                    // Serializar el objeto a JSON
                    var content = new StringContent(JsonConvert.SerializeObject(datos), Encoding.UTF8, "application/json");

                    // Realizar la solicitud PUT al endpoint
                    HttpResponseMessage response = await client.PutAsync("https://localhost:44383/user/ModificarUsuario", content);

                    // Asegurarse de que la solicitud fue exitosa
                    response.EnsureSuccessStatusCode();

                    // Leer y deserializar la respuesta del servidor
                    var responseBody = await response.Content.ReadAsStringAsync();
                    dynamic data = JsonConvert.DeserializeObject(responseBody);

                    // Devolver el mensaje contenido en la respuesta
                    return data.mensaje;
                }
                catch (Exception ex)
                {
                    // Manejo de excepciones
                    return $"INCORRECTO: {ex.Message}";
                }
            }
        }
    }
        
}
