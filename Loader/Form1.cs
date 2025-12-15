using KeyAuth;
using Loader.Utils;
using SharpMonoInjector;
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace Loader
{
    public partial class Form1 : Form
    {
        public static Form1 Instance;
        private int progress = 0;
        private bool increasing = true;
        public static bool IsLogin = false;

        public static api KeyAuthApp = new api(
            name: "name",
            ownerid: "Your Owner ID",
            version: "1.0"
        );

        public Form1()
        {
            Instance = this;
            InitializeComponent();
            this.DoubleBuffered = true;
            animationTimer.Start();
            progressBarBack.Visible = false;
            injectButton.Visible = false;
            consoleBox.Visible = false;
        }

        private async void submitButton_Click(object sender, EventArgs e)
        {
            await KeyAuthApp.license(keyTextBox.Text.Trim());
            if (KeyAuthApp.response.success)
            {
                FilesThings.Write(keyTextBox.Text.Trim());
                statusLabel.Text = $"Welcome, {Environment.UserName}!";
                var onlineUsers = await KeyAuthApp.fetchOnline();
                Log("success", "Connected to server.");
                Log("User", "----------------------------");
                Log("User", $"Online Users:{onlineUsers.Count}");
                Log("User", $"Time/Days Left:{KeyAuthApp.expirydaysleft()}");
                Log("User", "----------------------------");
                keyPanel.Visible = false;
                progressBarBack.Visible = true;
                consoleBox.Visible = true;
                Log("info", "Loading modules...");
            }
            else
            {
                Instance.Hide();
                MessageBox.Show($"{KeyAuthApp.response.message}", "CVRX - Auth");
                Environment.FailFast("Invalid Key");
            }
        }

        private void animationTimer_Tick(object sender, EventArgs e)
        {
            if (!progressBarBack.Visible) return;
            if (progress < 500 && increasing)
            {
                progress += 5;
                progressBarFill.Width = progress;
            }
            else if (progress >= 500)
            {
                increasing = false;
                OnLoadingComplete();
            }
        }

        private void OnLoadingComplete()
        {
            progress = 500;
            progressBarFill.Width = 500;
            statusLabel.Text = "Ready to inject";
            injectButton.Visible = true;
            consoleBox.Visible = true;
            Log("info", "Ready to inject.");
            progressBarBack.Visible = false;
            statusLabel.Location = new Point(0, 140);
        }

        private void injectButton_Click(object sender, EventArgs e)
        {
            injectButton.Enabled = false;
            injectButton.Text = "Injecting...";
            Log("info", "Injecting...");
            try
            {
                byte[] assemblyBytes = Properties.Resources.CVRX;
                using (MemoryStream ms = new MemoryStream(assemblyBytes))
                using (Injector injector = new Injector("ChilloutVR"))
                {
                    Log("info", "Injecting into process: ChilloutVR");
                    IntPtr handle = injector.Inject(ms.ToArray(), "CVRX", "Entry", "Load");
                    if (handle == IntPtr.Zero)
                    {
                        Log("error", "Injection failed. Handle is null.");
                        injectButton.Text = "Failed";
                        injectButton.Enabled = true;
                        return;
                    }
                    Log("success", "Injection successful.");
                    injectButton.Text = "Injected";
                    Thread.Sleep(1000);
                    Process.GetCurrentProcess().Kill();
                }
            }
            catch (Exception ex)
            {
                Log("error", $"Injection exception: {ex}");
                injectButton.Text = "Failed";
                injectButton.BackColor = Color.Red;
                injectButton.Enabled = true;
            }
        }

        public void Log(string type = "info", string message = null)
        {
            Color color = Color.LightGray;
            if (type == "success")
                color = Color.LimeGreen;
            if (type == "User")
                color = Color.Cyan;
            else if (type == "error")
                color = Color.OrangeRed;
            else if (type == "warning")
                color = Color.Orange;
            consoleBox.SelectionStart = consoleBox.TextLength;
            consoleBox.SelectionColor = color;
            consoleBox.AppendText($"[{DateTime.Now:HH:mm:ss}] {message}\n");
            consoleBox.ScrollToCaret();
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            await KeyAuthApp.init();
        }
    }
}