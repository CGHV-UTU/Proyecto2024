using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace BackofficeDeAdministracion
{
    public partial class Principal : Form
    {
        public static string admin;
        private bool modo = true;
        public Principal(string usuario)
        {
            InitializeComponent();   
            lblUsuarioBackoffice.Text = usuario;
            admin = usuario;
            PanelReportes.BringToFront();
            PanelBackoffice.BringToFront();
            PanelReportes.Location = new Point(0, 30);
            PanelBackoffice.Location = new Point(0, 65);
            Registro();
        }

        //Registrar la entrada del administrador
        private void Registro()
        {
            string path = @"C:\Users\emerg\Downloads\elbackoffice\Proyecto2024\Log.txt";
            string mensaje = $"{DateTime.Now}: {admin} ha accedido al sistema";
            using (StreamWriter writer = new StreamWriter(path, true))
            {
                writer.WriteLine(mensaje);
            }
        }

        //Cerrar aplicacion
        private void Salir()
        {
            var result = MessageBox.Show("¿Esta seguro de salir?", "Confirmar cierre", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                //Registrar salida del administrador
                string path = @"C:\Users\emerg\Downloads\elbackoffice\Proyecto2024\Log.txt";
                string mensaje = $"{DateTime.Now}: {admin} ha salido del sistema";
                using (StreamWriter writer = new StreamWriter(path, true))
                {
                    writer.WriteLine(mensaje);
                }
                Environment.Exit(0);
            }
        }

        //Botones para cerrar el programa
        private void Principal_FormClosing(object sender, FormClosingEventArgs e)
        {          
            Salir();
            e.Cancel = true;
        }
        private void lblSalir_Click(object sender, EventArgs e)
        {
            Salir();
        }


        // Cambio entre modo claro y modo oscuro
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
                    else if (control.Name.Equals("PanelIzquierdo") && color.Equals(Color.Black))
                    {
                        control.BackColor = Color.Silver;
                        PanelVista.BackColor = Color.LightGray;
                    }
                    CambiarModo(color, control);
                }
            }
        }

        //Cargar un formulario en la ventana principal
        private void CargarForm(string nombreForm)
        {
            PanelVista.Controls.Clear();
            Type form = Type.GetType(nombreForm);
            Form formInstance = (Form)Activator.CreateInstance(form, lblUsuarioBackoffice.Text);
            formInstance.TopLevel = false;
            formInstance.FormBorderStyle = FormBorderStyle.None;
            //Comprobar modo de color del programa
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

        // Pasar de un form a otro
        private bool VerificarYCargarForm(string nombreForm) 
        {
            if (PanelVista.Controls.Count == 0)
            {
                CargarForm(nombreForm);
                return true;
            }
            else
            {
                DialogResult result = MessageBox.Show("¿Desea salir de la ventana actual? Los datos no guardados se perderán", "Confirmar salida", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    Limpiar();
                    CargarForm(nombreForm);
                    return true;
                }
                return false;
            }
        }

        //Limpiar seleccion de la barra lateral
        private void Limpiar()
        {
            lblContenidoUsuarios.Font = new Font(lblContenidoUsuarios.Font, FontStyle.Regular);
            lblContenidoPosts.Font = new Font(lblContenidoPosts.Font, FontStyle.Regular);
            lblContenidoEventos.Font = new Font(lblContenidoPosts.Font, FontStyle.Regular);
            lblContenidoComentarios.Font = new Font(lblContenidoPosts.Font, FontStyle.Regular);
            lblContenidoGrupos.Font = new Font(lblContenidoPosts.Font, FontStyle.Regular);
            lblReportesUsuario.Font = new Font(lblReportesUsuario.Font, FontStyle.Regular);
            lblReportesPost.Font = new Font(lblReportesPost.Font, FontStyle.Regular);
            lblReportesComentario.Font = new Font(lblReportesComentario.Font, FontStyle.Regular);
        }

        //Botones Principales de la barra lateral
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
                PanelBackoffice.Location = new Point(0, 225);
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

        //Botones Secundarios de la barra lateral       
        private void ContenidoUsuarios_Click(object sender, EventArgs e)
        {           
            bool cargado = VerificarYCargarForm("BackofficeDeAdministracion.GestionarUsuarios");
            if (cargado)
            {
                lblContenidoUsuarios.Font = new Font(lblContenidoUsuarios.Font, FontStyle.Underline);
            }
        }

        private void ContenidoPosts_Click(object sender, EventArgs e)
        {
            bool cargado = VerificarYCargarForm("BackofficeDeAdministracion.GestionarPosts");
            if (cargado)
            {
                lblContenidoPosts.Font = new Font(lblContenidoPosts.Font, FontStyle.Underline);
            }
        }

        private void ContenidoEventos_Click(object sender, EventArgs e)
        {
            bool cargado = VerificarYCargarForm("BackofficeDeAdministracion.GestionarEventos");
            if (cargado)
            {
                lblContenidoEventos.Font = new Font(lblContenidoEventos.Font, FontStyle.Underline);
            }
        }

        private void ContenidoComentarios_Click(object sender, EventArgs e)
        {
            bool cargado = VerificarYCargarForm("BackofficeDeAdministracion.GestionarComentarios");
            if (cargado)
            {
                lblContenidoComentarios.Font = new Font(lblContenidoComentarios.Font, FontStyle.Underline);
            }
        }

        private void ContenidoGrupos_Click(object sender, EventArgs e)
        {
            bool cargado = VerificarYCargarForm("BackofficeDeAdministracion.GestionarGrupos");
            if (cargado)
            {
                lblContenidoGrupos.Font = new Font(lblContenidoGrupos.Font, FontStyle.Underline);
            }
        }

        private void ReportesUsuario_Click(object sender, EventArgs e)
        {
            bool cargado = VerificarYCargarForm("BackofficeDeAdministracion.ReporteUsuario");
            if (cargado)
            {
                lblReportesUsuario.Font = new Font(lblReportesUsuario.Font, FontStyle.Underline);
            }
        }

        private void ReportesPost_Click(object sender, EventArgs e)
        {
            bool cargado = VerificarYCargarForm("BackofficeDeAdministracion.ReportePost");
            if (cargado)
            {
                lblReportesPost.Font = new Font(lblReportesPost.Font, FontStyle.Underline);
            }
        }

        private void lblReportesEvento_Click(object sender, EventArgs e)
        {
            bool cargado = VerificarYCargarForm("BackofficeDeAdministracion.ReporteEvento");
            if (cargado)
            {
                lblReportesEvento.Font = new Font(lblReportesEvento.Font, FontStyle.Underline);
            }
        }

        private void ReportesComentario_Click(object sender, EventArgs e)
        {
            bool cargado = VerificarYCargarForm("BackofficeDeAdministracion.ReporteComentario");
            if (cargado)
            {
                lblReportesComentario.Font = new Font(lblReportesComentario.Font, FontStyle.Underline);
            }
        }

        private void ReportesGrupo_Click(object sender, EventArgs e)
        {
            bool cargado = VerificarYCargarForm("BackofficeDeAdministracion.ReporteGrupo");
            if (cargado)
            {
                lblReportesEvento.Font = new Font(lblReportesEvento.Font, FontStyle.Underline);
            }
        }

        private void BackofficeLog_Click(object sender, EventArgs e)
        {
            bool cargado = VerificarYCargarForm("BackofficeDeAdministracion.Log");
            if (cargado)
            {
                lblLogs.Font = new Font(lblLogs.Font, FontStyle.Underline);
            }
        }
    }
}
