﻿
namespace Frontend
{
    partial class Inicio
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Inicio));
            this.PanelSuperior = new System.Windows.Forms.Panel();
            this.PanelPosts = new System.Windows.Forms.Panel();
            this.PanelComentarios = new System.Windows.Forms.Panel();
            this.PictureBoxUsuario = new System.Windows.Forms.PictureBox();
            this.PictureboxLogo = new System.Windows.Forms.PictureBox();
            this.PictureBoxConfiguraciones = new System.Windows.Forms.PictureBox();
            this.PictureBoxNotificaciones = new System.Windows.Forms.PictureBox();
            this.PictureBoxCrear = new System.Windows.Forms.PictureBox();
            this.PictureBoxBuscar = new System.Windows.Forms.PictureBox();
            this.PictureBoxSalir = new System.Windows.Forms.PictureBox();
            this.PanelSuperior.SuspendLayout();
            this.PanelPosts.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBoxUsuario)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureboxLogo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBoxConfiguraciones)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBoxNotificaciones)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBoxCrear)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBoxBuscar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBoxSalir)).BeginInit();
            this.SuspendLayout();
            // 
            // PanelSuperior
            // 
            this.PanelSuperior.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.PanelSuperior.Controls.Add(this.PictureBoxUsuario);
            this.PanelSuperior.Controls.Add(this.PictureboxLogo);
            this.PanelSuperior.Controls.Add(this.PictureBoxConfiguraciones);
            this.PanelSuperior.Controls.Add(this.PictureBoxNotificaciones);
            this.PanelSuperior.Controls.Add(this.PictureBoxCrear);
            this.PanelSuperior.Controls.Add(this.PictureBoxBuscar);
            this.PanelSuperior.Dock = System.Windows.Forms.DockStyle.Top;
            this.PanelSuperior.Location = new System.Drawing.Point(0, 0);
            this.PanelSuperior.Name = "PanelSuperior";
            this.PanelSuperior.Size = new System.Drawing.Size(1264, 50);
            this.PanelSuperior.TabIndex = 0;
            // 
            // PanelPosts
            // 
            this.PanelPosts.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.PanelPosts.Controls.Add(this.PanelComentarios);
            this.PanelPosts.Location = new System.Drawing.Point(128, 56);
            this.PanelPosts.Name = "PanelPosts";
            this.PanelPosts.Size = new System.Drawing.Size(1012, 613);
            this.PanelPosts.TabIndex = 12;
            // 
            // PanelComentarios
            // 
            this.PanelComentarios.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.PanelComentarios.BackColor = System.Drawing.SystemColors.ControlDark;
            this.PanelComentarios.Location = new System.Drawing.Point(3, 3);
            this.PanelComentarios.Name = "PanelComentarios";
            this.PanelComentarios.Size = new System.Drawing.Size(1006, 607);
            this.PanelComentarios.TabIndex = 13;
            // 
            // PictureBoxUsuario
            // 
            this.PictureBoxUsuario.Cursor = System.Windows.Forms.Cursors.Hand;
            this.PictureBoxUsuario.Image = global::Frontend.Properties.Resources.User;
            this.PictureBoxUsuario.Location = new System.Drawing.Point(77, 0);
            this.PictureBoxUsuario.Name = "PictureBoxUsuario";
            this.PictureBoxUsuario.Size = new System.Drawing.Size(50, 50);
            this.PictureBoxUsuario.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PictureBoxUsuario.TabIndex = 4;
            this.PictureBoxUsuario.TabStop = false;
            // 
            // PictureboxLogo
            // 
            this.PictureboxLogo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.PictureboxLogo.Image = global::Frontend.Properties.Resources.Logo_Infini;
            this.PictureboxLogo.Location = new System.Drawing.Point(12, 0);
            this.PictureboxLogo.Name = "PictureboxLogo";
            this.PictureboxLogo.Size = new System.Drawing.Size(50, 50);
            this.PictureboxLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PictureboxLogo.TabIndex = 5;
            this.PictureboxLogo.TabStop = false;
            // 
            // PictureBoxConfiguraciones
            // 
            this.PictureBoxConfiguraciones.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.PictureBoxConfiguraciones.Cursor = System.Windows.Forms.Cursors.Hand;
            this.PictureBoxConfiguraciones.Image = global::Frontend.Properties.Resources.config;
            this.PictureBoxConfiguraciones.Location = new System.Drawing.Point(1202, 0);
            this.PictureBoxConfiguraciones.Name = "PictureBoxConfiguraciones";
            this.PictureBoxConfiguraciones.Size = new System.Drawing.Size(50, 50);
            this.PictureBoxConfiguraciones.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PictureBoxConfiguraciones.TabIndex = 1;
            this.PictureBoxConfiguraciones.TabStop = false;
            // 
            // PictureBoxNotificaciones
            // 
            this.PictureBoxNotificaciones.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.PictureBoxNotificaciones.Cursor = System.Windows.Forms.Cursors.Hand;
            this.PictureBoxNotificaciones.Image = global::Frontend.Properties.Resources.campana;
            this.PictureBoxNotificaciones.Location = new System.Drawing.Point(1146, 0);
            this.PictureBoxNotificaciones.Name = "PictureBoxNotificaciones";
            this.PictureBoxNotificaciones.Size = new System.Drawing.Size(50, 50);
            this.PictureBoxNotificaciones.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PictureBoxNotificaciones.TabIndex = 1;
            this.PictureBoxNotificaciones.TabStop = false;
            // 
            // PictureBoxCrear
            // 
            this.PictureBoxCrear.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.PictureBoxCrear.Cursor = System.Windows.Forms.Cursors.Hand;
            this.PictureBoxCrear.Image = global::Frontend.Properties.Resources.crear;
            this.PictureBoxCrear.Location = new System.Drawing.Point(623, 0);
            this.PictureBoxCrear.Name = "PictureBoxCrear";
            this.PictureBoxCrear.Size = new System.Drawing.Size(50, 50);
            this.PictureBoxCrear.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PictureBoxCrear.TabIndex = 3;
            this.PictureBoxCrear.TabStop = false;
            // 
            // PictureBoxBuscar
            // 
            this.PictureBoxBuscar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.PictureBoxBuscar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.PictureBoxBuscar.Image = global::Frontend.Properties.Resources.buscar;
            this.PictureBoxBuscar.Location = new System.Drawing.Point(1090, 0);
            this.PictureBoxBuscar.Name = "PictureBoxBuscar";
            this.PictureBoxBuscar.Size = new System.Drawing.Size(50, 50);
            this.PictureBoxBuscar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PictureBoxBuscar.TabIndex = 2;
            this.PictureBoxBuscar.TabStop = false;
            // 
            // PictureBoxSalir
            // 
            this.PictureBoxSalir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.PictureBoxSalir.Cursor = System.Windows.Forms.Cursors.Hand;
            this.PictureBoxSalir.Image = global::Frontend.Properties.Resources.salir;
            this.PictureBoxSalir.Location = new System.Drawing.Point(1146, 59);
            this.PictureBoxSalir.Name = "PictureBoxSalir";
            this.PictureBoxSalir.Size = new System.Drawing.Size(50, 50);
            this.PictureBoxSalir.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PictureBoxSalir.TabIndex = 6;
            this.PictureBoxSalir.TabStop = false;
            this.PictureBoxSalir.Click += new System.EventHandler(this.PictureBoxSalir_Click);
            // 
            // Inicio
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1264, 681);
            this.Controls.Add(this.PictureBoxSalir);
            this.Controls.Add(this.PanelPosts);
            this.Controls.Add(this.PanelSuperior);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(960, 540);
            this.Name = "Inicio";
            this.Text = "Inicio";
            this.PanelSuperior.ResumeLayout(false);
            this.PanelPosts.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBoxUsuario)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureboxLogo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBoxConfiguraciones)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBoxNotificaciones)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBoxCrear)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBoxBuscar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBoxSalir)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel PanelSuperior;
        private System.Windows.Forms.PictureBox PictureBoxConfiguraciones;
        private System.Windows.Forms.PictureBox PictureBoxNotificaciones;
        private System.Windows.Forms.PictureBox PictureBoxBuscar;
        private System.Windows.Forms.PictureBox PictureBoxCrear;
        private System.Windows.Forms.PictureBox PictureBoxUsuario;
        private System.Windows.Forms.PictureBox PictureboxLogo;
        private System.Windows.Forms.Panel PanelPosts;
        private System.Windows.Forms.Panel PanelComentarios;
        private System.Windows.Forms.PictureBox PictureBoxSalir;
    }
}
