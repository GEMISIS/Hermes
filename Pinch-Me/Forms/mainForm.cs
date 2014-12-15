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

        ImageForm colorImageForm, depthImageForm, irImageForm;

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
            manager.EnableStream(PXCMCapture.StreamType.STREAM_TYPE_IR, 640, 480, 60);
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
                this.newDepthFrame(0, sample);
                this.newIRFrame(0, sample);
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
            if (sample.color != null && colorImageForm != null && !colorImageForm.IsDisposed)
            {
                PXCMImage.ImageData data = new PXCMImage.ImageData();
                sample.color.AcquireAccess(PXCMImage.Access.ACCESS_READ, PXCMImage.PixelFormat.PIXEL_FORMAT_RGB32, out data);
                colorImageForm.Image = data.ToBitmap(0, sample.color.info.width, sample.color.info.height);
                sample.color.ReleaseAccess(data);
            }
            return pxcmStatus.PXCM_STATUS_NO_ERROR;
        }

        pxcmStatus newDepthFrame(int mid, PXCMCapture.Sample sample)
        {
            if (sample.depth != null && depthImageForm != null && !depthImageForm.IsDisposed)
            {
                PXCMImage.ImageData data = new PXCMImage.ImageData();
                sample.depth.AcquireAccess(PXCMImage.Access.ACCESS_READ, PXCMImage.PixelFormat.PIXEL_FORMAT_RGB32, out data);
                depthImageForm.Image = data.ToBitmap(0, sample.depth.info.width, sample.depth.info.height);
                sample.depth.ReleaseAccess(data);
            }
            return pxcmStatus.PXCM_STATUS_NO_ERROR;
        }

        pxcmStatus newIRFrame(int mid, PXCMCapture.Sample sample)
        {
            if (sample.ir != null && irImageForm != null && !irImageForm.IsDisposed)
            {
                PXCMImage.ImageData data = new PXCMImage.ImageData();
                sample.ir.AcquireAccess(PXCMImage.Access.ACCESS_READ, PXCMImage.PixelFormat.PIXEL_FORMAT_RGB32, out data);
                irImageForm.Image = data.ToBitmap(0, sample.ir.info.width, sample.ir.info.height);
                sample.ir.ReleaseAccess(data);
            }
            return pxcmStatus.PXCM_STATUS_NO_ERROR;
        }
        
        private void mainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            thread.Abort();
            thread.Join();
            manager.Close();
            manager.Dispose();
            session.Dispose();
        }

        private void colorStreamToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(colorImageForm == null || colorImageForm.IsDisposed)
            {
                colorImageForm = new ImageForm();
                colorImageForm.Show();
            }
        }

        private void depthStreamToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (depthImageForm == null || depthImageForm.IsDisposed)
            {
                depthImageForm = new ImageForm();
                depthImageForm.Show();
            }
        }

        private void iRStreamToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (irImageForm == null || irImageForm.IsDisposed)
            {
                irImageForm = new ImageForm();
                irImageForm.Show();
            }
        }
    }
}