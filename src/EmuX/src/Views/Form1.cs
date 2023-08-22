using EmuX.Services;
using EmuX.src.Models;
using EmuX.src.Services.Analyzer;
using EmuX.src.Services.Base.Converter;
using EmuX.src.Services.Emulator;
using EmuX.src.Views;
using Label = EmuX.src.Models.Label;

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
            openFD.FileName = string.Empty;
            openFD.Filter = "Text file (*.txt)|*.txt|Assenbly file (*.asm)|*.asm|All files (*.*)|*.*";

            if (openFD.ShowDialog() == DialogResult.OK)
            {
                RichTextboxAssemblyCode.Text = File.ReadAllText(openFD.FileName);
                Text = openFD.FileName.Split('\\')[openFD.FileName.Split('\\').Length - 1] + " - EmuX";
                this.save_path = openFD.FileName;

                UpdateOutput("Opened file " + this.save_path + " ...");
            }
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFD.FileName = string.Empty;
            saveFD.Filter = "Text file (*.txt)|*.txt|Assenbly file (*.asm)|*.asm|All files (*.*)|*.*";

            if (saveFD.ShowDialog() == DialogResult.OK)
            {
                StreamWriter file_writer = new(this.save_path);
                file_writer.Write(RichTextboxAssemblyCode.Text);
                file_writer.Close();

                UpdateOutput("File saved in location " + this.save_path + " ...");
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string contents = "Open source software \n\n" +
                                "EmuX, a x86 emulator written in C# for the purpose of making it easier" +
                                "to learn x86 assembly on Windows in an easy way\n\n" +
                                "Created by RysteQ";

            MessageBox.Show(contents, "About", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.save_path))
            {
                saveAsToolStripMenuItem_Click(sender, e);
                return;
            }

            StreamWriter file_writer = new(this.save_path);
            file_writer.Write(RichTextboxAssemblyCode.Text);
            file_writer.Close();

            UpdateOutput("File Saved...");
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private bool AnalyzeInstructions()
        {
            this.analyzer.AnalyzeInstructions(RichTextboxAssemblyCode.Text.TrimEnd('\n') + "\n");

            if (this.analyzer.Successful == false)
            {
                string error_message = $"There was an error at line {this.analyzer.ErrorLine + 1}\nError: {this.analyzer.ErrorMessage}";

                MessageBox.Show(error_message, "Error - Analyzer", MessageBoxButtons.OK, MessageBoxIcon.Error);
                UpdateOutput(error_message);
                return false;
            }

            if (InstructionVerifier.VerifyInstructions(this.analyzer.Instructions) == false)
            {
                MessageBox.Show("There was an error, invalid opcode / parameter combination", "Error - Verifier", MessageBoxButtons.OK, MessageBoxIcon.Error);
                UpdateOutput("There was an error, invalid opcode / parameter combination");
                return false;
            }

            List<Instruction> instructions = this.analyzer.Instructions;
            List<StaticData> static_data = this.analyzer.StaticData;
            List<Label> labels = this.analyzer.Labels;

            this.interrupt_handler.ResetInterrupt();
            this.assembly_analyzed = true;

            this.emulator.VirtualSystem = this.virtual_system;
            this.emulator.PrepareEmulator(instructions, static_data, labels);

            return true;
        }

        private void ButtonExecute_Click(object sender, EventArgs e)
        {
            if (CheckBoxResetVirtualSystemBeforeExecution.Checked)
                this.virtual_system.Reset();

            if (this.assembly_analyzed && MessageBox.Show("The source code has not changed, analyze the source code again ?", "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    AnalyzeInstructions();
            else
                AnalyzeInstructions();

            ProgressBarExecutionProgress.Maximum = this.emulator.GetInstructionCount();

            while (this.emulator.Error == false && this.emulator.ExitFound == false && this.emulator.CurrentInstructionIndex != this.emulator.GetInstructionCount())
            {
                this.emulator.Execute();
                this.emulator.NextInstruction();

                if (this.emulator.GetInterruptOccurance())
                {
                    if (this.video_form.IsOpen() == false)
                    {
                        this.video_form = new();

                        this.video_form.InitVideo(this.interrupt_handler.GetVideoOutput().Width, this.interrupt_handler.GetVideoOutput().Height);
                        this.video_form.Show();
                    }

                    // I dont like using try catch but the user may have the *grabs speaker* STUPIIIID
                    // ......
                    try
                    {
                        this.interrupt_handler.VirtualSystem = this.emulator.VirtualSystem;
                        this.interrupt_handler.Interrupt = this.emulator.Interrupt;
                        this.interrupt_handler.ExecuteInterrupt();

                        this.video_form.UpdateVideo(this.interrupt_handler.GetVideoOutput());

                        this.emulator.VirtualSystem = this.interrupt_handler.VirtualSystem;
                        this.emulator.ResetInterrupt();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"There was an error in the interrupt handler\n\nError: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    }
                }

                ProgressBarExecutionProgress.Value = this.emulator.CurrentInstructionIndex;
            }

            this.virtual_system = this.emulator.VirtualSystem;

            if (this.emulator.Error)
            {
                MessageBox.Show($"An error was encountered at command {this.emulator.CurrentInstructionIndex + 1}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                UpdateOutput($"An error was encountered at command {this.emulator.CurrentInstructionIndex + 1}");
            }

            // update the progress bar if the HLT instruction was found or any other early exit conditions
            if (this.emulator.ExitFound)
                ProgressBarExecutionProgress.Value = ProgressBarExecutionProgress.Maximum;

            if (this.emulator.HasInstructions())
                UpdateOutput("Execution Completed...");

            UpdateDetachableWindows();
        }

        private void EmuXTabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            ButtonExecuteOnAnotherTab.Visible = EmuXTabControl.SelectedIndex != 0;

            // this section is focused solely on the registers tab
            if (EmuXTabControl.SelectedIndex != 2)
                return;

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

            this.virtual_system = this.emulator.VirtualSystem;

            ulong[] values_to_display = virtual_system.GetAllRegisterValues();
            uint EFLAGS = this.virtual_system.EFLAGS;
            uint[] masks = this.virtual_system.GetEFLAGSMasks();

            for (int i = 0; i < checkboxes_to_update.Length; i++)
                checkboxes_to_update[i].Checked = (EFLAGS & masks[i]) != 0;

            for (int i = 0; i < textbox_to_update.Length; i++)
            {
                textbox_to_update[i].Text = values_to_display[i].ToString();
                textbox_to_update[i].BackColor = Color.White;
            }
        }

        private void ButtonSearchMemoryRange_Click(object sender, EventArgs e)
        {
            List<byte> bytes_to_show = new();

            int start = 0;
            int end = 0;

            if ((TextBoxMemoryRangeStart.Text.Trim() == string.Empty && TextBoxMemoryRangeEnd.Text.Trim() == string.Empty) || ComboBoxMemoryRepresentation.SelectedIndex == -1)
                return;

            if (TextBoxMemoryRangeEnd.Text.Trim().Length != 0 && TextBoxMemoryRangeStart.Text.Trim().Length != 0)
            {
                if ((int.TryParse(TextBoxMemoryRangeEnd.Text, out end) && int.TryParse(TextBoxMemoryRangeStart.Text, out start)) == false)
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

                // 0 = decimal, 1 = binary and 2 = hexadecimal
                switch (ComboBoxMemoryRepresentation.SelectedIndex)
                {
                    case 0:
                        for (int column = 0; column < 8; column++)
                            to_add.Add(bytes_to_show[row * 8 + column].ToString());

                        break;

                    case 1:
                        for (int column = 0; column < 8; column++)
                            to_add.Add("0b" + BinaryConverter.ConvertUlongToBase(bytes_to_show[row * 8 + column]));

                        break;

                    case 2:
                        for (int column = 0; column < 8; column++)
                            to_add.Add("0x" + HexadecimalConverter.ConvertUlongToBase(bytes_to_show[row * 8 + column]));

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

            if ((valid_end_range && valid_start_range && valid_value) == false && memory_end < memory_start)
            {
                MessageBox.Show("Please enter a valid memory range / value to set the memory range at", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            for (int index = memory_start; index < memory_end; index++)
                this.virtual_system.SetByteMemory(index, value_to_set);

            UpdateDetachableWindows();
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
            this.emulator.VirtualSystem = this.virtual_system;

            UpdateDetachableWindows();
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
            if (this.assembly_analyzed == false)
                if (AnalyzeInstructions() == false)
                    return;

            // count the labels as lines of code AKA instructions
            string[] lines_of_code = StringHandler.RemoveEmptyLines(
                RichTextboxAssemblyCode.Text.Split("section.text")[1].Split('\n')
                .Where(line => line.Trim().StartsWith(';') == false).ToArray());

            ProgressBarExecutionProgress.Maximum = this.emulator.GetInstructionCount();

            // cheks if the instructions is within bounds
            if (this.emulator.GetInstructionCount() != this.emulator.CurrentInstructionIndex)
            {
                this.emulator.Execute();
                this.emulator.NextInstruction();

                LabelCurrentInstruction.Text = "Current Instruction: " + lines_of_code[this.emulator.CurrentInstructionIndex - 1];
                ProgressBarExecutionProgress.Value = this.emulator.CurrentInstructionIndex;

                UpdateOutput("Stepped");
            }

            UpdateDetachableWindows();
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

        private void UpdateOutput(string message_to_append)
        {
            if (this.output_form.IsOpen())
                this.output_form.UpdateOutput(message_to_append);

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

        private void EmuXTabControl_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                switch (EmuXTabControl.SelectedIndex)
                {
                    case 1:
                        this.memory_form = new(ref this.virtual_system);
                        this.memory_form.Show();
                        break;

                    case 2:
                        this.registers_form = new(ref this.virtual_system, ref this.emulator);
                        this.registers_form.Show();
                        break;

                    case 3:
                        outputToolStripMenuItem_Click(null, null);
                        break;

                    default:
                        break;
                }
            }
        }

        private void outputToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.output_form.IsOpen())
                return;

            this.output_form = new Output_Form();
            this.output_form.UpdateOutput(RichtextBoxOutput.Text.TrimEnd('\n'));
            this.output_form.Show();
        }

        private void RichTextboxAssemblyCode_TextChanged(object sender, EventArgs e)
        {
            this.assembly_analyzed = false;
        }

        private void UpdateDetachableWindows()
        {
            if (this.registers_form != null && this.registers_form.IsOpen())
                this.registers_form.SetVirtualSystem(this.virtual_system);
        }

        private Output_Form output_form = new();
        private Registers_Form registers_form;
        private MemoryForm memory_form;

        private VideoForm video_form = new();
        private VirtualSystem virtual_system = new();
        private Analyzer analyzer = new();
        private Emulator emulator = new();
        private Interrupt_Handler interrupt_handler = new();
        private string save_path = string.Empty;
        private bool assembly_analyzed = false;
    }
}