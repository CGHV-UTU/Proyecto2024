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
            public string imagen { get; set; }
            public string configuraciones { get; set; }
            public string genero { get; set; }
            public string fechaDeNacimiento { get; set; }
            public string estadoDeCuenta { get; set; }
            public string contraseña { get; set; }
        }

        [System.Web.Mvc.HttpPost]
        [System.Web.Mvc.Route("RegistrarUsuario")]
        public dynamic RegistrarUsuario([FromBody] usuario user )
        {
            try
            {
                MySqlConnection conn = new MySqlConnection("Server=localhost; database=base; uID=root; pwd=;");
                conn.Open();
                MySqlCommand cmd;
                if (!string.IsNullOrEmpty(user.nombreDeCuenta)
                    && !string.IsNullOrEmpty(user.nombreVisible)
                    && !string.IsNullOrEmpty(user.email)
                    && !string.IsNullOrEmpty(user.imagen)
                    && !string.IsNullOrEmpty(user.configuraciones)
                    && !string.IsNullOrEmpty(user.genero)
                    && !string.IsNullOrEmpty(user.fechaDeNacimiento)
                    && !string.IsNullOrEmpty(user.estadoDeCuenta)
                    && !string.IsNullOrEmpty(user.contraseña))
                {
                    //Inserción en la tabla USUARIOS
                    byte[] foto = Convert.FromBase64String(user.imagen);
                    cmd = new MySqlCommand("INSERT INTO usuarios (NombreDeCuenta,NombreVisible,email,Foto,configuraciones,genero,fecha_de_nacimiento,estado_de_cuenta) VALUES (@nombredecuenta,@nombrevisible,@email,@foto,@configuraciones,@genero,@fecha_de_nacimiento,@estado_de_cuenta)", conn);
                    cmd.Parameters.AddWithValue("@nombredecuenta", user.nombreDeCuenta);
                    cmd.Parameters.AddWithValue("@nombrevisible", user.nombreVisible);
                    cmd.Parameters.AddWithValue("@email", user.email);
                    cmd.Parameters.AddWithValue("@foto", foto);
                    cmd.Parameters.AddWithValue("@configuraciones", user.configuraciones);
                    cmd.Parameters.AddWithValue("@genero", user.genero);
                    cmd.Parameters.AddWithValue("@fecha_de_nacimiento", user.fechaDeNacimiento);
                    cmd.Parameters.AddWithValue("@estado_de_cuenta", user.estadoDeCuenta);

                    //Inserción en la tabla LOGIN
                    MySqlCommand cmd2 = new MySqlCommand("INSERT INTO login (NombreDeCuenta, Contraseña)" +
                    " VALUES (@nombredecuenta, @contraseña)", conn);
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
                        cmd = new MySqlCommand("INSERT INTO usuarios (NombreDeCuenta,NombreVisible,email,Foto,configuraciones,genero,fecha_de_nacimiento,estado_de_cuenta,Descripcion) VALUES (@nombredecuenta,@nombrevisible,@email,@foto,@configuraciones,@genero,@fecha_de_nacimiento,@estado_de_cuenta,@descripcion)", conn);
                        cmd.Parameters.AddWithValue("@descripcion", user.descripcion);
                        cmd.Parameters.AddWithValue("@nombredecuenta", user.nombreDeCuenta);
                        cmd.Parameters.AddWithValue("@nombrevisible", user.nombreVisible);
                        cmd.Parameters.AddWithValue("@email", user.email);
                        cmd.Parameters.AddWithValue("@foto", foto);
                        cmd.Parameters.AddWithValue("@configuraciones", user.configuraciones);
                        cmd.Parameters.AddWithValue("@genero", user.genero);
                        cmd.Parameters.AddWithValue("@fecha_de_nacimiento", user.fechaDeNacimiento);
                        cmd.Parameters.AddWithValue("@estado_de_cuenta", user.estadoDeCuenta);
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

        [System.Web.Mvc.HttpGet]
        [System.Web.Mvc.Route("obtenerUsuario")]
        public dynamic ObtenerUsuario(string nombredecuenta)
        {
            try
            {
                MySqlConnection conn = new MySqlConnection("Server=localhost; database=base; uID=root; pwd=;");
                conn.Open();
                MySqlCommand command = new MySqlCommand("SELECT NombreVisible,email,Descripcion,Foto,configuraciones,genero,fecha_de_nacimiento,estado_de_cuenta FROM usuarios WHERE NombreDeCuenta=@NombreDeCuenta", conn);
                command.Parameters.AddWithValue("@NombreDeCuenta", nombredecuenta);
                MySqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    string nombrevisible;
                    nombrevisible = reader["NombreVisible"].ToString();
                    string email;
                    email = reader["email"].ToString();
                    string descripcion;
                    if (string.IsNullOrEmpty(reader["Descripcion"].ToString()))
                    {
                       descripcion = "";
                    }
                    else
                    {
                        descripcion = reader["Descripcion"].ToString();
                    }
                    string foto;
                    foto = Convert.ToBase64String((byte[])reader["Foto"]);
                    string configuraciones;
                    configuraciones = reader["configuraciones"].ToString();
                    string genero;
                    genero = reader["genero"].ToString();
                    string fecha_de_nacimiento;
                    fecha_de_nacimiento = reader["fecha_de_nacimiento"].ToString();
                    string estado_de_cuenta;
                    estado_de_cuenta = reader["estado_de_cuenta"].ToString();
                    var data = new { NombreVisible = nombrevisible, email = email, Descripcion = descripcion, Foto = foto, configuraciones = configuraciones, genero = genero, fecha_de_nacimiento = fecha_de_nacimiento, estado_de_cuenta = estado_de_cuenta };
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
                using (MySqlConnection conn = new MySqlConnection("Server=localhost; database=base; uID=root; pwd=;"))
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("SELECT 1 FROM usuarios WHERE NombreDeCuenta=@nombreDeCuenta", conn);
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
                using (MySqlConnection conn = new MySqlConnection("Server=localhost; database=base; uID=root; pwd=;"))
                {
                    conn.Open();
                    if (!string.IsNullOrEmpty(user.nombreDeCuenta)
                        && !string.IsNullOrEmpty(user.nombreVisible)
                        && !string.IsNullOrEmpty(user.email)
                        && user.imagen != null
                        && !string.IsNullOrEmpty(user.configuraciones)
                        && !string.IsNullOrEmpty(user.genero)
                        && !string.IsNullOrEmpty(user.fechaDeNacimiento)
                        && !string.IsNullOrEmpty(user.estadoDeCuenta))
                    {
                        string query = @"UPDATE usuarios 
                                 SET NombreVisible=@nombreVisible, 
                                     email=@Email, 
                                     Descripcion=@Descripcion, 
                                     Foto=@Foto, 
                                     configuraciones=@Configuraciones, 
                                     genero=@Genero, 
                                     fecha_de_nacimiento=@FechaDeNacimiento, 
                                     estado_de_cuenta=@EstadoDeCuenta 
                                 WHERE NombreDeCuenta=@NombreDeCuenta";

                        using (MySqlCommand cmd = new MySqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@NombreDeCuenta", user.nombreDeCuenta);
                            cmd.Parameters.AddWithValue("@NombreVisible", user.nombreVisible);
                            cmd.Parameters.AddWithValue("@Email", user.email);
                            cmd.Parameters.AddWithValue("@Descripcion", user.descripcion ?? string.Empty);
                            cmd.Parameters.AddWithValue("@Foto", user.imagen);
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
                MySqlConnection conn = new MySqlConnection("Server=localhost; database=base; uID=root; pwd=;");
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("INSERT INTO Reporte (NombreDeUsuario,tipo,descripcion) VALUES (@Nombre, @tipo, @descripcion)", conn);
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
                MySqlConnection conn = new MySqlConnection("Server=localhost; database=base; uID=root; pwd=;");
                conn.Open();
                MySqlCommand cmd;
                if (!string.IsNullOrEmpty(user.nombreDeCuenta)
                    && !string.IsNullOrEmpty(user.nombreVisible)
                    && !string.IsNullOrEmpty(user.email)
                    && !string.IsNullOrEmpty(user.imagen)
                    && !string.IsNullOrEmpty(user.configuraciones)
                    && !string.IsNullOrEmpty(user.genero)
                    && !string.IsNullOrEmpty(user.fechaDeNacimiento)
                    && !string.IsNullOrEmpty(user.estadoDeCuenta)
                    && !string.IsNullOrEmpty(user.contraseña))
                {
                    //Inserción en la tabla USUARIOS
                    byte[] foto = Convert.FromBase64String(user.imagen);
                    cmd = new MySqlCommand("INSERT INTO usuarios (NombreDeCuenta,NombreVisible,email,Foto,configuraciones,genero,fecha_de_nacimiento,estado_de_cuenta) VALUES (@nombredecuenta,@nombrevisible,@email,@foto,@configuraciones,@genero,@fecha_de_nacimiento,@estado_de_cuenta)", conn);
                    cmd.Parameters.AddWithValue("@nombredecuenta", user.nombreDeCuenta);
                    cmd.Parameters.AddWithValue("@nombrevisible", user.nombreVisible);
                    cmd.Parameters.AddWithValue("@email", user.email);
                    cmd.Parameters.AddWithValue("@foto", foto);
                    cmd.Parameters.AddWithValue("@configuraciones", user.configuraciones);
                    cmd.Parameters.AddWithValue("@genero", user.genero);
                    cmd.Parameters.AddWithValue("@fecha_de_nacimiento", user.fechaDeNacimiento);
                    cmd.Parameters.AddWithValue("@estado_de_cuenta", user.estadoDeCuenta);

                    //Inserción en la tabla LOGIN
                    MySqlCommand cmd2 = new MySqlCommand("INSERT INTO login (NombreDeCuenta, Contraseña)" +
                    " VALUES (@nombredecuenta, @contraseña)", conn);
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
                        cmd = new MySqlCommand("INSERT INTO usuarios (NombreDeCuenta,NombreVisible,email,Foto,configuraciones,genero,fecha_de_nacimiento,estado_de_cuenta,Descripcion) VALUES (@nombredecuenta,@nombrevisible,@email,@foto,@configuraciones,@genero,@fecha_de_nacimiento,@estado_de_cuenta,@descripcion)", conn);
                        cmd.Parameters.AddWithValue("@descripcion", user.descripcion);
                        cmd.Parameters.AddWithValue("@nombredecuenta", user.nombreDeCuenta);
                        cmd.Parameters.AddWithValue("@nombrevisible", user.nombreVisible);
                        cmd.Parameters.AddWithValue("@email", user.email);
                        cmd.Parameters.AddWithValue("@foto", foto);
                        cmd.Parameters.AddWithValue("@configuraciones", user.configuraciones);
                        cmd.Parameters.AddWithValue("@genero", user.genero);
                        cmd.Parameters.AddWithValue("@fecha_de_nacimiento", user.fechaDeNacimiento);
                        cmd.Parameters.AddWithValue("@estado_de_cuenta", user.estadoDeCuenta);
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
                MySqlConnection conn = new MySqlConnection("Server=localhost; database=base; uID=root; pwd=;");
                conn.Open();
                MySqlCommand command = new MySqlCommand("SELECT NombreVisible,email,Descripcion,Foto,configuraciones,genero,fecha_de_nacimiento,estado_de_cuenta FROM usuarios WHERE NombreDeCuenta=@NombreDeCuenta", conn);
                command.Parameters.AddWithValue("@NombreDeCuenta", nombredecuenta);
                MySqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    string nombrevisible;
                    nombrevisible = reader["NombreVisible"].ToString();
                    string email;
                    email = reader["email"].ToString();
                    string descripcion;
                    if (string.IsNullOrEmpty(reader["Descripcion"].ToString()))
                    {
                        descripcion = "";
                    }
                    else
                    {
                        descripcion = reader["Descripcion"].ToString();
                    }
                    string foto;
                    foto = Convert.ToBase64String((byte[])reader["Foto"]);
                    string configuraciones;
                    configuraciones = reader["configuraciones"].ToString();
                    string genero;
                    genero = reader["genero"].ToString();
                    string fecha_de_nacimiento;
                    fecha_de_nacimiento = reader["fecha_de_nacimiento"].ToString();
                    string estado_de_cuenta;
                    estado_de_cuenta = reader["estado_de_cuenta"].ToString();
                    var data = new { NombreVisible = nombrevisible, email = email, Descripcion = descripcion, Foto = foto, configuraciones = configuraciones, genero = genero, fecha_de_nacimiento = fecha_de_nacimiento, estado_de_cuenta = estado_de_cuenta };
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
                MySqlConnection conn = new MySqlConnection("Server=localhost; database=base; uID=root; pwd=;");
                conn.Open();
                MySqlCommand command = new MySqlCommand("SELECT NombreDeCuenta FROM usuarios WHERE NombreDeCuenta=@nombredecuenta", conn);
                command.Parameters.AddWithValue("@nombredecuenta", nombredecuenta);
                MySqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    var data = true;
                    if (reader["NombreDeCuenta"].ToString().Equals(Convert.ToString(nombredecuenta)))
                    {
                        data = true;
                        return JsonConvert.SerializeObject(data);
                    }
                    else
                    {
                        data = false;
                        return JsonConvert.SerializeObject(data);
                    }
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }
        }

    }
}
