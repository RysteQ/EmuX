using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EmuX
{
    public partial class Instruction_Set : Form
    {
        public Instruction_Set()
        {
            InitializeComponent();
        }

        private async void Instruction_Set_Load(object sender, EventArgs e)
        {
            await WebViewInstructionSet.EnsureCoreWebView2Async(null);
            WebViewInstructionSet.NavigateToString(Properties.Resources.InstructionSetHtmlPage);
        }
    }
}
