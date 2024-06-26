using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
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
            public string titulo { get; set; } = "";
            public string ubicacion { get; set; } = "";
            public string descripcion { get; set; } = "";
            public string imagen { get; set; } = "";
            public string fechayhora { get; set; } = "";
        }

        [HttpPost]
        [Route("postear")]
        public dynamic hacerPost([FromBody] PostData postdata)
        {
            MySqlConnection conn = new MySqlConnection("Server=localhost; database=base; uID=root; pwd=;");
            conn.Open();
            MySqlCommand cmd;
            if (!string.IsNullOrEmpty(postdata.text))
            {
                string texto = postdata.text;
                if (!string.IsNullOrEmpty(postdata.link))
                {
                    string url = postdata.link;
                    cmd = new MySqlCommand("INSERT INTO posts (texto,url) VALUES (@Texto,@url)", conn);
                    cmd.Parameters.AddWithValue("@Texto", texto);
                    cmd.Parameters.AddWithValue("@url", url);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    return "guardado correcto";
                }
                else
                {
                    if (!string.IsNullOrEmpty(postdata.image))
                    {
                        byte[] imagen = Convert.FromBase64String(postdata.image);
                        cmd = new MySqlCommand("INSERT INTO posts (texto,imagen) VALUES (@Texto,@Imagen)", conn);
                        cmd.Parameters.AddWithValue("@Texto", texto);
                        cmd.Parameters.AddWithValue("@Imagen", imagen);
                        cmd.ExecuteNonQuery();
                        conn.Close();
                        return "guardado correcto";
                    }
                    else
                    {
                        cmd = new MySqlCommand("INSERT INTO posts (texto) VALUES (@Texto)", conn);
                        cmd.Parameters.AddWithValue("@Texto", texto);
                        cmd.ExecuteNonQuery();
                        conn.Close();
                        return "guardado correcto";
                    }
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(postdata.image))
                {
                    byte[] imagen = Convert.FromBase64String(postdata.image);
                    cmd = new MySqlCommand("INSERT INTO posts (imagen) VALUES (@Imagen)", conn);
                    cmd.Parameters.AddWithValue("@Imagen", imagen);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    return "guardado correcto";
                }
                else
                {
                    string url = postdata.link;
                    cmd = new MySqlCommand("INSERT INTO posts (url) VALUES (@url)", conn);
                    cmd.Parameters.AddWithValue("@url", url);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    return "guardado correcto";
                }
            }
        }

        [HttpGet]
        [Route("postPorId")]
        public dynamic conseguirPost(int id)
        {
            try
            {
                MySqlConnection conn = new MySqlConnection("Server=localhost; database=base; uID=root; pwd=;");
                conn.Open();
                MySqlCommand command = new MySqlCommand("SELECT texto,imagen,url FROM posts WHERE id=@Id", conn);
                command.Parameters.AddWithValue("@Id", id);
                MySqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    string texto;
                    if (string.IsNullOrEmpty(reader["texto"].ToString()))
                    {
                        texto = "";
                    }
                    else
                    {
                        texto = reader["texto"].ToString();
                    }
                    string imagen;
                    if (string.IsNullOrEmpty(reader["imagen"].ToString()))
                    {
                        imagen = "";
                    }
                    else
                    {
                        imagen = Convert.ToBase64String((byte[])reader["imagen"]);
                    }
                    string url;
                    if (string.IsNullOrEmpty(reader["url"].ToString()))
                    {
                        url = "";
                    }
                    else
                    {
                        url = reader["url"].ToString();
                    }
                    var data = new { imagen = imagen, url = url, texto = texto };
                    return Json(data);
                }
                else
                {
                    return "no se encuentra";
                }
            }
            catch (Exception ex)
            {
                return "no se encuentra";
            }

            //devuelve un tipo reader para desempaquetar su contenido en la ventana principal
        }

        [HttpDelete]
        [Route("eliminarPost")]
        public dynamic eliminarPost(string id)
        {
            try
            {
                MySqlConnection conn = new MySqlConnection("Server=localhost; database=base; uID=root; pwd=;");
                conn.Open();
                MySqlCommand command = new MySqlCommand("DELETE FROM posts WHERE id=@Id", conn);
                command.Parameters.AddWithValue("@Id", int.Parse(id));
                command.ExecuteNonQuery();
                conn.Close();
                return "Post eliminado";
            }
            catch
            {
                return "Post no eliminado";
            }     
        }

        [HttpPut]
        [Route("modificarPost")]
        public dynamic modificarPost([FromBody] PostData postdata)
        {
            string id = postdata.id;
            try
            {
                MySqlConnection conn = new MySqlConnection("Server=localhost; database=base; uID=root; pwd=;");
                conn.Open();
                MySqlCommand cmd;
                
                if (!string.IsNullOrEmpty(postdata.text))
                {
                    string texto = postdata.text;
                    if (!postdata.link.Equals(""))
                    {
                        string url = postdata.link;
                        cmd = new MySqlCommand("UPDATE posts SET texto=@texto,imagen=@imagen,url=@url WHERE id=@id", conn);
                        cmd.Parameters.AddWithValue("@Texto", texto);
                        cmd.Parameters.AddWithValue("@url", url);
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.Parameters.AddWithValue("@Imagen", null);
                        cmd.ExecuteNonQuery();
                        conn.Close();
                        return "Modificacion correcta";
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(postdata.image))
                        {
                            byte[] imagen = Convert.FromBase64String(postdata.image);
                            cmd = new MySqlCommand("UPDATE posts SET texto=@texto,imagen=@imagen,url=@url WHERE id=@id", conn);
                            cmd.Parameters.AddWithValue("@Texto", texto);
                            cmd.Parameters.AddWithValue("@Imagen", imagen);
                            cmd.Parameters.AddWithValue("@id", id);
                            cmd.Parameters.AddWithValue("@url", null);
                            cmd.ExecuteNonQuery();
                            conn.Close();
                            return "Modificacion correcta";
                        }
                        else
                        {
                            cmd = new MySqlCommand("UPDATE posts SET texto=@texto,imagen=@imagen,url=@url WHERE id=@id", conn);
                            cmd.Parameters.AddWithValue("@Texto", texto);
                            cmd.Parameters.AddWithValue("@id", id);
                            cmd.Parameters.AddWithValue("@url", null);
                            cmd.Parameters.AddWithValue("@Imagen", null);
                            cmd.ExecuteNonQuery();
                            conn.Close();
                            return "Modificacion correcta";
                        }
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(postdata.image))
                    {
                        byte[] imagen = Convert.FromBase64String(postdata.image);
                        cmd = new MySqlCommand("UPDATE posts SET texto=@texto,imagen=@imagen,url=@url WHERE id=@id", conn);
                        cmd.Parameters.AddWithValue("@Imagen", imagen);
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.Parameters.AddWithValue("@Texto", null);
                        cmd.Parameters.AddWithValue("@url", null);
                        cmd.ExecuteNonQuery();
                        conn.Close();
                        return "Modificacion correcta";
                    }
                    else
                    {
                        string url = postdata.link;
                        cmd = new MySqlCommand("UPDATE posts SET texto=@texto,imagen=@imagen,url=@url WHERE id=@id", conn);
                        cmd.Parameters.AddWithValue("@url", url);
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.Parameters.AddWithValue("@Texto", null);
                        cmd.Parameters.AddWithValue("@Imagen", null);
                        cmd.ExecuteNonQuery();
                        conn.Close();
                        return "Modificacion correcta";
                    }
                }
            }
            catch (Exception ex)
            {
                return "Modificación incorrecta";
            }
        }

        [HttpPost]
        [Route("hacerEvento")]
        public dynamic hacerEvento([FromBody] EventData eventdata)
        {
            string titulo = eventdata.titulo;
            string fechayhora = eventdata.fechayhora;
            byte[] imagen = Convert.FromBase64String(eventdata.imagen);
            MySqlConnection conn = new MySqlConnection("Server=localhost; database=base; uID=root; pwd=;");
            conn.Open();
            if (!string.IsNullOrEmpty(eventdata.ubicacion))
            {
                if (!string.IsNullOrEmpty(eventdata.descripcion))
                {
                    string ubicacion = eventdata.ubicacion;
                    string descripcion = eventdata.descripcion;
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
                else
                {
                    string ubicacion = eventdata.ubicacion;
                    MySqlCommand cmd = new MySqlCommand("INSERT INTO eventos (titulo,ubicacion,fechayhora,foto) VALUES (@Titulo,@Ubicacion,@FechayHora,@Foto)", conn);
                    cmd.Parameters.AddWithValue("@Titulo", titulo);
                    cmd.Parameters.AddWithValue("@Ubicacion", ubicacion);
                    cmd.Parameters.AddWithValue("@FechayHora", fechayhora);
                    cmd.Parameters.AddWithValue("@Foto", imagen);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    return "guardado correcto";
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(eventdata.descripcion))
                {
                    string descripcion = eventdata.descripcion;
                    MySqlCommand cmd = new MySqlCommand("INSERT INTO eventos (titulo,descripcion,fechayhora,foto) VALUES (@Titulo,@Descripcion,@FechayHora,@Foto)", conn);
                    cmd.Parameters.AddWithValue("@Titulo", titulo);
                    cmd.Parameters.AddWithValue("@Descripcion", descripcion);
                    cmd.Parameters.AddWithValue("@FechayHora", fechayhora);
                    cmd.Parameters.AddWithValue("@Foto", imagen);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    return "guardado correcto";
                }
                else
                {
                    MySqlCommand cmd = new MySqlCommand("INSERT INTO eventos (titulo,fechayhora,foto) VALUES (@Titulo,@FechayHora,@Foto)", conn);
                    cmd.Parameters.AddWithValue("@Titulo", titulo);
                    cmd.Parameters.AddWithValue("@FechayHora", fechayhora);
                    cmd.Parameters.AddWithValue("@Foto", imagen);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    return "guardado correcto";
                }
            }
        }

        [HttpDelete]
        [Route("eliminarEvento")]
        public dynamic eliminarEvento(string id)
        {
            try
            {
                MySqlConnection conn = new MySqlConnection("Server=localhost; database=base; uID=root; pwd=;");
                conn.Open();
                MySqlCommand command = new MySqlCommand("DELETE FROM eventos WHERE id=@Id", conn);
                command.Parameters.AddWithValue("@Id", int.Parse(id));
                command.ExecuteNonQuery();
                conn.Close();
                return "Evento eliminado";
            }
            catch
            {
                return "Evento no eliminado";
            }
        }

        [HttpGet]
        [Route("eventoPorId")]
        public dynamic conseguirEvento(int id)
        {
            try
            {
                MySqlConnection conn = new MySqlConnection("Server=localhost; database=base; uID=root; pwd=;");
                conn.Open();
                MySqlCommand command = new MySqlCommand("SELECT titulo,ubicacion,descripcion,foto,fechayhora FROM eventos WHERE id=@Id", conn);
                command.Parameters.AddWithValue("@Id", id);
                MySqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    string ubicacion;
                    if (string.IsNullOrEmpty(reader["ubicacion"].ToString()))
                    {
                        ubicacion = "";
                    }
                    else
                    {
                        ubicacion = reader["ubicacion"].ToString();
                    }
                    string descripcion;
                    if (string.IsNullOrEmpty(reader["descripcion"].ToString()))
                    {
                        descripcion = "";
                    }
                    else
                    {
                        descripcion = reader["descripcion"].ToString();
                    }
                    var data = new { titulo = reader["titulo"].ToString(), ubicacion = ubicacion, descripcion = reader["descripcion"].ToString(), foto = Convert.ToBase64String((byte[])reader["foto"]), fechayhora = reader["fechayhora"].ToString() };
                    return Json(data);
                }
                else
                {
                    return null;
                }
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        [HttpPut]
        [Route("modificarEvento")]
        public string modificarEvento([FromBody] EventData eventdata)
        {
            string id = eventdata.id;
            MySqlConnection conn = new MySqlConnection("Server=localhost; database=base; uID=root; pwd=;");
            conn.Open();
            try
            {

                byte[] imagen = Convert.FromBase64String(eventdata.imagen);
                string titulo = eventdata.titulo, fechayhora = eventdata.fechayhora, ubicacion = eventdata.ubicacion, descripcion = eventdata.descripcion;
                MySqlCommand cmd = new MySqlCommand("UPDATE eventos SET ubicacion=@Ubicacion, titulo=@Titulo, descripcion=@Descripcion, foto=@Foto, fechayhora=@FechayHora WHERE id=@id", conn);
                if (!string.IsNullOrEmpty(eventdata.ubicacion))
                {
                    cmd.Parameters.AddWithValue("@Ubicacion", ubicacion);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Ubicacion", null);
                }
                if (!string.IsNullOrEmpty(eventdata.titulo))
                {
                    cmd.Parameters.AddWithValue("@Titulo", titulo);
                }
                else
                {
                    return "modificacion erronea";
                }
                if (!string.IsNullOrEmpty(eventdata.descripcion))
                {
                    cmd.Parameters.AddWithValue("@Descripcion", descripcion);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Descripcion", null);
                }
                if (!string.IsNullOrEmpty(eventdata.imagen))
                {
                    cmd.Parameters.AddWithValue("@Foto", imagen);
                }
                else
                {
                    return "modificacion erronea";
                }
                if (!string.IsNullOrEmpty(eventdata.fechayhora))
                {
                    cmd.Parameters.AddWithValue("@FechayHora", fechayhora);
                }
                else
                {
                    return "modificacion erronea";
                }
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
                conn.Close();
                return "modificacion correcta";

            }
            catch
            {
                return "modificación incorrecta";
            }
        }

        [HttpGet]
        [Route("existePost")]
        public dynamic existePost(int id)
        {
            MySqlConnection conn = new MySqlConnection("Server=localhost; database=base; uID=root; pwd=;");
            conn.Open();
            MySqlCommand command = new MySqlCommand("SELECT id FROM posts WHERE id=@Id", conn);
            command.Parameters.AddWithValue("@Id", id);
            MySqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                var data = true;
                if (reader["id"].ToString().Equals(Convert.ToString(id)))
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

            //devuelve un tipo reader para desempaquetar su contenido en la ventana principal
        }

        [HttpGet]
        [Route("ultimoPost")]
        public dynamic ultimoPost()
        {
            try
            {
                MySqlConnection conn = new MySqlConnection("Server=localhost; database=base; uID=root; pwd=;");
                conn.Open();
                MySqlCommand command = new MySqlCommand("SELECT id FROM posts ORDER BY id DESC LIMIT 1", conn);
                MySqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    string id = reader["id"].ToString();
                    return Json(id);
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

        [HttpGet]
        [Route("ultimoEvento")]
        public dynamic ultimoEvento()
        {
            try
            {
                MySqlConnection conn = new MySqlConnection("Server=localhost; database=base; uID=root; pwd=;");
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT id FROM eventos ORDER BY id DESC LIMIT 1", conn);
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    string id = reader["id"].ToString();
                    return Json(id);
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

        [HttpGet]
        [Route("seleccionarTodosLosPost")]
        public dynamic seleccionarTodosLosPost()
        {
            try
            {
                string connectionString = "server = localhost; database = base; uid = root; ";
                // Create a new MySQL connection
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("SELECT id,texto FROM posts", conn);
                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    return Json(dataTable);
                }
            }
            catch (Exception ex)
            {
                return "Error al cargar Datagrid";
            }
        }

        [HttpGet]
        [Route("seleccionarTodosLosEventos")]
        public dynamic seleccionarTodosLosEventos()
        {
            try
            {
                string connectionString = "server = localhost; database = base; uid = root; ";
                // Create a new MySQL connection
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("SELECT id,titulo FROM eventos", conn);
                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    return Json(dataTable);
                }
            }
            catch (Exception ex)
            {
                return "Error al cargar Datagrid";
            }
        }
    }
}