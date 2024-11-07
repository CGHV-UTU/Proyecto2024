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
    public partial class Busqueda : Form
    {
        private string token;
        private string user;
        private string nombreGrupo;
        private string modo;
        public event EventHandler<PersonalizedArgs> AbrirUsuario;
        public event EventHandler<PersonalizedArgs> AbrirEvento;
        public Busqueda(string user,string token, string nombreGrupo="", string modo="")
        {
            this.token = token;
            this.user = user;
            this.nombreGrupo = nombreGrupo;
            this.modo = modo;
            InitializeComponent();
            this.pnlOpciones.Visible = false;
            this.Size= new Size(1012, 342);
            pnlUsuario.Click += new EventHandler(pnlUsuario_Click);
            pnlGrupo.Click += new EventHandler(pnlGrupo_Click);
            pnlEvento.Click += new EventHandler(pnlEvento_Click);
            if (modo.Equals("Oscuro"))
            {
                this.BackColor= Color.FromArgb(45, 45, 45);
                pnlEvento.BackColor= Color.FromArgb(50, 50, 50);
                pnlGrupo.BackColor = Color.FromArgb(50, 50, 50);
                pnlUsuario.BackColor = Color.FromArgb(50, 50, 50);
                pnlOpciones.BackColor = Color.FromArgb(40, 40, 40);
                pnlMostrar.BackColor= Color.FromArgb(50, 50, 50);
                label1.ForeColor = Color.White;
                label2.ForeColor = Color.White;
                label3.ForeColor = Color.White;
                pictureBox3.Image = Frontend.Properties.Resources.User_negro;
                pictureBox4.Image = Frontend.Properties.Resources.Comunidad_Claro;
                pictureBox5.Image = Frontend.Properties.Resources.eventosBlanco;
                btnOpciones.Image = Frontend.Properties.Resources.mas_opciones_claro_relleno;
                btnBuscar.Image = Frontend.Properties.Resources.lupaBlanca;
                txtBusqueda.ForeColor = Color.White;
                txtBusqueda.BackColor= Color.FromArgb(50, 50, 50);
            }
            if (!string.IsNullOrEmpty(nombreGrupo))
            {
                this.Controls.Remove(pnlOpciones);
                tipoDeBusqueda = "usuarios";
                btnOpciones.Image = Frontend.Properties.Resources.User;
                pnlMostrar.Controls.Clear();
                pnlOpciones.Visible = false;
            }
        }
        static async Task<dynamic> BuscarUsuarios(string usuario, string token)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var datos = new { nombreVisible = usuario, token = token };
                    var content = new StringContent(JsonConvert.SerializeObject(datos), Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PutAsync($"https://localhost:44383/user/BuscarUsuarios", content);
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

        static async Task<dynamic> BuscarEventos(string titulo, string token)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var datos = new { titulo = titulo, token = token };
                    var content = new StringContent(JsonConvert.SerializeObject(datos), Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PutAsync($"https://localhost:44340/BuscarEventos", content);
                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();
                    dynamic data = JsonConvert.DeserializeObject(responseBody);
                    return data;
                }
                catch(Exception ex)
                {
                    return ex.Message;
                }
            }
        }
        static async Task<dynamic> BuscarGrupos(string nombreGrupo, string nombreUsuario, string token)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var datos = new { nombreDeCuenta=nombreUsuario, nombreVisible = nombreGrupo, token = token };
                    var content = new StringContent(JsonConvert.SerializeObject(datos), Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PutAsync($"https://localhost:44304/BuscarGrupos", content);
                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();
                    dynamic data = JsonConvert.DeserializeObject(responseBody);
                    return data;
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
            }
        }

        private void btnOpciones_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(nombreGrupo))
            {
                if (this.pnlOpciones.Visible == false)
                {
                    this.pnlOpciones.Visible = true;
                    this.pnlOpciones.Size = new Size(223, 183);
                    this.pnlOpciones.Location = new Point(19, 77);
                    this.pnlOpciones.Parent = this;
                }
                else
                {
                    this.pnlOpciones.Visible = false;
                }
            }
        }

        private async void btnBuscar_Click(object sender, EventArgs e)
        {
            switch (tipoDeBusqueda)
            {
                case "usuarios":
                    var respuesta = await BuscarUsuarios(txtBusqueda.Text, token);
                    if (Convert.ToString(respuesta).Equals("No se encontraron usuarios cuyos nombres concuerden con los parámetros de búsqueda especificados") || Convert.ToString(respuesta).Equals("Token expirado") || Convert.ToString(respuesta).Equals("Hubo un error"))
                    {
                        MessageBox.Show(Convert.ToString(respuesta));
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(nombreGrupo))
                        {
                            pnlMostrar.Controls.Clear();
                            foreach (dynamic usuario in respuesta)
                            {
                                var usercontrol = new Grupo_EventoParaListar(user, token, usuariobuscar: usuario, busqueda:true, modo:modo);
                                usercontrol.AbrirUsuario += Grupo_EventoParaListar_AbrirUsuario;
                                if (pnlMostrar.Controls.Count > 0)
                                {
                                    var lastControl = pnlMostrar.Controls[pnlMostrar.Controls.Count - 1];
                                    usercontrol.Location = new Point(0, lastControl.Bottom);
                                }
                                else
                                {
                                    usercontrol.Location = new Point(0, 52);
                                }
                                pnlMostrar.Controls.Add(usercontrol);
                            }
                        }
                        else
                        {
                            foreach (dynamic usuario in respuesta)
                            {
                                var usercontrol = new Grupo_EventoParaListar(user, token, usuariobuscar: usuario, busqueda: true, nombreGrupo: nombreGrupo);
                                usercontrol.AbrirUsuario += Grupo_EventoParaListar_AbrirUsuario;
                                if (pnlMostrar.Controls.Count > 0)
                                {
                                    var lastControl = pnlMostrar.Controls[pnlMostrar.Controls.Count - 1];
                                    usercontrol.Location = new Point(0, lastControl.Bottom);
                                }
                                else
                                {
                                    usercontrol.Location = new Point(0, 52);
                                }
                                pnlMostrar.Controls.Add(usercontrol);
                            }
                        }
                    }
                    break;
                case "grupos":
                    if (!txtBusqueda.Text.StartsWith("-"))
                    {
                        var respuesta2 = await BuscarGrupos(txtBusqueda.Text, user, token);
                        if (Convert.ToString(respuesta2).StartsWith("No se encontraron grupos cuyos nombres concuerden con los parámetros de búsqueda especificados") || Convert.ToString(respuesta2).Equals("Token expirado") || Convert.ToString(respuesta2).Equals("Hubo un error"))
                        {
                            MessageBox.Show(Convert.ToString(respuesta2));
                        }
                        else
                        {
                                pnlMostrar.Controls.Clear();
                                foreach (dynamic grupo in respuesta2)
                                {
                                var groupcontrol = new Grupo_EventoParaListar(user, token, grupo, busqueda:true,modo:modo);
                                    if (pnlMostrar.Controls.Count > 0)
                                    {
                                        var lastControl = pnlMostrar.Controls[pnlMostrar.Controls.Count - 1];
                                        groupcontrol.Location = new Point(0, lastControl.Bottom);
                                    }
                                    else
                                    {
                                        groupcontrol.Location = new Point(0, 52);
                                    }
                                    pnlMostrar.Controls.Add(groupcontrol);
                                }
                        }
                    }
                    break;
                case "eventos":
                    var respuesta3 = await BuscarEventos(txtBusqueda.Text, token);
                    if (Convert.ToString(respuesta3).Equals("No se encontraron eventos cuyos nombres concuerden con los parámetros de búsqueda especificados") || Convert.ToString(respuesta3).Equals("Token expirado") || Convert.ToString(respuesta3).Equals("Hubo un error"))
                    {
                        MessageBox.Show(Convert.ToString(respuesta3));
                    }
                    else
                    {
                        pnlMostrar.Controls.Clear();
                        foreach (dynamic evento in respuesta3)
                        {
                            var eventControl = new Grupo_EventoParaListar("", token, evento: evento, busqueda: true, modo:modo) ;
                            eventControl.AbrirEvento += Grupo_EventoParaListar_AbrirEvento;
                            if (pnlMostrar.Controls.Count > 0)
                            {
                                var lastControl = pnlMostrar.Controls[pnlMostrar.Controls.Count - 1];
                                eventControl.Location = new Point(0, lastControl.Bottom);
                            }
                            else
                            {
                                eventControl.Location = new Point(0, 52);
                            }
                            pnlMostrar.Controls.Add(eventControl);
                        }
                    }
                    break;
            }
        }
        private void Grupo_EventoParaListar_AbrirUsuario(object sender, PersonalizedArgs e)
        {
            AbrirUsuario?.Invoke(this, new PersonalizedArgs(e.arg));
        }
        private void Grupo_EventoParaListar_AbrirEvento(object sender, PersonalizedArgs e)
        {
            AbrirEvento?.Invoke(this, new PersonalizedArgs(e.arg));
        }
        private string tipoDeBusqueda;
        private void pnlUsuario_Click(object sender, EventArgs e)
        {
            tipoDeBusqueda = "usuarios";
            if (modo.Equals("Oscuro"))
            {
                btnOpciones.Image = Frontend.Properties.Resources.User_negro;
            }
            else
            {
                btnOpciones.Image = Frontend.Properties.Resources.User;
            }
            pnlMostrar.Controls.Clear();
            pnlOpciones.Visible = false;
        }
        private void pnlGrupo_Click(object sender, EventArgs e)
        {
            tipoDeBusqueda = "grupos";
            if (modo.Equals("Oscuro"))
            {
                btnOpciones.Image = Frontend.Properties.Resources.Comunidad_Claro;
            }
            else
            {
                btnOpciones.Image = Frontend.Properties.Resources.Comunidad;
            }
            pnlMostrar.Controls.Clear();
            pnlOpciones.Visible = false;
        }
        private void pnlEvento_Click(object sender, EventArgs e)
        {
            tipoDeBusqueda = "eventos";
            if (modo.Equals("Oscuro"))
            {
                btnOpciones.Image = Frontend.Properties.Resources.eventosBlanco;
            }
            else
            {
                btnOpciones.Image = Frontend.Properties.Resources.eventos_removebg_preview;
            }
            pnlMostrar.Controls.Clear();
            pnlOpciones.Visible = false;
        }
    }
}
