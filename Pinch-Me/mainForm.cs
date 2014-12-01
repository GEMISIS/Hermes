using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using libpxcclr.cs;

namespace Pinch_Me
{
    public partial class mainForm : Form
    {
        PXCMSession session;
        PXCMSenseManager manager;
        Thread thread;

        public mainForm()
        {
            InitializeComponent();
        }

        private void mainForm_Load(object sender, EventArgs e)
        {
            session = PXCMSession.CreateInstance();
            manager = session.CreateSenseManager();

            if (manager == null)
            {
                Console.WriteLine("Failed");
            }
            manager.EnableStream(PXCMCapture.StreamType.STREAM_TYPE_COLOR, 1920, 1080, 30);
            manager.EnableStream(PXCMCapture.StreamType.STREAM_TYPE_DEPTH, 640, 480, 60);
            manager.EnableHand();

            manager.Init();

            thread = new Thread(new ThreadStart(updateThread));
            thread.Start();
        }

        void updateThread()
        {
            while(true)
            {
                if (manager.AcquireFrame(true) < pxcmStatus.PXCM_STATUS_NO_ERROR)
                {
                    break;
                }
                PXCMHandModule hand = manager.QueryHand();
                PXCMCapture.Sample sample = manager.QuerySample();

                this.newColorFrame(0, sample);
                this.newHandFrame(hand);

                manager.ReleaseFrame();
            }
        }

        pxcmStatus newHandFrame(PXCMHandModule hand)
        {
            if(hand != null)
            {
            }
            return pxcmStatus.PXCM_STATUS_NO_ERROR;
        }

        pxcmStatus newColorFrame(int mid, PXCMCapture.Sample sample)
        {
            if (sample.color != null)
            {
                PXCMImage.ImageData data = new PXCMImage.ImageData();
                sample.color.AcquireAccess(PXCMImage.Access.ACCESS_READ, PXCMImage.PixelFormat.PIXEL_FORMAT_RGB32, out data);
                mainImage.Image = data.ToBitmap(0, sample.color.info.width, sample.color.info.height);
                sample.color.ReleaseAccess(data);
            }
            return pxcmStatus.PXCM_STATUS_NO_ERROR;
        }

        pxcmStatus newDepthFrame(int mid, PXCMCapture.Sample sample)
        {
            if (sample.depth != null)
            {
                PXCMImage.ImageData data = new PXCMImage.ImageData();
                sample.depth.AcquireAccess(PXCMImage.Access.ACCESS_READ, PXCMImage.PixelFormat.PIXEL_FORMAT_RGB32, out data);
                mainImage.Image = data.ToBitmap(0, sample.depth.info.width, sample.depth.info.height);
            }
            return pxcmStatus.PXCM_STATUS_NO_ERROR;
        }

        private void mainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            thread.Abort();
            thread.Join();
            manager.Close();
            manager.Dispose();
            session.Dispose();
        }
    }
}
