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
                .WithWillQualityOfServiceLevel(MqttQualityOfServiceLevel.AtLeastOnce);

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
            }));
        }

        private async void sendData_Click(object sender, EventArgs e)
        {
            // Input Validation
            if (string.IsNullOrWhiteSpace(mqttTopic.Text))
            {
                MessageBox.Show("Invalid Topic", "Bad Topic", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (
                !int.TryParse(Temperature.Text, out int temp) ||
                temp < -42 ||
                temp > 100
               )
            {
                MessageBox.Show("Invalid Temperature", "Bad Temperature", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (
                !int.TryParse(Humidity.Text, out int hum) ||
                hum < 0 ||
                hum > 100
                                                                           )
            {
                MessageBox.Show("Invalid Humidity", "Bad Humidity", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

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
            using(MemoryStream buff = new MemoryStream())
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
                } catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
