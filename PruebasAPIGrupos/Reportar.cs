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
    public partial class Reportar : Form
    {
        private static string user;
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

        public Reportar(string Usuario)
        {
            InitializeComponent();
            user = Usuario;
        }

        private async void btnReportar_Click(object sender, EventArgs e)
        {
            if (dataGridViewGrupos.CurrentCell != null && dataGridViewGrupos.CurrentCell.Selected)
            {
                int indice = dataGridViewGrupos.CurrentCell.RowIndex;

                if (indice >= 0 && indice < listaGrupos.Count)
                {
                    Grupo grupoSeleccionado = listaGrupos[indice];

                    Grupo grupoModificado = new Grupo
                    {
                        nombreReal = grupoSeleccionado.nombreReal,
                    };

                    try
                    {
                        var resultado = await ReportaGrupo(grupoModificado.nombreReal, txtTipo.Text, txtDesc.Text);
                        lblResultado.Text = resultado;
                        MessageBox.Show(resultado.ToString());
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ocurrió un error al modificar el grupo: {ex.Message}");
                    }
                }
                else
                {
                    MessageBox.Show("El índice seleccionado está fuera de los límites.");
                }
            }
            else
            {
                MessageBox.Show("No seleccionaste un grupo");
            }
        }
        /*
        public static async Task<string> Reporta(string usuario, string tipo, string descripcion)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var datos = new { usuario = usuario, tipo = tipo, descripcion = descripcion };
                    var content = new StringContent(JsonConvert.SerializeObject(datos), Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PostAsync("https://localhost:44383/user/ReportarUsuario", content);
                    response.EnsureSuccessStatusCode();
                    var responseBody = await response.Content.ReadAsStringAsync();
                    dynamic data = JsonConvert.DeserializeObject(responseBody);
                    return data;
                }
                catch
                {
                    return "Reporte fallido";
                }
            }
        }
        */
        public static async Task<string> ReportaGrupo(string nombreReal , string tipo, string descripcion)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var datos = new { nombreDeCuenta = user, nombreGrupo = nombreReal , tipo = tipo, descripcion = descripcion};
                    var content = new StringContent(JsonConvert.SerializeObject(datos), Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PostAsync("https://localhost:44383/user/Reportar", content);
                    response.EnsureSuccessStatusCode();
                    var responseBody = await response.Content.ReadAsStringAsync();
                    dynamic data = JsonConvert.DeserializeObject(responseBody);
                    return data;
                }
                catch
                {
                    return "Falla";
                }
            }
        }
        public async Task Buscar(string usuario, string nombreGrupo)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var datos = new { nombreDeCuenta = usuario, nombreVisible = nombreGrupo };
                    var contentGrupo = new StringContent(JsonConvert.SerializeObject(datos), Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PutAsync("https://localhost:44304/ObtenerGruposPorNombreVisibleYUsuario", contentGrupo);
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

        private async void btnBuscar_Click(object sender, EventArgs e)
        {
            await Buscar(txtUsuario.Text, txtGrupo.Text);
        }
    }
}
