namespace EmuX
{
    partial class ASCII_Table
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
            this.WebViewASCIITable = new Microsoft.Web.WebView2.WinForms.WebView2();
            ((System.ComponentModel.ISupportInitialize)(this.WebViewASCIITable)).BeginInit();
            this.SuspendLayout();
            // 
            // WebViewASCIITable
            // 
            this.WebViewASCIITable.AllowExternalDrop = true;
            this.WebViewASCIITable.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.WebViewASCIITable.CreationProperties = null;
            this.WebViewASCIITable.DefaultBackgroundColor = System.Drawing.Color.White;
            this.WebViewASCIITable.Location = new System.Drawing.Point(0, 0);
            this.WebViewASCIITable.Name = "WebViewASCIITable";
            this.WebViewASCIITable.Size = new System.Drawing.Size(800, 450);
            this.WebViewASCIITable.TabIndex = 0;
            this.WebViewASCIITable.ZoomFactor = 1D;
            // 
            // ASCII_Table
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.WebViewASCIITable);
            this.Name = "ASCII_Table";
            this.Text = "ASCII Table";
            this.Load += new System.EventHandler(this.ASCII_Table_Load);
            ((System.ComponentModel.ISupportInitialize)(this.WebViewASCIITable)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Web.WebView2.WinForms.WebView2 WebViewASCIITable;
    }
}