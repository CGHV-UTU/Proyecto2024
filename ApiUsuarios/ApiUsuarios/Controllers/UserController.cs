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
        public class Reporte
        {
            public string usuario { get; set; }
            public string tipo { get; set; }
            public string descripcion { get; set; }
        }

        //Conexiones con el repositorio de github
        public async Task<string> SubirImagenAGitHub(string imagen)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    string token = "token"; // Token para repositorio privado. Cambiar por el token real
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
                string token = "token"; // Token para repositorio privado. Cambiar por el token real
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

        public class pruebas
        {
            public string User { get; set; }
        }

        [System.Web.Mvc.HttpPost]
        [System.Web.Mvc.Route("prueba")]
        public dynamic prueba([FromBody] pruebas pruebas)
        {
            return pruebas.User+" si";
        }

        [System.Web.Mvc.HttpPost]
        [System.Web.Mvc.Route("obtenerUsuario")]
        public async Task<dynamic> ObtenerUsuario(string nombredecuenta)
        {
            try
            {
                conn.Open();
                MySqlCommand command = new MySqlCommand("SELECT nombreVisible,email,descripcion,foto,configuraciones,genero,fechaDeNacimiento,estadoDeCuenta FROM Usuarios WHERE nombreDeCuenta=@nombreDeCuenta", conn);
                command.Parameters.AddWithValue("@nombreDeCuenta", nombredecuenta);
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

        [System.Web.Mvc.HttpGet]
        [System.Web.Mvc.Route("obtenerImagenUsuario")]
        public async Task<dynamic> obtenerImagenUsuario(string nombredecuenta)
        {
            try
            {
                conn.Open();
                MySqlCommand command = new MySqlCommand("SELECT foto FROM Usuarios WHERE nombreDeCuenta=@nombreDeCuenta", conn);
                command.Parameters.AddWithValue("@nombreDeCuenta", nombredecuenta);
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
                    return Json("no se encuentra "+nombredecuenta, JsonRequestBehavior.AllowGet);
                }
            }
            catch
            {
                return Json("no se encuentra " + nombredecuenta, JsonRequestBehavior.AllowGet);
            }
        }

        [System.Web.Mvc.HttpGet]
        [System.Web.Mvc.Route("obtenerImagenNombreVyDescUsuario")]
        public async Task<dynamic> obtenerImagenNombreVyDescUsuario(string nombredecuenta)
        {
            try
            {
                conn.Open();
                MySqlCommand command = new MySqlCommand("SELECT nombreVisible,descripcion,foto FROM Usuarios WHERE nombreDeCuenta=@nombreDeCuenta", conn);
                command.Parameters.AddWithValue("@nombreDeCuenta", nombredecuenta);
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

        [System.Web.Mvc.HttpGet]
        [System.Web.Mvc.Route ("ExisteUsuario")]
        public dynamic ExisteUsuario( string nombreDeCuenta)
        {
            try
            {
                using (conn)
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("SELECT 1 FROM Usuarios WHERE nombreDeCuenta=@nombreDeCuenta", conn);
                    cmd.Parameters.AddWithValue("@nombreDeCuenta", nombreDeCuenta);

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
        [System.Web.Mvc.Route("ActualizarNotificaciones")]
        public dynamic ActualizarNotificaciones([FromBody] usuario user)
        {
            if (user == null)
            {
                return Json("nulo");
            }
            else
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("UPDATE Usuarios SET notificaciones=@notificaciones WHERE nombreDeCuenta=@nombreDeCuenta", conn);
                cmd.Parameters.AddWithValue("@nombreDeCuenta", user.nombreDeCuenta);
                cmd.Parameters.AddWithValue("@notificaciones", user.notificaciones);
                cmd.ExecuteNonQuery();
                conn.Close();
                return Json("Correcto");
            }
        }

        [System.Web.Mvc.HttpPost]
        [System.Web.Mvc.Route("agregarNotificaciones")]
        public dynamic AgregarNotificaciones(string user, string notificaciones)
        {
            if (!string.IsNullOrEmpty(user) && !string.IsNullOrEmpty(notificaciones))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("UPDATE Usuarios SET notificaciones = CONCAT(notificaciones, @notificaciones) WHERE nombreDeCuenta = @nombreDeCuenta", conn);
                cmd.Parameters.AddWithValue("@nombreDeCuenta", user);
                cmd.Parameters.AddWithValue("@notificaciones", notificaciones);
                cmd.ExecuteNonQuery();
                conn.Close();
                return Json("Correcto");
            }
            return Json("valor nulo: " + user + ", " + notificaciones);
        }

        [System.Web.Mvc.HttpPost]
        [System.Web.Mvc.Route("ConseguirNotificaciones")]
        public dynamic ConseguirNotificaciones([FromBody] usuario user)
        {
            if (user == null)
            {
                return Json("nulo");
            }
            else
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT notificaciones FROM Usuarios WHERE nombreDeCuenta=@nombreDeCuenta", conn);
                cmd.Parameters.AddWithValue("@nombreDeCuenta", user.nombreDeCuenta);
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    string notificaciones = reader["notificaciones"].ToString();
                    return Json(notificaciones);
                    conn.Close();
                }
                else
                {
                    conn.Close();
                    return Json("Hubo un error" + user.nombreDeCuenta);
                }
            }
        }
        
        [System.Web.Mvc.HttpPost]
        [System.Web.Mvc.Route("ReportarUsuario")]
        public dynamic ReportarUsuario([FromBody] Reporte user)
        {
            if (user == null)
            {
                return Json("nulo");
            }
            else
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("INSERT INTO ReporteUsuario (nombreDeUsuario,tipo,descripcion) VALUES (@nombre, @tipo, @descripcion)", conn);
                cmd.Parameters.AddWithValue("@nombre", user.usuario);
                cmd.Parameters.AddWithValue("@tipo", user.tipo);
                cmd.Parameters.AddWithValue("@descripcion", user.descripcion);
                cmd.ExecuteNonQuery();
                conn.Close();
                return Json("Reporte correcto");
            }
        }

        //pruebas de la lógica de la API
        public dynamic PRRegistrarUsuario(usuario user)
        {
            try
            {
                using (conn)
                {
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
                            return JsonConvert.SerializeObject("guardado incorrecto: formato de fecha inválido");
                        }

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
                            cmd.Parameters.AddWithValue("@foto", user.foto);
                            cmd.Parameters.AddWithValue("@configuraciones", user.configuraciones);
                            cmd.Parameters.AddWithValue("@genero", user.genero);
                            cmd.Parameters.AddWithValue("@fechaDeNacimiento", fechaNacimiento);
                            cmd.Parameters.AddWithValue("@estadoDeCuenta", user.estadoDeCuenta);

                            if (!string.IsNullOrEmpty(user.descripcion))
                            {
                                cmd.Parameters.AddWithValue("@descripcion", user.descripcion);
                            }

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
                        return JsonConvert.SerializeObject("guardado correcto");
                    }
                    else
                    {
                        conn.Close();
                        return JsonConvert.SerializeObject("guardado incorrecto1");
                    }
                }
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject($"guardado incorrecto2: {ex.Message}");
            }
        }


        public dynamic PRObtenerUsuario(string nombredecuenta)
        {
            try
            {
                conn.Open();
                MySqlCommand command = new MySqlCommand("SELECT nombreVisible,email,descripcion,foto,configuraciones,genero,fechaDeNacimiento,estadoDeCuenta FROM Usuarios WHERE nombreDeCuenta=@nombreDeCuenta", conn);
                command.Parameters.AddWithValue("@nombreDeCuenta", nombredecuenta);
                MySqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    var data = new
                    {
                        nombreVisible = reader["nombreVisible"].ToString(),
                        email = reader["email"].ToString(),
                        descripcion = reader["descripcion"].ToString() ?? "",
                        foto = reader["foto"].ToString() ?? "",
                        configuraciones = reader["configuraciones"].ToString(),
                        genero = reader["genero"].ToString(),
                        fechaDeNacimiento = reader["fechaDeNacimiento"].ToString(),
                        estadoDeCuenta = reader["estadoDeCuenta"].ToString()
                    };
                    conn.Close();
                    return JsonConvert.SerializeObject(data);
                }
                else
                {
                    conn.Close();
                    return JsonConvert.SerializeObject("no se encuentra");
                }
            }
            catch (Exception)
            {
                return JsonConvert.SerializeObject("no se encuentra");
            }
        }

        public dynamic PRexisteUsuario(string nombredecuenta)
        {
            try
            {
                using (conn)
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("SELECT 1 FROM Usuarios WHERE nombreDeCuenta=@nombreDeCuenta", conn);
                    cmd.Parameters.AddWithValue("@nombreDeCuenta", nombredecuenta);

                    var result = cmd.ExecuteScalar();

                    conn.Close();

                    if (result != null)
                    {
                        return JsonConvert.SerializeObject(new { mensaje = "El usuario existe", existe = true });
                    }
                    else
                    {
                        return JsonConvert.SerializeObject(new { mensaje = "El usuario no existe", existe = false });
                    }
                }
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { mensaje = "Error en la consulta", error = ex.Message });
            }
        }
        public dynamic PRModificarUsuario([FromBody] usuario user)
        {
            try
            {
                using (conn)
                {
                    conn.Open();
                    if (!string.IsNullOrEmpty(user.nombreDeCuenta)
                        && !string.IsNullOrEmpty(user.nombreVisible)
                        && !string.IsNullOrEmpty(user.email)
                        && !string.IsNullOrEmpty(user.foto)
                        && !string.IsNullOrEmpty(user.configuraciones)
                        && !string.IsNullOrEmpty(user.genero)
                        && !string.IsNullOrEmpty(user.fechaDeNacimiento)
                        && !string.IsNullOrEmpty(user.estadoDeCuenta))
                    {
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
                            cmd.Parameters.AddWithValue("@Foto", user.foto);
                            cmd.Parameters.AddWithValue("@Configuraciones", user.configuraciones);
                            cmd.Parameters.AddWithValue("@Genero", user.genero);
                            cmd.Parameters.AddWithValue("@FechaDeNacimiento", user.fechaDeNacimiento);
                            cmd.Parameters.AddWithValue("@EstadoDeCuenta", user.estadoDeCuenta);
                            cmd.ExecuteNonQuery();
                        }
                        conn.Close();
                        return JsonConvert.SerializeObject(new { mensaje = "Guardado correcto" });
                    }
                    else
                    {
                        conn.Close();
                        return JsonConvert.SerializeObject(new { mensaje = "Guardado incorrecto: faltan datos" });
                    }
                }
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { mensaje = "Guardado incorrecto: error en el servidor", error = ex.Message });
            }

        }
        public dynamic PRReportarUsuario([FromBody] Reporte user)
        {
            if (user == null)
            {
                return JsonConvert.SerializeObject("nulo");
            }
            else
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("INSERT INTO ReporteUsuario (nombreDeUsuario,tipo,descripcion) VALUES (@nombre, @tipo, @descripcion)", conn);
                cmd.Parameters.AddWithValue("@nombre", user.usuario);
                cmd.Parameters.AddWithValue("@tipo", user.tipo);
                cmd.Parameters.AddWithValue("@descripcion", user.descripcion);
                cmd.ExecuteNonQuery();
                conn.Close();
                return JsonConvert.SerializeObject("Reporte correcto");
            }
        }

        public dynamic PRCambiarConfiguracion(string nombreDeCuenta, string configuraciones)
        {
            if (string.IsNullOrEmpty(nombreDeCuenta) && string.IsNullOrEmpty(configuraciones))
            {
                return JsonConvert.SerializeObject("Datos invalidos");
            }
            else
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("UPDATE Usuarios SET configuraciones=@configuraciones WHERE nombreDeCuenta=@nombreDeCuenta", conn);
                cmd.Parameters.AddWithValue("@nombreDeCuenta", nombreDeCuenta);
                cmd.Parameters.AddWithValue("@configuraciones", configuraciones);
                cmd.ExecuteNonQuery();
                conn.Close();
                return JsonConvert.SerializeObject("Configuracion correcta");
            }
        }
        public dynamic PRConseguirConfiguracion(string nombreDeCuenta)
        {
            if (string.IsNullOrEmpty(nombreDeCuenta))
            {
                return JsonConvert.SerializeObject("nulo");
            }
            else
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT configuraciones FROM Usuarios WHERE nombreDeCuenta=@nombreDeCuenta", conn);
                cmd.Parameters.AddWithValue("@nombreDeCuenta", nombreDeCuenta);
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    string configuraciones = reader["configuraciones"].ToString();
                    conn.Close(); 
                    return JsonConvert.SerializeObject(configuraciones);
                }
                else
                {
                    conn.Close();
                    return JsonConvert.SerializeObject("Hubo un error" + nombreDeCuenta);
                }
            }
        }
        public dynamic PRActualizarNotificaciones(string nombreDeCuenta, string notificaciones)
        {
            if (string.IsNullOrEmpty(nombreDeCuenta) && string.IsNullOrEmpty(notificaciones))
            {
                return JsonConvert.SerializeObject("nulo");
            }
            else
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("UPDATE Usuarios SET notificaciones=@notificaciones WHERE nombreDeCuenta=@nombreDeCuenta", conn);
                cmd.Parameters.AddWithValue("@nombreDeCuenta", nombreDeCuenta);
                cmd.Parameters.AddWithValue("@notificaciones", notificaciones);
                cmd.ExecuteNonQuery();
                conn.Close();
                return JsonConvert.SerializeObject("Correcto");
            }
        }
        public dynamic PRConseguirNotificaciones(string nombreDeCuenta)
        {
            if (string.IsNullOrEmpty(nombreDeCuenta))
            {
                return JsonConvert.SerializeObject("nulo");
            }
            else
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT notificaciones FROM Usuarios WHERE nombreDeCuenta=@nombreDeCuenta", conn);
                cmd.Parameters.AddWithValue("@nombreDeCuenta", nombreDeCuenta);
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    string notificaciones = reader["notificaciones"].ToString();
                    conn.Close();
                    return JsonConvert.SerializeObject(notificaciones);
                }
                else
                {
                    conn.Close();
                    return JsonConvert.SerializeObject("Hubo un error" + nombreDeCuenta);
                }
            }
        }
    }
}
