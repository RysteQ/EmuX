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
            this.mainMenuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.textToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tODOToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.increaseSizeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.decreaseSizeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.convertToUppercaseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.convertToLowercaseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.converterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aSCIITableToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.instructionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.GroupboxInput = new System.Windows.Forms.GroupBox();
            this.RichTextboxAssemblyCode = new System.Windows.Forms.RichTextBox();
            this.GroupboxControls = new System.Windows.Forms.GroupBox();
            this.GroupboxExecutionProgress = new System.Windows.Forms.GroupBox();
            this.ProgressBarExecutionProgress = new System.Windows.Forms.ProgressBar();
            this.LabelCurrentInstruction = new System.Windows.Forms.Label();
            this.GroupboxExecution = new System.Windows.Forms.GroupBox();
            this.ButtonExecute = new System.Windows.Forms.Button();
            this.ButtonPreviousInstruction = new System.Windows.Forms.Button();
            this.ButtonNextInstruction = new System.Windows.Forms.Button();
            this.openFD = new System.Windows.Forms.OpenFileDialog();
            this.saveFD = new System.Windows.Forms.SaveFileDialog();
            this.EmuXTabControl = new System.Windows.Forms.TabControl();
            this.Assembly = new System.Windows.Forms.TabPage();
            this.Memory = new System.Windows.Forms.TabPage();
            this.GroupBoxMemoryControls = new System.Windows.Forms.GroupBox();
            this.ButtonClearMemoryTable = new System.Windows.Forms.Button();
            this.LabelValue = new System.Windows.Forms.Label();
            this.labelDataRepresentation = new System.Windows.Forms.Label();
            this.LabelMemoryRange = new System.Windows.Forms.Label();
            this.ButtonSetMemoryValue = new System.Windows.Forms.Button();
            this.ButtonSearchMemoryRange = new System.Windows.Forms.Button();
            this.TextBoxMemoryValue = new System.Windows.Forms.TextBox();
            this.TextBoxMemoryRangeEnd = new System.Windows.Forms.TextBox();
            this.TextBoxMemoryRangeStart = new System.Windows.Forms.TextBox();
            this.ComboBoxMemoryRepresentation = new System.Windows.Forms.ComboBox();
            this.DataGridViewMemory = new System.Windows.Forms.DataGridView();
            this.Registers = new System.Windows.Forms.TabPage();
            this.GroupBoxEFLAGS = new System.Windows.Forms.GroupBox();
            this.CheckBoxID = new System.Windows.Forms.CheckBox();
            this.CheckBoxVIP = new System.Windows.Forms.CheckBox();
            this.CheckBoxVIF = new System.Windows.Forms.CheckBox();
            this.LabelID = new System.Windows.Forms.Label();
            this.LabelVIP = new System.Windows.Forms.Label();
            this.LabelVIF = new System.Windows.Forms.Label();
            this.CheckBoxAC = new System.Windows.Forms.CheckBox();
            this.LabelAC = new System.Windows.Forms.Label();
            this.CheckBoxVM = new System.Windows.Forms.CheckBox();
            this.LabelVM = new System.Windows.Forms.Label();
            this.CheckBoxRF = new System.Windows.Forms.CheckBox();
            this.LabelRF = new System.Windows.Forms.Label();
            this.CheckBoxNT = new System.Windows.Forms.CheckBox();
            this.LabelNT = new System.Windows.Forms.Label();
            this.CheckBoxIOPL = new System.Windows.Forms.CheckBox();
            this.LabelIOPL = new System.Windows.Forms.Label();
            this.CheckBoxOF = new System.Windows.Forms.CheckBox();
            this.LabelOF = new System.Windows.Forms.Label();
            this.CheckBoxDF = new System.Windows.Forms.CheckBox();
            this.LabelDF = new System.Windows.Forms.Label();
            this.CheckBoxIF = new System.Windows.Forms.CheckBox();
            this.LabelIF = new System.Windows.Forms.Label();
            this.CheckBoxTF = new System.Windows.Forms.CheckBox();
            this.LabelTF = new System.Windows.Forms.Label();
            this.CheckBoxSF = new System.Windows.Forms.CheckBox();
            this.LabelSF = new System.Windows.Forms.Label();
            this.CheckBoxZF = new System.Windows.Forms.CheckBox();
            this.LabelZF = new System.Windows.Forms.Label();
            this.CheckBoxAF = new System.Windows.Forms.CheckBox();
            this.LabelAF = new System.Windows.Forms.Label();
            this.CheckBoxPF = new System.Windows.Forms.CheckBox();
            this.LabelPF = new System.Windows.Forms.Label();
            this.CheckBoxCF = new System.Windows.Forms.CheckBox();
            this.LabelCF = new System.Windows.Forms.Label();
            this.ButtonSetRegisterValues = new System.Windows.Forms.Button();
            this.GroupBoxGeneralPurposeRegisters = new System.Windows.Forms.GroupBox();
            this.ButtonResetVirtualSystem = new System.Windows.Forms.Button();
            this.TextBoxR15 = new System.Windows.Forms.TextBox();
            this.LabelR15 = new System.Windows.Forms.Label();
            this.TextBoxR14 = new System.Windows.Forms.TextBox();
            this.LabelR14 = new System.Windows.Forms.Label();
            this.TextBoxR13 = new System.Windows.Forms.TextBox();
            this.LabelR13 = new System.Windows.Forms.Label();
            this.TextBoxR12 = new System.Windows.Forms.TextBox();
            this.LabelR12 = new System.Windows.Forms.Label();
            this.TextBoxR11 = new System.Windows.Forms.TextBox();
            this.LabelR11 = new System.Windows.Forms.Label();
            this.TextBoxR10 = new System.Windows.Forms.TextBox();
            this.LabelR10 = new System.Windows.Forms.Label();
            this.TextBoxR9 = new System.Windows.Forms.TextBox();
            this.LabelR9 = new System.Windows.Forms.Label();
            this.TextBoxR8 = new System.Windows.Forms.TextBox();
            this.LabelR8 = new System.Windows.Forms.Label();
            this.LabelRAX = new System.Windows.Forms.Label();
            this.TextBoxRIP = new System.Windows.Forms.TextBox();
            this.TextBoxRAX = new System.Windows.Forms.TextBox();
            this.LabelRIP = new System.Windows.Forms.Label();
            this.LabelRBX = new System.Windows.Forms.Label();
            this.TextBoxRBP = new System.Windows.Forms.TextBox();
            this.TextBoxRBX = new System.Windows.Forms.TextBox();
            this.LabelRBP = new System.Windows.Forms.Label();
            this.LabelRCX = new System.Windows.Forms.Label();
            this.TextBoxRSP = new System.Windows.Forms.TextBox();
            this.TextBoxRCX = new System.Windows.Forms.TextBox();
            this.LabelRSP = new System.Windows.Forms.Label();
            this.LabelRDX = new System.Windows.Forms.Label();
            this.TextBoxRDI = new System.Windows.Forms.TextBox();
            this.TextBoxRDX = new System.Windows.Forms.TextBox();
            this.LabelRDI = new System.Windows.Forms.Label();
            this.LabelRSI = new System.Windows.Forms.Label();
            this.TextBoxRSI = new System.Windows.Forms.TextBox();
            this.Output = new System.Windows.Forms.TabPage();
            this.RichtextBoxOutput = new System.Windows.Forms.RichTextBox();
            this.fontDialog = new System.Windows.Forms.FontDialog();
            this.ButtonExecuteOnAnotherTab = new System.Windows.Forms.Button();
            this.mainMenuStrip.SuspendLayout();
            this.GroupboxInput.SuspendLayout();
            this.GroupboxControls.SuspendLayout();
            this.GroupboxExecutionProgress.SuspendLayout();
            this.GroupboxExecution.SuspendLayout();
            this.EmuXTabControl.SuspendLayout();
            this.Assembly.SuspendLayout();
            this.Memory.SuspendLayout();
            this.GroupBoxMemoryControls.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridViewMemory)).BeginInit();
            this.Registers.SuspendLayout();
            this.GroupBoxEFLAGS.SuspendLayout();
            this.GroupBoxGeneralPurposeRegisters.SuspendLayout();
            this.Output.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainMenuStrip
            // 
            this.mainMenuStrip.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.mainMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.textToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.mainMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.mainMenuStrip.Name = "mainMenuStrip";
            this.mainMenuStrip.Size = new System.Drawing.Size(644, 24);
            this.mainMenuStrip.TabIndex = 0;
            this.mainMenuStrip.Text = "mainMenuStrip";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.toolStripSeparator1,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.S)));
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.saveAsToolStripMenuItem.Text = "Save As";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(183, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // textToolStripMenuItem
            // 
            this.textToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tODOToolStripMenuItem,
            this.toolStripSeparator2,
            this.increaseSizeToolStripMenuItem,
            this.decreaseSizeToolStripMenuItem,
            this.toolStripSeparator3,
            this.convertToUppercaseToolStripMenuItem,
            this.convertToLowercaseToolStripMenuItem});
            this.textToolStripMenuItem.Name = "textToolStripMenuItem";
            this.textToolStripMenuItem.Size = new System.Drawing.Size(40, 20);
            this.textToolStripMenuItem.Text = "Text";
            // 
            // tODOToolStripMenuItem
            // 
            this.tODOToolStripMenuItem.Name = "tODOToolStripMenuItem";
            this.tODOToolStripMenuItem.Size = new System.Drawing.Size(231, 22);
            this.tODOToolStripMenuItem.Text = "Change Font";
            this.tODOToolStripMenuItem.Click += new System.EventHandler(this.tODOToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(228, 6);
            // 
            // increaseSizeToolStripMenuItem
            // 
            this.increaseSizeToolStripMenuItem.Name = "increaseSizeToolStripMenuItem";
            this.increaseSizeToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.I)));
            this.increaseSizeToolStripMenuItem.Size = new System.Drawing.Size(231, 22);
            this.increaseSizeToolStripMenuItem.Text = "Increase Size";
            this.increaseSizeToolStripMenuItem.Click += new System.EventHandler(this.increaseSizeToolStripMenuItem_Click);
            // 
            // decreaseSizeToolStripMenuItem
            // 
            this.decreaseSizeToolStripMenuItem.Name = "decreaseSizeToolStripMenuItem";
            this.decreaseSizeToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D)));
            this.decreaseSizeToolStripMenuItem.Size = new System.Drawing.Size(231, 22);
            this.decreaseSizeToolStripMenuItem.Text = "Decrease Size";
            this.decreaseSizeToolStripMenuItem.Click += new System.EventHandler(this.decreaseSizeToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(228, 6);
            // 
            // convertToUppercaseToolStripMenuItem
            // 
            this.convertToUppercaseToolStripMenuItem.Name = "convertToUppercaseToolStripMenuItem";
            this.convertToUppercaseToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.U)));
            this.convertToUppercaseToolStripMenuItem.Size = new System.Drawing.Size(231, 22);
            this.convertToUppercaseToolStripMenuItem.Text = "Convert To Uppercase";
            this.convertToUppercaseToolStripMenuItem.Click += new System.EventHandler(this.convertToUppercaseToolStripMenuItem_Click);
            // 
            // convertToLowercaseToolStripMenuItem
            // 
            this.convertToLowercaseToolStripMenuItem.Name = "convertToLowercaseToolStripMenuItem";
            this.convertToLowercaseToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.L)));
            this.convertToLowercaseToolStripMenuItem.Size = new System.Drawing.Size(231, 22);
            this.convertToLowercaseToolStripMenuItem.Text = "Convert To Lowercase";
            this.convertToLowercaseToolStripMenuItem.Click += new System.EventHandler(this.convertToLowercaseToolStripMenuItem_Click);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.converterToolStripMenuItem,
            this.aSCIITableToolStripMenuItem,
            this.instructionsToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(46, 20);
            this.toolsToolStripMenuItem.Text = "Tools";
            // 
            // converterToolStripMenuItem
            // 
            this.converterToolStripMenuItem.Name = "converterToolStripMenuItem";
            this.converterToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.C)));
            this.converterToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.converterToolStripMenuItem.Text = "Converter";
            this.converterToolStripMenuItem.Click += new System.EventHandler(this.converterToolStripMenuItem_Click);
            // 
            // aSCIITableToolStripMenuItem
            // 
            this.aSCIITableToolStripMenuItem.Name = "aSCIITableToolStripMenuItem";
            this.aSCIITableToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.A)));
            this.aSCIITableToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.aSCIITableToolStripMenuItem.Text = "ASCII Table";
            this.aSCIITableToolStripMenuItem.Click += new System.EventHandler(this.aSCIITableToolStripMenuItem_Click);
            // 
            // instructionsToolStripMenuItem
            // 
            this.instructionsToolStripMenuItem.Name = "instructionsToolStripMenuItem";
            this.instructionsToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.I)));
            this.instructionsToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.instructionsToolStripMenuItem.Text = "Instructions";
            this.instructionsToolStripMenuItem.Click += new System.EventHandler(this.instructionsToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // GroupboxInput
            // 
            this.GroupboxInput.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupboxInput.Controls.Add(this.RichTextboxAssemblyCode);
            this.GroupboxInput.Location = new System.Drawing.Point(6, 6);
            this.GroupboxInput.Name = "GroupboxInput";
            this.GroupboxInput.Size = new System.Drawing.Size(396, 244);
            this.GroupboxInput.TabIndex = 1;
            this.GroupboxInput.TabStop = false;
            this.GroupboxInput.Text = "Assembly";
            // 
            // RichTextboxAssemblyCode
            // 
            this.RichTextboxAssemblyCode.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.RichTextboxAssemblyCode.Location = new System.Drawing.Point(6, 22);
            this.RichTextboxAssemblyCode.Name = "RichTextboxAssemblyCode";
            this.RichTextboxAssemblyCode.Size = new System.Drawing.Size(384, 214);
            this.RichTextboxAssemblyCode.TabIndex = 0;
            this.RichTextboxAssemblyCode.Text = "";
            // 
            // GroupboxControls
            // 
            this.GroupboxControls.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupboxControls.Controls.Add(this.GroupboxExecutionProgress);
            this.GroupboxControls.Controls.Add(this.LabelCurrentInstruction);
            this.GroupboxControls.Controls.Add(this.GroupboxExecution);
            this.GroupboxControls.Location = new System.Drawing.Point(408, 10);
            this.GroupboxControls.Name = "GroupboxControls";
            this.GroupboxControls.Size = new System.Drawing.Size(220, 244);
            this.GroupboxControls.TabIndex = 2;
            this.GroupboxControls.TabStop = false;
            this.GroupboxControls.Text = "Controls";
            // 
            // GroupboxExecutionProgress
            // 
            this.GroupboxExecutionProgress.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupboxExecutionProgress.Controls.Add(this.ProgressBarExecutionProgress);
            this.GroupboxExecutionProgress.Location = new System.Drawing.Point(10, 165);
            this.GroupboxExecutionProgress.Name = "GroupboxExecutionProgress";
            this.GroupboxExecutionProgress.Size = new System.Drawing.Size(206, 72);
            this.GroupboxExecutionProgress.TabIndex = 4;
            this.GroupboxExecutionProgress.TabStop = false;
            this.GroupboxExecutionProgress.Text = "Execution Progress";
            // 
            // ProgressBarExecutionProgress
            // 
            this.ProgressBarExecutionProgress.Location = new System.Drawing.Point(8, 22);
            this.ProgressBarExecutionProgress.Name = "ProgressBarExecutionProgress";
            this.ProgressBarExecutionProgress.Size = new System.Drawing.Size(192, 44);
            this.ProgressBarExecutionProgress.TabIndex = 0;
            // 
            // LabelCurrentInstruction
            // 
            this.LabelCurrentInstruction.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.LabelCurrentInstruction.AutoSize = true;
            this.LabelCurrentInstruction.Location = new System.Drawing.Point(8, 25);
            this.LabelCurrentInstruction.Name = "LabelCurrentInstruction";
            this.LabelCurrentInstruction.Size = new System.Drawing.Size(110, 15);
            this.LabelCurrentInstruction.TabIndex = 1;
            this.LabelCurrentInstruction.Text = "Current Instruction:";
            // 
            // GroupboxExecution
            // 
            this.GroupboxExecution.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupboxExecution.Controls.Add(this.ButtonExecute);
            this.GroupboxExecution.Controls.Add(this.ButtonPreviousInstruction);
            this.GroupboxExecution.Controls.Add(this.ButtonNextInstruction);
            this.GroupboxExecution.Location = new System.Drawing.Point(8, 49);
            this.GroupboxExecution.Name = "GroupboxExecution";
            this.GroupboxExecution.Size = new System.Drawing.Size(208, 110);
            this.GroupboxExecution.TabIndex = 0;
            this.GroupboxExecution.TabStop = false;
            this.GroupboxExecution.Text = "Execution";
            // 
            // ButtonExecute
            // 
            this.ButtonExecute.Location = new System.Drawing.Point(6, 80);
            this.ButtonExecute.Name = "ButtonExecute";
            this.ButtonExecute.Size = new System.Drawing.Size(196, 23);
            this.ButtonExecute.TabIndex = 3;
            this.ButtonExecute.Text = "Execute";
            this.ButtonExecute.UseVisualStyleBackColor = true;
            this.ButtonExecute.Click += new System.EventHandler(this.ButtonExecute_Click);
            // 
            // ButtonPreviousInstruction
            // 
            this.ButtonPreviousInstruction.Location = new System.Drawing.Point(6, 51);
            this.ButtonPreviousInstruction.Name = "ButtonPreviousInstruction";
            this.ButtonPreviousInstruction.Size = new System.Drawing.Size(196, 23);
            this.ButtonPreviousInstruction.TabIndex = 1;
            this.ButtonPreviousInstruction.Text = "Previous Instruction";
            this.ButtonPreviousInstruction.UseVisualStyleBackColor = true;
            this.ButtonPreviousInstruction.Click += new System.EventHandler(this.ButtonPreviousInstruction_Click);
            // 
            // ButtonNextInstruction
            // 
            this.ButtonNextInstruction.Location = new System.Drawing.Point(6, 22);
            this.ButtonNextInstruction.Name = "ButtonNextInstruction";
            this.ButtonNextInstruction.Size = new System.Drawing.Size(196, 23);
            this.ButtonNextInstruction.TabIndex = 0;
            this.ButtonNextInstruction.Text = "Next Instruction";
            this.ButtonNextInstruction.UseVisualStyleBackColor = true;
            this.ButtonNextInstruction.Click += new System.EventHandler(this.ButtonNextInstruction_Click);
            // 
            // EmuXTabControl
            // 
            this.EmuXTabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.EmuXTabControl.Controls.Add(this.Assembly);
            this.EmuXTabControl.Controls.Add(this.Memory);
            this.EmuXTabControl.Controls.Add(this.Registers);
            this.EmuXTabControl.Controls.Add(this.Output);
            this.EmuXTabControl.Location = new System.Drawing.Point(0, 27);
            this.EmuXTabControl.Name = "EmuXTabControl";
            this.EmuXTabControl.SelectedIndex = 0;
            this.EmuXTabControl.Size = new System.Drawing.Size(646, 285);
            this.EmuXTabControl.TabIndex = 1;
            this.EmuXTabControl.SelectedIndexChanged += new System.EventHandler(this.EmuXTabControl_SelectedIndexChanged);
            // 
            // Assembly
            // 
            this.Assembly.Controls.Add(this.GroupboxInput);
            this.Assembly.Controls.Add(this.GroupboxControls);
            this.Assembly.Location = new System.Drawing.Point(4, 24);
            this.Assembly.Name = "Assembly";
            this.Assembly.Padding = new System.Windows.Forms.Padding(3);
            this.Assembly.Size = new System.Drawing.Size(638, 257);
            this.Assembly.TabIndex = 0;
            this.Assembly.Text = "Assembly";
            this.Assembly.UseVisualStyleBackColor = true;
            // 
            // Memory
            // 
            this.Memory.Controls.Add(this.GroupBoxMemoryControls);
            this.Memory.Controls.Add(this.DataGridViewMemory);
            this.Memory.Location = new System.Drawing.Point(4, 24);
            this.Memory.Name = "Memory";
            this.Memory.Padding = new System.Windows.Forms.Padding(3);
            this.Memory.Size = new System.Drawing.Size(638, 257);
            this.Memory.TabIndex = 1;
            this.Memory.Text = "Memory";
            this.Memory.UseVisualStyleBackColor = true;
            // 
            // GroupBoxMemoryControls
            // 
            this.GroupBoxMemoryControls.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBoxMemoryControls.Controls.Add(this.ButtonClearMemoryTable);
            this.GroupBoxMemoryControls.Controls.Add(this.LabelValue);
            this.GroupBoxMemoryControls.Controls.Add(this.labelDataRepresentation);
            this.GroupBoxMemoryControls.Controls.Add(this.LabelMemoryRange);
            this.GroupBoxMemoryControls.Controls.Add(this.ButtonSetMemoryValue);
            this.GroupBoxMemoryControls.Controls.Add(this.ButtonSearchMemoryRange);
            this.GroupBoxMemoryControls.Controls.Add(this.TextBoxMemoryValue);
            this.GroupBoxMemoryControls.Controls.Add(this.TextBoxMemoryRangeEnd);
            this.GroupBoxMemoryControls.Controls.Add(this.TextBoxMemoryRangeStart);
            this.GroupBoxMemoryControls.Controls.Add(this.ComboBoxMemoryRepresentation);
            this.GroupBoxMemoryControls.Location = new System.Drawing.Point(8, 6);
            this.GroupBoxMemoryControls.Name = "GroupBoxMemoryControls";
            this.GroupBoxMemoryControls.Size = new System.Drawing.Size(742, 112);
            this.GroupBoxMemoryControls.TabIndex = 1;
            this.GroupBoxMemoryControls.TabStop = false;
            this.GroupBoxMemoryControls.Text = "Controls";
            // 
            // ButtonClearMemoryTable
            // 
            this.ButtonClearMemoryTable.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonClearMemoryTable.Location = new System.Drawing.Point(397, 50);
            this.ButtonClearMemoryTable.Name = "ButtonClearMemoryTable";
            this.ButtonClearMemoryTable.Size = new System.Drawing.Size(227, 23);
            this.ButtonClearMemoryTable.TabIndex = 9;
            this.ButtonClearMemoryTable.Text = "Clear";
            this.ButtonClearMemoryTable.UseVisualStyleBackColor = true;
            this.ButtonClearMemoryTable.Click += new System.EventHandler(this.ButtonClearMemoryTable_Click);
            // 
            // LabelValue
            // 
            this.LabelValue.AutoSize = true;
            this.LabelValue.Location = new System.Drawing.Point(6, 84);
            this.LabelValue.Name = "LabelValue";
            this.LabelValue.Size = new System.Drawing.Size(35, 15);
            this.LabelValue.TabIndex = 8;
            this.LabelValue.Text = "Value";
            // 
            // labelDataRepresentation
            // 
            this.labelDataRepresentation.AutoSize = true;
            this.labelDataRepresentation.Location = new System.Drawing.Point(6, 54);
            this.labelDataRepresentation.Name = "labelDataRepresentation";
            this.labelDataRepresentation.Size = new System.Drawing.Size(113, 15);
            this.labelDataRepresentation.TabIndex = 7;
            this.labelDataRepresentation.Text = "Data Representation";
            // 
            // LabelMemoryRange
            // 
            this.LabelMemoryRange.AutoSize = true;
            this.LabelMemoryRange.Location = new System.Drawing.Point(6, 25);
            this.LabelMemoryRange.Name = "LabelMemoryRange";
            this.LabelMemoryRange.Size = new System.Drawing.Size(88, 15);
            this.LabelMemoryRange.TabIndex = 6;
            this.LabelMemoryRange.Text = "Memory Range";
            // 
            // ButtonSetMemoryValue
            // 
            this.ButtonSetMemoryValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonSetMemoryValue.Location = new System.Drawing.Point(397, 80);
            this.ButtonSetMemoryValue.Name = "ButtonSetMemoryValue";
            this.ButtonSetMemoryValue.Size = new System.Drawing.Size(227, 23);
            this.ButtonSetMemoryValue.TabIndex = 5;
            this.ButtonSetMemoryValue.Text = "Set";
            this.ButtonSetMemoryValue.UseVisualStyleBackColor = true;
            this.ButtonSetMemoryValue.Click += new System.EventHandler(this.ButtonSetMemoryValue_Click);
            // 
            // ButtonSearchMemoryRange
            // 
            this.ButtonSearchMemoryRange.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonSearchMemoryRange.Location = new System.Drawing.Point(397, 22);
            this.ButtonSearchMemoryRange.Name = "ButtonSearchMemoryRange";
            this.ButtonSearchMemoryRange.Size = new System.Drawing.Size(227, 23);
            this.ButtonSearchMemoryRange.TabIndex = 4;
            this.ButtonSearchMemoryRange.Text = "Search";
            this.ButtonSearchMemoryRange.UseVisualStyleBackColor = true;
            this.ButtonSearchMemoryRange.Click += new System.EventHandler(this.ButtonSearchMemoryRange_Click);
            // 
            // TextBoxMemoryValue
            // 
            this.TextBoxMemoryValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBoxMemoryValue.Location = new System.Drawing.Point(189, 80);
            this.TextBoxMemoryValue.Name = "TextBoxMemoryValue";
            this.TextBoxMemoryValue.Size = new System.Drawing.Size(202, 23);
            this.TextBoxMemoryValue.TabIndex = 3;
            // 
            // TextBoxMemoryRangeEnd
            // 
            this.TextBoxMemoryRangeEnd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBoxMemoryRangeEnd.Location = new System.Drawing.Point(291, 22);
            this.TextBoxMemoryRangeEnd.Name = "TextBoxMemoryRangeEnd";
            this.TextBoxMemoryRangeEnd.Size = new System.Drawing.Size(100, 23);
            this.TextBoxMemoryRangeEnd.TabIndex = 2;
            // 
            // TextBoxMemoryRangeStart
            // 
            this.TextBoxMemoryRangeStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBoxMemoryRangeStart.Location = new System.Drawing.Point(189, 22);
            this.TextBoxMemoryRangeStart.Name = "TextBoxMemoryRangeStart";
            this.TextBoxMemoryRangeStart.Size = new System.Drawing.Size(100, 23);
            this.TextBoxMemoryRangeStart.TabIndex = 1;
            // 
            // ComboBoxMemoryRepresentation
            // 
            this.ComboBoxMemoryRepresentation.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ComboBoxMemoryRepresentation.FormattingEnabled = true;
            this.ComboBoxMemoryRepresentation.Items.AddRange(new object[] {
            "Decimal",
            "Binary",
            "Hexadecimal"});
            this.ComboBoxMemoryRepresentation.Location = new System.Drawing.Point(189, 51);
            this.ComboBoxMemoryRepresentation.Name = "ComboBoxMemoryRepresentation";
            this.ComboBoxMemoryRepresentation.Size = new System.Drawing.Size(202, 23);
            this.ComboBoxMemoryRepresentation.TabIndex = 0;
            // 
            // DataGridViewMemory
            // 
            this.DataGridViewMemory.AllowUserToAddRows = false;
            this.DataGridViewMemory.AllowUserToDeleteRows = false;
            this.DataGridViewMemory.AllowUserToResizeColumns = false;
            this.DataGridViewMemory.AllowUserToResizeRows = false;
            this.DataGridViewMemory.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DataGridViewMemory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataGridViewMemory.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.DataGridViewMemory.Enabled = false;
            this.DataGridViewMemory.Location = new System.Drawing.Point(8, 124);
            this.DataGridViewMemory.Name = "DataGridViewMemory";
            this.DataGridViewMemory.ReadOnly = true;
            this.DataGridViewMemory.RowTemplate.Height = 25;
            this.DataGridViewMemory.Size = new System.Drawing.Size(624, 124);
            this.DataGridViewMemory.TabIndex = 0;
            // 
            // Registers
            // 
            this.Registers.Controls.Add(this.GroupBoxEFLAGS);
            this.Registers.Controls.Add(this.ButtonSetRegisterValues);
            this.Registers.Controls.Add(this.GroupBoxGeneralPurposeRegisters);
            this.Registers.Location = new System.Drawing.Point(4, 24);
            this.Registers.Name = "Registers";
            this.Registers.Size = new System.Drawing.Size(638, 257);
            this.Registers.TabIndex = 2;
            this.Registers.Text = "Registers";
            this.Registers.UseVisualStyleBackColor = true;
            // 
            // GroupBoxEFLAGS
            // 
            this.GroupBoxEFLAGS.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBoxEFLAGS.Controls.Add(this.CheckBoxID);
            this.GroupBoxEFLAGS.Controls.Add(this.CheckBoxVIP);
            this.GroupBoxEFLAGS.Controls.Add(this.CheckBoxVIF);
            this.GroupBoxEFLAGS.Controls.Add(this.LabelID);
            this.GroupBoxEFLAGS.Controls.Add(this.LabelVIP);
            this.GroupBoxEFLAGS.Controls.Add(this.LabelVIF);
            this.GroupBoxEFLAGS.Controls.Add(this.CheckBoxAC);
            this.GroupBoxEFLAGS.Controls.Add(this.LabelAC);
            this.GroupBoxEFLAGS.Controls.Add(this.CheckBoxVM);
            this.GroupBoxEFLAGS.Controls.Add(this.LabelVM);
            this.GroupBoxEFLAGS.Controls.Add(this.CheckBoxRF);
            this.GroupBoxEFLAGS.Controls.Add(this.LabelRF);
            this.GroupBoxEFLAGS.Controls.Add(this.CheckBoxNT);
            this.GroupBoxEFLAGS.Controls.Add(this.LabelNT);
            this.GroupBoxEFLAGS.Controls.Add(this.CheckBoxIOPL);
            this.GroupBoxEFLAGS.Controls.Add(this.LabelIOPL);
            this.GroupBoxEFLAGS.Controls.Add(this.CheckBoxOF);
            this.GroupBoxEFLAGS.Controls.Add(this.LabelOF);
            this.GroupBoxEFLAGS.Controls.Add(this.CheckBoxDF);
            this.GroupBoxEFLAGS.Controls.Add(this.LabelDF);
            this.GroupBoxEFLAGS.Controls.Add(this.CheckBoxIF);
            this.GroupBoxEFLAGS.Controls.Add(this.LabelIF);
            this.GroupBoxEFLAGS.Controls.Add(this.CheckBoxTF);
            this.GroupBoxEFLAGS.Controls.Add(this.LabelTF);
            this.GroupBoxEFLAGS.Controls.Add(this.CheckBoxSF);
            this.GroupBoxEFLAGS.Controls.Add(this.LabelSF);
            this.GroupBoxEFLAGS.Controls.Add(this.CheckBoxZF);
            this.GroupBoxEFLAGS.Controls.Add(this.LabelZF);
            this.GroupBoxEFLAGS.Controls.Add(this.CheckBoxAF);
            this.GroupBoxEFLAGS.Controls.Add(this.LabelAF);
            this.GroupBoxEFLAGS.Controls.Add(this.CheckBoxPF);
            this.GroupBoxEFLAGS.Controls.Add(this.LabelPF);
            this.GroupBoxEFLAGS.Controls.Add(this.CheckBoxCF);
            this.GroupBoxEFLAGS.Controls.Add(this.LabelCF);
            this.GroupBoxEFLAGS.Location = new System.Drawing.Point(479, 3);
            this.GroupBoxEFLAGS.Name = "GroupBoxEFLAGS";
            this.GroupBoxEFLAGS.Size = new System.Drawing.Size(146, 193);
            this.GroupBoxEFLAGS.TabIndex = 20;
            this.GroupBoxEFLAGS.TabStop = false;
            this.GroupBoxEFLAGS.Text = "EFLAGS";
            // 
            // CheckBoxID
            // 
            this.CheckBoxID.AutoSize = true;
            this.CheckBoxID.Location = new System.Drawing.Point(126, 92);
            this.CheckBoxID.Name = "CheckBoxID";
            this.CheckBoxID.Size = new System.Drawing.Size(15, 14);
            this.CheckBoxID.TabIndex = 33;
            this.CheckBoxID.UseVisualStyleBackColor = true;
            // 
            // CheckBoxVIP
            // 
            this.CheckBoxVIP.AutoSize = true;
            this.CheckBoxVIP.Location = new System.Drawing.Point(126, 77);
            this.CheckBoxVIP.Name = "CheckBoxVIP";
            this.CheckBoxVIP.Size = new System.Drawing.Size(15, 14);
            this.CheckBoxVIP.TabIndex = 32;
            this.CheckBoxVIP.UseVisualStyleBackColor = true;
            // 
            // CheckBoxVIF
            // 
            this.CheckBoxVIF.AutoSize = true;
            this.CheckBoxVIF.Location = new System.Drawing.Point(126, 62);
            this.CheckBoxVIF.Name = "CheckBoxVIF";
            this.CheckBoxVIF.Size = new System.Drawing.Size(15, 14);
            this.CheckBoxVIF.TabIndex = 31;
            this.CheckBoxVIF.UseVisualStyleBackColor = true;
            // 
            // LabelID
            // 
            this.LabelID.AutoSize = true;
            this.LabelID.Location = new System.Drawing.Point(84, 94);
            this.LabelID.Name = "LabelID";
            this.LabelID.Size = new System.Drawing.Size(18, 15);
            this.LabelID.TabIndex = 30;
            this.LabelID.Text = "ID";
            // 
            // LabelVIP
            // 
            this.LabelVIP.AutoSize = true;
            this.LabelVIP.Location = new System.Drawing.Point(84, 79);
            this.LabelVIP.Name = "LabelVIP";
            this.LabelVIP.Size = new System.Drawing.Size(24, 15);
            this.LabelVIP.TabIndex = 29;
            this.LabelVIP.Text = "VIP";
            // 
            // LabelVIF
            // 
            this.LabelVIF.AutoSize = true;
            this.LabelVIF.Location = new System.Drawing.Point(84, 64);
            this.LabelVIF.Name = "LabelVIF";
            this.LabelVIF.Size = new System.Drawing.Size(23, 15);
            this.LabelVIF.TabIndex = 28;
            this.LabelVIF.Text = "VIF";
            // 
            // CheckBoxAC
            // 
            this.CheckBoxAC.AutoSize = true;
            this.CheckBoxAC.Location = new System.Drawing.Point(126, 47);
            this.CheckBoxAC.Name = "CheckBoxAC";
            this.CheckBoxAC.Size = new System.Drawing.Size(15, 14);
            this.CheckBoxAC.TabIndex = 27;
            this.CheckBoxAC.UseVisualStyleBackColor = true;
            // 
            // LabelAC
            // 
            this.LabelAC.AutoSize = true;
            this.LabelAC.Location = new System.Drawing.Point(84, 49);
            this.LabelAC.Name = "LabelAC";
            this.LabelAC.Size = new System.Drawing.Size(23, 15);
            this.LabelAC.TabIndex = 26;
            this.LabelAC.Text = "AC";
            // 
            // CheckBoxVM
            // 
            this.CheckBoxVM.AutoSize = true;
            this.CheckBoxVM.Location = new System.Drawing.Point(126, 32);
            this.CheckBoxVM.Name = "CheckBoxVM";
            this.CheckBoxVM.Size = new System.Drawing.Size(15, 14);
            this.CheckBoxVM.TabIndex = 25;
            this.CheckBoxVM.UseVisualStyleBackColor = true;
            // 
            // LabelVM
            // 
            this.LabelVM.AutoSize = true;
            this.LabelVM.Location = new System.Drawing.Point(84, 33);
            this.LabelVM.Name = "LabelVM";
            this.LabelVM.Size = new System.Drawing.Size(25, 15);
            this.LabelVM.TabIndex = 24;
            this.LabelVM.Text = "VM";
            // 
            // CheckBoxRF
            // 
            this.CheckBoxRF.AutoSize = true;
            this.CheckBoxRF.Location = new System.Drawing.Point(126, 17);
            this.CheckBoxRF.Name = "CheckBoxRF";
            this.CheckBoxRF.Size = new System.Drawing.Size(15, 14);
            this.CheckBoxRF.TabIndex = 23;
            this.CheckBoxRF.UseVisualStyleBackColor = true;
            // 
            // LabelRF
            // 
            this.LabelRF.AutoSize = true;
            this.LabelRF.Location = new System.Drawing.Point(84, 18);
            this.LabelRF.Name = "LabelRF";
            this.LabelRF.Size = new System.Drawing.Size(20, 15);
            this.LabelRF.TabIndex = 22;
            this.LabelRF.Text = "RF";
            // 
            // CheckBoxNT
            // 
            this.CheckBoxNT.AutoSize = true;
            this.CheckBoxNT.Location = new System.Drawing.Point(53, 169);
            this.CheckBoxNT.Name = "CheckBoxNT";
            this.CheckBoxNT.Size = new System.Drawing.Size(15, 14);
            this.CheckBoxNT.TabIndex = 21;
            this.CheckBoxNT.UseVisualStyleBackColor = true;
            // 
            // LabelNT
            // 
            this.LabelNT.AutoSize = true;
            this.LabelNT.Location = new System.Drawing.Point(7, 169);
            this.LabelNT.Name = "LabelNT";
            this.LabelNT.Size = new System.Drawing.Size(22, 15);
            this.LabelNT.TabIndex = 20;
            this.LabelNT.Text = "NT";
            // 
            // CheckBoxIOPL
            // 
            this.CheckBoxIOPL.AutoSize = true;
            this.CheckBoxIOPL.Location = new System.Drawing.Point(53, 154);
            this.CheckBoxIOPL.Name = "CheckBoxIOPL";
            this.CheckBoxIOPL.Size = new System.Drawing.Size(15, 14);
            this.CheckBoxIOPL.TabIndex = 19;
            this.CheckBoxIOPL.UseVisualStyleBackColor = true;
            // 
            // LabelIOPL
            // 
            this.LabelIOPL.AutoSize = true;
            this.LabelIOPL.Location = new System.Drawing.Point(7, 154);
            this.LabelIOPL.Name = "LabelIOPL";
            this.LabelIOPL.Size = new System.Drawing.Size(32, 15);
            this.LabelIOPL.TabIndex = 18;
            this.LabelIOPL.Text = "IOPL";
            // 
            // CheckBoxOF
            // 
            this.CheckBoxOF.AutoSize = true;
            this.CheckBoxOF.Location = new System.Drawing.Point(53, 139);
            this.CheckBoxOF.Name = "CheckBoxOF";
            this.CheckBoxOF.Size = new System.Drawing.Size(15, 14);
            this.CheckBoxOF.TabIndex = 17;
            this.CheckBoxOF.UseVisualStyleBackColor = true;
            // 
            // LabelOF
            // 
            this.LabelOF.AutoSize = true;
            this.LabelOF.Location = new System.Drawing.Point(7, 139);
            this.LabelOF.Name = "LabelOF";
            this.LabelOF.Size = new System.Drawing.Size(22, 15);
            this.LabelOF.TabIndex = 16;
            this.LabelOF.Text = "OF";
            // 
            // CheckBoxDF
            // 
            this.CheckBoxDF.AutoSize = true;
            this.CheckBoxDF.Location = new System.Drawing.Point(53, 124);
            this.CheckBoxDF.Name = "CheckBoxDF";
            this.CheckBoxDF.Size = new System.Drawing.Size(15, 14);
            this.CheckBoxDF.TabIndex = 15;
            this.CheckBoxDF.UseVisualStyleBackColor = true;
            // 
            // LabelDF
            // 
            this.LabelDF.AutoSize = true;
            this.LabelDF.Location = new System.Drawing.Point(7, 124);
            this.LabelDF.Name = "LabelDF";
            this.LabelDF.Size = new System.Drawing.Size(21, 15);
            this.LabelDF.TabIndex = 14;
            this.LabelDF.Text = "DF";
            // 
            // CheckBoxIF
            // 
            this.CheckBoxIF.AutoSize = true;
            this.CheckBoxIF.Location = new System.Drawing.Point(53, 109);
            this.CheckBoxIF.Name = "CheckBoxIF";
            this.CheckBoxIF.Size = new System.Drawing.Size(15, 14);
            this.CheckBoxIF.TabIndex = 13;
            this.CheckBoxIF.UseVisualStyleBackColor = true;
            // 
            // LabelIF
            // 
            this.LabelIF.AutoSize = true;
            this.LabelIF.Location = new System.Drawing.Point(7, 109);
            this.LabelIF.Name = "LabelIF";
            this.LabelIF.Size = new System.Drawing.Size(16, 15);
            this.LabelIF.TabIndex = 12;
            this.LabelIF.Text = "IF";
            // 
            // CheckBoxTF
            // 
            this.CheckBoxTF.AutoSize = true;
            this.CheckBoxTF.Location = new System.Drawing.Point(53, 94);
            this.CheckBoxTF.Name = "CheckBoxTF";
            this.CheckBoxTF.Size = new System.Drawing.Size(15, 14);
            this.CheckBoxTF.TabIndex = 11;
            this.CheckBoxTF.UseVisualStyleBackColor = true;
            // 
            // LabelTF
            // 
            this.LabelTF.AutoSize = true;
            this.LabelTF.Location = new System.Drawing.Point(6, 94);
            this.LabelTF.Name = "LabelTF";
            this.LabelTF.Size = new System.Drawing.Size(19, 15);
            this.LabelTF.TabIndex = 10;
            this.LabelTF.Text = "TF";
            // 
            // CheckBoxSF
            // 
            this.CheckBoxSF.AutoSize = true;
            this.CheckBoxSF.Location = new System.Drawing.Point(53, 79);
            this.CheckBoxSF.Name = "CheckBoxSF";
            this.CheckBoxSF.Size = new System.Drawing.Size(15, 14);
            this.CheckBoxSF.TabIndex = 9;
            this.CheckBoxSF.UseVisualStyleBackColor = true;
            // 
            // LabelSF
            // 
            this.LabelSF.AutoSize = true;
            this.LabelSF.Location = new System.Drawing.Point(6, 79);
            this.LabelSF.Name = "LabelSF";
            this.LabelSF.Size = new System.Drawing.Size(19, 15);
            this.LabelSF.TabIndex = 8;
            this.LabelSF.Text = "SF";
            // 
            // CheckBoxZF
            // 
            this.CheckBoxZF.AutoSize = true;
            this.CheckBoxZF.Location = new System.Drawing.Point(53, 64);
            this.CheckBoxZF.Name = "CheckBoxZF";
            this.CheckBoxZF.Size = new System.Drawing.Size(15, 14);
            this.CheckBoxZF.TabIndex = 7;
            this.CheckBoxZF.UseVisualStyleBackColor = true;
            // 
            // LabelZF
            // 
            this.LabelZF.AutoSize = true;
            this.LabelZF.Location = new System.Drawing.Point(6, 64);
            this.LabelZF.Name = "LabelZF";
            this.LabelZF.Size = new System.Drawing.Size(20, 15);
            this.LabelZF.TabIndex = 6;
            this.LabelZF.Text = "ZF";
            // 
            // CheckBoxAF
            // 
            this.CheckBoxAF.AutoSize = true;
            this.CheckBoxAF.Location = new System.Drawing.Point(53, 49);
            this.CheckBoxAF.Name = "CheckBoxAF";
            this.CheckBoxAF.Size = new System.Drawing.Size(15, 14);
            this.CheckBoxAF.TabIndex = 5;
            this.CheckBoxAF.UseVisualStyleBackColor = true;
            // 
            // LabelAF
            // 
            this.LabelAF.AutoSize = true;
            this.LabelAF.Location = new System.Drawing.Point(6, 49);
            this.LabelAF.Name = "LabelAF";
            this.LabelAF.Size = new System.Drawing.Size(21, 15);
            this.LabelAF.TabIndex = 4;
            this.LabelAF.Text = "AF";
            // 
            // CheckBoxPF
            // 
            this.CheckBoxPF.AutoSize = true;
            this.CheckBoxPF.Location = new System.Drawing.Point(53, 34);
            this.CheckBoxPF.Name = "CheckBoxPF";
            this.CheckBoxPF.Size = new System.Drawing.Size(15, 14);
            this.CheckBoxPF.TabIndex = 3;
            this.CheckBoxPF.UseVisualStyleBackColor = true;
            // 
            // LabelPF
            // 
            this.LabelPF.AutoSize = true;
            this.LabelPF.Location = new System.Drawing.Point(7, 33);
            this.LabelPF.Name = "LabelPF";
            this.LabelPF.Size = new System.Drawing.Size(20, 15);
            this.LabelPF.TabIndex = 2;
            this.LabelPF.Text = "PF";
            // 
            // CheckBoxCF
            // 
            this.CheckBoxCF.AutoSize = true;
            this.CheckBoxCF.Location = new System.Drawing.Point(53, 19);
            this.CheckBoxCF.Name = "CheckBoxCF";
            this.CheckBoxCF.Size = new System.Drawing.Size(15, 14);
            this.CheckBoxCF.TabIndex = 1;
            this.CheckBoxCF.UseVisualStyleBackColor = true;
            // 
            // LabelCF
            // 
            this.LabelCF.AutoSize = true;
            this.LabelCF.Location = new System.Drawing.Point(7, 19);
            this.LabelCF.Name = "LabelCF";
            this.LabelCF.Size = new System.Drawing.Size(21, 15);
            this.LabelCF.TabIndex = 0;
            this.LabelCF.Text = "CF";
            // 
            // ButtonSetRegisterValues
            // 
            this.ButtonSetRegisterValues.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonSetRegisterValues.Location = new System.Drawing.Point(8, 202);
            this.ButtonSetRegisterValues.Name = "ButtonSetRegisterValues";
            this.ButtonSetRegisterValues.Size = new System.Drawing.Size(617, 46);
            this.ButtonSetRegisterValues.TabIndex = 19;
            this.ButtonSetRegisterValues.Text = "Set Register Values";
            this.ButtonSetRegisterValues.UseVisualStyleBackColor = true;
            this.ButtonSetRegisterValues.Click += new System.EventHandler(this.ButtonSetRegisterValues_Click);
            // 
            // GroupBoxGeneralPurposeRegisters
            // 
            this.GroupBoxGeneralPurposeRegisters.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBoxGeneralPurposeRegisters.Controls.Add(this.ButtonResetVirtualSystem);
            this.GroupBoxGeneralPurposeRegisters.Controls.Add(this.TextBoxR15);
            this.GroupBoxGeneralPurposeRegisters.Controls.Add(this.LabelR15);
            this.GroupBoxGeneralPurposeRegisters.Controls.Add(this.TextBoxR14);
            this.GroupBoxGeneralPurposeRegisters.Controls.Add(this.LabelR14);
            this.GroupBoxGeneralPurposeRegisters.Controls.Add(this.TextBoxR13);
            this.GroupBoxGeneralPurposeRegisters.Controls.Add(this.LabelR13);
            this.GroupBoxGeneralPurposeRegisters.Controls.Add(this.TextBoxR12);
            this.GroupBoxGeneralPurposeRegisters.Controls.Add(this.LabelR12);
            this.GroupBoxGeneralPurposeRegisters.Controls.Add(this.TextBoxR11);
            this.GroupBoxGeneralPurposeRegisters.Controls.Add(this.LabelR11);
            this.GroupBoxGeneralPurposeRegisters.Controls.Add(this.TextBoxR10);
            this.GroupBoxGeneralPurposeRegisters.Controls.Add(this.LabelR10);
            this.GroupBoxGeneralPurposeRegisters.Controls.Add(this.TextBoxR9);
            this.GroupBoxGeneralPurposeRegisters.Controls.Add(this.LabelR9);
            this.GroupBoxGeneralPurposeRegisters.Controls.Add(this.TextBoxR8);
            this.GroupBoxGeneralPurposeRegisters.Controls.Add(this.LabelR8);
            this.GroupBoxGeneralPurposeRegisters.Controls.Add(this.LabelRAX);
            this.GroupBoxGeneralPurposeRegisters.Controls.Add(this.TextBoxRIP);
            this.GroupBoxGeneralPurposeRegisters.Controls.Add(this.TextBoxRAX);
            this.GroupBoxGeneralPurposeRegisters.Controls.Add(this.LabelRIP);
            this.GroupBoxGeneralPurposeRegisters.Controls.Add(this.LabelRBX);
            this.GroupBoxGeneralPurposeRegisters.Controls.Add(this.TextBoxRBP);
            this.GroupBoxGeneralPurposeRegisters.Controls.Add(this.TextBoxRBX);
            this.GroupBoxGeneralPurposeRegisters.Controls.Add(this.LabelRBP);
            this.GroupBoxGeneralPurposeRegisters.Controls.Add(this.LabelRCX);
            this.GroupBoxGeneralPurposeRegisters.Controls.Add(this.TextBoxRSP);
            this.GroupBoxGeneralPurposeRegisters.Controls.Add(this.TextBoxRCX);
            this.GroupBoxGeneralPurposeRegisters.Controls.Add(this.LabelRSP);
            this.GroupBoxGeneralPurposeRegisters.Controls.Add(this.LabelRDX);
            this.GroupBoxGeneralPurposeRegisters.Controls.Add(this.TextBoxRDI);
            this.GroupBoxGeneralPurposeRegisters.Controls.Add(this.TextBoxRDX);
            this.GroupBoxGeneralPurposeRegisters.Controls.Add(this.LabelRDI);
            this.GroupBoxGeneralPurposeRegisters.Controls.Add(this.LabelRSI);
            this.GroupBoxGeneralPurposeRegisters.Controls.Add(this.TextBoxRSI);
            this.GroupBoxGeneralPurposeRegisters.Location = new System.Drawing.Point(8, 3);
            this.GroupBoxGeneralPurposeRegisters.Name = "GroupBoxGeneralPurposeRegisters";
            this.GroupBoxGeneralPurposeRegisters.Size = new System.Drawing.Size(465, 193);
            this.GroupBoxGeneralPurposeRegisters.TabIndex = 18;
            this.GroupBoxGeneralPurposeRegisters.TabStop = false;
            this.GroupBoxGeneralPurposeRegisters.Text = "General Purpose Register";
            // 
            // ButtonResetVirtualSystem
            // 
            this.ButtonResetVirtualSystem.Location = new System.Drawing.Point(313, 160);
            this.ButtonResetVirtualSystem.Name = "ButtonResetVirtualSystem";
            this.ButtonResetVirtualSystem.Size = new System.Drawing.Size(144, 24);
            this.ButtonResetVirtualSystem.TabIndex = 34;
            this.ButtonResetVirtualSystem.Text = "Reset Virtual System";
            this.ButtonResetVirtualSystem.UseVisualStyleBackColor = true;
            this.ButtonResetVirtualSystem.Click += new System.EventHandler(this.ButtonResetVirtualSystem_Click);
            // 
            // TextBoxR15
            // 
            this.TextBoxR15.Location = new System.Drawing.Point(357, 132);
            this.TextBoxR15.Name = "TextBoxR15";
            this.TextBoxR15.Size = new System.Drawing.Size(100, 23);
            this.TextBoxR15.TabIndex = 33;
            // 
            // LabelR15
            // 
            this.LabelR15.AutoSize = true;
            this.LabelR15.Location = new System.Drawing.Point(313, 135);
            this.LabelR15.Name = "LabelR15";
            this.LabelR15.Size = new System.Drawing.Size(26, 15);
            this.LabelR15.TabIndex = 32;
            this.LabelR15.Text = "R15";
            // 
            // TextBoxR14
            // 
            this.TextBoxR14.Location = new System.Drawing.Point(357, 103);
            this.TextBoxR14.Name = "TextBoxR14";
            this.TextBoxR14.Size = new System.Drawing.Size(100, 23);
            this.TextBoxR14.TabIndex = 31;
            // 
            // LabelR14
            // 
            this.LabelR14.AutoSize = true;
            this.LabelR14.Location = new System.Drawing.Point(313, 106);
            this.LabelR14.Name = "LabelR14";
            this.LabelR14.Size = new System.Drawing.Size(26, 15);
            this.LabelR14.TabIndex = 30;
            this.LabelR14.Text = "R14";
            // 
            // TextBoxR13
            // 
            this.TextBoxR13.Location = new System.Drawing.Point(357, 74);
            this.TextBoxR13.Name = "TextBoxR13";
            this.TextBoxR13.Size = new System.Drawing.Size(100, 23);
            this.TextBoxR13.TabIndex = 29;
            // 
            // LabelR13
            // 
            this.LabelR13.AutoSize = true;
            this.LabelR13.Location = new System.Drawing.Point(313, 77);
            this.LabelR13.Name = "LabelR13";
            this.LabelR13.Size = new System.Drawing.Size(26, 15);
            this.LabelR13.TabIndex = 28;
            this.LabelR13.Text = "R13";
            // 
            // TextBoxR12
            // 
            this.TextBoxR12.Location = new System.Drawing.Point(357, 45);
            this.TextBoxR12.Name = "TextBoxR12";
            this.TextBoxR12.Size = new System.Drawing.Size(100, 23);
            this.TextBoxR12.TabIndex = 27;
            // 
            // LabelR12
            // 
            this.LabelR12.AutoSize = true;
            this.LabelR12.Location = new System.Drawing.Point(313, 48);
            this.LabelR12.Name = "LabelR12";
            this.LabelR12.Size = new System.Drawing.Size(26, 15);
            this.LabelR12.TabIndex = 26;
            this.LabelR12.Text = "R12";
            // 
            // TextBoxR11
            // 
            this.TextBoxR11.Location = new System.Drawing.Point(357, 16);
            this.TextBoxR11.Name = "TextBoxR11";
            this.TextBoxR11.Size = new System.Drawing.Size(100, 23);
            this.TextBoxR11.TabIndex = 25;
            // 
            // LabelR11
            // 
            this.LabelR11.AutoSize = true;
            this.LabelR11.Location = new System.Drawing.Point(313, 19);
            this.LabelR11.Name = "LabelR11";
            this.LabelR11.Size = new System.Drawing.Size(26, 15);
            this.LabelR11.TabIndex = 24;
            this.LabelR11.Text = "R11";
            // 
            // TextBoxR10
            // 
            this.TextBoxR10.Location = new System.Drawing.Point(204, 161);
            this.TextBoxR10.Name = "TextBoxR10";
            this.TextBoxR10.Size = new System.Drawing.Size(100, 23);
            this.TextBoxR10.TabIndex = 23;
            // 
            // LabelR10
            // 
            this.LabelR10.AutoSize = true;
            this.LabelR10.Location = new System.Drawing.Point(160, 164);
            this.LabelR10.Name = "LabelR10";
            this.LabelR10.Size = new System.Drawing.Size(26, 15);
            this.LabelR10.TabIndex = 22;
            this.LabelR10.Text = "R10";
            // 
            // TextBoxR9
            // 
            this.TextBoxR9.Location = new System.Drawing.Point(204, 132);
            this.TextBoxR9.Name = "TextBoxR9";
            this.TextBoxR9.Size = new System.Drawing.Size(100, 23);
            this.TextBoxR9.TabIndex = 21;
            // 
            // LabelR9
            // 
            this.LabelR9.AutoSize = true;
            this.LabelR9.Location = new System.Drawing.Point(160, 135);
            this.LabelR9.Name = "LabelR9";
            this.LabelR9.Size = new System.Drawing.Size(20, 15);
            this.LabelR9.TabIndex = 20;
            this.LabelR9.Text = "R9";
            // 
            // TextBoxR8
            // 
            this.TextBoxR8.Location = new System.Drawing.Point(204, 103);
            this.TextBoxR8.Name = "TextBoxR8";
            this.TextBoxR8.Size = new System.Drawing.Size(100, 23);
            this.TextBoxR8.TabIndex = 19;
            // 
            // LabelR8
            // 
            this.LabelR8.AutoSize = true;
            this.LabelR8.Location = new System.Drawing.Point(160, 106);
            this.LabelR8.Name = "LabelR8";
            this.LabelR8.Size = new System.Drawing.Size(20, 15);
            this.LabelR8.TabIndex = 18;
            this.LabelR8.Text = "R8";
            // 
            // LabelRAX
            // 
            this.LabelRAX.AutoSize = true;
            this.LabelRAX.Location = new System.Drawing.Point(6, 19);
            this.LabelRAX.Name = "LabelRAX";
            this.LabelRAX.Size = new System.Drawing.Size(29, 15);
            this.LabelRAX.TabIndex = 0;
            this.LabelRAX.Text = "RAX";
            // 
            // TextBoxRIP
            // 
            this.TextBoxRIP.Location = new System.Drawing.Point(204, 74);
            this.TextBoxRIP.Name = "TextBoxRIP";
            this.TextBoxRIP.Size = new System.Drawing.Size(100, 23);
            this.TextBoxRIP.TabIndex = 17;
            // 
            // TextBoxRAX
            // 
            this.TextBoxRAX.Location = new System.Drawing.Point(50, 16);
            this.TextBoxRAX.Name = "TextBoxRAX";
            this.TextBoxRAX.Size = new System.Drawing.Size(100, 23);
            this.TextBoxRAX.TabIndex = 1;
            // 
            // LabelRIP
            // 
            this.LabelRIP.AutoSize = true;
            this.LabelRIP.Location = new System.Drawing.Point(160, 77);
            this.LabelRIP.Name = "LabelRIP";
            this.LabelRIP.Size = new System.Drawing.Size(24, 15);
            this.LabelRIP.TabIndex = 16;
            this.LabelRIP.Text = "RIP";
            // 
            // LabelRBX
            // 
            this.LabelRBX.AutoSize = true;
            this.LabelRBX.Location = new System.Drawing.Point(6, 48);
            this.LabelRBX.Name = "LabelRBX";
            this.LabelRBX.Size = new System.Drawing.Size(28, 15);
            this.LabelRBX.TabIndex = 2;
            this.LabelRBX.Text = "RBX";
            // 
            // TextBoxRBP
            // 
            this.TextBoxRBP.Location = new System.Drawing.Point(204, 45);
            this.TextBoxRBP.Name = "TextBoxRBP";
            this.TextBoxRBP.Size = new System.Drawing.Size(100, 23);
            this.TextBoxRBP.TabIndex = 15;
            // 
            // TextBoxRBX
            // 
            this.TextBoxRBX.Location = new System.Drawing.Point(50, 45);
            this.TextBoxRBX.Name = "TextBoxRBX";
            this.TextBoxRBX.Size = new System.Drawing.Size(100, 23);
            this.TextBoxRBX.TabIndex = 3;
            // 
            // LabelRBP
            // 
            this.LabelRBP.AutoSize = true;
            this.LabelRBP.Location = new System.Drawing.Point(160, 48);
            this.LabelRBP.Name = "LabelRBP";
            this.LabelRBP.Size = new System.Drawing.Size(28, 15);
            this.LabelRBP.TabIndex = 14;
            this.LabelRBP.Text = "RBP";
            // 
            // LabelRCX
            // 
            this.LabelRCX.AutoSize = true;
            this.LabelRCX.Location = new System.Drawing.Point(6, 77);
            this.LabelRCX.Name = "LabelRCX";
            this.LabelRCX.Size = new System.Drawing.Size(29, 15);
            this.LabelRCX.TabIndex = 4;
            this.LabelRCX.Text = "RCX";
            // 
            // TextBoxRSP
            // 
            this.TextBoxRSP.Location = new System.Drawing.Point(204, 16);
            this.TextBoxRSP.Name = "TextBoxRSP";
            this.TextBoxRSP.Size = new System.Drawing.Size(100, 23);
            this.TextBoxRSP.TabIndex = 13;
            // 
            // TextBoxRCX
            // 
            this.TextBoxRCX.Location = new System.Drawing.Point(50, 74);
            this.TextBoxRCX.Name = "TextBoxRCX";
            this.TextBoxRCX.Size = new System.Drawing.Size(100, 23);
            this.TextBoxRCX.TabIndex = 5;
            // 
            // LabelRSP
            // 
            this.LabelRSP.AutoSize = true;
            this.LabelRSP.Location = new System.Drawing.Point(160, 19);
            this.LabelRSP.Name = "LabelRSP";
            this.LabelRSP.Size = new System.Drawing.Size(27, 15);
            this.LabelRSP.TabIndex = 12;
            this.LabelRSP.Text = "RSP";
            // 
            // LabelRDX
            // 
            this.LabelRDX.AutoSize = true;
            this.LabelRDX.Location = new System.Drawing.Point(6, 106);
            this.LabelRDX.Name = "LabelRDX";
            this.LabelRDX.Size = new System.Drawing.Size(29, 15);
            this.LabelRDX.TabIndex = 6;
            this.LabelRDX.Text = "RDX";
            // 
            // TextBoxRDI
            // 
            this.TextBoxRDI.Location = new System.Drawing.Point(50, 161);
            this.TextBoxRDI.Name = "TextBoxRDI";
            this.TextBoxRDI.Size = new System.Drawing.Size(100, 23);
            this.TextBoxRDI.TabIndex = 11;
            // 
            // TextBoxRDX
            // 
            this.TextBoxRDX.Location = new System.Drawing.Point(50, 103);
            this.TextBoxRDX.Name = "TextBoxRDX";
            this.TextBoxRDX.Size = new System.Drawing.Size(100, 23);
            this.TextBoxRDX.TabIndex = 7;
            // 
            // LabelRDI
            // 
            this.LabelRDI.AutoSize = true;
            this.LabelRDI.Location = new System.Drawing.Point(6, 164);
            this.LabelRDI.Name = "LabelRDI";
            this.LabelRDI.Size = new System.Drawing.Size(25, 15);
            this.LabelRDI.TabIndex = 10;
            this.LabelRDI.Text = "RDI";
            // 
            // LabelRSI
            // 
            this.LabelRSI.AutoSize = true;
            this.LabelRSI.Location = new System.Drawing.Point(6, 135);
            this.LabelRSI.Name = "LabelRSI";
            this.LabelRSI.Size = new System.Drawing.Size(23, 15);
            this.LabelRSI.TabIndex = 8;
            this.LabelRSI.Text = "RSI";
            // 
            // TextBoxRSI
            // 
            this.TextBoxRSI.Location = new System.Drawing.Point(50, 132);
            this.TextBoxRSI.Name = "TextBoxRSI";
            this.TextBoxRSI.Size = new System.Drawing.Size(100, 23);
            this.TextBoxRSI.TabIndex = 9;
            // 
            // Output
            // 
            this.Output.Controls.Add(this.RichtextBoxOutput);
            this.Output.Location = new System.Drawing.Point(4, 24);
            this.Output.Name = "Output";
            this.Output.Size = new System.Drawing.Size(638, 257);
            this.Output.TabIndex = 3;
            this.Output.Text = "Output";
            this.Output.UseVisualStyleBackColor = true;
            // 
            // RichtextBoxOutput
            // 
            this.RichtextBoxOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.RichtextBoxOutput.Enabled = false;
            this.RichtextBoxOutput.Location = new System.Drawing.Point(8, 3);
            this.RichtextBoxOutput.Name = "RichtextBoxOutput";
            this.RichtextBoxOutput.Size = new System.Drawing.Size(627, 245);
            this.RichtextBoxOutput.TabIndex = 0;
            this.RichtextBoxOutput.Text = "";
            // 
            // ButtonExecuteOnAnotherTab
            // 
            this.ButtonExecuteOnAnotherTab.Location = new System.Drawing.Point(542, 0);
            this.ButtonExecuteOnAnotherTab.Name = "ButtonExecuteOnAnotherTab";
            this.ButtonExecuteOnAnotherTab.Size = new System.Drawing.Size(100, 23);
            this.ButtonExecuteOnAnotherTab.TabIndex = 2;
            this.ButtonExecuteOnAnotherTab.Text = "Execute";
            this.ButtonExecuteOnAnotherTab.UseVisualStyleBackColor = true;
            this.ButtonExecuteOnAnotherTab.Visible = false;
            this.ButtonExecuteOnAnotherTab.Click += new System.EventHandler(this.ButtonExecuteOnAnotherTab_Click);
            // 
            // mainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(644, 311);
            this.Controls.Add(this.ButtonExecuteOnAnotherTab);
            this.Controls.Add(this.EmuXTabControl);
            this.Controls.Add(this.mainMenuStrip);
            this.MainMenuStrip = this.mainMenuStrip;
            this.MinimumSize = new System.Drawing.Size(657, 350);
            this.Name = "mainForm";
            this.Text = "EmuX";
            this.mainMenuStrip.ResumeLayout(false);
            this.mainMenuStrip.PerformLayout();
            this.GroupboxInput.ResumeLayout(false);
            this.GroupboxControls.ResumeLayout(false);
            this.GroupboxControls.PerformLayout();
            this.GroupboxExecutionProgress.ResumeLayout(false);
            this.GroupboxExecution.ResumeLayout(false);
            this.EmuXTabControl.ResumeLayout(false);
            this.Assembly.ResumeLayout(false);
            this.Memory.ResumeLayout(false);
            this.GroupBoxMemoryControls.ResumeLayout(false);
            this.GroupBoxMemoryControls.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridViewMemory)).EndInit();
            this.Registers.ResumeLayout(false);
            this.GroupBoxEFLAGS.ResumeLayout(false);
            this.GroupBoxEFLAGS.PerformLayout();
            this.GroupBoxGeneralPurposeRegisters.ResumeLayout(false);
            this.GroupBoxGeneralPurposeRegisters.PerformLayout();
            this.Output.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

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
        private Button ButtonResetVirtualSystem;
    }
}