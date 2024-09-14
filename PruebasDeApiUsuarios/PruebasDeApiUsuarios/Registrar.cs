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
    public partial class Registrar : Form
    {
        public Registrar()
        {
            InitializeComponent();
            redondearPictureBox(Properties.Resources.perfilVacio);
        }

        private void btnSeleccionar_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                Image selectedImage = Image.FromFile(ofd.FileName);
                pictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;
                redondearPictureBox(selectedImage);
            }
        }

        private async void btnRegistrar_Click(object sender, EventArgs e)
        {
            if (verificarDatos())
            {
                string nombreCuenta = txtNombre.Text; 
                bool userExists = await ExisteUser(nombreCuenta);
                Console.WriteLine(userExists);
                if (userExists==false)
                {
                    MemoryStream ms = new MemoryStream();
                    pictureBox1.Image.Save(ms, ImageFormat.Jpeg);
                    byte[] imagen = ms.ToArray();
                    var resultado = await Registro(txtNombre.Text, txtContraseña.Text, txtNombreV.Text, txtEmail.Text, txtDescripcion.Text, cbxGenero.SelectedItem.ToString(), dtpFecha.Text, imagen);
                    lblRespuesta.Text = resultado;
                    MessageBox.Show(resultado, "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Ha ocurrido un error. Ya existe alguien con ese nombre", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                   
                }
            } 
        }

        public static async Task<string> Registro(string nombreCuenta, string contraseña, string nombreVisible, string email, string descripcion, string género, string fechaDeNacimiento,byte[]imagen)
        {
            using (HttpClient client=new HttpClient())
            {
                try
                {
                    var datos = new { nombreDeCuenta = nombreCuenta, nombreVisible = nombreVisible, email = email, descripcion=descripcion,foto=imagen,configuraciones="nada",genero=género, fechaDeNacimiento=fechaDeNacimiento, estadoDeCuenta = "activo", contraseña=contraseña };
                    var content = new StringContent(JsonConvert.SerializeObject(datos), Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PostAsync("https://localhost:44383/user/RegistrarUsuario", content);
                    response.EnsureSuccessStatusCode();
                    var responseBody = await response.Content.ReadAsStringAsync();
                    dynamic data = JsonConvert.DeserializeObject(responseBody);
                    return data;
                }
                catch
                {
                    return "INCORRECTO";
                }
            }
        }

        private async Task<bool> ExisteUser(string nombreCuenta)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var datos = new { nombreDeCuenta = nombreCuenta};
                    var content = new StringContent(JsonConvert.SerializeObject(datos), Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PutAsync($"https://localhost:44383/user/ExisteUsuario",content);
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

        public void redondearPictureBox(Image image)
        {        
            System.Drawing.Drawing2D.GraphicsPath gp = new System.Drawing.Drawing2D.GraphicsPath();
            gp.AddEllipse(10, 5, pictureBox1.Width -24 , pictureBox1.Height - 6);
            Region rg = new Region(gp);
            pictureBox1.Region = rg;
            pictureBox1.Image = image;
        }

        public Boolean verificarDatos()
        {
            if (
                string.IsNullOrEmpty(txtNombre.Text)                ||
                string.IsNullOrEmpty(txtContraseña.Text)            ||
                string.IsNullOrEmpty(txtNombreV.Text)               ||
                string.IsNullOrEmpty(txtVerificarContraseña.Text)   ||
                string.IsNullOrEmpty(txtEmail.Text)               //  ||
               // string.IsNullOrEmpty(cbxGenero.SelectedText)
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

            if (txtNombre.Equals(txtContraseña))
            {
                MessageBox.Show("Ha ocurrido un error. El nombre no puede ser igual a la contraseña", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (txtContraseña.Text.Length<8)
            {
                MessageBox.Show("Ha ocurrido un error. Pruebe con una contraseña de al menos 8 dígitos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (!txtContraseña.Equals(txtContraseña))
            {
                MessageBox.Show("Ha ocurrido un error. Las contraseñas no coinciden", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        private void btnVerContraseña_Click(object sender, EventArgs e)
        {
            if (!txtContraseña.UseSystemPasswordChar)
            {
                txtContraseña.UseSystemPasswordChar = true;
            } else
            {
                txtContraseña.UseSystemPasswordChar = false;
            }
        }

        private void Registrar_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
