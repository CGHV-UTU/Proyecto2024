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
            ApiUsuarios.Controllers.UserController.usuario datos = new ApiUsuarios.Controllers.UserController.usuario()
            { nombreDeCuenta = "nombre", nombreVisible = "Nombre", contraseña = "contraseña", email = "a@gmail", foto = "foto.com", configuraciones = "hola", genero = "Prefiero no decirlo", fechaDeNacimiento = "01/01/2000", estadoDeCuenta = "activo" };
            Assert.AreEqual(respuestaEsperada, JsonConvert.DeserializeObject(controller.PRRegistrarUsuario(datos)));
        }

        [TestMethod]
        public void TestMethod2()
        {
            string nombreDeCuenta = "nombre";
            string respuestaEsperada = "Nombre";
            ApiUsuarios.Controllers.UserController controller = new ApiUsuarios.Controllers.UserController();
            string response = controller.PRObtenerUsuario(nombreDeCuenta);
            Console.WriteLine($"Response from PRObtenerUsuario: {response}");
            if (response.Contains("nombreVisible"))
            {
                var resultObject = JsonConvert.DeserializeObject<dynamic>(response);
                Assert.AreEqual(respuestaEsperada, (string)resultObject.nombreVisible);
            }
            else
            {
                Assert.Fail($"No se encontró el usuario cuando se esperaba uno. Respuesta: {response}");
            }
        }

        [TestMethod]
        public void TestMethod3()
        {
            bool respuestaEsperada = true;
            ApiUsuarios.Controllers.UserController controller = new ApiUsuarios.Controllers.UserController();
            var json = controller.PRexisteUsuario("nombre");
            dynamic respuestaRecibida = JsonConvert.DeserializeObject<dynamic>(json);
            Assert.AreEqual(respuestaEsperada, (bool)respuestaRecibida.existe);
        }

        [TestMethod]
        public void TestMethod4()
        {
            var controller = new ApiUsuarios.Controllers.UserController();
            ApiUsuarios.Controllers.UserController.usuario datos = new ApiUsuarios.Controllers.UserController.usuario()
            { nombreDeCuenta = "nombre", nombreVisible = "NuevoNombreVisible", email = "nuevoemail@example.com", descripcion = "Nueva descripción", foto = "nuevafoto.png", configuraciones = "nuevas configuraciones", genero = "Masculino", fechaDeNacimiento = "2007-01-01", estadoDeCuenta = "Activo" };
            var jsonResponse = controller.PRModificarUsuario(datos);
            dynamic response = JsonConvert.DeserializeObject<dynamic>(jsonResponse);
            Assert.AreEqual("Guardado correcto", (string)response.mensaje);
        }

        [TestMethod]
        public void TestMethod5()
        {
            var controller = new ApiUsuarios.Controllers.UserController();
            ApiUsuarios.Controllers.UserController.Reporte datos = new ApiUsuarios.Controllers.UserController.Reporte()
            { usuario = "nombre", tipo = "Abuso", descripcion = "Descripción del reporte" };
            var jsonResponse = controller.PRReportarUsuario(datos);
            string respuesta = JsonConvert.DeserializeObject<string>(jsonResponse);
            Assert.AreEqual("Reporte correcto", respuesta);
        }

        [TestMethod]
        public void TestMethod6()
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
        public void TestMethod7()
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
