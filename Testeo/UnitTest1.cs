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
            BackofficeDeAdministracion.GestionarPosts editarPost = new BackofficeDeAdministracion.GestionarPosts();
            byte[] imagen = new byte[0];
            string respuestaRecibida = editarPost.ModificarPost(editarPost.UltimoPost(), "google.com", "adios");
            Assert.AreEqual(respuestaEsperada, respuestaRecibida);
        }
        [TestMethod]
        public void ComprobarPost()
        {
            string respuestaEsperada = "adios";
            BackofficeDeAdministracion.GestionarPosts editarPost = new BackofficeDeAdministracion.GestionarPosts();
            string respuestaRecibida = editarPost.ConseguirPost(int.Parse(editarPost.UltimoPost()));
            Assert.AreEqual(respuestaEsperada, respuestaRecibida);
        }

        //Eventos
        [TestMethod]
        public void ModificarEvento()
        {
            string respuestaEsperada = "modificacion correcta";
            BackofficeDeAdministracion.GestionarEventos editarEvento = new BackofficeDeAdministracion.GestionarEventos();
            string respuestaRecibida = editarEvento.ModificarEvento(editarEvento.UltimoEvento(), "TITULO CAMBIADO UNICAMENTE");
            Assert.AreEqual(respuestaEsperada, respuestaRecibida);
        }

        //Comentarios
        //Se modifica el último comentario creado
        [TestMethod]
        public void ModificarComentario()
        {
            string respuestaEsperada = "Modificacion correcta";
            BackofficeDeAdministracion.GestionarComentarios editarComentario = new BackofficeDeAdministracion.GestionarComentarios();
            string respuestaRecibida = editarComentario.ModificarComentario(editarComentario.UltimoComentario(), "me encanta este post es muy bueno");
            Assert.AreEqual(respuestaEsperada, respuestaRecibida);
        }
        [TestMethod]
        public void ComprobarComentario()
        {
            string respuestaEsperada = "me encanta este post es muy bueno";
            BackofficeDeAdministracion.GestionarComentarios comprobarComentario = new BackofficeDeAdministracion.GestionarComentarios();
            string respuestaRecibida = comprobarComentario.ConseguirComentario(int.Parse(comprobarComentario.UltimoComentario()));
            Assert.AreEqual(respuestaEsperada, respuestaRecibida);
        }
        [TestMethod]
        public void EliminarComentario()
        {
            string respuestaEsperada = "Comentario eliminado";
            BackofficeDeAdministracion.GestionarComentarios eliminarComentario = new BackofficeDeAdministracion.GestionarComentarios();
            string respuestaRecibida = eliminarComentario.EliminarComentario(int.Parse(eliminarComentario.UltimoComentario()));
            Assert.AreEqual(respuestaEsperada, respuestaRecibida);
        }

        //Grupos
        //Se modifica el último grupo creado
        [TestMethod]
        public void EliminarGrupo()
        {
            string respuestaEsperada = "Grupo eliminado";
            BackofficeDeAdministracion.GestionarGrupos eliminarGrupo = new BackofficeDeAdministracion.GestionarGrupos();
            string respuestaRecibida = eliminarGrupo.EliminarGrupo(eliminarGrupo.UltimoGrupo());
            Assert.AreEqual(respuestaEsperada, respuestaRecibida);
        }

        //Usuarios
        //Se modifica el último usuario creado
        [TestMethod]
        public void BaneoPermanente()
        {
            string respuestaEsperada = "Usuario baneado";
            BackofficeDeAdministracion.GestionarUsuarios banearUsuario = new BackofficeDeAdministracion.GestionarUsuarios();
            string respuestaRecibida = banearUsuario.baneoPermanente(banearUsuario.ultimoUsuario());
            Assert.AreEqual(respuestaEsperada, respuestaRecibida);
        }

        [TestMethod]
       public void ActivarCuenta()
        {
            string respuestaEsperada = "Usuario desbaneado";
            BackofficeDeAdministracion.GestionarUsuarios banearUsuario = new BackofficeDeAdministracion.GestionarUsuarios();
            string respuestaRecibida = banearUsuario.desbanear(banearUsuario.ultimoUsuario());
            Assert.AreEqual(respuestaEsperada, respuestaRecibida);
        }
    }
}
