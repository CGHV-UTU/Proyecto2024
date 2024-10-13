
namespace Frontend
{
    partial class Comunidad
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.PanelGrupos = new System.Windows.Forms.Panel();
            this.panelEventos = new System.Windows.Forms.Panel();
            this.pictureBox6 = new System.Windows.Forms.PictureBox();
            this.pictureBox5 = new System.Windows.Forms.PictureBox();
            this.PictureBoxEventos = new System.Windows.Forms.PictureBox();
            this.PictureBoxGrupos = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBoxEventos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBoxGrupos)).BeginInit();
            this.SuspendLayout();
            // 
            // PanelGrupos
            // 
            this.PanelGrupos.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PanelGrupos.Location = new System.Drawing.Point(348, 69);
            this.PanelGrupos.Name = "PanelGrupos";
            this.PanelGrupos.Size = new System.Drawing.Size(357, 493);
            this.PanelGrupos.TabIndex = 3;
            // 
            // panelEventos
            // 
            this.panelEventos.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelEventos.Location = new System.Drawing.Point(348, 69);
            this.panelEventos.Name = "panelEventos";
            this.panelEventos.Size = new System.Drawing.Size(357, 493);
            this.panelEventos.TabIndex = 4;
            // 
            // pictureBox6
            // 
            this.pictureBox6.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox6.Image = global::Frontend.Properties.Resources.subrayado_removebg_preview;
            this.pictureBox6.Location = new System.Drawing.Point(588, 33);
            this.pictureBox6.Name = "pictureBox6";
            this.pictureBox6.Size = new System.Drawing.Size(128, 21);
            this.pictureBox6.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox6.TabIndex = 8;
            this.pictureBox6.TabStop = false;
            // 
            // pictureBox5
            // 
            this.pictureBox5.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox5.Image = global::Frontend.Properties.Resources.subrayado_removebg_preview;
            this.pictureBox5.Location = new System.Drawing.Point(336, 33);
            this.pictureBox5.Name = "pictureBox5";
            this.pictureBox5.Size = new System.Drawing.Size(128, 21);
            this.pictureBox5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox5.TabIndex = 7;
            this.pictureBox5.TabStop = false;
            // 
            // PictureBoxEventos
            // 
            this.PictureBoxEventos.Image = global::Frontend.Properties.Resources.eventos_removebg_preview;
            this.PictureBoxEventos.Location = new System.Drawing.Point(603, -10);
            this.PictureBoxEventos.Name = "PictureBoxEventos";
            this.PictureBoxEventos.Size = new System.Drawing.Size(100, 73);
            this.PictureBoxEventos.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.PictureBoxEventos.TabIndex = 6;
            this.PictureBoxEventos.TabStop = false;
            this.PictureBoxEventos.Click += new System.EventHandler(this.PictureBoxEventos_Click);
            // 
            // PictureBoxGrupos
            // 
            this.PictureBoxGrupos.Image = global::Frontend.Properties.Resources.grupos_removebg_preview;
            this.PictureBoxGrupos.Location = new System.Drawing.Point(349, -10);
            this.PictureBoxGrupos.Name = "PictureBoxGrupos";
            this.PictureBoxGrupos.Size = new System.Drawing.Size(100, 73);
            this.PictureBoxGrupos.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.PictureBoxGrupos.TabIndex = 5;
            this.PictureBoxGrupos.TabStop = false;
            // 
            // Comunidad
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(996, 574);
            this.Controls.Add(this.PanelGrupos);
            this.Controls.Add(this.panelEventos);
            this.Controls.Add(this.pictureBox6);
            this.Controls.Add(this.pictureBox5);
            this.Controls.Add(this.PictureBoxEventos);
            this.Controls.Add(this.PictureBoxGrupos);
            this.Name = "Comunidad";
            this.Text = "Comunidad";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBoxEventos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBoxGrupos)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel PanelGrupos;
        private System.Windows.Forms.PictureBox PictureBoxGrupos;
        private System.Windows.Forms.PictureBox PictureBoxEventos;
        private System.Windows.Forms.PictureBox pictureBox5;
        private System.Windows.Forms.PictureBox pictureBox6;
        private System.Windows.Forms.Panel panelEventos;
    }
}