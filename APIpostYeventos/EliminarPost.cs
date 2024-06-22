using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using MySql.Data.MySqlClient;

namespace APIpostYeventos
{
    public partial class EliminarPost : Form
    {
        public EliminarPost()
        {
            InitializeComponent();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            Eliminar(txtID.Text);
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();
            this.Close();
        }

        static async Task Eliminar(string id)
        {
            using(HttpClient client=new HttpClient())
            {
                try 
                {
                    HttpResponseMessage response = await client.DeleteAsync($"https://localhost:44340/eliminarPost?id={id}");
                    response.EnsureSuccessStatusCode();
                }
                catch(Exception ex)
                {

                }
            }
        }
    }
}
