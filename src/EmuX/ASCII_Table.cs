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
    public partial class ASCII_Table : Form
    {
        public ASCII_Table()
        {
            InitializeComponent();
        }

        private async void ASCII_Table_Load(object sender, EventArgs e)
        {
            await WebViewASCIITable.EnsureCoreWebView2Async(null);
            WebViewASCIITable.NavigateToString("<h1>TODO</h1>");
        }

        private void WebViewASCIITable_Click(object sender, EventArgs e)
        {
            // TODO
        }
    }
}
