using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmuX
{
    internal class Emulator
    {
        public void SetData(List<Instruction> instructions)
        {
            this.instructions = instructions;
        }

        public void Execute()
        {
            // TODO
        }

        private List<Instruction> instructions = new List<Instruction>();
    }
}
