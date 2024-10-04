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

namespace Frontend
{
    public partial class Registrarse : Form
    {
        public Registrarse()
        {
            InitializeComponent();
            
            txtUsuario.Enter += txtUsuario_Enter;
            txtUsuario.Leave += txtUsuario_Leave;
            txtNombreVisible.Enter += txtNombreVisible_Enter;
            txtNombreVisible.Leave += txtNombreVisible_Leave;
            txtContraseña.Enter += txtContraseña_Enter;
            txtContraseña.Leave += txtContraseña_Leave;
            txtContraseña2.Enter += txtContraseña2_Enter;
            txtContraseña2.Leave += txtContraseña2_Leave;
            txtEmail.Enter += txtEmail_Enter;
            txtEmail.Leave += txtEmail_Leave;
            txtDescripcion.Enter += txtDescripcion_Enter;
            txtDescripcion.Leave += txtDescripcion_Leave;

            redondearPictureBox(Properties.Resources.Perfil);
        }

        private async void btnRegistrar_Click(object sender, EventArgs e)
        {
            if (verificarDatos())
            {
                string nombreCuenta = txtUsuario.Text;
                bool userExists = await ExisteUser(nombreCuenta);
                Console.WriteLine(userExists);
                if (userExists == false)
                {
                    if (pictureBox6.Image == null)
                    {
                        MessageBox.Show("Ha ocurrido un error. Foto nula", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    MemoryStream ms = new MemoryStream();
                    pictureBox6.Image.Save(ms, ImageFormat.Jpeg);
                    byte[] imagen = ms.ToArray();
                    if (imagen == null || imagen.Length == 0)
                    {
                        MessageBox.Show("Ha ocurrido un error. Foto nula", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    } else
                    {
                       var resultado = await Registro(txtUsuario.Text, txtContraseña.Text, txtNombreVisible.Text,
                       txtEmail.Text, txtDescripcion.Text, cbxGenero.SelectedItem.ToString(), dtpFecha.Text,
                       imagen);
                       MessageBox.Show(resultado, "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                }
                else
                {
                    MessageBox.Show("Ha ocurrido un error. Ya existe alguien con ese nombre", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
        }

        private void pcbxLogo_Click(object sender, EventArgs e)
        {
            IniciarSesion iniciarSesion = new IniciarSesion();
            iniciarSesion.FormClosed += (s, args) => this.Close();
            iniciarSesion.Show();
            this.Hide();
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                Image selectedImage = Image.FromFile(ofd.FileName);
                //pictureBox1.ImageLocation = ofd.FileName;
                pictureBox6.SizeMode = PictureBoxSizeMode.CenterImage;
                redondearPictureBox(selectedImage);
            }
        }

        public void redondearPictureBox(Image image)
        {
            if (image == null)
            {
                MessageBox.Show("La imagen es nula. No se puede redondear.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            System.Drawing.Drawing2D.GraphicsPath gp = new System.Drawing.Drawing2D.GraphicsPath();
            gp.AddEllipse(0, 0, pictureBox6.Width, pictureBox6.Height);
            Region rg = new Region(gp);
            pictureBox6.Region = rg;
            pictureBox6.Image = image;
        }

        public Boolean verificarDatos()
        {
            if (
                string.IsNullOrEmpty(txtUsuario.Text) ||
                string.IsNullOrEmpty(txtContraseña.Text) ||
                string.IsNullOrEmpty(txtNombreVisible.Text) ||
                string.IsNullOrEmpty(txtContraseña2.Text) ||
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

            if (txtUsuario.Equals(txtContraseña))
            {
                MessageBox.Show("Ha ocurrido un error. El nombre no puede ser igual a la contraseña", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (txtContraseña.Text.Length < 8)
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

        private void pcbxVerContraseña_Click(object sender, EventArgs e)
        {
            if (!txtContraseña.UseSystemPasswordChar)
            {
                txtContraseña.UseSystemPasswordChar = true;
            }
            else
            {
                txtContraseña.UseSystemPasswordChar = false;
            }
        }

        private void pcbxVerContraseña2_Click(object sender, EventArgs e)
        {
            if (!txtContraseña2.UseSystemPasswordChar)
            {
                txtContraseña2.UseSystemPasswordChar = true;
            }
            else
            {
                txtContraseña2.UseSystemPasswordChar = false;
            }
        }

        private async Task<bool> ExisteUser(string nombreCuenta)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var dato = new { nombreDeCuenta=nombreCuenta };
                    var content = new StringContent(JsonConvert.SerializeObject(dato), Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PutAsync($"https://localhost:44383/user/ExisteUsuario", content);
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

        public static async Task<string> Registro(string nombreCuenta, string contraseña, string nombreVisible, string email, string descripcion, string género, string fechaDeNacimiento, byte[] imagen)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string imagenString = Convert.ToBase64String(imagen);
                    var datos = new
                    {
                        nombreDeCuenta = nombreCuenta,
                        nombreVisible = nombreVisible,
                        email = email,
                        descripcion = descripcion,
                        foto = imagenString,
                        configuraciones = "{Claro};{Español}",
                        genero = género,
                        fechaDeNacimiento = fechaDeNacimiento,
                        estadoDeCuenta = "activo",
                        contraseña = contraseña
                    };
                    var content = new StringContent(JsonConvert.SerializeObject(datos), Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PostAsync("https://localhost:44383/user/RegistrarUsuario", content);
                    response.EnsureSuccessStatusCode();
                    var responseBody = await response.Content.ReadAsStringAsync();
                    dynamic data = JsonConvert.DeserializeObject(responseBody);

                    // Convertir el objeto a string para evitar el error de conversión
                    return data.ToString();
                }
                catch (Exception ex)
                {
                    return "INCORRECTO: " + ex.Message;
                }
            }
        }


        private void SetPlaceholder(string texto)
        {
            switch (texto)
            {
                case "txtUsuario":
                    txtUsuario.ForeColor = Color.Gray;
                    txtUsuario.Text = "Usuario";
                    break;

                case "txtNombreVisible":
                    txtNombreVisible.ForeColor = Color.Gray;
                    txtNombreVisible.Text = "Nombre Visible";
                    break;

                case "txtContraseña":
                    txtContraseña.ForeColor = Color.Gray;
                    txtContraseña.Text = "Contraseña";
                    break;

                case "txtContraseña2":
                    txtContraseña2.ForeColor = Color.Gray;
                    txtContraseña2.Text = "Repita su contraseña";
                    break;

                case "txtEmail":
                    txtEmail.ForeColor = Color.Gray;
                    txtEmail.Text = "Email";
                    break;

                case "txtDescripcion":
                    txtDescripcion.ForeColor = Color.Gray;
                    txtDescripcion.Text = "Descripción";
                    break;
            }

        }
        private void txtUsuario_Enter(object sender, EventArgs e)
        {
            if (txtUsuario.Text == "Usuario")
            {
                txtUsuario.Text = "";
                txtUsuario.ForeColor = Color.Black;
            }
        }

        private void txtUsuario_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUsuario.Text))
            {
                SetPlaceholder("txtUsuario");
            }
        }

        private void txtNombreVisible_Enter(object sender, EventArgs e)
        {
            if (txtNombreVisible.Text == "Nombre visible")
            {
                txtNombreVisible.Text = "";
                txtNombreVisible.ForeColor = Color.Black;
            }
        }

        private void txtNombreVisible_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombreVisible.Text))
            {
                SetPlaceholder("txtNombreVisible");
            }
        }

        private void txtContraseña_Enter(object sender, EventArgs e)
        {
            if (txtContraseña.Text == "Contraseña")
            {
                txtContraseña.Text = "";
                txtContraseña.ForeColor = Color.Black;
                txtContraseña.UseSystemPasswordChar = true; // Oculta el texto al ser escrito
            }
        }

        private void txtContraseña_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtContraseña.Text))
            {
                txtContraseña.UseSystemPasswordChar = false; // Muestra el placeholder como texto
                SetPlaceholder("txtContraseña");
            }
        }

        private void txtContraseña2_Enter(object sender, EventArgs e)
        {
            if (txtContraseña2.Text == "Repita su contraseña")
            {
                txtContraseña2.Text = "";
                txtContraseña2.ForeColor = Color.Black;
                txtContraseña2.UseSystemPasswordChar = true; // Oculta el texto al ser escrito
            }
        }

        private void txtContraseña2_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtContraseña2.Text))
            {
                txtContraseña2.UseSystemPasswordChar = false; // Muestra el placeholder como texto
                SetPlaceholder("txtContraseña2");
            }
        }

        private void txtEmail_Enter(object sender, EventArgs e)
        {
            if (txtEmail.Text == "Email")
            {
                txtEmail.Text = "";
                txtEmail.ForeColor = Color.Black;
            }
        }

        private void txtEmail_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                SetPlaceholder("txtEmail");
            }
        }

        private void txtDescripcion_Enter(object sender, EventArgs e)
        {
            if (txtDescripcion.Text == "Descripción")
            {
                txtDescripcion.Text = "";
                txtDescripcion.ForeColor = Color.Black;
            }
        }

        private void txtDescripcion_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtDescripcion.Text))
            {
                SetPlaceholder("txtDescripcion");
            }
        }
    }
}