namespace EmuX_Nano.Modules
{
    partial class PopupInput
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
            labelQuestion = new Label();
            textBoxUserInput = new TextBox();
            SuspendLayout();
            // 
            // labelQuestion
            // 
            labelQuestion.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            labelQuestion.AutoSize = true;
            labelQuestion.Location = new Point(12, 9);
            labelQuestion.Name = "labelQuestion";
            labelQuestion.Size = new Size(32, 15);
            labelQuestion.TabIndex = 0;
            labelQuestion.Text = "label";
            // 
            // textBoxUserInput
            // 
            textBoxUserInput.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            textBoxUserInput.Location = new Point(12, 33);
            textBoxUserInput.Name = "textBoxUserInput";
            textBoxUserInput.Size = new Size(157, 23);
            textBoxUserInput.TabIndex = 1;
            textBoxUserInput.KeyPress += textBoxUserInput_KeyPress;
            // 
            // PopupInput
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(177, 65);
            Controls.Add(textBoxUserInput);
            Controls.Add(labelQuestion);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "PopupInput";
            TopMost = true;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label labelQuestion;
        private TextBox textBoxUserInput;
    }
}