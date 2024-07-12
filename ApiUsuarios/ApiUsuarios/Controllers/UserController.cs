using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MySql.Data.MySqlClient;
namespace ApiUsuarios.Controllers
{
    public class UserController : Controller
    {
        [HttpPost]
        [Route("RegistrarUsuario")]
        public dynamic CrearUsuario(string nombredecuenta, string nombrevisible, string email, string descripcion, string imagen, string configuraciones, string genero, string fecha_de_nacimiento, string estado_de_cuenta)
        {
            try
            {
                MySqlConnection conn = new MySqlConnection("Server=localhost; database=base; uID=root; pwd=;");
                conn.Open();
                MySqlCommand cmd;
                if (!string.IsNullOrEmpty(nombredecuenta) && !string.IsNullOrEmpty(nombrevisible) && !string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(imagen) && !string.IsNullOrEmpty(configuraciones) && !string.IsNullOrEmpty(genero) && !string.IsNullOrEmpty(fecha_de_nacimiento) && !string.IsNullOrEmpty(estado_de_cuenta))
                {
                    byte[] foto = Convert.FromBase64String(imagen);
                    cmd = new MySqlCommand("INSERT INTO usuarios (NombreDeCuenta,NombreVisible,email,Foto,configuraciones,genero,fecha_de_nacimiento,estado_de_cuenta) VALUES (@nombredecuenta,@nombrevisible,@email,@foto,@configuraciones,@genero,@fecha_de_nacimiento,@estado_de_cuenta)", conn);
                    cmd.Parameters.AddWithValue("@nombredecuenta", nombredecuenta);
                    cmd.Parameters.AddWithValue("@nombrevisible", nombrevisible);
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.Parameters.AddWithValue("@foto", foto);
                    cmd.Parameters.AddWithValue("@configuraciones", configuraciones);
                    cmd.Parameters.AddWithValue("@genero", genero);
                    cmd.Parameters.AddWithValue("@fecha_de_nacimiento", fecha_de_nacimiento);
                    cmd.Parameters.AddWithValue("@estado_de_cuenta", estado_de_cuenta);
                    if (string.IsNullOrEmpty(descripcion))
                    {
                        cmd.ExecuteNonQuery();
                        conn.Close();
                        return Json("guardado correcto");
                    }
                    else
                    {
                        cmd = new MySqlCommand("INSERT INTO usuarios (Descripcion) VALUES (@descripcion)", conn);
                        cmd.Parameters.AddWithValue("@descripcion", descripcion);
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

        [HttpGet]
        [Route("obtenerUsuario")]
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

        [HttpGet]
        [Route ("existeUsuario")]
        public dynamic existeUsuario(string nombredecuenta)
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
                        return Json(data);
                    }
                    else
                    {
                        data = false;
                        return Json(data);
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
