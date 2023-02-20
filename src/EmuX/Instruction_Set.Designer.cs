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
            this.WebViewInstructionSet = new Microsoft.Web.WebView2.WinForms.WebView2();
            ((System.ComponentModel.ISupportInitialize)(this.WebViewInstructionSet)).BeginInit();
            this.SuspendLayout();
            // 
            // WebViewInstructionSet
            // 
            this.WebViewInstructionSet.AllowExternalDrop = true;
            this.WebViewInstructionSet.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.WebViewInstructionSet.CreationProperties = null;
            this.WebViewInstructionSet.DefaultBackgroundColor = System.Drawing.Color.White;
            this.WebViewInstructionSet.Location = new System.Drawing.Point(-1, -2);
            this.WebViewInstructionSet.Name = "WebViewInstructionSet";
            this.WebViewInstructionSet.Size = new System.Drawing.Size(804, 453);
            this.WebViewInstructionSet.TabIndex = 0;
            this.WebViewInstructionSet.ZoomFactor = 1D;
            // 
            // Instruction_Set
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.WebViewInstructionSet);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Instruction_Set";
            this.Text = "Instruction_Set";
            this.Load += new System.EventHandler(this.Instruction_Set_Load);
            ((System.ComponentModel.ISupportInitialize)(this.WebViewInstructionSet)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Web.WebView2.WinForms.WebView2 WebViewInstructionSet;
    }
}