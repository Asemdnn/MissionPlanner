using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using MissionPlanner.Utilities;

namespace MissionPlanner.GCSViews
{
    /// <summary>
    /// Standalone Android tablet mirror window using scrcpy.
    /// Can be moved to a second or third monitor for external display.
    /// </summary>
    public class TabletMirrorForm : Form
    {
        private Process _scrcpyProcess;
        private Process _adbProcess;
        private bool _isMirroring;
        private IntPtr _scrcpyWindowHandle = IntPtr.Zero;
        private System.Windows.Forms.Timer _statusTimer;

        // UI Components
        private Panel _topPanel;
        private Label _lblStatus;
        private Label _lblDeviceInfo;
        private Panel _displayPanel;
        private Label _lblNoDevice;
        private Panel _buttonPanel;
        private Button _btnRefresh;
        private Button _btnStart;
        private Button _btnStop;
        private Button _btnOpenExternal;
        private ComboBox _cmbDevices;
        private Label _lblDevice;

        // Static instance for singleton pattern
        private static TabletMirrorForm _instance = null;
        private static readonly object _lock = new object();

        // Windows API for embedding scrcpy window
        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll")]
        private static extern bool SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);

        [DllImport("user32.dll")]
        private static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

        private static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);
        private static readonly IntPtr HWND_NOTOPMOST = new IntPtr(-2);
        private const uint SWP_NOSIZE = 0x0001;
        private const uint SWP_NOMOVE = 0x0002;
        private const uint SWP_SHOWWINDOW = 0x0040;

        public static TabletMirrorForm Instance
        {
            get
            {
                if (_instance == null || _instance.IsDisposed)
                {
                    lock (_lock)
                    {
                        if (_instance == null || _instance.IsDisposed)
                        {
                            _instance = new TabletMirrorForm();
                        }
                    }
                }
                return _instance;
            }
        }

        public static void ShowTabletMirror()
        {
            var form = Instance;
            if (form.WindowState == FormWindowState.Minimized)
                form.WindowState = FormWindowState.Normal;
            form.Activate();
            form.Show();
        }

        private TabletMirrorForm()
        {
            InitializeComponent();
            PositionOnSecondaryMonitor();
            _statusTimer = new System.Windows.Forms.Timer { Interval = 1000 };
            _statusTimer.Tick += StatusTimer_Tick;
            _statusTimer.Start();
            RefreshDevices();
        }

        private void InitializeComponent()
        {
            this.Text = "Android Tablet Mirror";
            this.Size = new Size(900, 700);
            this.MinimumSize = new Size(640, 480);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormClosing += TabletMirrorForm_FormClosing;
            this.BackColor = Color.FromArgb(30, 30, 30);

            // Top panel with status
            _topPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 50,
                BackColor = Color.FromArgb(45, 45, 48),
                Padding = new Padding(10)
            };

            _lblStatus = new Label
            {
                Text = "Ready",
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10F),
                AutoSize = true,
                Location = new Point(10, 15)
            };

            _lblDeviceInfo = new Label
            {
                Text = "",
                ForeColor = Color.LightGray,
                Font = new Font("Segoe UI", 9F),
                AutoSize = true,
                Location = new Point(200, 17)
            };

            _topPanel.Controls.AddRange(new Control[] { _lblStatus, _lblDeviceInfo });

            // Device selection panel
            var devicePanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 40,
                BackColor = Color.FromArgb(37, 37, 38),
                Padding = new Padding(10, 5, 10, 5)
            };

            _lblDevice = new Label
            {
                Text = "Device:",
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(10, 10)
            };

            _cmbDevices = new ComboBox
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                Width = 300,
                Location = new Point(60, 7),
                BackColor = Color.FromArgb(63, 63, 70),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };

            _btnRefresh = new Button
            {
                Text = "Refresh Devices",
                Width = 120,
                Location = new Point(380, 5),
                BackColor = Color.FromArgb(0, 122, 204),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            _btnRefresh.Click += BtnRefresh_Click;

            devicePanel.Controls.AddRange(new Control[] { _lblDevice, _cmbDevices, _btnRefresh });

            // Display panel (where scrcpy will be embedded)
            _displayPanel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.FromArgb(20, 20, 20),
                Padding = new Padding(5)
            };

            _lblNoDevice = new Label
            {
                Text = "No device connected.\n\nConnect your Android tablet via USB-C and click 'Refresh Devices'.",
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = Color.Gray,
                Font = new Font("Segoe UI", 12F),
                Dock = DockStyle.Fill,
                BackColor = Color.Transparent
            };

            _displayPanel.Controls.Add(_lblNoDevice);

            // Button panel at bottom
            _buttonPanel = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 50,
                BackColor = Color.FromArgb(45, 45, 48),
                Padding = new Padding(10, 8, 10, 8)
            };

            _btnStart = new Button
            {
                Text = "Start Mirror",
                Width = 120,
                BackColor = Color.FromArgb(0, 122, 204),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Location = new Point(10, 8)
            };
            _btnStart.Click += BtnStart_Click;

            _btnStop = new Button
            {
                Text = "Stop Mirror",
                Width = 120,
                BackColor = Color.FromArgb(204, 0, 0),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Location = new Point(140, 8),
                Enabled = false
            };
            _btnStop.Click += BtnStop_Click;

            _btnOpenExternal = new Button
            {
                Text = "Open External Mirror",
                Width = 150,
                BackColor = Color.FromArgb(70, 70, 70),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Location = new Point(270, 8)
            };
            _btnOpenExternal.Click += BtnOpenExternal_Click;

            _buttonPanel.Controls.AddRange(new Control[] { _btnStart, _btnStop, _btnOpenExternal });

            // Add all panels to form
            this.Controls.Add(_displayPanel);
            this.Controls.Add(devicePanel);
            this.Controls.Add(_topPanel);
            this.Controls.Add(_buttonPanel);
        }

        private void PositionOnSecondaryMonitor()
        {
            try
            {
                Screen[] screens = Screen.AllScreens;
                if (screens.Length > 1)
                {
                    Screen secondary = screens[1];
                    this.StartPosition = FormStartPosition.Manual;
                    this.Location = new Point(secondary.Bounds.X, secondary.Bounds.Y);
                    this.Size = new Size(secondary.Bounds.Width / 2, secondary.Bounds.Height - 100);
                }
            }
            catch { }
        }

        private void StatusTimer_Tick(object sender, EventArgs e)
        {
            if (_isMirroring && _scrcpyWindowHandle == IntPtr.Zero)
            {
                // Try to find the scrcpy window
                _scrcpyWindowHandle = FindWindow(null, "DIMP Android Tablet Mirror");
                if (_scrcpyWindowHandle == IntPtr.Zero)
                    _scrcpyWindowHandle = FindWindow(null, "scrcpy");

                if (_scrcpyWindowHandle != IntPtr.Zero)
                {
                    EmbedScrcpyWindow();
                }
            }
        }

        private void TabletMirrorForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            StopMirror();
            _statusTimer?.Stop();
            _statusTimer?.Dispose();
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            RefreshDevices();
        }

        private void BtnStart_Click(object sender, EventArgs e)
        {
            StartMirror();
        }

        private void BtnStop_Click(object sender, EventArgs e)
        {
            StopMirror();
        }

        private void BtnOpenExternal_Click(object sender, EventArgs e)
        {
            OpenExternalScrcpy();
        }

        private string GetToolsPath()
        {
            string basePath = Settings.GetRunningDirectory();
            return Path.Combine(basePath, "Tools", "scrcpy");
        }

        private string GetAdbPath()
        {
            string toolsPath = GetToolsPath();
            string adbPath = Path.Combine(toolsPath, "adb.exe");
            if (!File.Exists(adbPath))
            {
                // Try system PATH
                adbPath = "adb.exe";
            }
            return adbPath;
        }

        private string GetScrcpyPath()
        {
            string toolsPath = GetToolsPath();
            string scrcpyPath = Path.Combine(toolsPath, "scrcpy.exe");
            if (!File.Exists(scrcpyPath))
            {
                scrcpyPath = Path.Combine(toolsPath, "scrcpy");
            }
            return scrcpyPath;
        }

        private List<AndroidDevice> GetConnectedDevices()
        {
            var devices = new List<AndroidDevice>();
            string adbPath = GetAdbPath();

            try
            {
                // Check if ADB exists
                string scrcpyPath = GetScrcpyPath();
                if (!File.Exists(scrcpyPath) && adbPath != "adb.exe")
                {
                    UpdateStatus("ADB/scrcpy tools not found. Please place them in Tools/scrcpy folder.", Color.Yellow);
                    return devices;
                }

                var psi = new ProcessStartInfo
                {
                    FileName = adbPath,
                    Arguments = "devices",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    CreateNoWindow = true
                };

                using (var proc = Process.Start(psi))
                {
                    string output = proc.StandardOutput.ReadToEnd();
                    proc.WaitForExit(5000);

                    foreach (string rawLine in output.Split('\n'))
                    {
                        string line = rawLine.Trim();
                        if (string.IsNullOrEmpty(line) || line.StartsWith("List") || line.StartsWith("*"))
                            continue;

                        string[] parts = line.Split('\t');
                        if (parts.Length >= 2)
                        {
                            var device = new AndroidDevice
                            {
                                Serial = parts[0],
                                State = parts[1].Trim()
                            };

                            // Get device model
                            var modelPsi = new ProcessStartInfo
                            {
                                FileName = adbPath,
                                Arguments = $"-s {device.Serial} shell getprop ro.product.model",
                                UseShellExecute = false,
                                RedirectStandardOutput = true,
                                CreateNoWindow = true
                            };
                            using (var modelProc = Process.Start(modelPsi))
                            {
                                device.Model = modelProc?.StandardOutput.ReadToEnd()?.Trim() ?? "Unknown";
                                modelProc?.WaitForExit(3000);
                            }

                            devices.Add(device);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                UpdateStatus($"Error detecting devices: {ex.Message}", Color.Red);
            }

            return devices;
        }

        private void RefreshDevices()
        {
            _cmbDevices.Items.Clear();
            var devices = GetConnectedDevices();

            if (devices.Count == 0)
            {
                _cmbDevices.Items.Add("No devices found");
                _cmbDevices.SelectedIndex = 0;
                _lblNoDevice.Text = "No device connected.\n\nConnect your Android tablet via USB-C and click 'Refresh Devices'.";
                _lblNoDevice.Visible = true;
                _lblDeviceInfo.Text = "";
                UpdateStatus("No Android device detected. Check USB-C cable, enable Developer Options and USB Debugging.", Color.Yellow);
                _btnStart.Enabled = false;
            }
            else
            {
                foreach (var device in devices)
                {
                    string display = $"{device.Model} ({device.Serial}) - {device.State}";
                    _cmbDevices.Items.Add(new DeviceItem { Device = device, Display = display });
                }
                _cmbDevices.SelectedIndex = 0;
                _btnStart.Enabled = true;

                var selectedDevice = devices[0];
                if (selectedDevice.State == "unauthorized")
                {
                    _lblNoDevice.Text = "Device unauthorized.\n\nUnlock the tablet and accept the USB debugging authorization.";
                    _lblNoDevice.Visible = true;
                    _lblDeviceInfo.Text = selectedDevice.State;
                    UpdateStatus("Device connected but unauthorized. Unlock tablet and accept USB debugging.", Color.Orange);
                    _btnStart.Enabled = false;
                }
                else
                {
                    _lblNoDevice.Visible = false;
                    _lblDeviceInfo.Text = $"{devices.Count} device(s) found";
                    UpdateStatus($"Device ready: {selectedDevice.Model}", Color.LimeGreen);
                }
            }
        }

        private void StartMirror()
        {
            if (_isMirroring)
            {
                UpdateStatus("Already mirroring", Color.Yellow);
                return;
            }

            DeviceItem selectedItem = _cmbDevices.SelectedItem as DeviceItem;
            if (selectedItem == null)
            {
                UpdateStatus("No device selected", Color.Red);
                return;
            }

            string deviceSerial = selectedItem.Device.Serial;
            string scrcpyPath = GetScrcpyPath();

            if (!File.Exists(scrcpyPath))
            {
                UpdateStatus("scrcpy.exe not found. Please place it in Tools/scrcpy folder.", Color.Red);
                MessageBox.Show(
                    "scrcpy.exe not found.\n\nPlease download scrcpy and place scrcpy.exe and adb.exe in the Tools/scrcpy folder.\n\nYou can download scrcpy from: https://github.com/Genymobile/scrcpy",
                    "Tools Missing",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            try
            {
                UpdateStatus("Starting mirror...", Color.Cyan);
                _lblNoDevice.Visible = false;
                _scrcpyWindowHandle = IntPtr.Zero;

                // Start scrcpy with settings for low latency and stability
                var psi = new ProcessStartInfo
                {
                    FileName = scrcpyPath,
                    Arguments = $"-s {deviceSerial} --window-title \"DIMP Android Tablet Mirror\" --no-audio --stay-awake --max-size 1280 --video-bit-rate 8M --turn-screen-off",
                    UseShellExecute = true,
                    CreateNoWindow = false
                };

                _scrcpyProcess = Process.Start(psi);
                _isMirroring = true;

                _btnStart.Enabled = false;
                _btnStop.Enabled = true;
                UpdateStatus($"Mirroring: {selectedItem.Device.Model}", Color.LimeGreen);

                // Start timer to try embedding the window
                _statusTimer.Start();
            }
            catch (Exception ex)
            {
                UpdateStatus($"Failed to start mirror: {ex.Message}", Color.Red);
                _isMirroring = false;
            }
        }

        private void StopMirror()
        {
            if (!_isMirroring && _scrcpyProcess == null)
                return;

            try
            {
                UpdateStatus("Stopping mirror...", Color.Orange);

                // Stop the status timer
                _statusTimer.Stop();

                // Kill scrcpy process
                if (_scrcpyProcess != null && !_scrcpyProcess.HasExited)
                {
                    _scrcpyProcess.Kill();
                    _scrcpyProcess.Dispose();
                    _scrcpyProcess = null;
                }

                // Also try to kill any scrcpy process by name
                try
                {
                    var scrcpyProcs = Process.GetProcessesByName("scrcpy");
                    foreach (var p in scrcpyProcs)
                    {
                        p.Kill();
                        p.Dispose();
                    }
                }
                catch { }

                _isMirroring = false;
                _scrcpyWindowHandle = IntPtr.Zero;

                _btnStart.Enabled = true;
                _btnStop.Enabled = false;
                _lblNoDevice.Visible = true;
                _lblNoDevice.Text = "Mirror stopped.\n\nClick 'Start Mirror' to begin again.";

                UpdateStatus("Mirror stopped", Color.LightGray);
            }
            catch (Exception ex)
            {
                UpdateStatus($"Error stopping mirror: {ex.Message}", Color.Red);
            }
        }

        private void OpenExternalScrcpy()
        {
            DeviceItem selectedItem = _cmbDevices.SelectedItem as DeviceItem;
            string deviceSerial = selectedItem?.Device?.Serial ?? "";
            string scrcpyPath = GetScrcpyPath();

            if (!File.Exists(scrcpyPath))
            {
                UpdateStatus("scrcpy.exe not found. Please place it in Tools/scrcpy folder.", Color.Red);
                MessageBox.Show(
                    "scrcpy.exe not found.\n\nPlease download scrcpy and place scrcpy.exe in the Tools/scrcpy folder.",
                    "Tools Missing",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string args = string.IsNullOrEmpty(deviceSerial) ? "" : $"-s {deviceSerial}";
                args += " --window-title \"DIMP Android Tablet Mirror\" --no-audio --stay-awake --max-size 1280 --video-bit-rate 8M";

                Process.Start(new ProcessStartInfo
                {
                    FileName = scrcpyPath,
                    Arguments = args,
                    UseShellExecute = true
                });

                UpdateStatus("External mirror opened", Color.LimeGreen);
            }
            catch (Exception ex)
            {
                UpdateStatus($"Failed to open external mirror: {ex.Message}", Color.Red);
            }
        }

        private void EmbedScrcpyWindow()
        {
            if (_scrcpyWindowHandle == IntPtr.Zero || _displayPanel.IsDisposed)
                return;

            try
            {
                // Set scrcpy window as child of our display panel
                SetParent(_scrcpyWindowHandle, _displayPanel.Handle);

                // Resize scrcpy window to fit the display panel
                MoveWindow(_scrcpyWindowHandle, 0, 0, _displayPanel.Width, _displayPanel.Height, true);

                // Bring our form to front
                this.Activate();

                UpdateStatus("Mirror embedded successfully", Color.LimeGreen);
            }
            catch (Exception ex)
            {
                UpdateStatus($"Could not embed window: {ex.Message}", Color.Yellow);
            }
        }

        private void UpdateStatus(string message, Color color)
        {
            if (_lblStatus.InvokeRequired)
            {
                _lblStatus.Invoke(new Action(() => UpdateStatus(message, color)));
                return;
            }
            _lblStatus.Text = message;
            _lblStatus.ForeColor = color;
        }

        private class AndroidDevice
        {
            public string Serial { get; set; }
            public string State { get; set; }
            public string Model { get; set; }
        }

        private class DeviceItem
        {
            public AndroidDevice Device { get; set; }
            public string Display { get; set; }
            public override string ToString() => Display;
        }
    }
}
