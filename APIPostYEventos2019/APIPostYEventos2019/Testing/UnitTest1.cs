using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Data;
using System.IO;
using System.Threading.Tasks;

namespace Testing
{
    [TestClass]
    public class UnitTest1
    {
        private string fechaHoraString;
        private APIPostYEventos2019.Controllers.PostController.PostData post;
        private int ultimopost;
        [TestMethod]
        public async Task TestMethod01()
        {
            string respuestaEsperada = "Guardado correcto";
            DateTime fechayhoraactual = DateTime.Now;
            fechaHoraString = fechayhoraactual.ToString("yyyy-MM-dd HH:mm:ss");
            post = new APIPostYEventos2019.Controllers.PostController.PostData
            {
                text = "hola",
                user = "juan123",
                fechayhora = fechaHoraString,
                token = "TestToken"
            };
            APIPostYEventos2019.Controllers.PostController controller = new APIPostYEventos2019.Controllers.PostController();
            var resultado = await controller.HacerPost(post);
            var jsonResult = resultado as System.Web.Http.Results.JsonResult<string>;
            string resultadoString = jsonResult.Content;
            Assert.AreEqual(respuestaEsperada, resultadoString);
        }

        [TestMethod]
        public async Task TestMethod02()
        {
            string respuestaEsperada = "hola";
            APIPostYEventos2019.Controllers.PostController controller = new APIPostYEventos2019.Controllers.PostController();
            APIPostYEventos2019.Controllers.PostController.PostData token = new APIPostYEventos2019.Controllers.PostController.PostData()
            {
                token = "TestToken"
            };
            var ultimopostLLamar = controller.ultimoPost(token);
            var jsonUltimoPost = ultimopostLLamar as System.Web.Http.Results.JsonResult<string>;
            ultimopost = int.Parse(jsonUltimoPost.Content);
            APIPostYEventos2019.Controllers.PostController.PostData postdata = new APIPostYEventos2019.Controllers.PostController.PostData()
            {
                id = Convert.ToString(ultimopost),
                token = "TestToken"
            };
            var resultado = await controller.conseguirPost(postdata);
            var jsonResult = resultado as System.Web.Http.Results.JsonResult<APIPostYEventos2019.Controllers.PostController.PostData>;
            dynamic data = jsonResult.Content;
            string resultadoString = data.text;
            Assert.AreEqual(respuestaEsperada, resultadoString);
        }

        [TestMethod]
        public async Task TestMethod03()
        {
            string respuestaEsperada = "juan123";
            APIPostYEventos2019.Controllers.PostController controller = new APIPostYEventos2019.Controllers.PostController();
            APIPostYEventos2019.Controllers.PostController.PostData token = new APIPostYEventos2019.Controllers.PostController.PostData()
            {
                token = "TestToken"
            };
            var ultimopostLLamar = controller.ultimoPost(token);
            var jsonUltimoPost = ultimopostLLamar as System.Web.Http.Results.JsonResult<string>;
            ultimopost = int.Parse(jsonUltimoPost.Content);
            APIPostYEventos2019.Controllers.PostController.PostData postdata = new APIPostYEventos2019.Controllers.PostController.PostData()
            {
                id = Convert.ToString(ultimopost),
                token = "TestToken"
            };
            var resultado = controller.conseguirCreador(postdata);
            var jsonResult = resultado as System.Web.Http.Results.JsonResult<string>;
            string resultadoString = jsonResult.Content;
            Assert.AreEqual(respuestaEsperada, resultadoString);
        }

        [TestMethod]
        public async Task TestMethod04()
        {
            string respuestaEsperada = "Modificacion correcta";
            APIPostYEventos2019.Controllers.PostController controller = new APIPostYEventos2019.Controllers.PostController();
            byte[] imagenfalsa = new byte[0];
            APIPostYEventos2019.Controllers.PostController.PostData token = new APIPostYEventos2019.Controllers.PostController.PostData()
            {
                token = "TestToken"
            };
            var ultimopostLLamar = controller.ultimoPost(token);
            var jsonUltimoPost = ultimopostLLamar as System.Web.Http.Results.JsonResult<string>;
            ultimopost = int.Parse(jsonUltimoPost.Content);
            APIPostYEventos2019.Controllers.PostController.PostData postdata = new APIPostYEventos2019.Controllers.PostController.PostData()
            {
                id = Convert.ToString(ultimopost),
                text = "chau",
                image = Convert.ToBase64String(imagenfalsa),
                link = "",
                token = "TestToken"
            };
            var resultado = await controller.modificarPost(postdata);
            var jsonResult = resultado as System.Web.Http.Results.JsonResult<string>;
            string resultadoString = jsonResult.Content;
            Assert.AreEqual(respuestaEsperada, resultadoString);
        }

