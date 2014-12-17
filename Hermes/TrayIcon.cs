using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using libpxcclr.cs;

namespace Hermes
{
    class TrayIcon : ApplicationContext
    {
        NotifyIcon icon;

        PXCMSession session;
        PXCMSenseManager manager;
        Thread thread;

        HandForm handForm;
        ImageForm colorImageForm, depthImageForm, irImageForm;

        public TrayIcon()
        {
            this.icon = new NotifyIcon();

            this.icon.Text = "Hermes";
            this.icon.Icon = Hermes.Properties.Resources.test;

            this.icon.ContextMenu = new ContextMenu();
            this.icon.ContextMenu.MenuItems.Add(new MenuItem("Color Stream", new EventHandler(showColorStream)));
            this.icon.ContextMenu.MenuItems.Add(new MenuItem("Depth Stream", new EventHandler(showDepthStream)));
            this.icon.ContextMenu.MenuItems.Add(new MenuItem("Infrared Stream", new EventHandler(showIRStream)));
            this.icon.ContextMenu.MenuItems.Add(new MenuItem("Hand Stream", new EventHandler(showHandStream)));
            this.icon.ContextMenu.MenuItems.Add(new MenuItem("Exit Hermes", new EventHandler(exit)));

            this.icon.Visible = true;

            startCamera();
        }

        private void startCamera()
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

            PXCMHandConfiguration config = manager.QueryHand().CreateActiveConfiguration();

            config.EnableAllAlerts();
            config.EnableSegmentationImage(true);
            config.EnableTrackedJoints(true);
            config.LoadGesturePack("navigation");
            config.EnableAllGestures(true);

            config.ApplyChanges();
            config.Dispose();

            manager.Init();

            thread = new Thread(new ThreadStart(updateThread));
            thread.Start();
        }

        void updateThread()
        {
            while (true)
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
            if (hand != null)
            {
                PXCMHandData handData = hand.CreateOutput();
                handData.Update();

                PXCMHandData.IHand iHandDataLeft = null, iHandDataRight = null;
                PXCMHandData.JointData jointData = null;
                PXCMImage image = null;

                handData.QueryHandData(PXCMHandData.AccessOrderType.ACCESS_ORDER_LEFT_HANDS, 0, out iHandDataLeft);
                handData.QueryHandData(PXCMHandData.AccessOrderType.ACCESS_ORDER_RIGHT_HANDS, 0, out iHandDataRight);
                if (handForm != null && !handForm.IsDisposed)
                {
                    this.handForm.HandCount = handData.QueryNumberOfHands();
                    if (iHandDataLeft != null)
                    {
                        iHandDataLeft.QuerySegmentationImage(out image);
                        if (image != null)
                        {
                            PXCMImage.ImageData data = new PXCMImage.ImageData();
                            image.AcquireAccess(PXCMImage.Access.ACCESS_READ, PXCMImage.PixelFormat.PIXEL_FORMAT_RGB32, out data);
                            handForm.LeftHand = data.ToBitmap(0, image.info.width, image.info.height);
                            image.ReleaseAccess(data);
                        }
                    }
                    if (iHandDataRight != null)
                    {
                        iHandDataRight.QuerySegmentationImage(out image);
                        if (image != null)
                        {
                            PXCMImage.ImageData data = new PXCMImage.ImageData();
                            image.AcquireAccess(PXCMImage.Access.ACCESS_READ, PXCMImage.PixelFormat.PIXEL_FORMAT_RGB32, out data);
                            handForm.RightHand = data.ToBitmap(0, image.info.width, image.info.height);
                            image.ReleaseAccess(data);
                        }
                    }
                }
                if (iHandDataLeft != null)
                {
                    if (jointData == null)
                    {
                        iHandDataLeft.QueryTrackedJoint(PXCMHandData.JointType.JOINT_INDEX_TIP, out jointData);
                    }

                }
                if (iHandDataRight != null)
                {
                    if (jointData == null)
                    {
                        iHandDataRight.QueryTrackedJoint(PXCMHandData.JointType.JOINT_INDEX_TIP, out jointData);
                    }

                }
                if (jointData != null)
                {
                    Cursor.Position = new System.Drawing.Point(
                        (int)((640.0f - jointData.positionImage.x) * Screen.PrimaryScreen.Bounds.Width / 640.0f),
                        (int)(jointData.positionImage.y * Screen.PrimaryScreen.Bounds.Height / 480.0f));
                    PXCMHandData.GestureData gestureData = null;
                    if(handData.IsGestureFired("two_fingers_pinch_open", out gestureData))
                    {
                        Program.DoMouseClick();
                    }
                    Console.WriteLine("Z Position: " + jointData.positionWorld.z);
                }
                
                handData.Dispose();
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

        protected override void OnMainFormClosed(object sender, EventArgs e)
        {
            base.OnMainFormClosed(sender, e);
            thread.Abort();
            thread.Join();
            manager.Close();
            manager.Dispose();
            session.Dispose();
        }

        private void showColorStream(object sender, EventArgs eventArgs)
        {
            if (colorImageForm == null || colorImageForm.IsDisposed)
            {
                colorImageForm = new ImageForm();
                colorImageForm.Show();
            }
        }
        private void showDepthStream(object sender, EventArgs eventArgs)
        {
            if (depthImageForm == null || depthImageForm.IsDisposed)
            {
                depthImageForm = new ImageForm();
                depthImageForm.Show();
            }
        }
        private void showIRStream(object sender, EventArgs eventArgs)
        {
            if (irImageForm == null || irImageForm.IsDisposed)
            {
                irImageForm = new ImageForm();
                irImageForm.Show();
            }
        }
        private void showHandStream(object sender, EventArgs eventArgs)
        {
            if (handForm == null || handForm.IsDisposed)
            {
                handForm = new HandForm();
                handForm.Show();
            }
        }
        private void exit(object sender, EventArgs eventArgs)
        {
            thread.Abort();
            thread.Join();
            manager.Close();
            manager.Dispose();
            session.Dispose();
            Application.Exit();
        }
    }
}
