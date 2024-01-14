namespace DevTool
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void overrideTS_CheckedChanged(object sender, EventArgs e)
        {
            dateTS.Enabled = overrideTS.Checked;
            timeTS.Enabled = overrideTS.Checked;
        }
    }
}
