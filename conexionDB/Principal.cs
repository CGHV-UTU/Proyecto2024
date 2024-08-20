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
            PanelReportes.BringToFront();
            PanelBackoffice.BringToFront();
            PanelReportes.Location = new Point(0, 30);
            PanelBackoffice.Location = new Point(0, 65);
        }

        private void CargarForm(string nombreForm)
        {
            PanelVista.Controls.Clear();
            Type form = Type.GetType(nombreForm);
            Form formInstance = (Form)Activator.CreateInstance(form);
            formInstance.TopLevel = false;
            formInstance.FormBorderStyle = FormBorderStyle.None;
            formInstance.BackColor = Color.LightGray;
            formInstance.Dock = DockStyle.Fill;
            PanelVista.Controls.Add(formInstance);
            formInstance.Show();
        }        
        private void lblSalir_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }


        //Botones Principales
        private void AbrirContenido_Click(object sender, EventArgs e)
        {
            PanelOpcionesReportes.Visible = false;
            PanelOpcionesBackoffice.Visible = false;
            PanelOpcionesContenido.Visible = !PanelOpcionesContenido.Visible;
            if (PanelOpcionesContenido.Visible)
            {
                PanelReportes.Location = new Point(0, 177);
                PanelBackoffice.Location = new Point(0, 211);
            }
            else
            {
                PanelReportes.Location = new Point(0, 30);
                PanelBackoffice.Location = new Point(0, 65);
            }                 
        }
        private void AbrirReportes_Click(object sender, EventArgs e)
        {
            PanelOpcionesContenido.Visible = false;
            PanelOpcionesBackoffice.Visible = false;
            PanelOpcionesReportes.Visible = !PanelOpcionesReportes.Visible;
            if (PanelOpcionesReportes.Visible)
            {
                PanelReportes.Location = new Point(0, 30);
                PanelBackoffice.Location = new Point(0, 200);
            }
            else
            {
                PanelReportes.Location = new Point(0, 30);
                PanelBackoffice.Location = new Point(0, 65);
            }
        }
        private void AbrirBackoffice_Click(object sender, EventArgs e)
        {
            PanelOpcionesContenido.Visible = false;
            PanelOpcionesReportes.Visible = false;
            PanelOpcionesBackoffice.Visible = !PanelOpcionesBackoffice.Visible;
            PanelReportes.Location = new Point(0, 30);
            PanelBackoffice.Location = new Point(0, 65);
        }

        //Botones Secundarios
        private void ContenidoUsuarios_Click(object sender, EventArgs e)
        {
            CargarForm("BackofficeDeAdministracion.GestionarUsuarios");
        }

        private void ContenidoPosts_Click(object sender, EventArgs e)
        {
            CargarForm("BackofficeDeAdministracion.GestionarPosts");
        }

        private void ContenidoEventos_Click(object sender, EventArgs e)
        {
            CargarForm("BackofficeDeAdministracion.GestionarEventos");
        }

        private void ContenidoComentarios_Click(object sender, EventArgs e)
        {
            CargarForm("BackofficeDeAdministracion.GestionarComentarios");
        }

        private void ContenidoGrupos_Click(object sender, EventArgs e)
        {
            CargarForm("BackofficeDeAdministracion.GestionarGrupos");
        }

        private void ReportesUsuario_Click(object sender, EventArgs e)
        {
            CargarForm("BackofficeDeAdministracion.ReporteUsuario");
        }

        private void ReportesPost_Click(object sender, EventArgs e)
        {
            CargarForm("BackofficeDeAdministracion.ReportePost");
        }

        private void ReportesComentario_Click(object sender, EventArgs e)
        {
            CargarForm("BackofficeDeAdministracion.ReporteComentario");
        }

        private void ReportesGrupo_Click(object sender, EventArgs e)
        {

        }

        private void BackofficeAgregar_Click(object sender, EventArgs e)
        {
            CargarForm("BackofficeDeAdministracion.AgregarAdmin");
        }

        private void BackofficeEliminar_Click(object sender, EventArgs e)
        {
            CargarForm("BackofficeDeAdministracion.EliminarAdmin");
        }
    }
}
