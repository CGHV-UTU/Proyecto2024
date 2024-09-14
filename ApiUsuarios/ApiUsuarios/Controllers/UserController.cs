using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;

namespace ApiUsuarios.Controllers
{
    [System.Web.Mvc.RoutePrefix("user")]
    public class UserController : Controller
    {
        private MySqlConnection conn = new MySqlConnection("Server=localhost; database=infini; uID=root; pwd=;");
        public class usuario
        {
            public string nombreDeCuenta { get; set;}
            public string nombreVisible { get; set; }
            public string email { get; set; }
            public string descripcion { get; set; }
            public string foto { get; set; }
            public string configuraciones { get; set; }
            public string genero { get; set; }
            public string fechaDeNacimiento { get; set; }
            public string estadoDeCuenta { get; set; }
            public string contraseña { get; set; }
            public string notificaciones { get; set; }
        }
        public class Reportes
        {
            public string nombreDeCuenta { get; set; }
            public string cuentaReporteUsuario { get; set; }
            public string idPost { get; set; }
            public string creadorDelPost { get; set; }
            public string idComentario { get; set; }
            public string creadorDelComentario { get; set; }
            public string nombreGrupo { get; set; }
            public string idEvento { get; set; }
            public string tipo { get; set; }
            public string descripcion { get; set; }
        }
        public class Notificaciones
        {
            public string nombreDeCuenta { get; set; }
            public string idNotificacion { get; set; }
            public string tipo { get; set; }
            public string texto { get; set; }
            public string fechaYhora { get; set; }
            public string imagen { get; set; }
        }

        //Conexiones con el repositorio de github
        public async Task<string> SubirImagenAGitHub(string imagen)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    string token = "11BKZVKOQ0DjsNNMCl27pG_bWGpU4CD8HpcEIQooMyAsLtedjVMN7kzcrz1WrYLmA9NOKBAL3W9WQKb76D"; // Token para repositorio privado. Cambiar por el token real
                    string nombreDeImagen = GenerarIdAleatorio(8) + ".png"; // nombre aleatorio para que el nombre del archivo no se repita
                    string carpeta = "UserImages"; // Carpeta de GitHub en donde se guarda la imagen
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

        [System.Web.Mvc.HttpPost]
        [System.Web.Mvc.Route("RegistrarUsuario")]
        public async Task<dynamic> RegistrarUsuario([System.Web.Http.FromBody] usuario user)
        {
            try
            {
                using (conn)
                {
                    string linkImagen = null;

                    conn.Open();

                    if (!string.IsNullOrEmpty(user.nombreDeCuenta)
                        && !string.IsNullOrEmpty(user.nombreVisible)
                        && !string.IsNullOrEmpty(user.email)
                        && !string.IsNullOrEmpty(user.foto)
                        && !string.IsNullOrEmpty(user.configuraciones)
                        && !string.IsNullOrEmpty(user.genero)
                        && !string.IsNullOrEmpty(user.fechaDeNacimiento)
                        && !string.IsNullOrEmpty(user.estadoDeCuenta)
                        && !string.IsNullOrEmpty(user.contraseña))
                    {
                        // Convertir fechaDeNacimiento de string a DateTime
                        DateTime fechaNacimiento;
                        if (!DateTime.TryParse(user.fechaDeNacimiento, out fechaNacimiento))
                        {
                            conn.Close();
                            return Json("guardado incorrecto: formato de fecha inválido");
                        }

                        linkImagen = await SubirImagenAGitHub(user.foto);

                        // Inserción en la tabla USUARIOS
                        string query = string.IsNullOrEmpty(user.descripcion) ?
                            "INSERT INTO Usuarios (nombreDeCuenta,nombreVisible,email,foto,configuraciones,genero,fechaDeNacimiento,estadoDeCuenta) " +
                            "VALUES (@nombredecuenta,@nombrevisible,@email,@foto,@configuraciones,@genero,@fechaDeNacimiento,@estadoDeCuenta)" :
                            "INSERT INTO Usuarios (nombreDeCuenta,nombreVisible,email,descripcion,foto,configuraciones,genero,fechaDeNacimiento,estadoDeCuenta) " +
                            "VALUES (@nombredecuenta,@nombrevisible,@email,@descripcion,@foto,@configuraciones,@genero,@fechaDeNacimiento,@estadoDeCuenta)";

                        using (MySqlCommand cmd = new MySqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@nombredecuenta", user.nombreDeCuenta);
                            cmd.Parameters.AddWithValue("@nombrevisible", user.nombreVisible);
                            cmd.Parameters.AddWithValue("@email", user.email);
                            cmd.Parameters.AddWithValue("@foto", linkImagen);
                            cmd.Parameters.AddWithValue("@configuraciones", user.configuraciones);
                            cmd.Parameters.AddWithValue("@genero", user.genero);
                            cmd.Parameters.AddWithValue("@fechaDeNacimiento", fechaNacimiento);
                            cmd.Parameters.AddWithValue("@estadoDeCuenta", user.estadoDeCuenta);
                            cmd.Parameters.AddWithValue("@descripcion", string.IsNullOrEmpty(user.descripcion) ? DBNull.Value : (object)user.descripcion);
                            cmd.ExecuteNonQuery();
                        }

                        // Inserción en la tabla LOGIN
                        using (MySqlCommand cmd2 = new MySqlCommand("INSERT INTO Login (nombreDeCuenta, contrasena) VALUES (@nombredecuenta, @contraseña)", conn))
                        {
                            cmd2.Parameters.AddWithValue("@nombredecuenta", user.nombreDeCuenta);
                            cmd2.Parameters.AddWithValue("@contraseña", user.contraseña);
                            cmd2.ExecuteNonQuery();
                        }

                        conn.Close();
                        return Json("guardado correcto");
                    }
                    else
                    {
                        conn.Close();
                        return Json("guardado incorrecto1");
                    }
                }
            }
            catch (Exception ex)
            {
                return Json($"guardado incorrecto2: {ex.Message}");
            }
        }

