using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using Google.Protobuf;
using System.Web.Http;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using Org.BouncyCastle.Security;
using static API_Grupos.Controllers.GroupController;
using RouteAttribute = System.Web.Mvc.RouteAttribute;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text;

namespace API_Grupos.Controllers
{
    public class GroupController : ApiController
    {
        public class Grupo
        {
            public string nombreReal { get; set; }
            public string nombreVisible { get; set; }
            public string configuracion { get; set; }
            public string descripcion { get; set; }
            public string imagen { get; set; }
            public string chatGrupal { get; set; }
        }

        public class UsuarioGrupo
        {
            public string nombreReal { get; set; }
            public string nombreDeCuenta { get; set; }
        }

        public class Reporte
        {
            public int numeroReporte { get; set; }
            public string nombreReal { get; set; }
            public string tipo { get; set; }
            public string descripcion { get; set; }
        }

        public string connectionString = "Server=localhost; database=infini; uID=root; pwd=;";

        public async Task<string> SubirImagenAGitHub(string imagen)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    string token = "Token"; // Token para repositorio privado. Cambiar por el token real
                    string nombreDeImagen = GenerarIdAleatorio(8) + ".png"; // nombre aleatorio para que el nombre del archivo no se repita
                    string carpeta = "GroupImages"; // Carpeta de GitHub en donde se guarda la imagen
                                                   // No es necesario crear la carpeta a mano, se crea si le intentas subir algo.
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    client.DefaultRequestHeaders.UserAgent.ParseAdd("request");
                    var content = new { message = "Nueva imagen", content = imagen };
                    var jsonContent = new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8, "application/json");
                    var response = await client.PutAsync($"https://api.github.com/repos/imagesinfini/publicImages/contents/{carpeta}/{nombreDeImagen}", jsonContent);
                    if (!response.IsSuccessStatusCode)
                    {
                        var error = await response.Content.ReadAsStringAsync();
                        throw new Exception($"Error al subir la imagen: {response.StatusCode} - {error}");
                    }
                    var resultado = await response.Content.ReadAsStringAsync();
                    dynamic json = JsonConvert.DeserializeObject(resultado);

