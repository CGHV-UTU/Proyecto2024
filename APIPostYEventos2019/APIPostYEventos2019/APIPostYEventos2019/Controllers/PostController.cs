using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using static APIPostYEventos2019.Controllers.PostController;

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
            public string user { get; set; }

            public string fechayhora { get; set; }
        }
        public class EventData
        {
            public string id { get; set; }

            public string nombreReal { get; set; } = "";
            public string titulo { get; set; } = "";
            public string ubicacion { get; set; } = "";
            public string descripcion { get; set; } = "";
            public string imagen { get; set; } = "";
            public string fechayhora { get; set; } = "";
            public string user { get; set; }

        }
        public class CommentData
        {
            public string id { get; set; }

            public string NombreDeCuenta { get; set; }
            public string NombreCreador { get; set; }

            public string IdPost { get; set; }

            public string texto { get; set; }

            public string fechayhora { get; set; }
        }
        public class ReportePostOComentario
        {
            public string usuario { get; set; }
            public int id { get; set; }
            public string tipo { get; set; }
            public string descripcion { get; set; }
        }
        public async Task<string> SubirImagenAGitHub(string imagen, string carpeta)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    string token = "token"; // Token para repositorio privado. Cambiar por el token real
                    string nombreDeImagen = GenerarIdAleatorio(8) + ".png"; // nombre aleatorio para que el nombre del archivo no se repita // Carpeta de GitHub en donde se guarda la imagen

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

        [HttpPost]
        [Route("postear")]
        public async Task<dynamic> HacerPost([FromBody] PostData postdata)
        {
            using (MySqlConnection conn = new MySqlConnection("Server=localhost; database=infini; uID=root; pwd=;"))
            {
                await conn.OpenAsync();
                int idPost;
                string linkImagen = null;

                if (!string.IsNullOrEmpty(postdata.text))
                {
                    string texto = postdata.text;

                    if (!string.IsNullOrEmpty(postdata.link))
                    {
                        string url = postdata.link;
                        using (MySqlCommand cmd = new MySqlCommand("INSERT INTO Posts (nombreDeCuenta, texto, video, fechaYhora) VALUES (@NombreDeCuenta, @Texto, @url, @fechaYhora); SELECT LAST_INSERT_id();", conn))
                        {
                            cmd.Parameters.AddWithValue("@NombreDeCuenta", postdata.user);
                            cmd.Parameters.AddWithValue("@Texto", texto);
                            cmd.Parameters.AddWithValue("@url", url);
                            cmd.Parameters.AddWithValue("fechaYhora", postdata.fechayhora);
                            idPost = Convert.ToInt32(await cmd.ExecuteScalarAsync());
                        }
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(postdata.image))
                        {
                            linkImagen = await SubirImagenAGitHub(postdata.image, "PostImages");

                            using (MySqlCommand cmd = new MySqlCommand("INSERT INTO Posts (nombreDeCuenta, texto, imagen, fechaYhora) VALUES (@NombreDeCuenta, @Texto, @Imagen, @fechaYhora); SELECT LAST_INSERT_id();", conn))
                            {
                                cmd.Parameters.AddWithValue("@NombreDeCuenta", postdata.user);
                                cmd.Parameters.AddWithValue("@Texto", texto);
                                cmd.Parameters.AddWithValue("@Imagen", linkImagen);
                                cmd.Parameters.AddWithValue("fechaYhora", postdata.fechayhora);
                                idPost = Convert.ToInt32(await cmd.ExecuteScalarAsync());
                            }
                        }
                        else
                        {
                            using (MySqlCommand cmd = new MySqlCommand("INSERT INTO Posts (nombreDeCuenta, texto, fechaYhora) VALUES (@NombreDeCuenta, @Texto, @fechaYhora); SELECT LAST_INSERT_id();", conn))
                            {
                                cmd.Parameters.AddWithValue("@NombreDeCuenta", postdata.user);
                                cmd.Parameters.AddWithValue("@Texto", texto);
                                cmd.Parameters.AddWithValue("fechaYhora", postdata.fechayhora);
                                idPost = Convert.ToInt32(await cmd.ExecuteScalarAsync());
                            }
                        }
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(postdata.image))
                    {
                        linkImagen = await SubirImagenAGitHub(postdata.image, "PostImages");

                        using (MySqlCommand cmd = new MySqlCommand("INSERT INTO Posts (nombreDeCuenta, imagen, fechaYhora) VALUES (@NombreDeCuenta, @Imagen, @fechaYhora); SELECT LAST_INSERT_id();", conn))
                        {
                            cmd.Parameters.AddWithValue("@NombreDeCuenta", postdata.user);
                            cmd.Parameters.AddWithValue("@Imagen", linkImagen);
                            cmd.Parameters.AddWithValue("fechaYhora", postdata.fechayhora);
                            idPost = Convert.ToInt32(await cmd.ExecuteScalarAsync());
                        }
                    }
                    else
                    {
                        string url = postdata.link;
                        using (MySqlCommand cmd = new MySqlCommand("INSERT INTO Posts (nombreDeCuenta, video, fechaYhora) VALUES (@NombreDeCuenta, @url, @fechaYhora); SELECT LAST_INSERT_id();", conn))
                        {
                            cmd.Parameters.AddWithValue("@NombreDeCuenta", postdata.user);
                            cmd.Parameters.AddWithValue("@url", url);
                            cmd.Parameters.AddWithValue("fechaYhora", postdata.fechayhora);
                            idPost = Convert.ToInt32(await cmd.ExecuteScalarAsync());
                        }
                    }
                }

                using (MySqlCommand cmd2 = new MySqlCommand("INSERT INTO PostPublico (nombreDeCuenta, idPost, fechaYhora) VALUES (@NombreDeCuenta, @id, @fechaYhora)", conn))
                {
                    cmd2.Parameters.AddWithValue("@NombreDeCuenta", postdata.user);
                    cmd2.Parameters.AddWithValue("@id", idPost);
                    cmd2.Parameters.AddWithValue("fechaYhora", postdata.fechayhora);
                    await cmd2.ExecuteNonQueryAsync();
                }
                return new { mensaje = "Guardado correcto" };
            }
        }

        [HttpGet]
        [Route("postPorId")]
        public async Task<dynamic> conseguirPost(int id)
        {
            try
            {
                MySqlConnection conn = new MySqlConnection("Server=localhost; database=infini; uID=root; pwd=;");
                conn.Open();
                MySqlCommand command = new MySqlCommand("SELECT texto,imagen,video,fechaYhora FROM Posts WHERE idPost=@Id", conn);
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
                    string fechaYhora;
                    if (string.IsNullOrEmpty(reader["fechaYhora"].ToString()))
                    {
                        fechaYhora = "";
                    }
                    else
                    {
                        fechaYhora = reader["fechaYhora"].ToString();
                    }
                    string imagen;
                    if (string.IsNullOrEmpty(reader["imagen"].ToString()))
                    {
                        imagen = "";
                    }
                    else
                    {
                        imagen = await CargarImagenDeGitHub(reader["imagen"].ToString());
                    }

                    string url;
                    if (string.IsNullOrEmpty(reader["video"].ToString()))
                    {
                        url = "";
                    }
                    else
                    {
                        url = reader["video"].ToString();
                    }
                    var data = new { texto = texto, imagen = imagen, url = url, fechaYhora = fechaYhora };
                    return Json(data);
                }
                else
                {
                    return Json("no se encuentra");
                }
            }
            catch (Exception)
            {
                return Json("no se encuentra");
            }
        }

        [HttpGet]
        [Route("conseguirCreador")]
        public dynamic conseguirCreador(int id)
        {
            try
            {
                MySqlConnection conn = new MySqlConnection("Server=localhost; database=infini; uID=root; pwd=;");
                conn.Open();
                MySqlCommand command = new MySqlCommand("SELECT nombreDeCuenta FROM Posts WHERE idPost=@Id", conn);
                command.Parameters.AddWithValue("@Id", id);
                MySqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    string nombreDeCuenta;
                    if (string.IsNullOrEmpty(reader["nombreDeCuenta"].ToString()))
                    {
                        nombreDeCuenta = "";
                        return Json(nombreDeCuenta);
                    }
                    else
                    {
                        nombreDeCuenta = reader["nombreDeCuenta"].ToString();
                        return Json(nombreDeCuenta);
                    }
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

        [HttpDelete]
        [Route("eliminarPost")]
        public dynamic eliminarPost(string id)
        {
            try
            {
                MySqlConnection conn = new MySqlConnection("Server=localhost; database=infini; uID=root; pwd=;");
                conn.Open();
                MySqlCommand command = new MySqlCommand("DELETE FROM Comentarios WHERE idPost=@Id;DELETE FROM PostPublico WHERE idPost = @Id;DELETE FROM PostGrupo WHERE idPost = @Id;DELETE FROM Posts WHERE idPost = @Id; ", conn);
                command.Parameters.AddWithValue("@Id", id);
                command.ExecuteNonQuery();
                conn.Close();
                return Json("Post eliminado");
            }
            catch
            {
                return Json("Post no eliminado");
            }
        }

        [HttpPut]
        [Route("modificarPost")]
        public async Task<dynamic> modificarPost([FromBody] PostData postdata)
        {
            string linkImagen;
            string id = postdata.id;
            try
            {
                MySqlConnection conn = new MySqlConnection("Server=localhost; database=infini; uID=root; pwd=;");
                conn.Open();
                MySqlCommand cmd;

                if (!string.IsNullOrEmpty(postdata.text))
                {
                    string texto = postdata.text;
                    if (!postdata.link.Equals(""))
                    {
                        string url = postdata.link;
                        cmd = new MySqlCommand("UPDATE Posts SET texto=@texto,imagen=@imagen,url=@url WHERE idPost=@id", conn);
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
                            linkImagen = await SubirImagenAGitHub(postdata.image, "PostImages");
                            cmd = new MySqlCommand("UPDATE Posts SET texto=@texto,imagen=@imagen,video=@url WHERE idPost=@id", conn);
                            cmd.Parameters.AddWithValue("@Texto", texto);
                            cmd.Parameters.AddWithValue("@Imagen", linkImagen);
                            cmd.Parameters.AddWithValue("@id", id);
                            cmd.Parameters.AddWithValue("@url", null);
                            cmd.ExecuteNonQuery();
                            conn.Close();
                            return "Modificacion correcta";
                        }
                        else
                        {
                            cmd = new MySqlCommand("UPDATE Posts SET texto=@texto,imagen=@imagen,video=@url WHERE idPost=@id", conn);
                            cmd.Parameters.AddWithValue("@texto", texto);
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
                        linkImagen = await SubirImagenAGitHub(postdata.image, "PostImages");
                        cmd = new MySqlCommand("UPDATE Posts SET texto=@texto,imagen=@imagen,video=@url WHERE idPost=@id", conn);
                        cmd.Parameters.AddWithValue("@imagen", linkImagen);
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.Parameters.AddWithValue("@texto", null);
                        cmd.Parameters.AddWithValue("@url", null);
                        cmd.ExecuteNonQuery();
                        conn.Close();
                        return "Modificacion correcta";
                    }
                    else
                    {
                        string url = postdata.link;
                        cmd = new MySqlCommand("UPDATE Posts SET texto=@texto,imagen=@imagen,video=@url WHERE idPost=@id", conn);
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
            catch (Exception)
            {
                return "Modificación incorrecta";
            }
        }

        [HttpPost]
        [Route("hacerEvento")]
        public async Task<dynamic> hacerEvento([FromBody] EventData eventdata)
        {
            string titulo = eventdata.titulo;
            string fechayhora = eventdata.fechayhora;
            string linkImagen;
            linkImagen = await SubirImagenAGitHub(eventdata.imagen, "EventImages");
            MySqlConnection conn = new MySqlConnection("Server=localhost; database=infini; uID=root; pwd=;");
            conn.Open();
            MySqlCommand cmd2;
            if (string.IsNullOrEmpty(eventdata.user))
            {
                throw new Exception("ERROR: El campo 'user' no puede ser nulo");
            }
            if (string.IsNullOrEmpty(eventdata.nombreReal))
            {
                throw new Exception("ERROR: El campo 'nombreReal' no puede ser nulo");
            }
            if (!string.IsNullOrEmpty(eventdata.ubicacion))
            {
                if (!string.IsNullOrEmpty(eventdata.descripcion))
                {
                    string ubicacion = eventdata.ubicacion;
                    string descripcion = eventdata.descripcion;
                    MySqlCommand cmd = new MySqlCommand("INSERT INTO Eventos (nombreReal,titulo,ubicacion,descripcion,fechaYHora,foto) VALUES (@nombreReal,@Titulo,@Ubicacion,@Descripcion,@FechayHora,@Foto); SELECT LAST_INSERT_id();", conn);
                    cmd.Parameters.AddWithValue("@Titulo", titulo);
                    cmd.Parameters.AddWithValue("@Ubicacion", ubicacion);
                    cmd.Parameters.AddWithValue("@Descripcion", descripcion);
                    cmd.Parameters.AddWithValue("@FechayHora", fechayhora);
                    cmd.Parameters.AddWithValue("@Foto", linkImagen);
                    cmd.Parameters.AddWithValue("@nombreReal", eventdata.nombreReal);
                    long idEvento = Convert.ToInt64(cmd.ExecuteScalar());
                    cmd2 = new MySqlCommand("UPDATE Participa SET participaEvento=true WHERE nombreDeCuenta=@NombreDeCuenta AND nombreReal=@nombreReal;", conn);
                    cmd2.Parameters.AddWithValue("@NombreDeCuenta", eventdata.user);
                    cmd2.Parameters.AddWithValue("@nombreReal", eventdata.nombreReal);
                    cmd2.ExecuteNonQuery();
                    conn.Close();
                    return "guardado correcto";
                }
                else
                {
                    string ubicacion = eventdata.ubicacion;
                    MySqlCommand cmd = new MySqlCommand("INSERT INTO Eventos (nombreReal,titulo,ubicacion,fechayhora,foto) VALUES (@nombreReal,@Titulo,@Ubicacion,@FechayHora,@Foto); SELECT LAST_INSERT_id();", conn);
                    cmd.Parameters.AddWithValue("@Titulo", titulo);
                    cmd.Parameters.AddWithValue("@Ubicacion", ubicacion);
                    cmd.Parameters.AddWithValue("@FechayHora", fechayhora);
                    cmd.Parameters.AddWithValue("@Foto", linkImagen);
                    cmd.Parameters.AddWithValue("@nombreReal", eventdata.nombreReal);
                    long idEvento = Convert.ToInt64(cmd.ExecuteScalar());
                    cmd2 = new MySqlCommand("UPDATE Participa SET participaEvento=true WHERE nombreDeCuenta=@NombreDeCuenta AND nombreReal=@nombreReal;", conn);
                    cmd2.Parameters.AddWithValue("@NombreDeCuenta", eventdata.user);
                    cmd2.Parameters.AddWithValue("@nombreReal", eventdata.nombreReal);
                    cmd2.ExecuteNonQuery();
                    conn.Close();
                    return "guardado correcto";
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(eventdata.descripcion))
                {
                    string descripcion = eventdata.descripcion;
                    MySqlCommand cmd = new MySqlCommand("INSERT INTO Eventos (nombreReal,titulo,descripcion,fechayhora,foto) VALUES (@nombreReal,@Titulo,@Descripcion,@FechayHora,@Foto); SELECT LAST_INSERT_id();", conn);
                    cmd.Parameters.AddWithValue("@Titulo", titulo);
                    cmd.Parameters.AddWithValue("@Descripcion", descripcion);
                    cmd.Parameters.AddWithValue("@FechayHora", fechayhora);
                    cmd.Parameters.AddWithValue("@Foto", linkImagen);
                    cmd.Parameters.AddWithValue("@nombreReal", eventdata.nombreReal);
                    long idEvento = Convert.ToInt64(cmd.ExecuteScalar());
                    cmd2 = new MySqlCommand("UPDATE Participa SET participaEvento=true WHERE nombreDeCuenta=@NombreDeCuenta AND nombreReal=@nombreReal;", conn);
                    cmd2.Parameters.AddWithValue("@NombreDeCuenta", eventdata.user);
                    cmd2.Parameters.AddWithValue("@nombreReal", eventdata.nombreReal);
                    cmd2.ExecuteNonQuery();
                    conn.Close();
                    return "guardado correcto";
                }
                else
                {
                    MySqlCommand cmd = new MySqlCommand("INSERT INTO Eventos (nombreReal,titulo,fechayhora,foto) VALUES (@nombreReal,@Titulo,@FechayHora,@Foto); SELECT LAST_INSERT_id();", conn);
                    cmd.Parameters.AddWithValue("@Titulo", titulo);
                    cmd.Parameters.AddWithValue("@FechayHora", fechayhora);
                    cmd.Parameters.AddWithValue("@Foto", linkImagen);
                    long idEvento = Convert.ToInt64(cmd.ExecuteScalar());
                    cmd2 = new MySqlCommand("UPDATE Participa SET participaEvento=true WHERE nombreDeCuenta=@NombreDeCuenta AND nombreReal=@nombreReal;", conn);
                    cmd2.Parameters.AddWithValue("@NombreDeCuenta", eventdata.user);
                    cmd2.Parameters.AddWithValue("@nombreReal", eventdata.nombreReal);
                    cmd2.ExecuteNonQuery();
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
                MySqlConnection conn = new MySqlConnection("Server=localhost; database=infini; uID=root; pwd=;");
                conn.Open();
                MySqlCommand command = new MySqlCommand("DELETE FROM Eventos WHERE idEvento=@Id", conn);
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
        public async Task<dynamic> conseguirEvento(int id)
        {
            try
            {
                MySqlConnection conn = new MySqlConnection("Server=localhost; database=infini; uID=root; pwd=;");
                conn.Open();
                MySqlCommand command = new MySqlCommand("SELECT nombreReal,titulo,ubicacion,descripcion,foto,fechaYHora FROM Eventos WHERE idEvento=@Id", conn);
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
                    string imagen;
                    if (string.IsNullOrEmpty(reader["foto"].ToString()))
                    {
                        imagen = "";
                    }
                    else
                    {
                        imagen = await CargarImagenDeGitHub(reader["foto"].ToString());
                    }
                    var data = new { nombreReal = reader["nombreReal"].ToString(), titulo = reader["titulo"].ToString(), ubicacion = ubicacion, descripcion = reader["descripcion"].ToString(), foto = imagen, fechayhora = reader["fechaYHora"].ToString() };
                    return Json(data);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        [HttpPut]
        [Route("modificarEvento")]
        public async Task<dynamic> modificarEvento([FromBody] EventData eventdata)
        {

            string id = eventdata.id;
            MySqlConnection conn = new MySqlConnection("Server=localhost; database=infini; uID=root; pwd=;");
            conn.Open();
            try
            {

                string linkImagen;
                linkImagen = await SubirImagenAGitHub(eventdata.imagen, "EventImages");
                string titulo = eventdata.titulo, fechayhora = eventdata.fechayhora, ubicacion = eventdata.ubicacion, descripcion = eventdata.descripcion;
                MySqlCommand cmd = new MySqlCommand("UPDATE Eventos SET ubicacion=@Ubicacion, titulo=@Titulo, descripcion=@Descripcion, foto=@Foto, fechaYHora=@FechayHora WHERE idEvento=@id", conn);
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
                    cmd.Parameters.AddWithValue("@Foto", linkImagen);
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

        //Comentarios

        [HttpPost]
        [Route("hacerComentario")]
        public dynamic hacerComentario([FromBody] CommentData commentdata)
        {
            MySqlConnection conn = new MySqlConnection("Server=localhost; database=infini; uID=root; pwd=;");
            conn.Open();
            MySqlCommand cmd;
            if (!string.IsNullOrEmpty(commentdata.NombreDeCuenta) &&
                !string.IsNullOrEmpty(commentdata.IdPost) && !string.IsNullOrEmpty(commentdata.texto) &&
                !string.IsNullOrEmpty(commentdata.fechayhora) &&
                !string.IsNullOrEmpty(commentdata.NombreCreador))
            {
                string NombreDeCuenta = commentdata.NombreDeCuenta;
                string IdPost = commentdata.IdPost;
                string texto = commentdata.texto;
                string fechayhora = commentdata.fechayhora;
                cmd = new MySqlCommand("INSERT INTO Comentarios (nombreDeCuenta, idPost, nombreCreador, texto, fechaYHora) VALUES (@NombreDeCuenta,@IdPost, @nombreCreador,@Texto,@FechayHora)", conn);
                cmd.Parameters.AddWithValue("@NombreDeCuenta", NombreDeCuenta);
                cmd.Parameters.AddWithValue("@nombreCreador", commentdata.NombreCreador);
                cmd.Parameters.AddWithValue("@IdPost", IdPost);
                cmd.Parameters.AddWithValue("@Texto", texto);
                cmd.Parameters.AddWithValue("@FechayHora", fechayhora);
                cmd.ExecuteNonQuery();
                conn.Close();
                return Json("guardado correcto");
            }
            else
            {
                return Json("guardado incorrecto");
            }
        }

        [HttpDelete]
        [Route("eliminarComentario")]
        public dynamic eliminarComentario(string id)
        {
            try
            {
                MySqlConnection conn = new MySqlConnection("Server=localhost; database=infini; uID=root; pwd=;");
                conn.Open();
                MySqlCommand command = new MySqlCommand("DELETE FROM Comentarios WHERE id=@Id", conn);
                command.Parameters.AddWithValue("@Id", int.Parse(id));
                command.ExecuteNonQuery();
                conn.Close();
                return Json("Comentario eliminado");
            }
            catch
            {
                return Json("Comentario no eliminado");
            }
        }

        [HttpPut]
        [Route("modificarComentario")]
        public dynamic modificarComentario([FromBody] CommentData commentdata)
        {
            string id = commentdata.id;
            try
            {
                MySqlConnection conn = new MySqlConnection("Server=localhost; database=infini; uID=root; pwd=;");
                conn.Open();
                MySqlCommand cmd;
                if (!string.IsNullOrEmpty(commentdata.texto))
                {
                    string texto = commentdata.texto;
                    cmd = new MySqlCommand("UPDATE Comentarios SET texto=@Texto WHERE id=@id", conn);
                    cmd.Parameters.AddWithValue("@Texto", texto);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    return "Modificaciòn correcta";
                }
                else
                {
                    return "Modificaciòn incorrecta";
                }
            }
            catch (Exception)
            {
                return "no se encuentra";
            }
        }
        [HttpGet]
        [Route("conseguirComentario")]
        public dynamic conseguirComentario(int id)
        {
            try
            {
                MySqlConnection conn = new MySqlConnection("Server=localhost; database=infini; uID=root; pwd=;");
                conn.Open();
                MySqlCommand command = new MySqlCommand("SELECT nombreDeCuenta, texto, fechaYHora FROM Comentarios WHERE id=@Id", conn);
                command.Parameters.AddWithValue("@Id", id);
                MySqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    string NombreDeCuenta;
                    NombreDeCuenta = reader["NombreDeCuenta"].ToString();

                    string texto;
                    texto = reader["texto"].ToString();

                    string fechayhora;
                    fechayhora = reader["fechaYHora"].ToString();

                    var data = new { NombreDeCuenta = NombreDeCuenta, texto = texto, fechayhora = fechayhora };
                    return Json(data);
                }
                else
                {
                    return "no se encuentra";
                }
            }
            catch (Exception)
            {
                return "no se encuentra";
            }

        }


        //Existe

        [HttpGet]
        [Route("existePost")]
        public dynamic existePost(int id)
        {
            MySqlConnection conn = new MySqlConnection("Server=localhost; database=infini; uID=root; pwd=;");
            conn.Open();
            MySqlCommand command = new MySqlCommand("SELECT idPost FROM Posts WHERE idPost=@Id", conn);
            command.Parameters.AddWithValue("@Id", id);
            MySqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                var data = true;
                if (reader["idPost"].ToString().Equals(Convert.ToString(id)))
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
        [Route("existeEvento")]
        public dynamic existeEvento(int id)
        {
            MySqlConnection conn = new MySqlConnection("Server=localhost; database=infini; uID=root; pwd=;");
            conn.Open();
            MySqlCommand command = new MySqlCommand("SELECT idEvento FROM Eventos WHERE idEvento=@Id", conn);
            command.Parameters.AddWithValue("@Id", id);
            MySqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                var data = true;
                if (reader["idEvento"].ToString().Equals(Convert.ToString(id)))
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
        [Route("existeComentario")]
        public dynamic existeComentario(int id)
        {
            MySqlConnection conn = new MySqlConnection("Server=localhost; database=infini; uID=root; pwd=;");
            conn.Open();
            MySqlCommand command = new MySqlCommand("SELECT id FROM Comentarios WHERE id=@Id", conn);
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

        //Seleccionar último
        [HttpGet]
        [Route("ultimoPost")]
        public dynamic ultimoPost()
        {
            try
            {
                MySqlConnection conn = new MySqlConnection("Server=localhost; database=infini; uID=root; pwd=;");
                conn.Open();
                MySqlCommand command = new MySqlCommand("SELECT idPost FROM Posts ORDER BY idPost DESC LIMIT 1", conn);
                MySqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    string id = reader["idPost"].ToString();
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
                MySqlConnection conn = new MySqlConnection("Server=localhost; database=infini; uID=root; pwd=;");
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT idEvento FROM Eventos ORDER BY idEvento DESC LIMIT 1", conn);
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    string id = reader["idEvento"].ToString();
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
        [Route("ultimoComentario")]
        public dynamic ultimoComentario()
        {
            try
            {
                MySqlConnection conn = new MySqlConnection("Server=localhost; database=infini; uID=root; pwd=;");
                conn.Open();
                MySqlCommand command = new MySqlCommand("SELECT id FROM Comentarios ORDER BY id DESC LIMIT 1", conn);
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

        //Seleccionar Todos

        [HttpGet]
        [Route("seleccionarTodosLosPost")]
        public dynamic seleccionarTodosLosPost()
        {
            try
            {
                string connectionString = "server = localhost; database = infini; uid = root; ";
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("SELECT idPost,texto FROM Posts", conn);
                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    return Json(dataTable);
                }
            }
            catch (Exception)
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
                string connectionString = "server = localhost; database = infini; uid = root; ";
                // Create a new MySQL connection
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("SELECT idEvento,titulo FROM Eventos", conn);
                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    return Json(dataTable);
                }
            }
            catch (Exception)
            {
                return "Error al cargar Datagrid";
            }
        }

        [HttpGet]
        [Route("seleccionarTodosLosComentarios")]
        public dynamic seleccionarTodosLosComentarios(int id)
        {
            try
            {
                string connectionString = "server = localhost; database = infini; uid = root; ";
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("SELECT id,texto,nombreCreador,fechaYHora FROM Comentarios WHERE idPost = @Id;", conn);
                    cmd.Parameters.AddWithValue("@Id", id);
                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    if (dataTable.Rows[0]["id"].ToString() != null)
                    {
                        return Json(dataTable);
                    }
                    else
                    {
                        return Json("error");
                    }
                }
            }
            catch (Exception)
            {
                return "Error al cargar Datagrid";
            }
        }

        //Reportes

        [HttpPost]
        [Route("ReportarPost")]
        public dynamic ReportarPost([FromBody] ReportePostOComentario reporte)
        {
            try
            {
                MySqlConnection conn = new MySqlConnection("Server=localhost; database=infini; uID=root; pwd=;");
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("INSERT INTO ReportePost (nombreDeCuenta,idPost,tipo, descripcion) VALUES (@Nombre, @id, @tipo, @descripcion)", conn);
                cmd.Parameters.AddWithValue("@nombre", reporte.usuario);
                cmd.Parameters.AddWithValue("@id", reporte.id);
                cmd.Parameters.AddWithValue("@tipo", reporte.tipo);
                cmd.Parameters.AddWithValue("@descripcion", reporte.descripcion);
                cmd.ExecuteNonQuery();
                conn.Close();
                return Json("Reporte correcto");
            }
            catch
            {
                return Json("Reporte incorrecto");
            }
        }

        [HttpPost]
        [Route("ReportarComentario")]
        public dynamic ReportarComentario([FromBody] ReportePostOComentario reporte)
        {
            try
            {
                MySqlConnection conn = new MySqlConnection("Server=localhost; database=infini; uID=root; pwd=;");
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("INSERT INTO ReporteComentario (nombreDeCuenta,idComentario, tipo, descripcion) VALUES (@Nombre, @id, @tipo, @descripcion)", conn);
                cmd.Parameters.AddWithValue("@nombre", reporte.usuario);
                cmd.Parameters.AddWithValue("@id", reporte.id);
                cmd.Parameters.AddWithValue("@tipo", reporte.tipo);
                cmd.Parameters.AddWithValue("@descripcion", reporte.descripcion);
                cmd.ExecuteNonQuery();
                conn.Close();
                return Json("Reporte correcto");
            }
            catch
            {
                return Json("Reporte incorrecto");
            }
        }
    }
}