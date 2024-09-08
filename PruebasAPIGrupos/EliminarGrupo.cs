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
            public string foto { get; set; }
        }

        private List<Grupo> listaGrupos = new List<Grupo>();

        private async void button2_Click(object sender, EventArgs e)
        {
            if (dataGridViewGrupos.CurrentCell != null && dataGridViewGrupos.CurrentCell.Selected)
            {
                if (MessageBox.Show("¿Estás seguro de eliminar este grupo?", "Eliminar grupo", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    int indice = dataGridViewGrupos.CurrentCell.RowIndex;

                    if (indice >= 0 && indice < listaGrupos.Count)
                    {
                        Grupo grupoSeleccionado = listaGrupos[indice];

                        try
                        {
                            dynamic resultado = await EliminarGrupo(grupoSeleccionado.nombreReal);
                            MessageBox.Show(resultado.ToString());
                            listaGrupos.RemoveAt(indice);
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
                            MessageBox.Show($"Ocurrió un error al eliminar el grupo: {ex.Message}");
                        }
                    }
                    else
                    {
                        MessageBox.Show("El índice seleccionado está fuera de los límites.");
                    }
                }
            }
            else
            {
                MessageBox.Show("No seleccionaste un grupo");
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
