using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Testing
{
    [TestClass]
    public class UnitTest1
    {

        public static string token;

        [TestMethod]
        public async Task TestMethod01()
        {
            string respuestaEsperada = "guardado correcto";
            ApiUsuarios.Controllers.UserController controller = new ApiUsuarios.Controllers.UserController();
            string projectDirectory = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.FullName;
            string imagePath = Path.Combine(projectDirectory, "Testing", "Imagen.jpg");
            byte[] imageBytes = File.ReadAllBytes(imagePath);
            string base64Image = Convert.ToBase64String(imageBytes);

            ApiUsuarios.Controllers.UserController.usuario datos = new ApiUsuarios.Controllers.UserController.usuario()
            {
                nombreDeCuenta = "nombre",
                nombreVisible = "Nombre",
                contraseña = "contraseña",
                email = "a@gmail",
                descripcion = "Descripción",
                foto = base64Image,
                configuraciones = "hola",
                genero = "Prefiero no decirlo",
                fechaDeNacimiento = "01/01/2000",
                estadoDeCuenta = "activo"
            };

            var result = await controller.RegistrarUsuario(datos);
            Console.WriteLine($"Result: {result}");
            var jsonResult = result as JsonResult;
            Assert.IsNotNull(jsonResult, "Se esperaba un JsonResult.");

            var resultData = jsonResult.Data as string;
            Assert.IsNotNull(resultData, "El Data en JsonResult no es del tipo esperado.");

            Assert.AreEqual(respuestaEsperada, resultData);
        }

        [TestMethod]
        public async Task TestMethod02()
        {
            string nombreDeCuenta = "nombre";
            string respuestaEsperada = "Nombre";
            ApiUsuarios.Controllers.UserController controller = new ApiUsuarios.Controllers.UserController();

            ApiUsuarios.Controllers.UserController.usuario user = new ApiUsuarios.Controllers.UserController.usuario
            {
                nombreDeCuenta = nombreDeCuenta
            };

            var response = await controller.ObtenerUsuario(user);
            Console.WriteLine($"Response from ObtenerUsuario: {response}");

            var jsonResult = response as JsonResult;
            Assert.IsNotNull(jsonResult, "Se esperaba un JsonResult.");

            var resultData = jsonResult.Data;
            string resultString = JsonConvert.SerializeObject(resultData);

            if (resultString.Contains("nombreVisible"))
            {
                dynamic resultObject = JsonConvert.DeserializeObject<dynamic>(resultString);
                Assert.AreEqual(respuestaEsperada, (string)resultObject.nombreVisible);
            }
            else
            {
                Assert.Fail($"No se encontró el usuario cuando se esperaba uno. Respuesta: {resultString}");
            }
        }


        [TestMethod]
        public async Task TestMethod03()
        {
            ApiUsuarios.Controllers.UserController controller = new ApiUsuarios.Controllers.UserController();
            ApiUsuarios.Controllers.UserController.usuario testUser = new ApiUsuarios.Controllers.UserController.usuario
            {
                nombreDeCuenta = "nombre"
            };

            var result = await controller.obtenerImagenUsuario(testUser);

            var jsonResult = result as JsonResult;
            Assert.IsNotNull(jsonResult, "Se esperaba un JsonResult.");

            string resultData = jsonResult.Data as string;
            Assert.IsNotNull(resultData, "El Data en JsonResult no es del tipo esperado.");

            Console.WriteLine($"Result Data: {resultData}");

            if (resultData.Contains("no se encuentra"))
            {
                Assert.Fail($"Usuario no encontrado: {testUser.nombreDeCuenta}");
            }
            else
            {
                try
                {
                    byte[] imageBytes = Convert.FromBase64String(resultData);
                    Assert.IsTrue(imageBytes.Length > 0, "El contenido de la imagen no es válido.");
                }
                catch (FormatException)
                {
                    Assert.Fail("El contenido devuelto no es una cadena base64 válida.");
                }
            }
        }


        [TestMethod]
        public async Task TestMethod04()
        {
            ApiUsuarios.Controllers.UserController controller = new ApiUsuarios.Controllers.UserController();
            ApiUsuarios.Controllers.UserController.usuario testUser = new ApiUsuarios.Controllers.UserController.usuario
            {
                nombreDeCuenta = "nombre"
            };

            var result = await controller.ObtenerUsuario(testUser);

            var jsonResult = result as JsonResult;
            Assert.IsNotNull(jsonResult, "Se esperaba un JsonResult.");

            string resultData = jsonResult.Data as string;
            if (resultData != null)
            {
                Console.WriteLine("Response JSON (string):");
                Console.WriteLine(resultData);

                if (resultData.Contains("no se encuentra"))
                {
                    Assert.Fail($"Usuario no encontrado: {testUser.nombreDeCuenta}");
                }
            }
            else
            {
                try
                {
                    string jsonString = JsonConvert.SerializeObject(jsonResult.Data, Formatting.Indented);
                    Console.WriteLine("Response JSON (object):");
                    Console.WriteLine(jsonString);

                    var userData = JsonConvert.DeserializeObject<dynamic>(jsonString);
                    string expectedNombreVisible = "Nombre";
                    Assert.AreEqual(expectedNombreVisible, (string)userData.nombreVisible, "El nombre visible no coincide con el esperado.");
                }
                catch (JsonReaderException)
                {
                    Assert.Fail("El contenido devuelto no es un JSON válido.");
                }
            }
        }

        [TestMethod]
        public async Task TestMethod05()
        {
            ApiUsuarios.Controllers.UserController controller = new ApiUsuarios.Controllers.UserController();
            ApiUsuarios.Controllers.UserController.usuario testUser = new ApiUsuarios.Controllers.UserController.usuario
            {
                nombreDeCuenta = "nombre"
            };

            var result = controller.ExisteUsuario(testUser);

            var jsonResult = result as JsonResult;
            Assert.IsNotNull(jsonResult, "Se esperaba un JsonResult.");

            string jsonString = JsonConvert.SerializeObject(jsonResult.Data, Formatting.Indented);
            Console.WriteLine("Response JSON:");
            Console.WriteLine(jsonString);

            try
            {
                dynamic data = JsonConvert.DeserializeObject<dynamic>(jsonString);

                if (data.existe == true)
                {
                    Assert.AreEqual("El usuario existe", (string)data.mensaje, "El mensaje no coincide para un usuario existente.");
                }
                else
                {
                    Assert.AreEqual("El usuario no existe", (string)data.mensaje, "El mensaje no coincide para un usuario no existente.");
                }
            }
            catch (JsonReaderException)
            {
                Assert.Fail("El contenido devuelto no es un JSON válido.");
            }
        }

        [TestMethod]
        public async Task TestMethod06()
        {
            ApiUsuarios.Controllers.UserController controller = new ApiUsuarios.Controllers.UserController();
            string projectDirectory = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.FullName;
            string imagePath = Path.Combine(projectDirectory, "Testing", "Imagen.jpg");


            byte[] imageBytes = File.ReadAllBytes(imagePath);
            string base64Image = Convert.ToBase64String(imageBytes);

            ApiUsuarios.Controllers.UserController.usuario testUser = new ApiUsuarios.Controllers.UserController.usuario
            {
                nombreDeCuenta = "nombre",
                nombreVisible = "Nombre Modificado",
                contraseña = "contraseña",
                email = "modificado@gmail.com",
                descripcion = "Descripción modificada",
                foto = base64Image,
                configuraciones = "Configuraciones modificadas",
                genero = "Prefiero no decirlo",
                fechaDeNacimiento = "01/01/2000",
                estadoDeCuenta = "inactivo"
            };

            var result = await controller.ModificarUsuario(testUser);
            var jsonResult = result as JsonResult;
            Assert.IsNotNull(jsonResult, "Se esperaba un JsonResult.");

            string jsonString = JsonConvert.SerializeObject(jsonResult.Data, Formatting.Indented);
            Console.WriteLine("Response JSON:");
            Console.WriteLine(jsonString);

            try
            {
                dynamic data = JsonConvert.DeserializeObject<dynamic>(jsonString);


                Assert.AreEqual("Guardado correcto", (string)data.mensaje, "La modificación no se completó correctamente.");
            }
            catch (JsonReaderException)
            {
                Assert.Fail("El contenido devuelto no es un JSON válido.");
            }
        }

        [TestMethod]
        public void TestMethod07()
        {
            ApiUsuarios.Controllers.UserController controller = new ApiUsuarios.Controllers.UserController();
            ApiUsuarios.Controllers.UserController.usuario testUser = new ApiUsuarios.Controllers.UserController.usuario
            {
                nombreDeCuenta = "nombre",
                configuraciones = "Nueva configuración"
            };
            var result = controller.CambiarConfiguracion(testUser);
            var jsonResult = result as JsonResult;
            Assert.IsNotNull(jsonResult, "Se esperaba un JsonResult.");
            string resultData = jsonResult.Data as string;
            Assert.IsNotNull(resultData, "El Data en JsonResult no es del tipo esperado.");
            Console.WriteLine($"Response: {resultData}");
            Assert.AreEqual("Configuracion correcta", resultData, "La configuración no se cambió correctamente.");
        }

        [TestMethod]
        public void TestMethod08()
        {
            ApiUsuarios.Controllers.UserController controller = new ApiUsuarios.Controllers.UserController();
            ApiUsuarios.Controllers.UserController.usuario testUser = new ApiUsuarios.Controllers.UserController.usuario
            {
                nombreDeCuenta = "nombre"
            };
            var result = controller.ConseguirConfiguracion(testUser);
            var jsonResult = result as JsonResult;
            Assert.IsNotNull(jsonResult, "Se esperaba un JsonResult.");
            string resultData = jsonResult.Data as string;
            Assert.IsNotNull(resultData, "El Data en JsonResult no es del tipo esperado.");
            Console.WriteLine($"Response: {resultData}");
            if (resultData.Contains("Hubo un error"))
            {
                Assert.Fail($"No se pudo obtener la configuración para el usuario: {testUser.nombreDeCuenta}");
            }
            else
            {
                string expectedConfiguracion = "Nueva configuración";
                Assert.AreEqual(expectedConfiguracion, resultData, "La configuración obtenida no coincide con la esperada.");
            }
        }

        [TestMethod]
        public async Task TestMethod09()
        {
            ApiUsuarios.Controllers.UserController controller = new ApiUsuarios.Controllers.UserController();

            string projectDirectory = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.FullName;
            string imagePath = Path.Combine(projectDirectory, "Testing", "Imagen.jpg"); 
            byte[] imageBytes = File.ReadAllBytes(imagePath);
            string base64Image = Convert.ToBase64String(imageBytes);

            ApiUsuarios.Controllers.UserController.Notificaciones testNotificacion = new ApiUsuarios.Controllers.UserController.Notificaciones
            {
                nombreDeCuenta = "nombre",
                texto = "Nueva notificación",
                tipo = "nuevoMensaje",
                imagen = base64Image
            };

            var result = await controller.AgregarNotificaciones(testNotificacion);
            var jsonResult = result as JsonResult;
            Assert.IsNotNull(jsonResult, "Se esperaba un JsonResult.");

            string resultData = jsonResult.Data as string;
            Assert.IsNotNull(resultData, "El Data en JsonResult no es del tipo esperado.");
            Console.WriteLine($"Response: {resultData}");

            Assert.AreEqual("Correcto", resultData, "La notificación no se agregó correctamente.");
        }

        [TestMethod]
        public async Task TestMethod10()
        {
            ApiUsuarios.Controllers.UserController controller = new ApiUsuarios.Controllers.UserController();

            string projectDirectory = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.FullName;
            string imagePath = Path.Combine(projectDirectory, "Testing", "Imagen.jpg");
            byte[] imageBytes = File.ReadAllBytes(imagePath);
            string base64Image = Convert.ToBase64String(imageBytes);

            ApiUsuarios.Controllers.UserController.Notificaciones testNotificacion = new ApiUsuarios.Controllers.UserController.Notificaciones
            {
                nombreDeCuenta = "nombre",
                texto = "Actualización de notificación",
                tipo = "actualización",
                imagen = base64Image
            };

            var result = await controller.ActualizarNotificaciones(testNotificacion);
            var jsonResult = result as JsonResult;
            Assert.IsNotNull(jsonResult, "Se esperaba un JsonResult.");

            string resultData = jsonResult.Data as string;
            Assert.IsNotNull(resultData, "El Data en JsonResult no es del tipo esperado.");
            Console.WriteLine($"Response: {resultData}");

            Assert.AreEqual("Correcto", resultData, "La notificación no se actualizó correctamente.");
        }

        [TestMethod]
        public async Task TestMethod11()
        {
            ApiUsuarios.Controllers.UserController controller = new ApiUsuarios.Controllers.UserController();

            ApiUsuarios.Controllers.UserController.Notificaciones testNotificacion = new ApiUsuarios.Controllers.UserController.Notificaciones
            {
                nombreDeCuenta = "nombre"
            };

            var result = await controller.ConseguirNotificaciones(testNotificacion);
            var jsonResult = result as JsonResult;
            Assert.IsNotNull(jsonResult, "Se esperaba un JsonResult.");

            var resultData = jsonResult.Data;
            Assert.IsNotNull(resultData, "El Data en JsonResult no es del tipo esperado.");
            Console.WriteLine($"Response: {JsonConvert.SerializeObject(resultData)}");

            if (resultData is string resultString && resultString.Contains("No se encontraron notificaciones"))
            {
                Assert.Fail("No se encontraron notificaciones para el usuario.");
            }
            else if (resultData is string errorString && errorString.Contains("Error"))
            {
                Assert.Fail($"Ocurrió un error al intentar conseguir las notificaciones: {errorString}");
            }
            else
            {
                var notificationsList = JsonConvert.DeserializeObject<List<dynamic>>(JsonConvert.SerializeObject(resultData));
                Assert.IsNotNull(notificationsList, "El resultado no es una lista de notificaciones.");
                Assert.IsTrue(notificationsList.Count > 0, "La lista de notificaciones está vacía.");
                Assert.IsNotNull(notificationsList[0].texto, "La notificación no contiene 'texto'.");
                Assert.IsNotNull(notificationsList[0].tipo, "La notificación no contiene 'tipo'.");
                Assert.IsNotNull(notificationsList[0].fechaYHora, "La notificación no contiene 'fechaYHora'.");
                Assert.IsNotNull(notificationsList[0].imagen, "La notificación no contiene 'imagen'.");

                string base64Image = notificationsList[0].imagen.ToString();
                try
                {
                    Convert.FromBase64String(base64Image);
                }
                catch (FormatException)
                {
                    Assert.Fail("La 'imagen' no es un string base64 válido.");
                }
            }
        }

        [TestMethod]
        public async Task TestMethod12()
        {
            ApiUsuarios.Controllers.UserController controller = new ApiUsuarios.Controllers.UserController();
            string projectDirectory = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.FullName;
            string imagePath = Path.Combine(projectDirectory, "Testing", "Imagen.jpg");
            byte[] imageBytes = File.ReadAllBytes(imagePath);
            string base64Image = Convert.ToBase64String(imageBytes);

            // Creo Usuario para Reportar
            ApiUsuarios.Controllers.UserController.usuario newUser = new ApiUsuarios.Controllers.UserController.usuario
            {
                nombreDeCuenta = "usuarioReportar",
                nombreVisible = "Usuario Reportar",
                contraseña = "password123",
                email = "usuarioReportar@example.com",
                foto = base64Image,
                configuraciones = "default",
                genero = "No especificado",
                fechaDeNacimiento = "01/01/1990",
                estadoDeCuenta = "activo"
            };
            var createUserResult = await controller.RegistrarUsuario(newUser);
            var createUserJsonResult = createUserResult as JsonResult;
            Assert.IsNotNull(createUserJsonResult, "Se esperaba un JsonResult al crear el usuario para reportar.");
            string createUserResultData = createUserJsonResult.Data as string;
            Assert.IsNotNull(createUserResultData, "El Data en JsonResult al crear el usuario no es del tipo esperado.");
            Assert.AreEqual("guardado correcto", createUserResultData, "No se pudo crear el usuario que se va a reportar.");

            // Reporto el usuario
            ApiUsuarios.Controllers.UserController.Reportes testReporteUsuario = new ApiUsuarios.Controllers.UserController.Reportes
            {
                nombreDeCuenta = "nombre",
                cuentaReporteUsuario = "usuarioReportar",
                tipo = "Sexual",
                descripcion = "Subio contenido inapropiado"
            };

            var result = await controller.Reportar(testReporteUsuario);
            var jsonResult = result as JsonResult;
            Assert.IsNotNull(jsonResult, "Se esperaba un JsonResult.");

            string resultData = jsonResult.Data as string;
            Assert.IsNotNull(resultData, "El Data en JsonResult no es del tipo esperado.");
            Console.WriteLine($"Response: {resultData}");

            Assert.IsTrue(resultData.Contains("Se ha reportado correctamente al usuario"), "La respuesta no coincide con el reporte del usuario.");
        }



    }
}
