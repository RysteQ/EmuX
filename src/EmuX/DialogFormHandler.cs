using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmuX
{
    internal class DialogFormHandler
    {
        /// <summary>
        /// Handles the open file dialog and returns the file the user specified or returns null if the user did not select a file
        /// </summary>
        /// <param name="openFD"></param>
        /// <returns>The source path the user entered / selected</returns>
        public string openFileDialog(OpenFileDialog openFD)
        {
            openFD.FileName = "";
            openFD.Filter = "Text file (*.txt)|*.txt|Assenbly file (*.asm)|*.asm|All files (*.*)|*.*";

            if (openFD.ShowDialog() == DialogResult.OK)
                return openFD.FileName;

            return "";
        }

        /// <summary>
        /// Handles the save file dialog and returns the filename the user specified or returns null if the user did not select a filename
        /// </summary>
        /// <param name="saveFD"></param>
        /// <returns>The save path the user entered / selected</returns>
        public string saveFileDialog(SaveFileDialog saveFD)
        {
            saveFD.FileName = "";
            saveFD.Filter = "Text file (*.txt)|*.txt|Assenbly file (*.asm)|*.asm|All files (*.*)|*.*";

            if (saveFD.ShowDialog() == DialogResult.OK)
                return saveFD.FileName;

            return "";
        }
    }
}
