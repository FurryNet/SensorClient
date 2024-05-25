using MQTTnet;
using MQTTnet.Client;
using ProtoBuf;
using MQTTnet.Protocol;

namespace DevTool
{
    public partial class Form1 : Form
    {
        MqttFactory factory = new MqttFactory();
        IMqttClient client;
        public Form1()
        {
            InitializeComponent();
        }

        private void overrideTS_CheckedChanged(object sender, EventArgs e)
        {
            dateTS.Enabled = overrideTS.Checked;
            timeTS.Enabled = overrideTS.Checked;
        }

        private async void mqttLogin_Click(object sender, EventArgs e)
        {
#if DEBUG
            // allow fake login to test UI.
            if(string.IsNullOrWhiteSpace(mqttIP.Text))
            {
                formControl(true, "Connected (Emu)", Color.Green);
                MessageBox.Show("You're currently in emulated login mode. Certain functionality will be disable as it requires real login.", "Emulated Login", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
#endif
            // User Input Validation

            if (string.IsNullOrWhiteSpace(mqttIP.Text))
            {
                MessageBox.Show("Invalid IP Address", "Bad IP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (
                !int.TryParse(mqttPort.Text, out int port) ||
                port < 1 ||
                port > 65535
               )
            {
                MessageBox.Show("Invalid port number", "Bad Port", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (
                !string.IsNullOrWhiteSpace(mqttUsername.Text) &&
                string.IsNullOrWhiteSpace(mqttPassword.Text)
               )
            {
                MessageBox.Show("Missing Password", "Bad Credentials", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (
                string.IsNullOrWhiteSpace(mqttUsername.Text) &&
                !string.IsNullOrWhiteSpace(mqttPassword.Text)
               )
            {
                MessageBox.Show("Missing Username", "Bad Credentials", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Start Constructing MQTT Client

            client = factory.CreateMqttClient();
            string clientId = Guid.NewGuid().ToString();
            MqttClientOptionsBuilder cliOpt = new MqttClientOptionsBuilder()
                .WithClientId("DevTool_" + clientId)
                .WithTcpServer(mqttIP.Text, port)
                .WithWillQualityOfServiceLevel(MqttQualityOfServiceLevel.AtLeastOnce)
                .WithProtocolVersion(MQTTnet.Formatter.MqttProtocolVersion.V500);

            if (
                !string.IsNullOrWhiteSpace(mqttUsername.Text) &&
                !string.IsNullOrWhiteSpace(mqttPassword.Text)
              )
                cliOpt.WithCredentials(mqttUsername.Text, mqttPassword.Text);

            if (mqttTLS.Checked)
                cliOpt.WithTlsOptions((o) =>
                    o.WithCertificateValidationHandler((c) => true)
                );

            // Attach Event Handlers
            client.DisconnectedAsync += mqttDisconnect_Event;
            client.ConnectedAsync += mqttConnect_Event;
            // Connect to MQTT Broker
            try
            {
                using (CancellationTokenSource cts = new CancellationTokenSource(10000))
                {
                    MqttClientConnectResult status = await client.ConnectAsync(cliOpt.Build(), cts.Token);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                client.Dispose();
                return;
            }
        }

        private void mqttTLS_CheckedChanged(object sender, EventArgs e)
        {
            if (mqttPort.Text == "1883" && mqttTLS.Checked)
                mqttPort.Text = "8883";
            else if (mqttPort.Text == "8883" && !mqttTLS.Checked)
                mqttPort.Text = "1883";
        }

        private async Task mqttDisconnect_Event(MqttClientDisconnectedEventArgs e)
        {
            client.Dispose();
            formControl(false);
        }

        private async Task mqttConnect_Event(MqttClientConnectedEventArgs e)
        {
            // Now enable the rest of the UI
            formControl(true);
        }

        private async void mqttLogout_Click(object sender, EventArgs e)
        {
#if DEBUG
            // Check if we're in emulated mode
            if (isEmulatedMode())
            {
                formControl(false);
                return;
            }
#endif
            await client.DisconnectAsync();
        }

        public void formControl(bool isAuth, string? customStatText = null, Color? customStatColor = null)
        {
            mqttStatus.Invoke(new MethodInvoker(delegate ()
            {
                mqttStatus.Text = !string.IsNullOrWhiteSpace(customStatText) ? customStatText : (isAuth ? "Connected" : "Disconnected");
                mqttStatus.ForeColor = customStatColor ?? (isAuth ? Color.Green : Color.Red);
                mqttLogin.Enabled = !isAuth;
                mqttIP.Enabled = !isAuth;
                mqttPort.Enabled = !isAuth;
                mqttUsername.Enabled = !isAuth;
                mqttPassword.Enabled = !isAuth;
                mqttTLS.Enabled = !isAuth;
                mqttLogout.Enabled = isAuth;
                mqttTopic.Enabled = isAuth;
                Temperature.Enabled = isAuth;
                Humidity.Enabled = isAuth;
                Identifier.Enabled = isAuth;
                overrideTS.Enabled = isAuth;
                sendData.Enabled = isAuth;
                SkipValidation.Enabled = isAuth;
                dateTS.Enabled = isAuth && overrideTS.Checked;
                timeTS.Enabled = isAuth && overrideTS.Checked;
            }));
        }

        private async void sendData_Click(object sender, EventArgs e)
        {
            // Input Validation

            // Start Mandatory Validation
            if (!double.TryParse(Temperature.Text, out double temp))
            {
                MessageBox.Show("Invalid Temperature Input", "Bad Temperature", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!double.TryParse(Humidity.Text, out double hum))
            {
                MessageBox.Show("Invalid Humidity Input", "Bad Humidity", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!SkipValidation.Checked)
            {
                if (string.IsNullOrWhiteSpace(mqttTopic.Text))
                {
                    MessageBox.Show("Invalid Topic", "Bad Topic", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (
                    temp < -40 ||
                    temp > 125
                   )
                {
                    MessageBox.Show("Invalid Temperature Value", "Bad Temperature", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (
                    hum < 0 ||
                    hum > 100
                                                                               )
                {
                    MessageBox.Show("Invalid Humidity Value", "Bad Humidity", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (
                    !string.IsNullOrWhiteSpace(Identifier.Text) &&
                    Identifier.Text.Length > 92
                   )
                {
                    MessageBox.Show("Identifier too long, maximum 92 characters (though it's actually 100 but devtool reserved the other 8)", "Bad Identifier", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                // Check if the date exceeded the current date
                if (overrideTS.Checked)
                {
                    DateTime manualDate = new DateTime(
                        dateTS.Value.Year,
                        dateTS.Value.Month,
                        dateTS.Value.Day,
                        timeTS.Value.Hour,
                        timeTS.Value.Minute,
                        timeTS.Value.Second,
                        timeTS.Value.Millisecond);
                    if (manualDate > DateTime.UtcNow)
                    {
                        MessageBox.Show("Overridden date cannot exceed your system date", "Bad Date", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }
#if DEBUG
            // Check if we're in emulated mode
            if (isEmulatedMode())
            {
                MessageBox.Show("In non-emulated mode, data would be processed to be sent", "Valid Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
#endif
            // Start Serializing Data
            DateTime dateTime = overrideTS.Checked ?
                new DateTime(dateTS.Value.Year,
                dateTS.Value.Month,
                dateTS.Value.Day,
                timeTS.Value.Hour,
                timeTS.Value.Minute,
                timeTS.Value.Second,
                timeTS.Value.Millisecond) :
                DateTime.UtcNow;

            QueueData data = new QueueData
            {
                temperature = temp,
                humidity = hum,
                identifier = "DevTool_" + Identifier.Text,
                timestamp = (ulong)Math.Round(dateTime.Subtract(new DateTime(1970, 1, 1)).TotalMilliseconds)
            };
            using (MemoryStream buff = new MemoryStream())
            {
                Serializer.Serialize<QueueData>(buff, data);
                MqttApplicationMessage msg = new MqttApplicationMessageBuilder()
                    .WithTopic(mqttTopic.Text)
                    .WithPayload(buff.ToArray())
                    .WithQualityOfServiceLevel(MqttQualityOfServiceLevel.AtLeastOnce)
                    .Build();
                try
                {
                    await client.PublishAsync(msg);
                    MessageBox.Show("Data Sent", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void SkipValidation_CheckedChanged(object sender, EventArgs e)
        {
            dateTS.MinDate = SkipValidation.Checked ? new DateTime(1753, 1, 1) : new DateTime(1970, 1, 1);
        }

#if DEBUG
        private bool isEmulatedMode()
        {
            return mqttStatus.Text == "Connected (Emu)";
        }
#endif
    }
}
