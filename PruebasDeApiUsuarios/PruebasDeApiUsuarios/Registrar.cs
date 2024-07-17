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

        private async void btnRegistrar_Click(object sender, EventArgs e)
        {
            var existe = await ExisteUser(txtNombre.Text);
            if (!txtNombre.Text.Equals(txtContraseña.Text))
            {
                if (!existe)
                {
                    MemoryStream ms = new MemoryStream();
                    pictureBox1.Image.Save(ms, ImageFormat.Jpeg);
                    byte[] imagen = ms.ToArray();
                    var resultado = await Registro(txtNombre.Text, txtContraseña.Text, txtNombreV.Text, txtEmail.Text, txtDescripcion.Text, cbxGenero.SelectedItem.ToString(), dtpFecha.Text, imagen);
                    lblRespuesta.Text = resultado;
                }
                else
                {
                    lblRespuesta.Text = "Ya existe alguien con ese nombre";
                }
            }
            else
            {
                lblRespuesta.Text = "No puede tener el mismo nombre que contraseña";
            }   
        }

        public static async Task<string> Registro(string nombreCuenta, string contraseña, string nombreVisible, string email, string descripcion, string género, string fechaDeNacimiento,byte[]imagen)
        {
            using (HttpClient client=new HttpClient())
            {
                try
                {
                    var datos = new { nombreDeCuenta = nombreCuenta, nombreVisible = nombreVisible, email = email, descripcion=descripcion,imagen=imagen,configuraciones="nada",genero=género, fechaDeNacimiento=fechaDeNacimiento, estadoDeCuenta = "activo", contraseña=contraseña };
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

        public static async Task<bool> ExisteUser(string nombreCuenta)
        {
            using (HttpClient client=new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync($"https://localhost:44383/user/existeUsuario?nombredecuenta={nombreCuenta}");
                    response.EnsureSuccessStatusCode();
                    var responseBody = await response.Content.ReadAsStringAsync();
                    dynamic data = JsonConvert.DeserializeObject(responseBody);
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