        [TestMethod]
        public async Task TestMethod05()
        {
            string respuestaEsperada = "guardado correcto";
            DateTime fechayhoraactual = DateTime.Now;
            string fechaHoraFinalString = fechayhoraactual.ToString("yyyy-MM-dd HH:mm:ss");
            string projectDirectory = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.FullName;
            string imagePath = Path.Combine(projectDirectory, "Testing", "Imagen.jpg");
            byte[] imageBytes = File.ReadAllBytes(imagePath);
            string base64Image = Convert.ToBase64String(imageBytes);
            APIPostYEventos2019.Controllers.PostController controller = new APIPostYEventos2019.Controllers.PostController();
            APIPostYEventos2019.Controllers.PostController.EventData eventData = new APIPostYEventos2019.Controllers.PostController.EventData()
            {
                titulo = "evento",
                fechaYhora_Inicio = fechaHoraFinalString,
                fechaYhora_Final = fechaHoraFinalString,
                foto = base64Image,
                user = "juan123",
                token = "TestToken"
            };
            var resultado = await controller.hacerEvento(eventData);
            var jsonResult = resultado as System.Web.Http.Results.JsonResult<string>;
            string resultadoString = jsonResult.Content;
            Assert.AreEqual(respuestaEsperada, resultadoString);
        }

        [TestMethod]
        public async Task TestMethod06()
        {
            string respuestaEsperada = "modificacion correcta";
            DateTime fechayhoraactual = DateTime.Now;
            string fechaHoraFinalString = fechayhoraactual.ToString("yyyy-MM-dd HH:mm:ss");
            string projectDirectory = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.FullName;
            string imagePath = Path.Combine(projectDirectory, "Testing", "Imagen.jpg");
            byte[] imageBytes = File.ReadAllBytes(imagePath);
            string base64Image = Convert.ToBase64String(imageBytes);
            APIPostYEventos2019.Controllers.PostController controller = new APIPostYEventos2019.Controllers.PostController();
            APIPostYEventos2019.Controllers.PostController.EventData token = new APIPostYEventos2019.Controllers.PostController.EventData()
            {
                token = "TestToken"
            };
            var ultimoEventoLLamar = controller.ultimoEvento(token);
            var jsonUltimoEvento = ultimoEventoLLamar as System.Web.Http.Results.JsonResult<string>;
            int ultimoEvento = int.Parse(jsonUltimoEvento.Content);
            APIPostYEventos2019.Controllers.PostController.EventData eventData = new APIPostYEventos2019.Controllers.PostController.EventData()
            {
                id = Convert.ToString(ultimoEvento),
                titulo = "eventoModificado",
                fechaYhora_Inicio = fechaHoraFinalString,
                fechaYhora_Final = fechaHoraFinalString,
                foto = base64Image,
                user = "juan123",
                token = "TestToken"
            };
            var resultado = await controller.modificarEvento(eventData);
            var jsonResult = resultado as System.Web.Http.Results.JsonResult<string>;
            string resultadoString = jsonResult.Content;
            Assert.AreEqual(respuestaEsperada, resultadoString);
        }

