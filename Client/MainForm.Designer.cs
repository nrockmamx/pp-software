namespace Client
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            menuStrip1 = new MenuStrip();
            toolStripMenuItem1 = new ToolStripMenuItem();
            toolStripMenuItem3 = new ToolStripMenuItem();
            checkInToolStripMenuItem = new ToolStripMenuItem();
            historyToolStripMenuItem = new ToolStripMenuItem();
            toolStripMenuItem2 = new ToolStripMenuItem();
            memberCardRegisterToolStripMenuItem = new ToolStripMenuItem();
            searchToolStripMenuItem = new ToolStripMenuItem();
            panelContrainer = new Panel();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(24, 24);
            menuStrip1.Items.AddRange(new ToolStripItem[] { toolStripMenuItem1, toolStripMenuItem2 });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(1828, 33);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // toolStripMenuItem1
            // 
            toolStripMenuItem1.DropDownItems.AddRange(new ToolStripItem[] { toolStripMenuItem3 });
            toolStripMenuItem1.Name = "toolStripMenuItem1";
            toolStripMenuItem1.Size = new Size(113, 29);
            toolStripMenuItem1.Text = "Promotion";
            // 
            // toolStripMenuItem3
            // 
            toolStripMenuItem3.DropDownItems.AddRange(new ToolStripItem[] { checkInToolStripMenuItem, historyToolStripMenuItem });
            toolStripMenuItem3.Name = "toolStripMenuItem3";
            toolStripMenuItem3.Size = new Size(270, 34);
            toolStripMenuItem3.Text = "Passport";
            // 
            // checkInToolStripMenuItem
            // 
            checkInToolStripMenuItem.Name = "checkInToolStripMenuItem";
            checkInToolStripMenuItem.Size = new Size(270, 34);
            checkInToolStripMenuItem.Text = "CheckIn";
            checkInToolStripMenuItem.Click += checkInToolStripMenuItem_Click;
            // 
            // historyToolStripMenuItem
            // 
            historyToolStripMenuItem.Name = "historyToolStripMenuItem";
            historyToolStripMenuItem.Size = new Size(270, 34);
            historyToolStripMenuItem.Text = "History";
            // 
            // toolStripMenuItem2
            // 
            toolStripMenuItem2.DropDownItems.AddRange(new ToolStripItem[] { memberCardRegisterToolStripMenuItem, searchToolStripMenuItem });
            toolStripMenuItem2.Name = "toolStripMenuItem2";
            toolStripMenuItem2.Size = new Size(95, 29);
            toolStripMenuItem2.Text = "Member";
            // 
            // memberCardRegisterToolStripMenuItem
            // 
            memberCardRegisterToolStripMenuItem.Name = "memberCardRegisterToolStripMenuItem";
            memberCardRegisterToolStripMenuItem.Size = new Size(177, 34);
            memberCardRegisterToolStripMenuItem.Text = "Register";
            memberCardRegisterToolStripMenuItem.Click += memberCardRegisterToolStripMenuItem_Click;
            // 
            // searchToolStripMenuItem
            // 
            searchToolStripMenuItem.Name = "searchToolStripMenuItem";
            searchToolStripMenuItem.Size = new Size(177, 34);
            searchToolStripMenuItem.Text = "Search";
            // 
            // panelContrainer
            // 
            panelContrainer.Location = new Point(12, 36);
            panelContrainer.Name = "panelContrainer";
            panelContrainer.Size = new Size(1804, 1252);
            panelContrainer.TabIndex = 1;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1828, 1376);
            Controls.Add(panelContrainer);
            Controls.Add(menuStrip1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MainMenuStrip = menuStrip1;
            Name = "MainForm";
            Text = "Palace Casino V1.0";
            WindowState = FormWindowState.Maximized;
            Load += OnLoad;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem toolStripMenuItem1;
        private ToolStripMenuItem toolStripMenuItem3;
        private ToolStripMenuItem checkInToolStripMenuItem;
        private ToolStripMenuItem historyToolStripMenuItem;
        private ToolStripMenuItem toolStripMenuItem2;
        private Panel panelContrainer;
        private ToolStripMenuItem memberCardRegisterToolStripMenuItem;
        private ToolStripMenuItem searchToolStripMenuItem;
    }
}