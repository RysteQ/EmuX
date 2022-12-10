namespace EmuX
{
    public partial class mainForm : Form
    {
        public mainForm()
        {
            InitializeComponent();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // open the open file dialog
            DialogFormHandler dialogFormHandler = new DialogFormHandler();
            string filename = dialogFormHandler.openFileDialog(openFD);

            // make sure the user entered the filename
            if (filename == null)
                return;

            // open the file
            RichTextboxAssemblyCode.Text = File.ReadAllText(filename);
            mainForm.ActiveForm.Text = filename.Split('\\')[filename.Split('\\').Length - 1] + " - EmuX";

            save_path = filename;
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // open the save file dialog
            DialogFormHandler dialogFormHandler = new DialogFormHandler();
            save_path = dialogFormHandler.saveFileDialog(saveFD);

            // make sure the user selected a path
            if (save_path == null)
                return;

            // save the file
            StreamWriter file_writer = new StreamWriter(save_path);
            file_writer.Write(RichTextboxAssemblyCode.Text);
            file_writer.Close();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Not yet completed", "About", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // make sure the user enter a path previously with save as
            if (save_path == null)
            {
                saveAsToolStripMenuItem_Click(null, null);
                return;
            }

            // save the file to the previously chosen path
            StreamWriter file_writer = new StreamWriter(save_path);
            file_writer.Write(RichTextboxAssemblyCode.Text);
            file_writer.Close();
        }

        string save_path = null;

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void mainForm_Load(object sender, EventArgs e)
        {
            virtual_system = new VirtualSystem();
        }

        private VirtualSystem virtual_system;

        private void ButtonExecute_Click(object sender, EventArgs e)
        {
            Analyzer analyzer = new Analyzer();
            Emulator emulator = new Emulator();

            analyzer.SetInstructions(RichTextboxAssemblyCode.Text);

            // check if there was an error while analyzing the code
            if (analyzer.AnalyzingSuccessful() == false)
            {
                // get the error line and the line that cause the error
                int error_line = analyzer.GetErrorLine();
                string error_line_text = RichTextboxAssemblyCode.Text.Split('\n')[error_line];

                MessageBox.Show("There was an error at line " + error_line.ToString() + "\nLine: " + error_line_text, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            emulator.SetData(analyzer.GetInstructions());
            emulator.Execute();
        }
    }
}