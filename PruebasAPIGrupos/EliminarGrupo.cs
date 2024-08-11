using Microsoft.Win32;
using Newtonsoft.Json;
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

namespace PruebasAPIGrupos
{
    public partial class Form3 : Form
    {
        private string user;
        public Form3(string usuario)
        {
            InitializeComponent();
            user = usuario;
        }

        public class Grupo
        {
            public string nombreReal { get; set; }
            public string nombreVisible { get; set; }
            public string configuracion { get; set; }
            public string descripcion { get; set; }
            public string imagen { get; set; }
        }

        private List<dynamic> listaGrupos = new List<dynamic>();

        private async void button2_Click(object sender, EventArgs e) //Eliminar grupo
        {
            if (dataGridViewGrupos.CurrentCell != null && dataGridViewGrupos.CurrentCell.Selected) {
                if (MessageBox.Show("¿Estas seguro de eliminar este grupo?", "Eliminar grupo", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                   int indice = dataGridViewGrupos.CurrentCell.RowIndex;
                   Grupo grupo = listaGrupos[indice];
                    try
                    {
                        dynamic resultado = await EliminarGrupo(grupo.nombreReal);
                        MessageBox.Show(resultado.ToString());
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ocurrió un error al eliminar el grupo: {ex.Message}");
                    }
                }
            } else
            {
                MessageBox.Show("No seleccionó un grupo");
            }
           
          
        }

        private async void btnBuscar_Click(object sender, EventArgs e)
        {
           await Buscar(user, txtNombre.Text);

        }

        public async Task Buscar(string usuario, string nombreGrupo)
        {    
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string url = $"https://localhost:44304/ObtenerGruposPorNombreVisibleYUsuario?nombreVisible={nombreGrupo}&nombreDeCuenta={usuario}";
                    HttpResponseMessage response = await client.GetAsync(url);
                    response.EnsureSuccessStatusCode();
                    var responseBody = await response.Content.ReadAsStringAsync();
                    listaGrupos = JsonConvert.DeserializeObject<List<dynamic>>(responseBody);

                    // Rellenar el DataGridView
                    dataGridViewGrupos.DataSource = listaGrupos.Select(g => new
                    {
                        clmnNombre = g.nombreVisible,
                        clmnDescripcion = g.Descripcion,
                        clmnConfig = g.Configuracion
                    }).ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al cargar los grupos: {ex.Message}");
                }
            }

        }

        public static async Task<dynamic> EliminarGrupo(string nombreReal)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.DeleteAsync($"https://localhost:44304/EliminarGrupo?nombreReal={Uri.EscapeDataString(nombreReal)}");
                    response.EnsureSuccessStatusCode();
                    var responseBody = await response.Content.ReadAsStringAsync();
                    dynamic data = JsonConvert.DeserializeObject(responseBody);
                    return data;
                }
                catch (Exception ex)
                {

                    return $"No se pudo eliminar el grupo: {ex.Message}";
                }
            }

        }


    }
}
