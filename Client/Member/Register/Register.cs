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
using Domain.Model.Request;
using Emgu.CV.Reg;
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

        private void OnLoad(object sender, EventArgs e)
        {
            Size size = new Size(488, 247);
            member_card_back_pictureBox.Image = GlobalFunc.resizeImage(Image.FromFile("Resources/backSide.BMP"), size);
            member_card_front_pictureBox.Image = GlobalFunc.resizeImage(Image.FromFile("Resources/frontSilverNoNumber.bmp"), size);
            from_comboBox.SelectedIndex = 0;

#if DEBUG
            nickname_textBox.Text = "sdsadasdasdasd";
            tel_textBox.Text = "23434343434";
#endif
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
                    var bitMap = GlobalFunc.Base64StringToBitmap(card.photo);
                    Size size = new Size(213, 229);
                    personal_pictureBox.Image = GlobalFunc.resizeImage((Image)bitMap, size);
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


        private async void register_button_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tel_textBox.Text) || tel_textBox.Text.Length < 10)
            {
                MessageBox.Show("Please input phonenumber with number only", "Error");
                return;
            }


            if (string.IsNullOrEmpty(nickname_textBox.Text))
            {
                MessageBox.Show("Please input Nickname", "Error");
                return;
            }


            var client = new RestClient(MasterCache.ApiUrl);
            var request = new RestRequest($"/v1/member/register");
            
            MemberCardRegisterRequest memberCardRegisterRequest = new MemberCardRegisterRequest();
            memberCardRegisterRequest.CardId = ssid_textBox.Text;
            memberCardRegisterRequest.NickName = nickname_textBox.Text;
            memberCardRegisterRequest.Tel = tel_textBox.Text;
            
            memberCardRegisterRequest.CardIden = new MemberCardRegisterRequest.CardIdenDetail();
            memberCardRegisterRequest.CardIden.Address = addressTh_textBox.Text;
            memberCardRegisterRequest.CardIden.NameTh = nameTh_textBox.Text;
            memberCardRegisterRequest.CardIden.NameEng = nameEng_textBox.Text;
            
            if(!string.IsNullOrEmpty(dob_textBox.Text))
                memberCardRegisterRequest.CardIden.DateOfBirth = DateTime.Parse(dob_textBox.Text);

            memberCardRegisterRequest.Tel = tel_textBox.Text;
            memberCardRegisterRequest.From = from_comboBox.SelectedItem.ToString();
            memberCardRegisterRequest.CardIden.Ssid = ssid_textBox.Text;

            request.AddBody(memberCardRegisterRequest);
            var res = await client.ExecutePostAsync(request);

            if (res.StatusCode != System.Net.HttpStatusCode.OK)
            {
                MessageBox.Show("Internal Error");
                return;
            }

            ModelResponse resp = Newtonsoft.Json.JsonConvert.DeserializeObject<ModelResponse>(res.Content.ToString());

            if (!resp.GetStatus())
            {
                switch(resp.data)
                {
                    case "NICKNAME_ERROR":
                        MessageBox.Show("Please input Nickname", "Error");
                        break;
                    case "TEL_ERROR":
                        MessageBox.Show("Please input phonenumber with number only", "Error");
                        break;
                    case "TEL_USED":
                        MessageBox.Show("Phonenumber already used", "Error");
                        break;
                }

                return;
            }

            MessageBox.Show("Register Success", "Success");
            printcard_button.Enabled = true;
        }
    }
}
