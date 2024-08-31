using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frontend
{
    public class PersonalizedArgs : EventArgs
    {
        public string arg { get; set; }

        public PersonalizedArgs(string arg)
        {
            this.arg = arg;
        }
    }
}
