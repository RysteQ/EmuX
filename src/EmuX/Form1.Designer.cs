﻿namespace EmuX
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
            this.memoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showMemoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.memoryDumpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.registersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showRegistersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.GroupboxInput = new System.Windows.Forms.GroupBox();
            this.RichTextboxAssemblyCode = new System.Windows.Forms.RichTextBox();
            this.GroupboxControls = new System.Windows.Forms.GroupBox();
            this.GroupboxExecutionProgress = new System.Windows.Forms.GroupBox();
            this.ProgressBarExecutionProgress = new System.Windows.Forms.ProgressBar();
            this.GroupboxRegisters = new System.Windows.Forms.GroupBox();
            this.ButtonShowRegisterValues = new System.Windows.Forms.Button();
            this.GroupboxMemory = new System.Windows.Forms.GroupBox();
            this.ButtonShowMemory = new System.Windows.Forms.Button();
            this.LabelCurrentInstruction = new System.Windows.Forms.Label();
            this.GroupboxExecution = new System.Windows.Forms.GroupBox();
            this.ButtonExecute = new System.Windows.Forms.Button();
            this.ButtonPreviousInstruction = new System.Windows.Forms.Button();
            this.ButtonNextInstruction = new System.Windows.Forms.Button();
            this.openFD = new System.Windows.Forms.OpenFileDialog();
            this.saveFD = new System.Windows.Forms.SaveFileDialog();
            this.mainMenuStrip.SuspendLayout();
            this.GroupboxInput.SuspendLayout();
            this.GroupboxControls.SuspendLayout();
            this.GroupboxExecutionProgress.SuspendLayout();
            this.GroupboxRegisters.SuspendLayout();
            this.GroupboxMemory.SuspendLayout();
            this.GroupboxExecution.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainMenuStrip
            // 
            this.mainMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.memoryToolStripMenuItem,
            this.registersToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.mainMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.mainMenuStrip.Name = "mainMenuStrip";
            this.mainMenuStrip.Size = new System.Drawing.Size(800, 24);
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
            // memoryToolStripMenuItem
            // 
            this.memoryToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showMemoryToolStripMenuItem,
            this.memoryDumpToolStripMenuItem});
            this.memoryToolStripMenuItem.Name = "memoryToolStripMenuItem";
            this.memoryToolStripMenuItem.Size = new System.Drawing.Size(64, 20);
            this.memoryToolStripMenuItem.Text = "Memory";
            // 
            // showMemoryToolStripMenuItem
            // 
            this.showMemoryToolStripMenuItem.Name = "showMemoryToolStripMenuItem";
            this.showMemoryToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.M)));
            this.showMemoryToolStripMenuItem.Size = new System.Drawing.Size(232, 22);
            this.showMemoryToolStripMenuItem.Text = "Show Memory";
            // 
            // memoryDumpToolStripMenuItem
            // 
            this.memoryDumpToolStripMenuItem.Name = "memoryDumpToolStripMenuItem";
            this.memoryDumpToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.M)));
            this.memoryDumpToolStripMenuItem.Size = new System.Drawing.Size(232, 22);
            this.memoryDumpToolStripMenuItem.Text = "Memory Dump";
            // 
            // registersToolStripMenuItem
            // 
            this.registersToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showRegistersToolStripMenuItem});
            this.registersToolStripMenuItem.Name = "registersToolStripMenuItem";
            this.registersToolStripMenuItem.Size = new System.Drawing.Size(66, 20);
            this.registersToolStripMenuItem.Text = "Registers";
            // 
            // showRegistersToolStripMenuItem
            // 
            this.showRegistersToolStripMenuItem.Name = "showRegistersToolStripMenuItem";
            this.showRegistersToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.R)));
            this.showRegistersToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
            this.showRegistersToolStripMenuItem.Text = "Show Registers";
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
            this.GroupboxInput.Location = new System.Drawing.Point(12, 27);
            this.GroupboxInput.Name = "GroupboxInput";
            this.GroupboxInput.Size = new System.Drawing.Size(550, 367);
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
            this.RichTextboxAssemblyCode.Size = new System.Drawing.Size(538, 336);
            this.RichTextboxAssemblyCode.TabIndex = 0;
            this.RichTextboxAssemblyCode.Text = "";
            // 
            // GroupboxControls
            // 
            this.GroupboxControls.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupboxControls.Controls.Add(this.GroupboxExecutionProgress);
            this.GroupboxControls.Controls.Add(this.GroupboxRegisters);
            this.GroupboxControls.Controls.Add(this.GroupboxMemory);
            this.GroupboxControls.Controls.Add(this.LabelCurrentInstruction);
            this.GroupboxControls.Controls.Add(this.GroupboxExecution);
            this.GroupboxControls.Location = new System.Drawing.Point(568, 27);
            this.GroupboxControls.Name = "GroupboxControls";
            this.GroupboxControls.Size = new System.Drawing.Size(220, 367);
            this.GroupboxControls.TabIndex = 2;
            this.GroupboxControls.TabStop = false;
            this.GroupboxControls.Text = "Controls";
            // 
            // GroupboxExecutionProgress
            // 
            this.GroupboxExecutionProgress.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupboxExecutionProgress.Controls.Add(this.ProgressBarExecutionProgress);
            this.GroupboxExecutionProgress.Location = new System.Drawing.Point(8, 286);
            this.GroupboxExecutionProgress.Name = "GroupboxExecutionProgress";
            this.GroupboxExecutionProgress.Size = new System.Drawing.Size(200, 72);
            this.GroupboxExecutionProgress.TabIndex = 4;
            this.GroupboxExecutionProgress.TabStop = false;
            this.GroupboxExecutionProgress.Text = "Execution Progress";
            // 
            // ProgressBarExecutionProgress
            // 
            this.ProgressBarExecutionProgress.Location = new System.Drawing.Point(8, 22);
            this.ProgressBarExecutionProgress.Name = "ProgressBarExecutionProgress";
            this.ProgressBarExecutionProgress.Size = new System.Drawing.Size(186, 44);
            this.ProgressBarExecutionProgress.TabIndex = 0;
            // 
            // GroupboxRegisters
            // 
            this.GroupboxRegisters.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupboxRegisters.Controls.Add(this.ButtonShowRegisterValues);
            this.GroupboxRegisters.Location = new System.Drawing.Point(8, 228);
            this.GroupboxRegisters.Name = "GroupboxRegisters";
            this.GroupboxRegisters.Size = new System.Drawing.Size(206, 52);
            this.GroupboxRegisters.TabIndex = 3;
            this.GroupboxRegisters.TabStop = false;
            this.GroupboxRegisters.Text = "Registers";
            // 
            // ButtonShowRegisterValues
            // 
            this.ButtonShowRegisterValues.Location = new System.Drawing.Point(6, 22);
            this.ButtonShowRegisterValues.Name = "ButtonShowRegisterValues";
            this.ButtonShowRegisterValues.Size = new System.Drawing.Size(194, 23);
            this.ButtonShowRegisterValues.TabIndex = 0;
            this.ButtonShowRegisterValues.Text = "Show Registers";
            this.ButtonShowRegisterValues.UseVisualStyleBackColor = true;
            this.ButtonShowRegisterValues.Click += new System.EventHandler(this.ButtonShowRegisterValues_Click);
            // 
            // GroupboxMemory
            // 
            this.GroupboxMemory.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupboxMemory.Controls.Add(this.ButtonShowMemory);
            this.GroupboxMemory.Location = new System.Drawing.Point(8, 168);
            this.GroupboxMemory.Name = "GroupboxMemory";
            this.GroupboxMemory.Size = new System.Drawing.Size(206, 54);
            this.GroupboxMemory.TabIndex = 2;
            this.GroupboxMemory.TabStop = false;
            this.GroupboxMemory.Text = "Memory";
            // 
            // ButtonShowMemory
            // 
            this.ButtonShowMemory.Location = new System.Drawing.Point(6, 22);
            this.ButtonShowMemory.Name = "ButtonShowMemory";
            this.ButtonShowMemory.Size = new System.Drawing.Size(194, 23);
            this.ButtonShowMemory.TabIndex = 0;
            this.ButtonShowMemory.Text = "Show Memory";
            this.ButtonShowMemory.UseVisualStyleBackColor = true;
            this.ButtonShowMemory.Click += new System.EventHandler(this.ButtonShowMemory_Click);
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
            this.GroupboxExecution.Location = new System.Drawing.Point(6, 52);
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
            // mainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 404);
            this.Controls.Add(this.GroupboxControls);
            this.Controls.Add(this.GroupboxInput);
            this.Controls.Add(this.mainMenuStrip);
            this.MainMenuStrip = this.mainMenuStrip;
            this.MinimumSize = new System.Drawing.Size(816, 443);
            this.Name = "mainForm";
            this.Text = "EmuX";
            this.Load += new System.EventHandler(this.mainForm_Load);
            this.mainMenuStrip.ResumeLayout(false);
            this.mainMenuStrip.PerformLayout();
            this.GroupboxInput.ResumeLayout(false);
            this.GroupboxControls.ResumeLayout(false);
            this.GroupboxControls.PerformLayout();
            this.GroupboxExecutionProgress.ResumeLayout(false);
            this.GroupboxRegisters.ResumeLayout(false);
            this.GroupboxMemory.ResumeLayout(false);
            this.GroupboxExecution.ResumeLayout(false);
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
        private ToolStripMenuItem memoryToolStripMenuItem;
        private ToolStripMenuItem showMemoryToolStripMenuItem;
        private ToolStripMenuItem memoryDumpToolStripMenuItem;
        private ToolStripMenuItem registersToolStripMenuItem;
        private ToolStripMenuItem showRegistersToolStripMenuItem;
        private ToolStripMenuItem aboutToolStripMenuItem;
        private GroupBox GroupboxInput;
        private RichTextBox RichTextboxAssemblyCode;
        private GroupBox GroupboxControls;
        private GroupBox GroupboxExecutionProgress;
        private ProgressBar ProgressBarExecutionProgress;
        private GroupBox GroupboxRegisters;
        private Button ButtonShowRegisterValues;
        private GroupBox GroupboxMemory;
        private Button ButtonShowMemory;
        private Label LabelCurrentInstruction;
        private GroupBox GroupboxExecution;
        private Button ButtonExecute;
        private Button ButtonPreviousInstruction;
        private Button ButtonNextInstruction;
        private OpenFileDialog openFD;
        private SaveFileDialog saveFD;
    }
}