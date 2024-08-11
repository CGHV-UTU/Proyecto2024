using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace PruebasAPIGrupos
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        
        private async void btnIniciarSesion_ClickAsync(object sender, EventArgs e)
        {
            
            var token = await HacerPeticion(txtUsuario.Text, txtContraseña.Text);
            lblResultado.Text = "Resultado: " + token;
            if (!token.Equals("Solicitud inválida"))
            {
                var resultado = await ComprobarPeticion(token);
                if (resultado)
                {
                    lblResultado.Text = "Resultado: Inicio correcto";
                    MenuGrupo form = new MenuGrupo(txtUsuario.Text);
                    form.Visible = true;

                }
                else
                {
                    lblResultado.Text = "Resultado: Inicio incorrecto";
                }
            }
            
        }

        private void btnVerContraseña_Click(object sender, EventArgs e)
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

        private void btnRegistrarse_Click(object sender, EventArgs e)
        {
            Registrar form = new Registrar();
            form.Visible = true;
        }
    
    
    public static async Task<string> HacerPeticion(string usuario, string contraseña)
    {
        using (HttpClient client = new HttpClient())
        {
            try
            {
                var datos = new { User = usuario, Pass = contraseña };
                var content = new StringContent(JsonConvert.SerializeObject(datos), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync("https://localhost:44304/auth/token", content);
                response.EnsureSuccessStatusCode();
                var responseBody = await response.Content.ReadAsStringAsync();
                dynamic data = JsonConvert.DeserializeObject(responseBody);
                return data;
            }
            catch
            {
                return "Solicitud incorrecta";
            }
        }
    }
        
        public static async Task<bool> ComprobarPeticion(string token)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var tokenPayload = new { Token = token };
                    var content = new StringContent(JsonConvert.SerializeObject(tokenPayload), Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PostAsync("https://localhost:44304/auth/testToken", content);
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

        //CAMBIAR, SACARLO, ES UN CÁNCER, UN ERROR DE LA HUMANIDAD. 
        //Wtf? Qué dice? Creo que ya lo arreglé...
        private void btnMenuGrupos_Click(object sender, EventArgs e)
        {
            MenuGrupo form = new MenuGrupo(txtUsuario.Text);
            form.Visible = true;
        }
    }
}

