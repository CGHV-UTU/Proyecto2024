using System;
using System.Collections;
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
    public partial class EliminarPost : Form
    {
        public EliminarPost()
        {
            InitializeComponent();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            List<Post> posts = new List<Post>();
            XmlSerializer deserializer =new XmlSerializer(typeof(List<Post>));
            using (FileStream fs=new FileStream(@"Guardado.xml", FileMode.Open, FileAccess.Read))
            {
                posts=(List<Post>)deserializer.Deserialize(fs);
                fs.Close();
            }
            foreach(Post p in posts)
            {
                if (p.id == int.Parse(txtID.Text))
                {
                    posts.Remove(p);
                    break;
                }
            }
            FileStream fs2 = new FileStream(@"Guardado.xml", FileMode.Create, FileAccess.Write);
            using (fs2)
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<Post>));
                serializer.Serialize(fs2, posts);
            }
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();
            this.Close();
        }
    }
}