        [TestMethod]
        public void TestMethod07()
        {
            string respuestaEsperada = "like correcto";
            APIPostYEventos2019.Controllers.PostController controller = new APIPostYEventos2019.Controllers.PostController();
            APIPostYEventos2019.Controllers.PostController.PostData token = new APIPostYEventos2019.Controllers.PostController.PostData()
            {
                token = "TestToken"
            };
            var ultimopostLLamar = controller.ultimoPost(token);
            var jsonUltimoPost = ultimopostLLamar as System.Web.Http.Results.JsonResult<string>;
            ultimopost = int.Parse(jsonUltimoPost.Content);
            APIPostYEventos2019.Controllers.PostController.like like = new APIPostYEventos2019.Controllers.PostController.like()
            {
                nombreDeCuenta = "juan123",
                idpost = ultimopost,
                nombredeCreador = "juan123",
                token = "TestToken"
            };
            var resultado = controller.darLike(like);
            var jsonResult = resultado as System.Web.Http.Results.JsonResult<string>;
            string resultadoString = jsonResult.Content;
            Assert.AreEqual(respuestaEsperada, resultadoString);
        }

        [TestMethod]
        public void TestMethod08()
        {
            APIPostYEventos2019.Controllers.PostController controller = new APIPostYEventos2019.Controllers.PostController();
            APIPostYEventos2019.Controllers.PostController.PostData token = new APIPostYEventos2019.Controllers.PostController.PostData()
            {
                token = "TestToken"
            };
            var ultimopostLLamar = controller.ultimoPost(token);
            var jsonUltimoPost = ultimopostLLamar as System.Web.Http.Results.JsonResult<string>;
            ultimopost = int.Parse(jsonUltimoPost.Content);
            APIPostYEventos2019.Controllers.PostController.like like = new APIPostYEventos2019.Controllers.PostController.like()
            {
                nombreDeCuenta = "juan123",
                idpost = ultimopost,
                nombredeCreador = "juan123",
                token = "TestToken"
            };
            var resultado = controller.dioLike(like);
            var jsonResult = resultado as System.Web.Http.Results.JsonResult<bool>;
            bool resultadoBool = jsonResult.Content;
            Assert.IsTrue(resultadoBool);
        }

        [TestMethod]
        public void TestMethod09()
        {
            string respuestaEsperada = "like correcto";
            APIPostYEventos2019.Controllers.PostController controller = new APIPostYEventos2019.Controllers.PostController();
            APIPostYEventos2019.Controllers.PostController.PostData token = new APIPostYEventos2019.Controllers.PostController.PostData()
            {
                token = "TestToken"
            };
            var ultimopostLLamar = controller.ultimoPost(token);
            var jsonUltimoPost = ultimopostLLamar as System.Web.Http.Results.JsonResult<string>;
            ultimopost = int.Parse(jsonUltimoPost.Content);
            APIPostYEventos2019.Controllers.PostController.like like = new APIPostYEventos2019.Controllers.PostController.like()
            {
                nombreDeCuenta = "juan123",
                idpost = ultimopost,
                nombredeCreador = "juan123",
                token = "TestToken"
            };
            var resultado = controller.quitarLike(like);
            var jsonResult = resultado as System.Web.Http.Results.JsonResult<string>;
            string resultadoString = jsonResult.Content;
            Assert.AreEqual(respuestaEsperada, resultadoString);
        }

        [TestMethod]
        public async Task TestMethod10()
        {
            string respuestaEsperada = "eventoModificado";
            APIPostYEventos2019.Controllers.PostController controller = new APIPostYEventos2019.Controllers.PostController();
            APIPostYEventos2019.Controllers.PostController.EventData token = new APIPostYEventos2019.Controllers.PostController.EventData()
            {
                token = "TestToken"
            };
            var ultimoEventoLLamar = controller.ultimoEvento(token);
            var jsonUltimoEvento = ultimoEventoLLamar as System.Web.Http.Results.JsonResult<string>;
            int ultimoEvento = int.Parse(jsonUltimoEvento.Content);
            APIPostYEventos2019.Controllers.PostController.EventData evento = new APIPostYEventos2019.Controllers.PostController.EventData()
            {
                id = Convert.ToString(ultimoEvento),
                token = "TestToken"
            };
            var resultado = await controller.eventoPorId(evento);
            var jsonResult = resultado as System.Web.Http.Results.JsonResult<APIPostYEventos2019.Controllers.PostController.EventData>;
            dynamic data = jsonResult.Content;
            string resultadoString = data.titulo;
            Assert.AreEqual(respuestaEsperada, resultadoString);
        }

