using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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

namespace PruebasDeApiUsuarios
{
    public partial class Registrar : Form
    {
        public Registrar()
        {
            InitializeComponent();
        }





























        /*
        private async void btnRegistrar_Click(object sender, EventArgs e)
        {
            string imagen = ""; //MODIFICAR
            string configuraciones = "aa"; //MODIFICAR
            string genero = "Hombre";
            string estado = "activo";
            Console.WriteLine(dtpNacimiento.Text);
            var x = await RegisterUserAsync(txtNombreCuenta.Text, txtNombreVisible.Text, txtEmail.Text, txtDescripcion.Text,
                imagen, configuraciones, genero, dtpNacimiento.Text, estado, txtContraseña.Text);
            if (!x.Equals("Error al registrar el usuario"))
            {
                lblResultado.Text = "Resultado: todo mal";
            }
        }

        private async Task<string> RegisterUserAsync(string nombreCuenta, string nombreVisible, string email,
                                                          string descripcion, string imagen, string configuraciones,
                                                          string genero, string fechaDeNacimiento, string estadoDeCuenta, 
                                                          string contraseña)
        {
            using (HttpClient client = new HttpClient())
            {
               // try 
                //{
                    var url = "https://localhost:44383/RegistrarUsuario";
                    var user = new
                    {
                        nombreDeCuenta = nombreCuenta,
                        nombreVisible = nombreVisible,
                        email = email,
                        descripcion = descripcion,
                        imagen = imagen,
                        configuraciones = configuraciones,
                        genero = genero,
                        fechaDeNacimiento = fechaDeNacimiento,
                        estadoDeCuenta = estadoDeCuenta,
                        contraseña = contraseña
                    };
                    var json = JsonConvert.SerializeObject(user);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PostAsync(url, content);
                    response.EnsureSuccessStatusCode();
                    var responseBody = await response.Content.ReadAsStringAsync();
                    dynamic data = JsonConvert.DeserializeObject(responseBody);
                    return data;                 
                } catch
                {
                    return "Error al registrar el usuario";
                }  
              
            }
        }
      */

    }
}
