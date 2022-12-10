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
            this.Registers = new System.Windows.Forms.TabPage();
            this.Output = new System.Windows.Forms.TabPage();
            this.mainMenuStrip.SuspendLayout();
            this.GroupboxInput.SuspendLayout();
            this.GroupboxControls.SuspendLayout();
            this.GroupboxExecutionProgress.SuspendLayout();
            this.GroupboxExecution.SuspendLayout();
            this.EmuXTabControl.SuspendLayout();
            this.Assembly.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainMenuStrip
            // 
            this.mainMenuStrip.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.mainMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.mainMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.mainMenuStrip.Name = "mainMenuStrip";
            this.mainMenuStrip.Size = new System.Drawing.Size(584, 24);
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
            this.GroupboxInput.Size = new System.Drawing.Size(338, 244);
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
            this.RichTextboxAssemblyCode.Size = new System.Drawing.Size(326, 214);
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
            this.GroupboxControls.Location = new System.Drawing.Point(350, 6);
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
            // 
            // ButtonNextInstruction
            // 
            this.ButtonNextInstruction.Location = new System.Drawing.Point(6, 22);
            this.ButtonNextInstruction.Name = "ButtonNextInstruction";
            this.ButtonNextInstruction.Size = new System.Drawing.Size(196, 23);
            this.ButtonNextInstruction.TabIndex = 0;
            this.ButtonNextInstruction.Text = "Next Instruction";
            this.ButtonNextInstruction.UseVisualStyleBackColor = true;
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
            this.EmuXTabControl.Size = new System.Drawing.Size(586, 285);
            this.EmuXTabControl.TabIndex = 1;
            // 
            // Assembly
            // 
            this.Assembly.Controls.Add(this.GroupboxInput);
            this.Assembly.Controls.Add(this.GroupboxControls);
            this.Assembly.Location = new System.Drawing.Point(4, 24);
            this.Assembly.Name = "Assembly";
            this.Assembly.Padding = new System.Windows.Forms.Padding(3);
            this.Assembly.Size = new System.Drawing.Size(578, 257);
            this.Assembly.TabIndex = 0;
            this.Assembly.Text = "Assembly";
            this.Assembly.UseVisualStyleBackColor = true;
            // 
            // Memory
            // 
            this.Memory.Location = new System.Drawing.Point(4, 24);
            this.Memory.Name = "Memory";
            this.Memory.Padding = new System.Windows.Forms.Padding(3);
            this.Memory.Size = new System.Drawing.Size(576, 257);
            this.Memory.TabIndex = 1;
            this.Memory.Text = "Memory";
            this.Memory.UseVisualStyleBackColor = true;
            // 
            // Registers
            // 
            this.Registers.Location = new System.Drawing.Point(4, 24);
            this.Registers.Name = "Registers";
            this.Registers.Size = new System.Drawing.Size(576, 257);
            this.Registers.TabIndex = 2;
            this.Registers.Text = "Registers";
            this.Registers.UseVisualStyleBackColor = true;
            // 
            // Output
            // 
            this.Output.Location = new System.Drawing.Point(4, 24);
            this.Output.Name = "Output";
            this.Output.Size = new System.Drawing.Size(576, 257);
            this.Output.TabIndex = 3;
            this.Output.Text = "Output";
            this.Output.UseVisualStyleBackColor = true;
            // 
            // mainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 311);
            this.Controls.Add(this.EmuXTabControl);
            this.Controls.Add(this.mainMenuStrip);
            this.MainMenuStrip = this.mainMenuStrip;
            this.MinimumSize = new System.Drawing.Size(600, 350);
            this.Name = "mainForm";
            this.Text = "EmuX";
            this.Load += new System.EventHandler(this.mainForm_Load);
            this.mainMenuStrip.ResumeLayout(false);
            this.mainMenuStrip.PerformLayout();
            this.GroupboxInput.ResumeLayout(false);
            this.GroupboxControls.ResumeLayout(false);
            this.GroupboxControls.PerformLayout();
            this.GroupboxExecutionProgress.ResumeLayout(false);
            this.GroupboxExecution.ResumeLayout(false);
            this.EmuXTabControl.ResumeLayout(false);
            this.Assembly.ResumeLayout(false);
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
    }
}