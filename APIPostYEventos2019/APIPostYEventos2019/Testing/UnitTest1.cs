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
                text="hola", user="UsuarioDePrueba", fechayhora=fechaHoraString
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
            var ultimopostLLamar = controller.ultimoPost();
            var jsonUltimoPost= ultimopostLLamar as System.Web.Http.Results.JsonResult<string>;
            ultimopost = int.Parse(jsonUltimoPost.Content);
            APIPostYEventos2019.Controllers.PostController.PostData postdata = new APIPostYEventos2019.Controllers.PostController.PostData()
            {
                id = Convert.ToString(ultimopost)
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
            string respuestaEsperada = "UsuarioDePrueba";
            APIPostYEventos2019.Controllers.PostController controller = new APIPostYEventos2019.Controllers.PostController();
            var ultimopostLLamar = controller.ultimoPost();
            var jsonUltimoPost = ultimopostLLamar as System.Web.Http.Results.JsonResult<string>;
            ultimopost = int.Parse(jsonUltimoPost.Content);
            APIPostYEventos2019.Controllers.PostController.PostData postdata = new APIPostYEventos2019.Controllers.PostController.PostData()
            {
                id = Convert.ToString(ultimopost)
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
            var ultimopostLLamar = controller.ultimoPost();
            var jsonUltimoPost = ultimopostLLamar as System.Web.Http.Results.JsonResult<string>;
            ultimopost = int.Parse(jsonUltimoPost.Content);
            APIPostYEventos2019.Controllers.PostController.PostData postdata = new APIPostYEventos2019.Controllers.PostController.PostData()
            {
                id = Convert.ToString(ultimopost), text = "chau", image= Convert.ToBase64String(imagenfalsa), link=""
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
                titulo="evento", fechaYhora_Inicio= fechaHoraFinalString, fechaYhora_Final= fechaHoraFinalString, foto= base64Image, user="UsuarioDePrueba"
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
            var ultimoEventoLLamar = controller.ultimoEvento();
            var jsonUltimoEvento = ultimoEventoLLamar as System.Web.Http.Results.JsonResult<string>;
            int ultimoEvento = int.Parse(jsonUltimoEvento.Content);
            APIPostYEventos2019.Controllers.PostController.EventData eventData = new APIPostYEventos2019.Controllers.PostController.EventData()
            {
                id= Convert.ToString(ultimoEvento),
                titulo = "eventoModificado",
                fechaYhora_Inicio = fechaHoraFinalString,
                fechaYhora_Final = fechaHoraFinalString,
                foto = base64Image,
                user = "UsuarioDePrueba"
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
            var ultimopostLLamar = controller.ultimoPost();
            var jsonUltimoPost = ultimopostLLamar as System.Web.Http.Results.JsonResult<string>;
            ultimopost = int.Parse(jsonUltimoPost.Content);
            APIPostYEventos2019.Controllers.PostController.like like = new APIPostYEventos2019.Controllers.PostController.like()
            {
                nombreDeCuenta="UsuarioDePrueba", idpost= ultimopost, nombredeCreador="UsuarioDePrueba" //por revisar
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
            var ultimopostLLamar = controller.ultimoPost();
            var jsonUltimoPost = ultimopostLLamar as System.Web.Http.Results.JsonResult<string>;
            ultimopost = int.Parse(jsonUltimoPost.Content);
            APIPostYEventos2019.Controllers.PostController.like like = new APIPostYEventos2019.Controllers.PostController.like()
            {
                nombreDeCuenta = "UsuarioDePrueba",
                idpost = ultimopost,
                nombredeCreador = "UsuarioDePrueba" //por revisar
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
            var ultimopostLLamar = controller.ultimoPost();
            var jsonUltimoPost = ultimopostLLamar as System.Web.Http.Results.JsonResult<string>;
            ultimopost = int.Parse(jsonUltimoPost.Content);
            APIPostYEventos2019.Controllers.PostController.like like = new APIPostYEventos2019.Controllers.PostController.like()
            {
                nombreDeCuenta = "UsuarioDePrueba",
                idpost = ultimopost,
                nombredeCreador = "UsuarioDePrueba" //por revisar
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
            var ultimoEventoLLamar = controller.ultimoEvento();
            var jsonUltimoEvento = ultimoEventoLLamar as System.Web.Http.Results.JsonResult<string>;
            int ultimoEvento = int.Parse(jsonUltimoEvento.Content);
            APIPostYEventos2019.Controllers.PostController.EventData evento = new APIPostYEventos2019.Controllers.PostController.EventData()
            {
                id = Convert.ToString(ultimoEvento)
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
            var ultimopostLLamar = controller.ultimoPost();
            var jsonUltimoPost = ultimopostLLamar as System.Web.Http.Results.JsonResult<string>;
            ultimopost = int.Parse(jsonUltimoPost.Content);
            APIPostYEventos2019.Controllers.PostController.CommentData comentario = new APIPostYEventos2019.Controllers.PostController.CommentData()
            {
                IdPost = Convert.ToString(ultimopost), fechayhora=fechaHoraString, NombreCreador="UsuarioDePrueba", NombreDeCuenta= "UsuarioDePrueba", texto= "buen post"
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
            var ultimoComentarioLLamar = controller.ultimoComentario();
            var jsonUltimoComentario = ultimoComentarioLLamar as System.Web.Http.Results.JsonResult<string>;
            int ultimoComentario = int.Parse(jsonUltimoComentario.Content);
            APIPostYEventos2019.Controllers.PostController.CommentData comentario = new APIPostYEventos2019.Controllers.PostController.CommentData()
            {
                id = Convert.ToString(ultimoComentario),
                texto = "mal post"
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
            var ultimoComentarioLLamar = controller.ultimoComentario();
            var jsonUltimoComentario = ultimoComentarioLLamar as System.Web.Http.Results.JsonResult<string>;
            int ultimoComentario = int.Parse(jsonUltimoComentario.Content);
            APIPostYEventos2019.Controllers.PostController.PostData comentario = new APIPostYEventos2019.Controllers.PostController.PostData()
            {
                id = Convert.ToString(ultimoComentario)
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
            var ultimopostLLamar = controller.ultimoPost();
            var jsonUltimoPost = ultimopostLLamar as System.Web.Http.Results.JsonResult<string>;
            ultimopost = int.Parse(jsonUltimoPost.Content);
            APIPostYEventos2019.Controllers.PostController.PostData post = new APIPostYEventos2019.Controllers.PostController.PostData()
            {
                id=Convert.ToString(ultimopost)
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
            var ultimoEventoLlamar = controller.ultimoEvento();
            var jsonUltimoEvento = ultimoEventoLlamar as System.Web.Http.Results.JsonResult<string>;
            int ultimoEvento = int.Parse(jsonUltimoEvento.Content);
            APIPostYEventos2019.Controllers.PostController.EventData eventData = new APIPostYEventos2019.Controllers.PostController.EventData()
            {
                id = Convert.ToString(ultimoEvento)
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
            var ultimoComentarioLLamar = controller.ultimoComentario();
            var jsonUltimoComentario = ultimoComentarioLLamar as System.Web.Http.Results.JsonResult<string>;
            int ultimoComentario = int.Parse(jsonUltimoComentario.Content);
            APIPostYEventos2019.Controllers.PostController.PostData post = new APIPostYEventos2019.Controllers.PostController.PostData()
            {
                id = Convert.ToString(ultimoComentario)
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
            var resultado = controller.seleccionarTodosLosPost();
            var jsonResult = resultado as System.Web.Http.Results.JsonResult<DataTable>;
            dynamic data = jsonResult.Content;
            string resultadoString = data.Rows[data.Rows.Count-1]["texto"];
            Assert.AreEqual(respuestaEsperada, resultadoString);
        }

        [TestMethod]
        public async Task TestMethod18()
        {
            string respuestaEsperada = "eventoModificado";
            APIPostYEventos2019.Controllers.PostController controller = new APIPostYEventos2019.Controllers.PostController();
            var resultado = controller.seleccionarTodosLosEventos();
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
            var ultimopostLLamar = controller.ultimoPost();
            var jsonUltimoPost = ultimopostLLamar as System.Web.Http.Results.JsonResult<string>;
            ultimopost = int.Parse(jsonUltimoPost.Content);
            APIPostYEventos2019.Controllers.PostController.PostData postData = new APIPostYEventos2019.Controllers.PostController.PostData()
            {
                id = Convert.ToString(ultimopost)
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
            var ultimopostLLamar = controller.ultimoPost();
            var jsonUltimoPost = ultimopostLLamar as System.Web.Http.Results.JsonResult<string>;
            string ultimoPost = jsonUltimoPost.Content;
            string respuestaEsperada = ultimoPost;
            APIPostYEventos2019.Controllers.PostController.PostData postData = new APIPostYEventos2019.Controllers.PostController.PostData()
            {
                user="UsuarioDePrueba"
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
            var ultimoComentarioLLamar = controller.ultimoComentario();
            var jsonUltimoComentario = ultimoComentarioLLamar as System.Web.Http.Results.JsonResult<string>;
            int ultimoComentario = int.Parse(jsonUltimoComentario.Content);
            APIPostYEventos2019.Controllers.PostController.like like = new APIPostYEventos2019.Controllers.PostController.like()
            {
                nombreDeCuenta = "UsuarioDePrueba",
                idpost = ultimoComentario,
                nombredeCreador = "UsuarioDePrueba" //por revisar
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
            var ultimoComentarioLLamar = controller.ultimoComentario();
            var jsonUltimoComentario = ultimoComentarioLLamar as System.Web.Http.Results.JsonResult<string>;
            int ultimoComentario = int.Parse(jsonUltimoComentario.Content);
            APIPostYEventos2019.Controllers.PostController.like like = new APIPostYEventos2019.Controllers.PostController.like()
            {
                nombreDeCuenta = "UsuarioDePrueba",
                idpost = ultimoComentario,
                nombredeCreador = "UsuarioDePrueba" //por revisar
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
            var ultimoComentarioLLamar = controller.ultimoComentario();
            var jsonUltimoComentario = ultimoComentarioLLamar as System.Web.Http.Results.JsonResult<string>;
            int ultimoComentario = int.Parse(jsonUltimoComentario.Content);
            APIPostYEventos2019.Controllers.PostController.like like = new APIPostYEventos2019.Controllers.PostController.like()
            {
                nombreDeCuenta = "UsuarioDePrueba",
                idpost = ultimoComentario,
                nombredeCreador = "UsuarioDePrueba" //por revisar
            };
            var resultado = controller.quitarLikeComentario(like);
            var jsonResult = resultado as System.Web.Http.Results.JsonResult<string>;
            string resultadoString = jsonResult.Content;
            Assert.AreEqual(respuestaEsperada, resultadoString);
        }

        [TestMethod]
        public async Task TestMethod24()
        {
            string respuestaEsperada = "UsuarioDePrueba";
            APIPostYEventos2019.Controllers.PostController controller = new APIPostYEventos2019.Controllers.PostController();
            var ultimocomentarioLLamar = controller.ultimoComentario();
            var jsonUltimoComentario = ultimocomentarioLLamar as System.Web.Http.Results.JsonResult<string>;
            int ultimocomentario = int.Parse(jsonUltimoComentario.Content);
            APIPostYEventos2019.Controllers.PostController.PostData postdata = new APIPostYEventos2019.Controllers.PostController.PostData()
            {
                id = Convert.ToString(ultimocomentario)
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
            var ultimoComentarioLLamar = controller.ultimoComentario();
            var jsonUltimoComentario = ultimoComentarioLLamar as System.Web.Http.Results.JsonResult<string>;
            int ultimoComentario = int.Parse(jsonUltimoComentario.Content);
            APIPostYEventos2019.Controllers.PostController.PostData comentario = new APIPostYEventos2019.Controllers.PostController.PostData()
            {
                id = Convert.ToString(ultimoComentario)
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
            var ultimopostLLamar = controller.ultimoPost();
            var jsonUltimoPost = ultimopostLLamar as System.Web.Http.Results.JsonResult<string>;
            ultimopost = int.Parse(jsonUltimoPost.Content);
            APIPostYEventos2019.Controllers.PostController.PostData postdata = new APIPostYEventos2019.Controllers.PostController.PostData()
            {
                id = Convert.ToString(ultimopost)
            };
            var resultado = controller.eliminarPost(postdata);
            var jsonResult = resultado as System.Web.Http.Results.JsonResult<string>;
            string resultadoString = jsonResult.Content;
            Assert.AreEqual(respuestaEsperada, resultadoString);
        }


        [TestMethod]
        public void TestMethodSecond03()
        {
            string respuestaEsperada = "Evento eliminado";
            APIPostYEventos2019.Controllers.PostController controller = new APIPostYEventos2019.Controllers.PostController();
            var ultimoEventoLLamar = controller.ultimoEvento();
            var jsonUltimoEvento = ultimoEventoLLamar as System.Web.Http.Results.JsonResult<string>;
            int ultimoEvento = int.Parse(jsonUltimoEvento.Content);
            APIPostYEventos2019.Controllers.PostController.EventData eventData = new APIPostYEventos2019.Controllers.PostController.EventData()
            {
                id = Convert.ToString(ultimoEvento)
            };
            var resultado = controller.eliminarEvento(eventData);
            var jsonResult = resultado as System.Web.Http.Results.JsonResult<string>;
            string resultadoString = jsonResult.Content;
            Assert.AreEqual(respuestaEsperada, resultadoString);
        }

        
    }
}
