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
    public partial class AgregarUsuarioAGrupo : Form
    {
        private string user;
        private List<dynamic> listaGrupos = new List<dynamic>();
        public class Grupo
        {
            public string nombreReal { get; set; }
            public string nombreVisible { get; set; }
            public string configuracion { get; set; }
            public string descripcion { get; set; }
            public string imagen { get; set; }
        }

        public AgregarUsuarioAGrupo(string usuario)
        {
            InitializeComponent();
            user = usuario;
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
                    string url = $"https://localhost:44347/group/ObtenerGruposPorNombreVisibleYUsuario?nombreVisible={nombreGrupo}&nombreDeCuenta={usuario}";
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

        private async void btnAgregar_Click(object sender, EventArgs e)
        {
            if (dataGridViewGrupos.CurrentCell != null)
            {
                string nombreReal = listaGrupos[dataGridViewGrupos.CurrentCell.RowIndex].nombreReal;
                dynamic resultado = await AgregarUsuario(user, txtNombreUsuario.Text, nombreReal);
                MessageBox.Show(resultado);
            }
            else
            {
                MessageBox.Show("Por favor, selecciona un grupo en la lista.");
            }
        }

        public async Task<dynamic> AgregarUsuario(string admin, string nombreUsuario, string nombreRealGrupo)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var datos = new Dictionary<string, string>
            {
                { "admin", admin },
                { "nombreUsuario", nombreUsuario },
                { "nombreGrupo", nombreRealGrupo }
            };

                    var content = new FormUrlEncodedContent(datos);
                    HttpResponseMessage response = await client.PostAsync($"https://localhost:44304/group/AgregarUsuarioAGrupoUG", content);
                    response.EnsureSuccessStatusCode();
                    var responseBody = await response.Content.ReadAsStringAsync();
                    dynamic data = JsonConvert.DeserializeObject(responseBody);
                    return data;
                }
                catch (Exception ex)
                {
                    return $"No se pudo agregar el usuario al grupo: {ex.Message}";
                }
            }
        }

    }

}

