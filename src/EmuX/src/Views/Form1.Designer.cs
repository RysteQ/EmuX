namespace EmuX
{
    partial class mainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            mainMenuStrip = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            openToolStripMenuItem = new ToolStripMenuItem();
            saveToolStripMenuItem = new ToolStripMenuItem();
            saveAsToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator1 = new ToolStripSeparator();
            exitToolStripMenuItem = new ToolStripMenuItem();
            textToolStripMenuItem = new ToolStripMenuItem();
            tODOToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator2 = new ToolStripSeparator();
            increaseSizeToolStripMenuItem = new ToolStripMenuItem();
            decreaseSizeToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator3 = new ToolStripSeparator();
            convertToUppercaseToolStripMenuItem = new ToolStripMenuItem();
            convertToLowercaseToolStripMenuItem = new ToolStripMenuItem();
            toolsToolStripMenuItem = new ToolStripMenuItem();
            converterToolStripMenuItem = new ToolStripMenuItem();
            aSCIITableToolStripMenuItem = new ToolStripMenuItem();
            instructionsToolStripMenuItem = new ToolStripMenuItem();
            aboutToolStripMenuItem = new ToolStripMenuItem();
            GroupboxInput = new GroupBox();
            RichTextboxAssemblyCode = new RichTextBox();
            GroupboxControls = new GroupBox();
            GroupboxExecutionProgress = new GroupBox();
            ProgressBarExecutionProgress = new ProgressBar();
            LabelCurrentInstruction = new Label();
            GroupboxExecution = new GroupBox();
            ButtonExecute = new Button();
            ButtonPreviousInstruction = new Button();
            ButtonNextInstruction = new Button();
            openFD = new OpenFileDialog();
            saveFD = new SaveFileDialog();
            EmuXTabControl = new TabControl();
            Assembly = new TabPage();
            Memory = new TabPage();
            GroupBoxMemoryControls = new GroupBox();
            ButtonClearMemoryTable = new Button();
            LabelValue = new Label();
            labelDataRepresentation = new Label();
            LabelMemoryRange = new Label();
            ButtonSetMemoryValue = new Button();
            ButtonSearchMemoryRange = new Button();
            TextBoxMemoryValue = new TextBox();
            TextBoxMemoryRangeEnd = new TextBox();
            TextBoxMemoryRangeStart = new TextBox();
            ComboBoxMemoryRepresentation = new ComboBox();
            DataGridViewMemory = new DataGridView();
            Registers = new TabPage();
            GroupBoxEFLAGS = new GroupBox();
            CheckBoxID = new CheckBox();
            CheckBoxVIP = new CheckBox();
            CheckBoxVIF = new CheckBox();
            LabelID = new Label();
            LabelVIP = new Label();
            LabelVIF = new Label();
            CheckBoxAC = new CheckBox();
            LabelAC = new Label();
            CheckBoxVM = new CheckBox();
            LabelVM = new Label();
            CheckBoxRF = new CheckBox();
            LabelRF = new Label();
            CheckBoxNT = new CheckBox();
            LabelNT = new Label();
            CheckBoxIOPL = new CheckBox();
            LabelIOPL = new Label();
            CheckBoxOF = new CheckBox();
            LabelOF = new Label();
            CheckBoxDF = new CheckBox();
            LabelDF = new Label();
            CheckBoxIF = new CheckBox();
            LabelIF = new Label();
            CheckBoxTF = new CheckBox();
            LabelTF = new Label();
            CheckBoxSF = new CheckBox();
            LabelSF = new Label();
            CheckBoxZF = new CheckBox();
            LabelZF = new Label();
            CheckBoxAF = new CheckBox();
            LabelAF = new Label();
            CheckBoxPF = new CheckBox();
            LabelPF = new Label();
            CheckBoxCF = new CheckBox();
            LabelCF = new Label();
            ButtonSetRegisterValues = new Button();
            GroupBoxGeneralPurposeRegisters = new GroupBox();
            TextBoxR15 = new TextBox();
            LabelR15 = new Label();
            TextBoxR14 = new TextBox();
            LabelR14 = new Label();
            TextBoxR13 = new TextBox();
            LabelR13 = new Label();
            TextBoxR12 = new TextBox();
            LabelR12 = new Label();
            TextBoxR11 = new TextBox();
            LabelR11 = new Label();
            TextBoxR10 = new TextBox();
            LabelR10 = new Label();
            TextBoxR9 = new TextBox();
            LabelR9 = new Label();
            TextBoxR8 = new TextBox();
            LabelR8 = new Label();
            LabelRAX = new Label();
            TextBoxRIP = new TextBox();
            TextBoxRAX = new TextBox();
            LabelRIP = new Label();
            LabelRBX = new Label();
            TextBoxRBP = new TextBox();
            TextBoxRBX = new TextBox();
            LabelRBP = new Label();
            LabelRCX = new Label();
            TextBoxRSP = new TextBox();
            TextBoxRCX = new TextBox();
            LabelRSP = new Label();
            LabelRDX = new Label();
            TextBoxRDI = new TextBox();
            TextBoxRDX = new TextBox();
            LabelRDI = new Label();
            LabelRSI = new Label();
            TextBoxRSI = new TextBox();
            Output = new TabPage();
            RichtextBoxOutput = new RichTextBox();
            fontDialog = new FontDialog();
            ButtonExecuteOnAnotherTab = new Button();
            tabsToolStripMenuItem = new ToolStripMenuItem();
            memoryToolStripMenuItem = new ToolStripMenuItem();
            registersToolStripMenuItem = new ToolStripMenuItem();
            refreshVideoFormTimer = new System.Windows.Forms.Timer(components);
            mainMenuStrip.SuspendLayout();
            GroupboxInput.SuspendLayout();
            GroupboxControls.SuspendLayout();
            GroupboxExecutionProgress.SuspendLayout();
            GroupboxExecution.SuspendLayout();
            EmuXTabControl.SuspendLayout();
            Assembly.SuspendLayout();
            Memory.SuspendLayout();
            GroupBoxMemoryControls.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)DataGridViewMemory).BeginInit();
            Registers.SuspendLayout();
            GroupBoxEFLAGS.SuspendLayout();
            GroupBoxGeneralPurposeRegisters.SuspendLayout();
            Output.SuspendLayout();
            SuspendLayout();
            // 
            // mainMenuStrip
            // 
            mainMenuStrip.ImageScalingSize = new Size(24, 24);
            mainMenuStrip.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, textToolStripMenuItem, tabsToolStripMenuItem, toolsToolStripMenuItem, aboutToolStripMenuItem });
            mainMenuStrip.Location = new Point(0, 0);
            mainMenuStrip.Name = "mainMenuStrip";
            mainMenuStrip.Padding = new Padding(9, 3, 0, 3);
            mainMenuStrip.Size = new Size(920, 35);
            mainMenuStrip.TabIndex = 0;
            mainMenuStrip.Text = "mainMenuStrip";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { openToolStripMenuItem, saveToolStripMenuItem, saveAsToolStripMenuItem, toolStripSeparator1, exitToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(54, 29);
            fileToolStripMenuItem.Text = "File";
            // 
            // openToolStripMenuItem
            // 
            openToolStripMenuItem.Name = "openToolStripMenuItem";
            openToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.O;
            openToolStripMenuItem.Size = new Size(285, 34);
            openToolStripMenuItem.Text = "Open";
            openToolStripMenuItem.Click += openToolStripMenuItem_Click;
            // 
            // saveToolStripMenuItem
            // 
            saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            saveToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.S;
            saveToolStripMenuItem.Size = new Size(285, 34);
            saveToolStripMenuItem.Text = "Save";
            saveToolStripMenuItem.Click += saveToolStripMenuItem_Click;
            // 
            // saveAsToolStripMenuItem
            // 
            saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            saveAsToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.Shift | Keys.S;
            saveAsToolStripMenuItem.Size = new Size(285, 34);
            saveAsToolStripMenuItem.Text = "Save As";
            saveAsToolStripMenuItem.Click += saveAsToolStripMenuItem_Click;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(282, 6);
            // 
            // exitToolStripMenuItem
            // 
            exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            exitToolStripMenuItem.Size = new Size(285, 34);
            exitToolStripMenuItem.Text = "Exit";
            exitToolStripMenuItem.Click += exitToolStripMenuItem_Click;
            // 
            // textToolStripMenuItem
            // 
            textToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { tODOToolStripMenuItem, toolStripSeparator2, increaseSizeToolStripMenuItem, decreaseSizeToolStripMenuItem, toolStripSeparator3, convertToUppercaseToolStripMenuItem, convertToLowercaseToolStripMenuItem });
            textToolStripMenuItem.Name = "textToolStripMenuItem";
            textToolStripMenuItem.Size = new Size(58, 29);
            textToolStripMenuItem.Text = "Text";
            // 
            // tODOToolStripMenuItem
            // 
            tODOToolStripMenuItem.Name = "tODOToolStripMenuItem";
            tODOToolStripMenuItem.Size = new Size(350, 34);
            tODOToolStripMenuItem.Text = "Change Font";
            tODOToolStripMenuItem.Click += tODOToolStripMenuItem_Click;
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new Size(347, 6);
            // 
            // increaseSizeToolStripMenuItem
            // 
            increaseSizeToolStripMenuItem.Name = "increaseSizeToolStripMenuItem";
            increaseSizeToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.I;
            increaseSizeToolStripMenuItem.Size = new Size(350, 34);
            increaseSizeToolStripMenuItem.Text = "Increase Size";
            increaseSizeToolStripMenuItem.Click += increaseSizeToolStripMenuItem_Click;
            // 
            // decreaseSizeToolStripMenuItem
            // 
            decreaseSizeToolStripMenuItem.Name = "decreaseSizeToolStripMenuItem";
            decreaseSizeToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.D;
            decreaseSizeToolStripMenuItem.Size = new Size(350, 34);
            decreaseSizeToolStripMenuItem.Text = "Decrease Size";
            decreaseSizeToolStripMenuItem.Click += decreaseSizeToolStripMenuItem_Click;
            // 
            // toolStripSeparator3
            // 
            toolStripSeparator3.Name = "toolStripSeparator3";
            toolStripSeparator3.Size = new Size(347, 6);
            // 
            // convertToUppercaseToolStripMenuItem
            // 
            convertToUppercaseToolStripMenuItem.Name = "convertToUppercaseToolStripMenuItem";
            convertToUppercaseToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.U;
            convertToUppercaseToolStripMenuItem.Size = new Size(350, 34);
            convertToUppercaseToolStripMenuItem.Text = "Convert To Uppercase";
            convertToUppercaseToolStripMenuItem.Click += convertToUppercaseToolStripMenuItem_Click;
            // 
            // convertToLowercaseToolStripMenuItem
            // 
            convertToLowercaseToolStripMenuItem.Name = "convertToLowercaseToolStripMenuItem";
            convertToLowercaseToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.L;
            convertToLowercaseToolStripMenuItem.Size = new Size(350, 34);
            convertToLowercaseToolStripMenuItem.Text = "Convert To Lowercase";
            convertToLowercaseToolStripMenuItem.Click += convertToLowercaseToolStripMenuItem_Click;
            // 
            // toolsToolStripMenuItem
            // 
            toolsToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { converterToolStripMenuItem, aSCIITableToolStripMenuItem, instructionsToolStripMenuItem });
            toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            toolsToolStripMenuItem.Size = new Size(69, 29);
            toolsToolStripMenuItem.Text = "Tools";
            // 
            // converterToolStripMenuItem
            // 
            converterToolStripMenuItem.Name = "converterToolStripMenuItem";
            converterToolStripMenuItem.ShortcutKeys = Keys.Alt | Keys.C;
            converterToolStripMenuItem.Size = new Size(270, 34);
            converterToolStripMenuItem.Text = "Converter";
            converterToolStripMenuItem.Click += converterToolStripMenuItem_Click;
            // 
            // aSCIITableToolStripMenuItem
            // 
            aSCIITableToolStripMenuItem.Name = "aSCIITableToolStripMenuItem";
            aSCIITableToolStripMenuItem.ShortcutKeys = Keys.Alt | Keys.A;
            aSCIITableToolStripMenuItem.Size = new Size(270, 34);
            aSCIITableToolStripMenuItem.Text = "ASCII Table";
            aSCIITableToolStripMenuItem.Click += aSCIITableToolStripMenuItem_Click;
            // 
            // instructionsToolStripMenuItem
            // 
            instructionsToolStripMenuItem.Name = "instructionsToolStripMenuItem";
            instructionsToolStripMenuItem.ShortcutKeys = Keys.Alt | Keys.I;
            instructionsToolStripMenuItem.Size = new Size(270, 34);
            instructionsToolStripMenuItem.Text = "Instructions";
            instructionsToolStripMenuItem.Click += instructionsToolStripMenuItem_Click;
            // 
            // aboutToolStripMenuItem
            // 
            aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            aboutToolStripMenuItem.Size = new Size(78, 29);
            aboutToolStripMenuItem.Text = "About";
            aboutToolStripMenuItem.Click += aboutToolStripMenuItem_Click;
            // 
            // GroupboxInput
            // 
            GroupboxInput.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            GroupboxInput.Controls.Add(RichTextboxAssemblyCode);
            GroupboxInput.Location = new Point(9, 10);
            GroupboxInput.Margin = new Padding(4, 5, 4, 5);
            GroupboxInput.Name = "GroupboxInput";
            GroupboxInput.Padding = new Padding(4, 5, 4, 5);
            GroupboxInput.Size = new Size(566, 407);
            GroupboxInput.TabIndex = 1;
            GroupboxInput.TabStop = false;
            GroupboxInput.Text = "Assembly";
            // 
            // RichTextboxAssemblyCode
            // 
            RichTextboxAssemblyCode.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            RichTextboxAssemblyCode.Location = new Point(9, 37);
            RichTextboxAssemblyCode.Margin = new Padding(4, 5, 4, 5);
            RichTextboxAssemblyCode.Name = "RichTextboxAssemblyCode";
            RichTextboxAssemblyCode.Size = new Size(547, 354);
            RichTextboxAssemblyCode.TabIndex = 0;
            RichTextboxAssemblyCode.Text = "; the static data goes here\n; for example\n; number_ten: db 10\n\nsection.text\n\n; the instructions go here\n; for example\n; mov al, byte [number_ten]\n; inc byte al";
            // 
            // GroupboxControls
            // 
            GroupboxControls.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            GroupboxControls.Controls.Add(GroupboxExecutionProgress);
            GroupboxControls.Controls.Add(LabelCurrentInstruction);
            GroupboxControls.Controls.Add(GroupboxExecution);
            GroupboxControls.Location = new Point(583, 10);
            GroupboxControls.Margin = new Padding(4, 5, 4, 5);
            GroupboxControls.Name = "GroupboxControls";
            GroupboxControls.Padding = new Padding(4, 5, 4, 5);
            GroupboxControls.Size = new Size(314, 407);
            GroupboxControls.TabIndex = 2;
            GroupboxControls.TabStop = false;
            GroupboxControls.Text = "Controls";
            // 
            // GroupboxExecutionProgress
            // 
            GroupboxExecutionProgress.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            GroupboxExecutionProgress.Controls.Add(ProgressBarExecutionProgress);
            GroupboxExecutionProgress.Location = new Point(14, 275);
            GroupboxExecutionProgress.Margin = new Padding(4, 5, 4, 5);
            GroupboxExecutionProgress.Name = "GroupboxExecutionProgress";
            GroupboxExecutionProgress.Padding = new Padding(4, 5, 4, 5);
            GroupboxExecutionProgress.Size = new Size(294, 120);
            GroupboxExecutionProgress.TabIndex = 4;
            GroupboxExecutionProgress.TabStop = false;
            GroupboxExecutionProgress.Text = "Execution Progress";
            // 
            // ProgressBarExecutionProgress
            // 
            ProgressBarExecutionProgress.Location = new Point(11, 37);
            ProgressBarExecutionProgress.Margin = new Padding(4, 5, 4, 5);
            ProgressBarExecutionProgress.Name = "ProgressBarExecutionProgress";
            ProgressBarExecutionProgress.Size = new Size(274, 73);
            ProgressBarExecutionProgress.TabIndex = 0;
            // 
            // LabelCurrentInstruction
            // 
            LabelCurrentInstruction.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            LabelCurrentInstruction.AutoSize = true;
            LabelCurrentInstruction.Location = new Point(11, 42);
            LabelCurrentInstruction.Margin = new Padding(4, 0, 4, 0);
            LabelCurrentInstruction.Name = "LabelCurrentInstruction";
            LabelCurrentInstruction.Size = new Size(163, 25);
            LabelCurrentInstruction.TabIndex = 1;
            LabelCurrentInstruction.Text = "Current Instruction:";
            // 
            // GroupboxExecution
            // 
            GroupboxExecution.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            GroupboxExecution.Controls.Add(ButtonExecute);
            GroupboxExecution.Controls.Add(ButtonPreviousInstruction);
            GroupboxExecution.Controls.Add(ButtonNextInstruction);
            GroupboxExecution.Location = new Point(11, 82);
            GroupboxExecution.Margin = new Padding(4, 5, 4, 5);
            GroupboxExecution.Name = "GroupboxExecution";
            GroupboxExecution.Padding = new Padding(4, 5, 4, 5);
            GroupboxExecution.Size = new Size(297, 183);
            GroupboxExecution.TabIndex = 0;
            GroupboxExecution.TabStop = false;
            GroupboxExecution.Text = "Execution";
            // 
            // ButtonExecute
            // 
            ButtonExecute.Location = new Point(9, 133);
            ButtonExecute.Margin = new Padding(4, 5, 4, 5);
            ButtonExecute.Name = "ButtonExecute";
            ButtonExecute.Size = new Size(280, 38);
            ButtonExecute.TabIndex = 3;
            ButtonExecute.Text = "Execute";
            ButtonExecute.UseVisualStyleBackColor = true;
            ButtonExecute.Click += ButtonExecute_Click;
            // 
            // ButtonPreviousInstruction
            // 
            ButtonPreviousInstruction.Location = new Point(9, 85);
            ButtonPreviousInstruction.Margin = new Padding(4, 5, 4, 5);
            ButtonPreviousInstruction.Name = "ButtonPreviousInstruction";
            ButtonPreviousInstruction.Size = new Size(280, 38);
            ButtonPreviousInstruction.TabIndex = 1;
            ButtonPreviousInstruction.Text = "Previous Instruction";
            ButtonPreviousInstruction.UseVisualStyleBackColor = true;
            ButtonPreviousInstruction.Click += ButtonPreviousInstruction_Click;
            // 
            // ButtonNextInstruction
            // 
            ButtonNextInstruction.Location = new Point(9, 37);
            ButtonNextInstruction.Margin = new Padding(4, 5, 4, 5);
            ButtonNextInstruction.Name = "ButtonNextInstruction";
            ButtonNextInstruction.Size = new Size(280, 38);
            ButtonNextInstruction.TabIndex = 0;
            ButtonNextInstruction.Text = "Next Instruction";
            ButtonNextInstruction.UseVisualStyleBackColor = true;
            ButtonNextInstruction.Click += ButtonNextInstruction_Click;
            // 
            // EmuXTabControl
            // 
            EmuXTabControl.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            EmuXTabControl.Controls.Add(Assembly);
            EmuXTabControl.Controls.Add(Memory);
            EmuXTabControl.Controls.Add(Registers);
            EmuXTabControl.Controls.Add(Output);
            EmuXTabControl.Location = new Point(0, 45);
            EmuXTabControl.Margin = new Padding(4, 5, 4, 5);
            EmuXTabControl.Name = "EmuXTabControl";
            EmuXTabControl.SelectedIndex = 0;
            EmuXTabControl.Size = new Size(923, 475);
            EmuXTabControl.TabIndex = 1;
            EmuXTabControl.SelectedIndexChanged += EmuXTabControl_SelectedIndexChanged;
            // 
            // Assembly
            // 
            Assembly.Controls.Add(GroupboxInput);
            Assembly.Controls.Add(GroupboxControls);
            Assembly.Location = new Point(4, 34);
            Assembly.Margin = new Padding(4, 5, 4, 5);
            Assembly.Name = "Assembly";
            Assembly.Padding = new Padding(4, 5, 4, 5);
            Assembly.Size = new Size(915, 437);
            Assembly.TabIndex = 0;
            Assembly.Text = "Assembly";
            Assembly.UseVisualStyleBackColor = true;
            // 
            // Memory
            // 
            Memory.Controls.Add(GroupBoxMemoryControls);
            Memory.Controls.Add(DataGridViewMemory);
            Memory.Location = new Point(4, 34);
            Memory.Margin = new Padding(4, 5, 4, 5);
            Memory.Name = "Memory";
            Memory.Padding = new Padding(4, 5, 4, 5);
            Memory.Size = new Size(915, 437);
            Memory.TabIndex = 1;
            Memory.Text = "Memory";
            Memory.UseVisualStyleBackColor = true;
            // 
            // GroupBoxMemoryControls
            // 
            GroupBoxMemoryControls.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            GroupBoxMemoryControls.Controls.Add(ButtonClearMemoryTable);
            GroupBoxMemoryControls.Controls.Add(LabelValue);
            GroupBoxMemoryControls.Controls.Add(labelDataRepresentation);
            GroupBoxMemoryControls.Controls.Add(LabelMemoryRange);
            GroupBoxMemoryControls.Controls.Add(ButtonSetMemoryValue);
            GroupBoxMemoryControls.Controls.Add(ButtonSearchMemoryRange);
            GroupBoxMemoryControls.Controls.Add(TextBoxMemoryValue);
            GroupBoxMemoryControls.Controls.Add(TextBoxMemoryRangeEnd);
            GroupBoxMemoryControls.Controls.Add(TextBoxMemoryRangeStart);
            GroupBoxMemoryControls.Controls.Add(ComboBoxMemoryRepresentation);
            GroupBoxMemoryControls.Location = new Point(11, 10);
            GroupBoxMemoryControls.Margin = new Padding(4, 5, 4, 5);
            GroupBoxMemoryControls.Name = "GroupBoxMemoryControls";
            GroupBoxMemoryControls.Padding = new Padding(4, 5, 4, 5);
            GroupBoxMemoryControls.Size = new Size(1060, 187);
            GroupBoxMemoryControls.TabIndex = 1;
            GroupBoxMemoryControls.TabStop = false;
            GroupBoxMemoryControls.Text = "Controls";
            // 
            // ButtonClearMemoryTable
            // 
            ButtonClearMemoryTable.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            ButtonClearMemoryTable.Location = new Point(567, 83);
            ButtonClearMemoryTable.Margin = new Padding(4, 5, 4, 5);
            ButtonClearMemoryTable.Name = "ButtonClearMemoryTable";
            ButtonClearMemoryTable.Size = new Size(324, 38);
            ButtonClearMemoryTable.TabIndex = 9;
            ButtonClearMemoryTable.Text = "Clear";
            ButtonClearMemoryTable.UseVisualStyleBackColor = true;
            ButtonClearMemoryTable.Click += ButtonClearMemoryTable_Click;
            // 
            // LabelValue
            // 
            LabelValue.AutoSize = true;
            LabelValue.Location = new Point(9, 140);
            LabelValue.Margin = new Padding(4, 0, 4, 0);
            LabelValue.Name = "LabelValue";
            LabelValue.Size = new Size(54, 25);
            LabelValue.TabIndex = 8;
            LabelValue.Text = "Value";
            // 
            // labelDataRepresentation
            // 
            labelDataRepresentation.AutoSize = true;
            labelDataRepresentation.Location = new Point(9, 90);
            labelDataRepresentation.Margin = new Padding(4, 0, 4, 0);
            labelDataRepresentation.Name = "labelDataRepresentation";
            labelDataRepresentation.Size = new Size(172, 25);
            labelDataRepresentation.TabIndex = 7;
            labelDataRepresentation.Text = "Data Representation";
            // 
            // LabelMemoryRange
            // 
            LabelMemoryRange.AutoSize = true;
            LabelMemoryRange.Location = new Point(9, 42);
            LabelMemoryRange.Margin = new Padding(4, 0, 4, 0);
            LabelMemoryRange.Name = "LabelMemoryRange";
            LabelMemoryRange.Size = new Size(134, 25);
            LabelMemoryRange.TabIndex = 6;
            LabelMemoryRange.Text = "Memory Range";
            // 
            // ButtonSetMemoryValue
            // 
            ButtonSetMemoryValue.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            ButtonSetMemoryValue.Location = new Point(567, 133);
            ButtonSetMemoryValue.Margin = new Padding(4, 5, 4, 5);
            ButtonSetMemoryValue.Name = "ButtonSetMemoryValue";
            ButtonSetMemoryValue.Size = new Size(324, 38);
            ButtonSetMemoryValue.TabIndex = 5;
            ButtonSetMemoryValue.Text = "Set";
            ButtonSetMemoryValue.UseVisualStyleBackColor = true;
            ButtonSetMemoryValue.Click += ButtonSetMemoryValue_Click;
            // 
            // ButtonSearchMemoryRange
            // 
            ButtonSearchMemoryRange.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            ButtonSearchMemoryRange.Location = new Point(567, 37);
            ButtonSearchMemoryRange.Margin = new Padding(4, 5, 4, 5);
            ButtonSearchMemoryRange.Name = "ButtonSearchMemoryRange";
            ButtonSearchMemoryRange.Size = new Size(324, 38);
            ButtonSearchMemoryRange.TabIndex = 4;
            ButtonSearchMemoryRange.Text = "Search";
            ButtonSearchMemoryRange.UseVisualStyleBackColor = true;
            ButtonSearchMemoryRange.Click += ButtonSearchMemoryRange_Click;
            // 
            // TextBoxMemoryValue
            // 
            TextBoxMemoryValue.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            TextBoxMemoryValue.Location = new Point(270, 133);
            TextBoxMemoryValue.Margin = new Padding(4, 5, 4, 5);
            TextBoxMemoryValue.Name = "TextBoxMemoryValue";
            TextBoxMemoryValue.Size = new Size(287, 31);
            TextBoxMemoryValue.TabIndex = 3;
            // 
            // TextBoxMemoryRangeEnd
            // 
            TextBoxMemoryRangeEnd.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            TextBoxMemoryRangeEnd.Location = new Point(416, 37);
            TextBoxMemoryRangeEnd.Margin = new Padding(4, 5, 4, 5);
            TextBoxMemoryRangeEnd.Name = "TextBoxMemoryRangeEnd";
            TextBoxMemoryRangeEnd.Size = new Size(141, 31);
            TextBoxMemoryRangeEnd.TabIndex = 2;
            // 
            // TextBoxMemoryRangeStart
            // 
            TextBoxMemoryRangeStart.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            TextBoxMemoryRangeStart.Location = new Point(270, 37);
            TextBoxMemoryRangeStart.Margin = new Padding(4, 5, 4, 5);
            TextBoxMemoryRangeStart.Name = "TextBoxMemoryRangeStart";
            TextBoxMemoryRangeStart.Size = new Size(141, 31);
            TextBoxMemoryRangeStart.TabIndex = 1;
            // 
            // ComboBoxMemoryRepresentation
            // 
            ComboBoxMemoryRepresentation.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            ComboBoxMemoryRepresentation.FormattingEnabled = true;
            ComboBoxMemoryRepresentation.Items.AddRange(new object[] { "Decimal", "Binary", "Hexadecimal" });
            ComboBoxMemoryRepresentation.Location = new Point(270, 85);
            ComboBoxMemoryRepresentation.Margin = new Padding(4, 5, 4, 5);
            ComboBoxMemoryRepresentation.Name = "ComboBoxMemoryRepresentation";
            ComboBoxMemoryRepresentation.Size = new Size(287, 33);
            ComboBoxMemoryRepresentation.TabIndex = 0;
            // 
            // DataGridViewMemory
            // 
            DataGridViewMemory.AllowUserToAddRows = false;
            DataGridViewMemory.AllowUserToDeleteRows = false;
            DataGridViewMemory.AllowUserToResizeColumns = false;
            DataGridViewMemory.AllowUserToResizeRows = false;
            DataGridViewMemory.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            DataGridViewMemory.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            DataGridViewMemory.EditMode = DataGridViewEditMode.EditProgrammatically;
            DataGridViewMemory.Enabled = false;
            DataGridViewMemory.Location = new Point(11, 207);
            DataGridViewMemory.Margin = new Padding(4, 5, 4, 5);
            DataGridViewMemory.Name = "DataGridViewMemory";
            DataGridViewMemory.ReadOnly = true;
            DataGridViewMemory.RowHeadersWidth = 62;
            DataGridViewMemory.RowTemplate.Height = 25;
            DataGridViewMemory.Size = new Size(891, 207);
            DataGridViewMemory.TabIndex = 0;
            // 
            // Registers
            // 
            Registers.Controls.Add(GroupBoxEFLAGS);
            Registers.Controls.Add(ButtonSetRegisterValues);
            Registers.Controls.Add(GroupBoxGeneralPurposeRegisters);
            Registers.Location = new Point(4, 34);
            Registers.Margin = new Padding(4, 5, 4, 5);
            Registers.Name = "Registers";
            Registers.Size = new Size(915, 437);
            Registers.TabIndex = 2;
            Registers.Text = "Registers";
            Registers.UseVisualStyleBackColor = true;
            // 
            // GroupBoxEFLAGS
            // 
            GroupBoxEFLAGS.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            GroupBoxEFLAGS.Controls.Add(CheckBoxID);
            GroupBoxEFLAGS.Controls.Add(CheckBoxVIP);
            GroupBoxEFLAGS.Controls.Add(CheckBoxVIF);
            GroupBoxEFLAGS.Controls.Add(LabelID);
            GroupBoxEFLAGS.Controls.Add(LabelVIP);
            GroupBoxEFLAGS.Controls.Add(LabelVIF);
            GroupBoxEFLAGS.Controls.Add(CheckBoxAC);
            GroupBoxEFLAGS.Controls.Add(LabelAC);
            GroupBoxEFLAGS.Controls.Add(CheckBoxVM);
            GroupBoxEFLAGS.Controls.Add(LabelVM);
            GroupBoxEFLAGS.Controls.Add(CheckBoxRF);
            GroupBoxEFLAGS.Controls.Add(LabelRF);
            GroupBoxEFLAGS.Controls.Add(CheckBoxNT);
            GroupBoxEFLAGS.Controls.Add(LabelNT);
            GroupBoxEFLAGS.Controls.Add(CheckBoxIOPL);
            GroupBoxEFLAGS.Controls.Add(LabelIOPL);
            GroupBoxEFLAGS.Controls.Add(CheckBoxOF);
            GroupBoxEFLAGS.Controls.Add(LabelOF);
            GroupBoxEFLAGS.Controls.Add(CheckBoxDF);
            GroupBoxEFLAGS.Controls.Add(LabelDF);
            GroupBoxEFLAGS.Controls.Add(CheckBoxIF);
            GroupBoxEFLAGS.Controls.Add(LabelIF);
            GroupBoxEFLAGS.Controls.Add(CheckBoxTF);
            GroupBoxEFLAGS.Controls.Add(LabelTF);
            GroupBoxEFLAGS.Controls.Add(CheckBoxSF);
            GroupBoxEFLAGS.Controls.Add(LabelSF);
            GroupBoxEFLAGS.Controls.Add(CheckBoxZF);
            GroupBoxEFLAGS.Controls.Add(LabelZF);
            GroupBoxEFLAGS.Controls.Add(CheckBoxAF);
            GroupBoxEFLAGS.Controls.Add(LabelAF);
            GroupBoxEFLAGS.Controls.Add(CheckBoxPF);
            GroupBoxEFLAGS.Controls.Add(LabelPF);
            GroupBoxEFLAGS.Controls.Add(CheckBoxCF);
            GroupBoxEFLAGS.Controls.Add(LabelCF);
            GroupBoxEFLAGS.Location = new Point(684, 5);
            GroupBoxEFLAGS.Margin = new Padding(4, 5, 4, 5);
            GroupBoxEFLAGS.Name = "GroupBoxEFLAGS";
            GroupBoxEFLAGS.Padding = new Padding(4, 5, 4, 5);
            GroupBoxEFLAGS.Size = new Size(209, 322);
            GroupBoxEFLAGS.TabIndex = 20;
            GroupBoxEFLAGS.TabStop = false;
            GroupBoxEFLAGS.Text = "EFLAGS";
            // 
            // CheckBoxID
            // 
            CheckBoxID.AutoSize = true;
            CheckBoxID.Location = new Point(180, 153);
            CheckBoxID.Margin = new Padding(4, 5, 4, 5);
            CheckBoxID.Name = "CheckBoxID";
            CheckBoxID.Size = new Size(22, 21);
            CheckBoxID.TabIndex = 33;
            CheckBoxID.UseVisualStyleBackColor = true;
            // 
            // CheckBoxVIP
            // 
            CheckBoxVIP.AutoSize = true;
            CheckBoxVIP.Location = new Point(180, 128);
            CheckBoxVIP.Margin = new Padding(4, 5, 4, 5);
            CheckBoxVIP.Name = "CheckBoxVIP";
            CheckBoxVIP.Size = new Size(22, 21);
            CheckBoxVIP.TabIndex = 32;
            CheckBoxVIP.UseVisualStyleBackColor = true;
            // 
            // CheckBoxVIF
            // 
            CheckBoxVIF.AutoSize = true;
            CheckBoxVIF.Location = new Point(180, 103);
            CheckBoxVIF.Margin = new Padding(4, 5, 4, 5);
            CheckBoxVIF.Name = "CheckBoxVIF";
            CheckBoxVIF.Size = new Size(22, 21);
            CheckBoxVIF.TabIndex = 31;
            CheckBoxVIF.UseVisualStyleBackColor = true;
            // 
            // LabelID
            // 
            LabelID.AutoSize = true;
            LabelID.Location = new Point(120, 157);
            LabelID.Margin = new Padding(4, 0, 4, 0);
            LabelID.Name = "LabelID";
            LabelID.Size = new Size(30, 25);
            LabelID.TabIndex = 30;
            LabelID.Text = "ID";
            // 
            // LabelVIP
            // 
            LabelVIP.AutoSize = true;
            LabelVIP.Location = new Point(120, 132);
            LabelVIP.Margin = new Padding(4, 0, 4, 0);
            LabelVIP.Name = "LabelVIP";
            LabelVIP.Size = new Size(38, 25);
            LabelVIP.TabIndex = 29;
            LabelVIP.Text = "VIP";
            // 
            // LabelVIF
            // 
            LabelVIF.AutoSize = true;
            LabelVIF.Location = new Point(120, 107);
            LabelVIF.Margin = new Padding(4, 0, 4, 0);
            LabelVIF.Name = "LabelVIF";
            LabelVIF.Size = new Size(37, 25);
            LabelVIF.TabIndex = 28;
            LabelVIF.Text = "VIF";
            // 
            // CheckBoxAC
            // 
            CheckBoxAC.AutoSize = true;
            CheckBoxAC.Location = new Point(180, 78);
            CheckBoxAC.Margin = new Padding(4, 5, 4, 5);
            CheckBoxAC.Name = "CheckBoxAC";
            CheckBoxAC.Size = new Size(22, 21);
            CheckBoxAC.TabIndex = 27;
            CheckBoxAC.UseVisualStyleBackColor = true;
            // 
            // LabelAC
            // 
            LabelAC.AutoSize = true;
            LabelAC.Location = new Point(120, 82);
            LabelAC.Margin = new Padding(4, 0, 4, 0);
            LabelAC.Name = "LabelAC";
            LabelAC.Size = new Size(35, 25);
            LabelAC.TabIndex = 26;
            LabelAC.Text = "AC";
            // 
            // CheckBoxVM
            // 
            CheckBoxVM.AutoSize = true;
            CheckBoxVM.Location = new Point(180, 53);
            CheckBoxVM.Margin = new Padding(4, 5, 4, 5);
            CheckBoxVM.Name = "CheckBoxVM";
            CheckBoxVM.Size = new Size(22, 21);
            CheckBoxVM.TabIndex = 25;
            CheckBoxVM.UseVisualStyleBackColor = true;
            // 
            // LabelVM
            // 
            LabelVM.AutoSize = true;
            LabelVM.Location = new Point(120, 55);
            LabelVM.Margin = new Padding(4, 0, 4, 0);
            LabelVM.Name = "LabelVM";
            LabelVM.Size = new Size(39, 25);
            LabelVM.TabIndex = 24;
            LabelVM.Text = "VM";
            // 
            // CheckBoxRF
            // 
            CheckBoxRF.AutoSize = true;
            CheckBoxRF.Location = new Point(180, 28);
            CheckBoxRF.Margin = new Padding(4, 5, 4, 5);
            CheckBoxRF.Name = "CheckBoxRF";
            CheckBoxRF.Size = new Size(22, 21);
            CheckBoxRF.TabIndex = 23;
            CheckBoxRF.UseVisualStyleBackColor = true;
            // 
            // LabelRF
            // 
            LabelRF.AutoSize = true;
            LabelRF.Location = new Point(120, 30);
            LabelRF.Margin = new Padding(4, 0, 4, 0);
            LabelRF.Name = "LabelRF";
            LabelRF.Size = new Size(32, 25);
            LabelRF.TabIndex = 22;
            LabelRF.Text = "RF";
            // 
            // CheckBoxNT
            // 
            CheckBoxNT.AutoSize = true;
            CheckBoxNT.Location = new Point(76, 282);
            CheckBoxNT.Margin = new Padding(4, 5, 4, 5);
            CheckBoxNT.Name = "CheckBoxNT";
            CheckBoxNT.Size = new Size(22, 21);
            CheckBoxNT.TabIndex = 21;
            CheckBoxNT.UseVisualStyleBackColor = true;
            // 
            // LabelNT
            // 
            LabelNT.AutoSize = true;
            LabelNT.Location = new Point(10, 282);
            LabelNT.Margin = new Padding(4, 0, 4, 0);
            LabelNT.Name = "LabelNT";
            LabelNT.Size = new Size(34, 25);
            LabelNT.TabIndex = 20;
            LabelNT.Text = "NT";
            // 
            // CheckBoxIOPL
            // 
            CheckBoxIOPL.AutoSize = true;
            CheckBoxIOPL.Location = new Point(76, 257);
            CheckBoxIOPL.Margin = new Padding(4, 5, 4, 5);
            CheckBoxIOPL.Name = "CheckBoxIOPL";
            CheckBoxIOPL.Size = new Size(22, 21);
            CheckBoxIOPL.TabIndex = 19;
            CheckBoxIOPL.UseVisualStyleBackColor = true;
            // 
            // LabelIOPL
            // 
            LabelIOPL.AutoSize = true;
            LabelIOPL.Location = new Point(10, 257);
            LabelIOPL.Margin = new Padding(4, 0, 4, 0);
            LabelIOPL.Name = "LabelIOPL";
            LabelIOPL.Size = new Size(49, 25);
            LabelIOPL.TabIndex = 18;
            LabelIOPL.Text = "IOPL";
            // 
            // CheckBoxOF
            // 
            CheckBoxOF.AutoSize = true;
            CheckBoxOF.Location = new Point(76, 232);
            CheckBoxOF.Margin = new Padding(4, 5, 4, 5);
            CheckBoxOF.Name = "CheckBoxOF";
            CheckBoxOF.Size = new Size(22, 21);
            CheckBoxOF.TabIndex = 17;
            CheckBoxOF.UseVisualStyleBackColor = true;
            // 
            // LabelOF
            // 
            LabelOF.AutoSize = true;
            LabelOF.Location = new Point(10, 232);
            LabelOF.Margin = new Padding(4, 0, 4, 0);
            LabelOF.Name = "LabelOF";
            LabelOF.Size = new Size(35, 25);
            LabelOF.TabIndex = 16;
            LabelOF.Text = "OF";
            // 
            // CheckBoxDF
            // 
            CheckBoxDF.AutoSize = true;
            CheckBoxDF.Location = new Point(76, 207);
            CheckBoxDF.Margin = new Padding(4, 5, 4, 5);
            CheckBoxDF.Name = "CheckBoxDF";
            CheckBoxDF.Size = new Size(22, 21);
            CheckBoxDF.TabIndex = 15;
            CheckBoxDF.UseVisualStyleBackColor = true;
            // 
            // LabelDF
            // 
            LabelDF.AutoSize = true;
            LabelDF.Location = new Point(10, 207);
            LabelDF.Margin = new Padding(4, 0, 4, 0);
            LabelDF.Name = "LabelDF";
            LabelDF.Size = new Size(34, 25);
            LabelDF.TabIndex = 14;
            LabelDF.Text = "DF";
            // 
            // CheckBoxIF
            // 
            CheckBoxIF.AutoSize = true;
            CheckBoxIF.Location = new Point(76, 182);
            CheckBoxIF.Margin = new Padding(4, 5, 4, 5);
            CheckBoxIF.Name = "CheckBoxIF";
            CheckBoxIF.Size = new Size(22, 21);
            CheckBoxIF.TabIndex = 13;
            CheckBoxIF.UseVisualStyleBackColor = true;
            // 
            // LabelIF
            // 
            LabelIF.AutoSize = true;
            LabelIF.Location = new Point(10, 182);
            LabelIF.Margin = new Padding(4, 0, 4, 0);
            LabelIF.Name = "LabelIF";
            LabelIF.Size = new Size(26, 25);
            LabelIF.TabIndex = 12;
            LabelIF.Text = "IF";
            // 
            // CheckBoxTF
            // 
            CheckBoxTF.AutoSize = true;
            CheckBoxTF.Location = new Point(76, 157);
            CheckBoxTF.Margin = new Padding(4, 5, 4, 5);
            CheckBoxTF.Name = "CheckBoxTF";
            CheckBoxTF.Size = new Size(22, 21);
            CheckBoxTF.TabIndex = 11;
            CheckBoxTF.UseVisualStyleBackColor = true;
            // 
            // LabelTF
            // 
            LabelTF.AutoSize = true;
            LabelTF.Location = new Point(9, 157);
            LabelTF.Margin = new Padding(4, 0, 4, 0);
            LabelTF.Name = "LabelTF";
            LabelTF.Size = new Size(30, 25);
            LabelTF.TabIndex = 10;
            LabelTF.Text = "TF";
            // 
            // CheckBoxSF
            // 
            CheckBoxSF.AutoSize = true;
            CheckBoxSF.Location = new Point(76, 132);
            CheckBoxSF.Margin = new Padding(4, 5, 4, 5);
            CheckBoxSF.Name = "CheckBoxSF";
            CheckBoxSF.Size = new Size(22, 21);
            CheckBoxSF.TabIndex = 9;
            CheckBoxSF.UseVisualStyleBackColor = true;
            // 
            // LabelSF
            // 
            LabelSF.AutoSize = true;
            LabelSF.Location = new Point(9, 132);
            LabelSF.Margin = new Padding(4, 0, 4, 0);
            LabelSF.Name = "LabelSF";
            LabelSF.Size = new Size(31, 25);
            LabelSF.TabIndex = 8;
            LabelSF.Text = "SF";
            // 
            // CheckBoxZF
            // 
            CheckBoxZF.AutoSize = true;
            CheckBoxZF.Location = new Point(76, 107);
            CheckBoxZF.Margin = new Padding(4, 5, 4, 5);
            CheckBoxZF.Name = "CheckBoxZF";
            CheckBoxZF.Size = new Size(22, 21);
            CheckBoxZF.TabIndex = 7;
            CheckBoxZF.UseVisualStyleBackColor = true;
            // 
            // LabelZF
            // 
            LabelZF.AutoSize = true;
            LabelZF.Location = new Point(9, 107);
            LabelZF.Margin = new Padding(4, 0, 4, 0);
            LabelZF.Name = "LabelZF";
            LabelZF.Size = new Size(31, 25);
            LabelZF.TabIndex = 6;
            LabelZF.Text = "ZF";
            // 
            // CheckBoxAF
            // 
            CheckBoxAF.AutoSize = true;
            CheckBoxAF.Location = new Point(76, 82);
            CheckBoxAF.Margin = new Padding(4, 5, 4, 5);
            CheckBoxAF.Name = "CheckBoxAF";
            CheckBoxAF.Size = new Size(22, 21);
            CheckBoxAF.TabIndex = 5;
            CheckBoxAF.UseVisualStyleBackColor = true;
            // 
            // LabelAF
            // 
            LabelAF.AutoSize = true;
            LabelAF.Location = new Point(9, 82);
            LabelAF.Margin = new Padding(4, 0, 4, 0);
            LabelAF.Name = "LabelAF";
            LabelAF.Size = new Size(33, 25);
            LabelAF.TabIndex = 4;
            LabelAF.Text = "AF";
            // 
            // CheckBoxPF
            // 
            CheckBoxPF.AutoSize = true;
            CheckBoxPF.Location = new Point(76, 57);
            CheckBoxPF.Margin = new Padding(4, 5, 4, 5);
            CheckBoxPF.Name = "CheckBoxPF";
            CheckBoxPF.Size = new Size(22, 21);
            CheckBoxPF.TabIndex = 3;
            CheckBoxPF.UseVisualStyleBackColor = true;
            // 
            // LabelPF
            // 
            LabelPF.AutoSize = true;
            LabelPF.Location = new Point(10, 55);
            LabelPF.Margin = new Padding(4, 0, 4, 0);
            LabelPF.Name = "LabelPF";
            LabelPF.Size = new Size(31, 25);
            LabelPF.TabIndex = 2;
            LabelPF.Text = "PF";
            // 
            // CheckBoxCF
            // 
            CheckBoxCF.AutoSize = true;
            CheckBoxCF.Location = new Point(76, 32);
            CheckBoxCF.Margin = new Padding(4, 5, 4, 5);
            CheckBoxCF.Name = "CheckBoxCF";
            CheckBoxCF.Size = new Size(22, 21);
            CheckBoxCF.TabIndex = 1;
            CheckBoxCF.UseVisualStyleBackColor = true;
            // 
            // LabelCF
            // 
            LabelCF.AutoSize = true;
            LabelCF.Location = new Point(10, 32);
            LabelCF.Margin = new Padding(4, 0, 4, 0);
            LabelCF.Name = "LabelCF";
            LabelCF.Size = new Size(32, 25);
            LabelCF.TabIndex = 0;
            LabelCF.Text = "CF";
            // 
            // ButtonSetRegisterValues
            // 
            ButtonSetRegisterValues.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            ButtonSetRegisterValues.Location = new Point(11, 337);
            ButtonSetRegisterValues.Margin = new Padding(4, 5, 4, 5);
            ButtonSetRegisterValues.Name = "ButtonSetRegisterValues";
            ButtonSetRegisterValues.Size = new Size(881, 77);
            ButtonSetRegisterValues.TabIndex = 19;
            ButtonSetRegisterValues.Text = "Set Register Values";
            ButtonSetRegisterValues.UseVisualStyleBackColor = true;
            ButtonSetRegisterValues.Click += ButtonSetRegisterValues_Click;
            // 
            // GroupBoxGeneralPurposeRegisters
            // 
            GroupBoxGeneralPurposeRegisters.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            GroupBoxGeneralPurposeRegisters.Controls.Add(TextBoxR15);
            GroupBoxGeneralPurposeRegisters.Controls.Add(LabelR15);
            GroupBoxGeneralPurposeRegisters.Controls.Add(TextBoxR14);
            GroupBoxGeneralPurposeRegisters.Controls.Add(LabelR14);
            GroupBoxGeneralPurposeRegisters.Controls.Add(TextBoxR13);
            GroupBoxGeneralPurposeRegisters.Controls.Add(LabelR13);
            GroupBoxGeneralPurposeRegisters.Controls.Add(TextBoxR12);
            GroupBoxGeneralPurposeRegisters.Controls.Add(LabelR12);
            GroupBoxGeneralPurposeRegisters.Controls.Add(TextBoxR11);
            GroupBoxGeneralPurposeRegisters.Controls.Add(LabelR11);
            GroupBoxGeneralPurposeRegisters.Controls.Add(TextBoxR10);
            GroupBoxGeneralPurposeRegisters.Controls.Add(LabelR10);
            GroupBoxGeneralPurposeRegisters.Controls.Add(TextBoxR9);
            GroupBoxGeneralPurposeRegisters.Controls.Add(LabelR9);
            GroupBoxGeneralPurposeRegisters.Controls.Add(TextBoxR8);
            GroupBoxGeneralPurposeRegisters.Controls.Add(LabelR8);
            GroupBoxGeneralPurposeRegisters.Controls.Add(LabelRAX);
            GroupBoxGeneralPurposeRegisters.Controls.Add(TextBoxRIP);
            GroupBoxGeneralPurposeRegisters.Controls.Add(TextBoxRAX);
            GroupBoxGeneralPurposeRegisters.Controls.Add(LabelRIP);
            GroupBoxGeneralPurposeRegisters.Controls.Add(LabelRBX);
            GroupBoxGeneralPurposeRegisters.Controls.Add(TextBoxRBP);
            GroupBoxGeneralPurposeRegisters.Controls.Add(TextBoxRBX);
            GroupBoxGeneralPurposeRegisters.Controls.Add(LabelRBP);
            GroupBoxGeneralPurposeRegisters.Controls.Add(LabelRCX);
            GroupBoxGeneralPurposeRegisters.Controls.Add(TextBoxRSP);
            GroupBoxGeneralPurposeRegisters.Controls.Add(TextBoxRCX);
            GroupBoxGeneralPurposeRegisters.Controls.Add(LabelRSP);
            GroupBoxGeneralPurposeRegisters.Controls.Add(LabelRDX);
            GroupBoxGeneralPurposeRegisters.Controls.Add(TextBoxRDI);
            GroupBoxGeneralPurposeRegisters.Controls.Add(TextBoxRDX);
            GroupBoxGeneralPurposeRegisters.Controls.Add(LabelRDI);
            GroupBoxGeneralPurposeRegisters.Controls.Add(LabelRSI);
            GroupBoxGeneralPurposeRegisters.Controls.Add(TextBoxRSI);
            GroupBoxGeneralPurposeRegisters.Location = new Point(11, 5);
            GroupBoxGeneralPurposeRegisters.Margin = new Padding(4, 5, 4, 5);
            GroupBoxGeneralPurposeRegisters.Name = "GroupBoxGeneralPurposeRegisters";
            GroupBoxGeneralPurposeRegisters.Padding = new Padding(4, 5, 4, 5);
            GroupBoxGeneralPurposeRegisters.Size = new Size(664, 322);
            GroupBoxGeneralPurposeRegisters.TabIndex = 18;
            GroupBoxGeneralPurposeRegisters.TabStop = false;
            GroupBoxGeneralPurposeRegisters.Text = "General Purpose Register";
            // 
            // TextBoxR15
            // 
            TextBoxR15.Location = new Point(510, 220);
            TextBoxR15.Margin = new Padding(4, 5, 4, 5);
            TextBoxR15.Name = "TextBoxR15";
            TextBoxR15.Size = new Size(141, 31);
            TextBoxR15.TabIndex = 33;
            // 
            // LabelR15
            // 
            LabelR15.AutoSize = true;
            LabelR15.Location = new Point(447, 225);
            LabelR15.Margin = new Padding(4, 0, 4, 0);
            LabelR15.Name = "LabelR15";
            LabelR15.Size = new Size(43, 25);
            LabelR15.TabIndex = 32;
            LabelR15.Text = "R15";
            // 
            // TextBoxR14
            // 
            TextBoxR14.Location = new Point(510, 172);
            TextBoxR14.Margin = new Padding(4, 5, 4, 5);
            TextBoxR14.Name = "TextBoxR14";
            TextBoxR14.Size = new Size(141, 31);
            TextBoxR14.TabIndex = 31;
            // 
            // LabelR14
            // 
            LabelR14.AutoSize = true;
            LabelR14.Location = new Point(447, 177);
            LabelR14.Margin = new Padding(4, 0, 4, 0);
            LabelR14.Name = "LabelR14";
            LabelR14.Size = new Size(43, 25);
            LabelR14.TabIndex = 30;
            LabelR14.Text = "R14";
            // 
            // TextBoxR13
            // 
            TextBoxR13.Location = new Point(510, 123);
            TextBoxR13.Margin = new Padding(4, 5, 4, 5);
            TextBoxR13.Name = "TextBoxR13";
            TextBoxR13.Size = new Size(141, 31);
            TextBoxR13.TabIndex = 29;
            // 
            // LabelR13
            // 
            LabelR13.AutoSize = true;
            LabelR13.Location = new Point(447, 128);
            LabelR13.Margin = new Padding(4, 0, 4, 0);
            LabelR13.Name = "LabelR13";
            LabelR13.Size = new Size(43, 25);
            LabelR13.TabIndex = 28;
            LabelR13.Text = "R13";
            // 
            // TextBoxR12
            // 
            TextBoxR12.Location = new Point(510, 75);
            TextBoxR12.Margin = new Padding(4, 5, 4, 5);
            TextBoxR12.Name = "TextBoxR12";
            TextBoxR12.Size = new Size(141, 31);
            TextBoxR12.TabIndex = 27;
            // 
            // LabelR12
            // 
            LabelR12.AutoSize = true;
            LabelR12.Location = new Point(447, 80);
            LabelR12.Margin = new Padding(4, 0, 4, 0);
            LabelR12.Name = "LabelR12";
            LabelR12.Size = new Size(43, 25);
            LabelR12.TabIndex = 26;
            LabelR12.Text = "R12";
            // 
            // TextBoxR11
            // 
            TextBoxR11.Location = new Point(510, 27);
            TextBoxR11.Margin = new Padding(4, 5, 4, 5);
            TextBoxR11.Name = "TextBoxR11";
            TextBoxR11.Size = new Size(141, 31);
            TextBoxR11.TabIndex = 25;
            // 
            // LabelR11
            // 
            LabelR11.AutoSize = true;
            LabelR11.Location = new Point(447, 32);
            LabelR11.Margin = new Padding(4, 0, 4, 0);
            LabelR11.Name = "LabelR11";
            LabelR11.Size = new Size(43, 25);
            LabelR11.TabIndex = 24;
            LabelR11.Text = "R11";
            // 
            // TextBoxR10
            // 
            TextBoxR10.Location = new Point(291, 268);
            TextBoxR10.Margin = new Padding(4, 5, 4, 5);
            TextBoxR10.Name = "TextBoxR10";
            TextBoxR10.Size = new Size(141, 31);
            TextBoxR10.TabIndex = 23;
            // 
            // LabelR10
            // 
            LabelR10.AutoSize = true;
            LabelR10.Location = new Point(229, 273);
            LabelR10.Margin = new Padding(4, 0, 4, 0);
            LabelR10.Name = "LabelR10";
            LabelR10.Size = new Size(43, 25);
            LabelR10.TabIndex = 22;
            LabelR10.Text = "R10";
            // 
            // TextBoxR9
            // 
            TextBoxR9.Location = new Point(291, 220);
            TextBoxR9.Margin = new Padding(4, 5, 4, 5);
            TextBoxR9.Name = "TextBoxR9";
            TextBoxR9.Size = new Size(141, 31);
            TextBoxR9.TabIndex = 21;
            // 
            // LabelR9
            // 
            LabelR9.AutoSize = true;
            LabelR9.Location = new Point(229, 225);
            LabelR9.Margin = new Padding(4, 0, 4, 0);
            LabelR9.Name = "LabelR9";
            LabelR9.Size = new Size(33, 25);
            LabelR9.TabIndex = 20;
            LabelR9.Text = "R9";
            // 
            // TextBoxR8
            // 
            TextBoxR8.Location = new Point(291, 172);
            TextBoxR8.Margin = new Padding(4, 5, 4, 5);
            TextBoxR8.Name = "TextBoxR8";
            TextBoxR8.Size = new Size(141, 31);
            TextBoxR8.TabIndex = 19;
            // 
            // LabelR8
            // 
            LabelR8.AutoSize = true;
            LabelR8.Location = new Point(229, 177);
            LabelR8.Margin = new Padding(4, 0, 4, 0);
            LabelR8.Name = "LabelR8";
            LabelR8.Size = new Size(33, 25);
            LabelR8.TabIndex = 18;
            LabelR8.Text = "R8";
            // 
            // LabelRAX
            // 
            LabelRAX.AutoSize = true;
            LabelRAX.Location = new Point(9, 32);
            LabelRAX.Margin = new Padding(4, 0, 4, 0);
            LabelRAX.Name = "LabelRAX";
            LabelRAX.Size = new Size(46, 25);
            LabelRAX.TabIndex = 0;
            LabelRAX.Text = "RAX";
            // 
            // TextBoxRIP
            // 
            TextBoxRIP.Location = new Point(291, 123);
            TextBoxRIP.Margin = new Padding(4, 5, 4, 5);
            TextBoxRIP.Name = "TextBoxRIP";
            TextBoxRIP.Size = new Size(141, 31);
            TextBoxRIP.TabIndex = 17;
            // 
            // TextBoxRAX
            // 
            TextBoxRAX.Location = new Point(71, 27);
            TextBoxRAX.Margin = new Padding(4, 5, 4, 5);
            TextBoxRAX.Name = "TextBoxRAX";
            TextBoxRAX.Size = new Size(141, 31);
            TextBoxRAX.TabIndex = 1;
            // 
            // LabelRIP
            // 
            LabelRIP.AutoSize = true;
            LabelRIP.Location = new Point(229, 128);
            LabelRIP.Margin = new Padding(4, 0, 4, 0);
            LabelRIP.Name = "LabelRIP";
            LabelRIP.Size = new Size(38, 25);
            LabelRIP.TabIndex = 16;
            LabelRIP.Text = "RIP";
            // 
            // LabelRBX
            // 
            LabelRBX.AutoSize = true;
            LabelRBX.Location = new Point(9, 80);
            LabelRBX.Margin = new Padding(4, 0, 4, 0);
            LabelRBX.Name = "LabelRBX";
            LabelRBX.Size = new Size(44, 25);
            LabelRBX.TabIndex = 2;
            LabelRBX.Text = "RBX";
            // 
            // TextBoxRBP
            // 
            TextBoxRBP.Location = new Point(291, 75);
            TextBoxRBP.Margin = new Padding(4, 5, 4, 5);
            TextBoxRBP.Name = "TextBoxRBP";
            TextBoxRBP.Size = new Size(141, 31);
            TextBoxRBP.TabIndex = 15;
            // 
            // TextBoxRBX
            // 
            TextBoxRBX.Location = new Point(71, 75);
            TextBoxRBX.Margin = new Padding(4, 5, 4, 5);
            TextBoxRBX.Name = "TextBoxRBX";
            TextBoxRBX.Size = new Size(141, 31);
            TextBoxRBX.TabIndex = 3;
            // 
            // LabelRBP
            // 
            LabelRBP.AutoSize = true;
            LabelRBP.Location = new Point(229, 80);
            LabelRBP.Margin = new Padding(4, 0, 4, 0);
            LabelRBP.Name = "LabelRBP";
            LabelRBP.Size = new Size(43, 25);
            LabelRBP.TabIndex = 14;
            LabelRBP.Text = "RBP";
            // 
            // LabelRCX
            // 
            LabelRCX.AutoSize = true;
            LabelRCX.Location = new Point(9, 128);
            LabelRCX.Margin = new Padding(4, 0, 4, 0);
            LabelRCX.Name = "LabelRCX";
            LabelRCX.Size = new Size(45, 25);
            LabelRCX.TabIndex = 4;
            LabelRCX.Text = "RCX";
            // 
            // TextBoxRSP
            // 
            TextBoxRSP.Location = new Point(291, 27);
            TextBoxRSP.Margin = new Padding(4, 5, 4, 5);
            TextBoxRSP.Name = "TextBoxRSP";
            TextBoxRSP.Size = new Size(141, 31);
            TextBoxRSP.TabIndex = 13;
            // 
            // TextBoxRCX
            // 
            TextBoxRCX.Location = new Point(71, 123);
            TextBoxRCX.Margin = new Padding(4, 5, 4, 5);
            TextBoxRCX.Name = "TextBoxRCX";
            TextBoxRCX.Size = new Size(141, 31);
            TextBoxRCX.TabIndex = 5;
            // 
            // LabelRSP
            // 
            LabelRSP.AutoSize = true;
            LabelRSP.Location = new Point(229, 32);
            LabelRSP.Margin = new Padding(4, 0, 4, 0);
            LabelRSP.Name = "LabelRSP";
            LabelRSP.Size = new Size(43, 25);
            LabelRSP.TabIndex = 12;
            LabelRSP.Text = "RSP";
            // 
            // LabelRDX
            // 
            LabelRDX.AutoSize = true;
            LabelRDX.Location = new Point(9, 177);
            LabelRDX.Margin = new Padding(4, 0, 4, 0);
            LabelRDX.Name = "LabelRDX";
            LabelRDX.Size = new Size(46, 25);
            LabelRDX.TabIndex = 6;
            LabelRDX.Text = "RDX";
            // 
            // TextBoxRDI
            // 
            TextBoxRDI.Location = new Point(71, 268);
            TextBoxRDI.Margin = new Padding(4, 5, 4, 5);
            TextBoxRDI.Name = "TextBoxRDI";
            TextBoxRDI.Size = new Size(141, 31);
            TextBoxRDI.TabIndex = 11;
            // 
            // TextBoxRDX
            // 
            TextBoxRDX.Location = new Point(71, 172);
            TextBoxRDX.Margin = new Padding(4, 5, 4, 5);
            TextBoxRDX.Name = "TextBoxRDX";
            TextBoxRDX.Size = new Size(141, 31);
            TextBoxRDX.TabIndex = 7;
            // 
            // LabelRDI
            // 
            LabelRDI.AutoSize = true;
            LabelRDI.Location = new Point(9, 273);
            LabelRDI.Margin = new Padding(4, 0, 4, 0);
            LabelRDI.Name = "LabelRDI";
            LabelRDI.Size = new Size(41, 25);
            LabelRDI.TabIndex = 10;
            LabelRDI.Text = "RDI";
            // 
            // LabelRSI
            // 
            LabelRSI.AutoSize = true;
            LabelRSI.Location = new Point(9, 225);
            LabelRSI.Margin = new Padding(4, 0, 4, 0);
            LabelRSI.Name = "LabelRSI";
            LabelRSI.Size = new Size(38, 25);
            LabelRSI.TabIndex = 8;
            LabelRSI.Text = "RSI";
            // 
            // TextBoxRSI
            // 
            TextBoxRSI.Location = new Point(71, 220);
            TextBoxRSI.Margin = new Padding(4, 5, 4, 5);
            TextBoxRSI.Name = "TextBoxRSI";
            TextBoxRSI.Size = new Size(141, 31);
            TextBoxRSI.TabIndex = 9;
            // 
            // Output
            // 
            Output.Controls.Add(RichtextBoxOutput);
            Output.Location = new Point(4, 34);
            Output.Margin = new Padding(4, 5, 4, 5);
            Output.Name = "Output";
            Output.Size = new Size(915, 437);
            Output.TabIndex = 3;
            Output.Text = "Output";
            Output.UseVisualStyleBackColor = true;
            // 
            // RichtextBoxOutput
            // 
            RichtextBoxOutput.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            RichtextBoxOutput.Enabled = false;
            RichtextBoxOutput.Location = new Point(4, 5);
            RichtextBoxOutput.Margin = new Padding(4, 5, 4, 5);
            RichtextBoxOutput.Name = "RichtextBoxOutput";
            RichtextBoxOutput.Size = new Size(901, 416);
            RichtextBoxOutput.TabIndex = 0;
            RichtextBoxOutput.Text = "";
            // 
            // ButtonExecuteOnAnotherTab
            // 
            ButtonExecuteOnAnotherTab.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            ButtonExecuteOnAnotherTab.Location = new Point(774, 0);
            ButtonExecuteOnAnotherTab.Margin = new Padding(4, 5, 4, 5);
            ButtonExecuteOnAnotherTab.Name = "ButtonExecuteOnAnotherTab";
            ButtonExecuteOnAnotherTab.Size = new Size(143, 38);
            ButtonExecuteOnAnotherTab.TabIndex = 2;
            ButtonExecuteOnAnotherTab.Text = "Execute";
            ButtonExecuteOnAnotherTab.UseVisualStyleBackColor = true;
            ButtonExecuteOnAnotherTab.Visible = false;
            ButtonExecuteOnAnotherTab.Click += ButtonExecuteOnAnotherTab_Click;
            // 
            // tabsToolStripMenuItem
            // 
            tabsToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { memoryToolStripMenuItem, registersToolStripMenuItem });
            tabsToolStripMenuItem.Name = "tabsToolStripMenuItem";
            tabsToolStripMenuItem.Size = new Size(63, 29);
            tabsToolStripMenuItem.Text = "Tabs";
            // 
            // memoryToolStripMenuItem
            // 
            memoryToolStripMenuItem.Name = "memoryToolStripMenuItem";
            memoryToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.M;
            memoryToolStripMenuItem.Size = new Size(270, 34);
            memoryToolStripMenuItem.Text = "Memory";
            memoryToolStripMenuItem.Click += memoryToolStripMenuItem_Click;
            // 
            // registersToolStripMenuItem
            // 
            registersToolStripMenuItem.Name = "registersToolStripMenuItem";
            registersToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.R;
            registersToolStripMenuItem.Size = new Size(270, 34);
            registersToolStripMenuItem.Text = "Registers";
            registersToolStripMenuItem.Click += registersToolStripMenuItem_Click;
            // 
            // refreshVideoFormTimer
            // 
            refreshVideoFormTimer.Interval = 20;
            refreshVideoFormTimer.Tick += refreshVideoFormTimer_Tick;
            // 
            // mainForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(920, 518);
            Controls.Add(ButtonExecuteOnAnotherTab);
            Controls.Add(EmuXTabControl);
            Controls.Add(mainMenuStrip);
            MainMenuStrip = mainMenuStrip;
            Margin = new Padding(4, 5, 4, 5);
            MinimumSize = new Size(929, 546);
            Name = "mainForm";
            Text = "EmuX";
            mainMenuStrip.ResumeLayout(false);
            mainMenuStrip.PerformLayout();
            GroupboxInput.ResumeLayout(false);
            GroupboxControls.ResumeLayout(false);
            GroupboxControls.PerformLayout();
            GroupboxExecutionProgress.ResumeLayout(false);
            GroupboxExecution.ResumeLayout(false);
            EmuXTabControl.ResumeLayout(false);
            Assembly.ResumeLayout(false);
            Memory.ResumeLayout(false);
            GroupBoxMemoryControls.ResumeLayout(false);
            GroupBoxMemoryControls.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)DataGridViewMemory).EndInit();
            Registers.ResumeLayout(false);
            GroupBoxEFLAGS.ResumeLayout(false);
            GroupBoxEFLAGS.PerformLayout();
            GroupBoxGeneralPurposeRegisters.ResumeLayout(false);
            GroupBoxGeneralPurposeRegisters.PerformLayout();
            Output.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip mainMenuStrip;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem openToolStripMenuItem;
        private ToolStripMenuItem saveToolStripMenuItem;
        private ToolStripMenuItem saveAsToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem exitToolStripMenuItem;
        private ToolStripMenuItem aboutToolStripMenuItem;
        private GroupBox GroupboxInput;
        private RichTextBox RichTextboxAssemblyCode;
        private GroupBox GroupboxControls;
        private GroupBox GroupboxExecutionProgress;
        private ProgressBar ProgressBarExecutionProgress;
        private Label LabelCurrentInstruction;
        private GroupBox GroupboxExecution;
        private Button ButtonExecute;
        private Button ButtonPreviousInstruction;
        private Button ButtonNextInstruction;
        private OpenFileDialog openFD;
        private SaveFileDialog saveFD;
        private TabControl EmuXTabControl;
        private TabPage Assembly;
        private TabPage Memory;
        private TabPage Registers;
        private TabPage Output;
        private GroupBox GroupBoxMemoryControls;
        private Label labelDataRepresentation;
        private Label LabelMemoryRange;
        private Button ButtonSetMemoryValue;
        private Button ButtonSearchMemoryRange;
        private TextBox TextBoxMemoryValue;
        private TextBox TextBoxMemoryRangeEnd;
        private TextBox TextBoxMemoryRangeStart;
        private ComboBox ComboBoxMemoryRepresentation;
        private DataGridView DataGridViewMemory;
        private Label LabelValue;
        private Button ButtonClearMemoryTable;
        private GroupBox GroupBoxGeneralPurposeRegisters;
        private Label LabelRAX;
        private TextBox TextBoxRIP;
        private TextBox TextBoxRAX;
        private Label LabelRIP;
        private Label LabelRBX;
        private TextBox TextBoxRBP;
        private TextBox TextBoxRBX;
        private Label LabelRBP;
        private Label LabelRCX;
        private TextBox TextBoxRSP;
        private TextBox TextBoxRCX;
        private Label LabelRSP;
        private Label LabelRDX;
        private TextBox TextBoxRDI;
        private TextBox TextBoxRDX;
        private Label LabelRDI;
        private Label LabelRSI;
        private TextBox TextBoxRSI;
        private RichTextBox RichtextBoxOutput;
        private ToolStripMenuItem textToolStripMenuItem;
        private ToolStripMenuItem tODOToolStripMenuItem;
        private GroupBox GroupBoxEFLAGS;
        private CheckBox CheckBoxCF;
        private Label LabelCF;
        private Button ButtonSetRegisterValues;
        private TextBox TextBoxR15;
        private Label LabelR15;
        private TextBox TextBoxR14;
        private Label LabelR14;
        private TextBox TextBoxR13;
        private Label LabelR13;
        private TextBox TextBoxR12;
        private Label LabelR12;
        private TextBox TextBoxR11;
        private Label LabelR11;
        private TextBox TextBoxR10;
        private Label LabelR10;
        private TextBox TextBoxR9;
        private Label LabelR9;
        private TextBox TextBoxR8;
        private Label LabelR8;
        private CheckBox CheckBoxAC;
        private Label LabelAC;
        private CheckBox CheckBoxVM;
        private Label LabelVM;
        private CheckBox CheckBoxRF;
        private Label LabelRF;
        private CheckBox CheckBoxNT;
        private Label LabelNT;
        private CheckBox CheckBoxIOPL;
        private Label LabelIOPL;
        private CheckBox CheckBoxOF;
        private Label LabelOF;
        private CheckBox CheckBoxDF;
        private Label LabelDF;
        private CheckBox CheckBoxIF;
        private Label LabelIF;
        private CheckBox CheckBoxTF;
        private Label LabelTF;
        private CheckBox CheckBoxSF;
        private Label LabelSF;
        private CheckBox CheckBoxZF;
        private Label LabelZF;
        private CheckBox CheckBoxAF;
        private Label LabelAF;
        private CheckBox CheckBoxPF;
        private Label LabelPF;
        private CheckBox CheckBoxID;
        private CheckBox CheckBoxVIP;
        private CheckBox CheckBoxVIF;
        private Label LabelID;
        private Label LabelVIP;
        private Label LabelVIF;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripMenuItem increaseSizeToolStripMenuItem;
        private ToolStripMenuItem decreaseSizeToolStripMenuItem;
        private FontDialog fontDialog;
        private ToolStripSeparator toolStripSeparator3;
        private ToolStripMenuItem convertToUppercaseToolStripMenuItem;
        private ToolStripMenuItem convertToLowercaseToolStripMenuItem;
        private ToolStripMenuItem toolsToolStripMenuItem;
        private ToolStripMenuItem converterToolStripMenuItem;
        private ToolStripMenuItem aSCIITableToolStripMenuItem;
        private Button ButtonExecuteOnAnotherTab;
        private ToolStripMenuItem instructionsToolStripMenuItem;
        private ToolStripMenuItem tabsToolStripMenuItem;
        private ToolStripMenuItem memoryToolStripMenuItem;
        private ToolStripMenuItem registersToolStripMenuItem;
        private System.Windows.Forms.Timer refreshVideoFormTimer;
    }
}