using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using libpxcclr.cs;

namespace Pinch_Me
{
    public partial class mainForm : Form
    {
        PXCMSession session;
        PXCMSenseManager manager;

        public mainForm()
        {
            InitializeComponent();
        }

        private void mainForm_Load(object sender, EventArgs e)
        {
            session = PXCMSession.CreateInstance();
            manager = PXCMSenseManager.CreateInstance();

            if (manager == null)
            {
                Console.WriteLine("Failed");
            }
            manager.EnableStream(PXCMCapture.StreamType.STREAM_TYPE_COLOR, 1920, 1080, 30);
            manager.Init();

            Timer t = new Timer();
            t.Tick += t_Tick;
            t.Start();
        }

        void t_Tick(object sender, EventArgs e)
        {
            if(manager.AcquireFrame(true) < pxcmStatus.PXCM_STATUS_NO_ERROR)
            {
                return;
            }
            PXCMCapture.Sample sample = manager.QuerySample();
            this.newColorFrame(0, sample);
            manager.ReleaseFrame();
        }

        pxcmStatus newColorFrame(int mid, PXCMCapture.Sample sample)
        {
            if(sample.color != null)
            {
                PXCMImage.ImageData data = new PXCMImage.ImageData();
                sample.color.AcquireAccess(PXCMImage.Access.ACCESS_READ, PXCMImage.PixelFormat.PIXEL_FORMAT_RGB32, out data);
                mainImage.Image = data.ToBitmap(0, sample.color.info.width, sample.color.info.height);
            }
            return pxcmStatus.PXCM_STATUS_NO_ERROR;
        }

        private void mainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            manager.Close();
            manager.Dispose();
            session.Dispose();
        }
    }
}
