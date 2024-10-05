using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
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
        public Comunidad(string modo, string user, string token)
        {
            this.modo = modo;
            this.user = user;
            this.token = token;
            InitializeComponent();
            Iniciar();
        }

        private async void Iniciar()
        {
            
            this.SuspendLayout();
            // panelPosts
            this.PanelMostrar.AutoScroll = true;
            this.PanelMostrar.Dock = DockStyle.Fill;
            this.PanelMostrar.Location = new System.Drawing.Point(58, 69);
            this.PanelMostrar.Name = "PanelMostrar";
            this.PanelMostrar.Size = new System.Drawing.Size(893, 493);
            this.PanelMostrar.TabIndex = 0;
            this.BackColor = Color.LightGray;
            // Form1
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1012, 613);
            this.Controls.Add(this.PanelMostrar);
            this.Name = "Form1";
            this.Text = "Infinite Scroll Posts";
            this.ResumeLayout(false);
        }
    }
}
