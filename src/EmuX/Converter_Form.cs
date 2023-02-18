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
    public partial class Converter_Form : Form
    {
        public Converter_Form()
        {
            InitializeComponent();
        }

        private void Converter_Form_Load(object sender, EventArgs e)
        {
            // Make the text dissapear, that way you can still click it in the designer
            // but the label is empty once the form loads for the user
            LabelResult.Text = "";

            // select the convertion methods
            ComboBoxConvertFrom.SelectedIndex = 0;
            ComboBoxConvertTo.SelectedIndex = 0;
        }

        private void ButtonConvert_Click(object sender, EventArgs e)
        {
            HexConverter hex_converter = new HexConverter();
            BaseConverter base_converter = new BaseConverter();

            // make sure the user entered an actual conversion in the first place
            if (ComboBoxConvertFrom.Text == ComboBoxConvertTo.Text)
            {
                LabelResult.Text = TextBoxConvertFrom.Text;
                return;
            }

            // convert the input to a ulong value first
            ulong value_to_convert = 0;

            // 0: Decimal
            // 1: Binary
            // 2: Hexadecimal
            switch (ComboBoxConvertFrom.SelectedIndex)
            {
                case 0:
                    // check for any user errors
                    if (ulong.TryParse(TextBoxConvertFrom.Text, out value_to_convert) == false)
                    {
                        MessageBox.Show("The input given is not a number", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    break;

                case 1:
                    // check for any user errors
                    if (hex_converter.IsBinary(TextBoxConvertFrom.Text) == false)
                    {
                        MessageBox.Show("The input given is not binary", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    value_to_convert = base_converter.ConvertBinaryToUnsignedLong(TextBoxConvertFrom.Text);

                    break;

                case 2:
                    // check for any user errors
                    if (hex_converter.IsHex(TextBoxConvertFrom.Text) == false)
                    {
                        MessageBox.Show("The input given is not hexadecimal", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    value_to_convert = base_converter.ConvertHexToUnsignedLong(TextBoxConvertFrom.Text);

                    break;
            }

            // convert the previous value to the new form the user wants
            // 0: Decimal
            // 1: Binary
            // 2: Hexadecimal
            switch (ComboBoxConvertTo.SelectedIndex)
            {
                case 0:
                    LabelResult.Text = value_to_convert.ToString();
                    break;

                case 1:
                    LabelResult.Text = base_converter.ConvertUnsignedLongToBinaryString(value_to_convert);
                    break;

                case 2:
                    LabelResult.Text = hex_converter.ConvertUnsignedLongToHex(value_to_convert);
                    break;
            }
        }
    }
}