                    string linkAImagen = $"https://github.com/imagesinfini/publicImages/raw/main/{carpeta}/{nombreDeImagen}";
                    return linkAImagen;
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }
        public string GenerarIdAleatorio(int longitud)
        {
            var random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, longitud)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private async Task<string> CargarImagenDeGitHub(string urlImagen)
        {
            using (var client = new HttpClient())
            {
                string token = "Token"; // Token para repositorio privado. Cambiar por el token real
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var response = await client.GetAsync(urlImagen);
                if (response.IsSuccessStatusCode)
                {
                    byte[] imagenBytes = await response.Content.ReadAsByteArrayAsync();
                    return Convert.ToBase64String(imagenBytes);
                }
                else
                {
                    throw new Exception("No se pudo descargar la imagen desde GitHub.");
                }
            }
        }
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("RegistrarGrupo")]
        public async Task<dynamic> RegistrarGrupo([FromBody] Grupo group)
        {
            string linkImagen = null;
            try
            {
                group.nombreReal = crearNombreGrupo();
                if (string.IsNullOrEmpty(group.nombreVisible)
                    || string.IsNullOrEmpty(group.configuracion)
                    || string.IsNullOrEmpty(group.imagen))
                {
                    return Json("Valores nulos");
                }
                linkImagen = await SubirImagenAGitHub(group.imagen);

                MySqlConnection conn = new MySqlConnection(connectionString);
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("insert into Grupos (nombreReal, nombreVisible, configuracion, foto) values (@nombreReal, @nombreVisible, @configuracion, @foto)", conn);
                cmd.Parameters.AddWithValue("@nombreReal", group.nombreReal);
                cmd.Parameters.AddWithValue("@nombreVisible", group.nombreVisible);
                cmd.Parameters.AddWithValue("@configuracion", group.configuracion);
                cmd.Parameters.AddWithValue("@foto", linkImagen);
                cmd.ExecuteNonQuery();
                if (!string.IsNullOrEmpty(group.descripcion))
                {
                    MySqlCommand cmd2 = new MySqlCommand("update Grupos set descripcion = @descripcion where nombreReal = @nombreReal", conn);
                    cmd2.Parameters.AddWithValue("@descripcion", group.descripcion);
                    cmd2.ExecuteNonQuery();
                }
                conn.Close();
                return Json("Registro correcto");
            }
            catch (Exception ex)
            {
                return Json($"Registro fallido: {ex.Message}");
            }
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("RegistrarGrupoUG")]
        public dynamic RegistrarGrupoUG([FromBody] UsuarioGrupo ug)
        {
            try
            {
                // Validación de campos
                if (string.IsNullOrEmpty(ug.nombreReal) || string.IsNullOrEmpty(ug.nombreDeCuenta))
                {
                    return Json("Valores nulos o vacíos");
                }

                // Cadena de conexión a la base de datos
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    // Consulta de inserción
                    string insertQuery = "INSERT INTO Participa (nombreReal, nombreDeCuenta, rol) VALUES (@nombreReal, @nombreDeCuenta, 'c')";

                    // Creación del comando SQL
                    using (MySqlCommand cmd = new MySqlCommand(insertQuery, conn))
                    {
                        // Agregando parámetros
                        cmd.Parameters.AddWithValue("@nombreReal", ug.nombreReal);
                        cmd.Parameters.AddWithValue("@nombreDeCuenta", ug.nombreDeCuenta);

                        // Ejecución de la consulta
                        int rowsAffected = cmd.ExecuteNonQuery();

                        // Verificación de la inserción
                        if (rowsAffected > 0)
                        {
                            return Json("Registro correcto");
                        }
                        else
                        {
                            return Json("No se pudo insertar el registro. Filas afectadas: " + rowsAffected);
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                return Json($"Error en la base de datos: {ex.Message}");
            }
            catch (Exception ex)
            {
                return Json($"Registro fallido: {ex.Message}");
            }
        }



        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("ObtenerGruposPorNombreVisibleYUsuario")] 
        public async Task<dynamic> ObtenerGruposPorNombreVisibleYUsuario(string nombreVisible, string nombreDeCuenta)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    string query = @"
                SELECT g.nombreReal, g.nombreVisible, g.configuracion, g.descripcion, g.foto
                FROM Grupos g
                JOIN Participa ug ON g.nombreReal = ug.nombreReal
                WHERE g.nombreVisible = @nombreVisible AND ug.nombreDeCuenta = @nombreDeCuenta";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@nombreVisible", nombreVisible);
                        cmd.Parameters.AddWithValue("@nombreDeCuenta", nombreDeCuenta);

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            List<dynamic> Grupos = new List<dynamic>();

                            while (reader.Read())
                            {
                                string nombreReal = reader["nombreReal"].ToString();
                                string configuracion = reader["configuracion"].ToString();
                                string descripcion = reader["descripcion"].ToString();
                                string foto = await CargarImagenDeGitHub(reader["foto"].ToString());

                                var grupo = new
                                {
                                    nombreReal = nombreReal,
                                    nombreVisible = nombreVisible,
                                    configuracion = configuracion,
                                    descripcion = descripcion,
                                    foto = foto
                                };

                                Grupos.Add(grupo);
                            }

                            conn.Close();

                            if (Grupos.Count > 0)
                            {
                                return Json(Grupos);
                            }
                            else
                            {
                                return Json("No se encontraron Grupos para el usuario especificado");
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                return Json($"Error al obtener los Grupos: {e.Message}");
            }
        }

        [System.Web.Http.HttpGet]
        [Route("prueba")]
        public dynamic prueba()
        {
            return "hola";
        }


        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("ObtenerGrupo")]
        public dynamic ObtenerGrupo(string nombre)
        {
            try
            {
                MySqlConnection conn = new MySqlConnection(connectionString);
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("select * from Grupos where nombreReal=@nombreReal", conn);
                cmd.Parameters.AddWithValue("@nombreReal", nombre);
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    string nombreGrupo = reader["nombreReal"].ToString();
                    string nombreVisible = reader["nombreVisible"].ToString();
                    string configuracion = reader["configuracion"].ToString();

                    var data = new { nombreGrupo = nombreGrupo, nombreVisible = nombreVisible, configuracion = configuracion };
                    conn.Close();
                    return Json(data);
                }
                else
                {
                    return Json("No se encuentra el grupo");
                }
            }
            catch (Exception e)
            {
                return Json("No se encuentra el grupo");
            }
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("ObtenerMensajes")]
        public dynamic ObtenerMensajes(string nombre)
        {
            try
            {
                MySqlConnection conn = new MySqlConnection(connectionString);
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("select chatGrupal from Grupos where nombreReal=@nombreReal", conn);
                cmd.Parameters.AddWithValue("@nombreReal", nombre);
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    string chatGrupal = reader["chatGrupal"].ToString();
                    conn.Close();
                    return Json(chatGrupal);
                }
                else
                {
                    return Json("No se encuentra el grupo");
                }
            }
            catch (Exception ex)
            {
                return Json("No se encuentra el grupo");
            }
        }

        [System.Web.Http.HttpDelete]
        [System.Web.Http.Route("EliminarGrupo")]
        public dynamic EliminarGrupo(string nombreReal)
        {
            try
            {
                MySqlConnection conn = new MySqlConnection(connectionString);
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("delete from Grupos where nombreReal = @nombreReal", conn);
                cmd.Parameters.AddWithValue("@nombreReal", nombreReal);
                cmd.ExecuteNonQuery();
                conn.Close();
                return Json("Se pudo eliminar");
            }
            catch
            {
                return Json("No se pudo eliminar");
            }
        }

        [System.Web.Http.HttpPut]
        [System.Web.Http.Route("EditarGrupo")]
        public async Task<dynamic> EditarGrupo([FromBody] Grupo group)
        {
            string linkImagen = null;
            if (string.IsNullOrEmpty(group.nombreReal)
                    || string.IsNullOrEmpty(group.nombreVisible)
                    || string.IsNullOrEmpty(group.configuracion)
                    || string.IsNullOrEmpty(group.imagen)
                    || group.configuracion.Length > 255)
            {
                return Json("Datos inválidos");
            }
            try
            {
                MySqlConnection conn = new MySqlConnection(connectionString);
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("update Grupos " +
                    "set nombreVisible = @nombreVisible," +
                    "configuracion = @configuracion," +
                    "descripcion = @descripcion," +
                    "foto = @foto " +
                    "where nombreReal = @nombreReal", conn);
                cmd.Parameters.AddWithValue("@nombreReal", group.nombreReal);
                cmd.Parameters.AddWithValue("@nombreVisible", group.nombreVisible);
                cmd.Parameters.AddWithValue("@configuracion", group.configuracion);
                cmd.Parameters.AddWithValue("@descripcion", group.descripcion);
                cmd.Parameters.AddWithValue("@foto", linkImagen = await SubirImagenAGitHub(group.imagen));
                cmd.ExecuteNonQuery();
                conn.Close();
                return Json("Se editó el grupo correctamente");
            }
            catch
            {
                return Json("No se pudo editar el grupo");
            }
        }
        [System.Web.Http.HttpPut]
        [System.Web.Http.Route("ActualizarMensajes")]
        public dynamic ActualizarMensajes(string nombreReal, string chatGrupal)
        {
            if (string.IsNullOrEmpty(nombreReal) && string.IsNullOrEmpty(chatGrupal))
            {
                return Json("Datos inválidos");
            }
            try
            {
                MySqlConnection conn = new MySqlConnection(connectionString);
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("update Grupos " +
                    "set chatGrupal = @chatGrupal " +
                    "where nombreReal = @nombreReal", conn);
                cmd.Parameters.AddWithValue("@nombreReal", nombreReal);
                cmd.Parameters.AddWithValue("@chatGrupal", chatGrupal);
                cmd.ExecuteNonQuery();
                conn.Close();
                return Json("Se actualizaron los mensajes correctamente");
            }
            catch (Exception ex)
            {
                return Json($"No se pudo editar el grupo: {ex.Message}");
            }
        }

        [System.Web.Http.HttpPut]
        [System.Web.Http.Route("EditarGrupoUG")]
        public dynamic EditarGrupoUG([FromBody] Grupo group, string usuario)
        {

            if (string.IsNullOrEmpty(group.nombreReal)
                    || string.IsNullOrEmpty(group.nombreVisible)
                    || string.IsNullOrEmpty(group.configuracion)
                    || string.IsNullOrEmpty(group.imagen)
                    || string.IsNullOrEmpty(usuario))
            {
                return Json("Datos inválidos");
            }
            try
            {
                MySqlConnection conn = new MySqlConnection(connectionString);
                conn.Open();
                string permisosQuery = "SELECT rol FROM Participa WHERE nombreReal = @nombreReal AND nombreDeCuenta = @nombreDeCuenta AND (rol = 'c' OR rol = 'a')";
                MySqlCommand permisosCmd = new MySqlCommand(permisosQuery, conn);
                permisosCmd.Parameters.AddWithValue("@nombreReal", group.nombreReal);
                permisosCmd.Parameters.AddWithValue("@nombreDeCuenta", usuario);
                MySqlDataReader reader = permisosCmd.ExecuteReader();
                conn.Close();
                return Json("Se editó el grupo correctamente");
            }
            catch (Exception ex)
            {
                return Json($"No se pudo editar el grupo: {ex.Message}");
            }
        }

        //Grupos al que pertenece un usuario
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("ObtenerGruposPorUsuario")]
        public async Task<dynamic> ObtenerGruposPorUsuario(string nombreDeCuenta)
        {
            try
            {
                List<Grupo> Grupos = new List<Grupo>();
                MySqlConnection conn = new MySqlConnection(connectionString);
                conn.Open();

                MySqlCommand cmd = new MySqlCommand(@"
                    SELECT g.nombreReal, g.nombreVisible, g.configuracion, g.descripcion, g.foto
                    FROM Grupos g
                    JOIN Participa ug ON g.nombreReal = ug.nombreReal
                    WHERE ug.nombreDeCuenta = @nombreDeCuenta", conn);

                cmd.Parameters.AddWithValue("@nombreDeCuenta", nombreDeCuenta);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Grupo grupo = new Grupo
                    {
                        nombreReal = reader["nombreReal"].ToString(),
                        nombreVisible = reader["nombreVisible"].ToString(),
                        configuracion = reader["configuracion"].ToString(),
                        descripcion = reader["descripcion"].ToString(),
                        imagen = await CargarImagenDeGitHub(reader["foto"].ToString())
                    };
                    Grupos.Add(grupo);
                }

                conn.Close();
                return Json(Grupos);
            }
            catch (Exception e)
            {
                return Json("No se pudieron obtener los Grupos");
            }
        }



        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("AgregarUsuarioAGrupo")]
        public dynamic AgregarUsuarioAGrupo(string nombreUsuario, string nombreGrupo)
        {
            try
            {
                MySqlConnection conn = new MySqlConnection(connectionString);
                conn.Open();
                string query = "INSERT INTO Participa (nombreDeCuenta, nombreReal) VALUES (@nombreUsuario, @nombreGrupo)";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@nombreUsuario", nombreUsuario);
                cmd.Parameters.AddWithValue("@nombreGrupo", nombreGrupo);
                cmd.ExecuteNonQuery();

                return Json("Usuario agregado al grupo");
            }
            catch (Exception)
            {
                return Json("No se pudo agregar el usuario al grupo");
            }
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("AgregarUsuarioAGrupoUG")]
        public dynamic AgregarUsuarioAGrupoUG(string admin, string nombreUsuario, string nombreGrupo)
        {
            if (string.IsNullOrEmpty(admin) || string.IsNullOrEmpty(nombreUsuario) || string.IsNullOrEmpty(nombreGrupo))
            {
                return Json("Datos inválidos");
            }

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    // Verificar si la persona que ingresa los datos es administrador o creador del grupo
                    //Puede ser que no funcione con "Rol" como varchar
                    string permisosQuery = "SELECT rol FROM Participa WHERE nombreReal = @nombreGrupo AND nombreDeCuenta = @nombreAdminOCreador AND (rol = 'a' OR rol = 'c')";
                    using (MySqlCommand permisosCmd = new MySqlCommand(permisosQuery, conn))
                    {
                        permisosCmd.Parameters.AddWithValue("@nombreGrupo", nombreGrupo);
                        permisosCmd.Parameters.AddWithValue("@nombreAdminOCreador", admin);

                        object rol = permisosCmd.ExecuteScalar();
                        if (rol == null)
                        {
                            return Json("No tienes permisos de administrador o creador para agregar usuarios a este grupo");
                        }
                    }

                    // Agregar el usuario al grupo
                    string query = "INSERT INTO Participa (nombreDeCuenta, nombreReal) VALUES (@nombreUsuario, @nombreGrupo)";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@nombreUsuario", nombreUsuario);
                        cmd.Parameters.AddWithValue("@nombreGrupo", nombreGrupo);
                        cmd.ExecuteNonQuery();
                    }

                    return Json("Usuario agregado al grupo");
                }
            }
            catch (Exception ex)
            {
                return Json($"No se pudo agregar el usuario al grupo: {ex.Message}");
            }
        }



        [System.Web.Http.HttpDelete]
        [System.Web.Http.Route("EliminarUsuarioDeGrupo")]
        public dynamic EliminarUsuarioDeGrupo(string nombreRealGrupo, string nombreUsuario)
        {
            try
            {
                MySqlConnection conn = new MySqlConnection(connectionString);
                {
                    conn.Open();

                    // Verificar si el grupo existe y si el usuario pertenece al grupo
                    string verificarQuery = @"
                SELECT COUNT(*) 
                FROM Participa 
                WHERE nombreReal = @nombreRealGrupo AND nombreDeCuenta = @nombreUsuario";
                    MySqlCommand verificarCmd = new MySqlCommand(verificarQuery, conn);
                    verificarCmd.Parameters.AddWithValue("@nombreRealGrupo", nombreRealGrupo);
                    verificarCmd.Parameters.AddWithValue("@nombreUsuario", nombreUsuario);
                    int existe = Convert.ToInt32(verificarCmd.ExecuteScalar());

                    if (existe > 0)
                    {
                        // Eliminar al usuario del grupo
                        string eliminarQuery = "DELETE FROM Participa WHERE nombreReal = @nombreRealGrupo AND nombreDeCuenta = @nombreUsuario";
                        MySqlCommand eliminarCmd = new MySqlCommand(eliminarQuery, conn);
                        eliminarCmd.Parameters.AddWithValue("@nombreRealGrupo", nombreRealGrupo);
                        eliminarCmd.Parameters.AddWithValue("@nombreUsuario", nombreUsuario);
                        eliminarCmd.ExecuteNonQuery();
                        return Json("Grupo eliminado del usuario correctamente");
                    }
                    else
                    {
                        return Json("El grupo no existe o el usuario no pertenece al grupo");
                    }
                }
            }
            catch (Exception ex)
            {
                return Json($"Error al eliminar el grupo: {ex.Message}");
            }

        }


        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("ReportarGrupo")]
        public dynamic ReportarGrupo([FromBody] Reporte reporte) //Lo cambié porque hablaba de un nombre de usuario
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    // Insertar el reporte en la base de datos
                    MySqlCommand cmd = new MySqlCommand("INSERT INTO ReporteGrupo (numeroDeReporte, nombreReal, tipo) VALUES (@reporte, @nombreReal, @tipo)", conn);
                    cmd.Parameters.AddWithValue("@reporte", reporte.numeroReporte);
                    cmd.Parameters.AddWithValue("@tipo", reporte.tipo);
                    cmd.Parameters.AddWithValue("@nombreReal", reporte.nombreReal);
                    cmd.ExecuteNonQuery();

                    // Actualizar la descripción si no es nula
                    if (!string.IsNullOrEmpty(reporte.descripcion))
                    {
                        MySqlCommand cmd2 = new MySqlCommand("UPDATE ReporteGrupo SET descripcion = @descripcion WHERE numeroDeReporte = @reporte AND nombreReal = @nombreReal", conn);
                        cmd2.Parameters.AddWithValue("@descripcion", reporte.descripcion);
                        cmd2.Parameters.AddWithValue("@reporte", reporte.numeroReporte);
                        cmd2.Parameters.AddWithValue("@nombreReal", reporte.nombreReal);
                        cmd2.ExecuteNonQuery();
                    }
                }

                return Json("Reporte correcto");
            }
            catch (Exception ex)
            {
                // Devuelve el mensaje de error para ayudar en la depuración
                return Json($"Reporte incorrecto: {ex.Message}");
            }
        }


        private string crearNombreGrupo()
        {
            string nombre = "";
            string caracteres = "AaBbCcDdEeFfGgHhIiJjKkLlMmNnOoPpQqRrSsTtUuVvWwXxYyZz0123456789";
            Random rnd = new Random();
            for (int i = 0; i < 8; i++)
            {

                nombre += caracteres[rnd.Next(0, 61)];
            }
            return nombre;
        }

        //Testing
        public dynamic PRRegistrarGrupo(Grupo group)
        {
            try
            {
                group.nombreReal = crearNombreGrupo();
                if (string.IsNullOrEmpty(group.nombreVisible)
                    || string.IsNullOrEmpty(group.configuracion)
                    || string.IsNullOrEmpty(group.imagen))
                {
                    return JsonConvert.SerializeObject("Valores nulos");
                }

                MySqlConnection conn = new MySqlConnection(connectionString);
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("insert into Grupos (nombreReal, nombreVisible, configuracion, foto) values (@nombreReal, @nombreVisible, @configuracion, @foto)", conn);
                cmd.Parameters.AddWithValue("@nombreReal", group.nombreReal);
                cmd.Parameters.AddWithValue("@nombreVisible", group.nombreVisible);
                cmd.Parameters.AddWithValue("@configuracion", group.configuracion);
                cmd.Parameters.AddWithValue("@foto", group.imagen);
                cmd.ExecuteNonQuery();
                if (!string.IsNullOrEmpty(group.descripcion))
                {
                    MySqlCommand cmd2 = new MySqlCommand("update Grupos set descripcion = @descripcion where nombreReal = @nombreReal", conn);
                    cmd2.Parameters.AddWithValue("@descripcion", group.descripcion);
                    cmd2.Parameters.AddWithValue("@nombreReal", group.nombreReal);
                    cmd2.ExecuteNonQuery();
                }
                conn.Close();
                return JsonConvert.SerializeObject("Registro correcto");
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject($"Registro fallido: {ex.Message}");
            }
        }
        public dynamic PRRegistrarGrupoUG(UsuarioGrupo ug)
        {
            try
            {
                if (string.IsNullOrEmpty(ug.nombreReal) || string.IsNullOrEmpty(ug.nombreDeCuenta))
                {
                    return Json("Valores nulos");
                }

                MySqlConnection conn = new MySqlConnection(connectionString);
                conn.Open();
                //Debería funcionar incluso cuando "Rol" es un varchar y no un char.
                string insertQuery = "INSERT INTO Participa (nombreReal, nombreDeCuenta, rol) VALUES (@nombreReal, @nombreDeCuenta, 'a')";
                MySqlCommand cmd = new MySqlCommand(insertQuery, conn);
                cmd.Parameters.AddWithValue("@nombreReal", ug.nombreReal);
                cmd.Parameters.AddWithValue("@nombreDeCuenta", ug.nombreDeCuenta);
                cmd.ExecuteNonQuery();
                conn.Close();
                return JsonConvert.SerializeObject("Registro correcto");
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject($"Registro fallido: {ex.Message}");
            }
        }

        public dynamic PRObtenerGruposPorNombreVisibleYUsuario(string nombreVisible, string nombreDeCuenta)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    string query = @"
                SELECT g.nombreReal, g.nombreVisible, g.configuracion, g.descripcion, g.foto
                FROM Grupos g
                JOIN Participa ug ON g.nombreReal = ug.nombreReal
                WHERE g.nombreVisible = @nombreVisible AND ug.nombreDeCuenta = @nombreDeCuenta";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@nombreVisible", nombreVisible);
                        cmd.Parameters.AddWithValue("@nombreDeCuenta", nombreDeCuenta);

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            List<dynamic> Grupos = new List<dynamic>();

                            while (reader.Read())
                            {
                                string nombreReal = reader["nombreReal"].ToString();
                                string configuracion = reader["configuracion"].ToString();
                                string descripcion = reader["descripcion"].ToString();
                                string foto = reader["foto"].ToString();

                                var grupo = new
                                {
                                    nombreReal = nombreReal,
                                    nombreVisible = nombreVisible,
                                    configuracion = configuracion,
                                    descripcion = descripcion,
                                    foto = foto
                                };

                                Grupos.Add(grupo);
                            }

                            conn.Close();

                            if (Grupos.Count > 0)
                            {
                                return JsonConvert.SerializeObject(Grupos);
                            }
                            else
                            {
                                return JsonConvert.SerializeObject("No se encontraron Grupos para el usuario especificado");

                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                return JsonConvert.SerializeObject($"Error al obtener los Grupos: {e.Message}");
            }
        }
        public dynamic PRObtenerGrupo(string nombre)
        {
            try
            {
                MySqlConnection conn = new MySqlConnection(connectionString);
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("select * from Grupos where nombreReal=@nombreReal", conn);
                cmd.Parameters.AddWithValue("@nombreReal", nombre);
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    string nombreGrupo = reader["nombreReal"].ToString();
                    string nombreVisible = reader["nombreVisible"].ToString();
                    string configuracion = reader["configuracion"].ToString();

                    var data = new { nombreGrupo = nombreGrupo, nombreVisible = nombreVisible, configuracion = configuracion };
                    conn.Close();
                    return JsonConvert.SerializeObject(data);
                }
                else
                {
                    return JsonConvert.SerializeObject("No se encuentra el grupo");
                }
            }
            catch (Exception e)
            {
                return JsonConvert.SerializeObject("No se encuentra el grupo");
            }
        }
        public dynamic PREditarGrupo(Grupo group)
        {
            
            if (string.IsNullOrEmpty(group.nombreReal)
                || string.IsNullOrEmpty(group.nombreVisible)
                || string.IsNullOrEmpty(group.configuracion)
                || string.IsNullOrEmpty(group.imagen)  
                || group.configuracion.Length > 255)
            {
                return JsonConvert.SerializeObject("Datos inválidos");
            }

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("UPDATE Grupos " +
                        "SET nombreVisible = @nombreVisible, " +
                        "configuracion = @configuracion, " +
                        "descripcion = @descripcion, " +
                        "foto = @foto " +  
                        "WHERE nombreReal = @nombreReal", conn);
                    cmd.Parameters.AddWithValue("@nombreReal", group.nombreReal);
                    cmd.Parameters.AddWithValue("@nombreVisible", group.nombreVisible);
                    cmd.Parameters.AddWithValue("@configuracion", group.configuracion);
                    cmd.Parameters.AddWithValue("@descripcion", group.descripcion);
                    cmd.Parameters.AddWithValue("@foto", group.imagen);
                    cmd.ExecuteNonQuery();
                }
                return JsonConvert.SerializeObject("Se editó el grupo correctamente");
            }
            catch (MySqlException ex)
            {
                
                return JsonConvert.SerializeObject($"No se pudo editar el grupo: {ex.Message}");
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject($"Error inesperado: {ex.Message}");
            }
        }


        public dynamic PREditarGrupoUG(string nombreReal, string usuario)
        {
            if (string.IsNullOrEmpty(nombreReal) && string.IsNullOrEmpty(usuario))
            {
                return JsonConvert.SerializeObject("Datos inválidos");
            }
            try
            {
                MySqlConnection conn = new MySqlConnection(connectionString);
                conn.Open();
                // Verificar si el usuario tiene permisos
                //Puede ser que no funcione con "Rol" como varchar
                string permisosQuery = "SELECT rol FROM Participa WHERE nombreReal = @nombreReal AND nombreDeCuenta = @nombreDeCuenta AND (rol = 'c' OR rol = 'a')";
                MySqlCommand permisosCmd = new MySqlCommand(permisosQuery, conn);
                permisosCmd.Parameters.AddWithValue("@nombreReal", nombreReal);
                permisosCmd.Parameters.AddWithValue("@nombreDeCuenta", usuario);
                MySqlDataReader reader = permisosCmd.ExecuteReader();
                conn.Close();
                return JsonConvert.SerializeObject("Se editó el grupo correctamente");
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject($"No se pudo editar el grupo: {ex.Message}");
            }
        }

        public dynamic PRObtenerGruposPorUsuario(string nombreDeCuenta)
        {
            try
            {
                List<Grupo> Grupos = new List<Grupo>();
                MySqlConnection conn = new MySqlConnection(connectionString);
                conn.Open();

                MySqlCommand cmd = new MySqlCommand(@"
                    SELECT g.nombreReal, g.nombreVisible, g.configuracion, g.descripcion, g.foto
                    FROM Grupos g
                    JOIN Participa ug ON g.nombreReal = ug.nombreReal
                    WHERE ug.nombreDeCuenta = @nombreDeCuenta", conn);

                cmd.Parameters.AddWithValue("@nombreDeCuenta", nombreDeCuenta);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Grupo grupo = new Grupo
                    {
                        nombreReal = reader["nombreReal"].ToString(),
                        nombreVisible = reader["nombreVisible"].ToString(),
                        configuracion = reader["configuracion"].ToString(),
                        descripcion = reader["descripcion"].ToString(),
                        imagen = reader["foto"].ToString()
                    };
                    Grupos.Add(grupo);
                }

                conn.Close();
                return JsonConvert.SerializeObject(Grupos);
            }
            catch (Exception e)
            {
                return JsonConvert.SerializeObject("No se pudieron obtener los Grupos");
            }
        }
        public dynamic PRAgregarUsuarioAGrupo(string nombreUsuario, string nombreGrupo)
        {
            try
            {
                MySqlConnection conn = new MySqlConnection(connectionString);
                conn.Open();
                string query = "INSERT INTO Participa (nombreDeCuenta, nombreReal) VALUES (@nombreUsuario, @nombreGrupo)";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@nombreUsuario", nombreUsuario);
                cmd.Parameters.AddWithValue("@nombreGrupo", nombreGrupo);
                cmd.ExecuteNonQuery();

                return JsonConvert.SerializeObject("Usuario agregado al grupo");
            }
            catch (Exception)
            {
                return JsonConvert.SerializeObject("No se pudo agregar el usuario al grupo");
            }
        }

        public dynamic PRAgregarUsuarioAGrupoUG(string admin, string nombreUsuario, string nombreGrupo)
        {
            if (string.IsNullOrEmpty(admin) || string.IsNullOrEmpty(nombreUsuario) || string.IsNullOrEmpty(nombreGrupo))
            {
                return JsonConvert.SerializeObject("Datos inválidos");
            }

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string permisosQuery = "SELECT rol FROM Participa WHERE nombreReal = @nombreGrupo AND nombreDeCuenta = @nombreAdminOCreador AND (rol = 'a' OR rol = 'c')";
                    using (MySqlCommand permisosCmd = new MySqlCommand(permisosQuery, conn))
                    {
                        permisosCmd.Parameters.AddWithValue("@nombreGrupo", nombreGrupo);
                        permisosCmd.Parameters.AddWithValue("@nombreAdminOCreador", admin);

                        object rol = permisosCmd.ExecuteScalar();
                        if (rol == null)
                        {
                            return JsonConvert.SerializeObject("No tienes permisos de administrador o creador para agregar usuarios a este grupo");
                        }
                    }

                    // Agregar el usuario al grupo
                    string query = "INSERT INTO Participa (nombreDeCuenta, nombreReal) VALUES (@nombreUsuario, @nombreGrupo)";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@nombreUsuario", nombreUsuario);
                        cmd.Parameters.AddWithValue("@nombreGrupo", nombreGrupo);
                        cmd.ExecuteNonQuery();
                    }

                    return JsonConvert.SerializeObject("Usuario agregado al grupo");
                }
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject($"No se pudo agregar el usuario al grupo: {ex.Message}");
            }
        }

        public dynamic PREliminarUsuarioDeGrupo(string nombreRealGrupo, string nombreUsuario)
        {
            try
            {
                MySqlConnection conn = new MySqlConnection(connectionString);
                {
                    conn.Open();

                    // Verificar si el grupo existe y si el usuario pertenece al grupo
                    string verificarQuery = @"
                SELECT COUNT(*) 
                FROM Participa 
                WHERE nombreReal = @nombreRealGrupo AND nombreDeCuenta = @nombreUsuario";
                    MySqlCommand verificarCmd = new MySqlCommand(verificarQuery, conn);
                    verificarCmd.Parameters.AddWithValue("@nombreRealGrupo", nombreRealGrupo);
                    verificarCmd.Parameters.AddWithValue("@nombreUsuario", nombreUsuario);
                    int existe = Convert.ToInt32(verificarCmd.ExecuteScalar());

                    if (existe > 0)
                    {
                        // Eliminar al usuario del grupo
                        string eliminarQuery = "DELETE FROM Participa WHERE nombreReal = @nombreRealGrupo AND nombreDeCuenta = @nombreUsuario";
                        MySqlCommand eliminarCmd = new MySqlCommand(eliminarQuery, conn);
                        eliminarCmd.Parameters.AddWithValue("@nombreRealGrupo", nombreRealGrupo);
                        eliminarCmd.Parameters.AddWithValue("@nombreUsuario", nombreUsuario);
                        eliminarCmd.ExecuteNonQuery();

                        // Opcional: Eliminar el grupo si no tiene más usuarios asociados
                        string eliminarGrupoQuery = "DELETE FROM Grupos WHERE nombreReal = @nombreRealGrupo AND NOT EXISTS (SELECT * FROM Participa WHERE nombreReal = @nombreRealGrupo)";
                        MySqlCommand eliminarGrupoCmd = new MySqlCommand(eliminarGrupoQuery, conn);
                        eliminarGrupoCmd.Parameters.AddWithValue("@nombreRealGrupo", nombreRealGrupo);
                        eliminarGrupoCmd.ExecuteNonQuery();

                        return "Grupo eliminado del usuario correctamente";
                    }
                    else
                    {
                        return "El grupo no existe o el usuario no pertenece al grupo";
                    }
                }
            }
            catch (Exception ex)
            {
                return $"Error al eliminar el grupo: {ex.Message}";
            }

        }
        public dynamic PRReportarGrupo(Reporte reporte)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    // Insertar el reporte en la base de datos
                    MySqlCommand cmd = new MySqlCommand("INSERT INTO ReporteGrupo (numeroDeReporte, nombreReal, tipo) VALUES (@reporte, @nombreReal, @tipo)", conn);
                    cmd.Parameters.AddWithValue("@reporte", reporte.numeroReporte);
                    cmd.Parameters.AddWithValue("@tipo", reporte.tipo);
                    cmd.Parameters.AddWithValue("@nombreReal", reporte.nombreReal);
                    cmd.ExecuteNonQuery();

                    // Actualizar la descripción si no es nula
                    if (!string.IsNullOrEmpty(reporte.descripcion))
                    {
                        MySqlCommand cmd2 = new MySqlCommand("UPDATE ReporteGrupo SET descripcion = @descripcion WHERE numeroDeReporte = @reporte AND nombreReal = @nombreReal", conn);
                        cmd2.Parameters.AddWithValue("@descripcion", reporte.descripcion);
                        cmd2.Parameters.AddWithValue("@reporte", reporte.numeroReporte);
                        cmd2.Parameters.AddWithValue("@nombreReal", reporte.nombreReal);
                        cmd2.ExecuteNonQuery();
                    }
                }

                return JsonConvert.SerializeObject("Reporte correcto");
            }
            catch (Exception ex)
            {
                // Devuelve el mensaje de error para ayudar en la depuración
                return JsonConvert.SerializeObject($"Reporte incorrecto: {ex.Message}");
            }
        }
        public dynamic PRActualizarMensajes(string nombreReal, string chatGrupal)
        {
            if (string.IsNullOrEmpty(nombreReal) && string.IsNullOrEmpty(chatGrupal))
            {
                return Json("Datos inválidos");
            }
            try
            {
                MySqlConnection conn = new MySqlConnection(connectionString);
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("update Grupos " +
                    "set chatGrupal = @chatGrupal " +
                    "where nombreReal = @nombreReal", conn);
                cmd.Parameters.AddWithValue("@nombreReal", nombreReal);
                cmd.Parameters.AddWithValue("@chatGrupal", chatGrupal);
                cmd.ExecuteNonQuery();
                conn.Close();
                return JsonConvert.SerializeObject("Se actualizaron los mensajes correctamente");
            }
            catch(Exception ex)
            {
                return JsonConvert.SerializeObject($"No se pudo editar el grupo: {ex.Message}");
            }
        }
        public dynamic PRObtenerMensajes(string nombre)
        {
            try
            {
                MySqlConnection conn = new MySqlConnection(connectionString);
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("select chatGrupal from Grupos where nombreReal=@nombreReal", conn);
                cmd.Parameters.AddWithValue("@nombreReal", nombre);
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    string chatGrupal = reader["chatGrupal"].ToString();
                    conn.Close();
                    return JsonConvert.SerializeObject(chatGrupal);
                }
                else
                {
                    return JsonConvert.SerializeObject("No se encuentra el grupo");
                }
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject("No se encuentra el grupo");
            }
        }




    }
}
