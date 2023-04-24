namespace EmuX
{
    partial class Converter_Form
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
            this.ComboBoxConvertFrom = new System.Windows.Forms.ComboBox();
            this.GroupBoxConvertFrom = new System.Windows.Forms.GroupBox();
            this.TextBoxConvertFrom = new System.Windows.Forms.TextBox();
            this.GroupBoxConvertTo = new System.Windows.Forms.GroupBox();
            this.ComboBoxConvertTo = new System.Windows.Forms.ComboBox();
            this.ButtonConvert = new System.Windows.Forms.Button();
            this.GroupBoxResult = new System.Windows.Forms.GroupBox();
            this.LabelResult = new System.Windows.Forms.Label();
            this.GroupBoxConvertFrom.SuspendLayout();
            this.GroupBoxConvertTo.SuspendLayout();
            this.GroupBoxResult.SuspendLayout();
            this.SuspendLayout();
            // 
            // ComboBoxConvertFrom
            // 
            this.ComboBoxConvertFrom.FormattingEnabled = true;
            this.ComboBoxConvertFrom.Items.AddRange(new object[] {
            "Decimal",
            "Binary",
            "Hexadecimal"});
            this.ComboBoxConvertFrom.Location = new System.Drawing.Point(6, 22);
            this.ComboBoxConvertFrom.Name = "ComboBoxConvertFrom";
            this.ComboBoxConvertFrom.Size = new System.Drawing.Size(121, 23);
            this.ComboBoxConvertFrom.TabIndex = 0;
            // 
            // GroupBoxConvertFrom
            // 
            this.GroupBoxConvertFrom.Controls.Add(this.ComboBoxConvertFrom);
            this.GroupBoxConvertFrom.Location = new System.Drawing.Point(12, 12);
            this.GroupBoxConvertFrom.Name = "GroupBoxConvertFrom";
            this.GroupBoxConvertFrom.Size = new System.Drawing.Size(137, 56);
            this.GroupBoxConvertFrom.TabIndex = 1;
            this.GroupBoxConvertFrom.TabStop = false;
            this.GroupBoxConvertFrom.Text = "Convert From";
            // 
            // TextBoxConvertFrom
            // 
            this.TextBoxConvertFrom.Location = new System.Drawing.Point(12, 71);
            this.TextBoxConvertFrom.Name = "TextBoxConvertFrom";
            this.TextBoxConvertFrom.Size = new System.Drawing.Size(280, 23);
            this.TextBoxConvertFrom.TabIndex = 5;
            // 
            // GroupBoxConvertTo
            // 
            this.GroupBoxConvertTo.Controls.Add(this.ComboBoxConvertTo);
            this.GroupBoxConvertTo.Location = new System.Drawing.Point(155, 12);
            this.GroupBoxConvertTo.Name = "GroupBoxConvertTo";
            this.GroupBoxConvertTo.Size = new System.Drawing.Size(137, 56);
            this.GroupBoxConvertTo.TabIndex = 2;
            this.GroupBoxConvertTo.TabStop = false;
            this.GroupBoxConvertTo.Text = "Convert To";
            // 
            // ComboBoxConvertTo
            // 
            this.ComboBoxConvertTo.FormattingEnabled = true;
            this.ComboBoxConvertTo.Items.AddRange(new object[] {
            "Decimal",
            "Binary",
            "Hexadecimal"});
            this.ComboBoxConvertTo.Location = new System.Drawing.Point(6, 22);
            this.ComboBoxConvertTo.Name = "ComboBoxConvertTo";
            this.ComboBoxConvertTo.Size = new System.Drawing.Size(121, 23);
            this.ComboBoxConvertTo.TabIndex = 3;
            // 
            // ButtonConvert
            // 
            this.ButtonConvert.Location = new System.Drawing.Point(298, 71);
            this.ButtonConvert.Name = "ButtonConvert";
            this.ButtonConvert.Size = new System.Drawing.Size(117, 23);
            this.ButtonConvert.TabIndex = 3;
            this.ButtonConvert.Text = "Convert";
            this.ButtonConvert.UseVisualStyleBackColor = true;
            this.ButtonConvert.Click += new System.EventHandler(this.ButtonConvert_Click);
            // 
            // GroupBoxResult
            // 
            this.GroupBoxResult.Controls.Add(this.LabelResult);
            this.GroupBoxResult.Location = new System.Drawing.Point(298, 12);
            this.GroupBoxResult.Name = "GroupBoxResult";
            this.GroupBoxResult.Size = new System.Drawing.Size(117, 53);
            this.GroupBoxResult.TabIndex = 4;
            this.GroupBoxResult.TabStop = false;
            this.GroupBoxResult.Text = "Result";
            // 
            // LabelResult
            // 
            this.LabelResult.AutoSize = true;
            this.LabelResult.Location = new System.Drawing.Point(6, 30);
            this.LabelResult.Name = "LabelResult";
            this.LabelResult.Size = new System.Drawing.Size(39, 15);
            this.LabelResult.TabIndex = 0;
            this.LabelResult.Text = "Result";
            // 
            // Converter_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(425, 103);
            this.Controls.Add(this.TextBoxConvertFrom);
            this.Controls.Add(this.GroupBoxResult);
            this.Controls.Add(this.ButtonConvert);
            this.Controls.Add(this.GroupBoxConvertTo);
            this.Controls.Add(this.GroupBoxConvertFrom);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Converter_Form";
            this.Text = "Converter Form";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.Converter_Form_Load);
            this.GroupBoxConvertFrom.ResumeLayout(false);
            this.GroupBoxConvertTo.ResumeLayout(false);
            this.GroupBoxResult.ResumeLayout(false);
            this.GroupBoxResult.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ComboBox ComboBoxConvertFrom;
        private GroupBox GroupBoxConvertFrom;
        private GroupBox GroupBoxConvertTo;
        private ComboBox ComboBoxConvertTo;
        private Button ButtonConvert;
        private GroupBox GroupBoxResult;
        private Label LabelResult;
        private TextBox TextBoxConvertFrom;
    }
}