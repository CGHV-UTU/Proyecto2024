using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace APIpostYeventos
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnAñadirPost_Click(object sender, EventArgs e)
        {
            AñadirPost f=new AñadirPost();
            f.Show();
            this.Hide();
        }

        private void btnEditarPost_Click(object sender, EventArgs e)
        {
            EditarPost editar=new EditarPost();
            editar.Show();
            this.Hide();
        }
        private void btnEliminarPost_Click(object sender, EventArgs e)
        {
            EliminarPost eliminar=new EliminarPost();
            eliminar.Show();
            this.Hide();
        }

        private void btnAñadirEvento_Click(object sender, EventArgs e)
        {
            AñadirEvento añadir = new AñadirEvento();
            añadir.Show();
            this.Hide();
        }

        private void btnEliminarEvento_Click(object sender, EventArgs e)
        {
            EliminarEvento eliminar = new EliminarEvento();
            eliminar.Show();
            this.Hide();
        }

        private void btnEditarEvento_Click(object sender, EventArgs e)
        {

        } 
    }
}
