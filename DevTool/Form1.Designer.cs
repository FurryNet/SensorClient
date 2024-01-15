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
            mqttIP = new TextBox();
            mqttLogin = new Button();
            mqttLogout = new Button();
            label4 = new Label();
            label5 = new Label();
            mqttStatus = new Label();
            label6 = new Label();
            label7 = new Label();
            label8 = new Label();
            mqttTopic = new TextBox();
            Temperature = new TextBox();
            Humidity = new TextBox();
            sendData = new Button();
            overrideTS = new CheckBox();
            label9 = new Label();
            dateTS = new DateTimePicker();
            timeTS = new DateTimePicker();
            label10 = new Label();
            Identifier = new TextBox();
            label11 = new Label();
            mqttPort = new TextBox();
            mqttTLS = new CheckBox();
            label12 = new Label();
            label13 = new Label();
            mqttUsername = new TextBox();
            mqttPassword = new TextBox();
            SkipValidation = new CheckBox();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(-7, 171);
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
            label3.Size = new Size(42, 15);
            label3.TabIndex = 2;
            label3.Text = "IP/DN:";
            // 
            // mqttIP
            // 
            mqttIP.Location = new Point(60, 46);
            mqttIP.Name = "mqttIP";
            mqttIP.Size = new Size(270, 23);
            mqttIP.TabIndex = 1;
            // 
            // mqttLogin
            // 
            mqttLogin.Location = new Point(12, 145);
            mqttLogin.Name = "mqttLogin";
            mqttLogin.Size = new Size(75, 23);
            mqttLogin.TabIndex = 6;
            mqttLogin.Text = "Login";
            mqttLogin.UseVisualStyleBackColor = true;
            mqttLogin.Click += mqttLogin_Click;
            // 
            // mqttLogout
            // 
            mqttLogout.Enabled = false;
            mqttLogout.Location = new Point(93, 145);
            mqttLogout.Name = "mqttLogout";
            mqttLogout.Size = new Size(75, 23);
            mqttLogout.TabIndex = 7;
            mqttLogout.Text = "Logout";
            mqttLogout.UseVisualStyleBackColor = true;
            mqttLogout.Click += mqttLogout_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 15.75F, FontStyle.Underline, GraphicsUnit.Point, 0);
            label4.ForeColor = Color.FromArgb(0, 192, 0);
            label4.Location = new Point(126, 193);
            label4.Name = "label4";
            label4.Size = new Size(220, 30);
            label4.TabIndex = 6;
            label4.Text = "Sample Data Manager";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label5.Location = new Point(246, 144);
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
            mqttStatus.Location = new Point(340, 144);
            mqttStatus.Name = "mqttStatus";
            mqttStatus.Size = new Size(103, 21);
            mqttStatus.TabIndex = 8;
            mqttStatus.Text = "Disconnected";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(12, 234);
            label6.Name = "label6";
            label6.Size = new Size(84, 15);
            label6.TabIndex = 9;
            label6.Text = "Publish Name:";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(12, 263);
            label7.Name = "label7";
            label7.Size = new Size(76, 15);
            label7.TabIndex = 10;
            label7.Text = "Temperature:";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(14, 292);
            label8.Name = "label8";
            label8.Size = new Size(60, 15);
            label8.TabIndex = 11;
            label8.Text = "Humidity:";
            // 
            // mqttTopic
            // 
            mqttTopic.Enabled = false;
            mqttTopic.Location = new Point(102, 231);
            mqttTopic.Name = "mqttTopic";
            mqttTopic.Size = new Size(371, 23);
            mqttTopic.TabIndex = 8;
            mqttTopic.Text = "SensorRecord";
            // 
            // Temperature
            // 
            Temperature.Enabled = false;
            Temperature.Location = new Point(102, 260);
            Temperature.Name = "Temperature";
            Temperature.Size = new Size(371, 23);
            Temperature.TabIndex = 9;
            // 
            // Humidity
            // 
            Humidity.Enabled = false;
            Humidity.Location = new Point(102, 289);
            Humidity.Name = "Humidity";
            Humidity.Size = new Size(371, 23);
            Humidity.TabIndex = 10;
            // 
            // sendData
            // 
            sendData.Enabled = false;
            sendData.Location = new Point(12, 395);
            sendData.Name = "sendData";
            sendData.Size = new Size(196, 23);
            sendData.TabIndex = 14;
            sendData.Text = "Send Sample Data";
            sendData.UseVisualStyleBackColor = true;
            sendData.Click += sendData_Click;
            // 
            // overrideTS
            // 
            overrideTS.AutoSize = true;
            overrideTS.Enabled = false;
            overrideTS.Location = new Point(214, 398);
            overrideTS.Name = "overrideTS";
            overrideTS.Size = new Size(133, 19);
            overrideTS.TabIndex = 15;
            overrideTS.Text = "Override Timestamp";
            overrideTS.UseVisualStyleBackColor = true;
            overrideTS.CheckedChanged += overrideTS_CheckedChanged;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(14, 350);
            label9.Name = "label9";
            label9.Size = new Size(69, 15);
            label9.TabIndex = 17;
            label9.Text = "Timestamp:";
            // 
            // dateTS
            // 
            dateTS.Enabled = false;
            dateTS.Location = new Point(102, 347);
            dateTS.MinDate = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            dateTS.Name = "dateTS";
            dateTS.Size = new Size(223, 23);
            dateTS.TabIndex = 12;
            // 
            // timeTS
            // 
            timeTS.Enabled = false;
            timeTS.Format = DateTimePickerFormat.Time;
            timeTS.Location = new Point(331, 347);
            timeTS.Name = "timeTS";
            timeTS.ShowUpDown = true;
            timeTS.Size = new Size(142, 23);
            timeTS.TabIndex = 13;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(14, 321);
            label10.Name = "label10";
            label10.Size = new Size(57, 15);
            label10.TabIndex = 20;
            label10.Text = "Identifier:";
            // 
            // Identifier
            // 
            Identifier.Enabled = false;
            Identifier.Location = new Point(102, 318);
            Identifier.Name = "Identifier";
            Identifier.Size = new Size(371, 23);
            Identifier.TabIndex = 11;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(335, 49);
            label11.Name = "label11";
            label11.Size = new Size(32, 15);
            label11.TabIndex = 22;
            label11.Text = "Port:";
            // 
            // mqttPort
            // 
            mqttPort.Location = new Point(373, 46);
            mqttPort.Name = "mqttPort";
            mqttPort.Size = new Size(100, 23);
            mqttPort.TabIndex = 2;
            mqttPort.Text = "1883";
            // 
            // mqttTLS
            // 
            mqttTLS.AutoSize = true;
            mqttTLS.Location = new Point(174, 148);
            mqttTLS.Name = "mqttTLS";
            mqttTLS.Size = new Size(66, 19);
            mqttTLS.TabIndex = 5;
            mqttTLS.Text = "Use TLS";
            mqttTLS.UseVisualStyleBackColor = true;
            mqttTLS.CheckedChanged += mqttTLS_CheckedChanged;
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Location = new Point(11, 78);
            label12.Name = "label12";
            label12.Size = new Size(63, 15);
            label12.TabIndex = 25;
            label12.Text = "Username:";
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Location = new Point(11, 107);
            label13.Name = "label13";
            label13.Size = new Size(60, 15);
            label13.TabIndex = 26;
            label13.Text = "Password:";
            // 
            // mqttUsername
            // 
            mqttUsername.Location = new Point(81, 75);
            mqttUsername.Name = "mqttUsername";
            mqttUsername.Size = new Size(392, 23);
            mqttUsername.TabIndex = 3;
            // 
            // mqttPassword
            // 
            mqttPassword.Location = new Point(81, 104);
            mqttPassword.Name = "mqttPassword";
            mqttPassword.PasswordChar = '*';
            mqttPassword.Size = new Size(392, 23);
            mqttPassword.TabIndex = 4;
            // 
            // SkipValidation
            // 
            SkipValidation.AutoSize = true;
            SkipValidation.Enabled = false;
            SkipValidation.Location = new Point(353, 399);
            SkipValidation.Name = "SkipValidation";
            SkipValidation.Size = new Size(130, 19);
            SkipValidation.TabIndex = 27;
            SkipValidation.Text = "Skip Data Validation";
            SkipValidation.UseVisualStyleBackColor = true;
            SkipValidation.CheckedChanged += SkipValidation_CheckedChanged;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(485, 430);
            Controls.Add(SkipValidation);
            Controls.Add(mqttPassword);
            Controls.Add(mqttUsername);
            Controls.Add(label13);
            Controls.Add(label12);
            Controls.Add(mqttTLS);
            Controls.Add(mqttPort);
            Controls.Add(label11);
            Controls.Add(Identifier);
            Controls.Add(label10);
            Controls.Add(timeTS);
            Controls.Add(dateTS);
            Controls.Add(label9);
            Controls.Add(overrideTS);
            Controls.Add(sendData);
            Controls.Add(Humidity);
            Controls.Add(Temperature);
            Controls.Add(mqttTopic);
            Controls.Add(label8);
            Controls.Add(label7);
            Controls.Add(label6);
            Controls.Add(mqttStatus);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(mqttLogout);
            Controls.Add(mqttLogin);
            Controls.Add(mqttIP);
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
        private TextBox mqttIP;
        private Button mqttLogin;
        private Button mqttLogout;
        private Label label4;
        private Label label5;
        private Label mqttStatus;
        private Label label6;
        private Label label7;
        private Label label8;
        private TextBox mqttTopic;
        private TextBox Temperature;
        private TextBox Humidity;
        private Button sendData;
        private CheckBox overrideTS;
        private Label label9;
        private DateTimePicker dateTS;
        private DateTimePicker timeTS;
        private Label label10;
        private TextBox Identifier;
        private Label label11;
        private TextBox mqttPort;
        private CheckBox mqttTLS;
        private Label label12;
        private Label label13;
        private TextBox mqttUsername;
        private TextBox mqttPassword;
        private CheckBox SkipValidation;
    }
}
