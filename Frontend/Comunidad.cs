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

namespace Frontend
{
    public partial class Comunidad : Form
    {
        private string modo;
        private string user;
        private string token;
        private bool eventosCargados = false;
        private bool gruposCargados = false;
        private DataTable eventos;
        public event EventHandler<PersonalizedArgs> AbrirEvento;
        public event EventHandler<PersonalizedArgs> AbrirGrupo;
        public Comunidad(string modo, string user, string token)
        {
            this.modo = modo;
            this.user = user;
            this.token = token;
            InitializeComponent();
            Iniciar();
        }

        private void Iniciar()
        {
            this.SuspendLayout();
            // panelPosts
            this.PanelGrupos.AutoScroll = true;
            this.PanelGrupos.Dock = DockStyle.Fill;
            this.PanelGrupos.Location = new System.Drawing.Point(58, 69);
            this.PanelGrupos.Name = "PanelMostrar";
            this.PanelGrupos.Size = new System.Drawing.Size(893, 493);
            this.PanelGrupos.TabIndex = 0;
            this.BackColor = Color.LightGray;
            // Form1
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1012, 613);
            this.Controls.Add(this.PanelGrupos);
            this.Name = "Form1";
            this.Text = "Infinite Scroll Posts";
            this.ResumeLayout(false);
        }

        static async Task<DataTable> Eventos(string usuario, string token)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var datos = new { user=usuario, token = token };
                    var content = new StringContent(JsonConvert.SerializeObject(datos), Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PutAsync($"https://localhost:44340/eventoParticipa", content);
                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();
                    dynamic data = JsonConvert.DeserializeObject<DataTable>(responseBody);
                    return data;
                }
                catch
                {
                    return null;
                }
            }
        }

        static async Task<dynamic> grupos(string usuario, string token)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var datos = new { nombreDeCuenta = usuario, token = token };
                    var content = new StringContent(JsonConvert.SerializeObject(datos), Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PutAsync($"https://localhost:44304/ObtenerGruposPorUsuario", content);
                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();
                    dynamic data = JsonConvert.DeserializeObject(responseBody);
                    return data;
                }
                catch
                {
                    return null;
                }
            }
        }

        private async void CargarEventos()
        {
            
            eventos = await Eventos(user, token);
            if (eventos != null)
            {
                //PanelGrupos.Controls.Clear();
                for (int i = 0; i < eventos.Rows.Count; i++)
                {
                    int idevento = Convert.ToInt32(eventos.Rows[i]["idEvento"]);
                    var eventControl = new Grupo_EventoParaListar(user, token, "",idevento);
                    eventControl.AbrirEvento += Grupo_EventoParaListar_AbrirEvento;
                    // probando, antes iba debajo del else
                    if (panelEventos.Controls.Count > 0)
                    {
                        var lastControl = panelEventos.Controls[panelEventos.Controls.Count - 1];
                        eventControl.Location = new Point(0, lastControl.Bottom);
                        MessageBox.Show(""+eventControl.Location);
                    }
                    else
                    {
                        eventControl.Location = new Point(0, 52);
                        MessageBox.Show(""+eventControl.Location);
                    }
                    panelEventos.Controls.Add(eventControl);
                    // aca
                }
            }
        }
        private void Grupo_EventoParaListar_AbrirEvento(object sender, PersonalizedArgs e)
        {
            AbrirEvento?.Invoke(this, new PersonalizedArgs(e.arg));
        }
        private void Grupo_EventoParaListar_AbrirGrupo(object sender, PersonalizedArgs e)
        {
            AbrirGrupo?.Invoke(this, new PersonalizedArgs(e.arg));
        }

        private async void CargarGrupos()
        {
            var lista = await grupos(user, token);
            if (lista != null)
            {
                foreach(var elemento in lista)
                {
                    var eventControl = new Grupo_EventoParaListar(user, token, Convert.ToString(elemento.nombreReal), 0);
                    eventControl.AbrirGrupo += Grupo_EventoParaListar_AbrirGrupo;
                    // probando, antes iba debajo del else
                    if (PanelGrupos.Controls.Count > 0)
                    {
                        var lastControl = PanelGrupos.Controls[PanelGrupos.Controls.Count - 1];
                        eventControl.Location = new Point(0, lastControl.Bottom);
                    }
                    else
                    {
                        eventControl.Location = new Point(0, 52);
                    }
                    PanelGrupos.Controls.Add(eventControl);
                    // aca
                }
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        

        private void PictureBoxGrupos_Click(object sender, EventArgs e)
        {
            PictureBoxEventos.Image = Frontend.Properties.Resources.eventos_removebg_preview;
            PictureBoxGrupos.Image = Frontend.Properties.Resources.grupos_seleccionar_removebg_preview__1_;
            pictureBox5.Visible = true;
            pictureBox6.Visible = false;
            PanelGrupos.Visible = true;
            panelEventos.Visible = false;
            PanelGrupos.Parent = this;
            if (!gruposCargados)
            {
                CargarGrupos();
                gruposCargados = true;
            }
                
        }

        private void PictureBoxEventos_Click(object sender, EventArgs e)
        {
            PictureBoxEventos.Image = Frontend.Properties.Resources.eventos_seleccionado_removebg_preview1;
            PictureBoxGrupos.Image = Frontend.Properties.Resources.grupos_removebg_preview;
            pictureBox5.Visible = false;
            pictureBox6.Visible = true;
            PanelGrupos.Visible = false;
            panelEventos.Visible = true;
            panelEventos.Parent = this;
            if (!eventosCargados)
            {
                CargarEventos();
                eventosCargados = true;
            }
            
        }

        private void Comunidad_Load(object sender, EventArgs e)
        {

        }
    }
}
