namespace EmuX
{
    partial class Instruction_Set
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
            WebViewInstructionSet = new Microsoft.Web.WebView2.WinForms.WebView2();
            ((System.ComponentModel.ISupportInitialize)WebViewInstructionSet).BeginInit();
            SuspendLayout();
            // 
            // WebViewInstructionSet
            // 
            WebViewInstructionSet.AllowExternalDrop = true;
            WebViewInstructionSet.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            WebViewInstructionSet.CreationProperties = null;
            WebViewInstructionSet.DefaultBackgroundColor = Color.White;
            WebViewInstructionSet.Location = new Point(-1, -2);
            WebViewInstructionSet.Name = "WebViewInstructionSet";
            WebViewInstructionSet.Size = new Size(804, 453);
            WebViewInstructionSet.TabIndex = 0;
            WebViewInstructionSet.ZoomFactor = 1D;
            // 
            // Instruction_Set
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(WebViewInstructionSet);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "Instruction_Set";
            Text = "Instruction Set";
            Load += Instruction_Set_Load;
            ((System.ComponentModel.ISupportInitialize)WebViewInstructionSet).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Microsoft.Web.WebView2.WinForms.WebView2 WebViewInstructionSet;
    }
}