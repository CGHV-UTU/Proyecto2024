using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
namespace APIpostYeventos
{
    public partial class EliminarEvento : Form
    {
        public EliminarEvento()
        {
            InitializeComponent();
        }
        private void btnVolver_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();
            this.Close();
        }
        static async Task Eliminar(string id)
        {
            using (HttpClient client = new HttpClient())
            {
                    HttpResponseMessage response = await client.DeleteAsync($"https://localhost:44340/eliminarEvento?id={id}");
                    response.EnsureSuccessStatusCode();
            }
        }
        private void btnEliminar_Click_1(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtIdEvento.Text) || !int.TryParse(txtIdEvento.Text, out int numero) )
            {
                lblError.Show();
                lblError.Text = "Debe ingresar una ID válida";
            }
            else
            {
                Eliminar(txtIdEvento.Text);
                lblError.Text = "El Evento se eliminó correctamente";
                lblError.Show();
            }
        }
    }
}
