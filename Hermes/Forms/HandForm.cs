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

namespace Hermes
{
    public partial class HandForm : Form
    {
        private delegate void UpdateInvokeDelegate();

        private int handCount = 0;
        private Image leftHandImage, rightHandImage;

        private object leftLock = new object(), rightLock = new object();

        public HandForm()
        {
            InitializeComponent();
        }

        private void updateHandCountLabel()
        {
            this.handCountLabel.Text = "Number of hands: " + handCount;
        }

        private void updateLeftHandImage()
        {
            try
            {
                lock (leftLock)
                {
                    this.leftHand.Image = leftHandImage;
                }
            }
            catch (Exception e)
            {
            }
        }
        private void updateRightHandImage()
        {
            try
            {
                lock (rightLock)
                {
                    this.rightHand.Image = rightHandImage;
                }
            }
            catch (Exception e)
            {
            }
        }

        public int HandCount
        {
            set
            {
                handCount = value;
                this.BeginInvoke(new UpdateInvokeDelegate(updateHandCountLabel));
            }
            get
            {
                return handCount;
            }
        }

        public Image LeftHand
        {
            set
            {
                try
                {
                    lock (leftLock)
                    {
                        value.RotateFlip(RotateFlipType.RotateNoneFlipX);
                        this.leftHandImage = new Bitmap(value);
                    }
                    this.BeginInvoke(new UpdateInvokeDelegate(updateLeftHandImage));
                }
                catch(Exception e)
                {
                }
            }
        }
        public Image RightHand
        {
            set
            {
                try
                {
                    lock (rightLock)
                    {
                        value.RotateFlip(RotateFlipType.RotateNoneFlipX);
                        this.rightHandImage = new Bitmap(value);
                    }
                    this.BeginInvoke(new UpdateInvokeDelegate(updateRightHandImage));
                }
                catch (Exception e)
                {
                }
            }
        }
    }
}