        [TestMethod]
        public void TestMethod11()
        {
            string respuestaEsperada = "guardado correcto";
            APIPostYEventos2019.Controllers.PostController controller = new APIPostYEventos2019.Controllers.PostController();
            DateTime fechayhoraactual = DateTime.Now;
            string fechaHoraString = fechayhoraactual.ToString("yyyy-MM-dd HH:mm:ss");
            APIPostYEventos2019.Controllers.PostController.PostData token = new APIPostYEventos2019.Controllers.PostController.PostData()
            {
                token = "TestToken"
            };
            var ultimopostLLamar = controller.ultimoPost(token);
            var jsonUltimoPost = ultimopostLLamar as System.Web.Http.Results.JsonResult<string>;
            ultimopost = int.Parse(jsonUltimoPost.Content);
            APIPostYEventos2019.Controllers.PostController.CommentData comentario = new APIPostYEventos2019.Controllers.PostController.CommentData()
            {
                IdPost = Convert.ToString(ultimopost),
                fechayhora = fechaHoraString,
                NombreCreador = "juan123",
                NombreDeCuenta = "juan123",
                texto = "buen post",
                token = "TestToken"
            };
            var resultado = controller.hacerComentario(comentario);
            var jsonResult = resultado as System.Web.Http.Results.JsonResult<string>;
            string resultadoString = jsonResult.Content;
            Assert.AreEqual(respuestaEsperada, resultadoString);
        }

        [TestMethod]
        public void TestMethod12()
        {
            string respuestaEsperada = "Modificaciòn correcta";
            APIPostYEventos2019.Controllers.PostController controller = new APIPostYEventos2019.Controllers.PostController();
            DateTime fechayhoraactual = DateTime.Now;
            string fechaHoraString = fechayhoraactual.ToString("yyyy-MM-dd HH:mm:ss");
            APIPostYEventos2019.Controllers.PostController.CommentData token = new APIPostYEventos2019.Controllers.PostController.CommentData()
            {
                token = "TestToken"
            };
            var ultimoComentarioLLamar = controller.ultimoComentario(token);
            var jsonUltimoComentario = ultimoComentarioLLamar as System.Web.Http.Results.JsonResult<string>;
            int ultimoComentario = int.Parse(jsonUltimoComentario.Content);
            APIPostYEventos2019.Controllers.PostController.CommentData comentario = new APIPostYEventos2019.Controllers.PostController.CommentData()
            {
                id = Convert.ToString(ultimoComentario),
                texto = "mal post",
                token = "TestToken"
            };
            var resultado = controller.modificarComentario(comentario);
            var jsonResult = resultado as System.Web.Http.Results.JsonResult<string>;
            string resultadoString = jsonResult.Content;
            Assert.AreEqual(respuestaEsperada, resultadoString);
        }

        [TestMethod]
        public async Task TestMethod13()
        {
            string respuestaEsperada = "mal post";
            APIPostYEventos2019.Controllers.PostController controller = new APIPostYEventos2019.Controllers.PostController();
            APIPostYEventos2019.Controllers.PostController.CommentData token = new APIPostYEventos2019.Controllers.PostController.CommentData()
            {
                token = "TestToken"
            };
            var ultimoComentarioLLamar = controller.ultimoComentario(token);
            var jsonUltimoComentario = ultimoComentarioLLamar as System.Web.Http.Results.JsonResult<string>;
            int ultimoComentario = int.Parse(jsonUltimoComentario.Content);
            APIPostYEventos2019.Controllers.PostController.PostData comentario = new APIPostYEventos2019.Controllers.PostController.PostData()
            {
                id = Convert.ToString(ultimoComentario),
                token = "TestToken"
            };
            var resultado = controller.conseguirComentario(comentario);

            var jsonResult = resultado as System.Web.Http.Results.JsonResult<APIPostYEventos2019.Controllers.PostController.CommentData>;

            dynamic data = jsonResult.Content;
            string resultadoString = data.texto;
            Assert.AreEqual(respuestaEsperada, resultadoString);
        }

