using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PruebasAPIGrupos
{
    public partial class MenuGrupo : Form
    {
        private static string user;
        public MenuGrupo(string Usuario)
        {
            InitializeComponent();
            user = Usuario;
        }

        public class Grupo
        {
            public string nombreReal { get; set; }
            public string nombreVisible { get; set; }
            public string configuracion { get; set; }
            public string descripcion { get; set; }
            public string imagen { get; set; }
        }


        private void button1_Click(object sender, EventArgs e)
        {   
            switch (cbxMenu.SelectedItem.ToString())
            {
                case "Crear grupo":
                    Form2 CrearGrupo = new Form2(user);
                    CrearGrupo.Visible = true;
                    break;

                case "Eliminar grupo":
                    Form3 EliminarGrupo = new Form3(user);
                    EliminarGrupo.Visible=true;
                    break;

                case "Ver grupos de un usuario":
                    GruposUsuario gruposUsuario = new GruposUsuario(user);
                    gruposUsuario.Visible = true;
                    break;

                case "Modificar grupo":
                    ModificarGrupo modificarGrupo = new ModificarGrupo(user);
                    modificarGrupo.Visible = true;
                    break;
                case "Reportar":
                    Reportar reportar = new Reportar(user);
                    reportar.Visible = true;
                    break;
            }
        }
    }
}
