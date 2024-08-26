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

        [System.Web.Mvc.HttpPost]
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
                    return Json("guardado incorrecto");
                }
            }
            catch
            {
                return Json("guardado incorrecto");
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
                    string nombrevisible;
                    nombrevisible = reader["nombreVisible"].ToString();
                    string email;
                    email = reader["email"].ToString();
                    string descripcion;
                    if (string.IsNullOrEmpty(reader["descripcion"].ToString()))
                    {
                       descripcion = "";
                    }
                    else
                    {
                        descripcion = reader["descripcion"].ToString();
                    }
                    string foto;
                    if (string.IsNullOrEmpty(reader["foto"].ToString()))
                    {
                        foto = "";
                    }
                    else
                    {
                        foto = reader["foto"].ToString();
                    }
                    string configuraciones;
                    configuraciones = reader["configuraciones"].ToString();
                    string genero;
                    genero = reader["genero"].ToString();
                    string fecha_de_nacimiento;
                    fecha_de_nacimiento = reader["fechaDeNacimiento"].ToString();
                    string estado_de_cuenta;
                    estado_de_cuenta = reader["estadoDeCuenta"].ToString();
                    var data = new { nombreVisible = nombrevisible, email = email, descripcion = descripcion, foto = foto, configuraciones = configuraciones, genero = genero, fechaDeNacimiento = fecha_de_nacimiento, estadoDeCuenta = estado_de_cuenta };
                    return Json(data);
                }
                else
                {
                    return Json("no se encuentra");
                }
            }
            catch
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
                        return JsonConvert.SerializeObject("guardado correcto");
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
                        return JsonConvert.SerializeObject("guardado correcto");
                    }
                }
                else
                {
                    return JsonConvert.SerializeObject("guardado incorrecto");
                }
            }
            catch
            {
                return JsonConvert.SerializeObject("guardado incorrecto");
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
                    string nombrevisible;
                    nombrevisible = reader["nombreVisible"].ToString();
                    string email;
                    email = reader["email"].ToString();
                    string descripcion;
                    if (string.IsNullOrEmpty(reader["descripcion"].ToString()))
                    {
                        descripcion = "";
                    }
                    else
                    {
                        descripcion = reader["descripcion"].ToString();
                    }
                    string foto;
                    if (string.IsNullOrEmpty(reader["foto"].ToString()))
                    {
                        foto = "";
                    }
                    else
                    {
                        foto = reader["foto"].ToString();
                    }
                    string configuraciones;
                    configuraciones = reader["configuraciones"].ToString();
                    string genero;
                    genero = reader["genero"].ToString();
                    string fecha_de_nacimiento;
                    fecha_de_nacimiento = reader["fechaDeNacimiento"].ToString();
                    string estado_de_cuenta;
                    estado_de_cuenta = reader["estadoDeCuenta"].ToString();
                    var data = new { nombreVisible = nombrevisible, email = email, descripcion = descripcion, foto = foto, configuraciones = configuraciones, genero = genero, fechaDeNacimiento = fecha_de_nacimiento, estadoDeCuenta = estado_de_cuenta };
                    return JsonConvert.SerializeObject(data);
                }
                else
                {
                    return JsonConvert.SerializeObject("no se encuentra");
                }
            }
            catch
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

    }
}
