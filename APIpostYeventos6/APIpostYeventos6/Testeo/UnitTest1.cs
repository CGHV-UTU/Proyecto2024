using Microsoft.Azure.Storage.Core.Util;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace Testeo
{
    [TestClass]
    public class UnitTest1
    {
        //a partir de acá pruebas de posts
        [TestMethod]
        public void prueba1()
        {
            string respuestaEsperada = "El post se creó correctamente";
            APIpostYeventos.AñadirPost añadirPost = new APIpostYeventos.AñadirPost();
            string respuestaRecibida = añadirPost.hacerPost("youtube.com", "hol");
            Assert.AreEqual(respuestaEsperada, respuestaRecibida);
        }

        [TestMethod]
        public void prueba2()
        {
            string respuestaEsperada = "Modificacion correcta";
            APIpostYeventos.EditarPost editarPost = new APIpostYeventos.EditarPost();
            string respuestaRecibida = editarPost.modificarPost(editarPost.ultimoPost(), "google.com", "adios");
            Assert.AreEqual(respuestaEsperada, respuestaRecibida);
        }

        [TestMethod]
        public void prueba3()
        {
            string respuestaEsperada = "adios";
            APIpostYeventos.EditarPost editarPost = new APIpostYeventos.EditarPost();
            string respuestaRecibida = editarPost.conseguirPost(int.Parse(editarPost.ultimoPost()));  
            Assert.AreEqual(respuestaEsperada, respuestaRecibida);
        }

        [TestMethod]
        public void prueba4()
        {
            string respuestaEsperada = "El Post se eliminó correctamente";
            APIpostYeventos.EliminarPost eliminarPost = new APIpostYeventos.EliminarPost();
            string respuestaRecibida = eliminarPost.probarEliminarPost(eliminarPost.ultimoPost());
            Assert.AreEqual(respuestaEsperada, respuestaRecibida);
        }

        //a partir de acá pruebas de evento
        [TestMethod]
        public void prueba5()
        {
            string respuestaEsperada = "guardado correcto";
            APIpostYeventos.AñadirEvento añadirEvento = new APIpostYeventos.AñadirEvento();
            byte[] imagenParaProbar = new byte[0];
            string respuestaRecibida = añadirEvento.hacerEvento("EVENTO DE EJEMPLO",Convert.ToBase64String(imagenParaProbar),"14/06/2024 10:54");
            Assert.AreEqual(respuestaEsperada, respuestaRecibida);
        }

        [TestMethod]
        public void prueba6()
        {
            string respuestaEsperada = "modificacion correcta";
            APIpostYeventos.EditarEvento editarEvento = new APIpostYeventos.EditarEvento();
            string respuestaRecibida = editarEvento.modificarEvento(editarEvento.ultimoEvento(), "TITULO CAMBIADO","aca deberia haber una imagen", "21/06/2024 10:54");
            Assert.AreEqual(respuestaEsperada, respuestaRecibida);
        }

        [TestMethod]
        public void prueba7()
        {
            string respuestaEsperada = "TITULO CAMBIADO";
            APIpostYeventos.EditarEvento editarEvento = new APIpostYeventos.EditarEvento();
            string respuestaRecibida = editarEvento.conseguirEvento(int.Parse(editarEvento.ultimoEvento()));
            Assert.AreEqual(respuestaEsperada, respuestaRecibida);
        }

        [TestMethod]
        public void prueba8()
        {
            string respuestaEsperada = "Evento eliminado";
            APIpostYeventos.EliminarEvento eliminarEvento = new APIpostYeventos.EliminarEvento();
            string respuestaRecibida = eliminarEvento.eliminarEvento(eliminarEvento.ultimoEvento());
            Assert.AreEqual(respuestaEsperada, respuestaRecibida);
        }
    }
}
