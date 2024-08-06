using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Testeo
{
    [TestClass]
    public class UnitTest1
    {

        //Posts
        [TestMethod]
        public void ModificarPost()
        {
            string respuestaEsperada = "Modificacion correcta";
            BackofficeDeAdministracion.Editar_post editarPost = new BackofficeDeAdministracion.Editar_post();
            byte[] imagen = new byte[0];
            string respuestaRecibida = editarPost.modificarPost(editarPost.ultimoPost(), "google.com", "adios");
            Assert.AreEqual(respuestaEsperada, respuestaRecibida);
        }
        [TestMethod]
        public void ComprobarPost()
        {
            string respuestaEsperada = "adios";
            BackofficeDeAdministracion.Editar_post editarPost = new BackofficeDeAdministracion.Editar_post();
            string respuestaRecibida = editarPost.conseguirPost(int.Parse(editarPost.ultimoPost()));
            Assert.AreEqual(respuestaEsperada, respuestaRecibida);
        }

        //Eventos
        [TestMethod]
        public void ModificarEvento()
        {
            string respuestaEsperada = "modificacion correcta";
            BackofficeDeAdministracion.Editar_evento editarEvento = new BackofficeDeAdministracion.Editar_evento();
            string respuestaRecibida = editarEvento.modificarEvento(editarEvento.ultimoEvento(), "TITULO CAMBIADO UNICAMENTE");
            Assert.AreEqual(respuestaEsperada, respuestaRecibida);
        }

        //Comentarios
        //Se modifica el último comentario creado
        [TestMethod]
        public void ModificarComentario()
        {
            string respuestaEsperada = "Modificacion correcta";
            BackofficeDeAdministracion.Editar_comentario editarComentario = new BackofficeDeAdministracion.Editar_comentario();
            string respuestaRecibida = editarComentario.modificarComentario(editarComentario.ultimoComentario(), "me encanta este post es muy bueno");
            Assert.AreEqual(respuestaEsperada, respuestaRecibida);
        }
        [TestMethod]
        public void ComprobarComentario()
        {
            string respuestaEsperada = "me encanta este post es muy bueno";
            BackofficeDeAdministracion.Editar_comentario comprobarComentario = new BackofficeDeAdministracion.Editar_comentario();
            string respuestaRecibida = comprobarComentario.conseguirComentario(int.Parse(comprobarComentario.ultimoComentario()));
            Assert.AreEqual(respuestaEsperada, respuestaRecibida);
        }
        [TestMethod]
        public void EliminarComentario()
        {
            string respuestaEsperada = "Comentario eliminado";
            BackofficeDeAdministracion.Editar_comentario eliminarComentario = new BackofficeDeAdministracion.Editar_comentario();
            string respuestaRecibida = eliminarComentario.eliminarComentario(int.Parse(eliminarComentario.ultimoComentario()));
            Assert.AreEqual(respuestaEsperada, respuestaRecibida);
        }

        //Grupos
        //Se modifica el último grupo creado
        [TestMethod]
        public void EliminarGrupo()
        {
            string respuestaEsperada = "Grupo eliminado";
            BackofficeDeAdministracion.GestionarGrupos eliminarGrupo = new BackofficeDeAdministracion.GestionarGrupos();
            string respuestaRecibida = eliminarGrupo.eliminarGrupo(eliminarGrupo.ultimoGrupo());
            Assert.AreEqual(respuestaEsperada, respuestaRecibida);
        }

        //Usuarios
        //Se modifica el último usuario creado
        [TestMethod]
        public void BaneoPermanente()
        {
            string respuestaEsperada = "Usuario baneado";
            BackofficeDeAdministracion.GestionarUsuario banearUsuario = new BackofficeDeAdministracion.GestionarUsuario();
            string respuestaRecibida = banearUsuario.baneoPermanente(banearUsuario.ultimoUsuario());
            Assert.AreEqual(respuestaEsperada, respuestaRecibida);
        }

        [TestMethod]
       public void ActivarCuenta()
        {
            string respuestaEsperada = "Usuario desbaneado";
            BackofficeDeAdministracion.GestionarUsuario banearUsuario = new BackofficeDeAdministracion.GestionarUsuario();
            string respuestaRecibida = banearUsuario.desbanear(banearUsuario.ultimoUsuario());
            Assert.AreEqual(respuestaEsperada, respuestaRecibida);
        }
    }
}
