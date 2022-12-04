using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EmuX
{
    public partial class Registers_Form : Form
    {
        public Registers_Form()
        {
            InitializeComponent();
        }

        private void ButtonUpdateRegisters_Click(object sender, EventArgs e)
        {
            // TODO
        }

        private void Registers_Form_Load(object sender, EventArgs e)
        {
            ComboBoxAccumulator.SelectedIndex = 0;
            ComboBoxBase.SelectedIndex = 0;
            ComboBoxCounter.SelectedIndex = 0;
            ComboBoxData.SelectedIndex = 0;
            ComboBoxR8.SelectedIndex = 0;
            ComboBoxR9.SelectedIndex = 0;
            ComboBoxR10.SelectedIndex = 0;
            ComboBoxR11.SelectedIndex = 0;
            ComboBoxR12.SelectedIndex = 0;
            ComboBoxR13.SelectedIndex = 0;
            ComboBoxR14.SelectedIndex = 0;
            ComboBoxR15.SelectedIndex = 0;
        }
    }
}
