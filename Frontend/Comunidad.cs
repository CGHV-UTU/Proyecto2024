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
        private string idpost;
        private dynamic eventos;
        public event EventHandler<PersonalizedArgs> AbrirEvento;
        public event EventHandler<PersonalizedArgs> AbrirGrupo;
        public Comunidad(string modo, string user, string token, string idpost="")
        {
            this.modo = modo;
            this.user = user;
            this.token = token;
            this.idpost = idpost;
            InitializeComponent();
            Iniciar();
            PictureBoxEventos.Image = Frontend.Properties.Resources.eventos_removebg_preview;
            PictureBoxGrupos.Image = Frontend.Properties.Resources.grupos_seleccionar_removebg_preview__1_;
            pictureBox5.Visible = true;
            pictureBox6.Visible = false;
            PanelGrupos.Visible = true;
            panelEventos.Visible = false;
            PanelGrupos.Parent = this;
            if (modo.Equals("Oscuro"))
            {
                this.BackColor = Color.FromArgb(40, 40, 40);
                this.PanelGrupos.BackColor = Color.FromArgb(50, 50, 50);
                this.panelEventos.BackColor = Color.FromArgb(50, 50, 50);
                this.PictureBoxGrupos.Image = Frontend.Properties.Resources.chat_blanco_removebg_preview;
                this.PictureBoxEventos.Image = Frontend.Properties.Resources.evento_blanco_removebg_preview;
            }
            else
            {
                this.PictureBoxGrupos.Image = Frontend.Properties.Resources.chat_negro_removebg_preview__1_;
                this.PictureBoxEventos.Image = Frontend.Properties.Resources.evento_negro_removebg_preview;
            }
            CargarGrupos();
        }

        private void Iniciar()
        {
            this.SuspendLayout();
            // panelGrupos
            this.PanelGrupos.AutoScroll = true;
            this.PanelGrupos.Location = panelEventos.Location;
            this.PanelGrupos.Name = "PanelMostrar";
            this.PanelGrupos.Size = new System.Drawing.Size(357, 493);
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
            if (!string.IsNullOrEmpty(idpost))
            {
                PictureBoxGrupos.Visible = false;
                PictureBoxEventos.Visible = false;
                pictureBox5.Visible = false;
                pictureBox6.Visible = false;
                CargarGrupos();
            }
        }

        static async Task<dynamic> Eventos(string usuario, string token)
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
                    dynamic data = JsonConvert.DeserializeObject(responseBody);
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
            if (eventos != null && !Convert.ToString(eventos).Equals("Error"))
            {
                //PanelGrupos.Controls.Clear();
                foreach(var evento in eventos)
                {
                    var eventControl = new Grupo_EventoParaListar(user, token, evento:evento, modo: modo);
                    eventControl.AbrirEvento += Grupo_EventoParaListar_AbrirEvento;
                    // probando, antes iba debajo del else
                    if (panelEventos.Controls.Count > 0)
                    {
                        var lastControl = panelEventos.Controls[panelEventos.Controls.Count - 1];
                        eventControl.Location = new Point(0, lastControl.Bottom);
                    }
                    else
                    {
                        eventControl.Location = new Point(0, 52);
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
            AbrirGrupo?.Invoke(this, new PersonalizedArgs(e.arg,e.arg2));
        }
        static async Task<dynamic> RolEnElGrupo(string nombreReal, string nombre, string token)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var datos = new { nombreDeCuenta = nombre, nombreReal = nombreReal, token = token };
                    var content = new StringContent(JsonConvert.SerializeObject(datos), Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PutAsync($"https://localhost:44304/ObtenerRolDelUsuarioEnElGrupo", content);
                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();
                    dynamic data = JsonConvert.DeserializeObject(responseBody);
                    return data;
                }
                catch
                {
                    return "ERROR";
                }
            }
        }
        private async void CargarGrupos()
        {
            var lista = await grupos(user, token);
            if (lista != null)
            {
                if (string.IsNullOrEmpty(idpost))
                {
                    foreach (var elemento in lista)
                    {
                        var groupcontrol = new Grupo_EventoParaListar(user, token, elemento, modo:modo);
                        groupcontrol.AbrirGrupo += Grupo_EventoParaListar_AbrirGrupo;
                        if (PanelGrupos.Controls.Count > 0)
                        {
                            var lastControl = PanelGrupos.Controls[PanelGrupos.Controls.Count - 1];
                            groupcontrol.Location = new Point(0, lastControl.Bottom);
                        }
                        else
                        {
                            groupcontrol.Location = new Point(0, 52);
                        }
                        PanelGrupos.Controls.Add(groupcontrol);
                    }
                }
                else
                {
                    panelEventos.Visible = false;
                    PanelGrupos.Controls.Clear();
                    foreach (var elemento in lista)
                    {
                        var eventControl = new Grupo_EventoParaListar(user, token, elemento, idpost:idpost, modo: modo);
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
                    }
                }
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        

        private void PictureBoxGrupos_Click(object sender, EventArgs e)
        {
            pictureBox5.Visible = true;
            pictureBox6.Visible = false;
            PanelGrupos.Visible = true;
            panelEventos.Visible = false;
            PanelGrupos.Parent = this;
            if (PanelGrupos.Controls.Count<1)
            {
                CargarGrupos();
            }
        }

        private void PictureBoxEventos_Click(object sender, EventArgs e)
        {
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
    }
}
