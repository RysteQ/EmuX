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
            toolStripSeparator2 = new ToolStripSeparator();
            exitToolStripMenuItem = new ToolStripMenuItem();
            textToolStripMenuItem = new ToolStripMenuItem();
            increaseFontSizeToolStripMenuItem = new ToolStripMenuItem();
            decreaseFontSizeToolStripMenuItem = new ToolStripMenuItem();
            changeFontToolStripMenuItem = new ToolStripMenuItem();
            changeFontColourToolStripMenuItem = new ToolStripMenuItem();
            codeToolStripMenuItem = new ToolStripMenuItem();
            executeToolStripMenuItem = new ToolStripMenuItem();
            stepbystepToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator1 = new ToolStripSeparator();
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
            groupBox2 = new GroupBox();
            labelValueToAssign = new Label();
            labelValueToAssignSystem = new Label();
            textBoxValueToAssign = new TextBox();
            buttonAssignSelectedValues = new Button();
            comboBoxValueToAssignSystem = new ComboBox();
            groupBox1 = new GroupBox();
            checkBoxClearMemoryBeforeExecution = new CheckBox();
            checkBoxHighlightAccessedMemory = new CheckBox();
            labelSystem = new Label();
            comboBoxMemoryValueSystemRepresentation = new ComboBox();
            labelEnd = new Label();
            labelStart = new Label();
            textBoxMemorySearchRangeStart = new TextBox();
            textBoxMemorySearchRangeEnd = new TextBox();
            buttonSearch = new Button();
            dataGridView1 = new DataGridView();
            tabPageRegisters = new TabPage();
            tabPageOutput = new TabPage();
            richTextBoxOutput = new RichTextBox();
            groupBox3 = new GroupBox();
            textBox1 = new TextBox();
            label1 = new Label();
            label2 = new Label();
            textBox2 = new TextBox();
            groupBox4 = new GroupBox();
            textBox3 = new TextBox();
            label3 = new Label();
            label4 = new Label();
            textBox4 = new TextBox();
            groupBox5 = new GroupBox();
            textBox5 = new TextBox();
            label5 = new Label();
            label6 = new Label();
            textBox6 = new TextBox();
            groupBox6 = new GroupBox();
            textBox7 = new TextBox();
            label7 = new Label();
            label8 = new Label();
            textBox8 = new TextBox();
            groupBox7 = new GroupBox();
            groupBox8 = new GroupBox();
            groupBox9 = new GroupBox();
            textBox9 = new TextBox();
            label9 = new Label();
            groupBox10 = new GroupBox();
            label10 = new Label();
            textBox10 = new TextBox();
            groupBox11 = new GroupBox();
            label11 = new Label();
            textBox11 = new TextBox();
            groupBox12 = new GroupBox();
            label12 = new Label();
            textBox12 = new TextBox();
            menuStrip1.SuspendLayout();
            mainTabPageControl.SuspendLayout();
            tabPageAssemblyCode.SuspendLayout();
            tabPageMemory.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            tabPageRegisters.SuspendLayout();
            tabPageOutput.SuspendLayout();
            groupBox3.SuspendLayout();
            groupBox4.SuspendLayout();
            groupBox5.SuspendLayout();
            groupBox6.SuspendLayout();
            groupBox7.SuspendLayout();
            groupBox8.SuspendLayout();
            groupBox9.SuspendLayout();
            groupBox10.SuspendLayout();
            groupBox11.SuspendLayout();
            groupBox12.SuspendLayout();
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
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { openToolStripMenuItem, saveToolStripMenuItem, saveAsToolStripMenuItem, toolStripSeparator2, exitToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(37, 20);
            fileToolStripMenuItem.Text = "File";
            // 
            // openToolStripMenuItem
            // 
            openToolStripMenuItem.Name = "openToolStripMenuItem";
            openToolStripMenuItem.Size = new Size(112, 22);
            openToolStripMenuItem.Text = "Open";
            // 
            // saveToolStripMenuItem
            // 
            saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            saveToolStripMenuItem.Size = new Size(112, 22);
            saveToolStripMenuItem.Text = "Save";
            // 
            // saveAsToolStripMenuItem
            // 
            saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            saveAsToolStripMenuItem.Size = new Size(112, 22);
            saveAsToolStripMenuItem.Text = "Save as";
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new Size(109, 6);
            // 
            // exitToolStripMenuItem
            // 
            exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            exitToolStripMenuItem.Size = new Size(112, 22);
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
            increaseFontSizeToolStripMenuItem.Size = new Size(177, 22);
            increaseFontSizeToolStripMenuItem.Text = "Increase font size";
            // 
            // decreaseFontSizeToolStripMenuItem
            // 
            decreaseFontSizeToolStripMenuItem.Name = "decreaseFontSizeToolStripMenuItem";
            decreaseFontSizeToolStripMenuItem.Size = new Size(177, 22);
            decreaseFontSizeToolStripMenuItem.Text = "Decrease font size";
            // 
            // changeFontToolStripMenuItem
            // 
            changeFontToolStripMenuItem.Name = "changeFontToolStripMenuItem";
            changeFontToolStripMenuItem.Size = new Size(177, 22);
            changeFontToolStripMenuItem.Text = "Change font";
            // 
            // changeFontColourToolStripMenuItem
            // 
            changeFontColourToolStripMenuItem.Name = "changeFontColourToolStripMenuItem";
            changeFontColourToolStripMenuItem.Size = new Size(177, 22);
            changeFontColourToolStripMenuItem.Text = "Change font colour";
            // 
            // codeToolStripMenuItem
            // 
            codeToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { executeToolStripMenuItem, stepbystepToolStripMenuItem, toolStripSeparator1, executionSettingsToolStripMenuItem });
            codeToolStripMenuItem.Name = "codeToolStripMenuItem";
            codeToolStripMenuItem.Size = new Size(47, 20);
            codeToolStripMenuItem.Text = "Code";
            // 
            // executeToolStripMenuItem
            // 
            executeToolStripMenuItem.Name = "executeToolStripMenuItem";
            executeToolStripMenuItem.Size = new Size(170, 22);
            executeToolStripMenuItem.Text = "Execute";
            // 
            // stepbystepToolStripMenuItem
            // 
            stepbystepToolStripMenuItem.Name = "stepbystepToolStripMenuItem";
            stepbystepToolStripMenuItem.Size = new Size(170, 22);
            stepbystepToolStripMenuItem.Text = "Debug";
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(167, 6);
            // 
            // executionSettingsToolStripMenuItem
            // 
            executionSettingsToolStripMenuItem.Name = "executionSettingsToolStripMenuItem";
            executionSettingsToolStripMenuItem.Size = new Size(170, 22);
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
            tabPageMemory.Controls.Add(groupBox2);
            tabPageMemory.Controls.Add(groupBox1);
            tabPageMemory.Controls.Add(dataGridView1);
            tabPageMemory.Location = new Point(4, 24);
            tabPageMemory.Name = "tabPageMemory";
            tabPageMemory.Padding = new Padding(3);
            tabPageMemory.Size = new Size(792, 394);
            tabPageMemory.TabIndex = 1;
            tabPageMemory.Text = "Memory";
            tabPageMemory.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(labelValueToAssign);
            groupBox2.Controls.Add(labelValueToAssignSystem);
            groupBox2.Controls.Add(textBoxValueToAssign);
            groupBox2.Controls.Add(buttonAssignSelectedValues);
            groupBox2.Controls.Add(comboBoxValueToAssignSystem);
            groupBox2.Location = new Point(477, 8);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(307, 137);
            groupBox2.TabIndex = 12;
            groupBox2.TabStop = false;
            groupBox2.Text = "Change Memory Values";
            // 
            // labelValueToAssign
            // 
            labelValueToAssign.AutoSize = true;
            labelValueToAssign.Location = new Point(6, 25);
            labelValueToAssign.Name = "labelValueToAssign";
            labelValueToAssign.Size = new Size(35, 15);
            labelValueToAssign.TabIndex = 7;
            labelValueToAssign.Text = "Value";
            // 
            // labelValueToAssignSystem
            // 
            labelValueToAssignSystem.AutoSize = true;
            labelValueToAssignSystem.Location = new Point(5, 54);
            labelValueToAssignSystem.Name = "labelValueToAssignSystem";
            labelValueToAssignSystem.Size = new Size(45, 15);
            labelValueToAssignSystem.TabIndex = 10;
            labelValueToAssignSystem.Text = "System";
            // 
            // textBoxValueToAssign
            // 
            textBoxValueToAssign.Location = new Point(72, 22);
            textBoxValueToAssign.Name = "textBoxValueToAssign";
            textBoxValueToAssign.Size = new Size(228, 23);
            textBoxValueToAssign.TabIndex = 6;
            // 
            // buttonAssignSelectedValues
            // 
            buttonAssignSelectedValues.Location = new Point(5, 109);
            buttonAssignSelectedValues.Name = "buttonAssignSelectedValues";
            buttonAssignSelectedValues.Size = new Size(295, 23);
            buttonAssignSelectedValues.TabIndex = 8;
            buttonAssignSelectedValues.Text = "Assign";
            buttonAssignSelectedValues.UseVisualStyleBackColor = true;
            // 
            // comboBoxValueToAssignSystem
            // 
            comboBoxValueToAssignSystem.FormattingEnabled = true;
            comboBoxValueToAssignSystem.Location = new Point(72, 51);
            comboBoxValueToAssignSystem.Name = "comboBoxValueToAssignSystem";
            comboBoxValueToAssignSystem.Size = new Size(228, 23);
            comboBoxValueToAssignSystem.TabIndex = 7;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(checkBoxClearMemoryBeforeExecution);
            groupBox1.Controls.Add(checkBoxHighlightAccessedMemory);
            groupBox1.Controls.Add(labelSystem);
            groupBox1.Controls.Add(comboBoxMemoryValueSystemRepresentation);
            groupBox1.Controls.Add(labelEnd);
            groupBox1.Controls.Add(labelStart);
            groupBox1.Controls.Add(textBoxMemorySearchRangeStart);
            groupBox1.Controls.Add(textBoxMemorySearchRangeEnd);
            groupBox1.Controls.Add(buttonSearch);
            groupBox1.Location = new Point(6, 8);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(465, 137);
            groupBox1.TabIndex = 6;
            groupBox1.TabStop = false;
            groupBox1.Text = "Memory Range Search";
            // 
            // checkBoxClearMemoryBeforeExecution
            // 
            checkBoxClearMemoryBeforeExecution.AutoSize = true;
            checkBoxClearMemoryBeforeExecution.Location = new Point(287, 53);
            checkBoxClearMemoryBeforeExecution.Name = "checkBoxClearMemoryBeforeExecution";
            checkBoxClearMemoryBeforeExecution.Size = new Size(145, 19);
            checkBoxClearMemoryBeforeExecution.TabIndex = 4;
            checkBoxClearMemoryBeforeExecution.Text = "Clear before execution";
            checkBoxClearMemoryBeforeExecution.UseVisualStyleBackColor = true;
            // 
            // checkBoxHighlightAccessedMemory
            // 
            checkBoxHighlightAccessedMemory.AutoSize = true;
            checkBoxHighlightAccessedMemory.Location = new Point(287, 24);
            checkBoxHighlightAccessedMemory.Name = "checkBoxHighlightAccessedMemory";
            checkBoxHighlightAccessedMemory.Size = new Size(174, 19);
            checkBoxHighlightAccessedMemory.TabIndex = 3;
            checkBoxHighlightAccessedMemory.Text = "Highlight accessed memory";
            checkBoxHighlightAccessedMemory.UseVisualStyleBackColor = true;
            // 
            // labelSystem
            // 
            labelSystem.AutoSize = true;
            labelSystem.Location = new Point(6, 83);
            labelSystem.Name = "labelSystem";
            labelSystem.Size = new Size(45, 15);
            labelSystem.TabIndex = 7;
            labelSystem.Text = "System";
            // 
            // comboBoxMemoryValueSystemRepresentation
            // 
            comboBoxMemoryValueSystemRepresentation.FormattingEnabled = true;
            comboBoxMemoryValueSystemRepresentation.Location = new Point(73, 80);
            comboBoxMemoryValueSystemRepresentation.Name = "comboBoxMemoryValueSystemRepresentation";
            comboBoxMemoryValueSystemRepresentation.Size = new Size(208, 23);
            comboBoxMemoryValueSystemRepresentation.TabIndex = 2;
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
            textBoxMemorySearchRangeStart.Size = new Size(208, 23);
            textBoxMemorySearchRangeStart.TabIndex = 0;
            // 
            // textBoxMemorySearchRangeEnd
            // 
            textBoxMemorySearchRangeEnd.Location = new Point(73, 51);
            textBoxMemorySearchRangeEnd.Name = "textBoxMemorySearchRangeEnd";
            textBoxMemorySearchRangeEnd.Size = new Size(208, 23);
            textBoxMemorySearchRangeEnd.TabIndex = 1;
            // 
            // buttonSearch
            // 
            buttonSearch.Location = new Point(73, 109);
            buttonSearch.Name = "buttonSearch";
            buttonSearch.Size = new Size(386, 23);
            buttonSearch.TabIndex = 5;
            buttonSearch.Text = "Search";
            buttonSearch.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            dataGridView1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(8, 151);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.Size = new Size(776, 236);
            dataGridView1.TabIndex = 5;
            // 
            // tabPageRegisters
            // 
            tabPageRegisters.Controls.Add(groupBox8);
            tabPageRegisters.Controls.Add(groupBox7);
            tabPageRegisters.Location = new Point(4, 24);
            tabPageRegisters.Name = "tabPageRegisters";
            tabPageRegisters.Size = new Size(792, 394);
            tabPageRegisters.TabIndex = 2;
            tabPageRegisters.Text = "Registers";
            tabPageRegisters.UseVisualStyleBackColor = true;
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
            richTextBoxOutput.Location = new Point(6, 6);
            richTextBoxOutput.Name = "richTextBoxOutput";
            richTextBoxOutput.Size = new Size(780, 381);
            richTextBoxOutput.TabIndex = 0;
            richTextBoxOutput.Text = "";
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(textBox2);
            groupBox3.Controls.Add(label2);
            groupBox3.Controls.Add(label1);
            groupBox3.Controls.Add(textBox1);
            groupBox3.Location = new Point(6, 22);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(145, 78);
            groupBox3.TabIndex = 0;
            groupBox3.TabStop = false;
            groupBox3.Text = "groupBox3";
            // 
            // textBox1
            // 
            textBox1.Location = new Point(36, 16);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(100, 23);
            textBox1.TabIndex = 2;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(6, 19);
            label1.Name = "label1";
            label1.Size = new Size(24, 15);
            label1.TabIndex = 2;
            label1.Text = "AH";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(6, 48);
            label2.Name = "label2";
            label2.Size = new Size(21, 15);
            label2.TabIndex = 2;
            label2.Text = "AL";
            // 
            // textBox2
            // 
            textBox2.Location = new Point(36, 45);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(100, 23);
            textBox2.TabIndex = 2;
            // 
            // groupBox4
            // 
            groupBox4.Controls.Add(textBox3);
            groupBox4.Controls.Add(label3);
            groupBox4.Controls.Add(label4);
            groupBox4.Controls.Add(textBox4);
            groupBox4.Location = new Point(157, 22);
            groupBox4.Name = "groupBox4";
            groupBox4.Size = new Size(145, 78);
            groupBox4.TabIndex = 3;
            groupBox4.TabStop = false;
            groupBox4.Text = "groupBox4";
            // 
            // textBox3
            // 
            textBox3.Location = new Point(36, 45);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(100, 23);
            textBox3.TabIndex = 2;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(6, 48);
            label3.Name = "label3";
            label3.Size = new Size(21, 15);
            label3.TabIndex = 2;
            label3.Text = "AL";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(6, 19);
            label4.Name = "label4";
            label4.Size = new Size(24, 15);
            label4.TabIndex = 2;
            label4.Text = "AH";
            // 
            // textBox4
            // 
            textBox4.Location = new Point(36, 16);
            textBox4.Name = "textBox4";
            textBox4.Size = new Size(100, 23);
            textBox4.TabIndex = 2;
            // 
            // groupBox5
            // 
            groupBox5.Controls.Add(textBox5);
            groupBox5.Controls.Add(label5);
            groupBox5.Controls.Add(label6);
            groupBox5.Controls.Add(textBox6);
            groupBox5.Location = new Point(6, 106);
            groupBox5.Name = "groupBox5";
            groupBox5.Size = new Size(145, 78);
            groupBox5.TabIndex = 3;
            groupBox5.TabStop = false;
            groupBox5.Text = "groupBox5";
            // 
            // textBox5
            // 
            textBox5.Location = new Point(36, 45);
            textBox5.Name = "textBox5";
            textBox5.Size = new Size(100, 23);
            textBox5.TabIndex = 2;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(6, 48);
            label5.Name = "label5";
            label5.Size = new Size(21, 15);
            label5.TabIndex = 2;
            label5.Text = "AL";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(6, 19);
            label6.Name = "label6";
            label6.Size = new Size(24, 15);
            label6.TabIndex = 2;
            label6.Text = "AH";
            // 
            // textBox6
            // 
            textBox6.Location = new Point(36, 16);
            textBox6.Name = "textBox6";
            textBox6.Size = new Size(100, 23);
            textBox6.TabIndex = 2;
            // 
            // groupBox6
            // 
            groupBox6.Controls.Add(textBox7);
            groupBox6.Controls.Add(label7);
            groupBox6.Controls.Add(label8);
            groupBox6.Controls.Add(textBox8);
            groupBox6.Location = new Point(157, 106);
            groupBox6.Name = "groupBox6";
            groupBox6.Size = new Size(145, 78);
            groupBox6.TabIndex = 3;
            groupBox6.TabStop = false;
            groupBox6.Text = "groupBox6";
            // 
            // textBox7
            // 
            textBox7.Location = new Point(36, 45);
            textBox7.Name = "textBox7";
            textBox7.Size = new Size(100, 23);
            textBox7.TabIndex = 2;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(6, 48);
            label7.Name = "label7";
            label7.Size = new Size(21, 15);
            label7.TabIndex = 2;
            label7.Text = "AL";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(6, 19);
            label8.Name = "label8";
            label8.Size = new Size(24, 15);
            label8.TabIndex = 2;
            label8.Text = "AH";
            // 
            // textBox8
            // 
            textBox8.Location = new Point(36, 16);
            textBox8.Name = "textBox8";
            textBox8.Size = new Size(100, 23);
            textBox8.TabIndex = 2;
            // 
            // groupBox7
            // 
            groupBox7.Controls.Add(groupBox3);
            groupBox7.Controls.Add(groupBox6);
            groupBox7.Controls.Add(groupBox5);
            groupBox7.Controls.Add(groupBox4);
            groupBox7.Location = new Point(8, 3);
            groupBox7.Name = "groupBox7";
            groupBox7.Size = new Size(310, 191);
            groupBox7.TabIndex = 4;
            groupBox7.TabStop = false;
            groupBox7.Text = "groupBox7";
            // 
            // groupBox8
            // 
            groupBox8.Controls.Add(groupBox12);
            groupBox8.Controls.Add(groupBox11);
            groupBox8.Controls.Add(groupBox10);
            groupBox8.Controls.Add(groupBox9);
            groupBox8.Location = new Point(324, 3);
            groupBox8.Name = "groupBox8";
            groupBox8.Size = new Size(330, 128);
            groupBox8.TabIndex = 5;
            groupBox8.TabStop = false;
            groupBox8.Text = "groupBox8";
            // 
            // groupBox9
            // 
            groupBox9.Controls.Add(label9);
            groupBox9.Controls.Add(textBox9);
            groupBox9.Location = new Point(6, 22);
            groupBox9.Name = "groupBox9";
            groupBox9.Size = new Size(156, 47);
            groupBox9.TabIndex = 0;
            groupBox9.TabStop = false;
            groupBox9.Text = "groupBox9";
            // 
            // textBox9
            // 
            textBox9.Location = new Point(50, 16);
            textBox9.Name = "textBox9";
            textBox9.Size = new Size(100, 23);
            textBox9.TabIndex = 4;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(6, 19);
            label9.Name = "label9";
            label9.Size = new Size(38, 15);
            label9.TabIndex = 5;
            label9.Text = "label9";
            // 
            // groupBox10
            // 
            groupBox10.Controls.Add(label10);
            groupBox10.Controls.Add(textBox10);
            groupBox10.Location = new Point(6, 75);
            groupBox10.Name = "groupBox10";
            groupBox10.Size = new Size(156, 47);
            groupBox10.TabIndex = 6;
            groupBox10.TabStop = false;
            groupBox10.Text = "groupBox10";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(6, 19);
            label10.Name = "label10";
            label10.Size = new Size(44, 15);
            label10.TabIndex = 5;
            label10.Text = "label10";
            // 
            // textBox10
            // 
            textBox10.Location = new Point(50, 16);
            textBox10.Name = "textBox10";
            textBox10.Size = new Size(100, 23);
            textBox10.TabIndex = 4;
            // 
            // groupBox11
            // 
            groupBox11.Controls.Add(label11);
            groupBox11.Controls.Add(textBox11);
            groupBox11.Location = new Point(168, 22);
            groupBox11.Name = "groupBox11";
            groupBox11.Size = new Size(156, 47);
            groupBox11.TabIndex = 6;
            groupBox11.TabStop = false;
            groupBox11.Text = "groupBox11";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(6, 19);
            label11.Name = "label11";
            label11.Size = new Size(44, 15);
            label11.TabIndex = 5;
            label11.Text = "label11";
            // 
            // textBox11
            // 
            textBox11.Location = new Point(50, 16);
            textBox11.Name = "textBox11";
            textBox11.Size = new Size(100, 23);
            textBox11.TabIndex = 4;
            // 
            // groupBox12
            // 
            groupBox12.Controls.Add(label12);
            groupBox12.Controls.Add(textBox12);
            groupBox12.Location = new Point(168, 75);
            groupBox12.Name = "groupBox12";
            groupBox12.Size = new Size(156, 47);
            groupBox12.TabIndex = 7;
            groupBox12.TabStop = false;
            groupBox12.Text = "groupBox12";
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Location = new Point(6, 19);
            label12.Name = "label12";
            label12.Size = new Size(44, 15);
            label12.TabIndex = 5;
            label12.Text = "label12";
            // 
            // textBox12
            // 
            textBox12.Location = new Point(50, 16);
            textBox12.Name = "textBox12";
            textBox12.Size = new Size(100, 23);
            textBox12.TabIndex = 4;
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
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            mainTabPageControl.ResumeLayout(false);
            tabPageAssemblyCode.ResumeLayout(false);
            tabPageMemory.ResumeLayout(false);
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            tabPageRegisters.ResumeLayout(false);
            tabPageOutput.ResumeLayout(false);
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            groupBox4.ResumeLayout(false);
            groupBox4.PerformLayout();
            groupBox5.ResumeLayout(false);
            groupBox5.PerformLayout();
            groupBox6.ResumeLayout(false);
            groupBox6.PerformLayout();
            groupBox7.ResumeLayout(false);
            groupBox8.ResumeLayout(false);
            groupBox9.ResumeLayout(false);
            groupBox9.PerformLayout();
            groupBox10.ResumeLayout(false);
            groupBox10.PerformLayout();
            groupBox11.ResumeLayout(false);
            groupBox11.PerformLayout();
            groupBox12.ResumeLayout(false);
            groupBox12.PerformLayout();
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
        private DataGridView dataGridView1;
        private Button buttonAssignSelectedValues;
        private TextBox textBoxValueToAssign;
        private Button buttonSearch;
        private TextBox textBoxMemorySearchRangeEnd;
        private TextBox textBoxMemorySearchRangeStart;
        private GroupBox groupBox1;
        private Label labelEnd;
        private Label labelStart;
        private Label labelSystem;
        private ComboBox comboBoxMemoryValueSystemRepresentation;
        private Label labelValueToAssign;
        private CheckBox checkBoxHighlightAccessedMemory;
        private Label labelValueToAssignSystem;
        private ComboBox comboBoxValueToAssignSystem;
        private GroupBox groupBox2;
        private RichTextBox richTextBoxOutput;
        private CheckBox checkBoxClearMemoryBeforeExecution;
        private ToolStripMenuItem starterToolStripMenuItem;
        private ToolStripMenuItem terminalToolStripMenuItem;
        private ToolStripMenuItem openToolStripMenuItem;
        private ToolStripMenuItem saveToolStripMenuItem;
        private ToolStripMenuItem saveAsToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripMenuItem exitToolStripMenuItem;
        private ToolStripMenuItem increaseFontSizeToolStripMenuItem;
        private ToolStripMenuItem decreaseFontSizeToolStripMenuItem;
        private ToolStripMenuItem changeFontToolStripMenuItem;
        private ToolStripMenuItem changeFontColourToolStripMenuItem;
        private ToolStripMenuItem executeToolStripMenuItem;
        private ToolStripMenuItem stepbystepToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator1;
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
        private GroupBox groupBox8;
        private GroupBox groupBox9;
        private GroupBox groupBox10;
        private Label label10;
        private TextBox textBox10;
        private Label label9;
        private TextBox textBox9;
        private GroupBox groupBox7;
        private GroupBox groupBox3;
        private TextBox textBox2;
        private Label label2;
        private Label label1;
        private TextBox textBox1;
        private GroupBox groupBox6;
        private TextBox textBox7;
        private Label label7;
        private Label label8;
        private TextBox textBox8;
        private GroupBox groupBox5;
        private TextBox textBox5;
        private Label label5;
        private Label label6;
        private TextBox textBox6;
        private GroupBox groupBox4;
        private TextBox textBox3;
        private Label label3;
        private Label label4;
        private TextBox textBox4;
        private GroupBox groupBox12;
        private Label label12;
        private TextBox textBox12;
        private GroupBox groupBox11;
        private Label label11;
        private TextBox textBox11;
    }
}
