using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BackofficeDeAdministracion
{
    public partial class Log : Form
    {
        public Log()
        {
            InitializeComponent();
            CargarLog();

        }
        private void CargarLog()
        {
            string path = @"C:\Users\emerg\Downloads\lbackofinal\Proyecto2024\Log.txt";
            if (File.Exists(path))
            {
                dataGridView1.ColumnCount = 3;
                dataGridView1.Columns[0].Name = "Fecha";
                dataGridView1.Columns[1].Name = "Administrador";
                dataGridView1.Columns[2].Name = "Acción";
                string[] lines = File.ReadAllLines(path);
                foreach (string line in lines)
                {
                    string[] parts = line.Split(new[] { ": " }, 2, StringSplitOptions.None);
                    if (parts.Length == 2)
                    {
                        string fecha = parts[0];
                        string[] details = parts[1].Split(new[] { ' ' }, 2, StringSplitOptions.None);
                        if (details.Length == 2)
                        {
                            string admin = details[0];
                            string accion = details[1];
                            dataGridView1.Rows.Add(fecha, admin, accion);
                            DataGridViewCellStyle columnHeaderStyle = new DataGridViewCellStyle();
                            columnHeaderStyle.Font = new Font("Verdana", 10, FontStyle.Bold);
                            dataGridView1.ColumnHeadersDefaultCellStyle = columnHeaderStyle;
                            dataGridView1.Columns[0].Width = 130;
                            dataGridView1.Columns[1].Width = 165;
                            dataGridView1.Columns[2].Width = 514; 

                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("El archivo no se encontró.");
            }
        }

    }
}