        [TestMethod]
        public void TestMethod14()
        {
            APIPostYEventos2019.Controllers.PostController controller = new APIPostYEventos2019.Controllers.PostController();
            APIPostYEventos2019.Controllers.PostController.PostData token = new APIPostYEventos2019.Controllers.PostController.PostData()
            {
                token = "TestToken"
            };
            var ultimopostLLamar = controller.ultimoPost(token);
            var jsonUltimoPost = ultimopostLLamar as System.Web.Http.Results.JsonResult<string>;
            ultimopost = int.Parse(jsonUltimoPost.Content);
            APIPostYEventos2019.Controllers.PostController.PostData post = new APIPostYEventos2019.Controllers.PostController.PostData()
            {
                id = Convert.ToString(ultimopost),
                token = "TestToken"
            };
            var resultado = controller.existePost(post);
            var jsonResult = resultado as System.Web.Http.Results.JsonResult<bool>;
            bool resultadoBool = jsonResult.Content;
            Assert.IsTrue(resultadoBool);
        }

        [TestMethod]
        public void TestMethod15()
        {
            APIPostYEventos2019.Controllers.PostController controller = new APIPostYEventos2019.Controllers.PostController();
            APIPostYEventos2019.Controllers.PostController.EventData token = new APIPostYEventos2019.Controllers.PostController.EventData()
            {
                token = "TestToken"
            };
            var ultimoEventoLlamar = controller.ultimoEvento(token);
            var jsonUltimoEvento = ultimoEventoLlamar as System.Web.Http.Results.JsonResult<string>;
            int ultimoEvento = int.Parse(jsonUltimoEvento.Content);
            APIPostYEventos2019.Controllers.PostController.EventData eventData = new APIPostYEventos2019.Controllers.PostController.EventData()
            {
                id = Convert.ToString(ultimoEvento),
                token = "TestToken"
            };
            var resultado = controller.existeEvento(eventData);
            var jsonResult = resultado as System.Web.Http.Results.JsonResult<bool>;
            bool resultadoBool = jsonResult.Content;
            Assert.IsTrue(resultadoBool);
        }

        [TestMethod]
        public void TestMethod16()
        {
            APIPostYEventos2019.Controllers.PostController controller = new APIPostYEventos2019.Controllers.PostController();
            APIPostYEventos2019.Controllers.PostController.CommentData token = new APIPostYEventos2019.Controllers.PostController.CommentData()
            {
                token = "TestToken"
            };
            var ultimoComentarioLLamar = controller.ultimoComentario(token);
            var jsonUltimoComentario = ultimoComentarioLLamar as System.Web.Http.Results.JsonResult<string>;
            int ultimoComentario = int.Parse(jsonUltimoComentario.Content);
            APIPostYEventos2019.Controllers.PostController.PostData post = new APIPostYEventos2019.Controllers.PostController.PostData()
            {
                id = Convert.ToString(ultimoComentario),
                token = "TestToken"
            };
            var resultado = controller.existeComentario(post);
            var jsonResult = resultado as System.Web.Http.Results.JsonResult<bool>;
            bool resultadoBool = jsonResult.Content;
            Assert.IsTrue(resultadoBool);
        }

        [TestMethod]
        public async Task TestMethod17()
        {
            string respuestaEsperada = "chau";
            APIPostYEventos2019.Controllers.PostController controller = new APIPostYEventos2019.Controllers.PostController();
            APIPostYEventos2019.Controllers.PostController.PostData token = new APIPostYEventos2019.Controllers.PostController.PostData()
            {
                token = "TestToken"
            };
            var resultado = controller.seleccionarTodosLosPost(token);
            var jsonResult = resultado as System.Web.Http.Results.JsonResult<DataTable>;
            dynamic data = jsonResult.Content;
            string resultadoString = data.Rows[data.Rows.Count - 1]["texto"];
            Assert.AreEqual(respuestaEsperada, resultadoString);
        }

        [TestMethod]
        public async Task TestMethod18()
        {
            string respuestaEsperada = "eventoModificado";
            APIPostYEventos2019.Controllers.PostController controller = new APIPostYEventos2019.Controllers.PostController();
            APIPostYEventos2019.Controllers.PostController.EventData token = new APIPostYEventos2019.Controllers.PostController.EventData()
            {
                token = "TestToken"
            };
            var resultado = controller.seleccionarTodosLosEventos(token);
            var jsonResult = resultado as System.Web.Http.Results.JsonResult<DataTable>;
            dynamic data = jsonResult.Content;
            string resultadoString = data.Rows[data.Rows.Count - 1]["titulo"];
            Assert.AreEqual(respuestaEsperada, resultadoString);
        }

