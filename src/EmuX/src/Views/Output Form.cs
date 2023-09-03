namespace EmuX.src.Views;

public partial class Output_Form : Form
{
    public Output_Form()
    {
        InitializeComponent();
    }

    public bool IsOpen() => this.is_open;

    private void Output_Form_Load(object sender, EventArgs e) => this.is_open = true;

    private void Output_Form_FormClosing(object sender, FormClosingEventArgs e) => this.is_open = false;

    public void UpdateOutput(string to_append) => this.OutputRichTextbox.Text += to_append + "\n";

    private bool is_open = false;
}
