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
using System.Xml.Serialization;

namespace APIpostYeventos
{
    public partial class AñadirPost : Form
    {
        public AñadirPost()
        {
            InitializeComponent();
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            Form1 f1=new Form1();
            f1.Show();
            this.Close();
        }

        private void btnSeleccionar_Click(object sender, EventArgs e)
        {
             OpenFileDialog ofd=new OpenFileDialog();

            if (ofd.ShowDialog()== DialogResult.OK)
            {
                pbxImagen.ImageLocation = ofd.FileName;
                pbxImagen.SizeMode=PictureBoxSizeMode.StretchImage;
            }
        }

        private void btnPublicar_Click(object sender, EventArgs e)
        {
            List<Post> list = new List<Post>();
            Post p;

            if (File.Exists(@"Guardado.xml"))
            {
                FileStream fs2 = new FileStream(@"Guardado.xml", FileMode.Open, FileAccess.Read);
                using (fs2)
                {
                    XmlSerializer deserializer = new XmlSerializer(typeof(List<Post>));
                    list = (List<Post>)deserializer.Deserialize(fs2);
                }
                fs2.Close();
                p = new Post(list.Count + 1, txtTexto.Text, pbxImagen.ImageLocation, txtEnlace.Text);
                list.Add(p);
                FileStream fs3 = new FileStream(@"Guardado.xml", FileMode.Create, FileAccess.Write);
                using (fs3)
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(List<Post>));
                    serializer.Serialize(fs3, list);
                }
                fs3.Close();
            }
            else 
            {
                p = new Post(1, txtTexto.Text, pbxImagen.ImageLocation, txtEnlace.Text);
                list.Add(p);
                FileStream fs = new FileStream(@"Guardado.xml", FileMode.Create, FileAccess.Write);
                using (fs)
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(List<Post>));
                    serializer.Serialize(fs, list);
                }
                fs.Close();
            }

           
        }
    }
}
