using System.ComponentModel;
using System.DirectoryServices;
using EmuX.Models;
using EmuX.Services;
using EmuX.Services.Analyzer;
using EmuX.Services.Emulator;

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
            openFD.FileName = "";
            openFD.Filter = "Text file (*.txt)|*.txt|Assenbly file (*.asm)|*.asm|All files (*.*)|*.*";

            if (openFD.ShowDialog() == DialogResult.OK)
            {
                RichTextboxAssemblyCode.Text = File.ReadAllText(openFD.FileName);
                mainForm.ActiveForm.Text = openFD.FileName.Split('\\')[openFD.FileName.Split('\\').Length - 1] + " - EmuX";
                save_path = openFD.FileName;

                UpdateOutput("Opened file " + save_path + " ...");
            }
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFD.FileName = "";
            saveFD.Filter = "Text file (*.txt)|*.txt|Assenbly file (*.asm)|*.asm|All files (*.*)|*.*";

            if (saveFD.ShowDialog() == DialogResult.OK)
            {
                StreamWriter file_writer = new(save_path);
                file_writer.Write(RichTextboxAssemblyCode.Text);
                file_writer.Close();

                UpdateOutput("File saved in location " + save_path + " ...");
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // major versions are complete rewrites or very major changes to the structure of the software
            // minor versions are new features
            // hotfix is the bugfix count, increment by one for each version, each version can solve multiple bugs
            int major_version = 1;
            int minor_version = 2;
            int hotfix = 1;

            string version = major_version.ToString() + "." + minor_version.ToString() + "." + hotfix.ToString();

            MessageBox.Show("Version " + version, "About", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (save_path == "")
            {
                saveAsToolStripMenuItem_Click(sender, e);
                return;
            }

            StreamWriter file_writer = new(save_path);
            file_writer.Write(RichTextboxAssemblyCode.Text);
            file_writer.Close();

            UpdateOutput("File Saved...");
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ButtonExecute_Click(object sender, EventArgs e)
        {
            string code_to_analyze = RichTextboxAssemblyCode.Text.TrimEnd('\n') + "\n";

            this.analyzer.SetInstructions(code_to_analyze);
            this.analyzer.AnalyzeInstructions();

            if (this.analyzer.AnalyzingSuccessful() == false)
            {
                string error_line_text = this.analyzer.GetErrorLineData();

                MessageBox.Show("There was an error at line " + (this.analyzer.GetErrorLine() + 1).ToString() + "\nLine: " + error_line_text, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            this.verifier.SetInstructionData(this.analyzer.GetInstructions());
            this.verifier.VerifyInstructions();

            if (this.verifier.AreInstructionsValid() == false)
            {
                string error_message = this.verifier.GetErrorMessage();

                MessageBox.Show("There was an error at line " + (this.verifier.GetInstructionIndexError() + 1).ToString() + "\nMessage: " + error_message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            List<Instruction> instructions = this.analyzer.GetInstructions();
            List<StaticData> static_data = this.analyzer.GetStaticData();
            List<(string, int)> labels = this.analyzer.GetLabelData();

            this.interrupt_handler.ResetInterruptHandler();
            this.interrupt_handler.ResetInterrupt();

            this.virtual_system.ResetVirtualSystem();

            this.emulator.SetVirtualSystem(this.virtual_system);
            this.emulator.SetInstructions(instructions);
            this.emulator.SetStaticData(static_data);
            this.emulator.SetLabelData(labels);
            this.emulator.Reset();

            ProgressBarExecutionProgress.Maximum = this.emulator.GetInstructionCount();

            do
            {
                this.emulator.Execute();
                this.emulator.NextInstruction();

                if (this.emulator.GetInterruptOccurance())
                {
                    if (this.video_form.IsOpen() == false)
                    {
                        this.video_form = new VideoForm();

                        this.video_form.InitVideo(this.interrupt_handler.GetVideoOutput().Width, this.interrupt_handler.GetVideoOutput().Height);
                        this.video_form.Show();
                    }

                    // I dont like using try catch but the user may have the *grabs speaker* STUPIIIID
                    // ......
                    try
                    {
                        this.interrupt_handler.virtual_system = this.emulator.GetVirtualSystem();
                        this.interrupt_handler.SetInterrupt(this.emulator.GetInterrupt());
                        this.interrupt_handler.ExecuteInterrupt();

                        this.video_form.UpdateVideo(this.interrupt_handler.GetVideoOutput());

                        this.emulator.SetVirtualSystem(this.interrupt_handler.virtual_system);
                        this.emulator.ResetInterrupt();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("There was an error in the interrupt handler\n\nError: " + ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    }
                }

                ProgressBarExecutionProgress.Value = this.emulator.GetIndex();
            } while (this.emulator.ErrorEncountered() == false && this.emulator.GetExit() == false && this.emulator.GetIndex() != this.emulator.GetInstructionCount());

            this.virtual_system = this.emulator.GetVirtualSystem();

            if (this.emulator.ErrorEncountered())
                MessageBox.Show("An error was encountered at command " + (this.emulator.GetIndex() + 1).ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            // update the progress bar if the HLT instruction was found or any other early exit conditions
            if (this.emulator.GetExit())
                ProgressBarExecutionProgress.Value = ProgressBarExecutionProgress.Maximum;

            UpdateOutput("Execution Completed...");
        }

        private void EmuXTabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (EmuXTabControl.SelectedIndex != 0)
                ButtonExecuteOnAnotherTab.Visible = true;
            else
                ButtonExecuteOnAnotherTab.Visible = false;

            // this section is focused solely on the registers tab
            if (EmuXTabControl.SelectedIndex != 2)
                return;

            this.virtual_system = this.emulator.GetVirtualSystem();
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

            for (int i = 0; i < textbox_to_update.Length; i++)
            {
                textbox_to_update[i].Text = values_to_display[i].ToString();
                textbox_to_update[i].BackColor = Color.White;
            }

            uint EFLAGS = this.virtual_system.EFLAGS;
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
            HexConverter hex_converter = new();
            BaseConverter base_converter = new();
            List<byte> bytes_to_show = new();

            int start = 0;
            int end = 0;

            if ((TextBoxMemoryRangeStart.Text.Trim() == "" && TextBoxMemoryRangeEnd.Text.Trim() == "") || ComboBoxMemoryRepresentation.SelectedIndex == -1)
                return;

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

            // init the data grid view
            DataGridViewMemory.Rows.Clear();
            DataGridViewMemory.Columns.Clear();
            DataGridViewMemory.Columns.Add("empty", "");

            for (int i = start; i < end; i++)
                bytes_to_show.Add(this.virtual_system.GetByteMemory(i));

            for (int i = 0; i < 8; i++)
                DataGridViewMemory.Columns.Add("+" + i.ToString(), "+" + i.ToString());

            for (int row = 0; row < (end - start) / 8; row++)
            {
                List<string> to_add = new();
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

            bool valid_end_range = int.TryParse(TextBoxMemoryRangeEnd.Text, out memory_end);
            bool valid_start_range = int.TryParse(TextBoxMemoryRangeStart.Text, out memory_start);
            bool valid_value = byte.TryParse(TextBoxMemoryValue.Text, out value_to_set);

            if ((valid_end_range == false || valid_start_range == false || valid_value == false) && memory_end < memory_start)
            {
                MessageBox.Show("Please enter a valid memory range / value to set the memory range at", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            for (int index = memory_start; index < memory_end; index++)
                this.virtual_system.SetByteMemory(index, value_to_set);
        }

        private void ButtonSetRegisterValues_Click(object sender, EventArgs e)
        {
            List<ulong> values_to_set = new();
            uint[] masks = this.virtual_system.GetEFLAGSMasks();
            uint EFLAGS_to_set = 0;

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

            for (int i = 0; i < textbox_to_update.Length; i++)
                textbox_to_update[i].BackColor = Color.White;

            // get all of the values and to a validity check on them
            for (int i = 0; i < textbox_to_update.Length; i++)
            {
                if (textbox_to_update[i].Text.Trim().Length != 0 && ulong.TryParse(textbox_to_update[i].Text, out ulong value))
                    values_to_set.Add(value);
                else
                    textbox_to_update[i].BackColor = Color.Red;
            }

            this.virtual_system.SetAllRegisterValues(values_to_set.ToArray());

            // check and increment the corresponding bit of the EFLAGS
            for (int i = 0; i < masks.Length; i++)
                if (checkboxes_to_update[i].Checked)
                    EFLAGS_to_set += masks[i];

            this.virtual_system.EFLAGS = EFLAGS_to_set;

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
            Font font_to_set = new(RichTextboxAssemblyCode.Font.FontFamily, RichTextboxAssemblyCode.Font.Size - 1, FontStyle.Regular);

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

                LabelCurrentInstruction.Text = "Current Instruction: " + RichTextboxAssemblyCode.Text.Split('\n')[index];
                ProgressBarExecutionProgress.Value = this.emulator.GetIndex();
            }
        }

        private void ButtonPreviousInstruction_Click(object sender, EventArgs e)
        {
            int index = 0;

            if (this.emulator.HasInstructions() == false)
            {
                this.analyzer.SetInstructions(RichTextboxAssemblyCode.Text);
                this.analyzer.AnalyzeInstructions();
                this.emulator.SetInstructions(analyzer.GetInstructions());
            }

            ProgressBarExecutionProgress.Maximum = this.emulator.GetInstructionCount();

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

                LabelCurrentInstruction.Text = "Current Instruction: " + RichTextboxAssemblyCode.Text.Split('\n')[index];
                ProgressBarExecutionProgress.Value = this.emulator.GetIndex();
            }
        }

        private void ButtonExecuteOnAnotherTab_Click(object sender, EventArgs e)
        {
            ButtonExecute_Click(sender, e);

            // check if the user is at the register view tab, if so refresh the data
            if (EmuXTabControl.SelectedIndex == 2)
                EmuXTabControl_SelectedIndexChanged(sender, e);
        }

        private void converterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Converter_Form().Show();
        }

        private void aSCIITableToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new ASCII_Table().Show();
        }

        private void instructionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Instruction_Set().Show();
        }

        private void UpdateOutput(string message_to_append)
        {
            this.RichtextBoxOutput.Text += message_to_append + "\n";
        }

        private void memoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // TODO
        }

        private void registersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // TODO
        }

        private void refreshVideoFormTimer_Tick(object sender, EventArgs e)
        {
            // TODO
        }

        private VideoForm video_form = new();
        private VirtualSystem virtual_system = new();
        private Analyzer analyzer = new();
        private Emulator emulator = new();
        private Verifier verifier = new();
        private Interrupt_Handler interrupt_handler = new();
        private string save_path = "";
    }
}