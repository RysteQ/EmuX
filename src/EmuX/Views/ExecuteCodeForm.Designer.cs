namespace EmuX_Nano.Views
{
    partial class ExecuteCodeForm
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
            listBoxInstructions = new ListBox();
            buttonExecute = new Button();
            buttonStep = new Button();
            buttonUndo = new Button();
            buttonRedo = new Button();
            buttonReset = new Button();
            pictureBoxVideoOutput = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pictureBoxVideoOutput).BeginInit();
            SuspendLayout();
            // 
            // listBoxInstructions
            // 
            listBoxInstructions.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            listBoxInstructions.Enabled = false;
            listBoxInstructions.FormattingEnabled = true;
            listBoxInstructions.Location = new Point(12, 12);
            listBoxInstructions.Name = "listBoxInstructions";
            listBoxInstructions.Size = new Size(386, 229);
            listBoxInstructions.TabIndex = 0;
            listBoxInstructions.SelectedIndexChanged += listBoxInstructions_SelectedIndexChanged;
            // 
            // buttonExecute
            // 
            buttonExecute.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            buttonExecute.Location = new Point(404, 12);
            buttonExecute.Name = "buttonExecute";
            buttonExecute.Size = new Size(89, 42);
            buttonExecute.TabIndex = 1;
            buttonExecute.Text = "Execute";
            buttonExecute.UseVisualStyleBackColor = true;
            buttonExecute.Click += buttonExecute_Click;
            // 
            // buttonStep
            // 
            buttonStep.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            buttonStep.Location = new Point(403, 60);
            buttonStep.Name = "buttonStep";
            buttonStep.Size = new Size(90, 42);
            buttonStep.TabIndex = 2;
            buttonStep.Text = "Step";
            buttonStep.UseVisualStyleBackColor = true;
            buttonStep.Click += buttonStep_Click;
            // 
            // buttonUndo
            // 
            buttonUndo.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            buttonUndo.Location = new Point(404, 108);
            buttonUndo.Name = "buttonUndo";
            buttonUndo.Size = new Size(89, 42);
            buttonUndo.TabIndex = 3;
            buttonUndo.Text = "Undo";
            buttonUndo.UseVisualStyleBackColor = true;
            buttonUndo.Click += buttonUndo_Click;
            // 
            // buttonRedo
            // 
            buttonRedo.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            buttonRedo.Location = new Point(404, 156);
            buttonRedo.Name = "buttonRedo";
            buttonRedo.Size = new Size(89, 42);
            buttonRedo.TabIndex = 4;
            buttonRedo.Text = "Redo";
            buttonRedo.UseVisualStyleBackColor = true;
            buttonRedo.Click += buttonRedo_Click;
            // 
            // buttonReset
            // 
            buttonReset.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            buttonReset.Location = new Point(404, 204);
            buttonReset.Name = "buttonReset";
            buttonReset.Size = new Size(89, 42);
            buttonReset.TabIndex = 5;
            buttonReset.Text = "Reset";
            buttonReset.UseVisualStyleBackColor = true;
            buttonReset.Click += buttonReset_Click;
            // 
            // pictureBoxVideoOutput
            // 
            pictureBoxVideoOutput.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            pictureBoxVideoOutput.Location = new Point(12, 252);
            pictureBoxVideoOutput.Name = "pictureBoxVideoOutput";
            pictureBoxVideoOutput.Size = new Size(481, 334);
            pictureBoxVideoOutput.TabIndex = 6;
            pictureBoxVideoOutput.TabStop = false;
            // 
            // ExecuteCodeForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(507, 598);
            Controls.Add(pictureBoxVideoOutput);
            Controls.Add(buttonReset);
            Controls.Add(buttonRedo);
            Controls.Add(buttonUndo);
            Controls.Add(buttonStep);
            Controls.Add(buttonExecute);
            Controls.Add(listBoxInstructions);
            MinimumSize = new Size(523, 637);
            Name = "ExecuteCodeForm";
            Text = "Execute";
            ((System.ComponentModel.ISupportInitialize)pictureBoxVideoOutput).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private ListBox listBoxInstructions;
        private Button buttonExecute;
        private Button buttonStep;
        private Button buttonUndo;
        private Button buttonRedo;
        private Button buttonReset;
        private PictureBox pictureBoxVideoOutput;
    }
}