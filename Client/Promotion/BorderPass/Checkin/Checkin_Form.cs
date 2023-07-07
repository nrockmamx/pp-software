using Emgu.CV;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AForge.Video;
using AForge.Video.DirectShow;
using System.Drawing.Drawing2D;
using RestSharp;
using Domain.Model;
using System.Security.Policy;
using Emgu.Util;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Client.Promotion.BorderPass.CheckingVoucher
{
    public partial class Checkin_Form : Form
    {
        VideoCaptureDevice videoCapture;
        FilterInfoCollection filterInfoCollection;
        private bool run = true;

        public Checkin_Form()
        {
            InitializeComponent();

        }

        public void Dispose()
        {
            if (videoCapture != null)
            {

                videoCapture.SignalToStop();
                videoCapture.WaitForStop();
            }

            PassportScanner.Stop = true;
        }

        private async void ReadPassport()
        {
            while (run)
            {
                if (PassportScanner.passportInfo != null && !PassportScanner.passportInfo.Used)
                {

                    ThreadHelperClass.SetText(this, nameEng_textBox, $"{PassportScanner.passportInfo.Givenname} {PassportScanner.passportInfo.Familyname}");
                    ThreadHelperClass.SetText(this, passport_id_textBox, PassportScanner.passportInfo.DocumentNo);
                    ThreadHelperClass.SetText(this, dob_textBox, PassportScanner.passportInfo.Birthday);
                    ThreadHelperClass.SetText(this, ed_textBox, PassportScanner.passportInfo.Dateofexpiry);
                    ThreadHelperClass.SetText(this, ssid_textBox, PassportScanner.passportInfo.PersonalNo);
                    ThreadHelperClass.SetText(this, mrtds_textBox, PassportScanner.passportInfo.Mrtds);
                    ThreadHelperClass.Enable(this, checkin_button, true);
                    Size size = new Size(488, 225);
                    var pic = resizeImage(Bitmap.FromFile("PassportImage/temp.jpg"), size);

                    checkin_button.Enabled = true;
                    passport_pictureBox.Image = pic;
                    PassportScanner.passportInfo.Used = true;
                }

                await Task.Delay(1000);
            }
        }
        private void captureVideo(object sender, NewFrameEventArgs e)
        {
            Size size = new Size(488, 225);
            var video = resizeImage((Bitmap)e.Frame.Clone(), size);
            border_pass_pictureBox.Image = video;
            GC.Collect();
        }

        private async void get_voucher_button_Click(object sender, EventArgs e)
        {

        }

        private System.Drawing.Image resizeImage(System.Drawing.Image imgToResize, Size size)
        {
            //Get the image current width  
            int sourceWidth = imgToResize.Width;
            //Get the image current height  
            int sourceHeight = imgToResize.Height;
            float nPercent = 0;
            float nPercentW = 0;
            float nPercentH = 0;
            //Calulate  width with new desired size  
            nPercentW = ((float)size.Width / (float)sourceWidth);
            //Calculate height with new desired size  
            nPercentH = ((float)size.Height / (float)sourceHeight);
            if (nPercentH < nPercentW)
                nPercent = nPercentH;
            else
                nPercent = nPercentW;
            //New Width  
            int destWidth = (int)(sourceWidth * nPercent);
            //New Height  
            int destHeight = (int)(sourceHeight * nPercent);
            Bitmap b = new Bitmap(destWidth, destHeight);
            Graphics g = Graphics.FromImage((System.Drawing.Image)b);
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            // Draw image with new width and height  

            g.DrawImage(imgToResize, 0, 0, destWidth, destHeight);
            g.Dispose();
            return (System.Drawing.Image)b;
        }

        private void LoadFrom(object sender, EventArgs e)
        {
            filterInfoCollection = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            videoCapture = new VideoCaptureDevice(filterInfoCollection[0].MonikerString);
            videoCapture.NewFrame += new NewFrameEventHandler(captureVideo);
            videoCapture.DesiredFrameRate = 60;
            videoCapture.Start();

            Task.Run(() =>
            {
                PassportScanner.Start();
            });

            Thread thread1 = new Thread(ReadPassport);
            thread1.Start();
            run = true;
        }

        private void checkin_button_Click(object sender, EventArgs e)
        {

        }
    }
}
