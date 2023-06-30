using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Domain.Model;
using RestSharp;
using static Client.EVOSDK1Enums;
using static Client.EVOSDK1Wrapper;

namespace Client.Member.Register
{
    public partial class Register : Form
    {
        static IntPtr pHandle = IntPtr.Zero;
        public Register()
        {
            InitializeComponent();
        }

        private void printcard_Click(object sender, EventArgs e)
        {
            Printer p = new Printer();
            Data data = new Data("Evolis Primacy");
            p.Print(data, "", "");

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

        private void register_button_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(tel_textBox.Text))
            {
                MessageBox.Show("Please input tel with number only","Error");
                return;
            }

            if (string.IsNullOrEmpty(nickname_textBox.Text))
            {
                MessageBox.Show("Please input Nickname", "Error");
                return;
            }
        }
    }
}
