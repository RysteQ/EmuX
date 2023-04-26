using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmuX.src.Models
{
    public class Label
    {
        public Label(string name, int line)
        {
            this.name = name;
            this.line = line;
        }

        public string name = string.Empty;
        public int line;
    }
}
