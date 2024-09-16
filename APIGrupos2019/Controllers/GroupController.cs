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
            public string nombreDeCuenta { get; set; }
            public string rol { get; set; }
        }

        public class Reporte
        {
            public string nombreReal { get; set; }
            public string tipo { get; set; }
            public string descripcion { get; set; }
        }
        public class GrupoResponse
        {
            public string nombreReal { get; set; }
            public string nombreVisible { get; set; }
            public string configuracion { get; set; }
            public string descripcion { get; set; }
            public string foto { get; set; }
        }


        public string connectionString = "Server=localhost; database=infini; uID=root; pwd=;";

        public async Task<string> SubirImagenAGitHub(string imagen)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    string token = "11BKZVKOQ0DjsNNMCl27pG_bWGpU4CD8HpcEIQooMyAsLtedjVMN7kzcrz1WrYLmA9NOKBAL3W9WQKb76D"; // Token para repositorio privado. Cambiar por el token real
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
                string token = "11BKZVKOQ0DjsNNMCl27pG_bWGpU4CD8HpcEIQooMyAsLtedjVMN7kzcrz1WrYLmA9NOKBAL3W9WQKb76D"; // Token para repositorio privado. Cambiar por el token real
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
                    || string.IsNullOrEmpty(group.imagen)
                    || string.IsNullOrEmpty(group.nombreDeCuenta))
                {
                    return Json("Valores nulos");
                }

                linkImagen = await SubirImagenAGitHub(group.imagen);
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand("INSERT INTO Grupos (nombreReal, nombreVisible, configuracion, foto) VALUES (@nombreReal, @nombreVisible, @configuracion, @foto)", conn))
                    {
                        cmd.Parameters.AddWithValue("@nombreReal", group.nombreReal);
                        cmd.Parameters.AddWithValue("@nombreVisible", group.nombreVisible);
                        cmd.Parameters.AddWithValue("@configuracion", group.configuracion);
                        cmd.Parameters.AddWithValue("@foto", linkImagen);
                        cmd.ExecuteNonQuery();
                    }
                    using (MySqlCommand cmd2 = new MySqlCommand("INSERT INTO Participa (nombreReal, nombreDeCuenta, rol) VALUES (@nombreReal, @nombreDeCuenta, 'c')", conn))
                    {
                        cmd2.Parameters.AddWithValue("@nombreDeCuenta", group.nombreDeCuenta);
                        cmd2.Parameters.AddWithValue("@nombreReal", group.nombreReal);
                        cmd2.ExecuteNonQuery();
                    }
                    if (!string.IsNullOrEmpty(group.descripcion))
                    {
                        using (MySqlCommand cmd3 = new MySqlCommand("UPDATE Grupos SET descripcion = @descripcion WHERE nombreReal = @nombreReal", conn))
                        {
                            cmd3.Parameters.AddWithValue("@descripcion", group.descripcion);
                            cmd3.Parameters.AddWithValue("@nombreReal", group.nombreReal);
                            cmd3.ExecuteNonQuery();
                        }
                    }
                }

                return Json("Registro correcto");
            }
            catch (Exception ex)
            {
                return Json($"Registro fallido: {ex.Message}");
            }
        }


        [System.Web.Http.HttpPut]
        [System.Web.Http.Route("ObtenerGruposPorNombreVisibleYUsuario")]
        public async Task<IHttpActionResult> ObtenerGruposPorNombreVisibleYUsuario([FromBody] Grupo groupData)
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
                        cmd.Parameters.AddWithValue("@nombreVisible", groupData.nombreVisible);
                        cmd.Parameters.AddWithValue("@nombreDeCuenta", groupData.nombreDeCuenta);

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            List<GrupoResponse> grupos = new List<GrupoResponse>();

                            while (reader.Read())
                            {
                                string nombreReal = reader["nombreReal"].ToString();
                                string configuracion = reader["configuracion"].ToString();
                                string descripcion = reader["descripcion"].ToString();
                                string foto = await CargarImagenDeGitHub(reader["foto"].ToString());

                                var grupo = new GrupoResponse
                                {
                                    nombreReal = nombreReal,
                                    nombreVisible = groupData.nombreVisible,
                                    configuracion = configuracion,
                                    descripcion = descripcion,
                                    foto = foto
                                };

                                grupos.Add(grupo);
                            }

                            conn.Close();

                            if (grupos.Count > 0)
                            {
                                return Json(grupos);
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


        [System.Web.Http.HttpPut]
        [System.Web.Http.Route("ObtenerGrupo")]
        public IHttpActionResult ObtenerGrupo([FromBody] Grupo groupData)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("SELECT nombreReal, nombreVisible, configuracion, descripcion, foto FROM Grupos WHERE nombreReal=@nombreReal", conn);
                    cmd.Parameters.AddWithValue("@nombreReal", groupData.nombreReal);
                    MySqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        var grupo = new GrupoResponse
                        {
                            nombreReal = reader["nombreReal"].ToString(),
                            nombreVisible = reader["nombreVisible"].ToString(),
                            configuracion = reader["configuracion"].ToString(),
                            descripcion = reader["descripcion"].ToString(),
                            foto = reader["foto"].ToString()
                        };

                        conn.Close();
                        return Json(grupo);
                    }
                    else
                    {
                        conn.Close();
                        return Json("No se encuentra el grupo");
                    }
                }
            }
            catch (Exception)
            {
                return Json("No se encuentra el grupo");
            }
        }


        // Por revisar, no estoy seguro de como va a ser esto porque aun no hicimos mensajes
        [System.Web.Http.HttpPut]
        [System.Web.Http.Route("ObtenerMensajes")]
        public dynamic ObtenerMensajes([FromBody] Grupo groupData)
        {
            try
            {
                MySqlConnection conn = new MySqlConnection(connectionString);
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("select chatGrupal from Grupos where nombreReal=@nombreReal", conn);
                cmd.Parameters.AddWithValue("@nombreReal", groupData.nombreReal);
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
            catch (Exception)
            {
                return Json("No se encuentra el grupo");
            }
        }

        [System.Web.Http.HttpPut]
        [System.Web.Http.Route("EliminarGrupo")]
        public dynamic EliminarGrupo([FromBody] Grupo groupData)
        {
            try
            {
                MySqlConnection conn = new MySqlConnection(connectionString);
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("DELETE FROM Reportes WHERE nombreGrupo = @nombreReal;" +
                    "DELETE FROM Participa WHERE nombreReal = @nombreReal;" +
                    "DELETE FROM PostGrupo WHERE nombreReal = @nombreReal;" +
                    "DELETE FROM Grupos WHERE nombreReal = @nombreReal", conn);
                cmd.Parameters.AddWithValue("@nombreReal", groupData.nombreReal);
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
        public async Task<IHttpActionResult> EditarGrupo([FromBody] Grupo group)
        {
            string linkImagen = null;

            // Validate input
            if (string.IsNullOrEmpty(group.nombreReal) ||
                string.IsNullOrEmpty(group.nombreVisible) ||
                string.IsNullOrEmpty(group.configuracion) ||
                string.IsNullOrEmpty(group.imagen) ||
                group.configuracion.Length > 255)
            {
                return Json("Datos inválidos");
            }

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(@"
                UPDATE Grupos 
                SET nombreVisible = @nombreVisible,
                    configuracion = @configuracion,
                    descripcion = @descripcion,
                    foto = @foto 
                WHERE nombreReal = @nombreReal", conn);

                    cmd.Parameters.AddWithValue("@nombreReal", group.nombreReal);
                    cmd.Parameters.AddWithValue("@nombreVisible", group.nombreVisible);
                    cmd.Parameters.AddWithValue("@configuracion", group.configuracion);
                    cmd.Parameters.AddWithValue("@descripcion", group.descripcion);
                    cmd.Parameters.AddWithValue("@foto", linkImagen = await SubirImagenAGitHub(group.imagen));
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }

                return Json("Se editó el grupo correctamente");
            }
            catch
            {
                return Json("No se pudo editar el grupo");
            }
        }


        // Por revisar, no estoy seguro de como va a ser esto porque aun no hicimos mensajes
        [System.Web.Http.HttpPut]
        [System.Web.Http.Route("ActualizarMensajes")]
        public dynamic ActualizarMensajes([FromBody] Grupo groupData)
        {
            if (string.IsNullOrEmpty(groupData.nombreReal) && string.IsNullOrEmpty(groupData.chatGrupal))
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
                cmd.Parameters.AddWithValue("@nombreReal", groupData.nombreReal);
                cmd.Parameters.AddWithValue("@chatGrupal", groupData.chatGrupal);
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
        [System.Web.Http.Route("ObtenerGruposPorUsuario")]
        public async Task<IHttpActionResult> ObtenerGruposPorUsuario([FromBody] Grupo groupData)
        {
            try
            {
                List<GrupoResponse> grupos = new List<GrupoResponse>();

                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    MySqlCommand cmd = new MySqlCommand(@"
                SELECT g.nombreReal, g.nombreVisible, g.configuracion, g.descripcion, g.foto
                FROM Grupos g
                JOIN Participa ug ON g.nombreReal = ug.nombreReal
                WHERE ug.nombreDeCuenta = @nombreDeCuenta", conn);

                    cmd.Parameters.AddWithValue("@nombreDeCuenta", groupData.nombreDeCuenta);
                    MySqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        GrupoResponse grupo = new GrupoResponse
                        {
                            nombreReal = reader["nombreReal"].ToString(),
                            nombreVisible = reader["nombreVisible"].ToString(),
                            configuracion = reader["configuracion"].ToString(),
                            descripcion = reader["descripcion"].ToString(),
                            foto = await CargarImagenDeGitHub(reader["foto"].ToString())
                        };
                        grupos.Add(grupo);
                    }

                    conn.Close();
                }

                return Json(grupos);
            }
            catch (Exception)
            {
                return Json("No se pudieron obtener los Grupos");
            }
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("AgregarUsuarioAGrupo")]
        public async Task<IHttpActionResult> AgregarUsuarioAGrupo([FromBody] Grupo groupData)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    await conn.OpenAsync();
                    Console.WriteLine("Database connection opened successfully for AgregarUsuarioAGrupo.");

                    string query = "INSERT INTO Participa (nombreDeCuenta, nombreReal, rol) VALUES (@nombreUsuario, @nombreGrupo, @rol)";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@nombreUsuario", groupData.nombreDeCuenta);
                        cmd.Parameters.AddWithValue("@nombreGrupo", groupData.nombreReal);
                        cmd.Parameters.AddWithValue("@rol", groupData.rol);

                        Console.WriteLine($"Executing query: INSERT INTO Participa (nombreDeCuenta, nombreReal, rol) VALUES ({groupData.nombreDeCuenta}, {groupData.nombreReal}, {groupData.rol})");

                        await cmd.ExecuteNonQueryAsync();
                    }
                }

                Console.WriteLine("Usuario agregado al grupo successfully.");
                return Json("Usuario agregado al grupo");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in AgregarUsuarioAGrupo: {ex.Message}");
                return Json($"No se pudo agregar el usuario al grupo: {ex.Message}");
            }
        }




        [System.Web.Http.HttpPost] //YA NO EXISTE GRUPOUG
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

        
    }
}
