using Domain.Model;
using Domain.Model.Request;
using Domain.Model.Response;
using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();

#if DEBUG
            username_textBox.Text = "admin";
            password_textBox.Text = "admin";
#endif
        }

        private async void login_button_Click(object sender, EventArgs e)
        {
            try
            {
                var client = new RestClient(MasterCache.ApiUrl);
                var request = new RestRequest($"v1/login/accesstoken/get");

                AccessTokenGetRequest accessTokenGetRequest = new AccessTokenGetRequest();
                accessTokenGetRequest.Username = username_textBox.Text;
                accessTokenGetRequest.Password = password_textBox.Text;
                request.AddBody(accessTokenGetRequest);
                var res = await client.ExecutePostAsync(request);

                if (res.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    MessageBox.Show("Invalid Username or Password");
                    return;
                }

                ModelResponse resp = Newtonsoft.Json.JsonConvert.DeserializeObject<ModelResponse>(res.Content.ToString());

                if (!resp.GetStatus())
                {
                    MessageBox.Show("Invalid Username or Password");
                    return;
                }

                var data = resp.GetData<AccessTokenGetResponse>();

                MasterCache.Token = data.Token;

                var mainForm = new MainForm();
                mainForm.Show();
                this.Hide();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
