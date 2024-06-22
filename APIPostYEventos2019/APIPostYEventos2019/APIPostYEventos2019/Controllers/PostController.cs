using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Web;
using System.Web.Http;

namespace APIPostYEventos2019.Controllers
{
    [Route("post")]
    public class PostController : ApiController
    {
        public class PostData
        {
            public string id { get; set; }
            public string text { get; set; }
            public string link { get; set; }
            public string image { get; set; }
        }
        public class EventData
        {
            public string id { get; set;}
            public string titulo { get; set; }
            public string ubicacion { get; set; }
            public string descripcion { get; set; }
            public string imagen { get; set; }
            public string fechayhora { get; set; }
        }
        [HttpPost]
        [Route("postear")]
        public dynamic hacerPost([FromBody] PostData postdata)
        {
            string texto = postdata.text;
            string url = postdata.link;
            byte[] imagen = Convert.FromBase64String(postdata.image);
            MySqlConnection conn = new MySqlConnection("Server=localhost; database=base; uID=root; pwd=;");
            conn.Open();
            MySqlCommand cmd = new MySqlCommand("INSERT INTO posts (texto,imagen,url) VALUES (@Texto,@Imagen,@url)", conn);
            cmd.Parameters.AddWithValue("@Texto", texto);
            cmd.Parameters.AddWithValue("@Imagen", imagen);
            cmd.Parameters.AddWithValue("@url", url);
            cmd.ExecuteNonQuery();
            conn.Close();
            return "guardado correcto";
        }

        [HttpGet]
        [Route("postPorId")]
        public dynamic conseguirPost(int id)
        {
            MySqlConnection conn = new MySqlConnection("Server=localhost; database=base; uID=root; pwd=;");
            conn.Open();
            MySqlCommand command = new MySqlCommand("SELECT texto,imagen,url FROM posts WHERE id=@Id", conn);
            command.Parameters.AddWithValue("@Id", id);
            MySqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                var data = new { texto = reader["texto"].ToString(), imagen =Convert.ToBase64String((byte[])reader["imagen"]), url = reader["url"].ToString() };
                return Json(data);
            }
            else
            {
                return null;
            }

            //devuelve un tipo reader para desempaquetar su contenido en la ventana principal
        }

        [HttpDelete]
        [Route("eliminarPost")]
        public dynamic eliminarPost(string id)
        {
            MySqlConnection conn = new MySqlConnection("Server=localhost; database=base; uID=root; pwd=;");
            conn.Open();
            MySqlCommand command = new MySqlCommand("DELETE FROM posts WHERE id=@Id", conn);
            command.Parameters.AddWithValue("@Id", int.Parse(id));
            command.ExecuteNonQuery();
            conn.Close();
            return "Post eliminado";
        }

        [HttpPut]
        [Route("modificarPost")]
        public dynamic modificarPost([FromBody] PostData postdata)
        {
            string id = postdata.id;
            string texto = postdata.text;
            string url = postdata.link;
            byte[] imagen = Convert.FromBase64String(postdata.image);
            MySqlConnection conn = new MySqlConnection("Server=localhost; database=base; uID=root; pwd=;");
            conn.Open();
            MySqlCommand command = new MySqlCommand("UPDATE posts SET texto=@texto,imagen=@imagen,url=@url WHERE id=@id", conn);
            command.Parameters.AddWithValue("@id", int.Parse(id));
            command.Parameters.AddWithValue("@texto", texto);
            command.Parameters.AddWithValue("@imagen", imagen);
            command.Parameters.AddWithValue("@url", url);
            command.ExecuteNonQuery();
            conn.Close();
            return "modificacion correcta";
        }
        [HttpPost]
        [Route("hacerEvento")]
        public dynamic hacerEvento([FromBody] EventData eventdata)
        {
            string titulo = eventdata.titulo;
            string ubicacion = eventdata.ubicacion;
            string descripcion = eventdata.descripcion;
            string fechayhora = eventdata.fechayhora;
            byte[] imagen = Convert.FromBase64String(eventdata.imagen);
            MySqlConnection conn = new MySqlConnection("Server=localhost; database=base; uID=root; pwd=;");
            conn.Open();
            MySqlCommand cmd = new MySqlCommand("INSERT INTO eventos (titulo,ubicacion,descripcion,fechayhora,foto) VALUES (@Titulo,@Ubicacion,@Descripcion,@FechayHora,@Foto)", conn);
            cmd.Parameters.AddWithValue("@Titulo", titulo);
            cmd.Parameters.AddWithValue("@Ubicacion", ubicacion);
            cmd.Parameters.AddWithValue("@Descripcion", descripcion);
            cmd.Parameters.AddWithValue("@FechayHora", fechayhora);
            cmd.Parameters.AddWithValue("@Foto", imagen);
            cmd.ExecuteNonQuery();
            conn.Close();
            return "guardado correcto";
        }

        [HttpDelete]
        [Route("eliminarEvento")]
        public dynamic eliminarEvento(string id)
        {
            MySqlConnection conn = new MySqlConnection("Server=localhost; database=base; uID=root; pwd=;");
            conn.Open();
            MySqlCommand command = new MySqlCommand("DELETE FROM eventos WHERE id=@Id", conn);
            command.Parameters.AddWithValue("@Id", int.Parse(id));
            command.ExecuteNonQuery();
            conn.Close();
            return "Evento eliminado";
        }

        [HttpGet]
        [Route("eventoPorId")]
        public dynamic conseguirEvento(int id)
        {
            MySqlConnection conn = new MySqlConnection("Server=localhost; database=base; uID=root; pwd=;");
            conn.Open();
            MySqlCommand command = new MySqlCommand("SELECT titulo,ubicacion,descripcion,foto,fechayhora FROM eventos WHERE id=@Id", conn);
            command.Parameters.AddWithValue("@Id", id);
            MySqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                var data = new { titulo = reader["titulo"].ToString(), ubicacion = reader["ubicacion"].ToString(), descripcion = reader["descripcion"].ToString(), foto = Convert.ToBase64String((byte[])reader["foto"]), fechayhora = reader["fechayhora"].ToString() };
                return Json(data);
            }
            else
            {
                return null;
            }

            //devuelve un tipo reader para desempaquetar su contenido en la ventana principal
        }
        [HttpPut]
        [Route("modificarEvento")]
        public dynamic modificarEvento([FromBody] EventData eventdata)
        {
            string id = eventdata.id;
            string titulo = eventdata.titulo;
            string ubicacion = eventdata.ubicacion;
            string descripcion = eventdata.descripcion;
            string fechayhora = eventdata.fechayhora;
            byte[] foto = Convert.FromBase64String(eventdata.imagen);
            MySqlConnection conn = new MySqlConnection("Server=localhost; database=base; uID=root; pwd=;");
            conn.Open();
            MySqlCommand command = new MySqlCommand("UPDATE eventos SET titulo=@titulo, ubicacion=@ubicacion, descripcion=@descripcion, foto=@foto, fechayhora=@fechayhora WHERE id=@id", conn);
            command.Parameters.AddWithValue("@id", int.Parse(id));
            command.Parameters.AddWithValue("@titulo", titulo);
            command.Parameters.AddWithValue("@ubicacion", ubicacion);
            command.Parameters.AddWithValue("@descripcion", descripcion);
            command.Parameters.AddWithValue("@foto", foto);
            command.Parameters.AddWithValue("@fechayhora", fechayhora);
            command.ExecuteNonQuery();
            conn.Close();
            return "modificacion correcta";
        }
    }
}