using Microsoft.IdentityModel.Tokens;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
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
            public string categoria { get; set; }
            public string fechayhora { get; set; }
            public string idEvento { get; set; }
            public string nombreReal { get; set; }
            public string token { get; set; }

        }
        public class EventData
        {
            public string id { get; set; }
            public string titulo { get; set; } = "";
            public string ubicacion { get; set; } = "";
            public string descripcion { get; set; } = "";
            public string foto { get; set; } = "";
            public string fechaYhora_Inicio { get; set; } = "";
            public string fechaYhora_Final { get; set; } = "";
            public string user { get; set; }
            public string rol { get; set; } = "";
            public string token { get; set; }

        }
        public class CommentData
        {
            public string id { get; set; }

            public string NombreDeCuenta { get; set; }
            public string NombreCreador { get; set; }

            public string IdPost { get; set; }

            public string texto { get; set; }

            public string fechayhora { get; set; }
            public string token { get; set; }
        }
        public class ReportePostOComentario
        {
            public string usuario { get; set; }
            public int id { get; set; }
            public string tipo { get; set; }
            public string descripcion { get; set; }
            public string token { get; set; }
        }
        public class like
        {
            public string nombreDeCuenta { get; set; }
            public int idpost { get; set; }
            public string nombredeCreador { get; set; }
            public string token { get; set; }
        }
        public async Task<string> SubirImagenAGitHub(string imagen, string carpeta)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    string token = "11BKZVKOQ0So4CaeLdQqb2_s0qMD7Vd1EfzNiaVOyOKUE1KcekMlAPu94OKE3lQB9B7RTYHU6D0rP81rSy"; // Token para repositorio privado. Cambiar por el token real
                    string nombreDeImagen = GenerarIdAleatorio(8) + ".png"; // nombre aleatorio para que el nombre del archivo no se repita 

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
                string token = "11BKZVKOQ0So4CaeLdQqb2_s0qMD7Vd1EfzNiaVOyOKUE1KcekMlAPu94OKE3lQB9B7RTYHU6D0rP81rSy"; // Token para repositorio privado. Cambiar por el token real
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

        public bool TestToken(string pedido)
        {
            try
            {
                if (pedido.Equals("TestToken")) //esto lo hacemos pq no permite crear tokens de prueba en esta API, el método test token solo funciona con tokens creados en API Usuarios
                {
                    return true;
                }
                else
                {
                    var tokenHandler = new JwtSecurityTokenHandler();
                    SecurityToken securityToken;
                    var comprobar = tokenHandler.ValidateToken(pedido, ParametrosDeValidacionDelToken(), out securityToken);
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        private TokenValidationParameters ParametrosDeValidacionDelToken()
        {
            return new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidIssuer = "InfiniSV",
                ValidAudience = "usuario",
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("af431f66a2b44ddf1c8ee210f366d921"))
            };
        }

        [HttpPost]
        [Route("postear")]
        public async Task<dynamic> HacerPost([FromBody] PostData postdata)
        {
            if (TestToken(postdata.token))
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
                            using (MySqlCommand cmd = new MySqlCommand("INSERT INTO Posts (nombreDeCuenta, texto, video, fechaYhora, categoria) VALUES (@NombreDeCuenta, @Texto, @url, @fechaYhora, @categoria); SELECT LAST_INSERT_id();", conn))
                            {
                                cmd.Parameters.AddWithValue("@NombreDeCuenta", postdata.user);
                                cmd.Parameters.AddWithValue("@Texto", texto);
                                cmd.Parameters.AddWithValue("@url", url);
                                cmd.Parameters.AddWithValue("@fechaYhora", postdata.fechayhora);
                                cmd.Parameters.AddWithValue("@categoria", postdata.categoria);
                                idPost = Convert.ToInt32(await cmd.ExecuteScalarAsync());
                            }

                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(postdata.image))
                            {
                                linkImagen = await SubirImagenAGitHub(postdata.image, "PostImages");

                                using (MySqlCommand cmd = new MySqlCommand("INSERT INTO Posts (nombreDeCuenta, texto, imagen, fechaYhora, categoria) VALUES (@NombreDeCuenta, @Texto, @Imagen, @fechaYhora, @categoria); SELECT LAST_INSERT_id();", conn))
                                {
                                    cmd.Parameters.AddWithValue("@NombreDeCuenta", postdata.user);
                                    cmd.Parameters.AddWithValue("@Texto", texto);
                                    cmd.Parameters.AddWithValue("@Imagen", linkImagen);
                                    cmd.Parameters.AddWithValue("@fechaYhora", postdata.fechayhora);
                                    cmd.Parameters.AddWithValue("@categoria", postdata.categoria);
                                    idPost = Convert.ToInt32(await cmd.ExecuteScalarAsync());
                                }
                            }
                            else
                            {
                                using (MySqlCommand cmd = new MySqlCommand("INSERT INTO Posts (nombreDeCuenta, texto, fechaYhora, categoria) VALUES (@NombreDeCuenta, @Texto, @fechaYhora, @categoria); SELECT LAST_INSERT_id();", conn))
                                {
                                    cmd.Parameters.AddWithValue("@NombreDeCuenta", postdata.user);
                                    cmd.Parameters.AddWithValue("@Texto", texto);
                                    cmd.Parameters.AddWithValue("@fechaYhora", postdata.fechayhora);
                                    cmd.Parameters.AddWithValue("@categoria", postdata.categoria);
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

                            using (MySqlCommand cmd = new MySqlCommand("INSERT INTO Posts (nombreDeCuenta, imagen, fechaYhora, categoria) VALUES (@NombreDeCuenta, @Imagen, @fechaYhora, @categoria); SELECT LAST_INSERT_id();", conn))
                            {
                                cmd.Parameters.AddWithValue("@NombreDeCuenta", postdata.user);
                                cmd.Parameters.AddWithValue("@Imagen", linkImagen);
                                cmd.Parameters.AddWithValue("@fechaYhora", postdata.fechayhora);
                                cmd.Parameters.AddWithValue("@categoria", postdata.categoria);
                                idPost = Convert.ToInt32(await cmd.ExecuteScalarAsync());
                            }
                        }
                        else
                        {
                            string url = postdata.link;
                            using (MySqlCommand cmd = new MySqlCommand("INSERT INTO Posts (nombreDeCuenta, video, fechaYhora, categoria) VALUES (@NombreDeCuenta, @url, @fechaYhora, @categoria); SELECT LAST_INSERT_id();", conn))
                            {
                                cmd.Parameters.AddWithValue("@NombreDeCuenta", postdata.user);
                                cmd.Parameters.AddWithValue("@url", url);
                                cmd.Parameters.AddWithValue("@fechaYhora", postdata.fechayhora);
                                cmd.Parameters.AddWithValue("@categoria", postdata.categoria);
                                idPost = Convert.ToInt32(await cmd.ExecuteScalarAsync());
                            }
                        }
                    }


                    if (!string.IsNullOrEmpty(postdata.idEvento))
                    {
                        using (MySqlCommand cmd = new MySqlCommand("INSERT INTO PostEvento (idPost, nombreDeCuenta, idEvento) VALUES (@idPost,@NombreDeCuenta,@idEvento); SELECT LAST_INSERT_id();", conn))
                        {
                            cmd.Parameters.AddWithValue("@NombreDeCuenta", postdata.user);
                            cmd.Parameters.AddWithValue("@idPost", idPost);
                            cmd.Parameters.AddWithValue("@idEvento", postdata.idEvento);
                            cmd.ExecuteNonQuery();
                        }
                    }
                    if (!string.IsNullOrEmpty(postdata.nombreReal))
                    {
                        using (MySqlCommand cmd = new MySqlCommand("INSERT INTO PostGrupo (idPost, nombreDeCuenta, nombreReal) VALUES (@idPost,@NombreDeCuenta,@nombreReal); SELECT LAST_INSERT_id();", conn))
                        {
                            cmd.Parameters.AddWithValue("@NombreDeCuenta", postdata.user);
                            cmd.Parameters.AddWithValue("@idPost", idPost);
                            cmd.Parameters.AddWithValue("@nombreReal", postdata.nombreReal);
                            cmd.ExecuteNonQuery();
                        }
                    }

                    if (string.IsNullOrEmpty(postdata.idEvento) && string.IsNullOrEmpty(postdata.nombreReal))
                    {
                        using (MySqlCommand cmd2 = new MySqlCommand("INSERT INTO PostPublico (nombreDeCuenta, idPost) VALUES (@NombreDeCuenta, @id)", conn))
                        {
                            cmd2.Parameters.AddWithValue("@NombreDeCuenta", postdata.user);
                            cmd2.Parameters.AddWithValue("@id", idPost);
                            await cmd2.ExecuteNonQueryAsync();
                        }
                    }
                    conn.Close();
                    return Json("Guardado correcto");
                }
            }
            else
            {
                return Json("Token expirado");
            }
        }

        [HttpPut]
        [Route("postPorId")]
        public async Task<dynamic> conseguirPost([FromBody] PostData post)
        {
            if (TestToken(post.token))
            {
                try
                {
                    MySqlConnection conn = new MySqlConnection("Server=localhost; database=infini; uID=root; pwd=;");
                    conn.Open();
                    MySqlCommand command = new MySqlCommand("SELECT texto,imagen,video,fechaYhora FROM Posts WHERE idPost=@Id", conn);
                    command.Parameters.AddWithValue("@Id", post.id);
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
                        var data = new PostData { text = texto, image = imagen, link = url, fechayhora = fechaYhora };
                        conn.Close();
                        return Json(data);
                    }
                    else
                    {
                        conn.Close();
                        return Json("no se encuentra");

                    }
                }
                catch (Exception)
                {
                    return Json("no se encuentra");
                }
            }
            else
            {
                return Json("Token expirado");
            }
        }

        [HttpPut]
        [Route("conseguirCreador")]
        public dynamic conseguirCreador([FromBody] PostData post)
        {
            if (TestToken(post.token))
            {
                try
                {
                    MySqlConnection conn = new MySqlConnection("Server=localhost; database=infini; uID=root; pwd=;");
                    conn.Open();
                    MySqlCommand command = new MySqlCommand("SELECT nombreDeCuenta FROM Posts WHERE idPost=@Id", conn);
                    command.Parameters.AddWithValue("@Id", post.id);
                    MySqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        string nombreDeCuenta;
                        if (string.IsNullOrEmpty(reader["nombreDeCuenta"].ToString()))
                        {
                            nombreDeCuenta = "";
                            conn.Close();
                            return Json(nombreDeCuenta);
                        }
                        else
                        {
                            nombreDeCuenta = reader["nombreDeCuenta"].ToString();
                            conn.Close();
                            return Json(nombreDeCuenta);
                        }
                    }
                    else
                    {
                        conn.Close();
                        return Json($"no se encuentra");
                    }
                }
                catch
                {
                    return Json("no se encuentra");
                }
            }
            else
            {
                return Json("Token expirado");
            }
        }

        [HttpPut]
        [Route("eliminarPost")]
        public dynamic eliminarPost([FromBody] PostData post)
        {
            if (TestToken(post.token))
            {
                try
                {
                    MySqlConnection conn = new MySqlConnection("Server=localhost; database=infini; uID=root; pwd=;");
                    conn.Open();
                    MySqlCommand command = new MySqlCommand("DELETE FROM Comentarios WHERE idPost=@Id", conn);
                    MySqlCommand command2 = new MySqlCommand("DELETE FROM DaLike WHERE idPost = @Id", conn);
                    MySqlCommand command3 = new MySqlCommand("DELETE FROM PostPublico WHERE idPost = @Id", conn);
                    MySqlCommand command4 = new MySqlCommand("DELETE FROM PostGrupo WHERE idPost = @Id", conn);
                    MySqlCommand command5 = new MySqlCommand("DELETE FROM PostEvento WHERE idPost = @Id", conn);
                    MySqlCommand command6 = new MySqlCommand("DELETE FROM Posts WHERE idPost = @Id", conn);
                    MySqlCommand command7 = new MySqlCommand("DELETE FROM DaLikeComentario WHERE idComentario=(SELECT id FROM Comentarios WHERE idPost=@id)", conn);
                    command.Parameters.AddWithValue("@Id", post.id);
                    command2.Parameters.AddWithValue("@Id", post.id);
                    command3.Parameters.AddWithValue("@Id", post.id);
                    command4.Parameters.AddWithValue("@Id", post.id);
                    command5.Parameters.AddWithValue("@Id", post.id);
                    command6.Parameters.AddWithValue("@Id", post.id);
                    command7.Parameters.AddWithValue("@Id", post.id);
                    command7.ExecuteNonQuery();
                    command.ExecuteNonQuery();
                    command2.ExecuteNonQuery();
                    command3.ExecuteNonQuery();
                    command4.ExecuteNonQuery();
                    command5.ExecuteNonQuery();
                    command6.ExecuteNonQuery();
                    conn.Close();
                    return Json("Post eliminado");
                }
                catch
                {
                    return Json("Post no eliminado");
                }
            }
            else
            {
                return Json("Token expirado");
            }
        }

        [HttpPut]
        [Route("modificarPost")]
        public async Task<dynamic> modificarPost([FromBody] PostData postdata)
        {
            if (TestToken(postdata.token))
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
                            cmd = new MySqlCommand("UPDATE Posts SET texto=@texto,imagen=@imagen,video=@url WHERE idPost=@id", conn);
                            cmd.Parameters.AddWithValue("@Texto", texto);
                            cmd.Parameters.AddWithValue("@url", url);
                            cmd.Parameters.AddWithValue("@id", id);
                            cmd.Parameters.AddWithValue("@Imagen", null);
                            cmd.ExecuteNonQuery();
                            conn.Close();
                            return Json("Modificacion correcta");
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
                                return Json("Modificacion correcta");
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
                                return Json("Modificacion correcta");
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
                            return Json("Modificacion correcta");
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
                            return Json("Modificacion correcta");
                        }
                    }
                }
                catch (Exception)
                {
                    return Json("Modificacion incorrecta");
                }
            }
            else
            {
                return Json("Token expirado");
            }
        }

        [HttpPost]
        [Route("hacerEvento")]
        public async Task<dynamic> hacerEvento([FromBody] EventData eventdata)
        {
            if (TestToken(eventdata.token))
            {
                try
                {
                    string titulo = eventdata.titulo;
                    string fechaYhoraInicio = eventdata.fechaYhora_Inicio;
                    string fechaYhoraFinal = eventdata.fechaYhora_Final;
                    string linkImagen;
                    linkImagen = await SubirImagenAGitHub(eventdata.foto, "EventImages");
                    MySqlConnection conn = new MySqlConnection("Server=localhost; database=infini; uID=root; pwd=;");
                    conn.Open();
                    MySqlCommand cmd2;
                    if (string.IsNullOrEmpty(eventdata.user))
                    {
                        throw new Exception("ERROR: El campo 'user' no puede ser nulo");
                    }
                    if (!string.IsNullOrEmpty(eventdata.ubicacion))
                    {
                        if (!string.IsNullOrEmpty(eventdata.descripcion))
                        {
                            string ubicacion = eventdata.ubicacion;
                            string descripcion = eventdata.descripcion;
                            MySqlCommand cmd = new MySqlCommand("INSERT INTO Eventos (titulo,ubicacion,descripcion,fechaYhora_Inicio,fechaYhora_Final,foto) VALUES (@Titulo,@Ubicacion,@Descripcion,@FechayHoraInicio,@FechayHoraFinal,@Foto); SELECT LAST_INSERT_id();", conn);
                            cmd.Parameters.AddWithValue("@Titulo", titulo);
                            cmd.Parameters.AddWithValue("@Ubicacion", ubicacion);
                            cmd.Parameters.AddWithValue("@Descripcion", descripcion);
                            cmd.Parameters.AddWithValue("@FechayHoraInicio", fechaYhoraInicio);
                            cmd.Parameters.AddWithValue("@FechayHoraFinal", fechaYhoraFinal);
                            cmd.Parameters.AddWithValue("@Foto", linkImagen);
                            long idEvento = Convert.ToInt64(cmd.ExecuteScalar());
                            cmd2 = new MySqlCommand(@"INSERT INTO ParticipaEvento (nombreDeCuenta, idEvento, rol) VALUES (@NombreDeCuenta, @IdEvento, 'creador');", conn);
                            cmd2.Parameters.AddWithValue("@NombreDeCuenta", eventdata.user);
                            cmd2.Parameters.AddWithValue("@IdEvento", idEvento);
                            cmd2.ExecuteNonQuery();
                            conn.Close();
                            return Json("guardado correcto");
                        }
                        else
                        {
                            string ubicacion = eventdata.ubicacion;
                            MySqlCommand cmd = new MySqlCommand("INSERT INTO Eventos (titulo,ubicacion,fechaYhora_Inicio,fechaYhora_Final,foto) VALUES (@Titulo,@Ubicacion,@FechayHoraInicio,@FechayHoraFinal,@Foto); SELECT LAST_INSERT_id();", conn);
                            cmd.Parameters.AddWithValue("@Titulo", titulo);
                            cmd.Parameters.AddWithValue("@Ubicacion", ubicacion);
                            cmd.Parameters.AddWithValue("@FechayHoraInicio", fechaYhoraInicio);
                            cmd.Parameters.AddWithValue("@FechayHoraFinal", fechaYhoraFinal);
                            cmd.Parameters.AddWithValue("@Foto", linkImagen);
                            long idEvento = Convert.ToInt64(cmd.ExecuteScalar());
                            cmd2 = new MySqlCommand(@"INSERT INTO ParticipaEvento (nombreDeCuenta, idEvento, rol) VALUES (@NombreDeCuenta, @IdEvento, 'creador');", conn);
                            cmd2.Parameters.AddWithValue("@NombreDeCuenta", eventdata.user);
                            cmd2.Parameters.AddWithValue("@IdEvento", idEvento);
                            cmd2.ExecuteNonQuery();
                            conn.Close();
                            return Json("guardado correcto");
                        }
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(eventdata.descripcion))
                        {
                            string descripcion = eventdata.descripcion;
                            MySqlCommand cmd = new MySqlCommand("INSERT INTO Eventos (titulo,descripcion,fechaYhora_Inicio,fechaYhora_Final,foto) VALUES (@Titulo,@Descripcion,@FechayHoraInicio,@FechayHoraFinal,@Foto); SELECT LAST_INSERT_id();", conn);
                            cmd.Parameters.AddWithValue("@Titulo", titulo);
                            cmd.Parameters.AddWithValue("@Descripcion", descripcion);
                            cmd.Parameters.AddWithValue("@FechayHoraInicio", fechaYhoraInicio);
                            cmd.Parameters.AddWithValue("@FechayHoraFinal", fechaYhoraFinal);
                            cmd.Parameters.AddWithValue("@Foto", linkImagen);
                            long idEvento = Convert.ToInt64(cmd.ExecuteScalar());
                            cmd2 = new MySqlCommand(@"INSERT INTO ParticipaEvento (nombreDeCuenta, idEvento, rol) VALUES (@NombreDeCuenta, @IdEvento, 'creador');", conn);
                            cmd2.Parameters.AddWithValue("@NombreDeCuenta", eventdata.user);
                            cmd2.Parameters.AddWithValue("@IdEvento", idEvento);
                            cmd2.ExecuteNonQuery();
                            conn.Close();
                            return Json("guardado correcto");
                        }
                        else
                        {
                            string descripcion = eventdata.descripcion;
                            MySqlCommand cmd = new MySqlCommand("INSERT INTO Eventos (titulo,fechaYhora_Inicio,fechaYhora_Final,foto) VALUES (@Titulo,@FechayHoraInicio,@FechayHoraFinal,@Foto); SELECT LAST_INSERT_id();", conn);
                            cmd.Parameters.AddWithValue("@Titulo", titulo);
                            cmd.Parameters.AddWithValue("@FechayHoraInicio", fechaYhoraInicio);
                            cmd.Parameters.AddWithValue("@FechayHoraFinal", fechaYhoraFinal);
                            cmd.Parameters.AddWithValue("@Foto", linkImagen);
                            long idEvento = Convert.ToInt64(cmd.ExecuteScalar());
                            cmd2 = new MySqlCommand(@"INSERT INTO ParticipaEvento (nombreDeCuenta, idEvento, rol) VALUES (@NombreDeCuenta, @IdEvento, 'creador');", conn);
                            cmd2.Parameters.AddWithValue("@NombreDeCuenta", eventdata.user);
                            cmd2.Parameters.AddWithValue("@IdEvento", idEvento);
                            cmd2.ExecuteNonQuery();
                            conn.Close();
                            return Json("guardado correcto");
                        }
                    }
                    conn.Close();
                }
                catch
                {
                    return Json("guardado incorrecto");
                }
            }
            else
            {
                return Json("Token expirado");
            }

        }

        [HttpPut]
        [Route("eliminarEvento")]
        public dynamic eliminarEvento([FromBody] EventData eventdata)
        {
            if (TestToken(eventdata.token))
            {
                try
                {
                    MySqlConnection conn = new MySqlConnection("Server=localhost; database=infini; uID=root; pwd=;");
                    conn.Open();
                    MySqlCommand command = new MySqlCommand("DELETE FROM PostEvento WHERE idEvento=@Id", conn);
                    MySqlCommand command1 = new MySqlCommand("DELETE FROM Eventos WHERE idEvento=@Id", conn);
                    MySqlCommand command2 = new MySqlCommand("DELETE FROM ParticipaEvento WHERE idEvento=@Id", conn);
                    command2.Parameters.AddWithValue("@Id", int.Parse(eventdata.id));
                    command2.ExecuteNonQuery();
                    command.Parameters.AddWithValue("@Id", int.Parse(eventdata.id));
                    command.ExecuteNonQuery();
                    command1.Parameters.AddWithValue("@Id", int.Parse(eventdata.id));
                    command1.ExecuteNonQuery();
                    conn.Close();
                    return Json("Evento eliminado");
                }
                catch
                {
                    return Json("Evento no eliminado");
                }
            }
            else
            {
                return Json("Token expirado");
            }

        }

        [HttpPut]
        [Route("eventoPorId")]
        public async Task<dynamic> eventoPorId([FromBody] EventData eventData)
        {
            if (TestToken(eventData.token))
            {
                try
                {
                    MySqlConnection conn = new MySqlConnection("Server=localhost; database=infini; uID=root; pwd=;");
                    conn.Open();
                    MySqlCommand command = new MySqlCommand("SELECT idEvento,titulo,ubicacion,descripcion,foto,fechaYhora_Inicio,fechaYhora_Final FROM Eventos WHERE idEvento=@Id", conn);
                    command.Parameters.AddWithValue("@Id", eventData.id);
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
                        string foto;
                        if (string.IsNullOrEmpty(reader["foto"].ToString()))
                        {
                            foto = "";
                        }
                        else
                        {
                            foto = await CargarImagenDeGitHub(reader["foto"].ToString());
                        }
                        var data = new EventData { id= reader["idEvento"].ToString(), titulo = reader["titulo"].ToString(), ubicacion = ubicacion, descripcion = reader["descripcion"].ToString(), foto = foto, fechaYhora_Inicio = reader["fechaYhora_Inicio"].ToString(), fechaYhora_Final = reader["fechaYhora_Final"].ToString() };
                        conn.Close();
                        return Json(data);
                    }
                    else
                    {
                        conn.Close();
                        return Json("Error1");
                    }
                }
                catch (Exception ex)
                {
                    return Json("Error2"+ ex.Message);
                }
            }
            else
            {
                return Json("Token expirado");
            }

        }

        [HttpPost]
        [Route("participarDelEvento")]
        public dynamic participarDelEvento([FromBody] EventData eventData)
        {
            if (TestToken(eventData.token))
            {
                try
                {
                    MySqlConnection conn = new MySqlConnection("Server=localhost; database=infini; uID=root; pwd=;");
                    conn.Open();
                    MySqlCommand command = new MySqlCommand("INSERT INTO ParticipaEvento (nombreDeCuenta,idEvento,rol) VALUES (@nombreUsuario, @idEvento, @rol )", conn);
                    command.Parameters.AddWithValue("@nombreUsuario", eventData.user);
                    command.Parameters.AddWithValue("@idEvento", eventData.id);
                    command.Parameters.AddWithValue("@rol", eventData.rol);
                    command.ExecuteNonQuery();
                    conn.Close();
                    return Json("Participa del evento");
                }
                catch (Exception)
                {
                    return Json("Error");
                }
            }
            else
            {
                return Json("Token expirado");
            }

        }

        [HttpPut]
        [Route("eventoParticipa")]
        public dynamic eventoParticipa([FromBody] EventData eventData)
        {
            if (TestToken(eventData.token))
            {
                try
                {
                    MySqlConnection conn = new MySqlConnection("Server=localhost; database=infini; uID=root; pwd=;");
                    conn.Open();
                    MySqlCommand command = new MySqlCommand("SELECT e.idEvento FROM ParticipaEvento p JOIN Eventos e ON p.idEvento=e.idEvento WHERE p.nombreDeCuenta=@nombreUsuario", conn);
                    command.Parameters.AddWithValue("@nombreUsuario", eventData.user);
                    MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    conn.Close();
                    return Json(dataTable);
                }
                catch (Exception)
                {
                    return Json("Error");
                }
            }
            else
            {
                return Json("Token expirado");
            }

        }

        [HttpPut]
        [Route("modificarEvento")]
        public async Task<dynamic> modificarEvento([FromBody] EventData eventdata)
        {
            if (TestToken(eventdata.token))
            {
                string id = eventdata.id;
                MySqlConnection conn = new MySqlConnection("Server=localhost; database=infini; uID=root; pwd=;");
                conn.Open();
                try
                {

                    string linkImagen;
                    linkImagen = await SubirImagenAGitHub(eventdata.foto, "EventImages");
                    string titulo = eventdata.titulo, fechaYhoraInicio = eventdata.fechaYhora_Inicio, fechaYhoraFinal = eventdata.fechaYhora_Final, ubicacion = eventdata.ubicacion, descripcion = eventdata.descripcion;
                    MySqlCommand cmd = new MySqlCommand("UPDATE Eventos SET ubicacion=@Ubicacion, titulo=@Titulo, descripcion=@Descripcion, foto=@Foto, fechaYhora_Inicio=@FechayHoraInicio,fechaYhora_Final=@FechayHoraFinal WHERE idEvento=@id", conn);
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
                        return JsonConvert.SerializeObject("modificacion erronea");
                    }
                    if (!string.IsNullOrEmpty(eventdata.descripcion))
                    {
                        cmd.Parameters.AddWithValue("@Descripcion", descripcion);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@Descripcion", null);
                    }
                    if (!string.IsNullOrEmpty(eventdata.foto))
                    {
                        cmd.Parameters.AddWithValue("@Foto", linkImagen);
                    }
                    else
                    {
                        return Json("modificacion erronea");
                    }
                    if (!string.IsNullOrEmpty(eventdata.fechaYhora_Inicio))
                    {
                        cmd.Parameters.AddWithValue("@FechayHoraInicio", fechaYhoraInicio);
                    }
                    else
                    {
                        return Json("modificacion erronea");
                    }
                    if (!string.IsNullOrEmpty(eventdata.fechaYhora_Final))
                    {
                        cmd.Parameters.AddWithValue("@FechayHoraFinal", fechaYhoraInicio);
                    }
                    else
                    {
                        return Json("modificacion erronea");
                    }
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    return Json("modificacion correcta");

                }
                catch
                {
                    return Json("modificacion erronea");
                }
            }
            else
            {
                return Json("Token expirado");
            }

        }

        //Comentarios

        [HttpPost]
        [Route("hacerComentario")]
        public dynamic hacerComentario([FromBody] CommentData commentdata)
        {
            if (TestToken(commentdata.token))
            {
                try
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
                catch
                {
                    return Json("guardado incorrecto");
                }
            }
            else
            {
                return Json("Token expirado");
            }

        }

        [HttpPut]
        [Route("eliminarComentario")]
        public dynamic eliminarComentario([FromBody] PostData post)
        {
            if (TestToken(post.token))
            {
                try
                {
                    MySqlConnection conn = new MySqlConnection("Server=localhost; database=infini; uID=root; pwd=;");
                    conn.Open();
                    MySqlCommand command = new MySqlCommand("DELETE FROM Comentarios WHERE id=@Id", conn);
                    MySqlCommand command2 = new MySqlCommand("DELETE FROM DaLikeComentario WHERE idComentario=@Id", conn);
                    command.Parameters.AddWithValue("@Id", int.Parse(post.id));
                    command2.Parameters.AddWithValue("@Id", int.Parse(post.id));
                    command2.ExecuteNonQuery();
                    command.ExecuteNonQuery();
                    conn.Close();
                    return Json("Comentario eliminado");
                }
                catch
                {
                    return Json("Comentario no eliminado");
                }
            }
            else
            {
                return Json("Token expirado");
            }

        }

        [HttpPut]
        [Route("modificarComentario")]
        public dynamic modificarComentario([FromBody] CommentData commentdata)
        {
            if (TestToken(commentdata.token))
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
                        return Json("Modificaciòn correcta");
                    }
                    else
                    {
                        return Json("Modificaciòn incorrecta");
                    }
                }
                catch (Exception)
                {
                    return Json("no se encuentra");
                }
            }
            else
            {
                return Json("Token expirado");
            }

        }
        [HttpPut]
        [Route("conseguirComentario")]
        public dynamic conseguirComentario([FromBody] PostData post)
        {
            if (TestToken(post.token))
            {
                try
                {
                    MySqlConnection conn = new MySqlConnection("Server=localhost; database=infini; uID=root; pwd=;");
                    conn.Open();
                    MySqlCommand command = new MySqlCommand("SELECT nombreDeCuenta, texto, fechaYHora FROM Comentarios WHERE id=@Id", conn);
                    command.Parameters.AddWithValue("@Id", post.id);
                    MySqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        string NombreDeCuenta;
                        NombreDeCuenta = reader["NombreDeCuenta"].ToString();

                        string texto;
                        texto = reader["texto"].ToString();

                        string fechayhora;
                        fechayhora = reader["fechaYHora"].ToString();

                        var data = new CommentData { NombreDeCuenta = NombreDeCuenta, texto = texto, fechayhora = fechayhora };
                        conn.Close();
                        return Json(data);
                    }
                    else
                    {
                        conn.Close();
                        return Json("no se encuentra");
                    }
                }
                catch (Exception)
                {
                    return Json("no se encuentra");
                }
            }
            else
            {
                return Json("Token expirado");
            }
        }


        //Existe

        [HttpPut]
        [Route("existePost")]
        public dynamic existePost([FromBody] PostData post)
        {
            if (TestToken(post.token))
            {
                try
                {
                    MySqlConnection conn = new MySqlConnection("Server=localhost; database=infini; uID=root; pwd=;");
                    conn.Open();
                    MySqlCommand command = new MySqlCommand("SELECT idPost FROM Posts WHERE idPost=@Id", conn);
                    command.Parameters.AddWithValue("@Id", post.id);
                    MySqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        var data = true;
                        if (reader["idPost"].ToString().Equals(Convert.ToString(post.id)))
                        {
                            data = true;
                            conn.Close();
                            return Json(data);
                        }
                        else
                        {
                            data = false;
                            conn.Close();
                            return Json(data);
                        }

                    }
                    else
                    {
                        conn.Close();
                        return Json("no se encuentra");
                    }
                }
                catch
                {
                    return Json("no se encuentra");
                }
            }
            else
            {
                return Json("Token expirado");
            }
        }

        [HttpPut]
        [Route("existeEvento")]
        public dynamic existeEvento([FromBody] EventData eventData)
        {
            if (TestToken(eventData.token))
            {
                try
                {
                    MySqlConnection conn = new MySqlConnection("Server=localhost; database=infini; uID=root; pwd=;");
                    conn.Open();
                    MySqlCommand command = new MySqlCommand("SELECT idEvento FROM Eventos WHERE idEvento=@Id", conn);
                    command.Parameters.AddWithValue("@Id", eventData.id);
                    MySqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        var data = true;
                        if (reader["idEvento"].ToString().Equals(Convert.ToString(eventData.id)))
                        {
                            data = true;
                            conn.Close();
                            return Json(data);
                        }
                        else
                        {
                            data = false;
                            conn.Close();
                            return Json(data);
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
            else
            {
                return Json("Token expirado");
            }
        }

        [HttpPut]
        [Route("existeComentario")]
        public dynamic existeComentario([FromBody] PostData post)
        {
            if (TestToken(post.token))
            {
                try
                {
                    MySqlConnection conn = new MySqlConnection("Server=localhost; database=infini; uID=root; pwd=;");
                    conn.Open();
                    MySqlCommand command = new MySqlCommand("SELECT id FROM Comentarios WHERE id=@Id", conn);
                    command.Parameters.AddWithValue("@Id", post.id);
                    MySqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        var data = true;
                        if (reader["id"].ToString().Equals(Convert.ToString(post.id)))
                        {
                            data = true;
                            conn.Close();
                            return Json(data);
                        }
                        else
                        {
                            data = false;
                            conn.Close();
                            return Json(data);
                        }

                    }
                    else
                    {
                        conn.Close();
                        return Json("no se encuentra");
                    }
                }
                catch
                {
                    return Json("no se encuentra");
                }
            }
            else
            {
                return Json("Token expirado");
            }
        }

        //Seleccionar último
        [HttpPut]
        [Route("ultimoPost")]
        public dynamic ultimoPost([FromBody] PostData post)
        {
            if (TestToken(post.token))
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
                        conn.Close();
                        return Json(id);
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
            else
            {
                return Json("Token expirado");
            }
        }


        [HttpPut]
        [Route("ultimoEvento")]
        public dynamic ultimoEvento([FromBody] EventData evento)
        {
            if (TestToken(evento.token))
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
                        conn.Close();
                        return Json(id);
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
            else
            {
                return Json("Token expirado");
            }
        }

        [HttpPut]
        [Route("ultimoComentario")]
        public dynamic ultimoComentario([FromBody] CommentData comment)
        {
            if (TestToken(comment.token))
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
                        conn.Close();
                        return Json(id);
                    }
                    else
                    {
                        conn.Close();
                        return Json("no se encuentra");
                    }
                }
                catch
                {
                    return null;
                }
            }
            else
            {
                return Json("Token expirado");
            }
        }

        //Seleccionar Todos

        [HttpPut]
        [Route("seleccionarTodosLosPost")]
        public dynamic seleccionarTodosLosPost([FromBody] PostData post)
        {
            if (TestToken(post.token))
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
                        conn.Close();
                        return Json(dataTable);
                    }
                }
                catch (Exception)
                {
                    return Json("Error al cargar Datagrid");
                }
            }
            else
            {
                return Json("Token expirado");
            }
        }

        [HttpPut]
        [Route("seleccionarTodosLosPostPublicos")]
        public dynamic seleccionarTodosLosPostPublicos([FromBody] PostData post)
        {
            if (TestToken(post.token))
            {
                try
                {
                    string connectionString = "server = localhost; database = infini; uid = root; ";
                    using (MySqlConnection conn = new MySqlConnection(connectionString))
                    {
                        conn.Open();
                        MySqlCommand cmd = new MySqlCommand("SELECT idPost FROM PostPublico", conn);
                        MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        conn.Close();
                        return Json(dataTable);
                    }
                }
                catch (Exception)
                {
                    return Json("Error al cargar Datagrid");
                }
            }
            else
            {
                return Json("Token expirado");
            }
        }

        [HttpPut]
        [Route("seleccionarTodosLosEventos")]
        public dynamic seleccionarTodosLosEventos([FromBody] EventData evento)
        {
            if (TestToken(evento.token))
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
                        conn.Close();
                        return Json(dataTable);
                    }
                }
                catch (Exception)
                {
                    return Json("Error al cargar Datagrid");
                }
            }
            else
            {
                return Json("Token expirado");
            }
        }

        [HttpPut]
        [Route("seleccionarTodosLosComentarios")]
        public dynamic seleccionarTodosLosComentarios([FromBody] PostData post)
        {
            if (TestToken(post.token))
            {
                try
                {
                    string connectionString = "server = localhost; database = infini; uid = root; ";
                    using (MySqlConnection conn = new MySqlConnection(connectionString))
                    {
                        conn.Open();
                        MySqlCommand cmd = new MySqlCommand("SELECT id,texto,nombreCreador,fechaYHora FROM Comentarios WHERE idPost = @Id;", conn);
                        cmd.Parameters.AddWithValue("@Id", post.id);
                        MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        if (dataTable.Rows[0]["id"].ToString() != null)
                        {
                            conn.Close();
                            return Json(dataTable);
                        }
                        else
                        {
                            conn.Close();
                            return Json("error");
                        }
                    }
                }
                catch (Exception)
                {
                    return Json("Error al cargar Datagrid");
                }
            }
            else
            {
                return Json("Token expirado");
            }

        }
        [HttpPut]
        [Route("seleccionarTodosLosPostDelUsuario")]
        public dynamic seleccionarTodosLosPostDelUsuario([FromBody] PostData post)
        {
            if (TestToken(post.token))
            {
                try
                {
                    string connectionString = "server = localhost; database = infini; uid = root; ";
                    using (MySqlConnection conn = new MySqlConnection(connectionString))
                    {
                        conn.Open();
                        MySqlCommand cmd = new MySqlCommand("SELECT idPost FROM Posts WHERE nombreDeCuenta=@nombreDeCuenta", conn);
                        cmd.Parameters.AddWithValue("@nombreDeCuenta", post.user);
                        MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        conn.Close();
                        return Json(dataTable);
                    }
                }
                catch (Exception)
                {
                    return Json("Error al cargar Datagrid");
                }
            }
            else
            {
                return Json("Token expirado");
            }

        }
        [HttpPut]
        [Route("seleccionarTodosLosPostDelEvento")]
        public dynamic seleccionarTodosLosPostDelEvento([FromBody] PostData post)
        {
            if (TestToken(post.token))
            {
                try
                {
                    string connectionString = "server = localhost; database = infini; uid = root; ";
                    using (MySqlConnection conn = new MySqlConnection(connectionString))
                    {
                        conn.Open();
                        MySqlCommand cmd = new MySqlCommand("SELECT idPost FROM infini.PostEvento WHERE idEvento=@idevento", conn);
                        cmd.Parameters.AddWithValue("@idevento", post.idEvento);
                        MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        conn.Close();
                        return Json(dataTable);
                    }
                }
                catch (Exception)
                {
                    return Json("Error al cargar Datagrid");
                }
            }
            else
            {
                return Json("Token expirado");
            }

        }

        [HttpPost]
        [Route("darLike")]
        public dynamic darLike([FromBody] like like)
        {
            if (TestToken(like.token))
            {
                try
                {
                    MySqlConnection conn = new MySqlConnection("Server=localhost; database=infini; uID=root; pwd=;");
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("INSERT INTO DaLike (nombreDeCuenta, idPost, nombredeCreador) VALUES (@nombreDeCuenta, @idPost, @nombredeCreador)", conn);
                    cmd.Parameters.AddWithValue("@nombreDeCuenta", like.nombreDeCuenta);
                    cmd.Parameters.AddWithValue("@idPost", like.idpost);
                    cmd.Parameters.AddWithValue("@nombredeCreador", like.nombredeCreador);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    return Json("like correcto");
                }
                catch
                {
                    return Json("like incorrecto");
                }
            }
            else
            {
                return Json("Token expirado");
            }

        }

        [HttpPut]
        [Route("dioLike")]
        public dynamic dioLike([FromBody] like like)
        {
            if (TestToken(like.token))
            {
                try
                {
                    MySqlConnection conn = new MySqlConnection("Server=localhost; database=infini; uID=root; pwd=;");
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("SELECT idPost FROM DaLike WHERE idPost=@idpost AND nombreDeCuenta=@nombreDeCuenta AND nombredeCreador=@nombredeCreador", conn);
                    cmd.Parameters.AddWithValue("@nombreDeCuenta", like.nombreDeCuenta);
                    cmd.Parameters.AddWithValue("@idPost", like.idpost);
                    cmd.Parameters.AddWithValue("@nombredeCreador", like.nombredeCreador);
                    MySqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        if (reader["idPost"].ToString().Equals(Convert.ToString(like.idpost)))
                        {
                            conn.Close();
                            return Json(true);
                        }
                        else
                        {
                            conn.Close();
                            return Json(false);
                        }
                    }
                    else
                    {
                        conn.Close();
                        return Json(false);
                    }
                }
                catch
                {
                    return Json(false);
                }
            }
            else
            {
                return Json("Token expirado");
            }

        }

        [HttpPut]
        [Route("quitarLike")]
        public dynamic quitarLike([FromBody] like like)
        {
            if (TestToken(like.token))
            {
                try
                {
                    MySqlConnection conn = new MySqlConnection("Server=localhost; database=infini; uID=root; pwd=;");
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("DELETE FROM DaLike WHERE idPost=@idpost AND nombreDeCuenta=@nombreDeCuenta AND nombredeCreador=@nombredeCreador", conn);
                    cmd.Parameters.AddWithValue("@nombreDeCuenta", like.nombreDeCuenta);
                    cmd.Parameters.AddWithValue("@idPost", like.idpost);
                    cmd.Parameters.AddWithValue("@nombredeCreador", like.nombredeCreador);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    return Json("like correcto");
                }
                catch
                {
                    return Json("like incorrecto" + like.nombreDeCuenta + like.nombredeCreador + like.idpost);
                }
            }
            else
            {
                return Json("Token expirado");
            }

        }
        [HttpPost]
        [Route("darLikeComentario")]
        public dynamic darLikeComentario([FromBody] like like)
        {
            if (TestToken(like.token))
            {
                try
                {
                    MySqlConnection conn = new MySqlConnection("Server=localhost; database=infini; uID=root; pwd=;");
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("INSERT INTO DaLikeComentario (nombreDeCuenta, idComentario, quienDaLike) VALUES (@nombreDeCuenta, @idComentario, @quienDaLike)", conn);
                    cmd.Parameters.AddWithValue("@nombreDeCuenta", like.nombreDeCuenta);
                    cmd.Parameters.AddWithValue("@idComentario", like.idpost);
                    cmd.Parameters.AddWithValue("@quienDaLike", like.nombredeCreador);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    return Json("like correcto");
                }
                catch
                {
                    return Json("like incorrecto");
                }
            }
            else
            {
                return Json("Token expirado");
            }

        }

        [HttpPut]
        [Route("dioLikeComentario")]
        public dynamic dioLikeComentario([FromBody] like like)
        {
            if (TestToken(like.token))
            {
                try
                {
                    MySqlConnection conn = new MySqlConnection("Server=localhost; database=infini; uID=root; pwd=;");
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("SELECT idComentario FROM DaLikeComentario WHERE idComentario=@idComentario AND nombreDeCuenta=@nombreDeCuenta AND quienDaLike=@quienDaLike", conn);
                    cmd.Parameters.AddWithValue("@nombreDeCuenta", like.nombreDeCuenta);
                    cmd.Parameters.AddWithValue("@idComentario", like.idpost);
                    cmd.Parameters.AddWithValue("@quienDaLike", like.nombredeCreador);
                    MySqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        if (reader["idComentario"].ToString().Equals(Convert.ToString(like.idpost)))
                        {
                            conn.Close();
                            return Json(true);
                        }
                        else
                        {
                            conn.Close();
                            return Json(false);
                        }
                    }
                    else
                    {
                        conn.Close();
                        return Json(false);
                    }
                }
                catch
                {
                    return Json(false);
                }
            }
            else
            {
                return Json("Token expirado");
            }

        }

        [HttpPut]
        [Route("quitarLikeComentario")]
        public dynamic quitarLikeComentario([FromBody] like like)
        {
            if (TestToken(like.token))
            {
                try
                {
                    MySqlConnection conn = new MySqlConnection("Server=localhost; database=infini; uID=root; pwd=;");
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("DELETE FROM DaLikeComentario WHERE idComentario=@idComentario AND nombreDeCuenta=@nombreDeCuenta AND quienDaLike=@quienDaLike", conn);
                    cmd.Parameters.AddWithValue("@nombreDeCuenta", like.nombreDeCuenta);
                    cmd.Parameters.AddWithValue("@idComentario", like.idpost);
                    cmd.Parameters.AddWithValue("@quienDaLike", like.nombredeCreador);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    return Json("like correcto");
                }
                catch
                {
                    return Json("like incorrecto" + like.nombreDeCuenta + like.nombredeCreador + like.idpost);
                }
            }
            else
            {
                return Json("Token expirado");
            }
        }

        [HttpPut]
        [Route("conseguirCreadorComentario")]
        public dynamic conseguirCreadorComentario([FromBody] PostData post)
        {
            if (TestToken(post.token))
            {
                try
                {
                    MySqlConnection conn = new MySqlConnection("Server=localhost; database=infini; uID=root; pwd=;");
                    conn.Open();
                    MySqlCommand command = new MySqlCommand("SELECT nombreDeCuenta FROM Comentarios WHERE id=@Id", conn);
                    command.Parameters.AddWithValue("@Id", post.id);
                    MySqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        string nombreDeCuenta;
                        if (string.IsNullOrEmpty(reader["nombreDeCuenta"].ToString()))
                        {
                            nombreDeCuenta = "";
                            conn.Close();
                            return Json(nombreDeCuenta);
                        }
                        else
                        {
                            nombreDeCuenta = reader["nombreDeCuenta"].ToString();
                            conn.Close();
                            return Json(nombreDeCuenta);
                        }
                    }
                    else
                    {
                        conn.Close();
                        return Json($"no se encuentra");
                    }
                }
                catch
                {
                    return Json("no se encuentra");
                }
            }
            else
            {
                return Json("Token expirado");
            }
        }

        [HttpPut]
        [Route("BuscarEventos")]
        public async Task<dynamic> BuscarEventos([FromBody] EventData evento)
        {
            try
            {
                if (TestToken(evento.token))
                {
                    MySqlConnection conn = new MySqlConnection("Server=localhost; database=infini; uID=root; pwd=;");
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("SELECT idEvento, titulo, foto FROM Eventos WHERE titulo LIKE CONCAT('%', @titulo, '%')", conn);
                    cmd.Parameters.AddWithValue("@titulo", evento.titulo);
                    MySqlDataReader reader = cmd.ExecuteReader();

                    List<dynamic> lista = new List<dynamic>();

                    while (reader.Read())
                    {
                        lista.Add(new
                        {
                            idEvento = reader["idEvento"].ToString(),
                            titulo = reader["titulo"].ToString(),
                            foto = await CargarImagenDeGitHub(reader["foto"].ToString())
                        });
                    }
                    conn.Close();
                    if (lista.Count > 0)
                    {
                        return Json(lista);
                    }
                    else
                    {
                        return Json("No se encontraron eventos cuyos nombres concuerden con los parámetros de búsqueda especificados");
                    }
                }
                else
                {
                    return Json("Token expirado");
                }
            }
            catch
            {
                return Json("Hubo un error");
            }
        }
    }
}