        [System.Web.Mvc.HttpPut]
        [System.Web.Mvc.Route("obtenerUsuario")]
        public async Task<dynamic> ObtenerUsuario([FromBody] usuario user)
        {
            try
            {
                conn.Open();
                MySqlCommand command = new MySqlCommand("SELECT nombreVisible,email,descripcion,foto,configuraciones,genero,fechaDeNacimiento,estadoDeCuenta FROM Usuarios WHERE nombreDeCuenta=@nombreDeCuenta", conn);
                command.Parameters.AddWithValue("@nombreDeCuenta", user.nombreDeCuenta);
                MySqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    var data = new
                    {
                        nombreVisible = reader["nombreVisible"].ToString(),
                        email = reader["email"].ToString(),
                        descripcion = reader["descripcion"].ToString() ?? "",
                        foto = await CargarImagenDeGitHub(reader["foto"].ToString()),
                        configuraciones = reader["configuraciones"].ToString(),
                        genero = reader["genero"].ToString(),
                        fechaDeNacimiento = reader["fechaDeNacimiento"].ToString(),
                        estadoDeCuenta = reader["estadoDeCuenta"].ToString()
                    };
                    conn.Close();
                    return Json(data);
                }
                else
                {
                    conn.Close();
                    return Json("no se encuentra");
                }
            }
            catch (Exception ex)
            {
                return Json("no se encuentra");
            }
        }

        [System.Web.Mvc.HttpPut]
        [System.Web.Mvc.Route("obtenerImagenUsuario")]
        public async Task<dynamic> obtenerImagenUsuario([FromBody] usuario user)
        {
            try
            {
                conn.Open();
                MySqlCommand command = new MySqlCommand("SELECT foto FROM Usuarios WHERE nombreDeCuenta=@nombreDeCuenta", conn);
                command.Parameters.AddWithValue("@nombreDeCuenta", user.nombreDeCuenta);
                MySqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    string foto = await CargarImagenDeGitHub(reader["foto"].ToString());
                    conn.Close();
                    return Json(foto, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    conn.Close();
                    return Json("no se encuentra "+user.nombreDeCuenta, JsonRequestBehavior.AllowGet);
                }
            }
            catch
            {
                return Json("no se encuentra " + user.nombreDeCuenta, JsonRequestBehavior.AllowGet);
            }
        }

