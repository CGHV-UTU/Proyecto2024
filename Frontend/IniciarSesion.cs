using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Http;
using Newtonsoft.Json;
using System.Drawing.Drawing2D;

namespace Frontend
{
    public partial class IniciarSesion : Form
    {
        public IniciarSesion()
        {
            InitializeComponent();
            this.MaximizeBox = false;
        }

       

        public static async Task<bool> ComprobarPeticion(string token)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var tokenPayload = new { Token = token };
                    var content = new StringContent(JsonConvert.SerializeObject(tokenPayload), Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PostAsync("https://localhost:44383/auth/testToken", content);
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

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            Registrarse registro = new Registrarse();
            registro.FormClosed += (s, args) => this.Close();
            registro.Show();
            this.Hide();
        }
        private void SetPlaceholder()
        {
            txtUsuario.ForeColor = Color.Gray;
            txtUsuario.Text = "Usuario";
        }

        private void SetPlaceholder2()
        {
            txtContraseña.ForeColor = Color.Gray;
            txtContraseña.UseSystemPasswordChar = false;
            txtContraseña.Text = "Contraseña";
        }
       

        private void lblOlvidarContraseña_Click(object sender, EventArgs e)
        {
            MessageBox.Show("No podemos reiniciar tu contraseña porque no hay plata");
        }


        public static async Task<string> HacerPeticion(string usuario, string contraseña)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var datos = new { User = usuario, Pass = contraseña };
                    var content = new StringContent(JsonConvert.SerializeObject(datos), Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PostAsync("https://localhost:44383/auth/token", content);
                    response.EnsureSuccessStatusCode();
                    var responseBody = await response.Content.ReadAsStringAsync();
                    dynamic data = JsonConvert.DeserializeObject(responseBody);
                    return data;
                }
                catch
                {
                    return "Solicitud inválida";
                }
            }
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

      

        private async void btnIniciarSesion_Click_1(object sender, EventArgs e)
        {
            try
            {
                    var token = await HacerPeticion(txtUsuario.Text, txtContraseña.Text);
                    if (!token.Equals("Solicitud inválida"))
                    {
                        var resultado = await ComprobarPeticion(token);
                        if (resultado)
                        {
                            Inicio inicio = new Inicio(txtUsuario.Text);
                            inicio.FormClosed += (s, args) => this.Close();
                            inicio.Show();
                            this.Hide();
                        }
                        else
                        {
                            MessageBox.Show("Ha ocurrido un error. No se pudo iniciar sesión", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            txtUsuario.Text = "Usuario";
                            txtContraseña.Text = "Contraseña";
                        }
                    }
                    else
                    {
                        MessageBox.Show("Ha ocurrido un error. No se pudo iniciar sesión", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtUsuario.Text = "Usuario";
                        txtContraseña.Text = "Contraseña";
                    }              
            } 
            catch
            {
                MessageBox.Show("Ha ocurrido un error. No se pudo iniciar sesión", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtUsuario.Text = "Usuario";
                txtContraseña.Text = "Contraseña";
            }
           
            
        }

        private void txtContraseña_Leave_1(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtContraseña.Text))
            {
                SetPlaceholder2();
            }
        }

        private void txtContraseña_Enter_1(object sender, EventArgs e)
        {
            if (txtContraseña.Text == "Contraseña")
            {
                txtContraseña.Text = "";
                txtContraseña.ForeColor = Color.Black;
                txtContraseña.UseSystemPasswordChar = true;
            }
        }

        private void txtUsuario_Enter_1(object sender, EventArgs e)
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
                SetPlaceholder();
            }
        }
    }
}
