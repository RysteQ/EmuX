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
        public MemoryForm(byte[] memory_to_show, int bytes_to_show_per_row)
        {
            InitializeComponent();

            // initialize the data and the amount of columns to display each line
            this.memory_to_show = memory_to_show;
            this.bytes_to_show_per_row = bytes_to_show_per_row;
        }

        private void MemoryForm_Load(object sender, EventArgs e)
        {
            List<string> columns_to_show = ConstructColumns(this.bytes_to_show_per_row);
            List<string> bytes_to_show;

            // add the columns needed
            for (int i = 0; i < columns_to_show.Count; i++)
                DataGridViewMemory.Columns.Add(columns_to_show[i], columns_to_show[i]);

            // display the memory data to the datagridview
            for (int i = 0; i < (this.memory_to_show.Length / this.bytes_to_show_per_row); i++)
            {
                bytes_to_show = new List<string>();
                bytes_to_show.Add((i * this.bytes_to_show_per_row).ToString());

                for (int j = 0; j < this.bytes_to_show_per_row; j++)
                    bytes_to_show.Add(BaseConverter.ConvertUlongToHex(memory_to_show[i * this.bytes_to_show_per_row + j]));

                DataGridViewMemory.Rows.Add(bytes_to_show.ToArray());
            }
        }

        private List<string> ConstructColumns(int total_columns)
        {
            // add the columns and the information of the bytes offset
            List<string> toReturn = new List<string>();

            toReturn.Add("Offset");

            for (int i = 0; i < total_columns; i++)
                toReturn.Add("+" + (i).ToString());

            return toReturn;
        }

        private byte[] memory_to_show = new byte[8192];
        int bytes_to_show_per_row;
    }
}
