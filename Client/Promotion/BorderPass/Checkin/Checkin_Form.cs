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

namespace Client.Promotion.BorderPass.CheckingVoucher
{
    public partial class Checkin_Form : Form
    {
        VideoCaptureDevice videoCapture;
        FilterInfoCollection filterInfoCollection;

        public Checkin_Form()
        {
            InitializeComponent();

        }

        private void captureVideo(object sender, NewFrameEventArgs e)
        {
            Size size = new Size(382, 335);
            var video = resizeImage((Bitmap)e.Frame.Clone(), size);
            border_pass_pictureBox.Image = video;

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
            //videoCapture.DesiredFrameRate = 15;
            videoCapture.Start();
        }

        private async void readcard_button_Click(object sender, EventArgs e)
        {
            try
            {

                var client = new RestClient("http://localhost:21998");
                var request = new RestRequest($"info");

                var res = await client.ExecuteGetAsync(request);

                if (!res.IsSuccessful)
                {
                    MessageBox.Show("Can't find Driver Card Iden Reader", "Error");
                    return;
                }


                request = new RestRequest($"readCard");

                res = await client.ExecuteGetAsync(request);

                if (!res.IsSuccessful)
                {
                    MessageBox.Show("Please input card to reader", "Error");
                    return;
                }

                var card = Newtonsoft.Json.JsonConvert.DeserializeObject<CardReader>(res.Content.ToString());

                if (card != null)
                {
                    addressTh_textBox.Text = $"{card.address_no} {card.address_moo} {card.address_road} {card.address_soi} {card.address_trok} {card.address_tumbol} {card.address_amphor} {card.address_provinice}";
                    dob_textBox.Text = card.birthdate;
                    nameEng_textBox.Text = $"{card.fname_en} {card.sname_en}";
                    nameTh_textBox.Text = $"{card.fname_th} {card.sname_th}";
                    ssid_textBox.Text = card.nat_id;
                    is_textBox.Text = card.issuer;
                    ed_textBox.Text = card.issue_expire;
                    var bitMap = MasterCache.Base64StringToBitmap(card.photo);

                    Size size = new Size(275, 230);
                    personal_pictureBox.Image = resizeImage((Image)bitMap, size);
                    checkin_button.Enabled = true;
                }
                else
                {
                    MessageBox.Show("Can't read card", "Error");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Please check card", "Error");
            }
        }
    }
}