        [System.Web.Mvc.HttpPut]
        [System.Web.Mvc.Route("obtenerImagenNombreVyDescUsuario")]
        public async Task<dynamic> obtenerImagenNombreVyDescUsuario([FromBody] usuario user)
        {
            try
            {
                conn.Open();
                MySqlCommand command = new MySqlCommand("SELECT nombreVisible,descripcion,foto FROM Usuarios WHERE nombreDeCuenta=@nombreDeCuenta", conn);
                command.Parameters.AddWithValue("@nombreDeCuenta", user.nombreDeCuenta);
                MySqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    var data = new
                    {
                        nombreVisible = reader["nombreVisible"].ToString(),
                        descripcion = reader["descripcion"].ToString() ?? "",
                        foto = await CargarImagenDeGitHub(reader["foto"].ToString()),
                    };
                    conn.Close();
                    return Json(data, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    conn.Close();
                    return Json("no se encuentra", JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json("no se encuentra", JsonRequestBehavior.AllowGet);
            }
        }

        [System.Web.Mvc.HttpPut]
        [System.Web.Mvc.Route ("ExisteUsuario")]
        public dynamic ExisteUsuario([FromBody] usuario user)
        {
            try
            {
                using (conn)
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("SELECT 1 FROM Usuarios WHERE nombreDeCuenta=@nombreDeCuenta", conn);
                    cmd.Parameters.AddWithValue("@nombreDeCuenta", user.nombreDeCuenta);

                    var result = cmd.ExecuteScalar();

                    conn.Close();

                    if (result != null)
                    {
                        return Json(new { mensaje = "El usuario existe", existe = true }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { mensaje = "El usuario no existe", existe = false }, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            catch (Exception ex)
            {
                return Json(new { mensaje = "Error en la consulta", error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [System.Web.Mvc.HttpPut]
        [System.Web.Mvc.Route("ModificarUsuario")]
        public async Task<dynamic> ModificarUsuario([FromBody] usuario user)
        {
            try
            {
                using (conn)
                {
                    string linkImagen = null;

                    conn.Open();
                    if (string.IsNullOrEmpty(user.nombreDeCuenta))
                        return Json(new { mensaje = "Guardado incorrecto: falta nombreDeCuenta" });
                    if (string.IsNullOrEmpty(user.nombreVisible))
                        return Json(new { mensaje = "Guardado incorrecto: falta nombreVisible" });
                    if (string.IsNullOrEmpty(user.email))
                        return Json(new { mensaje = "Guardado incorrecto: falta email" });
                    if (string.IsNullOrEmpty(user.foto))
                        return Json(new { mensaje = "Guardado incorrecto: falta foto" });
                    if (string.IsNullOrEmpty(user.configuraciones))
                        return Json(new { mensaje = "Guardado incorrecto: falta configuraciones" });
                    if (string.IsNullOrEmpty(user.genero))
                        return Json(new { mensaje = "Guardado incorrecto: falta genero" });
                    if (string.IsNullOrEmpty(user.fechaDeNacimiento))
                        return Json(new { mensaje = "Guardado incorrecto: falta fechaDeNacimiento" });
                    if (string.IsNullOrEmpty(user.estadoDeCuenta))
                        return Json(new { mensaje = "Guardado incorrecto: falta estadoDeCuenta" });
                    Console.WriteLine(user.foto);
                    linkImagen = await SubirImagenAGitHub(user.foto);
                    // Convertir fechaDeNacimiento de string a DateTime
                    DateTime fechaNacimiento;
                    if (!DateTime.TryParse(user.fechaDeNacimiento, out fechaNacimiento))
                    {
                        conn.Close();
                        return Json("guardado incorrecto: formato de fecha inválido");
                    }

                    string query = @"UPDATE Usuarios 
                                 SET nombreVisible=@nombreVisible, 
                                     email=@Email, 
                                     descripcion=@Descripcion, 
                                     foto=@Foto, 
                                     configuraciones=@Configuraciones, 
                                     genero=@Genero, 
                                     fechaDeNacimiento=@FechaDeNacimiento, 
                                     estadoDeCuenta=@EstadoDeCuenta 
                                 WHERE nombreDeCuenta=@NombreDeCuenta";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@NombreDeCuenta", user.nombreDeCuenta);
                        cmd.Parameters.AddWithValue("@NombreVisible", user.nombreVisible);
                        cmd.Parameters.AddWithValue("@Email", user.email);
                        cmd.Parameters.AddWithValue("@Descripcion", user.descripcion ?? string.Empty);
                        cmd.Parameters.AddWithValue("@Foto", linkImagen);
                        cmd.Parameters.AddWithValue("@Configuraciones", user.configuraciones);
                        cmd.Parameters.AddWithValue("@Genero", user.genero);
                        cmd.Parameters.AddWithValue("@FechaDeNacimiento", fechaNacimiento);
                        cmd.Parameters.AddWithValue("@EstadoDeCuenta", user.estadoDeCuenta);
                        cmd.ExecuteNonQuery();
                    }
                    conn.Close();
                    return Json(new { mensaje = "Guardado correcto" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { mensaje = "Guardado incorrecto: error en el servidor", error = ex.Message });
            }

        }

        [System.Web.Mvc.HttpPost]
        [System.Web.Mvc.Route("CambiarConfiguracion")]
        public dynamic CambiarConfiguracion([FromBody] usuario user)
        {
            if (user == null)
            {
                return Json("nulo");
            }
            else
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("UPDATE Usuarios SET configuraciones=@configuraciones WHERE nombreDeCuenta=@nombreDeCuenta", conn);
                cmd.Parameters.AddWithValue("@nombreDeCuenta", user.nombreDeCuenta);
                cmd.Parameters.AddWithValue("@configuraciones", user.configuraciones);
                cmd.ExecuteNonQuery();
                conn.Close();
                return Json("Configuracion correcta");
            }
        }

        [System.Web.Mvc.HttpPost]
        [System.Web.Mvc.Route("ConseguirConfiguracion")]
        public dynamic ConseguirConfiguracion([FromBody] usuario user)
        {
            if (user == null)
            {
                return Json("nulo");
            }
            else
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT configuraciones FROM Usuarios WHERE nombreDeCuenta=@nombreDeCuenta", conn);
                cmd.Parameters.AddWithValue("@nombreDeCuenta", user.nombreDeCuenta);
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    string configuraciones = reader["configuraciones"].ToString();
                    conn.Close();
                    return Json(configuraciones);
                }
                else
                {
                    conn.Close();
                    return Json("Hubo un error"+user.nombreDeCuenta);
                }
            }
        }

        [System.Web.Mvc.HttpPost]
        [System.Web.Mvc.Route("agregarNotificaciones")]
        public async Task<dynamic> AgregarNotificaciones([FromBody] Notificaciones notificaciones)
        {

            if (notificaciones == null ||
                string.IsNullOrEmpty(notificaciones.nombreDeCuenta) ||
                string.IsNullOrEmpty(notificaciones.texto) ||
                string.IsNullOrEmpty(notificaciones.tipo) ||
                string.IsNullOrEmpty(notificaciones.imagen))
            {
                return Json("Faltan datos obligatorios.");
            }
            string linkImagen = null;
            linkImagen = await SubirImagenAGitHub(notificaciones.imagen);
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(
                    "INSERT INTO Notificaciones (tipo, texto, fechaYHora, imagen, nombreDeCuenta) VALUES (@tipo, @texto, @fechaYHora, @imagen, @nombreDeCuenta)", conn);
                cmd.Parameters.AddWithValue("@tipo", notificaciones.tipo);
                cmd.Parameters.AddWithValue("@texto", notificaciones.texto);
                cmd.Parameters.AddWithValue("@fechaYHora", DateTime.Now);
                cmd.Parameters.AddWithValue("@imagen", linkImagen);
                cmd.Parameters.AddWithValue("@nombreDeCuenta", notificaciones.nombreDeCuenta);
                cmd.ExecuteNonQuery();
                conn.Close();
                return Json("Correcto");
            }
            catch (Exception ex)
            {
                conn.Close();
                return Json("Error: " + ex.Message);
            }
        }



        [System.Web.Mvc.HttpPost]
        [System.Web.Mvc.Route("ActualizarNotificaciones")]
        public async Task<dynamic> ActualizarNotificaciones([FromBody] Notificaciones notificaciones)
        {
            string linkImagen = null;
            if (notificaciones == null ||
                string.IsNullOrEmpty(notificaciones.nombreDeCuenta) ||
                string.IsNullOrEmpty(notificaciones.texto) ||
                string.IsNullOrEmpty(notificaciones.tipo) ||
                string.IsNullOrEmpty(notificaciones.imagen))
            {
                return Json("Valor nulo: Faltan datos obligatorios.");
            }
            linkImagen = await SubirImagenAGitHub(notificaciones.imagen);
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(
                    "UPDATE Notificaciones SET texto = @texto, tipo = @tipo, fechaYHora = @fechaYHora, imagen = @imagen WHERE nombreDeCuenta = @nombreDeCuenta", conn);
                cmd.Parameters.AddWithValue("@nombreDeCuenta", notificaciones.nombreDeCuenta);
                cmd.Parameters.AddWithValue("@texto", notificaciones.texto);
                cmd.Parameters.AddWithValue("@tipo", notificaciones.tipo);
                cmd.Parameters.AddWithValue("@fechaYHora", DateTime.Now);
                cmd.Parameters.AddWithValue("@imagen", linkImagen);
                cmd.ExecuteNonQuery();
                conn.Close();
                return Json("Correcto");
            }
            catch (Exception ex)
            {
                conn.Close();
                return Json("Error: " + ex.Message);
            }
        }


        [System.Web.Mvc.HttpPost]
        [System.Web.Mvc.Route("ConseguirNotificaciones")]
        public async Task<JsonResult> ConseguirNotificaciones([FromBody] Notificaciones notificaciones)
        {
            if (notificaciones == null || string.IsNullOrEmpty(notificaciones.nombreDeCuenta))
            {
                return Json("Valor nulo: nombreDeCuenta es obligatorio.");
            }

            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT texto, tipo, fechaYHora, imagen FROM Notificaciones WHERE nombreDeCuenta = @nombreDeCuenta", conn);
                cmd.Parameters.AddWithValue("@nombreDeCuenta", notificaciones.nombreDeCuenta);
                MySqlDataReader reader = cmd.ExecuteReader();

                List<dynamic> notificacionesList = new List<dynamic>();

                while (reader.Read())
                {
                    notificacionesList.Add(new
                    {
                        texto = reader["texto"].ToString(),
                        tipo = reader["tipo"].ToString(),
                        fechaYHora = reader["fechaYHora"].ToString(),
                        imagen = await CargarImagenDeGitHub(reader["imagen"].ToString())
                    });
                }

                conn.Close();

                if (notificacionesList.Count > 0)
                {
                    return Json(notificacionesList);
                }
                else
                {
                    return Json("No se encontraron notificaciones para el usuario: " + notificaciones.nombreDeCuenta);
                }
            }
            catch (Exception ex)
            {
                conn.Close();
                return Json("Error: " + ex.Message);
            }
        }



        [System.Web.Mvc.HttpPost]
        [System.Web.Mvc.Route("Reportar")]
        public async Task<dynamic> Reportar([FromBody] Reportes reporte)
        {
            if (string.IsNullOrEmpty(reporte.nombreDeCuenta))
            {
                return Json("Debe existir un usuario que reporta");
            }

            try
            {
                using (var conn = new MySqlConnection("Server=localhost; database=infini; uID=root; pwd=;")) // Creo pq me obliga el testing
                {
                    await conn.OpenAsync(); 

                    if (!string.IsNullOrEmpty(reporte.cuentaReporteUsuario))
                    {
                        MySqlCommand cmd = new MySqlCommand(
                            "INSERT INTO Reportes (nombreDeCuenta, cuentaReporteUsuario, tipo, descripcion) VALUES (@nombreDeCuenta, @cuentaReporteUsuario, @tipo, @descripcion)", conn);
                        cmd.Parameters.AddWithValue("@nombreDeCuenta", reporte.nombreDeCuenta);
                        cmd.Parameters.AddWithValue("@cuentaReporteUsuario", reporte.cuentaReporteUsuario);
                        cmd.Parameters.AddWithValue("@tipo", reporte.tipo);
                        cmd.Parameters.AddWithValue("@descripcion", reporte.descripcion);
                        await cmd.ExecuteNonQueryAsync();
                        return Json("Se ha reportado correctamente al usuario: " + reporte.cuentaReporteUsuario);
                    }

                    if (!string.IsNullOrEmpty(reporte.idPost) && !string.IsNullOrEmpty(reporte.creadorDelPost))
                    {
                        MySqlCommand cmd = new MySqlCommand(
                            "INSERT INTO Reportes (nombreDeCuenta, idPost, creadorDelPost, tipo, descripcion) VALUES (@nombreDeCuenta, @idPost, @creadorDelPost, @tipo, @descripcion)", conn);
                        cmd.Parameters.AddWithValue("@nombreDeCuenta", reporte.nombreDeCuenta);
                        cmd.Parameters.AddWithValue("@idPost", int.Parse(reporte.idPost));
                        cmd.Parameters.AddWithValue("@creadorDelPost", reporte.creadorDelPost);
                        cmd.Parameters.AddWithValue("@tipo", reporte.tipo);
                        cmd.Parameters.AddWithValue("@descripcion", reporte.descripcion);
                        await cmd.ExecuteNonQueryAsync();
                        return Json("Se ha reportado correctamente al Post: " + reporte.idPost);
                    }

                    if (!string.IsNullOrEmpty(reporte.idComentario) && !string.IsNullOrEmpty(reporte.creadorDelComentario))
                    {
                        MySqlCommand cmd = new MySqlCommand(
                            "INSERT INTO Reportes (nombreDeCuenta, idComentario, creadorDelComentario, tipo, descripcion) VALUES (@nombreDeCuenta, @idComentario, @creadorDelComentario, @tipo, @descripcion)", conn);
                        cmd.Parameters.AddWithValue("@nombreDeCuenta", reporte.nombreDeCuenta);
                        cmd.Parameters.AddWithValue("@idComentario", int.Parse(reporte.idComentario));
                        cmd.Parameters.AddWithValue("@creadorDelComentario", reporte.creadorDelComentario);
                        cmd.Parameters.AddWithValue("@tipo", reporte.tipo);
                        cmd.Parameters.AddWithValue("@descripcion", reporte.descripcion);
                        await cmd.ExecuteNonQueryAsync();
                        return Json("Se ha reportado correctamente al Comentario: " + reporte.idComentario);
                    }

                    if (!string.IsNullOrEmpty(reporte.nombreGrupo))
                    {
                        MySqlCommand cmd = new MySqlCommand(
                            "INSERT INTO Reportes (nombreDeCuenta, nombreGrupo, tipo, descripcion) VALUES (@nombreDeCuenta, @nombreGrupo, @tipo, @descripcion)", conn);
                        cmd.Parameters.AddWithValue("@nombreDeCuenta", reporte.nombreDeCuenta);
                        cmd.Parameters.AddWithValue("@nombreGrupo", reporte.nombreGrupo);
                        cmd.Parameters.AddWithValue("@tipo", reporte.tipo);
                        cmd.Parameters.AddWithValue("@descripcion", reporte.descripcion);
                        await cmd.ExecuteNonQueryAsync();
                        return Json("Se ha reportado correctamente al Grupo: " + reporte.nombreGrupo);
                    }

                    if (!string.IsNullOrEmpty(reporte.idEvento))
                    {
                        MySqlCommand cmd = new MySqlCommand(
                            "INSERT INTO Reportes (nombreDeCuenta, idEvento, tipo, descripcion) VALUES (@nombreDeCuenta, @idEvento, @tipo, @descripcion)", conn);
                        cmd.Parameters.AddWithValue("@nombreDeCuenta", reporte.nombreDeCuenta);
                        cmd.Parameters.AddWithValue("@idEvento", int.Parse(reporte.idEvento));
                        cmd.Parameters.AddWithValue("@tipo", reporte.tipo);
                        cmd.Parameters.AddWithValue("@descripcion", reporte.descripcion);
                        await cmd.ExecuteNonQueryAsync();
                        return Json("Se ha reportado correctamente al Evento: " + reporte.idEvento);
                    }

                    return Json("Debe existir algo a lo que reportar");
                }
            }
            catch (Exception ex)
            {
                return Json("Error: " + ex.Message);
            }
        }

    }


}

