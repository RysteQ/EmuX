using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace EmuX
{
    internal class Analyzer
    {
        public void SetInstructions(string instructions_to_analyze)
        {
            this.instructions_to_analyze = instructions_to_analyze;
        }

        public void AnalyzeInstructions()
        {
            // TODO
        }

        public bool AnalyzingSuccessful()
        {
            return this.successful;
        }

        public List<Instruction> GetInstructions()
        {
            return this.instructions;
        }

        private string instructions_to_analyze;
        private List<Instruction> instructions = new List<Instruction>();
        private bool successful = false;
    }
}
