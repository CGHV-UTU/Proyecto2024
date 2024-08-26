using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frontend
{
    public class ConfiguraEventArgs : EventArgs
    {
        public string Modo { get; set; }
        public string Idioma { get; set; }

        public ConfiguraEventArgs(string modo, string idioma)
        {
            Modo = modo;
            Idioma = idioma;
        }
    }
}