        [TestMethod]
        public async Task TestMethod19()
        {
            string respuestaEsperada = "mal post";
            APIPostYEventos2019.Controllers.PostController controller = new APIPostYEventos2019.Controllers.PostController();
            APIPostYEventos2019.Controllers.PostController.PostData token = new APIPostYEventos2019.Controllers.PostController.PostData()
            {
                token = "TestToken"
            };
            var ultimopostLLamar = controller.ultimoPost(token);
            var jsonUltimoPost = ultimopostLLamar as System.Web.Http.Results.JsonResult<string>;
            ultimopost = int.Parse(jsonUltimoPost.Content);
            APIPostYEventos2019.Controllers.PostController.PostData postData = new APIPostYEventos2019.Controllers.PostController.PostData()
            {
                id = Convert.ToString(ultimopost),
                token = "TestToken"
            };
            var resultado = controller.seleccionarTodosLosComentarios(postData);
            var jsonResult = resultado as System.Web.Http.Results.JsonResult<DataTable>;
            dynamic data = jsonResult.Content;
            string resultadoString = data.Rows[data.Rows.Count - 1]["texto"];
            Assert.AreEqual(respuestaEsperada, resultadoString);
        }

        [TestMethod]
        public async Task TestMethod20()
        {
            APIPostYEventos2019.Controllers.PostController controller = new APIPostYEventos2019.Controllers.PostController();
            APIPostYEventos2019.Controllers.PostController.PostData token = new APIPostYEventos2019.Controllers.PostController.PostData()
            {
                token = "TestToken"
            };
            var ultimopostLLamar = controller.ultimoPost(token);
            var jsonUltimoPost = ultimopostLLamar as System.Web.Http.Results.JsonResult<string>;
            string ultimoPost = jsonUltimoPost.Content;
            string respuestaEsperada = ultimoPost;
            APIPostYEventos2019.Controllers.PostController.PostData postData = new APIPostYEventos2019.Controllers.PostController.PostData()
            {
                user = "juan123",
                token = "TestToken"
            };
            var resultado = controller.seleccionarTodosLosPostDelUsuario(postData);
            var jsonResult = resultado as System.Web.Http.Results.JsonResult<DataTable>;
            dynamic data = jsonResult.Content;
            string resultadoString = Convert.ToString(data.Rows[data.Rows.Count - 1]["idPost"]);
            Assert.AreEqual(respuestaEsperada, resultadoString);
        }

        [TestMethod]
        public void TestMethod21()
        {
            string respuestaEsperada = "like correcto";
            APIPostYEventos2019.Controllers.PostController controller = new APIPostYEventos2019.Controllers.PostController();
            APIPostYEventos2019.Controllers.PostController.CommentData token = new APIPostYEventos2019.Controllers.PostController.CommentData()
            {
                token = "TestToken"
            };
            var ultimoComentarioLLamar = controller.ultimoComentario(token);
            var jsonUltimoComentario = ultimoComentarioLLamar as System.Web.Http.Results.JsonResult<string>;
            int ultimoComentario = int.Parse(jsonUltimoComentario.Content);
            APIPostYEventos2019.Controllers.PostController.like like = new APIPostYEventos2019.Controllers.PostController.like()
            {
                nombreDeCuenta = "juan123",
                idpost = ultimoComentario,
                nombredeCreador = "juan123",
                token = "TestToken"
            };
            var resultado = controller.darLikeComentario(like);
            var jsonResult = resultado as System.Web.Http.Results.JsonResult<string>;
            string resultadoString = jsonResult.Content;
            Assert.AreEqual(respuestaEsperada, resultadoString);
        }

