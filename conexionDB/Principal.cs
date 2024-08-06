using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace BackofficeDeAdministracion
{
    public partial class Principal : Form
    {
        
        public Principal(string usuario)
        {
            InitializeComponent();   
            VerificarBaneosTemporales.Iniciar();
            lblUsuarioBackoffice.Text = usuario;          
        }
        
        private void btnCargar_Click(object sender, EventArgs e)
        {
            if (cbxTabla.SelectedItem != null)
            {
                string tabla = cbxTabla.SelectedItem.ToString();
                switch (tabla)
                {
                    case "Post":
                        Editar_post ventanaPost = new Editar_post();
                        ventanaPost.Show();
                        break;

                    case "Evento":
                        Editar_evento ventanaEvento = new Editar_evento();
                        ventanaEvento.Show();
                        break;

                    case "Comentario":
                        Editar_comentario ventanaComentario = new Editar_comentario();
                        ventanaComentario.Show();
                        break;

                    case "Usuario":
                        GestionarUsuario ventanaUsuario = new GestionarUsuario();
                        ventanaUsuario.Show();
                        break;
                    case "Grupo":
                        GestionarGrupos ventanaGrupo = new GestionarGrupos();
                        ventanaGrupo.Show();
                        break;
                }
            }
            else
            {
                MessageBox.Show("Seleccione una tabla", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }       

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);               
        }
    }
}
