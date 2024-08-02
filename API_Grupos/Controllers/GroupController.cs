using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;
using Google.Protobuf;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using Org.BouncyCastle.Security;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static API_Grupos.Controllers.GgroupController;

namespace API_Grupos.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("group")]
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

        [Microsoft.AspNetCore.Mvc.HttpPost]
        [Microsoft.AspNetCore.Mvc.Route("RegistrarGrupo")]
        public dynamic RegistrarGrupo([FromBody] Grupo group)
        {
            try
            {           
                if (!string.IsNullOrEmpty(group.nombreReal)
                    && !string.IsNullOrEmpty(group.nombreVisible)
                    && !string.IsNullOrEmpty(group.configuracion)
                    && !string.IsNullOrEmpty(group.imagen))
                {
                    MySqlConnection conn = new MySqlConnection("Server=localhost; database=base; uID=root; pwd=;");
                    conn.Open();
                    MySqlCommand cmd= new MySqlCommand("insert into grupos (nombreReal, nombreVisible, configuracion" +
                        ", foto) values (@nombreReal, @nombreVisible, @configuracion, @foto)", conn);
                    cmd.Parameters.AddWithValue("@nombreReal", group.nombreReal);
                    cmd.Parameters.AddWithValue("@nombreVisible", group.nombreVisible);
                    cmd.Parameters.AddWithValue("@configuracion", group.configuracion);
                    cmd.Parameters.AddWithValue("@imagen", group.imagen); //Es una URL, no? Entonces no lo transformo
                    cmd.ExecuteNonQuery();
                    if (!string.IsNullOrEmpty(group.descripcion))
                    {
                        MySqlCommand cmd2 = new MySqlCommand("update grupos set descripcion = @descripcion where nombreReal = @nombreReal",
                             conn);
                        cmd2.Parameters.AddWithValue("@descripcion", group.descripcion);
                        cmd2.ExecuteNonQuery();
                    }
                    conn.Close();
                    return Json("Registro correcto");
                }
                else
                {
                    return Json("Atributos nulos");
                }
            }
            catch
            {
                return Json("Registro fallido");
            }
        }


        [Microsoft.AspNetCore.Mvc.HttpGet]
        [Microsoft.AspNetCore.Mvc.Route("ObtenerGrupo")]
        //public dynamic ObtenerGrupo([FromBody] Grupo group)
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

        [Microsoft.AspNetCore.Mvc.HttpDelete]
        [Microsoft.AspNetCore.Mvc.Route("EliminarGrupo")]
        public dynamic EliminarGrupo(string nombreReal)
        {
            try
            {
                MySqlConnection conn = new MySqlConnection("Server=localhost; database=base; uid=root; pwd=;");
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("delete from grupos where nombreReal = @nombreReal");
                cmd.Parameters.AddWithValue("@nombreReal", nombreReal);
                cmd.ExecuteNonQuery();
                return Json("Se pudo eliminar");
            }
            catch
            {
                return Json("No se pudo eliminar");
            }
        }

        //Grupos al que pertenece un usuario
        [HttpGet]
        [Route("ObtenerGruposPorUsuario")]
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



        //Grupos al que pertenece un usuario



    }
}
