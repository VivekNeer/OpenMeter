using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Runtime.InteropServices;
using Microsoft.Win32;

namespace OpenMeter
{
    public class OverlayWindow : Form
    {
        private double downloadSpeed = 0;
        private double uploadSpeed = 0;
        private string unit = "MB/s";
        private bool isDragging = false;
        private Point dragStartPoint;

        [DllImport("user32.dll", SetLastError = true)]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll")]
        private static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

        private static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);
        private const uint SWP_NOSIZE = 0x0001;
        private const uint SWP_NOMOVE = 0x0002;
        private const uint SWP_SHOWWINDOW = 0x0040;

        private const int GWL_EXSTYLE = -20;
        private const int WS_EX_TOOLWINDOW = 0x00000080;
        private const int WS_EX_TOPMOST = 0x00000008;

        public OverlayWindow()
        {
            InitializeWindow();
        }

        private void InitializeWindow()
        {
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.Manual;
            this.TopMost = true;
            this.ShowInTaskbar = false;
            this.BackColor = Color.FromArgb(30, 30, 30);
            this.Size = new Size(140, 65);
            this.DoubleBuffered = true;
            this.Cursor = Cursors.SizeAll;
            this.Opacity = 1.0;

            PositionCenterScreen();

            this.Paint += OverlayWindow_Paint;
            this.MouseDown += OverlayWindow_MouseDown;
            this.MouseMove += OverlayWindow_MouseMove;
            this.MouseUp += OverlayWindow_MouseUp;
            
            SystemEvents.DisplaySettingsChanged += (s, e) => PositionCenterScreen();
            
            System.Windows.Forms.Timer topmostTimer = new System.Windows.Forms.Timer();
            topmostTimer.Interval = 500;
            topmostTimer.Tick += (s, e) => EnsureTopMost();
            topmostTimer.Start();
        }

        private void EnsureTopMost()
        {
            if (!this.TopMost)
            {
                this.TopMost = true;
            }
            SetWindowPos(this.Handle, HWND_TOPMOST, 0, 0, 0, 0, SWP_NOMOVE | SWP_NOSIZE | SWP_SHOWWINDOW);
        }

        private void OverlayWindow_MouseDown(object? sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = true;
                dragStartPoint = e.Location;
            }
        }

        private void OverlayWindow_MouseMove(object? sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                Point newLocation = this.Location;
                newLocation.X += e.X - dragStartPoint.X;
                newLocation.Y += e.Y - dragStartPoint.Y;
                this.Location = newLocation;
            }
        }

        private void OverlayWindow_MouseUp(object? sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = false;
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            
            this.BringToFront();
            EnsureTopMost();
        }

        private void PositionCenterScreen()
        {
            Rectangle screenBounds = Screen.PrimaryScreen!.Bounds;
            int x = (screenBounds.Width - this.Width) / 2;
            int y = (screenBounds.Height - this.Height) / 2;
            this.Location = new Point(x, y);
        }

        private void OverlayWindow_Paint(object? sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

            using (SolidBrush bgBrush = new SolidBrush(Color.FromArgb(30, 30, 30)))
            {
                g.FillRectangle(bgBrush, this.ClientRectangle);
            }

            using (Pen borderPen = new Pen(Color.FromArgb(70, 70, 70), 1))
            {
                g.DrawRectangle(borderPen, 0, 0, this.Width - 1, this.Height - 1);
            }

            Font speedFont = new Font("Segoe UI", 10, FontStyle.Bold);

            string downloadText = $"Å´ {downloadSpeed:F2} {unit}";
            string uploadText = $"Å™ {uploadSpeed:F2} {unit}";

            g.DrawString(downloadText, speedFont, Brushes.Lime, new PointF(8, 8));
            g.DrawString(uploadText, speedFont, Brushes.Red, new PointF(8, 38));
        }

        public void UpdateSpeeds(double download, double upload, string speedUnit)
        {
            downloadSpeed = download;
            uploadSpeed = upload;
            unit = speedUnit;
            this.Invalidate();
        }
    }
}
