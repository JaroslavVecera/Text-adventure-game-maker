using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextGameEditor
{
    public class Variable
    {
        public string Name { get; set; }
        public bool State { get; set; }

        public Variable()
        {
            Name = "";
            State = true;
        }
    }
}
