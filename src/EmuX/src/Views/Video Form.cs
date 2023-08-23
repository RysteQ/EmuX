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
    public partial class VideoForm : Form
    {
        public VideoForm()
        {
            InitializeComponent();
        }

        private void VideoForm_Load(object sender, EventArgs e)
        {
            this.IsOpen = true;
        }

        private void VideoForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.IsOpen = false;
        }

        public void InitVideo(int width, int height)
        {
            this.Size = new Size(width + 16, height + 40);
            this.PictureBoxVideoOutput.Size = new Size(width, height);
        }

        public void UpdateVideo(Bitmap video_to_set)
        {
            this.PictureBoxVideoOutput.Image = video_to_set;
        }

        public bool IsOpen = false;
    }
}
