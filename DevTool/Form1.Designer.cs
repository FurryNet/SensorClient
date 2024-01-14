namespace DevTool
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            mqttUri = new TextBox();
            mqttLogin = new Button();
            mqttLogout = new Button();
            label4 = new Label();
            label5 = new Label();
            mqttStatus = new Label();
            label6 = new Label();
            label7 = new Label();
            label8 = new Label();
            pubName = new TextBox();
            Temperature = new TextBox();
            Humidity = new TextBox();
            sendData = new Button();
            overrideTS = new CheckBox();
            label9 = new Label();
            dateTS = new DateTimePicker();
            timeTS = new DateTimePicker();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(-7, 106);
            label1.Name = "label1";
            label1.Size = new Size(497, 15);
            label1.TabIndex = 0;
            label1.Text = "__________________________________________________________________________________________________";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 15.75F, FontStyle.Underline, GraphicsUnit.Point, 0);
            label2.ForeColor = Color.FromArgb(0, 192, 0);
            label2.Location = new Point(135, 9);
            label2.Name = "label2";
            label2.Size = new Size(195, 30);
            label2.TabIndex = 1;
            label2.Text = "MQTT CREDENTIAL";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 49);
            label3.Name = "label3";
            label3.Size = new Size(62, 15);
            label3.TabIndex = 2;
            label3.Text = "MQTT URI:";
            // 
            // mqttUri
            // 
            mqttUri.Location = new Point(80, 46);
            mqttUri.Name = "mqttUri";
            mqttUri.Size = new Size(393, 23);
            mqttUri.TabIndex = 3;
            // 
            // mqttLogin
            // 
            mqttLogin.Location = new Point(12, 89);
            mqttLogin.Name = "mqttLogin";
            mqttLogin.Size = new Size(75, 23);
            mqttLogin.TabIndex = 4;
            mqttLogin.Text = "Login";
            mqttLogin.UseVisualStyleBackColor = true;
            // 
            // mqttLogout
            // 
            mqttLogout.Enabled = false;
            mqttLogout.Location = new Point(93, 89);
            mqttLogout.Name = "mqttLogout";
            mqttLogout.Size = new Size(75, 23);
            mqttLogout.TabIndex = 5;
            mqttLogout.Text = "Logout";
            mqttLogout.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 15.75F, FontStyle.Underline, GraphicsUnit.Point, 0);
            label4.ForeColor = Color.FromArgb(0, 192, 0);
            label4.Location = new Point(126, 128);
            label4.Name = "label4";
            label4.Size = new Size(220, 30);
            label4.TabIndex = 6;
            label4.Text = "Sample Data Manager";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label5.Location = new Point(246, 91);
            label5.Name = "label5";
            label5.Size = new Size(100, 21);
            label5.TabIndex = 7;
            label5.Text = "MQTT Status:";
            // 
            // mqttStatus
            // 
            mqttStatus.AutoSize = true;
            mqttStatus.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            mqttStatus.ForeColor = Color.Red;
            mqttStatus.Location = new Point(340, 91);
            mqttStatus.Name = "mqttStatus";
            mqttStatus.Size = new Size(61, 21);
            mqttStatus.TabIndex = 8;
            mqttStatus.Text = "Unauth";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(12, 169);
            label6.Name = "label6";
            label6.Size = new Size(84, 15);
            label6.TabIndex = 9;
            label6.Text = "Publish Name:";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(12, 198);
            label7.Name = "label7";
            label7.Size = new Size(76, 15);
            label7.TabIndex = 10;
            label7.Text = "Temperature:";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(14, 227);
            label8.Name = "label8";
            label8.Size = new Size(60, 15);
            label8.TabIndex = 11;
            label8.Text = "Humidity:";
            // 
            // pubName
            // 
            pubName.Location = new Point(102, 166);
            pubName.Name = "pubName";
            pubName.Size = new Size(371, 23);
            pubName.TabIndex = 12;
            // 
            // Temperature
            // 
            Temperature.Location = new Point(102, 195);
            Temperature.Name = "Temperature";
            Temperature.Size = new Size(371, 23);
            Temperature.TabIndex = 13;
            // 
            // Humidity
            // 
            Humidity.Location = new Point(102, 224);
            Humidity.Name = "Humidity";
            Humidity.Size = new Size(371, 23);
            Humidity.TabIndex = 14;
            // 
            // sendData
            // 
            sendData.Enabled = false;
            sendData.Location = new Point(12, 311);
            sendData.Name = "sendData";
            sendData.Size = new Size(334, 23);
            sendData.TabIndex = 15;
            sendData.Text = "Send Sample Data";
            sendData.UseVisualStyleBackColor = true;
            // 
            // overrideTS
            // 
            overrideTS.AutoSize = true;
            overrideTS.Location = new Point(352, 314);
            overrideTS.Name = "overrideTS";
            overrideTS.Size = new Size(133, 19);
            overrideTS.TabIndex = 16;
            overrideTS.Text = "Override Timestamp";
            overrideTS.UseVisualStyleBackColor = true;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(14, 259);
            label9.Name = "label9";
            label9.Size = new Size(69, 15);
            label9.TabIndex = 17;
            label9.Text = "Timestamp:";
            // 
            // dateTS
            // 
            dateTS.Location = new Point(102, 253);
            dateTS.Name = "dateTS";
            dateTS.Size = new Size(223, 23);
            dateTS.TabIndex = 18;
            // 
            // timeTS
            // 
            timeTS.Format = DateTimePickerFormat.Time;
            timeTS.Location = new Point(331, 253);
            timeTS.Name = "timeTS";
            timeTS.ShowUpDown = true;
            timeTS.Size = new Size(142, 23);
            timeTS.TabIndex = 19;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(485, 346);
            Controls.Add(timeTS);
            Controls.Add(dateTS);
            Controls.Add(label9);
            Controls.Add(overrideTS);
            Controls.Add(sendData);
            Controls.Add(Humidity);
            Controls.Add(Temperature);
            Controls.Add(pubName);
            Controls.Add(label8);
            Controls.Add(label7);
            Controls.Add(label6);
            Controls.Add(mqttStatus);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(mqttLogout);
            Controls.Add(mqttLogin);
            Controls.Add(mqttUri);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Form1";
            Text = "Sensor Server DevTool";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private Label label3;
        private TextBox mqttUri;
        private Button mqttLogin;
        private Button mqttLogout;
        private Label label4;
        private Label label5;
        private Label mqttStatus;
        private Label label6;
        private Label label7;
        private Label label8;
        private TextBox pubName;
        private TextBox Temperature;
        private TextBox Humidity;
        private Button sendData;
        private CheckBox overrideTS;
        private Label label9;
        private DateTimePicker dateTS;
        private DateTimePicker timeTS;
    }
}
