namespace EmuX
{
    partial class VideoForm
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
            this.PictureBoxVideoOutput = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBoxVideoOutput)).BeginInit();
            this.SuspendLayout();
            // 
            // PictureBoxVideoOutput
            // 
            this.PictureBoxVideoOutput.Location = new System.Drawing.Point(0, 0);
            this.PictureBoxVideoOutput.Name = "PictureBoxVideoOutput";
            this.PictureBoxVideoOutput.Size = new System.Drawing.Size(200, 100);
            this.PictureBoxVideoOutput.TabIndex = 0;
            this.PictureBoxVideoOutput.TabStop = false;
            // 
            // VideoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(200, 101);
            this.Controls.Add(this.PictureBoxVideoOutput);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "VideoForm";
            this.Text = "Video Form";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.VideoForm_FormClosing);
            this.Load += new System.EventHandler(this.VideoForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBoxVideoOutput)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private PictureBox PictureBoxVideoOutput;
    }
}