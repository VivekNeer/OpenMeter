using System.Diagnostics;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Net.NetworkInformation;

namespace OpenMeter
{
    public partial class Form1 : Form
    {
        private static Mutex? mutex;
        private PerformanceCounter? uploadCounter;
        private PerformanceCounter? downloadCounter;
        private string? activeAdapter;
        private SpeedUnit currentUnit = SpeedUnit.MBps;
        private OverlayWindow? overlayWindow;

        public enum SpeedUnit
        {
            KBps,
            MBps
        }

        public Form1()
        {
            InitializeComponent();
            
            DetectActiveNetworkAdapter();
            InitializePerformanceCounters();
            
            overlayWindow = new OverlayWindow();
            overlayWindow.Show();
            
            timer1.Tick += Timer1_Tick;
            timer1.Start();
            
            settingsToolStripMenuItem.Click += SettingsToolStripMenuItem_Click;
            exitToolStripMenuItem.Click += ExitToolStripMenuItem_Click;
            
            UpdateDisplay(0, 0);
        }

        protected override void SetVisibleCore(bool value)
        {
            base.SetVisibleCore(false);
        }

        private void SettingsToolStripMenuItem_Click(object? sender, EventArgs e)
        {
            using (var settingsForm = new SettingsForm(currentUnit))
            {
                if (settingsForm.ShowDialog() == DialogResult.OK)
                {
                    currentUnit = settingsForm.SelectedUnit;
                }
            }
        }

        private void ExitToolStripMenuItem_Click(object? sender, EventArgs e)
        {
            Application.Exit();
        }

        private void DetectActiveNetworkAdapter()
        {
            var networkInterfaces = NetworkInterface.GetAllNetworkInterfaces()
                .Where(ni => ni.OperationalStatus == OperationalStatus.Up
                          && ni.NetworkInterfaceType != NetworkInterfaceType.Loopback
                          && ni.NetworkInterfaceType != NetworkInterfaceType.Tunnel)
                .ToList();

            if (networkInterfaces.Any())
            {
                var mostActive = networkInterfaces
                    .OrderByDescending(ni => ni.GetIPv4Statistics().BytesReceived + ni.GetIPv4Statistics().BytesSent)
                    .FirstOrDefault();

                activeAdapter = mostActive?.Description;
            }
        }

        private void InitializePerformanceCounters()
        {
            if (string.IsNullOrEmpty(activeAdapter))
                return;

            try
            {
                uploadCounter = new PerformanceCounter("Network Interface", "Bytes Sent/sec", activeAdapter);
                downloadCounter = new PerformanceCounter("Network Interface", "Bytes Received/sec", activeAdapter);
                
                uploadCounter.NextValue();
                downloadCounter.NextValue();
            }
            catch
            {
            }
        }

        private void Timer1_Tick(object? sender, EventArgs e)
        {
            if (uploadCounter == null || downloadCounter == null)
                return;

            try
            {
                float uploadBytes = uploadCounter.NextValue();
                float downloadBytes = downloadCounter.NextValue();

                double uploadSpeed = currentUnit == SpeedUnit.MBps 
                    ? uploadBytes / (1024 * 1024)
                    : uploadBytes / 1024;
                
                double downloadSpeed = currentUnit == SpeedUnit.MBps 
                    ? downloadBytes / (1024 * 1024)
                    : downloadBytes / 1024;

                UpdateDisplay(downloadSpeed, uploadSpeed);
            }
            catch
            {
            }
        }

        private void UpdateDisplay(double download, double upload)
        {
            string unit = currentUnit == SpeedUnit.MBps ? "MB/s" : "KB/s";
            
            overlayWindow?.UpdateSpeeds(download, upload, unit);
            
            notifyIcon1.Text = $"Download: {download:F2} {unit}\nUpload: {upload:F2} {unit}";
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            uploadCounter?.Dispose();
            downloadCounter?.Dispose();
            overlayWindow?.Close();
            overlayWindow?.Dispose();
            notifyIcon1.Visible = false;
            mutex?.ReleaseMutex();
            base.OnFormClosing(e);
        }

        public static bool CheckSingleInstance()
        {
            mutex = new Mutex(true, "OpenMeter_SingleInstance", out bool isNewInstance);
            return isNewInstance;
        }
    }
}
