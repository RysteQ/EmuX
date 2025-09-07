namespace EmuX
{
    partial class MainForm
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
            menuStrip1 = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            openToolStripMenuItem = new ToolStripMenuItem();
            saveToolStripMenuItem = new ToolStripMenuItem();
            saveAsToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparatorFileSection = new ToolStripSeparator();
            exitToolStripMenuItem = new ToolStripMenuItem();
            textToolStripMenuItem = new ToolStripMenuItem();
            increaseFontSizeToolStripMenuItem = new ToolStripMenuItem();
            decreaseFontSizeToolStripMenuItem = new ToolStripMenuItem();
            changeFontToolStripMenuItem = new ToolStripMenuItem();
            changeFontColourToolStripMenuItem = new ToolStripMenuItem();
            codeToolStripMenuItem = new ToolStripMenuItem();
            executeToolStripMenuItem = new ToolStripMenuItem();
            stepByStepToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparatorCodeSection = new ToolStripSeparator();
            executionSettingsToolStripMenuItem = new ToolStripMenuItem();
            samplesToolStripMenuItem = new ToolStripMenuItem();
            starterToolStripMenuItem = new ToolStripMenuItem();
            addTwoNumbersToolStripMenuItem = new ToolStripMenuItem();
            forLoopToolStripMenuItem = new ToolStripMenuItem();
            beepToolStripMenuItem = new ToolStripMenuItem();
            terminalToolStripMenuItem = new ToolStripMenuItem();
            helloWorldToolStripMenuItem = new ToolStripMenuItem();
            pyramidToolStripMenuItem = new ToolStripMenuItem();
            graphicalToolStripMenuItem = new ToolStripMenuItem();
            drawLineToolStripMenuItem = new ToolStripMenuItem();
            drawSquareToolStripMenuItem = new ToolStripMenuItem();
            drawColouredToolStripMenuItem = new ToolStripMenuItem();
            aboutToolStripMenuItem = new ToolStripMenuItem();
            mainTabPageControl = new TabControl();
            tabPageAssemblyCode = new TabPage();
            richTextboxAssemblyCode = new RichTextBox();
            tabPageMemory = new TabPage();
            groupBox1 = new GroupBox();
            checkBoxHighlightAccessedMemory = new CheckBox();
            labelEnd = new Label();
            labelStart = new Label();
            textBoxMemorySearchRangeStart = new TextBox();
            textBoxMemorySearchRangeEnd = new TextBox();
            buttonSearch = new Button();
            dataGridMemoryView = new DataGridView();
            tabPageRegisters = new TabPage();
            listBoxVirtualCpuRegisters = new ListBox();
            tabPageOutput = new TabPage();
            richTextBoxOutput = new RichTextBox();
            menuStrip1.SuspendLayout();
            mainTabPageControl.SuspendLayout();
            tabPageAssemblyCode.SuspendLayout();
            tabPageMemory.SuspendLayout();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridMemoryView).BeginInit();
            tabPageRegisters.SuspendLayout();
            tabPageOutput.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, textToolStripMenuItem, codeToolStripMenuItem, samplesToolStripMenuItem, aboutToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(800, 24);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "MainMenuStrip";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { openToolStripMenuItem, saveToolStripMenuItem, saveAsToolStripMenuItem, toolStripSeparatorFileSection, exitToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(37, 20);
            fileToolStripMenuItem.Text = "File";
            // 
            // openToolStripMenuItem
            // 
            openToolStripMenuItem.Name = "openToolStripMenuItem";
            openToolStripMenuItem.Size = new Size(180, 22);
            openToolStripMenuItem.Text = "Open";
            // 
            // saveToolStripMenuItem
            // 
            saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            saveToolStripMenuItem.Size = new Size(180, 22);
            saveToolStripMenuItem.Text = "Save";
            // 
            // saveAsToolStripMenuItem
            // 
            saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            saveAsToolStripMenuItem.Size = new Size(180, 22);
            saveAsToolStripMenuItem.Text = "Save as";
            // 
            // toolStripSeparatorFileSection
            // 
            toolStripSeparatorFileSection.Name = "toolStripSeparatorFileSection";
            toolStripSeparatorFileSection.Size = new Size(177, 6);
            // 
            // exitToolStripMenuItem
            // 
            exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            exitToolStripMenuItem.Size = new Size(180, 22);
            exitToolStripMenuItem.Text = "Exit";
            // 
            // textToolStripMenuItem
            // 
            textToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { increaseFontSizeToolStripMenuItem, decreaseFontSizeToolStripMenuItem, changeFontToolStripMenuItem, changeFontColourToolStripMenuItem });
            textToolStripMenuItem.Name = "textToolStripMenuItem";
            textToolStripMenuItem.Size = new Size(40, 20);
            textToolStripMenuItem.Text = "Text";
            // 
            // increaseFontSizeToolStripMenuItem
            // 
            increaseFontSizeToolStripMenuItem.Name = "increaseFontSizeToolStripMenuItem";
            increaseFontSizeToolStripMenuItem.Size = new Size(180, 22);
            increaseFontSizeToolStripMenuItem.Text = "Increase font size";
            // 
            // decreaseFontSizeToolStripMenuItem
            // 
            decreaseFontSizeToolStripMenuItem.Name = "decreaseFontSizeToolStripMenuItem";
            decreaseFontSizeToolStripMenuItem.Size = new Size(180, 22);
            decreaseFontSizeToolStripMenuItem.Text = "Decrease font size";
            // 
            // changeFontToolStripMenuItem
            // 
            changeFontToolStripMenuItem.Name = "changeFontToolStripMenuItem";
            changeFontToolStripMenuItem.Size = new Size(180, 22);
            changeFontToolStripMenuItem.Text = "Change font";
            // 
            // changeFontColourToolStripMenuItem
            // 
            changeFontColourToolStripMenuItem.Name = "changeFontColourToolStripMenuItem";
            changeFontColourToolStripMenuItem.Size = new Size(180, 22);
            changeFontColourToolStripMenuItem.Text = "Change font colour";
            // 
            // codeToolStripMenuItem
            // 
            codeToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { executeToolStripMenuItem, stepByStepToolStripMenuItem, toolStripSeparatorCodeSection, executionSettingsToolStripMenuItem });
            codeToolStripMenuItem.Name = "codeToolStripMenuItem";
            codeToolStripMenuItem.Size = new Size(47, 20);
            codeToolStripMenuItem.Text = "Code";
            // 
            // executeToolStripMenuItem
            // 
            executeToolStripMenuItem.Name = "executeToolStripMenuItem";
            executeToolStripMenuItem.Size = new Size(169, 22);
            executeToolStripMenuItem.Text = "Execute";
            // 
            // stepByStepToolStripMenuItem
            // 
            stepByStepToolStripMenuItem.Name = "stepByStepToolStripMenuItem";
            stepByStepToolStripMenuItem.Size = new Size(169, 22);
            stepByStepToolStripMenuItem.Text = "Debug";
            // 
            // toolStripSeparatorCodeSection
            // 
            toolStripSeparatorCodeSection.Name = "toolStripSeparatorCodeSection";
            toolStripSeparatorCodeSection.Size = new Size(166, 6);
            // 
            // executionSettingsToolStripMenuItem
            // 
            executionSettingsToolStripMenuItem.Name = "executionSettingsToolStripMenuItem";
            executionSettingsToolStripMenuItem.Size = new Size(169, 22);
            executionSettingsToolStripMenuItem.Text = "Execution settings";
            // 
            // samplesToolStripMenuItem
            // 
            samplesToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { starterToolStripMenuItem, terminalToolStripMenuItem, graphicalToolStripMenuItem });
            samplesToolStripMenuItem.Name = "samplesToolStripMenuItem";
            samplesToolStripMenuItem.Size = new Size(63, 20);
            samplesToolStripMenuItem.Text = "Samples";
            // 
            // starterToolStripMenuItem
            // 
            starterToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { addTwoNumbersToolStripMenuItem, forLoopToolStripMenuItem, beepToolStripMenuItem });
            starterToolStripMenuItem.Name = "starterToolStripMenuItem";
            starterToolStripMenuItem.Size = new Size(124, 22);
            starterToolStripMenuItem.Text = "Starter";
            // 
            // addTwoNumbersToolStripMenuItem
            // 
            addTwoNumbersToolStripMenuItem.Name = "addTwoNumbersToolStripMenuItem";
            addTwoNumbersToolStripMenuItem.Size = new Size(169, 22);
            addTwoNumbersToolStripMenuItem.Text = "Add two numbers";
            // 
            // forLoopToolStripMenuItem
            // 
            forLoopToolStripMenuItem.Name = "forLoopToolStripMenuItem";
            forLoopToolStripMenuItem.Size = new Size(169, 22);
            forLoopToolStripMenuItem.Text = "For loop";
            // 
            // beepToolStripMenuItem
            // 
            beepToolStripMenuItem.Name = "beepToolStripMenuItem";
            beepToolStripMenuItem.Size = new Size(169, 22);
            beepToolStripMenuItem.Text = "Beep";
            // 
            // terminalToolStripMenuItem
            // 
            terminalToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { helloWorldToolStripMenuItem, pyramidToolStripMenuItem });
            terminalToolStripMenuItem.Name = "terminalToolStripMenuItem";
            terminalToolStripMenuItem.Size = new Size(124, 22);
            terminalToolStripMenuItem.Text = "Terminal";
            // 
            // helloWorldToolStripMenuItem
            // 
            helloWorldToolStripMenuItem.Name = "helloWorldToolStripMenuItem";
            helloWorldToolStripMenuItem.Size = new Size(137, 22);
            helloWorldToolStripMenuItem.Text = "Hello World";
            // 
            // pyramidToolStripMenuItem
            // 
            pyramidToolStripMenuItem.Name = "pyramidToolStripMenuItem";
            pyramidToolStripMenuItem.Size = new Size(137, 22);
            pyramidToolStripMenuItem.Text = "Pyramid";
            // 
            // graphicalToolStripMenuItem
            // 
            graphicalToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { drawLineToolStripMenuItem, drawSquareToolStripMenuItem, drawColouredToolStripMenuItem });
            graphicalToolStripMenuItem.Name = "graphicalToolStripMenuItem";
            graphicalToolStripMenuItem.Size = new Size(124, 22);
            graphicalToolStripMenuItem.Text = "Graphical";
            // 
            // drawLineToolStripMenuItem
            // 
            drawLineToolStripMenuItem.Name = "drawLineToolStripMenuItem";
            drawLineToolStripMenuItem.Size = new Size(151, 22);
            drawLineToolStripMenuItem.Text = "Draw line";
            // 
            // drawSquareToolStripMenuItem
            // 
            drawSquareToolStripMenuItem.Name = "drawSquareToolStripMenuItem";
            drawSquareToolStripMenuItem.Size = new Size(151, 22);
            drawSquareToolStripMenuItem.Text = "Draw square";
            // 
            // drawColouredToolStripMenuItem
            // 
            drawColouredToolStripMenuItem.Name = "drawColouredToolStripMenuItem";
            drawColouredToolStripMenuItem.Size = new Size(151, 22);
            drawColouredToolStripMenuItem.Text = "Draw coloured";
            // 
            // aboutToolStripMenuItem
            // 
            aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            aboutToolStripMenuItem.Size = new Size(52, 20);
            aboutToolStripMenuItem.Text = "About";
            aboutToolStripMenuItem.Click += aboutToolStripMenuItem_Click;
            // 
            // mainTabPageControl
            // 
            mainTabPageControl.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            mainTabPageControl.Controls.Add(tabPageAssemblyCode);
            mainTabPageControl.Controls.Add(tabPageMemory);
            mainTabPageControl.Controls.Add(tabPageRegisters);
            mainTabPageControl.Controls.Add(tabPageOutput);
            mainTabPageControl.Location = new Point(0, 27);
            mainTabPageControl.Name = "mainTabPageControl";
            mainTabPageControl.SelectedIndex = 0;
            mainTabPageControl.Size = new Size(800, 422);
            mainTabPageControl.TabIndex = 1;
            // 
            // tabPageAssemblyCode
            // 
            tabPageAssemblyCode.Controls.Add(richTextboxAssemblyCode);
            tabPageAssemblyCode.Location = new Point(4, 24);
            tabPageAssemblyCode.Name = "tabPageAssemblyCode";
            tabPageAssemblyCode.Padding = new Padding(3);
            tabPageAssemblyCode.Size = new Size(792, 394);
            tabPageAssemblyCode.TabIndex = 0;
            tabPageAssemblyCode.Text = "Assembly Code";
            tabPageAssemblyCode.UseVisualStyleBackColor = true;
            // 
            // richTextboxAssemblyCode
            // 
            richTextboxAssemblyCode.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            richTextboxAssemblyCode.Location = new Point(6, 6);
            richTextboxAssemblyCode.Name = "richTextboxAssemblyCode";
            richTextboxAssemblyCode.Size = new Size(780, 381);
            richTextboxAssemblyCode.TabIndex = 0;
            richTextboxAssemblyCode.Text = "";
            // 
            // tabPageMemory
            // 
            tabPageMemory.Controls.Add(groupBox1);
            tabPageMemory.Controls.Add(dataGridMemoryView);
            tabPageMemory.Location = new Point(4, 24);
            tabPageMemory.Name = "tabPageMemory";
            tabPageMemory.Padding = new Padding(3);
            tabPageMemory.Size = new Size(792, 394);
            tabPageMemory.TabIndex = 1;
            tabPageMemory.Text = "Memory";
            tabPageMemory.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(checkBoxHighlightAccessedMemory);
            groupBox1.Controls.Add(labelEnd);
            groupBox1.Controls.Add(labelStart);
            groupBox1.Controls.Add(textBoxMemorySearchRangeStart);
            groupBox1.Controls.Add(textBoxMemorySearchRangeEnd);
            groupBox1.Controls.Add(buttonSearch);
            groupBox1.Location = new Point(6, 8);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(778, 137);
            groupBox1.TabIndex = 6;
            groupBox1.TabStop = false;
            groupBox1.Text = "Memory Range Search";
            // 
            // checkBoxHighlightAccessedMemory
            // 
            checkBoxHighlightAccessedMemory.AutoSize = true;
            checkBoxHighlightAccessedMemory.Location = new Point(598, 25);
            checkBoxHighlightAccessedMemory.Name = "checkBoxHighlightAccessedMemory";
            checkBoxHighlightAccessedMemory.Size = new Size(174, 19);
            checkBoxHighlightAccessedMemory.TabIndex = 3;
            checkBoxHighlightAccessedMemory.Text = "Highlight accessed memory";
            checkBoxHighlightAccessedMemory.UseVisualStyleBackColor = true;
            // 
            // labelEnd
            // 
            labelEnd.AutoSize = true;
            labelEnd.Location = new Point(6, 54);
            labelEnd.Name = "labelEnd";
            labelEnd.Size = new Size(27, 15);
            labelEnd.TabIndex = 7;
            labelEnd.Text = "End";
            // 
            // labelStart
            // 
            labelStart.AutoSize = true;
            labelStart.Location = new Point(6, 25);
            labelStart.Name = "labelStart";
            labelStart.Size = new Size(31, 15);
            labelStart.TabIndex = 7;
            labelStart.Text = "Start";
            // 
            // textBoxMemorySearchRangeStart
            // 
            textBoxMemorySearchRangeStart.Location = new Point(73, 22);
            textBoxMemorySearchRangeStart.Name = "textBoxMemorySearchRangeStart";
            textBoxMemorySearchRangeStart.Size = new Size(519, 23);
            textBoxMemorySearchRangeStart.TabIndex = 0;
            // 
            // textBoxMemorySearchRangeEnd
            // 
            textBoxMemorySearchRangeEnd.Location = new Point(73, 51);
            textBoxMemorySearchRangeEnd.Name = "textBoxMemorySearchRangeEnd";
            textBoxMemorySearchRangeEnd.Size = new Size(519, 23);
            textBoxMemorySearchRangeEnd.TabIndex = 1;
            // 
            // buttonSearch
            // 
            buttonSearch.Location = new Point(73, 109);
            buttonSearch.Name = "buttonSearch";
            buttonSearch.Size = new Size(699, 23);
            buttonSearch.TabIndex = 5;
            buttonSearch.Text = "Search";
            buttonSearch.UseVisualStyleBackColor = true;
            // 
            // dataGridMemoryView
            // 
            dataGridMemoryView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dataGridMemoryView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridMemoryView.Location = new Point(8, 151);
            dataGridMemoryView.Name = "dataGridMemoryView";
            dataGridMemoryView.Size = new Size(776, 236);
            dataGridMemoryView.TabIndex = 5;
            // 
            // tabPageRegisters
            // 
            tabPageRegisters.Controls.Add(listBoxVirtualCpuRegisters);
            tabPageRegisters.Location = new Point(4, 24);
            tabPageRegisters.Name = "tabPageRegisters";
            tabPageRegisters.Size = new Size(792, 394);
            tabPageRegisters.TabIndex = 2;
            tabPageRegisters.Text = "Registers";
            tabPageRegisters.UseVisualStyleBackColor = true;
            // 
            // listBoxVirtualCpuRegisters
            // 
            listBoxVirtualCpuRegisters.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            listBoxVirtualCpuRegisters.FormattingEnabled = true;
            listBoxVirtualCpuRegisters.Location = new Point(3, 3);
            listBoxVirtualCpuRegisters.Name = "listBoxVirtualCpuRegisters";
            listBoxVirtualCpuRegisters.RightToLeft = RightToLeft.No;
            listBoxVirtualCpuRegisters.Size = new Size(786, 394);
            listBoxVirtualCpuRegisters.TabIndex = 0;
            listBoxVirtualCpuRegisters.DoubleClick += listBoxVirtualCpuRegisters_DoubleClick;
            listBoxVirtualCpuRegisters.KeyDown += listBoxVirtualCpuRegisters_KeyDown;
            // 
            // tabPageOutput
            // 
            tabPageOutput.Controls.Add(richTextBoxOutput);
            tabPageOutput.Location = new Point(4, 24);
            tabPageOutput.Name = "tabPageOutput";
            tabPageOutput.Size = new Size(792, 394);
            tabPageOutput.TabIndex = 3;
            tabPageOutput.Text = "Output";
            tabPageOutput.UseVisualStyleBackColor = true;
            // 
            // richTextBoxOutput
            // 
            richTextBoxOutput.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            richTextBoxOutput.Enabled = false;
            richTextBoxOutput.Location = new Point(6, 6);
            richTextBoxOutput.Name = "richTextBoxOutput";
            richTextBoxOutput.Size = new Size(780, 381);
            richTextBoxOutput.TabIndex = 0;
            richTextBoxOutput.Text = "";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(mainTabPageControl);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "MainForm";
            Text = "EmuX";
            Load += MainForm_Load;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            mainTabPageControl.ResumeLayout(false);
            tabPageAssemblyCode.ResumeLayout(false);
            tabPageMemory.ResumeLayout(false);
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridMemoryView).EndInit();
            tabPageRegisters.ResumeLayout(false);
            tabPageOutput.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip1;
        private TabControl mainTabPageControl;
        private TabPage tabPageAssemblyCode;
        private TabPage tabPageMemory;
        private RichTextBox richTextboxAssemblyCode;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem textToolStripMenuItem;
        private ToolStripMenuItem codeToolStripMenuItem;
        private ToolStripMenuItem aboutToolStripMenuItem;
        private ToolStripMenuItem samplesToolStripMenuItem;
        private TabPage tabPageRegisters;
        private TabPage tabPageOutput;
        private DataGridView dataGridMemoryView;
        private Button buttonSearch;
        private TextBox textBoxMemorySearchRangeEnd;
        private TextBox textBoxMemorySearchRangeStart;
        private GroupBox groupBox1;
        private Label labelEnd;
        private Label labelStart;
        private CheckBox checkBoxHighlightAccessedMemory;
        private RichTextBox richTextBoxOutput;
        private ToolStripMenuItem starterToolStripMenuItem;
        private ToolStripMenuItem terminalToolStripMenuItem;
        private ToolStripMenuItem openToolStripMenuItem;
        private ToolStripMenuItem saveToolStripMenuItem;
        private ToolStripMenuItem saveAsToolStripMenuItem;
        private ToolStripSeparator toolStripSeparatorFileSection;
        private ToolStripMenuItem exitToolStripMenuItem;
        private ToolStripMenuItem increaseFontSizeToolStripMenuItem;
        private ToolStripMenuItem decreaseFontSizeToolStripMenuItem;
        private ToolStripMenuItem changeFontToolStripMenuItem;
        private ToolStripMenuItem changeFontColourToolStripMenuItem;
        private ToolStripMenuItem executeToolStripMenuItem;
        private ToolStripMenuItem stepByStepToolStripMenuItem;
        private ToolStripSeparator toolStripSeparatorCodeSection;
        private ToolStripMenuItem executionSettingsToolStripMenuItem;
        private ToolStripMenuItem addTwoNumbersToolStripMenuItem;
        private ToolStripMenuItem forLoopToolStripMenuItem;
        private ToolStripMenuItem beepToolStripMenuItem;
        private ToolStripMenuItem helloWorldToolStripMenuItem;
        private ToolStripMenuItem pyramidToolStripMenuItem;
        private ToolStripMenuItem graphicalToolStripMenuItem;
        private ToolStripMenuItem drawLineToolStripMenuItem;
        private ToolStripMenuItem drawSquareToolStripMenuItem;
        private ToolStripMenuItem drawColouredToolStripMenuItem;
        private ListBox listBoxVirtualCpuRegisters;
    }
}
