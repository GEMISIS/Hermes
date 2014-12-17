using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hermes
{
    public partial class ImageForm : Form
    {
        public ImageForm()
        {
            InitializeComponent();
        }

        public Image Image
        {
            set
            {
                this.debugImage.Image = value;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Filter = "PNG Image File | *.png | BMP Image File | *.bmp | Jpeg Image File | *.jpg";
            if(saveDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.debugImage.Image.Save(saveDialog.FileName);
            }
        }
    }
}
