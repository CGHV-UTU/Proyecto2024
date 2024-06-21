using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIpostYeventos
{
    public class Post
    {
        public Post() { }
        public Post(int id,string texto,string imagen, string linkyt) 
        {
            this.id = id;
            this.texto = texto;
            this.imagen = imagen;
            this.linkYT = linkyt;
        }
        public int id {  get; set; }
        public string texto { get; set; }
        public string imagen { get; set; }
        public string linkYT { get; set; }
    }
}
