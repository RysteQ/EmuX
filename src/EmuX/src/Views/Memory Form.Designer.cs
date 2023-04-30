namespace EmuX
{
    partial class MemoryForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            groupBox1 = new GroupBox();
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
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)DataGridViewMemory).BeginInit();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(ButtonClearMemoryTable);
            groupBox1.Controls.Add(LabelValue);
            groupBox1.Controls.Add(labelDataRepresentation);
            groupBox1.Controls.Add(LabelMemoryRange);
            groupBox1.Controls.Add(ButtonSetMemoryValue);
            groupBox1.Controls.Add(ButtonSearchMemoryRange);
            groupBox1.Controls.Add(TextBoxMemoryValue);
            groupBox1.Controls.Add(TextBoxMemoryRangeEnd);
            groupBox1.Controls.Add(TextBoxMemoryRangeStart);
            groupBox1.Controls.Add(ComboBoxMemoryRepresentation);
            groupBox1.Location = new Point(12, 5);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(576, 115);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "groupBox1";
            // 
            // ButtonClearMemoryTable
            // 
            ButtonClearMemoryTable.Anchor = AnchorStyles.Top;
            ButtonClearMemoryTable.Location = new Point(339, 50);
            ButtonClearMemoryTable.Name = "ButtonClearMemoryTable";
            ButtonClearMemoryTable.Size = new Size(227, 23);
            ButtonClearMemoryTable.TabIndex = 19;
            ButtonClearMemoryTable.Text = "Clear";
            ButtonClearMemoryTable.UseVisualStyleBackColor = true;
            ButtonClearMemoryTable.Click += ButtonClearMemoryTable_Click;
            // 
            // LabelValue
            // 
            LabelValue.AutoSize = true;
            LabelValue.Location = new Point(11, 88);
            LabelValue.Name = "LabelValue";
            LabelValue.Size = new Size(35, 15);
            LabelValue.TabIndex = 18;
            LabelValue.Text = "Value";
            // 
            // labelDataRepresentation
            // 
            labelDataRepresentation.AutoSize = true;
            labelDataRepresentation.Location = new Point(11, 58);
            labelDataRepresentation.Name = "labelDataRepresentation";
            labelDataRepresentation.Size = new Size(113, 15);
            labelDataRepresentation.TabIndex = 17;
            labelDataRepresentation.Text = "Data Representation";
            // 
            // LabelMemoryRange
            // 
            LabelMemoryRange.AutoSize = true;
            LabelMemoryRange.Location = new Point(11, 29);
            LabelMemoryRange.Name = "LabelMemoryRange";
            LabelMemoryRange.Size = new Size(88, 15);
            LabelMemoryRange.TabIndex = 16;
            LabelMemoryRange.Text = "Memory Range";
            // 
            // ButtonSetMemoryValue
            // 
            ButtonSetMemoryValue.Anchor = AnchorStyles.Top;
            ButtonSetMemoryValue.Location = new Point(339, 80);
            ButtonSetMemoryValue.Name = "ButtonSetMemoryValue";
            ButtonSetMemoryValue.Size = new Size(227, 23);
            ButtonSetMemoryValue.TabIndex = 15;
            ButtonSetMemoryValue.Text = "Set";
            ButtonSetMemoryValue.UseVisualStyleBackColor = true;
            ButtonSetMemoryValue.Click += ButtonSetMemoryValue_Click;
            // 
            // ButtonSearchMemoryRange
            // 
            ButtonSearchMemoryRange.Anchor = AnchorStyles.Top;
            ButtonSearchMemoryRange.Location = new Point(339, 22);
            ButtonSearchMemoryRange.Name = "ButtonSearchMemoryRange";
            ButtonSearchMemoryRange.Size = new Size(227, 23);
            ButtonSearchMemoryRange.TabIndex = 14;
            ButtonSearchMemoryRange.Text = "Search";
            ButtonSearchMemoryRange.UseVisualStyleBackColor = true;
            ButtonSearchMemoryRange.Click += ButtonSearchMemoryRange_Click;
            // 
            // TextBoxMemoryValue
            // 
            TextBoxMemoryValue.Anchor = AnchorStyles.Top;
            TextBoxMemoryValue.Location = new Point(131, 80);
            TextBoxMemoryValue.Name = "TextBoxMemoryValue";
            TextBoxMemoryValue.Size = new Size(202, 23);
            TextBoxMemoryValue.TabIndex = 13;
            // 
            // TextBoxMemoryRangeEnd
            // 
            TextBoxMemoryRangeEnd.Anchor = AnchorStyles.Top;
            TextBoxMemoryRangeEnd.Location = new Point(233, 22);
            TextBoxMemoryRangeEnd.Name = "TextBoxMemoryRangeEnd";
            TextBoxMemoryRangeEnd.Size = new Size(100, 23);
            TextBoxMemoryRangeEnd.TabIndex = 12;
            // 
            // TextBoxMemoryRangeStart
            // 
            TextBoxMemoryRangeStart.Anchor = AnchorStyles.Top;
            TextBoxMemoryRangeStart.Location = new Point(131, 22);
            TextBoxMemoryRangeStart.Name = "TextBoxMemoryRangeStart";
            TextBoxMemoryRangeStart.Size = new Size(100, 23);
            TextBoxMemoryRangeStart.TabIndex = 11;
            // 
            // ComboBoxMemoryRepresentation
            // 
            ComboBoxMemoryRepresentation.Anchor = AnchorStyles.Top;
            ComboBoxMemoryRepresentation.FormattingEnabled = true;
            ComboBoxMemoryRepresentation.Items.AddRange(new object[] { "Decimal", "Binary", "Hexadecimal" });
            ComboBoxMemoryRepresentation.Location = new Point(131, 51);
            ComboBoxMemoryRepresentation.Name = "ComboBoxMemoryRepresentation";
            ComboBoxMemoryRepresentation.Size = new Size(202, 23);
            ComboBoxMemoryRepresentation.TabIndex = 10;
            // 
            // DataGridViewMemory
            // 
            DataGridViewMemory.AllowUserToAddRows = false;
            DataGridViewMemory.AllowUserToDeleteRows = false;
            DataGridViewMemory.AllowUserToResizeColumns = false;
            DataGridViewMemory.AllowUserToResizeRows = false;
            DataGridViewMemory.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            DataGridViewMemory.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            DataGridViewMemory.Enabled = false;
            DataGridViewMemory.Location = new Point(12, 126);
            DataGridViewMemory.Name = "DataGridViewMemory";
            DataGridViewMemory.ReadOnly = true;
            DataGridViewMemory.RowTemplate.Height = 25;
            DataGridViewMemory.Size = new Size(576, 150);
            DataGridViewMemory.TabIndex = 1;
            // 
            // MemoryForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(597, 286);
            Controls.Add(DataGridViewMemory);
            Controls.Add(groupBox1);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "MemoryForm";
            Text = "Memory Form";
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)DataGridViewMemory).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox1;
        private DataGridView DataGridViewMemory;
        private Button ButtonClearMemoryTable;
        private Label LabelValue;
        private Label labelDataRepresentation;
        private Label LabelMemoryRange;
        private Button ButtonSetMemoryValue;
        private Button ButtonSearchMemoryRange;
        private TextBox TextBoxMemoryValue;
        private TextBox TextBoxMemoryRangeEnd;
        private TextBox TextBoxMemoryRangeStart;
        private ComboBox ComboBoxMemoryRepresentation;
    }
}