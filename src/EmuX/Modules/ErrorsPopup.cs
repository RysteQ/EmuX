using System.Media;

namespace EmuX_Nano.Modules;

public partial class ErrorsPopup : Form
{
    public ErrorsPopup(string[] errors)
    {
        InitializeComponent();

        InitErrors(errors);
    }

    private void InitErrors(string[] errors)
    {
        foreach (string error in errors)
        {
            richTextBoxErrors.Text += error + "\n";
        }
    }

    private void ErrorsPopup_Load(object sender, EventArgs e)
    {
        SystemSounds.Beep.Play();
    }
}
