using MQTTnet;
using MQTTnet.Client;

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

        [Obsolete]
        private void mqttLogin_Click(object sender, EventArgs e)
        {
            client = factory.CreateMqttClient();

            string clientId = Guid.NewGuid().ToString();
            var test = new MqttClientOptionsBuilder()
                .WithClientId("DevTool_" + clientId)
                .WithConnectionUri(new Uri(mqttIP.Text))
                .Build();
        }

        private void mqttTLS_CheckedChanged(object sender, EventArgs e)
        {
            if(mqttPort.Text == "1883" && mqttTLS.Checked)
                mqttPort.Text = "8883";
            else if(mqttPort.Text == "8883" && !mqttTLS.Checked)
                mqttPort.Text = "1883";
        }
    }
}
