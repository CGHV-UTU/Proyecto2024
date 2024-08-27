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

                // Primero eliminar los registros de ReporteGrupo
                var cmdEliminarReportes = new MySqlCommand("DELETE FROM ReporteGrupo WHERE nombreReal = @nombreReal", conn);
                cmdEliminarReportes.Parameters.AddWithValue("@nombreReal", "NombreRealQueUsasteEnLaPrueba");
                cmdEliminarReportes.ExecuteNonQuery();

                // Luego eliminar los registros de Participa (asociaciones de usuarios con grupos)
                var cmdEliminarParticipa = new MySqlCommand("DELETE FROM Participa WHERE nombreReal = @nombreReal", conn);
                cmdEliminarParticipa.Parameters.AddWithValue("@nombreReal", "NombreRealQueUsasteEnLaPrueba");
                cmdEliminarParticipa.ExecuteNonQuery();

                // Eliminar los registros de Grupos
                var cmdEliminarGrupos = new MySqlCommand("DELETE FROM Grupos WHERE nombreReal = @nombreReal", conn);
                cmdEliminarGrupos.Parameters.AddWithValue("@nombreReal", "NombreRealQueUsasteEnLaPrueba");
                cmdEliminarGrupos.ExecuteNonQuery();

                // Finalmente, eliminar los registros de Usuarios
                var cmdEliminarUsuarios = new MySqlCommand("DELETE FROM Usuarios WHERE nombreDeCuenta = @nombreDeCuenta", conn);
                cmdEliminarUsuarios.Parameters.AddWithValue("@nombreDeCuenta", "UsuarioDePrueba");
                cmdEliminarUsuarios.ExecuteNonQuery();

                conn.Close();
            }
        }


        public string nombreRealGenerado;
        //Registro al grupo
        [TestMethod]
        public void TestMethod1_PRRegistrarGrupo()
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

            nombreRealGenerado = grupo.nombreReal;
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
            }

            API_Grupos.Controllers.GroupController.UsuarioGrupo usuarioGrupo = new API_Grupos.Controllers.GroupController.UsuarioGrupo()
            {
                nombreReal = nombreRealGenerado,
                nombreDeCuenta = "UsuarioTest"
            };

            var resultadoJson = controller.PRRegistrarGrupoUG(usuarioGrupo);
            var resultado = JsonConvert.DeserializeObject<string>(resultadoJson);
            Assert.AreEqual(respuestaEsperada, resultado);
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
            using (var conn = new MySqlConnection("Server=localhost; database=infini; uID=root; pwd=;"))
            {
                conn.Open();
                var cmdVerificacion = new MySqlCommand("SELECT nombreVisible, configuracion, descripcion, foto FROM Grupos WHERE nombreReal = @nombreReal", conn);
                cmdVerificacion.Parameters.AddWithValue("@nombreReal", nombreRealGenerado);

                using (var reader = cmdVerificacion.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        Assert.AreEqual("GrupoVisibleEditado", reader["nombreVisible"].ToString());
                        Assert.AreEqual("Configuración del grupo editada", reader["configuracion"].ToString());
                        Assert.AreEqual("Descripción del grupo editada", reader["descripcion"].ToString());
                        Assert.AreEqual("grupo_editado.jpg", reader["foto"].ToString());
                    }
                    else
                    {
                        Assert.Fail("El grupo no se encontró en la base de datos.");
                    }
                }
            }
            Cleanup();
        }

        [TestMethod]
        public void TestMethod5_PREditarGrupoUG()
        {
            API_Grupos.Controllers.GroupController controller = new API_Grupos.Controllers.GroupController();
            API_Grupos.Controllers.GroupController.Grupo grupo = new API_Grupos.Controllers.GroupController.Grupo()
            {
                nombreVisible = "Grupo Visible",
                configuracion = "Configuración del grupo",
                descripcion = "Descripción del grupo",
                imagen = "grupo.jpg"
            };

            // Registrar el grupo y obtener el nombreReal generado
            var resultadoGrupoJson = controller.PRRegistrarGrupo(grupo);
            var resultadoGrupo = JsonConvert.DeserializeObject<string>(resultadoGrupoJson);

            // Verificamos que el grupo se haya registrado correctamente
            Assert.AreEqual("Registro correcto", resultadoGrupo);

            // Obtener el nombreReal generado automáticamente
            string nombreRealGenerado = grupo.nombreReal;

            string usuario = "UsuarioTest";
            using (var conn = new MySqlConnection("Server=localhost; database=infini; uID=root; pwd=;"))
            {
                conn.Open();

                var cmdInsertarUsuario = new MySqlCommand(@"
            INSERT INTO Usuarios (nombreDeCuenta, nombreVisible, email, descripcion, foto, configuraciones, genero, fechaDeNacimiento, estadoDeCuenta) 
            VALUES (@nombreDeCuenta, @nombreVisible, @correo, @descripcion, @foto, @configuraciones, @genero, @fechaNacimiento, @estado)
            ON DUPLICATE KEY UPDATE nombreDeCuenta = nombreDeCuenta;", conn);
                cmdInsertarUsuario.Parameters.AddWithValue("@nombreDeCuenta", usuario);
                cmdInsertarUsuario.Parameters.AddWithValue("@nombreVisible", "Usuario Visible");
                cmdInsertarUsuario.Parameters.AddWithValue("@correo", "usuario@test.com");
                cmdInsertarUsuario.Parameters.AddWithValue("@descripcion", "Descripción de usuario");
                cmdInsertarUsuario.Parameters.AddWithValue("@foto", "foto.jpg");
                cmdInsertarUsuario.Parameters.AddWithValue("@configuraciones", "configuración inicial");
                cmdInsertarUsuario.Parameters.AddWithValue("@genero", "no especificado");
                cmdInsertarUsuario.Parameters.AddWithValue("@fechaNacimiento", new DateTime(1990, 1, 15));
                cmdInsertarUsuario.Parameters.AddWithValue("@estado", "activo");
                cmdInsertarUsuario.ExecuteNonQuery();

                var cmdPermiso = new MySqlCommand(@"
            INSERT INTO Participa (nombreReal, nombreDeCuenta, rol) 
            VALUES (@nombreReal, @nombreDeCuenta, 'a')
            ON DUPLICATE KEY UPDATE rol = 'a';", conn);
                cmdPermiso.Parameters.AddWithValue("@nombreReal", nombreRealGenerado);
                cmdPermiso.Parameters.AddWithValue("@nombreDeCuenta", usuario);
                cmdPermiso.ExecuteNonQuery();

                conn.Close();
            }

            API_Grupos.Controllers.GroupController.Grupo grupoEditado = new API_Grupos.Controllers.GroupController.Grupo()
            {
                nombreReal = nombreRealGenerado,
                nombreVisible = "GrupoVisibleEditado",
                configuracion = "Configuración del grupo editada",
                descripcion = "Descripción del grupo editada",
                imagen = "grupo_editado.jpg"
            };

            var resultadoEditarGrupoJson = controller.PREditarGrupoUG(grupoEditado, usuario);
            var resultadoEditarGrupo = JsonConvert.DeserializeObject<string>(resultadoEditarGrupoJson);

            // Verificar que la respuesta sea la esperada
            Assert.AreEqual("Se editó el grupo correctamente", resultadoEditarGrupo);

            // Verificar en la base de datos si los cambios se realizaron correctamente
            using (var conn = new MySqlConnection("Server=localhost; database=infini; uID=root; pwd=;"))
            {
                conn.Open();
                var cmdVerificacion = new MySqlCommand("SELECT nombreVisible, configuracion, descripcion, foto FROM Grupos WHERE nombreReal = @nombreReal", conn);
                cmdVerificacion.Parameters.AddWithValue("@nombreReal", nombreRealGenerado);

                using (var reader = cmdVerificacion.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        Assert.AreEqual("GrupoVisibleEditado", reader["nombreVisible"].ToString());
                        Assert.AreEqual("Configuración del grupo editada", reader["configuracion"].ToString());
                        Assert.AreEqual("Descripción del grupo editada", reader["descripcion"].ToString());
                        Assert.AreEqual("grupo_editado.jpg", reader["foto"].ToString());
                    }
                    else
                    {
                        Assert.Fail("El grupo no se encontró en la base de datos.");
                    }
                }
            }

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

            // Obtener el nombre real generado del grupo
            string nombreRealGenerado = grupo.nombreReal;

            // Intentar agregar el usuario al grupo
            var resultadoAgregarUsuarioJson = controller.PRAgregarUsuarioAGrupo(nombreUsuario, nombreRealGenerado);
            var resultadoAgregarUsuario = JsonConvert.DeserializeObject<string>(resultadoAgregarUsuarioJson);
            Assert.AreEqual("Usuario agregado al grupo", resultadoAgregarUsuario);

            // Verificar en la base de datos si el usuario fue agregado al grupo
            using (var conn = new MySqlConnection("Server=localhost; database=infini; uID=root; pwd=;"))
            {
                conn.Open();
                var cmdVerificacion = new MySqlCommand("SELECT * FROM Participa WHERE nombreDeCuenta = @nombreUsuario AND nombreReal = @nombreGrupo", conn);
                cmdVerificacion.Parameters.AddWithValue("@nombreUsuario", nombreUsuario);
                cmdVerificacion.Parameters.AddWithValue("@nombreGrupo", nombreRealGenerado);

                using (var reader = cmdVerificacion.ExecuteReader())
                {
                    Assert.IsTrue(reader.HasRows, "El usuario no fue agregado al grupo.");
                }
            }
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
            // Act
            // Llamar al método para agregar el usuario al grupo por el administrador
            var resultadoAgregarUsuarioJson = controller.PRAgregarUsuarioAGrupoUG(admin, nombreUsuario, nombreRealGenerado);
            var resultadoAgregarUsuario = JsonConvert.DeserializeObject<string>(resultadoAgregarUsuarioJson);

            // Assert
            // Verificar que la respuesta sea la esperada
            Assert.AreEqual("Usuario agregado al grupo", resultadoAgregarUsuario);

            // Verificar en la base de datos si el usuario fue agregado al grupo correctamente
            using (var conn = new MySqlConnection("Server=localhost; database=infini; uID=root; pwd=;"))
            {
                conn.Open();
                var cmdVerificacion = new MySqlCommand("SELECT * FROM Participa WHERE nombreDeCuenta = @nombreUsuario AND nombreReal = @nombreGrupo", conn);
                cmdVerificacion.Parameters.AddWithValue("@nombreUsuario", nombreUsuario);
                cmdVerificacion.Parameters.AddWithValue("@nombreGrupo", nombreRealGenerado);

                using (var reader = cmdVerificacion.ExecuteReader())
                {
                    Assert.IsTrue(reader.HasRows, "El usuario no fue agregado al grupo.");
                }
            }
            // Limpiar los datos de prueba después de la ejecución
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

            // Verificar que el grupo se haya registrado correctamente
            Assert.AreEqual("Registro correcto", resultadoGrupo);

            // Obtener el nombreReal generado automáticamente
            string nombreRealGenerado = grupo.nombreReal;

            // Crear un usuario en la tabla Usuarios
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

                // Agregar el usuario al grupo
                var cmdAgregarUsuario = new MySqlCommand(@"
        INSERT INTO Participa (nombreDeCuenta, nombreReal, rol) 
        VALUES (@nombreDeCuenta, @nombreReal, 'm');", conn);
                cmdAgregarUsuario.Parameters.AddWithValue("@nombreDeCuenta", nombreUsuario);
                cmdAgregarUsuario.Parameters.AddWithValue("@nombreReal", nombreRealGenerado);
                cmdAgregarUsuario.ExecuteNonQuery();

                conn.Close();
            }

            // Act
            // Llamar al método para eliminar al usuario del grupo
            var resultadoEliminarUsuario = controller.PREliminarUsuarioDeGrupo(nombreRealGenerado, nombreUsuario);

            // Asegúrate de que resultadoEliminarUsuario no es null
            Assert.IsNotNull(resultadoEliminarUsuario, "El resultado de la eliminación es null");

            // Convertir a string explícitamente
            string resultadoEliminarUsuarioStr = resultadoEliminarUsuario as string;

            // Assert
            // Verificar que la respuesta sea la esperada
            Assert.AreEqual("Grupo eliminado del usuario correctamente", resultadoEliminarUsuarioStr);

            // Verificar en la base de datos si el usuario fue eliminado del grupo correctamente
            using (var conn = new MySqlConnection("Server=localhost; database=infini; uID=root; pwd=;"))
            {
                conn.Open();
                var cmdVerificacion = new MySqlCommand("SELECT COUNT(*) FROM Participa WHERE nombreDeCuenta = @nombreUsuario AND nombreReal = @nombreGrupo", conn);
                cmdVerificacion.Parameters.AddWithValue("@nombreUsuario", nombreUsuario);
                cmdVerificacion.Parameters.AddWithValue("@nombreGrupo", nombreRealGenerado);

                int count = Convert.ToInt32(cmdVerificacion.ExecuteScalar());
                Assert.AreEqual(0, count, "El usuario no fue eliminado del grupo.");

                // Verificar si el grupo fue eliminado correctamente si no tiene más usuarios
                var cmdVerificarGrupo = new MySqlCommand("SELECT COUNT(*) FROM Grupos WHERE nombreReal = @nombreGrupo", conn);
                cmdVerificarGrupo.Parameters.AddWithValue("@nombreGrupo", nombreRealGenerado);

                count = Convert.ToInt32(cmdVerificarGrupo.ExecuteScalar());
                Assert.AreEqual(0, count, "El grupo no fue eliminado correctamente.");
            }

            // Limpiar los datos de prueba después de la ejecución
            Cleanup();
        }
        [TestMethod]
        public void TestMethod_PRReportarGrupo()
        {
            // Arrange
            string respuestaEsperada = JsonConvert.SerializeObject("Reporte correcto");
            API_Grupos.Controllers.GroupController controller = new API_Grupos.Controllers.GroupController();

            // Primero, registrar un grupo para obtener el nombreReal generado automáticamente
            API_Grupos.Controllers.GroupController.Grupo grupo = new API_Grupos.Controllers.GroupController.Grupo()
            {
                nombreVisible = "Grupo Visible",
                configuracion = "Configuración del grupo",
                descripcion = "Descripción del grupo",
                imagen = "grupo.jpg"
            };

            var resultadoGrupoJson = controller.PRRegistrarGrupo(grupo);
            var resultadoGrupo = JsonConvert.DeserializeObject<string>(resultadoGrupoJson);

            // Verificar que el grupo se haya registrado correctamente
            Assert.AreEqual("Registro correcto", resultadoGrupo);

            // Obtener el nombreReal generado automáticamente
            string nombreRealGenerado = grupo.nombreReal;

            // Crear un reporte para el grupo recién creado
            var reporte = new API_Grupos.Controllers.GroupController.Reporte()
            {
                numeroReporte = 1,
                nombreReal = nombreRealGenerado,
                tipo = "Infracción",
                descripcion = "Descripción del reporte"
            };

            // Act
            var resultadoJson = controller.PRReportarGrupo(reporte);

            // Convertir a string explícitamente
            string resultadoStr = resultadoJson as string;

            // Assert
            Assert.IsNotNull(resultadoStr, "El resultado de la operación es null");
            Assert.AreEqual(respuestaEsperada, resultadoStr);

            // Verificar en la base de datos si el reporte fue insertado correctamente
            using (var conn = new MySqlConnection("Server=localhost; database=infini; uID=root; pwd=;"))
            {
                conn.Open();

                var cmdVerificacion = new MySqlCommand("SELECT COUNT(*) FROM ReporteGrupo WHERE numeroDeReporte = @numeroReporte AND nombreReal = @nombreReal", conn);
                cmdVerificacion.Parameters.AddWithValue("@numeroReporte", reporte.numeroReporte);
                cmdVerificacion.Parameters.AddWithValue("@nombreReal", nombreRealGenerado);

                int count = Convert.ToInt32(cmdVerificacion.ExecuteScalar());
                Assert.AreEqual(1, count, "El reporte no fue insertado correctamente.");

                // Verificar si la descripción fue insertada correctamente
                var cmdVerificarDescripcion = new MySqlCommand("SELECT descripcion FROM ReporteGrupo WHERE numeroDeReporte = @numeroReporte AND nombreReal = @nombreReal", conn);
                cmdVerificarDescripcion.Parameters.AddWithValue("@numeroReporte", reporte.numeroReporte);
                cmdVerificarDescripcion.Parameters.AddWithValue("@nombreReal", nombreRealGenerado);

                var descripcion = cmdVerificarDescripcion.ExecuteScalar();
                Assert.AreEqual("Descripción del reporte", descripcion.ToString(), "La descripción del reporte no fue actualizada correctamente.");

                conn.Close();
            }
            // Limpiar los datos de prueba después de la ejecución
            Cleanup();
        }
    }
}
