using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Frontend
{
    public partial class CommentControl : UserControl
    {
        private string idpost;
        private string user;
        private int idcomentario;
        public event EventHandler<PersonalizedArgs> ReportarComentario;
        public CommentControl(string modo,string idpost, int idcomentario,string user)
        {
            this.idpost = idpost;
            this.idcomentario = idcomentario;
            this.user = user;
            InitializeComponent();
            Iniciar();
            txtBox.ReadOnly = true;
            AjustarTamaño();
            aplicarDatos();
        }
        static async Task<string[]> BuscarComentario(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var datos = new { id=id };
                    var content = new StringContent(JsonConvert.SerializeObject(datos), Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PutAsync($"https://localhost:44340/conseguirComentario",content);
                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();
                    dynamic data = JsonConvert.DeserializeObject(responseBody);
                    return new string[] { data.NombreDeCuenta, data.texto, data.fechayhora };
                }
                catch
                {
                    return null;
                }
            }
        }
        static async Task<string> conseguirImagenDelCreador(string creador)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync($"https://localhost:44383/user/obtenerImagenUsuario?nombredecuenta={creador}");
                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();
                    dynamic imagen = JsonConvert.DeserializeObject(responseBody);
                    return imagen;
                }
                catch
                {
                    MessageBox.Show("Error de conexión");
                    return "error";
                }
            }
        }
        static async Task<string> darLike(string NombreDeCuenta, int IdPost, string nombreCreador)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var datos = new { NombreDeCuenta = NombreDeCuenta, idpost = IdPost, nombredeCreador = nombreCreador };
                    var content = new StringContent(JsonConvert.SerializeObject(datos), Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PostAsync("https://localhost:44340/darLikeComentario", content);
                    response.EnsureSuccessStatusCode();
                    var responseBody = await response.Content.ReadAsStringAsync();
                    dynamic data = JsonConvert.DeserializeObject(responseBody);
                    return data;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("ERROR" + ex);
                    return "like erroneo";
                }
            }
        }
        static async Task<bool> dioLike(string NombreDeCuenta, int IdPost, string nombreCreador)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var datos = new { NombreDeCuenta = NombreDeCuenta, idpost = IdPost, nombredeCreador = nombreCreador };
                    var content = new StringContent(JsonConvert.SerializeObject(datos), Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PostAsync("https://localhost:44340/dioLikeComentario", content);
                    response.EnsureSuccessStatusCode();
                    var responseBody = await response.Content.ReadAsStringAsync();
                    dynamic data = JsonConvert.DeserializeObject(responseBody);
                    return data;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }
        static async Task<string> quitarLike(string NombreDeCuenta, int IdPost, string nombreCreador)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var datos = new { NombreDeCuenta = NombreDeCuenta, idpost = IdPost, nombredeCreador = nombreCreador };
                    var content = new StringContent(JsonConvert.SerializeObject(datos), Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PostAsync("https://localhost:44340/quitarLikeComentario", content);
                    response.EnsureSuccessStatusCode();
                    var responseBody = await response.Content.ReadAsStringAsync();
                    dynamic data = JsonConvert.DeserializeObject(responseBody);
                    return data;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("ERROR" + ex);
                    return "like erroneo";
                }
            }
        }
        private void AjustarTamaño()
        {
            txtBox.Size = new Size(this.Width - 70, this.Height - 65);
        }

        private async void aplicarDatos()
        {
            var data = await BuscarComentario(idcomentario);
            lblNombre.Text = data[0];
            txtBox.Text = data[1];
            if (lblNombre.Text.Equals(user))
            {
                //editar
                this.PictureBoxMasOpciones = new PictureBox();
                this.PictureBoxMasOpciones.Location = new System.Drawing.Point(410, 3);
                this.PictureBoxMasOpciones.Name = "PictureBoxMasOpciones";
                this.PictureBoxMasOpciones.Size = new System.Drawing.Size(50, 50);
                this.PictureBoxMasOpciones.SizeMode = PictureBoxSizeMode.StretchImage;
                this.PictureBoxMasOpciones.Image = Frontend.Properties.Resources.mas_opciones;
                this.PictureBoxMasOpciones.Click += PictureBoxMasOpciones_Click;
                this.PictureBoxMasOpciones.Cursor = Cursors.Hand;
                this.Controls.Add(this.PictureBoxMasOpciones);
            }
            string imagenB64 = await conseguirImagenDelCreador(lblNombre.Text);
            byte[] imagen = Convert.FromBase64String(imagenB64);
            MemoryStream ms = new MemoryStream(imagen);
            Bitmap bitmap = new Bitmap(ms);
            this.PictureBoxUsuario.Image = bitmap;
        }

        private bool isImage1 = true;

        private void PictureBoxLike_Click(object sender, EventArgs e)
        {
            if (isImage1)
            {
                PictureBoxLike.Image = Properties.Resources.Like_Relleno; 
                isImage1 = false;
            }
            else
            {
                PictureBoxLike.Image = Properties.Resources.like_infini; 
                isImage1 = true;
            }
        }

        private void Iniciar()
        {
            this.txtBoxEditar.Visible = false;
            this.lblNombre = new Label();
            this.txtBox = new RichTextBox();
            this.PictureBoxLike = new PictureBox();
            this.PictureBoxReportar = new PictureBox();
            this.PictureBoxUsuario = new PictureBox();
            this.SuspendLayout();

            // lblNombre
            this.lblNombre.AutoSize = true;
            this.lblNombre.Location = new System.Drawing.Point(59, 37);
            this.lblNombre.Name = "lblNombre";
            this.lblNombre.Size = new System.Drawing.Size(44, 13);
            this.lblNombre.TabIndex = 0;

            // txtBox
            this.txtBox.Location = new System.Drawing.Point(3, 53);
            this.txtBox.Name = "txtBox";
            this.txtBox.Size = new System.Drawing.Size(this.Width - 70, this.Height - 65);
            this.txtBox.TabIndex = 31;

            // PictureBoxUsuario
            this.PictureBoxUsuario.Location = new System.Drawing.Point(3, 3);
            this.PictureBoxUsuario.Name = "PictureBoxUsuario";
            this.PictureBoxUsuario.Size = new System.Drawing.Size(50, 50);
            this.PictureBoxUsuario.SizeMode = PictureBoxSizeMode.StretchImage;
            this.PictureBoxUsuario.Image = Properties.Resources.User;
            this.Cursor = Cursors.Hand;

            // PictureBoxLike
            this.PictureBoxLike.Location = new System.Drawing.Point(410, 53);
            this.PictureBoxLike.Name = "PictureBoxLike";
            this.PictureBoxLike.Size = new System.Drawing.Size(50, 50);
            this.PictureBoxLike.SizeMode = PictureBoxSizeMode.StretchImage;
            this.PictureBoxLike.Image = Properties.Resources.like_infini;
            this.PictureBoxLike.Click += PictureBoxLike_Click;
            this.Cursor = Cursors.Hand;

            // PictureBoxReportar
            this.PictureBoxReportar.Location = new System.Drawing.Point(410, 109);
            this.PictureBoxReportar.Name = "PictureBoxReportar";
            this.PictureBoxReportar.Size = new System.Drawing.Size(50,50);
            this.PictureBoxReportar.SizeMode = PictureBoxSizeMode.StretchImage;
            this.PictureBoxReportar.Image = Properties.Resources.reportar;
            this.PictureBoxReportar.Click += PictureBoxReportar_Click;
            this.Cursor = Cursors.Hand;

            // Añadir controles al CommentControl
            this.Controls.Add(this.lblNombre);
            this.Controls.Add(this.txtBox);
            this.Controls.Add(this.PictureBoxLike);
            this.Controls.Add(this.PictureBoxReportar);
            this.Controls.Add(this.PictureBoxUsuario);

            // Configuración final del control
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.Name = "CommentControl";
            this.Size = new System.Drawing.Size(465, 171);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void PictureBoxReportar_Click(object sender, EventArgs e)
        {
            ReportarComentario?.Invoke(this, new PersonalizedArgs("" + idpost, ""+idcomentario));
        }
        private bool opciones = false;
        private void PictureBoxMasOpciones_Click(object sender, EventArgs e)
        {
            if (!opciones)
            {
                this.btnEditar = new Label();
                this.btnEditar.Location = new System.Drawing.Point(360, 3);
                this.btnEditar.Name = "btnEditar";
                this.btnEditar.Size = new System.Drawing.Size(34, 13);
                this.btnEditar.TabIndex = 34;
                this.btnEditar.Click += btnEditar_Click;
                this.btnEditar.BackColor = Color.Blue;
                this.btnEditar.Text = "Editar";
                this.btnEditar.BringToFront();
                this.Controls.Add(this.btnEditar);

                this.btnEliminar = new Label();
                this.btnEliminar.Location = new System.Drawing.Point(360, 25);
                this.btnEliminar.Name = "btnEliminar";
                this.btnEliminar.Size = new System.Drawing.Size(34, 13);
                this.btnEliminar.TabIndex = 35;
                this.btnEliminar.Click += btnEliminar_Click;
                this.btnEliminar.BackColor = Color.Red;
                this.btnEliminar.Text = "Eliminar";
                this.btnEliminar.BringToFront();
                this.Controls.Add(this.btnEliminar);
                opciones = true;
            }
            else
            {
                this.Controls.Remove(this.btnEditar);
                this.Controls.Remove(this.btnEliminar);
                opciones = false;
            }
        }
        private bool editando = false;
        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (!editando)
            {
                this.txtBox.Visible = false;
                this.txtBoxEditar = new RichTextBox();
                this.txtBoxEditar.Location = new System.Drawing.Point(3, 53);
                this.txtBoxEditar.Name = "txtBoxEditar";
                this.txtBoxEditar.Size = new System.Drawing.Size(this.txtBoxEditar.Width, this.txtBoxEditar.Height);
                this.txtBoxEditar.TabIndex = 31;
                this.txtBoxEditar.Text = this.txtBox.Text;
                this.txtBoxEditar.ReadOnly = false;
                this.Controls.Add(txtBoxEditar);

                this.PictureBoxConfirmarCambios = new PictureBox();
                this.PictureBoxConfirmarCambios.Location = new System.Drawing.Point(202, 3);
                this.PictureBoxConfirmarCambios.Name = "PictureBoxConfirmarCambios";
                this.PictureBoxConfirmarCambios.Size = new System.Drawing.Size(50, 50);
                this.PictureBoxConfirmarCambios.SizeMode = PictureBoxSizeMode.StretchImage;
                this.PictureBoxConfirmarCambios.Image = Frontend.Properties.Resources.mas_opciones;
                this.PictureBoxConfirmarCambios.Click += PictureBoxConfirmarCambios_Click;
                this.PictureBoxConfirmarCambios.Cursor = Cursors.Hand;
                this.Controls.Add(this.PictureBoxConfirmarCambios);
                editando = true;
            }
            else
            {
                this.Controls.Remove(this.txtBoxEditar);
                this.Controls.Remove(this.PictureBoxConfirmarCambios);
                this.txtBox.Visible = true;
                editando = false;
            }
        }
        static async Task Eliminar(string id)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var datos = new { id=id};
                    var content = new StringContent(JsonConvert.SerializeObject(datos), Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PutAsync($"https://localhost:44340/eliminarComentario",content);
                    response.EnsureSuccessStatusCode();
                    MessageBox.Show("Comentario eliminado con éxito");
                }
                catch (Exception)
                {
                    MessageBox.Show("Fallo al eliminar comentario");
                }
            }
        }

        private async void btnEliminar_Click(object sender, EventArgs e)
        {
            await Eliminar(Convert.ToString(idcomentario));
        }
        static async Task Modificar(string id, string texto)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var data = new { id = id, texto = texto, };
                    var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PutAsync("https://localhost:44340/modificarComentario", content);
                    response.EnsureSuccessStatusCode();
                    MessageBox.Show("Comentario modificado correctamente");
                }
                catch (Exception)
                {
                    MessageBox.Show("Error");
                }
            }
        }
        private async void PictureBoxConfirmarCambios_Click(object sender, EventArgs e)
        {
            await Modificar(Convert.ToString(idcomentario),this.txtBoxEditar.Text);
        }

        private void PictureBoxLike_Click_1(object sender, EventArgs e)
        {
            
        }
    }
}
