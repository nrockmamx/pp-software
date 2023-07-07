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
using Domain.Model.Request;
using Domain.MongoDB.Collections;
using Domain.Model.Response;
using static MongoDB.Bson.Serialization.Serializers.SerializerHelper;
using System.Text.RegularExpressions;

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
                try
                {
                    if (PassportScanner.passportInfo != null && !PassportScanner.passportInfo.Used)
                    {

                        ThreadHelperClass.SetText(this, nameEng_textBox, $"{PassportScanner.passportInfo.Givenname} {PassportScanner.passportInfo.Familyname}");
                        ThreadHelperClass.SetText(this, passport_id_textBox, PassportScanner.passportInfo.DocumentNo);
                        ThreadHelperClass.SetText(this, dob_textBox, PassportScanner.passportInfo.Birthday);
                        ThreadHelperClass.SetText(this, ed_textBox, PassportScanner.passportInfo.Dateofexpiry);
                        ThreadHelperClass.SetText(this, ssid_textBox, PassportScanner.passportInfo.PersonalNo);
                        ThreadHelperClass.SetText(this, mrtds_textBox, PassportScanner.passportInfo.Mrtds);
                        ThreadHelperClass.SetText(this, phone_textBox, "");
                        ThreadHelperClass.Enable(this, checkin_button, true);
                        Size size = new Size(488, 225);
                        var pic = resizeImage(Bitmap.FromFile("PassportImage/temp.jpg"), size);

                        passport_pictureBox.Image = pic;
                        PassportScanner.passportInfo.Used = true;

                        var client = new RestClient(MasterCache.ApiUrl);
                        var request = new RestRequest($"v1/promotion/borderpass/history/{ssid_textBox.Text}/1");
                        request.AddHeader("Authorization", MasterCache.Token);

                        var res = await client.ExecuteGetAsync(request);

                        if (res.StatusCode != System.Net.HttpStatusCode.OK)
                        {
                            ThreadHelperClass.SetText(this, status_label, "Server Error");
                            await Task.Delay(1000);
                            ThreadHelperClass.Enable(this, checkin_button, false);
                            continue;
                        }

                        var resp = Newtonsoft.Json.JsonConvert.DeserializeObject<ModelResponse>(res.Content.ToString());

                        if (resp.GetStatus())
                        {
                            var data = Newtonsoft.Json.JsonConvert.DeserializeObject<List<PromotionCheckInResponse>>(resp.data.ToString());

                            if (data != null && data.Count > 0)
                            {

                                foreach (var d in data)
                                {
                                    string[] val = new string[4];
                                    val[0] = d.CreatedAt.ToString();
                                    val[1] = d.Dt;
                                    val[2] = d.PhoneNumber;
                                    val[3] = d.Amount.ToString();
                                    ThreadHelperClass.AddDataGrid(this, history_dataGridView, val);
                                }
                            }
                        }

                        client = new RestClient(MasterCache.ApiUrl);
                        request = new RestRequest($"v1/promotion/borderpass/check/{ssid_textBox.Text}");
                        request.AddHeader("Authorization", MasterCache.Token);

                        res = await client.ExecuteGetAsync(request);

                        if (res.StatusCode != System.Net.HttpStatusCode.OK)
                        {
                            ThreadHelperClass.SetText(this, status_label, "Server Error");
                            await Task.Delay(1000);
                            ThreadHelperClass.Enable(this, checkin_button, false);
                            continue;
                        }

                        resp = Newtonsoft.Json.JsonConvert.DeserializeObject<ModelResponse>(res.Content.ToString());

                        if (!resp.GetStatus())
                        {
                            ThreadHelperClass.SetText(this, status_label, "already");
                            await Task.Delay(1000);
                            ThreadHelperClass.Enable(this, checkin_button, false);
                            continue;
                        }

                        ThreadHelperClass.SetText(this, status_label, "Yess!!");
                        ThreadHelperClass.Enable(this, checkin_button, true);


                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.ToString(), "ERROR");
                }

                await Task.Delay(1000);
            }
        }
        private void captureVideo(object sender, NewFrameEventArgs e)
        {
            Size size = new Size(488, 225);
            var video = resizeImage((Bitmap)e.Frame.Clone(), size);
            camera_pictureBox.Image = video;
            GC.Collect();
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

            history_dataGridView.Rows.Clear();
            history_dataGridView.Columns.Clear();
            history_dataGridView.ColumnCount = 5;
            history_dataGridView.Columns[0].Name = "วันที่";
            history_dataGridView.Columns[2].Name = "เบอร์โทร";
            history_dataGridView.Columns[3].Name = "จำนวนเงิน";
            history_dataGridView.Columns[1].Name = "เวลา";
        }

        private async void checkin_button_Click(object sender, EventArgs e)
        {
            if(phone_textBox.Text.Length<10 || !Regex.IsMatch(phone_textBox.Text, "^[0-9]+$"))
            {
                MessageBox.Show("Please input Phone Number");
                return;
            }

            if (money_textBox.Text=="" || !Regex.IsMatch(money_textBox.Text, "^[0-9]+$"))
            {
                MessageBox.Show("Please input Money");
                return;
            }

            PromotionCheckInRequest promotionCheckInRequest = new PromotionCheckInRequest();
            promotionCheckInRequest.PassportInfo = new Domain.Model.PassportInfo();
            promotionCheckInRequest.PassportInfo.Angle = PassportScanner.passportInfo.Angle;
            promotionCheckInRequest.PassportInfo.Familyname = PassportScanner.passportInfo.Familyname;
            promotionCheckInRequest.PassportInfo.Birthday = PassportScanner.passportInfo.Birthday;
            promotionCheckInRequest.PassportInfo.Dateofexpiry = PassportScanner.passportInfo.Dateofexpiry;
            promotionCheckInRequest.PassportInfo.Angle = PassportScanner.passportInfo.Angle;
            promotionCheckInRequest.PassportInfo.Birthday = PassportScanner.passportInfo.Birthday;
            promotionCheckInRequest.PassportInfo.DocumentNo = PassportScanner.passportInfo.DocumentNo;
            promotionCheckInRequest.PassportInfo.Givenname = PassportScanner.passportInfo.Givenname;
            promotionCheckInRequest.PassportInfo.IssueState = PassportScanner.passportInfo.IssueState;
            promotionCheckInRequest.PassportInfo.Sex = PassportScanner.passportInfo.Sex;
            promotionCheckInRequest.PassportInfo.PersonalNo = PassportScanner.passportInfo.PersonalNo;
            promotionCheckInRequest.PassportInfo.Mrtds = PassportScanner.passportInfo.Mrtds;
            promotionCheckInRequest.PassportInfo.Nationality = PassportScanner.passportInfo.Nationality;
            promotionCheckInRequest.PassportInfo.NativeName = PassportScanner.passportInfo.NativeName;
            promotionCheckInRequest.PassportInfo.Type = PassportScanner.passportInfo.Type;
            promotionCheckInRequest.PhoneNumber = phone_textBox.Text;
            promotionCheckInRequest.Amount = Convert.ToDecimal(money_textBox.Text);

            using (var ms = new MemoryStream())
            {
                using (var bitmap = new Bitmap(camera_pictureBox.Image))
                {
                    bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                    promotionCheckInRequest.CameraImage = Convert.ToBase64String(ms.GetBuffer());
                }
            }

            using (var ms = new MemoryStream())
            {
                using (var bitmap = new Bitmap(passport_pictureBox.Image))
                {
                    bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                    promotionCheckInRequest.PassportImage = Convert.ToBase64String(ms.GetBuffer());
                }
            }


            var client = new RestClient(MasterCache.ApiUrl);
            var request = new RestRequest($"v1/promotion/borderpass/checkin");
            request.AddHeader("Authorization", MasterCache.Token);
            request.AddBody(promotionCheckInRequest);
            var res = await client.ExecutePostAsync(request);

            if (res.StatusCode != System.Net.HttpStatusCode.OK)
            {
                status_label.Text = "Server Error";
                checkin_button.Enabled = false;

                return;
            }


            ModelResponse resp = Newtonsoft.Json.JsonConvert.DeserializeObject<ModelResponse>(res.Content.ToString());

            if (resp.GetStatus())
            {
                status_label.Text = "Done";
                checkin_button.Enabled = false;

                client = new RestClient(MasterCache.ApiUrl);
                request = new RestRequest($"v1/promotion/borderpass/history/{ssid_textBox.Text}/1");
                request.AddHeader("Authorization", MasterCache.Token);

                res = await client.ExecuteGetAsync(request);

                if (res.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    return;
                }

                resp = Newtonsoft.Json.JsonConvert.DeserializeObject<ModelResponse>(res.Content.ToString());

                if (resp.GetStatus())
                {
                    var data = Newtonsoft.Json.JsonConvert.DeserializeObject<List<PromotionCheckInResponse>>(resp.data.ToString());

                    if (data != null && data.Count > 0)
                    {

                        foreach (var d in data)
                        {
                            string[] val = new string[4];
                            val[0] = d.CreatedAt.ToString();
                            val[1] = d.Dt;
                            val[2] = d.PhoneNumber;
                            val[3] = d.Amount.ToString();
                            ThreadHelperClass.AddDataGrid(this, history_dataGridView, val);
                        }
                    }
                }

                return;
            }

            status_label.Text = "Server Error";
            checkin_button.Enabled = false;

        }
    }
}
