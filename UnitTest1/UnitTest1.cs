using Microsoft.VisualStudio.TestTools.UnitTesting;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http.Results;
using API_Grupos.Controllers;

namespace UnitTest1
{
    [TestClass]
    public class UnitTest1
    {
        public string nombreRealGenerado;

        [TestMethod]
        public async Task TestMethod01()
        {
            API_Grupos.Controllers.GroupController controller = new API_Grupos.Controllers.GroupController();
            string projectDirectory = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.FullName;
            string imagePath = Path.Combine(projectDirectory, "UnitTest1", "Imagen.jpg");
            byte[] imageBytes = File.ReadAllBytes(imagePath);
            string base64Image = Convert.ToBase64String(imageBytes);

            API_Grupos.Controllers.GroupController.Grupo grupoPrueba = new API_Grupos.Controllers.GroupController.Grupo
            {
                nombreVisible = "Grupo de Prueba",
                configuracion = "default",
                imagen = base64Image,
                nombreDeCuenta = "nombre",
                descripcion = "Este es un grupo de prueba",
                token = "TestToken"
            };

            var result = await controller.RegistrarGrupo(grupoPrueba);
            var jsonResult = result as JsonResult<string>;

            Assert.IsNotNull(jsonResult, "Se esperaba un JsonResult.");
            string resultData = jsonResult.Content;
            Assert.IsNotNull(resultData, "El Data en JsonResult no es del tipo esperado.");
            Console.WriteLine($"Response: {resultData}");
            Assert.AreEqual("Registro correcto", resultData, "El grupo no se registró correctamente.");
        }

