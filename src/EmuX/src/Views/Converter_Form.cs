﻿using EmuX.src.Services.Base_Converter;

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
                    if (BaseVerifier.IsBinary(TextBoxConvertFrom.Text) == false)
                    {
                        MessageBox.Show("The input given is not binary", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    value_to_convert = BaseConverter.ConvertBinaryToUlong(TextBoxConvertFrom.Text);

                    break;

                case 2:
                    if (BaseVerifier.IsHex(TextBoxConvertFrom.Text) == false)
                    {
                        MessageBox.Show("The input given is not hexadecimal", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    value_to_convert = BaseConverter.ConvertHexToUlong(TextBoxConvertFrom.Text);

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
                    LabelResult.Text = BaseConverter.ConvertUlongToBinary(value_to_convert);
                    break;

                case 2:
                    LabelResult.Text = BaseConverter.ConvertUlongToHex(value_to_convert);
                    break;
            }
        }
    }
}