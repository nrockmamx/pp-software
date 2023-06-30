namespace Client.Member.Register
{
    partial class Register
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
            printcard_button = new Button();
            groupBox1 = new GroupBox();
            from_comboBox = new ComboBox();
            label12 = new Label();
            member_card_back_pictureBox = new PictureBox();
            member_card_front_pictureBox = new PictureBox();
            groupBox2 = new GroupBox();
            readcard_button = new Button();
            personal_pictureBox = new PictureBox();
            addressTh_textBox = new TextBox();
            label3 = new Label();
            ed_textBox = new TextBox();
            label11 = new Label();
            ssid_textBox = new TextBox();
            label10 = new Label();
            is_textBox = new TextBox();
            label9 = new Label();
            dob_textBox = new TextBox();
            label8 = new Label();
            groupBox4 = new GroupBox();
            textBox5 = new TextBox();
            nameEng_textBox = new TextBox();
            label5 = new Label();
            label6 = new Label();
            groupBox3 = new GroupBox();
            textBox3 = new TextBox();
            nameTh_textBox = new TextBox();
            label4 = new Label();
            label7 = new Label();
            tel_textBox = new TextBox();
            label2 = new Label();
            nickname_textBox = new TextBox();
            label1 = new Label();
            register_button = new Button();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)member_card_back_pictureBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)member_card_front_pictureBox).BeginInit();
            groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)personal_pictureBox).BeginInit();
            groupBox4.SuspendLayout();
            groupBox3.SuspendLayout();
            SuspendLayout();
            // 
            // printcard_button
            // 
            printcard_button.Enabled = false;
            printcard_button.Font = new Font("Segoe UI", 24F, FontStyle.Regular, GraphicsUnit.Point);
            printcard_button.Location = new Point(460, 971);
            printcard_button.Name = "printcard_button";
            printcard_button.Size = new Size(411, 158);
            printcard_button.TabIndex = 0;
            printcard_button.Text = "Print Card";
            printcard_button.UseVisualStyleBackColor = true;
            printcard_button.Click += printcard_Click;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(from_comboBox);
            groupBox1.Controls.Add(label12);
            groupBox1.Controls.Add(member_card_back_pictureBox);
            groupBox1.Controls.Add(member_card_front_pictureBox);
            groupBox1.Controls.Add(groupBox2);
            groupBox1.Controls.Add(tel_textBox);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(nickname_textBox);
            groupBox1.Controls.Add(label1);
            groupBox1.Location = new Point(28, 25);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(1338, 928);
            groupBox1.TabIndex = 1;
            groupBox1.TabStop = false;
            groupBox1.Text = "Personal Detail";
            // 
            // from_comboBox
            // 
            from_comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            from_comboBox.FormattingEnabled = true;
            from_comboBox.Items.AddRange(new object[] { "Walk In", "Junket", "Tour" });
            from_comboBox.Location = new Point(646, 42);
            from_comboBox.Name = "from_comboBox";
            from_comboBox.Size = new Size(217, 33);
            from_comboBox.TabIndex = 8;
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Location = new Point(579, 51);
            label12.Name = "label12";
            label12.Size = new Size(58, 25);
            label12.TabIndex = 7;
            label12.Text = "From:";
            // 
            // member_card_back_pictureBox
            // 
            member_card_back_pictureBox.Location = new Point(563, 663);
            member_card_back_pictureBox.Name = "member_card_back_pictureBox";
            member_card_back_pictureBox.Size = new Size(488, 247);
            member_card_back_pictureBox.TabIndex = 6;
            member_card_back_pictureBox.TabStop = false;
            // 
            // member_card_front_pictureBox
            // 
            member_card_front_pictureBox.Location = new Point(37, 663);
            member_card_front_pictureBox.Name = "member_card_front_pictureBox";
            member_card_front_pictureBox.Size = new Size(488, 247);
            member_card_front_pictureBox.TabIndex = 5;
            member_card_front_pictureBox.TabStop = false;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(readcard_button);
            groupBox2.Controls.Add(personal_pictureBox);
            groupBox2.Controls.Add(addressTh_textBox);
            groupBox2.Controls.Add(label3);
            groupBox2.Controls.Add(ed_textBox);
            groupBox2.Controls.Add(label11);
            groupBox2.Controls.Add(ssid_textBox);
            groupBox2.Controls.Add(label10);
            groupBox2.Controls.Add(is_textBox);
            groupBox2.Controls.Add(label9);
            groupBox2.Controls.Add(dob_textBox);
            groupBox2.Controls.Add(label8);
            groupBox2.Controls.Add(groupBox4);
            groupBox2.Controls.Add(groupBox3);
            groupBox2.Location = new Point(37, 152);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(1279, 481);
            groupBox2.TabIndex = 4;
            groupBox2.TabStop = false;
            groupBox2.Text = "Iden Card";
            // 
            // readcard_button
            // 
            readcard_button.Font = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point);
            readcard_button.Location = new Point(609, 346);
            readcard_button.Name = "readcard_button";
            readcard_button.Size = new Size(363, 103);
            readcard_button.TabIndex = 2;
            readcard_button.Text = "Read Iden Card";
            readcard_button.UseVisualStyleBackColor = true;
            readcard_button.Click += readcard_button_Click;
            // 
            // personal_pictureBox
            // 
            personal_pictureBox.BorderStyle = BorderStyle.Fixed3D;
            personal_pictureBox.Location = new Point(1046, 40);
            personal_pictureBox.Name = "personal_pictureBox";
            personal_pictureBox.Size = new Size(213, 229);
            personal_pictureBox.TabIndex = 19;
            personal_pictureBox.TabStop = false;
            // 
            // addressTh_textBox
            // 
            addressTh_textBox.Location = new Point(155, 163);
            addressTh_textBox.Multiline = true;
            addressTh_textBox.Name = "addressTh_textBox";
            addressTh_textBox.ReadOnly = true;
            addressTh_textBox.Size = new Size(341, 183);
            addressTh_textBox.TabIndex = 18;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(68, 163);
            label3.Name = "label3";
            label3.Size = new Size(81, 25);
            label3.TabIndex = 17;
            label3.Text = "Address:";
            // 
            // ed_textBox
            // 
            ed_textBox.Location = new Point(669, 162);
            ed_textBox.Name = "ed_textBox";
            ed_textBox.ReadOnly = true;
            ed_textBox.Size = new Size(339, 31);
            ed_textBox.TabIndex = 26;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(560, 164);
            label11.Name = "label11";
            label11.Size = new Size(105, 25);
            label11.TabIndex = 27;
            label11.Text = "Expire Date:";
            // 
            // ssid_textBox
            // 
            ssid_textBox.Location = new Point(669, 199);
            ssid_textBox.Name = "ssid_textBox";
            ssid_textBox.ReadOnly = true;
            ssid_textBox.Size = new Size(339, 31);
            ssid_textBox.TabIndex = 24;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(589, 202);
            label10.Name = "label10";
            label10.Size = new Size(74, 25);
            label10.TabIndex = 25;
            label10.Text = "Card Id:";
            // 
            // is_textBox
            // 
            is_textBox.Location = new Point(669, 276);
            is_textBox.Name = "is_textBox";
            is_textBox.ReadOnly = true;
            is_textBox.Size = new Size(339, 31);
            is_textBox.TabIndex = 22;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(584, 282);
            label9.Name = "label9";
            label9.Size = new Size(79, 25);
            label9.TabIndex = 23;
            label9.Text = "Issue At:";
            // 
            // dob_textBox
            // 
            dob_textBox.Location = new Point(669, 238);
            dob_textBox.Name = "dob_textBox";
            dob_textBox.ReadOnly = true;
            dob_textBox.Size = new Size(339, 31);
            dob_textBox.TabIndex = 20;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(546, 244);
            label8.Name = "label8";
            label8.Size = new Size(117, 25);
            label8.TabIndex = 21;
            label8.Text = "Date of birth:";
            // 
            // groupBox4
            // 
            groupBox4.Controls.Add(textBox5);
            groupBox4.Controls.Add(nameEng_textBox);
            groupBox4.Controls.Add(label5);
            groupBox4.Controls.Add(label6);
            groupBox4.Location = new Point(521, 40);
            groupBox4.Name = "groupBox4";
            groupBox4.Size = new Size(493, 116);
            groupBox4.TabIndex = 19;
            groupBox4.TabStop = false;
            groupBox4.Text = "English";
            // 
            // textBox5
            // 
            textBox5.Location = new Point(148, 75);
            textBox5.Name = "textBox5";
            textBox5.ReadOnly = true;
            textBox5.Size = new Size(339, 31);
            textBox5.TabIndex = 3;
            // 
            // nameEng_textBox
            // 
            nameEng_textBox.Location = new Point(148, 39);
            nameEng_textBox.Name = "nameEng_textBox";
            nameEng_textBox.ReadOnly = true;
            nameEng_textBox.Size = new Size(339, 31);
            nameEng_textBox.TabIndex = 2;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(19, 78);
            label5.Name = "label5";
            label5.Size = new Size(123, 25);
            label5.TabIndex = 1;
            label5.Text = "Middel Name:";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(77, 42);
            label6.Name = "label6";
            label6.Size = new Size(63, 25);
            label6.TabIndex = 0;
            label6.Text = "Name:";
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(textBox3);
            groupBox3.Controls.Add(nameTh_textBox);
            groupBox3.Controls.Add(label4);
            groupBox3.Controls.Add(label7);
            groupBox3.Location = new Point(7, 40);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(493, 116);
            groupBox3.TabIndex = 16;
            groupBox3.TabStop = false;
            groupBox3.Text = "Thai";
            // 
            // textBox3
            // 
            textBox3.Location = new Point(148, 75);
            textBox3.Name = "textBox3";
            textBox3.ReadOnly = true;
            textBox3.Size = new Size(339, 31);
            textBox3.TabIndex = 3;
            // 
            // nameTh_textBox
            // 
            nameTh_textBox.Location = new Point(148, 39);
            nameTh_textBox.Name = "nameTh_textBox";
            nameTh_textBox.ReadOnly = true;
            nameTh_textBox.Size = new Size(339, 31);
            nameTh_textBox.TabIndex = 2;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(19, 78);
            label4.Name = "label4";
            label4.Size = new Size(123, 25);
            label4.TabIndex = 1;
            label4.Text = "Middel Name:";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(79, 42);
            label7.Name = "label7";
            label7.Size = new Size(63, 25);
            label7.TabIndex = 0;
            label7.Text = "Name:";
            // 
            // tel_textBox
            // 
            tel_textBox.Location = new Point(130, 79);
            tel_textBox.Name = "tel_textBox";
            tel_textBox.Size = new Size(161, 31);
            tel_textBox.TabIndex = 3;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(88, 85);
            label2.Name = "label2";
            label2.Size = new Size(36, 25);
            label2.TabIndex = 2;
            label2.Text = "Tel:";
            // 
            // nickname_textBox
            // 
            nickname_textBox.Location = new Point(130, 42);
            nickname_textBox.Name = "nickname_textBox";
            nickname_textBox.Size = new Size(407, 31);
            nickname_textBox.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(30, 48);
            label1.Name = "label1";
            label1.Size = new Size(94, 25);
            label1.TabIndex = 0;
            label1.Text = "Nickname:";
            // 
            // register_button
            // 
            register_button.Font = new Font("Segoe UI", 24F, FontStyle.Regular, GraphicsUnit.Point);
            register_button.Location = new Point(28, 971);
            register_button.Name = "register_button";
            register_button.Size = new Size(411, 158);
            register_button.TabIndex = 5;
            register_button.Text = "Register";
            register_button.UseVisualStyleBackColor = true;
            register_button.Click += register_button_Click;
            // 
            // Register
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1378, 1493);
            Controls.Add(register_button);
            Controls.Add(printcard_button);
            Controls.Add(groupBox1);
            Name = "Register";
            Text = "Register";
            Load += OnLoad;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)member_card_back_pictureBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)member_card_front_pictureBox).EndInit();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)personal_pictureBox).EndInit();
            groupBox4.ResumeLayout(false);
            groupBox4.PerformLayout();
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Button printcard_button;
        private GroupBox groupBox1;
        private Label label1;
        private TextBox tel_textBox;
        private Label label2;
        private TextBox nickname_textBox;
        private GroupBox groupBox2;
        private TextBox addressTh_textBox;
        private Label label3;
        private TextBox ed_textBox;
        private Label label11;
        private TextBox ssid_textBox;
        private Label label10;
        private TextBox is_textBox;
        private Label label9;
        private TextBox dob_textBox;
        private Label label8;
        private GroupBox groupBox4;
        private TextBox textBox5;
        private TextBox nameEng_textBox;
        private Label label5;
        private Label label6;
        private GroupBox groupBox3;
        private TextBox textBox3;
        private TextBox nameTh_textBox;
        private Label label4;
        private Label label7;
        private Button register_button;
        private Button readcard_button;
        private PictureBox personal_pictureBox;
        private PictureBox member_card_back_pictureBox;
        private PictureBox member_card_front_pictureBox;
        private ComboBox from_comboBox;
        private Label label12;
    }
}