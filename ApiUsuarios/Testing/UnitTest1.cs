using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.IO;

namespace Testing
{
    [TestClass]
    public class UnitTest1
    {
        public static string token;
        [TestMethod]
        public void TestMethod1()
        {
            string respuestaEsperada = "guardado correcto";
            ApiUsuarios.Controllers.UserController controller = new ApiUsuarios.Controllers.UserController();
            string projectDirectory = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.FullName;
            string relativePath = @"Testing\imagen.jpg";
            string imagePath = Path.Combine(projectDirectory, relativePath);
            byte[] imageBytes = File.ReadAllBytes(imagePath);
            string base64String = Convert.ToBase64String(imageBytes);
            ApiUsuarios.Controllers.UserController.usuario datos = new ApiUsuarios.Controllers.UserController.usuario()
            { nombreDeCuenta = "nombre", nombreVisible = "Nombre", configuraciones = "nada", contraseña = "contraseña", email = "a@gmail", estadoDeCuenta = "activo", fechaDeNacimiento = "01/01/2000", genero = "Prefiero no decirlo", foto = "foto.com" };
            Assert.AreEqual(respuestaEsperada, JsonConvert.DeserializeObject(controller.PRRegistrarUsuario(datos)));
        }

        [TestMethod]
        public void TestMethod2()
        {
            string respuestaEsperada = "Nombre";
            ApiUsuarios.Controllers.UserController controller = new ApiUsuarios.Controllers.UserController();
            var json = controller.PRObtenerUsuario("nombre");
            dynamic objeto = JsonConvert.DeserializeObject(json);
            string nombre = objeto.NombreVisible;
            Assert.AreEqual(respuestaEsperada, nombre);
        }

        [TestMethod]
        public void TestMethod3()
        {
            bool respuestaEsperada = true;
            ApiUsuarios.Controllers.UserController controller = new ApiUsuarios.Controllers.UserController();
            var json = controller.PRexisteUsuario("nombre");
            bool respuestaRecibida = JsonConvert.DeserializeObject(json);
            Assert.AreEqual(respuestaEsperada, respuestaRecibida);
        }

        [TestMethod]
        public void TestMethod4()
        {
            string respuestaNoEsperada = "Solicitud inválida";
            ApiUsuarios.Controllers.AuthController controller = new ApiUsuarios.Controllers.AuthController();
            ApiUsuarios.Controllers.AuthController.formaLogin login = new ApiUsuarios.Controllers.AuthController.formaLogin() { User = "nombre", Pass = "contraseña" };
            var json = controller.PRToken(login);
            string respuestaRecibida = JsonConvert.DeserializeObject(json);
            token = respuestaRecibida;
            Assert.AreNotEqual(respuestaNoEsperada, respuestaRecibida);
        }

        [TestMethod]
        public void TestMethod5()
        {
            bool respuestaEsperada = true;
            ApiUsuarios.Controllers.AuthController controller = new ApiUsuarios.Controllers.AuthController();
            ApiUsuarios.Controllers.AuthController.TipoToken comprobacion = new ApiUsuarios.Controllers.AuthController.TipoToken() { token = token };
            var json = controller.PRTestToken(comprobacion);
            bool respuestaRecibida = JsonConvert.DeserializeObject(json);
            Assert.AreEqual(respuestaEsperada, respuestaRecibida);
        }
    }
}
