using System.DirectoryServices;

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

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void mainForm_Load(object sender, EventArgs e)
        {
            this.virtual_system = new VirtualSystem();
        }

        private VirtualSystem virtual_system;

        private void ButtonExecute_Click(object sender, EventArgs e)
        {
            string code_to_analyze = RichTextboxAssemblyCode.Text.TrimEnd('\n') + "\n";
            int instruction_index = 0;

            this.analyzer.SetInstructions(code_to_analyze);
            this.analyzer.AnalyzeInstructions();

            // check if there was an error while analyzing the code
            if (analyzer.AnalyzingSuccessful() == false)
            {
                // get the error line and the line that cause the error
                int error_line = this.analyzer.GetErrorLine();
                string error_line_text = RichTextboxAssemblyCode.Text.Split('\n')[error_line];

                MessageBox.Show("There was an error at line " + error_line.ToString() + "\nLine: " + error_line_text, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // initialize and set the data for the emulator
            List<Instruction> instructions = this.analyzer.GetInstructions();
            List<StaticData> static_data = this.analyzer.GetStaticData();
            List<(string, int)> labels = this.analyzer.GetLabelData();

            this.virtual_system.ClearCallStack();

            this.emulator.SetVirtualSystem(this.virtual_system);
            this.emulator.SetInstructions(instructions);
            this.emulator.SetStaticData(static_data);
            this.emulator.SetLabelData(labels);
            this.emulator.InitStaticData();
            this.emulator.Reset();

            // get the instruction count
            ProgressBarExecutionProgress.Maximum = this.emulator.GetInstructionCount();

            do
            {
                this.emulator.Execute();
                this.emulator.NextInstruction();

                ProgressBarExecutionProgress.Value = instruction_index;
            } while (instruction_index < this.emulator.GetIndex() && this.emulator.ErrorEncountered() == false && this.emulator.GetExit() == false);

            if (this.emulator.ErrorEncountered())
                MessageBox.Show("An error was encountered at command " + (this.emulator.GetIndex() + 1).ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            // get the virtual system back
            this.virtual_system = this.emulator.GetVirtualSystem();
        }

        private void EmuXTabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            // this section is focused solely on the registers tab
            if (EmuXTabControl.SelectedIndex != 2)
                return;

            // Get the virtual system
            this.virtual_system = this.emulator.GetVirtualSystem();

            // get the values to set + the textboxes to set the values with
            ulong[] values_to_display = virtual_system.GetAllRegisterValues();
            TextBox[] textbox_to_update = new TextBox[]
            {
                TextBoxRAX,
                TextBoxRBX,
                TextBoxRCX,
                TextBoxRDX,
                TextBoxRSI,
                TextBoxRDI,
                TextBoxRSP,
                TextBoxRBP,
                TextBoxRIP,
                TextBoxR8,
                TextBoxR9,
                TextBoxR10,
                TextBoxR11,
                TextBoxR12,
                TextBoxR13,
                TextBoxR14,
                TextBoxR15
            };

            // set the said values to said textboxed and reset the back colour
            for (int i = 0; i < textbox_to_update.Length; i++)
            {
                textbox_to_update[i].Text = values_to_display[i].ToString();
                textbox_to_update[i].BackColor = Color.White;
            }

            // get and set the EFLAGS checkboxes
            uint EFLAGS = this.virtual_system.GetEFLAGS();
            CheckBox[] checkboxes_to_update = new CheckBox[]
            {
                CheckBoxCF,
                CheckBoxPF,
                CheckBoxAF,
                CheckBoxZF,
                CheckBoxSF,
                CheckBoxTF,
                CheckBoxIF,
                CheckBoxDF,
                CheckBoxOF,
                CheckBoxIOPL,
                CheckBoxNT,
                CheckBoxRF,
                CheckBoxVM,
                CheckBoxAC,
                CheckBoxVIF,
                CheckBoxVIP,
                CheckBoxID
            };

            uint[] masks = this.virtual_system.GetEFLAGSMasks();

            for (int i = 0; i < checkboxes_to_update.Length; i++)
                checkboxes_to_update[i].Checked = (EFLAGS & masks[i]) != 0;
        }

        private void ButtonSearchMemoryRange_Click(object sender, EventArgs e)
        {
            if ((TextBoxMemoryRangeStart.Text.Trim() == "" && TextBoxMemoryRangeEnd.Text.Trim() == "") || ComboBoxMemoryRepresentation.SelectedIndex == -1)
                return;

            HexConverter hex_converter = new HexConverter();
            BaseConverter base_converter = new BaseConverter();
            List<byte> bytes_to_show = new List<byte>();

            int start = 0;
            int end = 0;

            // get and test the memory range start
            if (TextBoxMemoryRangeStart.Text.Trim().Length != 0)
            {
                if (int.TryParse(TextBoxMemoryRangeStart.Text, out start) == false)
                {
                    MessageBox.Show("Error converting memory range start to int", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    TextBoxMemoryRangeStart.BackColor = Color.Red;
                    return;
                }
            }

            // get and test the memory range and 
            if (TextBoxMemoryRangeEnd.Text.Trim().Length != 0)
            {
                if (int.TryParse(TextBoxMemoryRangeEnd.Text, out end) == false)
                {
                    MessageBox.Show("Error converting memory range end to int", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    TextBoxMemoryRangeEnd.BackColor = Color.Red;
                    return;
                }
            }

            // check if the values are valid
            if (end < start || (start - end) % 8 != 0)
            {
                MessageBox.Show("Please select a range of at least 8 bytes", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // get the bytes within the range
            for (int i = start; i < end; i++)
                bytes_to_show.Add(this.virtual_system.GetByteMemory(i));

            // init the data grid view
            ButtonClearMemoryTable_Click(null, null);
            DataGridViewMemory.Columns.Add("empty", "");

            for (int i = 0; i < 8; i++)
                DataGridViewMemory.Columns.Add("+" + (i + 1).ToString(), "+" + (i + 1).ToString());

            // add the bytes to the data grid view
            for (int row = 0; row < (end - start) / 8; row++)
            {
                List<string> to_add = new List<string>();
                to_add.Add((row * 8).ToString());

                // check which representation the user wants
                // 0 = decimal, 1 = binary and 2 = hexadecimal
                switch (ComboBoxMemoryRepresentation.SelectedIndex)
                {
                    case 0:
                        for (int column = 0; column < 8; column++)
                            to_add.Add(bytes_to_show[row * 8 + column].ToString());

                        break;

                    case 1:
                        for (int column = 0; column < 8; column++)
                            to_add.Add("0b" + base_converter.ConvertIntToBinaryString(bytes_to_show[row * 8 + column]));

                        break;

                    case 2:
                        for (int column = 0; column < 8; column++)
                            to_add.Add("0x" + hex_converter.ConvertByteToHex(bytes_to_show[row * 8 + column]));

                        break;
                }

                // add the memory data to the table
                DataGridViewMemory.Rows.Add(to_add.ToArray());
            }
        }

        private void ButtonClearMemoryTable_Click(object sender, EventArgs e)
        {
            DataGridViewMemory.Rows.Clear();
            DataGridViewMemory.Columns.Clear();
        }

        private void ButtonSetMemoryValue_Click(object sender, EventArgs e)
        {
            byte value_to_set;
            int memory_end;
            int memory_start;

            // parse all of the data
            bool valid_end_range = int.TryParse(TextBoxMemoryRangeEnd.Text, out memory_end);
            bool valid_start_range = int.TryParse(TextBoxMemoryRangeStart.Text, out memory_start);
            bool valid_value = byte.TryParse(TextBoxMemoryValue.Text, out value_to_set);

            // check if all of the data was parsed successfuly and that the range is valid
            if ((valid_end_range == false || valid_start_range == false || valid_value == false) && memory_end < memory_start)
            {
                MessageBox.Show("Please enter a valid memory range / value to set the memory range at", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // set all of the bytes to said value
            for (int index = memory_start; index < memory_end; index++)
                this.virtual_system.SetByteMemory(index, value_to_set);
        }

        private void ButtonSetRegisterValues_Click(object sender, EventArgs e)
        {
            // the textboxes to check the new register values of
            List<ulong> values_to_set = new List<ulong>();
            TextBox[] textbox_to_update = new TextBox[]
            {
                TextBoxRAX,
                TextBoxRBX,
                TextBoxRCX,
                TextBoxRDX,
                TextBoxRSI,
                TextBoxRDI,
                TextBoxRSP,
                TextBoxRBP,
                TextBoxRIP,
                TextBoxR8,
                TextBoxR9,
                TextBoxR10,
                TextBoxR11,
                TextBoxR12,
                TextBoxR13,
                TextBoxR14,
                TextBoxR15
            };

            // reset the back colour
            for (int i = 0; i < textbox_to_update.Length; i++)
                textbox_to_update[i].BackColor = Color.White;

            // get all of the values and to a validity check on them
            for (int i = 0; i < textbox_to_update.Length; i++)
            {
                ulong value = 0;

                if (textbox_to_update[i].Text.Trim().Length != 0 && ulong.TryParse(textbox_to_update[i].Text, out value))
                    values_to_set.Add(value);
                else
                    textbox_to_update[i].BackColor = Color.Red;
            }

            // set the values
            this.virtual_system.SetAllRegisterValues(values_to_set.ToArray());

            // the eflags masks and value to set the eflags at
            uint[] masks = this.virtual_system.GetEFLAGSMasks();
            uint EFLAGS_to_set = 0;

            CheckBox[] checkboxes_to_update = new CheckBox[]
            {
                CheckBoxCF,
                CheckBoxPF,
                CheckBoxAF,
                CheckBoxZF,
                CheckBoxSF,
                CheckBoxTF,
                CheckBoxIF,
                CheckBoxDF,
                CheckBoxOF,
                CheckBoxIOPL,
                CheckBoxNT,
                CheckBoxRF,
                CheckBoxVM,
                CheckBoxAC,
                CheckBoxVIF,
                CheckBoxVIP,
                CheckBoxID
            };

            // check and increment the corresponding bit of the EFLAGS
            for (int i = 0; i < masks.Length; i++)
                if (checkboxes_to_update[i].Checked)
                    EFLAGS_to_set += masks[i];

            this.virtual_system.SetEflags(EFLAGS_to_set);

            // Set the virtual system to the emulator
            this.emulator.SetVirtualSystem(this.virtual_system);
        }

        private void increaseSizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Font font_to_set = new Font(RichTextboxAssemblyCode.Font.FontFamily, RichTextboxAssemblyCode.Font.Size + 1, FontStyle.Regular);

            RichTextboxAssemblyCode.Font = font_to_set;
            RichtextBoxOutput.Font = font_to_set;
        }

        private void decreaseSizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Font font_to_set = new Font(RichTextboxAssemblyCode.Font.FontFamily, RichTextboxAssemblyCode.Font.Size + 1, FontStyle.Regular);

            RichTextboxAssemblyCode.Font = font_to_set;
            RichtextBoxOutput.Font = font_to_set;
        }

        private void tODOToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (fontDialog.ShowDialog() == DialogResult.OK)
            {
                RichTextboxAssemblyCode.Font = fontDialog.Font;
                RichtextBoxOutput.Font = fontDialog.Font;
            }
        }

        private void convertToUppercaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int cursor_index = RichTextboxAssemblyCode.SelectionStart;

            RichTextboxAssemblyCode.Text = RichTextboxAssemblyCode.Text.ToUpper();
            RichTextboxAssemblyCode.SelectionStart = cursor_index;
        }

        private void convertToLowercaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int cursor_index = RichTextboxAssemblyCode.SelectionStart;

            RichTextboxAssemblyCode.Text = RichTextboxAssemblyCode.Text.ToLower();
            RichTextboxAssemblyCode.SelectionStart = cursor_index;
        }

        private Analyzer analyzer = new Analyzer();
        private Emulator emulator = new Emulator();
        private string save_path = "";

        private void ButtonNextInstruction_Click(object sender, EventArgs e)
        {
            if (this.emulator.HasInstructions() == false)
            {
                this.analyzer.SetInstructions(RichTextboxAssemblyCode.Text);
                this.analyzer.AnalyzeInstructions();
                this.emulator.SetInstructions(this.analyzer.GetInstructions());
            }

            ProgressBarExecutionProgress.Maximum = this.emulator.GetInstructionCount();

            // cheks if the instructions is within bounds
            if (this.emulator.GetInstructionCount() != this.emulator.GetIndex())
            {
                this.emulator.Execute();
                this.emulator.NextInstruction();

                // Update the GUI elements
                LabelCurrentInstruction.Text = LabelCurrentInstruction.Text + " " + RichTextboxAssemblyCode.Text.Split('\n')[this.emulator.GetIndex() - 1];
                ProgressBarExecutionProgress.Value = this.emulator.GetIndex();
            }
        }

        private void ButtonPreviousInstruction_Click(object sender, EventArgs e)
        {
            if (this.emulator.HasInstructions() == false)
            {
                this.analyzer.SetInstructions(RichTextboxAssemblyCode.Text);
                this.analyzer.AnalyzeInstructions();
                this.emulator.SetInstructions(analyzer.GetInstructions());
            }

            ProgressBarExecutionProgress.Maximum = this.emulator.GetInstructionCount();

            // cheks if the instructions is within bounds
            if (this.emulator.GetIndex() > 0)
            {
                this.emulator.PreviousInstruction();
                this.emulator.Execute();

                // Update the GUI elements
                LabelCurrentInstruction.Text = LabelCurrentInstruction.Text + " " + RichTextboxAssemblyCode.Text.Split('\n')[this.emulator.GetIndex()];
                ProgressBarExecutionProgress.Value = this.emulator.GetIndex();
            }
        }
    }
}