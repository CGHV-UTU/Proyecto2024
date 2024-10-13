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
        private bool modo = true;
        public Principal(string usuario)
        {
            InitializeComponent();   
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
            if (modo)
            {
                formInstance.BackColor = Color.DimGray;
                foreach (Control control in formInstance.Controls.OfType<Label>())
                {
                    control.ForeColor = Color.White;
                }
            }
            else
            {
                formInstance.BackColor = Color.LightGray;
                foreach (Control control in formInstance.Controls.OfType<Label>())
                {
                    control.ForeColor = Color.Black;
                }
            }
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
        private void VerificarYCargarForm(string nombreForm)
        {
            if (PanelVista.Controls.Count == 0)
            {
                CargarForm(nombreForm);
            }
            else
            {
                DialogResult result = MessageBox.Show("¿Desea salir de la ventana actual? Los datos no guardados se perderán", "Confirmar salida", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    CargarForm(nombreForm);
                }
            }
        }
        private void ContenidoUsuarios_Click(object sender, EventArgs e)
        {           
            VerificarYCargarForm("BackofficeDeAdministracion.GestionarUsuarios");
            lblContenidoUsuarios.Font = new Font(lblContenidoUsuarios.Font, FontStyle.Underline);
        }
        private void ContenidoPosts_Click(object sender, EventArgs e)
        {
            VerificarYCargarForm("BackofficeDeAdministracion.GestionarPosts");
            lblContenidoPosts.Font = new Font(lblContenidoPosts.Font, FontStyle.Underline);
        }

        private void ContenidoEventos_Click(object sender, EventArgs e)
        {
            VerificarYCargarForm("BackofficeDeAdministracion.GestionarEventos");
            lblContenidoEventos.Font = new Font(lblContenidoEventos.Font, FontStyle.Underline);
        }

        private void ContenidoComentarios_Click(object sender, EventArgs e)
        {
            VerificarYCargarForm("BackofficeDeAdministracion.GestionarComentarios");
            lblContenidoComentarios.Font = new Font(lblContenidoComentarios.Font, FontStyle.Underline);
        }

        private void ContenidoGrupos_Click(object sender, EventArgs e)
        {
            VerificarYCargarForm("BackofficeDeAdministracion.GestionarGrupos");
            lblContenidoGrupos.Font = new Font(lblContenidoGrupos.Font, FontStyle.Underline);
        }

        private void ReportesUsuario_Click(object sender, EventArgs e)
        {
            VerificarYCargarForm("BackofficeDeAdministracion.ReporteUsuario");
        }

        private void ReportesPost_Click(object sender, EventArgs e)
        {
            VerificarYCargarForm("BackofficeDeAdministracion.ReportePost");
        }

        private void ReportesComentario_Click(object sender, EventArgs e)
        {
            VerificarYCargarForm("BackofficeDeAdministracion.ReporteComentario");
        }

        private void ReportesGrupo_Click(object sender, EventArgs e)
        {
            VerificarYCargarForm("BackofficeDeAdministracion.ReporteGrupo");
        }

        private void lblModo_Click(object sender, EventArgs e)
        {
            if (modo)
            {
                CambiarModo(Color.Black, this);
                modo = false;
            }
            else
            {
                CambiarModo(Color.White, this);
                modo = true;
            }
        }
        private void CambiarModo(Color color, Control control1)
        {
            foreach (Control control in control1.Controls)
            {
                if (control is Label label)
                {
                    label.ForeColor = color;
                }
                else if (control.HasChildren)
                {
                    if (control.Name.Equals("PanelIzquierdo") && color.Equals(Color.White))
                    {
                        control.BackColor = Color.FromArgb(64, 64, 64);
                        PanelVista.BackColor = Color.DimGray;
                    }
                    else if(control.Name.Equals("PanelIzquierdo") && color.Equals(Color.Black))
                    {
                        control.BackColor = Color.Silver;
                        PanelVista.BackColor = Color.LightGray;
                    }
                    CambiarModo(color, control);
                }
            }
        }

    }
}
