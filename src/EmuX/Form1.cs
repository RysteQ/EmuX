using System.ComponentModel;
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
            if (filename == "")
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
            if (save_path == "")
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
            if (save_path == "")
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

        private void ButtonExecute_Click(object sender, EventArgs e)
        {
            string code_to_analyze = RichTextboxAssemblyCode.Text.TrimEnd('\n') + "\n";

            this.analyzer.Flush();
            this.analyzer.SetInstructions(code_to_analyze);
            this.analyzer.AnalyzeInstructions();

            // check if there was an error while analyzing the code
            if (this.analyzer.AnalyzingSuccessful() == false)
            {
                // get the error line and the line that cause the error
                string error_line_text = this.analyzer.GetErrorLineData();

                MessageBox.Show("There was an error at line " + (this.analyzer.GetErrorLine() + 1).ToString() + "\nLine: " + error_line_text, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // initialize the data for the emulator
            List<Instruction> instructions = this.analyzer.GetInstructions();
            List<StaticData> static_data = this.analyzer.GetStaticData();
            List<(string, int)> labels = this.analyzer.GetLabelData();

            // make sure the instructions are of the correct variant and bitmode
            this.verifier.SetInstructionData(instructions);
            this.verifier.VerifyInstructions();

            if (this.verifier.AreInstructionsValid() == false)
            {
                string error_message = verifier.GetErrorMessage();
                
                MessageBox.Show("There was an error at line " + (this.verifier.GetInstructionIndexError() + 1).ToString() + "\nMessage: " + error_message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // reset the interrupt handler video and interrupt
            this.interrupt_handler.ResetInterrupt();

            // init the video form
            if (this.video_form.IsOpen() == false)
            {
                this.video_form = new VideoForm();

                this.video_form.InitVideo(this.interrupt_handler.GetVideoOutput().Width, this.interrupt_handler.GetVideoOutput().Height);
                this.video_form.Show();
            }

            // clear the previous values and prepare all the data the emulator needs
            this.virtual_system.ClearCallStack();

            this.emulator.SetVirtualSystem(this.virtual_system);
            this.emulator.SetInstructions(instructions);
            this.emulator.SetStaticData(static_data);
            this.emulator.SetLabelData(labels);
            this.emulator.Reset();

            // get the instruction count
            ProgressBarExecutionProgress.Maximum = this.emulator.GetInstructionCount();

            do
            {
                // execute each instruction individually
                this.emulator.Execute();
                this.emulator.NextInstruction();

                // check if an interrupt occured
                if (this.emulator.GetInterruptOccurance())
                {
                    // The user may have the *grabs speaker* STUPIIIID.....
                    // ......
                    // ......
                    // so this will basically prevent the user from doing any mistakes that may crash the application
                    // and also inform the user from his mistake and stop any further execution the code
                    try
                    {
                        // execute the interrupt
                        this.interrupt_handler.SetVirtualSystem(this.emulator.GetVirtualSystem());
                        this.interrupt_handler.SetInterrupt(this.emulator.GetInterrupt());
                        this.interrupt_handler.ExecuteInterrupt();

                        // update the video output
                        this.video_form.UpdateVideo(this.interrupt_handler.GetVideoOutput());

                        // reset the interrupt flag
                        this.emulator.SetVirtualSystem(this.interrupt_handler.GetVirtualSystem());
                        this.emulator.ResetInterrupt();
                    } catch (Exception ex)
                    {
                        MessageBox.Show("There was an error in the interrupt handler\n\nError: " + ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    }
                }

                ProgressBarExecutionProgress.Value = this.emulator.GetIndex();
            } while (this.emulator.ErrorEncountered() == false && this.emulator.GetExit() == false && this.emulator.GetIndex() != this.emulator.GetInstructionCount());

            if (this.emulator.ErrorEncountered())
                MessageBox.Show("An error was encountered at command " + (this.emulator.GetIndex() + 1).ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            // get the virtual system back
            this.virtual_system = this.emulator.GetVirtualSystem();
        }

        private void EmuXTabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            // check if the user is at the main tab, if not then show the secondary execute button
            if (EmuXTabControl.SelectedIndex != 0)
                ButtonExecuteOnAnotherTab.Visible = true;
            else
                ButtonExecuteOnAnotherTab.Visible = false;

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
            HexConverter hex_converter = new HexConverter();
            BaseConverter base_converter = new BaseConverter();
            List<byte> bytes_to_show = new List<byte>();

            int start = 0;
            int end = 0;

            if ((TextBoxMemoryRangeStart.Text.Trim() == "" && TextBoxMemoryRangeEnd.Text.Trim() == "") || ComboBoxMemoryRepresentation.SelectedIndex == -1)
                return;

            // get and test the memory range start / end 
            if (TextBoxMemoryRangeEnd.Text.Trim().Length != 0 && TextBoxMemoryRangeStart.Text.Trim().Length != 0)
            {
                if (int.TryParse(TextBoxMemoryRangeEnd.Text, out end) == false || int.TryParse(TextBoxMemoryRangeStart.Text, out start) == false)
                {
                    MessageBox.Show("Error converting memory range end to int", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                DataGridViewMemory.Columns.Add("+" + i.ToString(), "+" + i.ToString());

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
                            to_add.Add("0b" + base_converter.ConvertUnsignedLongToBinaryString(bytes_to_show[row * 8 + column]));

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

        private void ButtonNextInstruction_Click(object sender, EventArgs e)
        {
            if (this.emulator.HasInstructions() == false)
            {
                this.analyzer.SetInstructions(RichTextboxAssemblyCode.Text);
                this.analyzer.AnalyzeInstructions();
                this.emulator.SetInstructions(this.analyzer.GetInstructions());
            }

            ProgressBarExecutionProgress.Maximum = this.emulator.GetInstructionCount();
            int index = 0;

            // go through each line and don't count empty lines
            for (int i = 0; i < RichTextboxAssemblyCode.Text.Split('\n').Length && index != this.emulator.GetIndex(); i++)
            {
                if (RichTextboxAssemblyCode.Text.Split('\n')[i].Trim().Length == 0)
                    continue;

                index++;
            }

            // cheks if the instructions is within bounds
            if (this.emulator.GetInstructionCount() != this.emulator.GetIndex())
            {
                this.emulator.Execute();
                this.emulator.NextInstruction();

                // Update the GUI elements
                LabelCurrentInstruction.Text = "Current Instruction: " + RichTextboxAssemblyCode.Text.Split('\n')[index];
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
            int index = 0;

            // go through each line and don't count empty lines
            for (int i = 0; i < RichTextboxAssemblyCode.Text.Split('\n').Length && index != this.emulator.GetIndex(); i++)
            {
                if (RichTextboxAssemblyCode.Text.Split('\n')[i].Trim().Length == 0)
                    continue;

                index++;
            }

            // cheks if the instructions is within bounds
            if (this.emulator.GetIndex() > 0)
            {
                this.emulator.PreviousInstruction();
                this.emulator.Execute();

                // Update the GUI elements
                LabelCurrentInstruction.Text = "Current Instruction: " + RichTextboxAssemblyCode.Text.Split('\n')[index];
                ProgressBarExecutionProgress.Value = this.emulator.GetIndex();
            }
        }

        private void ButtonExecuteOnAnotherTab_Click(object sender, EventArgs e)
        {
            // execute the code
            ButtonExecute_Click(null, null);

            // check if the user is at the register view tab, if so refresh the data
            if (EmuXTabControl.SelectedIndex == 2)
                EmuXTabControl_SelectedIndexChanged(null, null);
        }

        private void converterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Converter_Form converter_form = new Converter_Form();

            // display the converter form
            converter_form.Show();
        }

        private void aSCIITableToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ASCII_Table ascii_table = new ASCII_Table();

            // display the ascii table form
            ascii_table.Show();
        }

        private void instructionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Instruction_Set instruction_set = new Instruction_Set();

            // display the instruction set form
            instruction_set.Show();
        }

        private void ButtonResetVirtualSystem_Click(object sender, EventArgs e)
        {
            // ask the user for confirmation before reseting the virtual system
            if (MessageBox.Show("Are you sure you want to reset the virtual system ?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                this.virtual_system.ResetVirtualSystem();
        }

        private VideoForm video_form = new VideoForm();
        private VirtualSystem virtual_system = new VirtualSystem();
        private Analyzer analyzer = new Analyzer();
        private Emulator emulator = new Emulator();
        private Verifier verifier = new Verifier();
        private Interrupt_Handler interrupt_handler = new Interrupt_Handler();
        private string save_path = "";
    }
}