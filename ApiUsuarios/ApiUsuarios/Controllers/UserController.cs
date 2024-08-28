using System;
using System.Collections.Generic;
using System.Linq;
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

        /*  [System.Web.Mvc.HttpPost]
          [System.Web.Mvc.Route("RegistrarUsuario")]
          public dynamic RegistrarUsuario([FromBody] usuario user )
          {
              try
              {
                  MySqlConnection conn = new MySqlConnection("Server=localhost; database=infini; uID=root; pwd=;");
                  conn.Open();
                  MySqlCommand cmd;
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
                      //Inserción en la tabla USUARIOS
                      cmd = new MySqlCommand("INSERT INTO Usuarios (nombreDeCuenta,nombreVisible,email,foto,configuraciones,genero,fechaDeNacimiento,estadoDeCuenta) VALUES (@nombredecuenta,@nombrevisible,@email,@foto,@configuraciones,@genero,@fechaDeNacimiento,@estadoDeCuenta)", conn);
                      cmd.Parameters.AddWithValue("@nombredecuenta", user.nombreDeCuenta);
                      cmd.Parameters.AddWithValue("@nombrevisible", user.nombreVisible);
                      cmd.Parameters.AddWithValue("@email", user.email);
                      cmd.Parameters.AddWithValue("@foto", user.foto);
                      cmd.Parameters.AddWithValue("@configuraciones", user.configuraciones);
                      cmd.Parameters.AddWithValue("@genero", user.genero);
                      cmd.Parameters.AddWithValue("@fechaDeNacimiento", user.fechaDeNacimiento);
                      cmd.Parameters.AddWithValue("@estadoDeCuenta", user.estadoDeCuenta);

                      //Inserción en la tabla LOGIN
                      MySqlCommand cmd2 = new MySqlCommand("INSERT INTO Login (nombreDeCuenta, contrasena) VALUES (@nombredecuenta, @contraseña)", conn);
                      cmd2.Parameters.AddWithValue("@nombredecuenta", user.nombreDeCuenta);
                      cmd2.Parameters.AddWithValue("@contraseña", user.contraseña);
                      cmd2.ExecuteNonQuery();
                      if (string.IsNullOrEmpty(user.descripcion))
                      {
                          cmd.ExecuteNonQuery();
                          conn.Close();
                          return Json("guardado correcto");
                      }
                      else
                      {
                          cmd = new MySqlCommand("INSERT INTO Usuarios (nombreDeCuenta,nombreVisible,email,descripcion,foto,configuraciones,genero,fechaDeNacimiento,estadoDeCuenta) VALUES (@nombredecuenta,@nombrevisible,@email,@descripcion,@foto,@configuraciones,@genero,@fechaDeNacimiento,@estadoDeCuenta)", conn);
                          cmd.Parameters.AddWithValue("@descripcion", user.descripcion);
                          cmd.Parameters.AddWithValue("@nombredecuenta", user.nombreDeCuenta);
                          cmd.Parameters.AddWithValue("@nombrevisible", user.nombreVisible);
                          cmd.Parameters.AddWithValue("@email", user.email);
                          cmd.Parameters.AddWithValue("@foto", user.foto);
                          cmd.Parameters.AddWithValue("@configuraciones", user.configuraciones);
                          cmd.Parameters.AddWithValue("@genero", user.genero);
                          cmd.Parameters.AddWithValue("@fechaDeNacimiento", user.fechaDeNacimiento);
                          cmd.Parameters.AddWithValue("@estadoDeCuenta", user.estadoDeCuenta);
                          cmd.ExecuteNonQuery();
                          conn.Close();
                          return Json("guardado correcto");
                      }
                  }
                  else
                  {
                      return Json("guardado incorrecto. Strings nulos" +
                          " Nombre de cuenta: " + user.nombreDeCuenta +
                          " NombreVisible: " + user.nombreVisible +
                          " contraseña: " + user.contraseña +
                          " email: " + user.email +
                          " genero: " + user.genero +
                          " descr: " + user.descripcion +
                          " fecha: " + user.fechaDeNacimiento +
                          " conf: " + user.configuraciones +
                          " foto: " + user.foto

                          );

                  }
              }
              catch (Exception ex)
              {
                  return Json("guardado incorrecto: " + ex.Message);
              }   
          }*/


        [System.Web.Mvc.HttpPost]
        [System.Web.Mvc.Route("RegistrarUsuario")]
        public JsonResult RegistrarUsuario([System.Web.Http.FromBody] usuario user)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection("Server=localhost; database=infini; uID=root; pwd=;"))
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
                            return Json("guardado incorrecto: formato de fecha inválido");
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
                        return Json("guardado correcto");
                    }
                    else
                    {
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
        public dynamic ObtenerUsuario(string nombredecuenta)
        {
            try
            {
                MySqlConnection conn = new MySqlConnection("Server=localhost; database=infini; uID=root; pwd=;");
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
                    return Json(data);
                }
                else
                {
                    return Json("no se encuentra");
                }
            }
            catch (Exception ex)
            {
                return Json("no se encuentra");
            }
        }

        [System.Web.Mvc.HttpGet]
        [System.Web.Mvc.Route ("ExisteUsuario")]
        public dynamic ExisteUsuario( string nombreDeCuenta)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection("Server=localhost; database=infini; uID=root; pwd=;"))
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
        public dynamic ModificarUsuario([FromBody] usuario user)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection("Server=localhost; database=infini; uID=root; pwd=;"))
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
                        return Json(new { mensaje = "Guardado correcto" });
                    }
                    else
                    {
                        return Json(new { mensaje = "Guardado incorrecto: faltan datos" });
                    }
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
                MySqlConnection conn = new MySqlConnection("Server=localhost; database=infini; uID=root; pwd=;");
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
                MySqlConnection conn = new MySqlConnection("Server=localhost; database=infini; uID=root; pwd=;");
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT configuraciones FROM Usuarios WHERE nombreDeCuenta=@nombreDeCuenta", conn);
                cmd.Parameters.AddWithValue("@nombreDeCuenta", user.nombreDeCuenta);
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    return Json(reader["configuraciones"].ToString());
                }
                else
                {
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
                MySqlConnection conn = new MySqlConnection("Server=localhost; database=infini; uID=root; pwd=;");
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
        [System.Web.Mvc.Route("ConseguirNotificaciones")]
        public dynamic ConseguirNotificaciones([FromBody] usuario user)
        {
            if (user == null)
            {
                return Json("nulo");
            }
            else
            {
                MySqlConnection conn = new MySqlConnection("Server=localhost; database=infini; uID=root; pwd=;");
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT notificaciones FROM Usuarios WHERE nombreDeCuenta=@nombreDeCuenta", conn);
                cmd.Parameters.AddWithValue("@nombreDeCuenta", user.nombreDeCuenta);
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    return Json(reader["notificaciones"].ToString());
                }
                else
                {
                    return Json("Hubo un error" + user.nombreDeCuenta);
                }
            }
        }

        public class Reporte
        {
            public string usuario { get; set; }
            public string tipo { get; set; }
            public string descripcion { get; set; }
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
                MySqlConnection conn = new MySqlConnection("Server=localhost; database=infini; uID=root; pwd=;");
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
                using (MySqlConnection conn = new MySqlConnection("Server=localhost; database=infini; uID=root; pwd=;"))
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
                MySqlConnection conn = new MySqlConnection("Server=localhost; database=infini; uID=root; pwd=;");
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
                    return JsonConvert.SerializeObject(data);
                }
                else
                {
                    return JsonConvert.SerializeObject("no se encuentra");
                }
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject("no se encuentra");
            }
        }


        public dynamic PRexisteUsuario(string nombredecuenta)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection("Server=localhost; database=infini; uID=root; pwd=;"))
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
                using (MySqlConnection conn = new MySqlConnection("Server=localhost; database=infini; uID=root; pwd=;"))
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
                        return JsonConvert.SerializeObject(new { mensaje = "Guardado correcto" });
                    }
                    else
                    {
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
                MySqlConnection conn = new MySqlConnection("Server=localhost; database=infini; uID=root; pwd=;");
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

    }
}
