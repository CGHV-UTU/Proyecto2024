using Microsoft.VisualStudio.TestTools.UnitTesting;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
namespace UnitTest1
{
    [TestClass]
    public class UnitTest1
    {
        [TestCleanup]
        public void Cleanup()
        {
            using (var conn = new MySqlConnection("Server=localhost; database=infini; uID=root; pwd=;"))
            {
                conn.Open();
                var cmdEliminarReportes = new MySqlCommand("DELETE FROM ReporteGrupo", conn);
                cmdEliminarReportes.ExecuteNonQuery();

                var cmdEliminarParticipa = new MySqlCommand("DELETE FROM Participa", conn);
                cmdEliminarParticipa.ExecuteNonQuery();

                var cmdEliminarGrupos = new MySqlCommand("DELETE FROM Grupos ", conn);
                cmdEliminarGrupos.ExecuteNonQuery();

                var cmdEliminarUsuarios = new MySqlCommand("DELETE FROM Usuarios ", conn);
                cmdEliminarUsuarios.ExecuteNonQuery();

                conn.Close();
            }
        }


        public string nombreRealGenerado;
        //Registro al grupo
        [TestMethod]
        public void TestMethod1_PRRegistrarGrupo()
        {
            string nombreVisibleUsuario = "Usuario Visible";
            string email = "usuario@test.com";
            string foto = "foto.jpg";
            string configuracionesUsuario = "configuración inicial";
            string genero = "no especificado";
            string estadoDeCuenta = "activo";
            DateTime fechaDeNacimiento = new DateTime(1990, 1, 15);

            using (var conn = new MySqlConnection("Server=localhost; database=infini; uID=root; pwd=;"))
            {
                conn.Open();
                var cmdUsuario = new MySqlCommand(@"
            INSERT INTO Usuarios (nombreDeCuenta, nombreVisible, email, descripcion, foto, configuraciones, genero, fechaDeNacimiento, estadoDeCuenta) 
            VALUES (@nombreDeCuenta, @nombreVisibleUsuario, @email, @descripcionUsuario, @fotoUsuario, @configuracionesUsuario, @generoUsuario, @fechaDeNacimiento, @estadoDeCuenta)
            ON DUPLICATE KEY UPDATE nombreDeCuenta = nombreDeCuenta;", conn);
                cmdUsuario.Parameters.AddWithValue("@nombreDeCuenta", "UsuarioTest");
                cmdUsuario.Parameters.AddWithValue("@nombreVisibleUsuario", nombreVisibleUsuario);
                cmdUsuario.Parameters.AddWithValue("@email", email);
                cmdUsuario.Parameters.AddWithValue("@descripcionUsuario", "Descripción de usuario");
                cmdUsuario.Parameters.AddWithValue("@fotoUsuario", foto);
                cmdUsuario.Parameters.AddWithValue("@configuracionesUsuario", configuracionesUsuario);
                cmdUsuario.Parameters.AddWithValue("@generoUsuario", genero);
                cmdUsuario.Parameters.AddWithValue("@fechaDeNacimiento", fechaDeNacimiento);
                cmdUsuario.Parameters.AddWithValue("@estadoDeCuenta", estadoDeCuenta);
                cmdUsuario.ExecuteNonQuery();
                conn.Close();

                string respuestaEsperada = "Registro correcto";
            API_Grupos.Controllers.GroupController controller = new API_Grupos.Controllers.GroupController();
            API_Grupos.Controllers.GroupController.Grupo grupo = new API_Grupos.Controllers.GroupController.Grupo()
            {
                nombreVisible = "Grupo Visible",
                configuracion = "Configuración del grupo",
                descripcion = "Descripción del grupo",
                imagen = "grupo.jpg",
                nombreDeCuenta = "UsuarioTest"
            };

            var resultadoGrupoJson = controller.PRRegistrarGrupo(grupo);
            var resultadoGrupo = JsonConvert.DeserializeObject<string>(resultadoGrupoJson);
            Assert.AreEqual("Registro correcto", resultadoGrupo);


            }
            Cleanup();
        }

        [TestMethod]
        public void TestMethod2_ObtenerGruposPorNombreVisibleYUsuario()
        {
            string nombreVisible = "Grupo Visible";
            string nombreDeCuenta = "UsuarioTest";
            API_Grupos.Controllers.GroupController controller = new API_Grupos.Controllers.GroupController();

            var resultadoJson = controller.PRObtenerGruposPorNombreVisibleYUsuario(nombreVisible, nombreDeCuenta);
            string resultadoStr = resultadoJson.ToString();
            if (resultadoStr.StartsWith("["))
            {
                var resultado = JsonConvert.DeserializeObject<List<dynamic>>(resultadoStr);

                Assert.IsNotNull(resultado);
                Assert.IsTrue(resultado.Count > 0);
                Assert.AreEqual(nombreVisible, resultado[0].nombreVisible.ToString());
            }
            else if (resultadoStr.StartsWith("\""))
            {
                resultadoStr = JsonConvert.DeserializeObject<string>(resultadoStr);
                Assert.AreEqual("No se encontraron Grupos para el usuario especificado", resultadoStr);
            }
            else
            {
                Assert.IsTrue(resultadoStr.StartsWith("Error al obtener los Grupos:"));
            }
            Cleanup();
        }

        [TestMethod]
        public void TestMethod3_PRObtenerGrupo()
        {
            string respuestaEsperada = "Registro correcto";
            API_Grupos.Controllers.GroupController controller = new API_Grupos.Controllers.GroupController();
            API_Grupos.Controllers.GroupController.Grupo grupo = new API_Grupos.Controllers.GroupController.Grupo()
            {
                nombreVisible = "Grupo Visible",
                configuracion = "Configuración del grupo",
                descripcion = "Descripción del grupo",
                imagen = "grupo.jpg"
            };
            var resultadoGrupoJson = controller.PRRegistrarGrupo(grupo);
            var resultadoGrupo = JsonConvert.DeserializeObject<string>(resultadoGrupoJson);
            Assert.AreEqual("Registro correcto", resultadoGrupo);
            string nombreRealGenerado = grupo.nombreReal;

            var resultadoJson = controller.PRObtenerGrupo(nombreRealGenerado);
            var resultado = JsonConvert.DeserializeObject<dynamic>(resultadoJson.ToString());

            Assert.IsNotNull(resultado);
            Assert.AreEqual(nombreRealGenerado, resultado.nombreGrupo.ToString());
            Assert.AreEqual("Grupo Visible", resultado.nombreVisible.ToString());
            Cleanup();
        }

        [TestMethod]
        public void TestMethod4_PREditarGrupo()
        {
            API_Grupos.Controllers.GroupController controller = new API_Grupos.Controllers.GroupController();
            API_Grupos.Controllers.GroupController.Grupo grupo = new API_Grupos.Controllers.GroupController.Grupo()
            {
                nombreVisible = "Grupo Visible",
                configuracion = "Configuración del grupo",
                descripcion = "Descripción del grupo",
                imagen = "grupo.jpg"
            };

            var resultadoGrupoJson = controller.PRRegistrarGrupo(grupo);
            var resultadoGrupo = JsonConvert.DeserializeObject<string>(resultadoGrupoJson);

            Assert.AreEqual("Registro correcto", resultadoGrupo);

            string nombreRealGenerado = grupo.nombreReal;

            API_Grupos.Controllers.GroupController.Grupo grupoEditado = new API_Grupos.Controllers.GroupController.Grupo()
            {
                nombreReal = nombreRealGenerado,
                nombreVisible = "GrupoVisibleEditado",
                configuracion = "Configuración del grupo editada",
                descripcion = "Descripción del grupo editada",
                imagen = "grupo_editado.jpg"
            };

            var resultadoEditarGrupoJson = controller.PREditarGrupo(grupoEditado);
            var resultadoEditarGrupo = JsonConvert.DeserializeObject<string>(resultadoEditarGrupoJson);

            Assert.AreEqual("Se editó el grupo correctamente", resultadoEditarGrupo);
            Cleanup();
        }

       

        [TestMethod]
        public void TestMethod6_PRObtenerGruposPorUsuario()
        {
            string nombreDeCuenta = "UsuarioTest";
            string nombreRealGenerado;


            using (var conn = new MySqlConnection("Server=localhost; database=infini; uID=root; pwd=;"))
            {
                conn.Open();

                // Crear el usuario en la tabla Usuarios si no existe
                var cmdInsertarUsuario = new MySqlCommand(@"
            INSERT INTO Usuarios (nombreDeCuenta, nombreVisible, email, descripcion, foto, configuraciones, genero, fechaDeNacimiento, estadoDeCuenta) 
            VALUES (@nombreDeCuenta, @nombreVisible, @correo, @descripcion, @foto, @configuraciones, @genero, @fechaNacimiento, @estado)
            ON DUPLICATE KEY UPDATE nombreDeCuenta = nombreDeCuenta;", conn);
                cmdInsertarUsuario.Parameters.AddWithValue("@nombreDeCuenta", nombreDeCuenta);
                cmdInsertarUsuario.Parameters.AddWithValue("@nombreVisible", "Usuario Visible");
                cmdInsertarUsuario.Parameters.AddWithValue("@correo", "usuario@test.com");
                cmdInsertarUsuario.Parameters.AddWithValue("@descripcion", "Descripción de usuario");
                cmdInsertarUsuario.Parameters.AddWithValue("@foto", "foto.jpg");
                cmdInsertarUsuario.Parameters.AddWithValue("@configuraciones", "configuración inicial");
                cmdInsertarUsuario.Parameters.AddWithValue("@genero", "no especificado");
                cmdInsertarUsuario.Parameters.AddWithValue("@fechaNacimiento", new DateTime(1990, 1, 15));
                cmdInsertarUsuario.Parameters.AddWithValue("@estado", "activo");
                cmdInsertarUsuario.ExecuteNonQuery();


                var cmdInsertarGrupo = new MySqlCommand(@"
            INSERT INTO Grupos (nombreReal, nombreVisible, configuracion, descripcion, foto) 
            VALUES ('grupo_test', 'Grupo de Prueba', 'Configuración de prueba', 'Descripción de prueba', 'grupo.jpg')
            ON DUPLICATE KEY UPDATE nombreReal = nombreReal;", conn);
                cmdInsertarGrupo.ExecuteNonQuery();


                var cmdInsertarParticipa = new MySqlCommand(@"
            INSERT INTO Participa (nombreReal, nombreDeCuenta, rol) 
            VALUES ('grupo_test', @nombreDeCuenta, 'a')
            ON DUPLICATE KEY UPDATE rol = 'a';", conn);
                cmdInsertarParticipa.Parameters.AddWithValue("@nombreDeCuenta", nombreDeCuenta);
                cmdInsertarParticipa.ExecuteNonQuery();

                conn.Close();
            }
            API_Grupos.Controllers.GroupController controller = new API_Grupos.Controllers.GroupController();

            var resultadoJson = controller.PRObtenerGruposPorUsuario(nombreDeCuenta);
            var resultado = JsonConvert.DeserializeObject<List<dynamic>>(resultadoJson.ToString());

            Assert.IsNotNull(resultado);
            Assert.IsTrue(resultado.Count > 0);
            Cleanup();
        }


        [TestMethod]
        public void TestMethod7_PRAgregarUsuarioAGrupo()
        {
            API_Grupos.Controllers.GroupController controller = new API_Grupos.Controllers.GroupController();
            string nombreUsuario = "UsuarioTest";

            using (var conn = new MySqlConnection("Server=localhost; database=infini; uID=root; pwd=;"))
            {
                conn.Open();

                // Crear el usuario en la tabla Usuarios si no existe
                var cmdInsertarUsuario = new MySqlCommand(@"
            INSERT INTO Usuarios (nombreDeCuenta, nombreVisible, email, descripcion, foto, configuraciones, genero, fechaDeNacimiento, estadoDeCuenta) 
            VALUES (@nombreDeCuenta, @nombreVisible, @correo, @descripcion, @foto, @configuraciones, @genero, @fechaNacimiento, @estado)
            ON DUPLICATE KEY UPDATE nombreDeCuenta = nombreDeCuenta;", conn);
                cmdInsertarUsuario.Parameters.AddWithValue("@nombreDeCuenta", nombreUsuario);
                cmdInsertarUsuario.Parameters.AddWithValue("@nombreVisible", "Usuario Visible");
                cmdInsertarUsuario.Parameters.AddWithValue("@correo", "usuario@test.com");
                cmdInsertarUsuario.Parameters.AddWithValue("@descripcion", "Descripción de usuario");
                cmdInsertarUsuario.Parameters.AddWithValue("@foto", "foto.jpg");
                cmdInsertarUsuario.Parameters.AddWithValue("@configuraciones", "configuración inicial");
                cmdInsertarUsuario.Parameters.AddWithValue("@genero", "no especificado");
                cmdInsertarUsuario.Parameters.AddWithValue("@fechaNacimiento", new DateTime(1990, 1, 15));
                cmdInsertarUsuario.Parameters.AddWithValue("@estado", "activo");
                cmdInsertarUsuario.ExecuteNonQuery();
                conn.Close();
            }

            // Registrar el grupo
            API_Grupos.Controllers.GroupController.Grupo grupo = new API_Grupos.Controllers.GroupController.Grupo()
            {
                nombreVisible = "Grupo Visible",
                configuracion = "Configuración del grupo",
                descripcion = "Descripción del grupo",
                imagen = "grupo.jpg"
            };
            var resultadoGrupoJson = controller.PRRegistrarGrupo(grupo);
            var resultadoGrupo = JsonConvert.DeserializeObject<string>(resultadoGrupoJson);
            Assert.AreEqual("Registro correcto", resultadoGrupo);
            string nombreRealGenerado = grupo.nombreReal;

            var resultadoAgregarUsuarioJson = controller.PRAgregarUsuarioAGrupo(nombreUsuario, nombreRealGenerado);
            var resultadoAgregarUsuario = JsonConvert.DeserializeObject<string>(resultadoAgregarUsuarioJson);
            Assert.AreEqual("Usuario agregado al grupo", resultadoAgregarUsuario);

            Cleanup();
        }



        [TestMethod]
        public void TestMethod8_PRAgregarUsuarioAGrupoUG()
        {
            API_Grupos.Controllers.GroupController controller = new API_Grupos.Controllers.GroupController();
            API_Grupos.Controllers.GroupController.Grupo grupo = new API_Grupos.Controllers.GroupController.Grupo()
            {
                nombreVisible = "Grupo Visible",
                configuracion = "Configuración del grupo",
                descripcion = "Descripción del grupo",
                imagen = "grupo.jpg"
            };
            var resultadoGrupoJson = controller.PRRegistrarGrupo(grupo);
            var resultadoGrupo = JsonConvert.DeserializeObject<string>(resultadoGrupoJson);
            Assert.AreEqual("Registro correcto", resultadoGrupo);
            string nombreRealGenerado = grupo.nombreReal;
            string admin = "AdminTest";
            string nombreUsuario = "UsuarioTest";
            using (var conn = new MySqlConnection("Server=localhost; database=infini; uID=root; pwd=;"))
            {
                conn.Open();

                // Crear el usuario administrador en la tabla Usuarios si no existe
                var cmdInsertarAdmin = new MySqlCommand(@"
            INSERT INTO Usuarios (nombreDeCuenta, nombreVisible, email, descripcion, foto, configuraciones, genero, fechaDeNacimiento, estadoDeCuenta) 
            VALUES (@nombreDeCuenta, @nombreVisible, @correo, @descripcion, @foto, @configuraciones, @genero, @fechaNacimiento, @estado)
            ON DUPLICATE KEY UPDATE nombreDeCuenta = nombreDeCuenta;", conn);
                cmdInsertarAdmin.Parameters.AddWithValue("@nombreDeCuenta", admin);
                cmdInsertarAdmin.Parameters.AddWithValue("@nombreVisible", "Administrador");
                cmdInsertarAdmin.Parameters.AddWithValue("@correo", "admin@test.com");
                cmdInsertarAdmin.Parameters.AddWithValue("@descripcion", "Descripción del administrador");
                cmdInsertarAdmin.Parameters.AddWithValue("@foto", "admin.jpg");
                cmdInsertarAdmin.Parameters.AddWithValue("@configuraciones", "configuración inicial");
                cmdInsertarAdmin.Parameters.AddWithValue("@genero", "no especificado");
                cmdInsertarAdmin.Parameters.AddWithValue("@fechaNacimiento", new DateTime(1990, 1, 15));
                cmdInsertarAdmin.Parameters.AddWithValue("@estado", "activo");
                cmdInsertarAdmin.ExecuteNonQuery();
                // Crear el usuario en la tabla Usuarios si no existe
                var cmdInsertarUsuario = new MySqlCommand(@"
            INSERT INTO Usuarios (nombreDeCuenta, nombreVisible, email, descripcion, foto, configuraciones, genero, fechaDeNacimiento, estadoDeCuenta) 
            VALUES (@nombreDeCuenta, @nombreVisible, @correo, @descripcion, @foto, @configuraciones, @genero, @fechaNacimiento, @estado)
            ON DUPLICATE KEY UPDATE nombreDeCuenta = nombreDeCuenta;", conn);
                cmdInsertarUsuario.Parameters.AddWithValue("@nombreDeCuenta", nombreUsuario);
                cmdInsertarUsuario.Parameters.AddWithValue("@nombreVisible", "Usuario Visible");
                cmdInsertarUsuario.Parameters.AddWithValue("@correo", "usuario@test.com");
                cmdInsertarUsuario.Parameters.AddWithValue("@descripcion", "Descripción de usuario");
                cmdInsertarUsuario.Parameters.AddWithValue("@foto", "usuario.jpg");
                cmdInsertarUsuario.Parameters.AddWithValue("@configuraciones", "configuración inicial");
                cmdInsertarUsuario.Parameters.AddWithValue("@genero", "no especificado");
                cmdInsertarUsuario.Parameters.AddWithValue("@fechaNacimiento", new DateTime(1990, 1, 15));
                cmdInsertarUsuario.Parameters.AddWithValue("@estado", "activo");
                cmdInsertarUsuario.ExecuteNonQuery();
                // Asignar permisos de administrador al usuario admin en el grupo
                var cmdPermiso = new MySqlCommand(@"
            INSERT INTO Participa (nombreDeCuenta, nombreReal, rol) 
            VALUES (@nombreAdmin, @nombreGrupo, 'a')
            ON DUPLICATE KEY UPDATE rol = 'a';", conn);
                cmdPermiso.Parameters.AddWithValue("@nombreAdmin", admin);
                cmdPermiso.Parameters.AddWithValue("@nombreGrupo", nombreRealGenerado);
                cmdPermiso.ExecuteNonQuery();

                conn.Close();
            }
            var resultadoAgregarUsuarioJson = controller.PRAgregarUsuarioAGrupoUG(admin, nombreUsuario, nombreRealGenerado);
            var resultadoAgregarUsuario = JsonConvert.DeserializeObject<string>(resultadoAgregarUsuarioJson);
            Assert.AreEqual("Usuario agregado al grupo", resultadoAgregarUsuario);

            Cleanup();
        }

        [TestMethod]
        public void TestMethod9_EliminarUsuarioDeGrupo()
        {
            // Arrange
            API_Grupos.Controllers.GroupController controller = new API_Grupos.Controllers.GroupController();

            // Registrar un grupo
            API_Grupos.Controllers.GroupController.Grupo grupo = new API_Grupos.Controllers.GroupController.Grupo()
            {
                nombreVisible = "Grupo de Prueba",
                configuracion = "Configuración de prueba",
                descripcion = "Descripción de prueba",
                imagen = "grupo.jpg"
            };

            var resultadoGrupoJson = controller.PRRegistrarGrupo(grupo);
            var resultadoGrupo = JsonConvert.DeserializeObject<string>(resultadoGrupoJson);

            Assert.AreEqual("Registro correcto", resultadoGrupo);

            string nombreRealGenerado = grupo.nombreReal;

            string nombreUsuario = "UsuarioDePrueba";
            using (var conn = new MySqlConnection("Server=localhost; database=infini; uID=root; pwd=;"))
            {
                conn.Open();
                var cmdCrearUsuario = new MySqlCommand(@"
        INSERT INTO Usuarios (nombreDeCuenta, nombreVisible, email, descripcion, foto, configuraciones, genero, fechaDeNacimiento, estadoDeCuenta) 
        VALUES (@nombreDeCuenta, @nombreVisible, @correo, @descripcion, @foto, @configuraciones, @genero, @fechaNacimiento, @estado)
        ON DUPLICATE KEY UPDATE nombreDeCuenta = nombreDeCuenta;", conn);
                cmdCrearUsuario.Parameters.AddWithValue("@nombreDeCuenta", nombreUsuario);
                cmdCrearUsuario.Parameters.AddWithValue("@nombreVisible", "Usuario Visible");
                cmdCrearUsuario.Parameters.AddWithValue("@correo", "usuario@prueba.com");
                cmdCrearUsuario.Parameters.AddWithValue("@descripcion", "Descripción del usuario");
                cmdCrearUsuario.Parameters.AddWithValue("@foto", "usuario.jpg");
                cmdCrearUsuario.Parameters.AddWithValue("@configuraciones", "configuración inicial");
                cmdCrearUsuario.Parameters.AddWithValue("@genero", "no especificado");
                cmdCrearUsuario.Parameters.AddWithValue("@fechaNacimiento", DateTime.Now);
                cmdCrearUsuario.Parameters.AddWithValue("@estado", "activo");
                cmdCrearUsuario.ExecuteNonQuery();

                var cmdAgregarUsuario = new MySqlCommand(@"
        INSERT INTO Participa (nombreDeCuenta, nombreReal, rol) 
        VALUES (@nombreDeCuenta, @nombreReal, 'm');", conn);
                cmdAgregarUsuario.Parameters.AddWithValue("@nombreDeCuenta", nombreUsuario);
                cmdAgregarUsuario.Parameters.AddWithValue("@nombreReal", nombreRealGenerado);
                cmdAgregarUsuario.ExecuteNonQuery();

                conn.Close();
            }

            var resultadoEliminarUsuario = controller.PREliminarUsuarioDeGrupo(nombreRealGenerado, nombreUsuario);
            Assert.IsNotNull(resultadoEliminarUsuario, "El resultado de la eliminación es null");
            string resultadoEliminarUsuarioStr = resultadoEliminarUsuario as string;
            Assert.AreEqual("Grupo eliminado del usuario correctamente", resultadoEliminarUsuarioStr);
            Cleanup();
        }
        [TestMethod]
        public void TestMethoda1_PRReportarGrupo()
        {
            string respuestaEsperada = JsonConvert.SerializeObject("Reporte correcto");
            API_Grupos.Controllers.GroupController controller = new API_Grupos.Controllers.GroupController();

            API_Grupos.Controllers.GroupController.Grupo grupo = new API_Grupos.Controllers.GroupController.Grupo()
            {
                nombreVisible = "Grupo Visible",
                configuracion = "Configuración del grupo",
                descripcion = "Descripción del grupo",
                imagen = "grupo.jpg"
            };

            var resultadoGrupoJson = controller.PRRegistrarGrupo(grupo);
            var resultadoGrupo = JsonConvert.DeserializeObject<string>(resultadoGrupoJson);
            Assert.AreEqual("Registro correcto", resultadoGrupo);
            string nombreRealGenerado = grupo.nombreReal;
            var reporte = new API_Grupos.Controllers.GroupController.Reporte()
            {
                numeroReporte = 1,
                nombreReal = nombreRealGenerado,
                tipo = "Infracción",
                descripcion = "Descripción del reporte"
            };
            var resultadoJson = controller.PRReportarGrupo(reporte);
            string resultadoStr = resultadoJson as string;
            Assert.IsNotNull(resultadoStr, "El resultado de la operación es null");
            Assert.AreEqual(respuestaEsperada, resultadoStr);
            Cleanup();
        }
        [TestMethod]
        public void TestMethoda2_PRActualizarMensajes()
        {
            API_Grupos.Controllers.GroupController controller = new API_Grupos.Controllers.GroupController();
            API_Grupos.Controllers.GroupController.Grupo grupo = new API_Grupos.Controllers.GroupController.Grupo()
            {
                nombreVisible = "Grupo Visible",
                configuracion = "Configuración del grupo",
                descripcion = "Descripción del grupo",
                imagen = "grupo.jpg"
            };

            var resultadoGrupoJson = controller.PRRegistrarGrupo(grupo);
            var resultadoGrupo = JsonConvert.DeserializeObject<string>(resultadoGrupoJson);
            Assert.AreEqual("Registro correcto", resultadoGrupo);

            string nombreRealGenerado = grupo.nombreReal;
            string chatGrupal = "Hola papus";      
            var resultadoEditarGrupoJson = controller.PRActualizarMensajes(nombreRealGenerado,chatGrupal);
            var resultadoEditarGrupo = JsonConvert.DeserializeObject<string>(resultadoEditarGrupoJson);
            Assert.AreEqual("Se actualizaron los mensajes correctamente", resultadoEditarGrupo);
            Cleanup();
        }

        [TestMethod]
        public void TestMethoda3_PRObtenerMensajes()
        {
            string respuestaEsperada = "Registro correcto";
            API_Grupos.Controllers.GroupController controller = new API_Grupos.Controllers.GroupController();
            API_Grupos.Controllers.GroupController.Grupo grupo = new API_Grupos.Controllers.GroupController.Grupo()
            {
                nombreVisible = "Grupo Visible",
                configuracion = "Configuración del grupo",
                descripcion = "Descripción del grupo",
                imagen = "grupo.jpg"
            };
            var resultadoGrupoJson = controller.PRRegistrarGrupo(grupo);
            var resultadoGrupo = JsonConvert.DeserializeObject<string>(resultadoGrupoJson);
            Assert.AreEqual("Registro correcto", resultadoGrupo);
            string nombreRealGenerado = grupo.nombreReal;
            string chatGrupal = "Hola papus";
            var resultadoEditarGrupoJson = controller.PRActualizarMensajes(nombreRealGenerado, chatGrupal);
            var resultadoEditarGrupo = JsonConvert.DeserializeObject<string>(resultadoEditarGrupoJson);
            Assert.AreEqual("Se actualizaron los mensajes correctamente", resultadoEditarGrupo);

            var resultadoJson = controller.PRObtenerMensajes(nombreRealGenerado);
            var resultado = JsonConvert.DeserializeObject<string>(resultadoJson);
            Assert.AreEqual("Hola papus", resultado.ToString());
            Cleanup();
        }
    }
}
