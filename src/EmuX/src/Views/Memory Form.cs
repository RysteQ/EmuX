using EmuX.Services;
using EmuX.src.Services.Base_Converter;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EmuX
{
    public partial class MemoryForm : Form
    {
        public MemoryForm(ref VirtualSystem virtual_system)
        {
            InitializeComponent();

            this.virtual_system = virtual_system;
        }

        private void ButtonSearchMemoryRange_Click(object sender, EventArgs e)
        {
            List<byte> bytes_to_show = new();

            int start = 0;
            int end = 0;

            if ((TextBoxMemoryRangeStart.Text.Trim() == "" && TextBoxMemoryRangeEnd.Text.Trim() == "") || ComboBoxMemoryRepresentation.SelectedIndex == -1)
                return;

            if (TextBoxMemoryRangeEnd.Text.Trim().Length != 0 && TextBoxMemoryRangeStart.Text.Trim().Length != 0)
            {
                if (int.TryParse(TextBoxMemoryRangeEnd.Text, out end) == false || int.TryParse(TextBoxMemoryRangeStart.Text, out start) == false)
                {
                    MessageBox.Show("Error converting memory range end to int", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            // check if the values are valid
            if (end < start || (start - end) % 8 != 0)
            {
                MessageBox.Show("Please select a range of at least 8 bytes", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // init the data grid view
            DataGridViewMemory.Rows.Clear();
            DataGridViewMemory.Columns.Clear();
            DataGridViewMemory.Columns.Add("empty", "");

            for (int i = start; i < end; i++)
                bytes_to_show.Add(this.virtual_system.GetByteMemory(i));

            for (int i = 0; i < 8; i++)
                DataGridViewMemory.Columns.Add("+" + i.ToString(), "+" + i.ToString());

            for (int row = 0; row < (end - start) / 8; row++)
            {
                List<string> to_add = new();
                to_add.Add((row * 8).ToString());

                // check which representation the user wants
                // 0 = decimal, 1 = binary and 2 = hexadecimal
                switch (ComboBoxMemoryRepresentation.SelectedIndex)
                {
                    case 0:
                        for (int column = 0; column < 8; column++)
                            to_add.Add(bytes_to_show[row * 8 + column].ToString());

                        break;

                    case 1:
                        for (int column = 0; column < 8; column++)
                            to_add.Add("0b" + BaseConverter.ConvertUlongToBinary(bytes_to_show[row * 8 + column]));

                        break;

                    case 2:
                        for (int column = 0; column < 8; column++)
                            to_add.Add("0x" + BaseConverter.ConvertUlongToHex(bytes_to_show[row * 8 + column]));

                        break;
                }

                DataGridViewMemory.Rows.Add(to_add.ToArray());
            }
        }

        private void ButtonClearMemoryTable_Click(object sender, EventArgs e)
        {
            DataGridViewMemory.Rows.Clear();
            DataGridViewMemory.Columns.Clear();
        }

        private void ButtonSetMemoryValue_Click(object sender, EventArgs e)
        {
            byte value_to_set;
            int memory_end;
            int memory_start;

            bool valid_end_range = int.TryParse(TextBoxMemoryRangeEnd.Text, out memory_end);
            bool valid_start_range = int.TryParse(TextBoxMemoryRangeStart.Text, out memory_start);
            bool valid_value = byte.TryParse(TextBoxMemoryValue.Text, out value_to_set);

            if ((valid_end_range == false || valid_start_range == false || valid_value == false) && memory_end < memory_start)
            {
                MessageBox.Show("Please enter a valid memory range / value to set the memory range at", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            for (int index = memory_start; index < memory_end; index++)
                this.virtual_system.SetByteMemory(index, value_to_set);
        }

        private VirtualSystem virtual_system;
    }
}
