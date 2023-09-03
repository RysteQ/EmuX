namespace EmuX.src.Views
{
    partial class MemoryDumpForm
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
            ComboBoxMemoryDumpMode = new ComboBox();
            CheckBoxMemoryDump = new CheckBox();
            CheckBoxStackDump = new CheckBox();
            ButtonApplyMDumpPreferences = new Button();
            SuspendLayout();
            // 
            // ComboBoxMemoryDumpMode
            // 
            ComboBoxMemoryDumpMode.FormattingEnabled = true;
            ComboBoxMemoryDumpMode.Items.AddRange(new object[] { "Total", "Incremental" });
            ComboBoxMemoryDumpMode.Location = new Point(12, 12);
            ComboBoxMemoryDumpMode.Name = "ComboBoxMemoryDumpMode";
            ComboBoxMemoryDumpMode.Size = new Size(121, 23);
            ComboBoxMemoryDumpMode.TabIndex = 0;
            ComboBoxMemoryDumpMode.SelectedIndexChanged += MemoryDumpModeChanged;
            // 
            // CheckBoxMemoryDump
            // 
            CheckBoxMemoryDump.AutoSize = true;
            CheckBoxMemoryDump.Location = new Point(12, 41);
            CheckBoxMemoryDump.Name = "CheckBoxMemoryDump";
            CheckBoxMemoryDump.Size = new Size(71, 19);
            CheckBoxMemoryDump.TabIndex = 1;
            CheckBoxMemoryDump.Text = "Memory";
            CheckBoxMemoryDump.UseVisualStyleBackColor = true;
            // 
            // CheckBoxStackDump
            // 
            CheckBoxStackDump.AutoSize = true;
            CheckBoxStackDump.Location = new Point(12, 66);
            CheckBoxStackDump.Name = "CheckBoxStackDump";
            CheckBoxStackDump.Size = new Size(54, 19);
            CheckBoxStackDump.TabIndex = 2;
            CheckBoxStackDump.Text = "Stack";
            CheckBoxStackDump.UseVisualStyleBackColor = true;
            // 
            // ButtonApplyMDumpPreferences
            // 
            ButtonApplyMDumpPreferences.Location = new Point(12, 91);
            ButtonApplyMDumpPreferences.Name = "ButtonApplyMDumpPreferences";
            ButtonApplyMDumpPreferences.Size = new Size(123, 23);
            ButtonApplyMDumpPreferences.TabIndex = 3;
            ButtonApplyMDumpPreferences.Text = "Apply";
            ButtonApplyMDumpPreferences.UseVisualStyleBackColor = true;
            ButtonApplyMDumpPreferences.Click += ButtonApplyMDumpPreferences_Click;
            // 
            // MemoryDumpForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(147, 120);
            Controls.Add(ButtonApplyMDumpPreferences);
            Controls.Add(CheckBoxStackDump);
            Controls.Add(CheckBoxMemoryDump);
            Controls.Add(ComboBoxMemoryDumpMode);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "MemoryDumpForm";
            Text = "MemoryDumpForm";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox ComboBoxMemoryDumpMode;
        private CheckBox CheckBoxMemoryDump;
        private CheckBox CheckBoxStackDump;
        private Button ButtonApplyMDumpPreferences;
    }
}