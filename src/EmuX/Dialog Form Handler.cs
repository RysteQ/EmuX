namespace EmuX
{
    internal class DialogFormHandler
    {
        /// <summary>
        /// Handles the open file dialog and returns the file the user specified or returns null if the user did not select a file
        /// </summary>
        /// <returns>The source path the user selected, if the user did not select a path then return an empty string</returns>
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
        /// <returns>The save path the user selected, if the user did not select a path then return an empty string</returns>
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