        [TestMethod]
        public void TestMethod22()
        {
            APIPostYEventos2019.Controllers.PostController controller = new APIPostYEventos2019.Controllers.PostController();
            APIPostYEventos2019.Controllers.PostController.CommentData token = new APIPostYEventos2019.Controllers.PostController.CommentData()
            {
                token = "TestToken"
            };
            var ultimoComentarioLLamar = controller.ultimoComentario(token);
            var jsonUltimoComentario = ultimoComentarioLLamar as System.Web.Http.Results.JsonResult<string>;
            int ultimoComentario = int.Parse(jsonUltimoComentario.Content);
            APIPostYEventos2019.Controllers.PostController.like like = new APIPostYEventos2019.Controllers.PostController.like()
            {
                nombreDeCuenta = "juan123",
                idpost = ultimoComentario,
                nombredeCreador = "juan123",
                token = "TestToken"
            };
            var resultado = controller.dioLikeComentario(like);
            var jsonResult = resultado as System.Web.Http.Results.JsonResult<bool>;
            bool resultadoBool = jsonResult.Content;
            Assert.IsTrue(resultadoBool);
        }

        [TestMethod]
        public void TestMethod23()
        {
            string respuestaEsperada = "like correcto";
            APIPostYEventos2019.Controllers.PostController controller = new APIPostYEventos2019.Controllers.PostController();
            APIPostYEventos2019.Controllers.PostController.CommentData token = new APIPostYEventos2019.Controllers.PostController.CommentData()
            {
                token = "TestToken"
            };
            var ultimoComentarioLLamar = controller.ultimoComentario(token);
            var jsonUltimoComentario = ultimoComentarioLLamar as System.Web.Http.Results.JsonResult<string>;
            int ultimoComentario = int.Parse(jsonUltimoComentario.Content);
            APIPostYEventos2019.Controllers.PostController.like like = new APIPostYEventos2019.Controllers.PostController.like()
            {
                nombreDeCuenta = "juan123",
                idpost = ultimoComentario,
                nombredeCreador = "juan123",
                token = "TestToken"
            };
            var resultado = controller.quitarLikeComentario(like);
            var jsonResult = resultado as System.Web.Http.Results.JsonResult<string>;
            string resultadoString = jsonResult.Content;
            Assert.AreEqual(respuestaEsperada, resultadoString);
        }

        [TestMethod]
        public async Task TestMethod24()
        {
            string respuestaEsperada = "juan123";
            APIPostYEventos2019.Controllers.PostController controller = new APIPostYEventos2019.Controllers.PostController();
            APIPostYEventos2019.Controllers.PostController.CommentData token = new APIPostYEventos2019.Controllers.PostController.CommentData()
            {
                token = "TestToken"
            };
            var ultimocomentarioLLamar = controller.ultimoComentario(token);
            var jsonUltimoComentario = ultimocomentarioLLamar as System.Web.Http.Results.JsonResult<string>;
            int ultimocomentario = int.Parse(jsonUltimoComentario.Content);
            APIPostYEventos2019.Controllers.PostController.PostData postdata = new APIPostYEventos2019.Controllers.PostController.PostData()
            {
                id = Convert.ToString(ultimocomentario),
                token = "TestToken"
            };
            var resultado = controller.conseguirCreadorComentario(postdata);
            var jsonResult = resultado as System.Web.Http.Results.JsonResult<string>;
            string resultadoString = jsonResult.Content;
            Assert.AreEqual(respuestaEsperada, resultadoString);
        }


        [TestMethod]
        public void TestMethodSecond01()
        {
            string respuestaEsperada = "Comentario eliminado";
            APIPostYEventos2019.Controllers.PostController controller = new APIPostYEventos2019.Controllers.PostController();
            APIPostYEventos2019.Controllers.PostController.CommentData token = new APIPostYEventos2019.Controllers.PostController.CommentData()
            {
                token = "TestToken"
            };
            var ultimoComentarioLLamar = controller.ultimoComentario(token);
            var jsonUltimoComentario = ultimoComentarioLLamar as System.Web.Http.Results.JsonResult<string>;
            int ultimoComentario = int.Parse(jsonUltimoComentario.Content);
            APIPostYEventos2019.Controllers.PostController.PostData comentario = new APIPostYEventos2019.Controllers.PostController.PostData()
            {
                id = Convert.ToString(ultimoComentario),
                token = "TestToken"
            };
            var resultado = controller.eliminarComentario(comentario);
            var jsonResult = resultado as System.Web.Http.Results.JsonResult<string>;
            string resultadoString = jsonResult.Content;
            Assert.AreEqual(respuestaEsperada, resultadoString);
        }

