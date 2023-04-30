using EmuX.src.Services.Emulator;

namespace EmuX
{
    public partial class Registers_Form : Form
    {
        public Registers_Form(ref VirtualSystem virtual_system, ref Emulator emulator)
        {
            InitializeComponent();

            this.virtual_system = virtual_system;
            this.emulator = emulator;
        }

        private void Registers_Form_Load(object sender, EventArgs e)
        {
            this.is_open = true;

            UpdateGUI();
        }

        public void SetVirtualSystem(VirtualSystem virtual_system)
        {
            this.virtual_system = virtual_system;

            UpdateGUI();
        }

        public bool IsOpen()
        {
            return this.is_open;
        }

        private void UpdateGUI()
        {
            ulong[] values_to_display = virtual_system.GetAllRegisterValues();

            TextBox[] textbox_to_update = new TextBox[]
            {
                TextBoxRAX,
                TextBoxRBX,
                TextBoxRCX,
                TextBoxRDX,
                TextBoxRSI,
                TextBoxRDI,
                TextBoxRSP,
                TextBoxRBP,
                TextBoxRIP,
                TextBoxR8,
                TextBoxR9,
                TextBoxR10,
                TextBoxR11,
                TextBoxR12,
                TextBoxR13,
                TextBoxR14,
                TextBoxR15
            };

            for (int i = 0; i < textbox_to_update.Length; i++)
            {
                textbox_to_update[i].Text = values_to_display[i].ToString();
                textbox_to_update[i].BackColor = Color.White;
            }

            uint EFLAGS = this.virtual_system.EFLAGS;
            CheckBox[] checkboxes_to_update = new CheckBox[]
            {
                CheckBoxCF,
                CheckBoxPF,
                CheckBoxAF,
                CheckBoxZF,
                CheckBoxSF,
                CheckBoxTF,
                CheckBoxIF,
                CheckBoxDF,
                CheckBoxOF,
                CheckBoxIOPL,
                CheckBoxNT,
                CheckBoxRF,
                CheckBoxVM,
                CheckBoxAC,
                CheckBoxVIF,
                CheckBoxVIP,
                CheckBoxID
            };

            uint[] masks = this.virtual_system.GetEFLAGSMasks();

            for (int i = 0; i < checkboxes_to_update.Length; i++)
                checkboxes_to_update[i].Checked = (EFLAGS & masks[i]) != 0;
        }

        private void ButtonSetRegisterValues_Click(object sender, EventArgs e)
        {
            List<ulong> values_to_set = new();
            uint[] masks = this.virtual_system.GetEFLAGSMasks();
            uint EFLAGS_to_set = 0;

            TextBox[] textbox_to_update = new TextBox[]
            {
                TextBoxRAX,
                TextBoxRBX,
                TextBoxRCX,
                TextBoxRDX,
                TextBoxRSI,
                TextBoxRDI,
                TextBoxRSP,
                TextBoxRBP,
                TextBoxRIP,
                TextBoxR8,
                TextBoxR9,
                TextBoxR10,
                TextBoxR11,
                TextBoxR12,
                TextBoxR13,
                TextBoxR14,
                TextBoxR15
            };

            CheckBox[] checkboxes_to_update = new CheckBox[]
            {
                CheckBoxCF,
                CheckBoxPF,
                CheckBoxAF,
                CheckBoxZF,
                CheckBoxSF,
                CheckBoxTF,
                CheckBoxIF,
                CheckBoxDF,
                CheckBoxOF,
                CheckBoxIOPL,
                CheckBoxNT,
                CheckBoxRF,
                CheckBoxVM,
                CheckBoxAC,
                CheckBoxVIF,
                CheckBoxVIP,
                CheckBoxID
            };

            for (int i = 0; i < textbox_to_update.Length; i++)
                textbox_to_update[i].BackColor = Color.White;

            // get all of the values and to a validity check on them
            for (int i = 0; i < textbox_to_update.Length; i++)
            {
                if (textbox_to_update[i].Text.Trim().Length != 0 && ulong.TryParse(textbox_to_update[i].Text, out ulong value))
                    values_to_set.Add(value);
                else
                    textbox_to_update[i].BackColor = Color.Red;
            }

            this.virtual_system.SetAllRegisterValues(values_to_set.ToArray());

            // check and increment the corresponding bit of the EFLAGS
            for (int i = 0; i < masks.Length; i++)
                if (checkboxes_to_update[i].Checked)
                    EFLAGS_to_set += masks[i];

            this.virtual_system.EFLAGS = EFLAGS_to_set;

            this.emulator.SetVirtualSystem(this.virtual_system);
        }

        private VirtualSystem virtual_system;
        private Emulator emulator;
        private bool is_open;
    }
}
