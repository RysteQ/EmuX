using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
