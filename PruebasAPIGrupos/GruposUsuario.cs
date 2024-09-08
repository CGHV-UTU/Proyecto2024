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
    public partial class GruposUsuario : Form
    {
        private List<Grupo> listaGrupos = new List<Grupo>();
        public class Grupo
        {
            public string nombreReal { get; set; }
            public string nombreVisible { get; set; }
            public string configuracion { get; set; }
            public string descripcion { get; set; }
            public string imagen { get; set; }
            public string nombreDeCuenta { get; set; }
        }
        public GruposUsuario(string usuario)
        {
            InitializeComponent();
        }

        private async void btnBuscar_Click(object sender, EventArgs e)
        {
            await Buscar(txtNombre.Text);
        }
        public async Task Buscar(string usuario)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string url = $"https://localhost:44304/ObtenerGruposPorUsuario?nombreDeCuenta={usuario}";
                    HttpResponseMessage response = await client.GetAsync(url);
                    response.EnsureSuccessStatusCode();

                    var responseBody = await response.Content.ReadAsStringAsync();
                    listaGrupos = JsonConvert.DeserializeObject<List<Grupo>>(responseBody);

                    dataGridViewGrupos.DataSource = listaGrupos.Select(g => new
                    {
                        NombreReal = g.nombreReal,
                        Nombre = g.nombreVisible,
                        Descripcion = g.descripcion,
                        Configuración = g.configuracion
                    }).ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al cargar los grupos: {ex.Message}");
                }
            }
        }
    }
}