        [TestMethod]
        public void TestMethodSecond02()
        {
            string respuestaEsperada = "Post eliminado";
            APIPostYEventos2019.Controllers.PostController controller = new APIPostYEventos2019.Controllers.PostController();

            // Token object with the test token
            APIPostYEventos2019.Controllers.PostController.PostData token = new APIPostYEventos2019.Controllers.PostController.PostData()
            {
                token = "TestToken"
            };

            // Retrieve the latest post ID
            var ultimopostLLamar = controller.ultimoPost(token);
            var jsonUltimoPost = ultimopostLLamar as System.Web.Http.Results.JsonResult<string>;

            if (jsonUltimoPost != null)
            {
                int ultimopost = int.Parse(jsonUltimoPost.Content);
                Console.WriteLine($"ID del último post: {Convert.ToString(ultimopost)}");

                // Check if the post ID is valid
                if (ultimopost == 0)
                {
                    Assert.Fail("El último post no existe o no se pudo recuperar.");
                }

                // Prepare post data with the retrieved post ID and token
                APIPostYEventos2019.Controllers.PostController.PostData postdata = new APIPostYEventos2019.Controllers.PostController.PostData()
                {
                    id = Convert.ToString(ultimopost),
                    token = "TestToken"
                };

                // Attempt to delete the post
                var resultado = controller.eliminarPost(postdata);
                var jsonResult = resultado as System.Web.Http.Results.JsonResult<string>;

                if (jsonResult != null)
                {
                    string resultadoString = jsonResult.Content;

                    // Assert the expected outcome and include the actual result in case of failure
                    Assert.AreEqual(respuestaEsperada, resultadoString,
                        $"Se esperaba <{respuestaEsperada}>, pero se recibió <{resultadoString}>. ID del último post: {ultimopost}.");
                }
                else
                {
                    Assert.Fail("No se recibió un JsonResult esperado para la eliminación del post.");
                }
            }
            else
            {
                Assert.Fail("No se pudo obtener el último post.");
            }
        }




        [TestMethod]
        public async Task TestMethodSecond04()
        {
            string respuestaEsperada = "Evento"; // Adjust based on your expected result
            APIPostYEventos2019.Controllers.PostController controller = new APIPostYEventos2019.Controllers.PostController();

            // Token is passed directly, no need for AuthController
            APIPostYEventos2019.Controllers.PostController.EventData token = new APIPostYEventos2019.Controllers.PostController.EventData
            {
                token = "TestToken" // Use a valid test token here
            };

            // Get the latest event ID
            var ultimoEventoLLamar = controller.ultimoEvento(token);
            var jsonUltimoEvento = ultimoEventoLLamar as System.Web.Http.Results.JsonResult<string>;
            int ultimoEvento = int.Parse(jsonUltimoEvento.Content);

            // Create event data with the retrieved event ID and the token
            APIPostYEventos2019.Controllers.PostController.EventData eventData = new APIPostYEventos2019.Controllers.PostController.EventData
            {
                id = Convert.ToString(ultimoEvento),
                token = "TestToken", // Use the same token
                titulo = "Evento" // Replace with the search title you're testing for
            };

            // Act: Call the BuscarEventos method
            var resultado = await controller.BuscarEventos(eventData);
            var jsonResult = resultado as System.Web.Http.Results.JsonResult<string>;
            string resultadoString = jsonResult.Content;

            // Assert: Verify the result
            if (resultadoString.Contains("titulo"))
            {
                Assert.AreEqual(respuestaEsperada, resultadoString, "El título del evento no coincide con el valor esperado.");
            }
            else if (resultadoString.Contains("No se encontraron eventos"))
            {
                Assert.Fail("No se encontró ningún evento con los parámetros especificados.");
            }
            else
            {
                Assert.Fail($"Ocurrió un error inesperado. Respuesta: {resultadoString}");
            }
        }
    }
}
