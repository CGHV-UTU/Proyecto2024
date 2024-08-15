using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Frontend
{
    public partial class Inicio : Form
    {
        private string user;
        public Inicio(string usuario)
        {
            InitializeComponent();
            VerPosts();
            PanelComentarios.Visible = false;
            PictureBoxSalir.Visible = false;
            user = usuario;
        }

        // cargar form de posts. -Puse un fondo gris para distinguirlo    
        private void VerPosts()
        {
            Posts post = new Posts();
            post.TopLevel = false;
            post.FormBorderStyle = FormBorderStyle.None;
            post.BackColor = Color.LightGray;
            post.Dock = DockStyle.Fill;
            post.AbrirComentarios += PostControl_AbrirComentarios;
            PanelPosts.Controls.Add(post);
            post.Show();
        }
        private void PostControl_AbrirComentarios(object sender, EventArgs e)
        {
            VerComentarios();
            PanelComentarios.Visible = true;
            PictureBoxSalir.Visible = true;
        }
        private void VerComentarios()
        {
            Comentarios comentario = new Comentarios();
            comentario.TopLevel = false;
            comentario.FormBorderStyle = FormBorderStyle.None;
            comentario.BackColor = Color.LightGray;
            comentario.Dock = DockStyle.Fill;
            PanelComentarios.Controls.Add(comentario);
            comentario.Show();
        }

        private void PictureBoxSalir_Click(object sender, EventArgs e)
        {
            PanelComentarios.Visible = false;
            PictureBoxSalir.Visible = false;
        }
    }
}
