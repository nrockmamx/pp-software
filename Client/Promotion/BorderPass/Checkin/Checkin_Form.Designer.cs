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
            history_dataGridView = new DataGridView();
            groupBox2 = new GroupBox();
            money_textBox = new TextBox();
            label4 = new Label();
            phone_textBox = new TextBox();
            label3 = new Label();
            label2 = new Label();
            status_label = new Label();
            mrtds_textBox = new TextBox();
            label1 = new Label();
            passport_id_textBox = new TextBox();
            label5 = new Label();
            nameEng_textBox = new TextBox();
            label6 = new Label();
            camera_pictureBox = new PictureBox();
            passport_pictureBox = new PictureBox();
            ed_textBox = new TextBox();
            label11 = new Label();
            ssid_textBox = new TextBox();
            label10 = new Label();
            dob_textBox = new TextBox();
            label8 = new Label();
            groupBox1.SuspendLayout();
            groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)history_dataGridView).BeginInit();
            groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)camera_pictureBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)passport_pictureBox).BeginInit();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(checkin_button);
            groupBox1.Controls.Add(groupBox5);
            groupBox1.Controls.Add(groupBox2);
            groupBox1.Location = new Point(29, 19);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(1485, 1032);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Passport CheckIn ";
            // 
            // checkin_button
            // 
            checkin_button.Enabled = false;
            checkin_button.Font = new Font("Segoe UI", 26F, FontStyle.Bold, GraphicsUnit.Point);
            checkin_button.Location = new Point(1052, 585);
            checkin_button.Name = "checkin_button";
            checkin_button.Size = new Size(382, 427);
            checkin_button.TabIndex = 3;
            checkin_button.Text = "Check In";
            checkin_button.UseVisualStyleBackColor = true;
            checkin_button.Click += checkin_button_Click;
            // 
            // groupBox5
            // 
            groupBox5.Controls.Add(history_dataGridView);
            groupBox5.Location = new Point(17, 572);
            groupBox5.Name = "groupBox5";
            groupBox5.Size = new Size(1029, 440);
            groupBox5.TabIndex = 2;
            groupBox5.TabStop = false;
            groupBox5.Text = "History";
            // 
            // history_dataGridView
            // 
            history_dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            history_dataGridView.Location = new Point(29, 34);
            history_dataGridView.Name = "history_dataGridView";
            history_dataGridView.ReadOnly = true;
            history_dataGridView.RowHeadersWidth = 62;
            history_dataGridView.RowTemplate.Height = 33;
            history_dataGridView.Size = new Size(981, 379);
            history_dataGridView.TabIndex = 0;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(money_textBox);
            groupBox2.Controls.Add(label4);
            groupBox2.Controls.Add(phone_textBox);
            groupBox2.Controls.Add(label3);
            groupBox2.Controls.Add(label2);
            groupBox2.Controls.Add(status_label);
            groupBox2.Controls.Add(mrtds_textBox);
            groupBox2.Controls.Add(label1);
            groupBox2.Controls.Add(passport_id_textBox);
            groupBox2.Controls.Add(label5);
            groupBox2.Controls.Add(nameEng_textBox);
            groupBox2.Controls.Add(label6);
            groupBox2.Controls.Add(camera_pictureBox);
            groupBox2.Controls.Add(passport_pictureBox);
            groupBox2.Controls.Add(ed_textBox);
            groupBox2.Controls.Add(label11);
            groupBox2.Controls.Add(ssid_textBox);
            groupBox2.Controls.Add(label10);
            groupBox2.Controls.Add(dob_textBox);
            groupBox2.Controls.Add(label8);
            groupBox2.Location = new Point(22, 30);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(1426, 536);
            groupBox2.TabIndex = 0;
            groupBox2.TabStop = false;
            groupBox2.Text = "Personal Detail";
            // 
            // money_textBox
            // 
            money_textBox.Location = new Point(125, 374);
            money_textBox.Name = "money_textBox";
            money_textBox.Size = new Size(140, 31);
            money_textBox.TabIndex = 27;
            money_textBox.Text = "200";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(48, 377);
            label4.Name = "label4";
            label4.Size = new Size(71, 25);
            label4.TabIndex = 28;
            label4.Text = "Money:";
            // 
            // phone_textBox
            // 
            phone_textBox.Location = new Point(125, 337);
            phone_textBox.Name = "phone_textBox";
            phone_textBox.Size = new Size(339, 31);
            phone_textBox.TabIndex = 25;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(24, 340);
            label3.Name = "label3";
            label3.Size = new Size(95, 25);
            label3.TabIndex = 26;
            label3.Text = "Phone No:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 36F, FontStyle.Regular, GraphicsUnit.Point);
            label2.Location = new Point(483, 341);
            label2.Name = "label2";
            label2.Size = new Size(249, 96);
            label2.TabIndex = 24;
            label2.Text = "Status:";
            label2.TextAlign = ContentAlignment.TopCenter;
            // 
            // status_label
            // 
            status_label.AutoSize = true;
            status_label.Font = new Font("Segoe UI", 36F, FontStyle.Regular, GraphicsUnit.Point);
            status_label.Location = new Point(717, 341);
            status_label.Name = "status_label";
            status_label.Size = new Size(670, 96);
            status_label.TabIndex = 23;
            status_label.Text = "Wait Insert Passport";
            status_label.TextAlign = ContentAlignment.TopCenter;
            // 
            // mrtds_textBox
            // 
            mrtds_textBox.Location = new Point(125, 215);
            mrtds_textBox.Multiline = true;
            mrtds_textBox.Name = "mrtds_textBox";
            mrtds_textBox.ReadOnly = true;
            mrtds_textBox.Size = new Size(339, 116);
            mrtds_textBox.TabIndex = 22;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(45, 215);
            label1.Name = "label1";
            label1.Size = new Size(74, 25);
            label1.TabIndex = 21;
            label1.Text = "MRTDS:";
            // 
            // passport_id_textBox
            // 
            passport_id_textBox.Location = new Point(125, 67);
            passport_id_textBox.Name = "passport_id_textBox";
            passport_id_textBox.ReadOnly = true;
            passport_id_textBox.Size = new Size(339, 31);
            passport_id_textBox.TabIndex = 3;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(12, 70);
            label5.Name = "label5";
            label5.Size = new Size(107, 25);
            label5.TabIndex = 1;
            label5.Text = "Passport ID:";
            // 
            // nameEng_textBox
            // 
            nameEng_textBox.Location = new Point(125, 30);
            nameEng_textBox.Name = "nameEng_textBox";
            nameEng_textBox.ReadOnly = true;
            nameEng_textBox.Size = new Size(339, 31);
            nameEng_textBox.TabIndex = 2;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(56, 33);
            label6.Name = "label6";
            label6.Size = new Size(63, 25);
            label6.TabIndex = 0;
            label6.Text = "Name:";
            // 
            // camera_pictureBox
            // 
            camera_pictureBox.BorderStyle = BorderStyle.Fixed3D;
            camera_pictureBox.Location = new Point(483, 33);
            camera_pictureBox.Name = "camera_pictureBox";
            camera_pictureBox.Size = new Size(426, 225);
            camera_pictureBox.TabIndex = 19;
            camera_pictureBox.TabStop = false;
            // 
            // passport_pictureBox
            // 
            passport_pictureBox.BorderStyle = BorderStyle.Fixed3D;
            passport_pictureBox.Location = new Point(924, 33);
            passport_pictureBox.Name = "passport_pictureBox";
            passport_pictureBox.Size = new Size(488, 225);
            passport_pictureBox.TabIndex = 18;
            passport_pictureBox.TabStop = false;
            // 
            // ed_textBox
            // 
            ed_textBox.Location = new Point(125, 141);
            ed_textBox.Name = "ed_textBox";
            ed_textBox.ReadOnly = true;
            ed_textBox.Size = new Size(339, 31);
            ed_textBox.TabIndex = 14;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(16, 143);
            label11.Name = "label11";
            label11.Size = new Size(105, 25);
            label11.TabIndex = 15;
            label11.Text = "Expire Date:";
            // 
            // ssid_textBox
            // 
            ssid_textBox.Location = new Point(125, 178);
            ssid_textBox.Name = "ssid_textBox";
            ssid_textBox.ReadOnly = true;
            ssid_textBox.Size = new Size(339, 31);
            ssid_textBox.TabIndex = 12;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(10, 181);
            label10.Name = "label10";
            label10.Size = new Size(111, 25);
            label10.TabIndex = 13;
            label10.Text = "Personal No:";
            // 
            // dob_textBox
            // 
            dob_textBox.Location = new Point(125, 104);
            dob_textBox.Name = "dob_textBox";
            dob_textBox.ReadOnly = true;
            dob_textBox.Size = new Size(339, 31);
            dob_textBox.TabIndex = 8;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(2, 110);
            label8.Name = "label8";
            label8.Size = new Size(117, 25);
            label8.TabIndex = 9;
            label8.Text = "Date of birth:";
            // 
            // Checkin_Form
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1517, 1079);
            Controls.Add(groupBox1);
            Name = "Checkin_Form";
            Text = "CheckingVoucher";
            Load += LoadFrom;
            groupBox1.ResumeLayout(false);
            groupBox5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)history_dataGridView).EndInit();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)camera_pictureBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)passport_pictureBox).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private TextBox passport_id_textBox;
        private TextBox nameEng_textBox;
        private Label label5;
        private Label label6;
        private PictureBox passport_pictureBox;
        private TextBox ed_textBox;
        private Label label11;
        private TextBox ssid_textBox;
        private Label label10;
        private TextBox is_textBox;
        private Label label9;
        private TextBox dob_textBox;
        private Label label8;
        private GroupBox groupBox5;
        private DataGridView history_dataGridView;
        private PictureBox camera_pictureBox;
        private Button checkin_button;
        private TextBox mrtds_textBox;
        private Label label1;
        private Label status_label;
        private Label label2;
        private TextBox phone_textBox;
        private Label label3;
        private TextBox money_textBox;
        private Label label4;
    }
}