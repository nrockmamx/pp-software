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
        public MainForm()
        {
            InitializeComponent();
        }

        private void checkInToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var checkIn = new Checkin_Form()
            {
                Dock = DockStyle.Fill,
                TopLevel = false,
                TopMost = true,
                FormBorderStyle = FormBorderStyle.None
            };

            this.panelContrainer.Controls.Add(checkIn);
            checkIn.Show();
        }
    }
}
