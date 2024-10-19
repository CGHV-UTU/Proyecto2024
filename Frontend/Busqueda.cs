using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Frontend
{
    public partial class Busqueda : Form
    {
        public Busqueda()
        {
            InitializeComponent();
            this.pnlOpciones.Visible = false;
            this.Size= new Size(1012, 128);
            this.pnlMostrar.Size = new Size(869, 211);
        }

        private void btnOpciones_Click(object sender, EventArgs e)
        {
            pnlOpciones.Visible = true;
            this.Size = new Size(1012, 342);
            this.pnlOpciones.Parent = this;
        }
    }
}
