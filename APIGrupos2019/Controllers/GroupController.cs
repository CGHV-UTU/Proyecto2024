using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using Google.Protobuf;
//using Microsoft.AspNetCore.Http;
using System.Web.Http;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using Org.BouncyCastle.Security;
//using static System.Runtime.InteropServices.JavaScript.JSType;
using static API_Grupos.Controllers.GgroupController;
//Sí, es la última versión
namespace API_Grupos.Controllers
{
    [System.Web.Http.Route("group")]
    [System.Web.Http.Route("group")]
    public class GgroupController : Controller
    {
        public class Grupo
        {
            public string nombreReal { get; set; }
            public string nombreVisible { get; set; }
            public string configuracion { get; set; }
            public string descripcion { get; set; }
            public string imagen { get; set; }
        }

        public class UsuarioGrupo
        {
            public string nombreReal { get; set; }
            public string nombreDeCuenta { get; set; }
        }


        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("RegistrarGrupo")] //Da error pero inserta los datos
        public dynamic RegistrarGrupo([FromBody] Grupo group)
        {
            try
            {
                group.nombreReal = crearNombreGrupo();
                if (string.IsNullOrEmpty(group.nombreVisible)
                    || string.IsNullOrEmpty(group.configuracion)
                    || string.IsNullOrEmpty(group.imagen))
                {
                    return Json("Valores nulos");
                }

                MySqlConnection conn = new MySqlConnection("Server=localhost; database=base; uID=root; pwd=;");
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("insert into grupos (nombreReal, nombreVisible, configuracion, foto) values (@nombreReal, @nombreVisible, @configuracion, @foto)", conn);
                cmd.Parameters.AddWithValue("@nombreReal", group.nombreReal);
                cmd.Parameters.AddWithValue("@nombreVisible", group.nombreVisible);
                cmd.Parameters.AddWithValue("@configuracion", group.configuracion);
                cmd.Parameters.AddWithValue("@foto", group.imagen);
                cmd.ExecuteNonQuery();
                if (!string.IsNullOrEmpty(group.descripcion))
                {
                    MySqlCommand cmd2 = new MySqlCommand("update grupos set descripcion = @descripcion where nombreReal = @nombreReal", conn);
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
        [System.Web.Http.Route("RegistrarGrupoUG")] //Funciona
        public dynamic RegistrarGrupoUG([FromBody] UsuarioGrupo ug)
        {
            try
            {
                if (string.IsNullOrEmpty(ug.nombreReal) || string.IsNullOrEmpty(ug.nombreDeCuenta))
                {
                    return Json("Valores nulos");
                }

                MySqlConnection conn = new MySqlConnection("Server=localhost; database=base; uid=root; pwd=;");
                conn.Open();
                string insertQuery = "INSERT INTO usuarios_grupos (nombreReal, NombreDeCuenta, rol) VALUES (@nombreReal, @nombreDeCuenta, 'c')";
                MySqlCommand cmd = new MySqlCommand(insertQuery, conn);
                cmd.Parameters.AddWithValue("@nombreReal", ug.nombreReal);
                cmd.Parameters.AddWithValue("@nombreDeCuenta", ug.nombreDeCuenta);
                cmd.ExecuteNonQuery();
                conn.Close();
                return Json("Registro correcto");
            }
            catch (Exception ex)
            {
                return Json($"Registro fallido: {ex.Message}");
            }
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("ObtenerGruposPorNombreVisibleYUsuario")] //Funciona
        public dynamic ObtenerGruposPorNombreVisibleYUsuario(string nombreVisible, string nombreDeCuenta)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection("Server=localhost; database=base; uid=root; pwd=;"))
                {
                    conn.Open();

                    string query = @"
                SELECT g.nombreReal, g.nombreVisible, g.configuracion, g.descripcion, g.foto
                FROM grupos g
                JOIN usuarios_grupos ug ON g.nombreReal = ug.nombreReal
                WHERE g.nombreVisible = @nombreVisible AND ug.NombreDeCuenta = @nombreDeCuenta";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@nombreVisible", nombreVisible);
                        cmd.Parameters.AddWithValue("@nombreDeCuenta", nombreDeCuenta);

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            List<dynamic> grupos = new List<dynamic>();

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

                                grupos.Add(grupo);
                            }

                            conn.Close();

                            if (grupos.Count > 0)
                            {
                                return Json(grupos);
                            }
                            else
                            {
                                return Json("No se encontraron grupos para el usuario especificado");
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                return Json($"Error al obtener los grupos: {e.Message}");
            }
        }



        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("ObtenerGrupo")] //Funciona
        public dynamic ObtenerGrupo(string nombre)
        {
            try
            {
                MySqlConnection conn = new MySqlConnection("Server=localhost; database=base; uid=root; pwd=;");
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("select * from grupos where nombreReal=@nombreReal", conn);
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

        [System.Web.Http.HttpDelete]
        [System.Web.Http.Route("EliminarGrupo")]//Salta FK
        public dynamic EliminarGrupo(string nombreReal)
        {
            try
            {
                MySqlConnection conn = new MySqlConnection("Server=localhost; database=base; uid=root; pwd=;");
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("delete from grupos where nombreReal = @nombreReal", conn);
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
        public dynamic EditarGrupo([FromBody] Grupo group)
        {
            if (string.IsNullOrEmpty(group.nombreReal)
                    || string.IsNullOrEmpty(group.nombreVisible)
                    || string.IsNullOrEmpty(group.configuracion)
                    || string.IsNullOrEmpty(group.imagen))
            {
                return Json("Datos inválidos");
            }
            try
            {
                MySqlConnection conn = new MySqlConnection("Server=localhost; database=base; uid=root; pwd=;");
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("update grupos " +
                    "set nombreVisible = @nombreVisible," +
                    "configuracion = @configuracion," +
                    "descripcion = @descripcion," +
                    "foto = @foto " +
                    "where nombreReal = @nombreReal", conn);
                cmd.Parameters.AddWithValue("@nombreReal", group.nombreReal);
                cmd.Parameters.AddWithValue("@nombreVisible", group.nombreVisible);
                cmd.Parameters.AddWithValue("@configuracion", group.configuracion);
                cmd.Parameters.AddWithValue("@descripcion", group.descripcion);
                cmd.Parameters.AddWithValue("@foto", group.imagen);
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
                MySqlConnection conn = new MySqlConnection("Server=localhost; database=base; uid=root; pwd=;");
                conn.Open();

                // Verificar si el usuario tiene permisos
                string permisosQuery = "SELECT rol FROM usuarios_grupos WHERE nombreReal = @nombreReal AND NombreDeCuenta = @NombreDeCuenta AND (rol = 'c' OR rol = 'a')";
                MySqlCommand permisosCmd = new MySqlCommand(permisosQuery, conn);
                permisosCmd.Parameters.AddWithValue("@nombreReal", group.nombreReal);
                permisosCmd.Parameters.AddWithValue("@NombreDeCuenta", usuario);

                MySqlDataReader reader = permisosCmd.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Close(); // Cerrar el reader antes de ejecutar otro comando

                    // Actualizar el grupo
                    string updateQuery = "UPDATE grupos " +
                        "SET nombreVisible = @nombreVisible, " +
                        "configuracion = @configuracion, " +
                        "descripcion = @descripcion, " +
                        "foto = @foto " +
                        "WHERE nombreReal = @nombreReal";
                    MySqlCommand cmd = new MySqlCommand(updateQuery, conn);
                    cmd.Parameters.AddWithValue("@nombreReal", group.nombreReal);
                    cmd.Parameters.AddWithValue("@nombreVisible", group.nombreVisible);
                    cmd.Parameters.AddWithValue("@configuracion", group.configuracion);
                    cmd.Parameters.AddWithValue("@descripcion", group.descripcion);
                    cmd.Parameters.AddWithValue("@foto", group.imagen);
                    cmd.ExecuteNonQuery();

                    conn.Close();
                    return Json("Se editó el grupo correctamente");
                }
                else
                {
                    conn.Close();
                    return Json("No tienes permisos para editar este grupo");
                }
            }
            catch (Exception ex)
            {
                return Json($"No se pudo editar el grupo: {ex.Message}");
            }
        }



        //Grupos al que pertenece un usuario
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("ObtenerGruposPorUsuario")]
        public dynamic ObtenerGruposPorUsuario(string NombreDeCuenta)
        {
            try
            {
                List<Grupo> grupos = new List<Grupo>();
                MySqlConnection conn = new MySqlConnection("Server=localhost; database=base; uid=root; pwd=;");
                conn.Open();

                MySqlCommand cmd = new MySqlCommand(@"
                    SELECT g.nombreReal, g.nombreVisible, g.configuracion, g.descripcion, g.foto
                    FROM grupos g
                    JOIN usuarios_grupos ug ON g.nombreReal = ug.nombreReal
                    WHERE ug.NombreDeCuenta = @NombreDeCuenta", conn);

                cmd.Parameters.AddWithValue("@NombreDeCuenta", NombreDeCuenta);
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
                    grupos.Add(grupo);
                }

                conn.Close();
                return Json(grupos);
            }
            catch (Exception e)
            {
                return Json("No se pudieron obtener los grupos");
            }
        }



        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("AgregarUsuarioAGrupo")]
        public dynamic AgregarUsuarioAGrupo(string nombreUsuario, string nombreGrupo)
        {
            try
            {
                MySqlConnection conn = new MySqlConnection("Server=localhost; database=base; uid=root; pwd=;");
                conn.Open();
                string query = "INSERT INTO usuarios_grupos (NombreDeCuenta, nombreReal) VALUES (@nombreUsuario, @nombreGrupo)";
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
                using (MySqlConnection conn = new MySqlConnection("Server=localhost; database=base; uid=root; pwd=;"))
                {
                    conn.Open();

                    // Verificar si la persona que ingresa los datos es administrador o creador del grupo
                    string permisosQuery = "SELECT rol FROM usuarios_grupos WHERE nombreReal = @nombreGrupo AND NombreDeCuenta = @nombreAdminOCreador AND (rol = 'a' OR rol = 'c')";
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
                    string query = "INSERT INTO usuarios_grupos (NombreDeCuenta, nombreReal) VALUES (@nombreUsuario, @nombreGrupo)";
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
                MySqlConnection conn = new MySqlConnection("Server=localhost; database=base; uid=root; pwd=;");
                {
                    conn.Open();

                    // Verificar si el grupo existe y si el usuario pertenece al grupo
                    string verificarQuery = @"
                SELECT COUNT(*) 
                FROM usuarios_grupos 
                WHERE nombreReal = @nombreRealGrupo AND NombreDeCuenta = @nombreUsuario";
                    MySqlCommand verificarCmd = new MySqlCommand(verificarQuery, conn);
                    verificarCmd.Parameters.AddWithValue("@nombreRealGrupo", nombreRealGrupo);
                    verificarCmd.Parameters.AddWithValue("@nombreUsuario", nombreUsuario);
                    int existe = Convert.ToInt32(verificarCmd.ExecuteScalar());

                    if (existe > 0)
                    {
                        // Eliminar al usuario del grupo
                        string eliminarQuery = "DELETE FROM usuarios_grupos WHERE nombreReal = @nombreRealGrupo AND NombreDeCuenta = @nombreUsuario";
                        MySqlCommand eliminarCmd = new MySqlCommand(eliminarQuery, conn);
                        eliminarCmd.Parameters.AddWithValue("@nombreRealGrupo", nombreRealGrupo);
                        eliminarCmd.Parameters.AddWithValue("@nombreUsuario", nombreUsuario);
                        eliminarCmd.ExecuteNonQuery();

                        // Opcional: Eliminar el grupo si no tiene más usuarios asociados
                        string eliminarGrupoQuery = "DELETE FROM grupos WHERE nombreReal = @nombreRealGrupo AND NOT EXISTS (SELECT * FROM usuarios_grupos WHERE nombreReal = @nombreRealGrupo)";
                        MySqlCommand eliminarGrupoCmd = new MySqlCommand(eliminarGrupoQuery, conn);
                        eliminarGrupoCmd.Parameters.AddWithValue("@nombreRealGrupo", nombreRealGrupo);
                        eliminarGrupoCmd.ExecuteNonQuery();

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
