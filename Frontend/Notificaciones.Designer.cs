﻿
namespace Frontend
{
    partial class Notificaciones
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
            this.PanelNotificaciones = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // PanelNotificaciones
            // 
            this.PanelNotificaciones.Location = new System.Drawing.Point(75, 42);
            this.PanelNotificaciones.Name = "PanelNotificaciones";
            this.PanelNotificaciones.Size = new System.Drawing.Size(634, 477);
            this.PanelNotificaciones.TabIndex = 19;
            // 
            // Notificaciones
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.PanelNotificaciones);
            this.Name = "Notificaciones";
            this.Text = "Notificaciones";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel PanelNotificaciones;
    }
}