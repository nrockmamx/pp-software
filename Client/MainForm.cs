using Client.Member.Register;
using Client.Promotion.BorderPass.CheckingVoucher;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class MainForm : Form
    {
        private Checkin_Form checkIn = new Checkin_Form();
        private Register register = new Register();
        public MainForm()
        {
            InitializeComponent();
        }

        private void OnLoad(object sender, EventArgs e)
        {

        }

        private void checkInToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clear();
            checkIn = new Checkin_Form()
            {
                Dock = DockStyle.Fill,
                TopLevel = false,
                TopMost = true,
                FormBorderStyle = FormBorderStyle.None
            };

            this.panelContrainer.Controls.Add(checkIn);
            checkIn.Show();
        }

        private void memberCardRegisterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clear();
            register = new Register()
            {
                Dock = DockStyle.Fill,
                TopLevel = false,
                TopMost = true,
                FormBorderStyle = FormBorderStyle.None
            };

            this.panelContrainer.Controls.Add(register);
            register.Show();
        }

        private void Clear()
        {
            if (checkIn != null)
            {
                checkIn.Close();
                checkIn.Dispose();
                checkIn = null;
            }

            if (register != null)
            {
                register.Dispose();
                register.Close();
                register = null;
            }

            GC.Collect();
        }
    }
}
