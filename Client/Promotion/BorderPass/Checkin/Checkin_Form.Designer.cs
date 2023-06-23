namespace Client.Promotion.BorderPass.CheckingVoucher
{
    partial class Checkin_Form
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
            groupBox1 = new GroupBox();
            checkin_button = new Button();
            groupBox5 = new GroupBox();
            dataGridView1 = new DataGridView();
            read_card_button = new Button();
            groupBox2 = new GroupBox();
            addressTh_textBox = new TextBox();
            label3 = new Label();
            border_pass_pictureBox = new PictureBox();
            personal_pictureBox = new PictureBox();
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
            textBox2 = new TextBox();
            nameTh_textBox = new TextBox();
            label2 = new Label();
            label1 = new Label();
            groupBox1.SuspendLayout();
            groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)border_pass_pictureBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)personal_pictureBox).BeginInit();
            groupBox4.SuspendLayout();
            groupBox3.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(checkin_button);
            groupBox1.Controls.Add(groupBox5);
            groupBox1.Controls.Add(read_card_button);
            groupBox1.Controls.Add(groupBox2);
            groupBox1.Location = new Point(29, 19);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(1485, 989);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Border Pass CheckIn ";
            // 
            // checkin_button
            // 
            checkin_button.Enabled = false;
            checkin_button.Font = new Font("Segoe UI", 26F, FontStyle.Bold, GraphicsUnit.Point);
            checkin_button.Location = new Point(1052, 702);
            checkin_button.Name = "checkin_button";
            checkin_button.Size = new Size(382, 93);
            checkin_button.TabIndex = 3;
            checkin_button.Text = "Check In";
            checkin_button.UseVisualStyleBackColor = true;
            // 
            // groupBox5
            // 
            groupBox5.Controls.Add(dataGridView1);
            groupBox5.Location = new Point(17, 572);
            groupBox5.Name = "groupBox5";
            groupBox5.Size = new Size(1029, 400);
            groupBox5.TabIndex = 2;
            groupBox5.TabStop = false;
            groupBox5.Text = "History";
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(29, 34);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 62;
            dataGridView1.RowTemplate.Height = 33;
            dataGridView1.Size = new Size(981, 289);
            dataGridView1.TabIndex = 0;
            // 
            // read_card_button
            // 
            read_card_button.Font = new Font("Segoe UI", 26F, FontStyle.Bold, GraphicsUnit.Point);
            read_card_button.Location = new Point(1052, 584);
            read_card_button.Name = "read_card_button";
            read_card_button.Size = new Size(382, 93);
            read_card_button.TabIndex = 1;
            read_card_button.Text = "Read Card";
            read_card_button.UseVisualStyleBackColor = true;
            read_card_button.Click += get_voucher_button_Click;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(addressTh_textBox);
            groupBox2.Controls.Add(label3);
            groupBox2.Controls.Add(border_pass_pictureBox);
            groupBox2.Controls.Add(personal_pictureBox);
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
            groupBox2.Location = new Point(22, 30);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(1426, 536);
            groupBox2.TabIndex = 0;
            groupBox2.TabStop = false;
            groupBox2.Text = "Personal Detail";
            // 
            // addressTh_textBox
            // 
            addressTh_textBox.Location = new Point(173, 153);
            addressTh_textBox.Multiline = true;
            addressTh_textBox.Name = "addressTh_textBox";
            addressTh_textBox.ReadOnly = true;
            addressTh_textBox.Size = new Size(341, 183);
            addressTh_textBox.TabIndex = 5;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(86, 153);
            label3.Name = "label3";
            label3.Size = new Size(81, 25);
            label3.TabIndex = 4;
            label3.Text = "Address:";
            // 
            // border_pass_pictureBox
            // 
            border_pass_pictureBox.BorderStyle = BorderStyle.Fixed3D;
            border_pass_pictureBox.Location = new Point(1076, 18);
            border_pass_pictureBox.Name = "border_pass_pictureBox";
            border_pass_pictureBox.Size = new Size(322, 267);
            border_pass_pictureBox.TabIndex = 19;
            border_pass_pictureBox.TabStop = false;
            // 
            // personal_pictureBox
            // 
            personal_pictureBox.BorderStyle = BorderStyle.Fixed3D;
            personal_pictureBox.Location = new Point(1110, 291);
            personal_pictureBox.Name = "personal_pictureBox";
            personal_pictureBox.Size = new Size(275, 230);
            personal_pictureBox.TabIndex = 18;
            personal_pictureBox.TabStop = false;
            // 
            // ed_textBox
            // 
            ed_textBox.Location = new Point(687, 152);
            ed_textBox.Name = "ed_textBox";
            ed_textBox.ReadOnly = true;
            ed_textBox.Size = new Size(339, 31);
            ed_textBox.TabIndex = 14;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(578, 154);
            label11.Name = "label11";
            label11.Size = new Size(105, 25);
            label11.TabIndex = 15;
            label11.Text = "Expire Date:";
            // 
            // ssid_textBox
            // 
            ssid_textBox.Location = new Point(687, 189);
            ssid_textBox.Name = "ssid_textBox";
            ssid_textBox.ReadOnly = true;
            ssid_textBox.Size = new Size(339, 31);
            ssid_textBox.TabIndex = 12;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(607, 192);
            label10.Name = "label10";
            label10.Size = new Size(74, 25);
            label10.TabIndex = 13;
            label10.Text = "Card Id:";
            // 
            // is_textBox
            // 
            is_textBox.Location = new Point(687, 266);
            is_textBox.Name = "is_textBox";
            is_textBox.ReadOnly = true;
            is_textBox.Size = new Size(339, 31);
            is_textBox.TabIndex = 10;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(602, 272);
            label9.Name = "label9";
            label9.Size = new Size(79, 25);
            label9.TabIndex = 11;
            label9.Text = "Issue At:";
            // 
            // dob_textBox
            // 
            dob_textBox.Location = new Point(687, 228);
            dob_textBox.Name = "dob_textBox";
            dob_textBox.ReadOnly = true;
            dob_textBox.Size = new Size(339, 31);
            dob_textBox.TabIndex = 8;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(564, 234);
            label8.Name = "label8";
            label8.Size = new Size(117, 25);
            label8.TabIndex = 9;
            label8.Text = "Date of birth:";
            // 
            // groupBox4
            // 
            groupBox4.Controls.Add(textBox5);
            groupBox4.Controls.Add(nameEng_textBox);
            groupBox4.Controls.Add(label5);
            groupBox4.Controls.Add(label6);
            groupBox4.Location = new Point(539, 30);
            groupBox4.Name = "groupBox4";
            groupBox4.Size = new Size(493, 116);
            groupBox4.TabIndex = 6;
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
            groupBox3.Controls.Add(textBox2);
            groupBox3.Controls.Add(nameTh_textBox);
            groupBox3.Controls.Add(label2);
            groupBox3.Controls.Add(label1);
            groupBox3.Location = new Point(25, 30);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(493, 116);
            groupBox3.TabIndex = 0;
            groupBox3.TabStop = false;
            groupBox3.Text = "Thai";
            // 
            // textBox2
            // 
            textBox2.Location = new Point(148, 75);
            textBox2.Name = "textBox2";
            textBox2.ReadOnly = true;
            textBox2.Size = new Size(339, 31);
            textBox2.TabIndex = 3;
            // 
            // nameTh_textBox
            // 
            nameTh_textBox.Location = new Point(148, 39);
            nameTh_textBox.Name = "nameTh_textBox";
            nameTh_textBox.ReadOnly = true;
            nameTh_textBox.Size = new Size(339, 31);
            nameTh_textBox.TabIndex = 2;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(19, 78);
            label2.Name = "label2";
            label2.Size = new Size(123, 25);
            label2.TabIndex = 1;
            label2.Text = "Middel Name:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(79, 42);
            label1.Name = "label1";
            label1.Size = new Size(63, 25);
            label1.TabIndex = 0;
            label1.Text = "Name:";
            // 
            // Checkin_Form
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1517, 1079);
            Controls.Add(groupBox1);
            Name = "Checkin_Form";
            Text = "CheckingVoucher";
            groupBox1.ResumeLayout(false);
            groupBox5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)border_pass_pictureBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)personal_pictureBox).EndInit();
            groupBox4.ResumeLayout(false);
            groupBox4.PerformLayout();
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox1;
        private Button read_card_button;
        private GroupBox groupBox2;
        private GroupBox groupBox3;
        private TextBox nameTh_textBox;
        private Label label2;
        private Label label1;
        private TextBox addressTh_textBox;
        private Label label3;
        private TextBox textBox2;
        private GroupBox groupBox4;
        private TextBox textBox5;
        private TextBox nameEng_textBox;
        private Label label5;
        private Label label6;
        private PictureBox personal_pictureBox;
        private TextBox ed_textBox;
        private Label label11;
        private TextBox ssid_textBox;
        private Label label10;
        private TextBox is_textBox;
        private Label label9;
        private TextBox dob_textBox;
        private Label label8;
        private GroupBox groupBox5;
        private DataGridView dataGridView1;
        private PictureBox border_pass_pictureBox;
        private Button checkin_button;
    }
}