namespace EmuX;

public partial class MainForm : Form
{
    public MainForm()
    {
        InitializeComponent();
    }

    private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
    {
        MessageBox.Show("Version: 2.0.0", "EmuX", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }
}