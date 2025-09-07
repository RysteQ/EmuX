using System.ComponentModel;

namespace EmuX_Nano.Modules;

public partial class PopupInput : Form
{
    public PopupInput()
    {
        InitializeComponent();
    }

    private void textBoxUserInput_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar != ENTER_ASCII_VALUE)
        {
            return;
        }

        if (!string.IsNullOrEmpty(textBoxUserInput.Text))
        {
            _userInput = textBoxUserInput.Text;
            this.Close();
        }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public string Question { get => labelQuestion.Text; set => labelQuestion.Text = value; }
    public string? UserInput { get => _userInput; }

    private string? _userInput = null;

    private const byte ENTER_ASCII_VALUE = 13;
}