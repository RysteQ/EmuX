using EmuX.Services;

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
            LabelResult.Text = "";

            ComboBoxConvertFrom.SelectedIndex = 0;
            ComboBoxConvertTo.SelectedIndex = 0;
        }

        private void ButtonConvert_Click(object sender, EventArgs e)
        {
            HexConverter hex_converter = new HexConverter();
            BaseConverter base_converter = new BaseConverter();
            ulong value_to_convert = 0;

            // make sure the user entered an actual conversion in the first place
            if (ComboBoxConvertFrom.Text == ComboBoxConvertTo.Text)
            {
                LabelResult.Text = TextBoxConvertFrom.Text;
                return;
            }

            // 0: Decimal
            // 1: Binary
            // 2: Hexadecimal
            switch (ComboBoxConvertFrom.SelectedIndex)
            {
                case 0:
                    if (ulong.TryParse(TextBoxConvertFrom.Text, out value_to_convert) == false)
                    {
                        MessageBox.Show("The input given is not a number", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    break;

                case 1:
                    if (hex_converter.IsBinary(TextBoxConvertFrom.Text) == false)
                    {
                        MessageBox.Show("The input given is not binary", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    value_to_convert = base_converter.ConvertBinaryToUnsignedLong(TextBoxConvertFrom.Text);

                    break;

                case 2:
                    if (hex_converter.IsHex(TextBoxConvertFrom.Text) == false)
                    {
                        MessageBox.Show("The input given is not hexadecimal", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    value_to_convert = base_converter.ConvertHexToUnsignedLong(TextBoxConvertFrom.Text);

                    break;
            }

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