        private bool IsBase64String(string base64)
        {
            try
            {
                Convert.FromBase64String(base64);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        [TestMethod]
        public async Task TestMethod02()
        {
            var controller = new API_Grupos.Controllers.GroupController();
            API_Grupos.Controllers.GroupController.Grupo testGroupData = new API_Grupos.Controllers.GroupController.Grupo
            {
                nombreVisible = "Grupo de Prueba",
                nombreDeCuenta = "nombre",
                token = "TestToken"
            };

            var result = await controller.ObtenerGruposPorNombreVisibleYUsuario(testGroupData);
            var jsonResult = result as System.Web.Http.Results.JsonResult<List<API_Grupos.Controllers.GroupController.GrupoResponse>>;

            Assert.IsNotNull(jsonResult, "The result should not be null");
            var gruposList = jsonResult.Content;
            Assert.IsNotNull(gruposList, "El contenido no debe ser nulo");
            Assert.IsTrue(gruposList.Count > 0, "El resultado debe contener un grupo");
            Assert.AreEqual(testGroupData.nombreVisible, gruposList[0].nombreVisible, "Nombre Visible incorrecto");
        }

        [TestMethod]
        public async Task TestMethod03()
        {
            // Primero obtienes el nombre Real
            var controller = new API_Grupos.Controllers.GroupController();
            API_Grupos.Controllers.GroupController.Grupo testGroupData = new API_Grupos.Controllers.GroupController.Grupo
            {
                nombreVisible = "Grupo de Prueba",
                nombreDeCuenta = "nombre",
                token = "TestToken"
            };

            // Obtiene lista de grupos
            var obtenerGruposResult = await controller.ObtenerGruposPorNombreVisibleYUsuario(testGroupData);
            var jsonResultList = obtenerGruposResult as System.Web.Http.Results.JsonResult<List<API_Grupos.Controllers.GroupController.GrupoResponse>>;

            // Validaciones de la lista
            Assert.IsNotNull(jsonResultList, "El resultado no debe ser nulo");
            var gruposList = jsonResultList.Content;
            Assert.IsNotNull(gruposList, "El contenido no debe ser nulo");
            Assert.IsTrue(gruposList.Count > 0, "El resultado debe contener al menos un grupo");

            // Obtiene el nombre real del primer grupo
            string nombreReal = gruposList[0].nombreReal;

            // Utilizas el nombre real para obtener el grupo específico
            API_Grupos.Controllers.GroupController.Grupo obtenerGrupoData = new API_Grupos.Controllers.GroupController.Grupo
            {
                nombreReal = nombreReal,
                token = "TestToken"
            };

            // Llamada para obtener los detalles del grupo
            var obtenerGrupoResult = await controller.ObtenerGrupo(obtenerGrupoData);
            var jsonResult = obtenerGrupoResult as System.Web.Http.Results.JsonResult<API_Grupos.Controllers.GroupController.GrupoResponse>;

            // Validaciones del grupo específico
            Assert.IsNotNull(jsonResult, "El resultado no debe ser nulo");
            var grupo = jsonResult.Content;
            Assert.IsNotNull(grupo, "El contenido no debe ser nulo");
            Assert.AreEqual(nombreReal, grupo.nombreReal, "Nombre de cuenta erróneo");
        }


        [TestMethod]
        public async Task TestMethod04()
        {
            var controller = new API_Grupos.Controllers.GroupController();
            API_Grupos.Controllers.GroupController.Grupo testGroupData = new API_Grupos.Controllers.GroupController.Grupo
            {
                nombreVisible = "Grupo de Prueba",
                nombreDeCuenta = "nombre",
                token = "TestToken"
            };

            var obtenerGruposResult = await controller.ObtenerGruposPorNombreVisibleYUsuario(testGroupData);
            var jsonResultList = obtenerGruposResult as System.Web.Http.Results.JsonResult<List<API_Grupos.Controllers.GroupController.GrupoResponse>>;

            Assert.IsNotNull(jsonResultList, "El resultado no debe ser nulo");
            var gruposList = jsonResultList.Content;
            Assert.IsNotNull(gruposList, "El contenido no debe ser nulo");
            Assert.IsTrue(gruposList.Count > 0, "El resultado debe contener al menos un grupo");

            string nombreReal = gruposList[0].nombreReal;
            API_Grupos.Controllers.GroupController.Grupo editarGroupData = new API_Grupos.Controllers.GroupController.Grupo
            {
                nombreReal = nombreReal,
                nombreVisible = "Nuevo Nombre Visible",
                configuracion = "Nueva Configuracion",
                descripcion = "Nueva Descripcion",
                imagen = "iVBORw0KGgoAAAANSUhEUgAAAAEAAAABCAQAAAC1HAwCAAAAC0lEQVR42mP8/wcAAgMBAQJ+Q4sAAAAASUVORK5CYII=", //Imagen de 1 pixel para probar
                token = "TestToken"
            };

            var editarGrupoResult = await controller.EditarGrupo(editarGroupData);
            var editarJsonResult = editarGrupoResult as System.Web.Http.Results.JsonResult<string>;

            Assert.IsNotNull(editarJsonResult, "El contenido no debe ser nulo");
            Assert.AreEqual("Se editó el grupo correctamente", editarJsonResult.Content, "No se edito correctamente");
        }

        [TestMethod]
        public async Task TestMethod05()
        {
            var controller = new API_Grupos.Controllers.GroupController();
            API_Grupos.Controllers.GroupController.Grupo testGroupData = new API_Grupos.Controllers.GroupController.Grupo
            {
                nombreDeCuenta = "nombre",
                token = "TestToken"
            };
            var result = await controller.ObtenerGruposPorUsuario(testGroupData);
            var jsonResultList = result as System.Web.Http.Results.JsonResult<List<API_Grupos.Controllers.GroupController.GrupoResponse>>;
            Assert.IsNotNull(jsonResultList, "El resultado no debe ser nulo");
            var gruposList = jsonResultList.Content;
            Assert.IsNotNull(gruposList, "El contenido no debe ser nulo");
            Assert.IsTrue(gruposList.Count > 0, "El resultado debe contener al menos un grupo");
            Assert.AreEqual("nombre", testGroupData.nombreDeCuenta, "Nombre de cuenta erroneo");
        }

        [TestMethod]
        public async Task TestMethod06()
        {
            var controller = new API_Grupos.Controllers.GroupController();
            API_Grupos.Controllers.GroupController.Grupo testGroupData = new API_Grupos.Controllers.GroupController.Grupo
            {
                nombreVisible = "Nuevo Nombre Visible",
                nombreDeCuenta = "nombre",
                token = "TestToken"
            };

            var obtenerGruposResult = await controller.ObtenerGruposPorNombreVisibleYUsuario(testGroupData);
            var jsonResultList = obtenerGruposResult as System.Web.Http.Results.JsonResult<List<API_Grupos.Controllers.GroupController.GrupoResponse>>;
            Assert.IsNotNull(jsonResultList, "El resultado de ObtenerGruposPorNombreVisibleYUsuario no debe ser nulo");
            var gruposList = jsonResultList.Content;
            Assert.IsNotNull(gruposList, "El contenido de ObtenerGruposPorNombreVisibleYUsuario no debe ser nulo");
            Assert.IsTrue(gruposList.Count > 0, "El resultado de ObtenerGruposPorNombreVisibleYUsuario debe contener al menos un grupo");

            string nombreReal = gruposList[0].nombreReal;

            API_Grupos.Controllers.GroupController.Grupo groupData = new API_Grupos.Controllers.GroupController.Grupo
            {
                nombreReal = nombreReal,
                nombreDeCuenta = "usuarioReportar",
                rol = "usuario",
                token = "TestToken"
            };

            var result = await controller.AgregarUsuarioAGrupo(groupData);
            var jsonResult = result as System.Web.Http.Results.JsonResult<string>;
            Assert.AreEqual("Usuario agregado al grupo", jsonResult.Content, "El usuario debería ser agregado al grupo correctamente");
        }
  
        [TestMethod]
        public async Task TestMethod07()
        {
            var controller = new API_Grupos.Controllers.GroupController();

            API_Grupos.Controllers.GroupController.Grupo testGroupData = new API_Grupos.Controllers.GroupController.Grupo
            {
                nombreVisible = "Nuevo Nombre Visible",
                nombreDeCuenta = "usuarioReportar",
                token = "TestToken"
            };

            var obtenerGruposResult = await controller.ObtenerGruposPorNombreVisibleYUsuario(testGroupData);
            var jsonResultList = obtenerGruposResult as System.Web.Http.Results.JsonResult<List<API_Grupos.Controllers.GroupController.GrupoResponse>>;
            Assert.IsNotNull(jsonResultList, "El resultado de ObtenerGruposPorNombreVisibleYUsuario no debe ser nulo");
            var gruposList = jsonResultList.Content;
            Assert.IsNotNull(gruposList, "El contenido de ObtenerGruposPorNombreVisibleYUsuario no debe ser nulo");
            Assert.IsTrue(gruposList.Count > 0, "El resultado de ObtenerGruposPorNombreVisibleYUsuario debe contener al menos un grupo");

            string nombreReal = gruposList[0].nombreReal;

            API_Grupos.Controllers.GroupController.Grupo groupData = new API_Grupos.Controllers.GroupController.Grupo
            {
                nombreReal = nombreReal,
                nombreDeCuenta = "usuarioReportar",
                token = "TestToken"
            };
            var eliminarResult = await controller.EliminarUsuarioDeGrupo(groupData);
            var eliminarJsonResult = eliminarResult as System.Web.Http.Results.JsonResult<string>;

            Assert.IsNotNull(eliminarJsonResult, "El resultado de EliminarUsuarioDeGrupo no debe ser nulo");
            Assert.AreEqual("Grupo eliminado del usuario correctamente", eliminarJsonResult.Content, "El usuario debería ser eliminado del grupo correctamente");

            Console.WriteLine(eliminarJsonResult.Content);
        }

        [TestMethod]
        public async Task TestMethod08()
        {
            // Arrange
            var controller = new API_Grupos.Controllers.GroupController();

            // Datos de prueba para el grupo
            var testGroupData = new API_Grupos.Controllers.GroupController.Grupo
            {
                nombreVisible = "Nuevo Nombre Visible",
                nombreDeCuenta = "nombre",
                token = "TestToken"
            };

            // Act: Obtener grupos
            var obtenerGruposResult = await controller.ObtenerGruposPorNombreVisibleYUsuario(testGroupData);
            var jsonResultList = obtenerGruposResult as System.Web.Http.Results.JsonResult<List<API_Grupos.Controllers.GroupController.GrupoResponse>>;

            // Afirmaciones: Asegurarse de que se obtiene una lista no nula de grupos
            Assert.IsNotNull(jsonResultList, "El resultado de ObtenerGruposPorNombreVisibleYUsuario no debe ser nulo");

            var gruposList = jsonResultList.Content;
            Assert.IsNotNull(gruposList, "El contenido de ObtenerGruposPorNombreVisibleYUsuario no debe ser nulo");
            Assert.IsTrue(gruposList.Count > 0, "El resultado de ObtenerGruposPorNombreVisibleYUsuario debe contener al menos un grupo");

            string nombreReal = gruposList[0].nombreReal;
            string projectDirectory = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.FullName;
            string imagePath = Path.Combine(projectDirectory, "UnitTest1", "Imagen.jpg");
            byte[] imageBytes = File.ReadAllBytes(imagePath);
            string base64Image = Convert.ToBase64String(imageBytes);
            DateTime fechaHoraActual = DateTime.Now;
            string fechaHoraFormateada = fechaHoraActual.ToString("yyyy-MM-dd HH:mm:ss");

            // Crear el mensaje
            var mensaje = new API_Grupos.Controllers.GroupController.Mensajes
            {
                nombreDeCuenta = "nombre",
                nombreReal = nombreReal,
                texto = "Hola papus miren esta imagen epicarda",
                fechaYHora = fechaHoraFormateada,
                imagen = base64Image,
                token = "TestToken"
            };

            // Act: Llamar al método AñadirMensaje
            var result = await controller.AñadirMensaje(mensaje);
            var result2 = await controller.AñadirMensaje(mensaje);
            var result3 = await controller.AñadirMensaje(mensaje);
            var jsonResult = result as JsonResult<string>;

            // Afirmaciones: Comprobar la respuesta
            Assert.IsNotNull(jsonResult, "Se esperaba un JsonResult.");
            string resultData = jsonResult.Content;
            Assert.IsNotNull(resultData, "El Data en JsonResult no es del tipo esperado.");
            Console.WriteLine($"Response: {resultData}");
            Assert.AreEqual("Se añadio el mensaje correctamente", resultData, "El mensaje no se añadió correctamente.");
        }

        [TestMethod]
        public async Task TestMethod09()
        {
            // Arrange
            var controller = new API_Grupos.Controllers.GroupController();

            // Datos de prueba para el grupo
            var testGroupData = new API_Grupos.Controllers.GroupController.Grupo
            {
                nombreVisible = "Nuevo Nombre Visible",
                nombreDeCuenta = "nombre",
                token = "TestToken"
            };

            string projectDirectory = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.FullName;
            var mensaje = new API_Grupos.Controllers.GroupController.Mensajes
            {
                idMensaje = "1",
                texto = "Menti es alto cepelin el de la imagen",
                token = "TestToken"
            };

            var result = await controller.ActualizarMensaje(mensaje);
            var jsonResult = result as JsonResult<string>;

            // Afirmaciones: Comprobar la respuesta
            Assert.IsNotNull(jsonResult, "Se esperaba un JsonResult.");
            string resultData = jsonResult.Content;
            Assert.IsNotNull(resultData, "El Data en JsonResult no es del tipo esperado.");
            Console.WriteLine($"Response: {resultData}");
            Assert.AreEqual("Se actualizó el mensaje correctamente", resultData, "El mensaje no se edito correctamente.");
        }

        [TestMethod]
        public async Task TestMethod10()
        {
            var controller = new API_Grupos.Controllers.GroupController();
            var testGroupData = new API_Grupos.Controllers.GroupController.Grupo
            {
                nombreVisible = "Nuevo Nombre Visible",
                nombreDeCuenta = "nombre",
                token = "TestToken"
            };
            var obtenerGruposResult = await controller.ObtenerGruposPorNombreVisibleYUsuario(testGroupData);
            var jsonResultList = obtenerGruposResult as System.Web.Http.Results.JsonResult<List<API_Grupos.Controllers.GroupController.GrupoResponse>>;

            Assert.IsNotNull(jsonResultList, "El resultado de ObtenerGruposPorNombreVisibleYUsuario no debe ser nulo");
            var gruposList = jsonResultList.Content;
            Assert.IsNotNull(gruposList, "El contenido de ObtenerGruposPorNombreVisibleYUsuario no debe ser nulo");
            Assert.IsTrue(gruposList.Count > 0, "El resultado de ObtenerGruposPorNombreVisibleYUsuario debe contener al menos un grupo");
            string nombreReal = gruposList[0].nombreReal;

            var messageController = new API_Grupos.Controllers.GroupController();
            API_Grupos.Controllers.GroupController.Mensajes mensaje = new API_Grupos.Controllers.GroupController.Mensajes
            {
                nombreReal = nombreReal,
                token = "TestToken"
            };

            var result = await controller.ObtenerMensajes(mensaje);
            var jsonResult = result as System.Web.Http.Results.JsonResult<List<API_Grupos.Controllers.GroupController.Mensajes>>;

            Assert.IsNotNull(jsonResult, "The result should not be null");
            var mensajesList = jsonResult.Content;
            Console.WriteLine(mensajesList.AsReadOnly());
            Assert.IsTrue(mensajesList.Count > 0, "El resultado debe contener un mensaje");
        }

        [TestMethod]
        public async Task TestMethod11()
        {
            var controller = new API_Grupos.Controllers.GroupController();
            var testGroupData = new API_Grupos.Controllers.GroupController.Grupo
            {
                nombreVisible = "Nuevo Nombre Visible",
                nombreDeCuenta = "nombre",
                token = "TestToken"
            };
            var obtenerGruposResult = await controller.ObtenerGruposPorNombreVisibleYUsuario(testGroupData);
            var jsonResultList = obtenerGruposResult as System.Web.Http.Results.JsonResult<List<API_Grupos.Controllers.GroupController.GrupoResponse>>;

            Assert.IsNotNull(jsonResultList, "El resultado de ObtenerGruposPorNombreVisibleYUsuario no debe ser nulo");
            var gruposList = jsonResultList.Content;
            Assert.IsNotNull(gruposList, "El contenido de ObtenerGruposPorNombreVisibleYUsuario no debe ser nulo");
            Assert.IsTrue(gruposList.Count > 0, "El resultado de ObtenerGruposPorNombreVisibleYUsuario debe contener al menos un grupo");
            string nombreReal = gruposList[0].nombreReal;

            var messageController = new API_Grupos.Controllers.GroupController();
            API_Grupos.Controllers.GroupController.Mensajes mensaje = new API_Grupos.Controllers.GroupController.Mensajes
            {
                idMensaje = "2",
                nombreReal = nombreReal,
                token = "TestToken"
            };

            var result = await controller.ObtenerMensajesMayorID(mensaje);
            var jsonResult = result as System.Web.Http.Results.JsonResult<List<API_Grupos.Controllers.GroupController.Mensajes>>;

            Assert.IsNotNull(jsonResult, "The result should not be null");
            var mensajesList = jsonResult.Content;
            Console.WriteLine(mensajesList.AsReadOnly());
            Assert.IsTrue(mensajesList.Count > 0, "El resultado debe contener un mensaje");
        }

        [TestMethod]
        public async Task TestMethod12()
        {
            // Arrange
            var controller = new API_Grupos.Controllers.GroupController();

            // Datos de prueba para el grupo
            var testGroupData = new API_Grupos.Controllers.GroupController.Grupo
            {
                nombreVisible = "Nuevo Nombre Visible",
                nombreDeCuenta = "nombre",
                token = "TestToken"
            };

            string projectDirectory = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.FullName;
            var mensaje = new API_Grupos.Controllers.GroupController.Mensajes
            {
                idMensaje = "1",
                token = "TestToken"
            };

            var result = await controller.EliminarMensaje(mensaje);
            var jsonResult = result as JsonResult<string>;

            // Afirmaciones: Comprobar la respuesta
            Assert.IsNotNull(jsonResult, "Se esperaba un JsonResult.");
            string resultData = jsonResult.Content;
            Assert.IsNotNull(resultData, "El Data en JsonResult no es del tipo esperado.");
            Console.WriteLine($"Response: {resultData}");
            Assert.AreEqual("Se eliminó el mensaje correctamente", resultData, "El mensaje no se edito correctamente.");
        }

        [TestMethod]
        public async Task TestMethod13()
        {
            var controller = new API_Grupos.Controllers.GroupController();

            API_Grupos.Controllers.GroupController.Grupo testGroupData = new API_Grupos.Controllers.GroupController.Grupo
            {
                nombreVisible = "Nuevo Nombre Visible",
                nombreDeCuenta = "nombre",
                token = "TestToken"
            };

            var obtenerGruposResult = await controller.ObtenerGruposPorNombreVisibleYUsuario(testGroupData);

            var jsonResultList = obtenerGruposResult as System.Web.Http.Results.JsonResult<List<API_Grupos.Controllers.GroupController.GrupoResponse>>;
            Assert.IsNotNull(jsonResultList, "El resultado de ObtenerGruposPorNombreVisibleYUsuario no debe ser nulo");

            // Validar que el contenido de la respuesta no sea nulo y contenga al menos un grupo
            var gruposList = jsonResultList.Content;
            Assert.IsNotNull(gruposList, "El contenido de ObtenerGruposPorNombreVisibleYUsuario no debe ser nulo");
            Assert.IsTrue(gruposList.Count > 0, "El resultado de ObtenerGruposPorNombreVisibleYUsuario debe contener al menos un grupo");

            string nombreReal = gruposList[0].nombreReal;
            API_Grupos.Controllers.GroupController.Grupo groupData = new API_Grupos.Controllers.GroupController.Grupo
            {
                nombreReal = nombreReal,
                token = "TestToken"
            };

            var eliminarResult = await controller.EliminarGrupo(groupData);
            var eliminarJsonResult = eliminarResult as System.Web.Http.Results.JsonResult<string>;

            Assert.IsNotNull(eliminarJsonResult, "El resultado de EliminarGrupo no debe ser nulo");
            string mensajeEliminacion = eliminarJsonResult.Content;
            Assert.AreEqual("Se pudo eliminar", mensajeEliminacion, $"Se esperaba que el grupo fuera eliminado correctamente, pero el mensaje fue: {mensajeEliminacion}");
        }




    }
}
