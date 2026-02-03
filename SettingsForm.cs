namespace OpenMeter
{
    public partial class SettingsForm : Form
    {
        public Form1.SpeedUnit SelectedUnit { get; private set; }

        public SettingsForm(Form1.SpeedUnit currentUnit)
        {
            InitializeComponent();
            SelectedUnit = currentUnit;

            if (currentUnit == Form1.SpeedUnit.MBps)
                radioButtonMB.Checked = true;
            else
                radioButtonKB.Checked = true;
        }

        private void ButtonOK_Click(object sender, EventArgs e)
        {
            SelectedUnit = radioButtonMB.Checked ? Form1.SpeedUnit.MBps : Form1.SpeedUnit.KBps;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
