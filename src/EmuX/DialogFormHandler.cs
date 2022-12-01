using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmuX
{
    internal class DialogFormHandler
    {
        public string openFileDialog(OpenFileDialog openFD)
        {
            openFD.FileName = "";
            openFD.Filter = "Text file (*.txt)|*.txt|Assenbly file (*.asm)|*.asm|All files (*.*)|*.*";

            if (openFD.ShowDialog() == DialogResult.OK)
            {
                return openFD.FileName;
            }

            return null;
        }

        public string saveFileDialog(SaveFileDialog saveFD)
        {
            saveFD.FileName = "";
            saveFD.Filter = "Text file (*.txt)|*.txt|Assenbly file (*.asm)|*.asm|All files (*.*)|*.*";

            if (saveFD.ShowDialog() == DialogResult.OK)
            {
                return saveFD.FileName;
            }

            return null;
        }
    }
}
