using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
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

        public async Task<dynamic> EditarGrupo(Grupo grupo)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var content = new StringContent(JsonConvert.SerializeObject(grupo), Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PutAsync($"https://localhost:44304/EditarGrupo", content);
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

        private async void btnModificar_Click(object sender, EventArgs e)
        {
            string configuracion = "TodosHablan";
            if (rbtnAdminHabla.Checked)
            {
                configuracion = "AdminHabla";
            }

            MemoryStream ms = new MemoryStream();
            pictureBox1.Image.Save(ms, ImageFormat.Jpeg);
            byte[] imagen = ms.ToArray();
            string foto = Convert.ToBase64String(imagen);

            if (verificarDatos(txtNombre.Text, foto, configuracion))
            {
                if (string.IsNullOrEmpty(txtDescripcion.Text))
                {
                    txtDescripcion.Text = "";
                }

                if (dataGridViewGrupos.CurrentCell != null && dataGridViewGrupos.CurrentCell.Selected)
                {
                    int indice = dataGridViewGrupos.CurrentCell.RowIndex;

                    if (indice >= 0 && indice < listaGrupos.Count)
                    {
                        Grupo grupoSeleccionado = listaGrupos[indice];

                        Grupo grupoModificado = new Grupo
                        {
                            nombreReal = grupoSeleccionado.nombreReal,
                            nombreVisible = txtNombreVisible.Text,
                            descripcion = txtDescripcion.Text,
                            imagen = foto,
                            configuracion = configuracion,
                            nombreDeCuenta = user
                        };

                        try
                        {
                            dynamic resultado = await EditarGrupo(grupoModificado);
                            MessageBox.Show(resultado.ToString());
                            listaGrupos[indice] = grupoModificado;

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
            else
            {
                MessageBox.Show("Datos inválidos");
            }
        }



        private void btnImagen_Click_1(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                Image selectedImage = Image.FromFile(ofd.FileName);
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                pictureBox1.Image = selectedImage;
            }
        }
    }
}
