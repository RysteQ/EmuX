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

        private void ButtonShowMemory_Click(object sender, EventArgs e)
        {
            // initialize the memory form and show it
            // Also, if you increase the memory limit in VisualSystem.cs expect for some SERIOUS LAG
            // So if you do that I would suggest to write some code so it doesn't display bytes but quads (8 bytes) or something like that
            // Byte = 1 byte, Short = 2 bytes, Double = 4 bytes, Quad = 8 bytes
            // Don't ask me why they have chosen such names, I do not know or care to find out

            MemoryForm memory_form = new MemoryForm(virtual_system.GetAllMemory(), 8);
            memory_form.Show();
        }

        private void mainForm_Load(object sender, EventArgs e)
        {
            virtual_system = new VirtualSystem();
        }

        private VirtualSystem virtual_system;

        private void ButtonShowRegisterValues_Click(object sender, EventArgs e)
        {
            Registers_Form registers_form = new Registers_Form();
            registers_form.Show();
        }
    }
}