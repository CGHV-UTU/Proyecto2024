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
        public string arg2 { get; set; }

        public PersonalizedArgs(string arg, string arg2="")
        {
            this.arg = arg;
            this.arg2 = arg2;
        }
    }
}
