using EmuX.src.Enums.Application.MemoryDump;
using EmuX.src.Models.Application;

namespace EmuX.src.Views;

public partial class MemoryDumpForm : Form
{
    public MemoryDumpForm(MDump memory_dump_preferences)
    {
        InitializeComponent();

        this.ComboBoxMemoryDumpMode.SelectedIndex = 0;
        this.MemoryDumpMode = MemoryDumpModes.Total;

        this.memory_dump_preferences = memory_dump_preferences;
    }

    private void ButtonApplyMDumpPreferences_Click(object sender, EventArgs e)
    {
        this.memory_dump_preferences.MemoryDumpMode = this.MemoryDumpMode;
        this.memory_dump_preferences.MemoryDumpEnabled = CheckBoxMemoryDump.Checked;
        this.memory_dump_preferences.StackDumpEnabled = CheckBoxStackDump.Checked;
    }

    private void MemoryDumpModeChanged(object sender, EventArgs e)
    {
        switch (ComboBoxMemoryDumpMode.SelectedIndex)
        {
            case 0: this.MemoryDumpMode = MemoryDumpModes.Total; break;
            case 1: this.MemoryDumpMode = MemoryDumpModes.Incremental; break;
        }
    }

    private MemoryDumpModes MemoryDumpMode;
    private MDump memory_dump_preferences;
}