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
    public partial class ModificarGrupo : Form
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

        public ModificarGrupo(string usuario)
        {
            InitializeComponent();
            user = usuario;
        }

        private async void btnBuscar_Click(object sender, EventArgs e)
        {
           await Buscar(user, txtNombre.Text);
        }

        private Boolean verificarDatos(string txtNombreVisible, string txtImagen, string configuracion)
        {
            Boolean correcto = false;
            if (!string.IsNullOrEmpty(txtNombreVisible)
                || !string.IsNullOrEmpty(txtImagen)
                || !string.IsNullOrEmpty(configuracion))
            {
                correcto = true;
            }
            return correcto;
        }

        public async Task<dynamic> EditarGrupo(Grupo grupo, string usuario)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var content = new StringContent(JsonConvert.SerializeObject(grupo), Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PutAsync($"https://localhost:44347/group/EditarGrupoUG?usuario={Uri.EscapeDataString(usuario)}", content);
                    response.EnsureSuccessStatusCode();
                    var responseBody = await response.Content.ReadAsStringAsync();
                    dynamic data = JsonConvert.DeserializeObject(responseBody);
                    return data;
                }
                catch (Exception ex)
                {
                    return $"No se pudo editar el grupo: {ex.Message}";
                }
            }
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

        private async void btnModificar_Click(object sender, EventArgs e)
        {
            string configuracion = "TodosHablan";
            if (rbtnAdminHabla.Checked)
            {
                configuracion = "AdminHabla";
            }

            if (verificarDatos(txtNombre.Text, txtImagen.Text, configuracion))
            {
                if (string.IsNullOrEmpty(txtDescripcion.Text))
                {
                    txtDescripcion.Text = "";
                }
                Grupo grupo = new Grupo
                {
                    nombreReal = listaGrupos[dataGridViewGrupos.CurrentCell.RowIndex].nombreReal,
                    nombreVisible = txtNombre.Text,
                    descripcion = txtDescripcion.Text,
                    imagen = txtImagen.Text,
                    configuracion = configuracion
                };

                try
                {
                    dynamic resultado = await EditarGrupo(grupo, user);
                    MessageBox.Show(resultado.ToString());
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ocurrió un error al modificar el grupo: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("Datos inválidos");
            }
        }






    }
}
