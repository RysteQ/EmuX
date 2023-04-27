namespace EmuX.src.Views
{
    partial class Output_Form
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
            OutputRichTextbox = new RichTextBox();
            SuspendLayout();
            // 
            // OutputRichTextbox
            // 
            OutputRichTextbox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            OutputRichTextbox.Enabled = false;
            OutputRichTextbox.Location = new Point(0, 0);
            OutputRichTextbox.Name = "OutputRichTextbox";
            OutputRichTextbox.Size = new Size(400, 400);
            OutputRichTextbox.TabIndex = 0;
            OutputRichTextbox.Text = "";
            // 
            // Output_Form
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(399, 399);
            Controls.Add(OutputRichTextbox);
            FormBorderStyle = FormBorderStyle.SizableToolWindow;
            Name = "Output_Form";
            Text = "Output_Form";
            TopMost = true;
            FormClosing += Output_Form_FormClosing;
            Load += Output_Form_Load;
            ResumeLayout(false);
        }

        #endregion

        private RichTextBox OutputRichTextbox;
    }
}