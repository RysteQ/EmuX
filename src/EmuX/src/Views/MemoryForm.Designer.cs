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
            this.DataGridViewMemory = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridViewMemory)).BeginInit();
            this.SuspendLayout();
            // 
            // DataGridViewMemory
            // 
            this.DataGridViewMemory.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DataGridViewMemory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataGridViewMemory.Location = new System.Drawing.Point(12, 12);
            this.DataGridViewMemory.Name = "DataGridViewMemory";
            this.DataGridViewMemory.RowTemplate.Height = 25;
            this.DataGridViewMemory.Size = new System.Drawing.Size(776, 426);
            this.DataGridViewMemory.TabIndex = 0;
            // 
            // MemoryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.DataGridViewMemory);
            this.Name = "MemoryForm";
            this.Text = "MemoryForm";
            this.Load += new System.EventHandler(this.MemoryForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DataGridViewMemory)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DataGridView DataGridViewMemory;
    }
}