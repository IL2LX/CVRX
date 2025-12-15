using System.Drawing;
using System.Windows.Forms;

namespace Loader
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        private Panel mainPanel;
        private Label titleLabel;
        private Label statusLabel;
        private Panel progressBarBack;
        private Panel progressBarFill;
        private Timer animationTimer;

        // Key Panel
        private Panel keyPanel;
        private TextBox keyTextBox;
        private Button submitButton;
        private Label keyLabel;

        // Loader Controls
        private Button injectButton;
        private Label lastUpdateLabel;
        private RichTextBox consoleBox;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.mainPanel = new System.Windows.Forms.Panel();
            this.titleLabel = new System.Windows.Forms.Label();
            this.statusLabel = new System.Windows.Forms.Label();
            this.progressBarBack = new System.Windows.Forms.Panel();
            this.progressBarFill = new System.Windows.Forms.Panel();
            this.injectButton = new System.Windows.Forms.Button();
            this.detectLabel = new System.Windows.Forms.Label();
            this.lastUpdateLabel = new System.Windows.Forms.Label();
            this.consoleBox = new System.Windows.Forms.RichTextBox();
            this.keyPanel = new System.Windows.Forms.Panel();
            this.keyLabel = new System.Windows.Forms.Label();
            this.keyTextBox = new System.Windows.Forms.TextBox();
            this.submitButton = new System.Windows.Forms.Button();
            this.animationTimer = new System.Windows.Forms.Timer(this.components);
            this.mainPanel.SuspendLayout();
            this.progressBarBack.SuspendLayout();
            this.keyPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainPanel
            // 
            this.mainPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.mainPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.mainPanel.Controls.Add(this.titleLabel);
            this.mainPanel.Controls.Add(this.statusLabel);
            this.mainPanel.Controls.Add(this.progressBarBack);
            this.mainPanel.Controls.Add(this.injectButton);
            this.mainPanel.Controls.Add(this.detectLabel);
            this.mainPanel.Controls.Add(this.lastUpdateLabel);
            this.mainPanel.Controls.Add(this.consoleBox);
            this.mainPanel.Controls.Add(this.keyPanel);
            this.mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainPanel.Location = new System.Drawing.Point(0, 0);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(800, 532);
            this.mainPanel.TabIndex = 0;
            // 
            // titleLabel
            // 
            this.titleLabel.Font = new System.Drawing.Font("Segoe UI", 36F, System.Drawing.FontStyle.Bold);
            this.titleLabel.ForeColor = System.Drawing.Color.Crimson;
            this.titleLabel.Location = new System.Drawing.Point(0, 20);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(800, 80);
            this.titleLabel.TabIndex = 0;
            this.titleLabel.Text = "CVRX";
            this.titleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // statusLabel
            // 
            this.statusLabel.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.statusLabel.ForeColor = System.Drawing.Color.LightGray;
            this.statusLabel.Location = new System.Drawing.Point(0, 110);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(800, 30);
            this.statusLabel.TabIndex = 1;
            this.statusLabel.Text = "Waiting for key...";
            this.statusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // progressBarBack
            // 
            this.progressBarBack.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.progressBarBack.Controls.Add(this.progressBarFill);
            this.progressBarBack.Location = new System.Drawing.Point(150, 150);
            this.progressBarBack.Name = "progressBarBack";
            this.progressBarBack.Size = new System.Drawing.Size(500, 25);
            this.progressBarBack.TabIndex = 2;
            // 
            // progressBarFill
            // 
            this.progressBarFill.BackColor = System.Drawing.Color.DarkRed;
            this.progressBarFill.Location = new System.Drawing.Point(0, 0);
            this.progressBarFill.Name = "progressBarFill";
            this.progressBarFill.Size = new System.Drawing.Size(0, 25);
            this.progressBarFill.TabIndex = 0;
            // 
            // injectButton
            // 
            this.injectButton.BackColor = System.Drawing.Color.DarkRed;
            this.injectButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.injectButton.ForeColor = System.Drawing.Color.White;
            this.injectButton.Location = new System.Drawing.Point(350, 200);
            this.injectButton.Name = "injectButton";
            this.injectButton.Size = new System.Drawing.Size(100, 40);
            this.injectButton.TabIndex = 3;
            this.injectButton.Text = "Inject";
            this.injectButton.UseVisualStyleBackColor = false;
            this.injectButton.Visible = false;
            this.injectButton.Click += new System.EventHandler(this.injectButton_Click);
            // 
            // detectLabel
            // 
            this.detectLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.detectLabel.Location = new System.Drawing.Point(1, 515);
            this.detectLabel.Name = "detectLabel";
            this.detectLabel.Size = new System.Drawing.Size(114, 17);
            this.detectLabel.TabIndex = 4;
            this.detectLabel.Text = "v0.2.2 - KitKat";
            // 
            // lastUpdateLabel
            // 
            this.lastUpdateLabel.ForeColor = System.Drawing.Color.Gray;
            this.lastUpdateLabel.Location = new System.Drawing.Point(669, 515);
            this.lastUpdateLabel.Name = "lastUpdateLabel";
            this.lastUpdateLabel.Size = new System.Drawing.Size(126, 15);
            this.lastUpdateLabel.TabIndex = 5;
            this.lastUpdateLabel.Text = "Last Update: 22-11-2025";
            // 
            // consoleBox
            // 
            this.consoleBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.consoleBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.consoleBox.Font = new System.Drawing.Font("Consolas", 10F);
            this.consoleBox.ForeColor = System.Drawing.Color.LightGray;
            this.consoleBox.Location = new System.Drawing.Point(50, 250);
            this.consoleBox.Name = "consoleBox";
            this.consoleBox.ReadOnly = true;
            this.consoleBox.Size = new System.Drawing.Size(700, 230);
            this.consoleBox.TabIndex = 6;
            this.consoleBox.Text = "";
            // 
            // keyPanel
            // 
            this.keyPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.keyPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.keyPanel.Controls.Add(this.keyLabel);
            this.keyPanel.Controls.Add(this.keyTextBox);
            this.keyPanel.Controls.Add(this.submitButton);
            this.keyPanel.Location = new System.Drawing.Point(200, 215);
            this.keyPanel.Name = "keyPanel";
            this.keyPanel.Size = new System.Drawing.Size(400, 150);
            this.keyPanel.TabIndex = 7;
            // 
            // keyLabel
            // 
            this.keyLabel.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.keyLabel.ForeColor = System.Drawing.Color.Crimson;
            this.keyLabel.Location = new System.Drawing.Point(0, 10);
            this.keyLabel.Name = "keyLabel";
            this.keyLabel.Size = new System.Drawing.Size(400, 30);
            this.keyLabel.TabIndex = 0;
            this.keyLabel.Text = "Enter Your Key";
            this.keyLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // keyTextBox
            // 
            this.keyTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.keyTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.keyTextBox.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.keyTextBox.ForeColor = System.Drawing.Color.White;
            this.keyTextBox.Location = new System.Drawing.Point(50, 50);
            this.keyTextBox.Name = "keyTextBox";
            this.keyTextBox.Size = new System.Drawing.Size(300, 29);
            this.keyTextBox.TabIndex = 1;
            this.keyTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // submitButton
            // 
            this.submitButton.BackColor = System.Drawing.Color.DarkRed;
            this.submitButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.submitButton.ForeColor = System.Drawing.Color.White;
            this.submitButton.Location = new System.Drawing.Point(150, 90);
            this.submitButton.Name = "submitButton";
            this.submitButton.Size = new System.Drawing.Size(100, 30);
            this.submitButton.TabIndex = 2;
            this.submitButton.Text = "Submit";
            this.submitButton.UseVisualStyleBackColor = false;
            this.submitButton.Click += new System.EventHandler(this.submitButton_Click);
            // 
            // animationTimer
            // 
            this.animationTimer.Interval = 20;
            this.animationTimer.Tick += new System.EventHandler(this.animationTimer_Tick);
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(800, 532);
            this.Controls.Add(this.mainPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CVRX";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.mainPanel.ResumeLayout(false);
            this.progressBarBack.ResumeLayout(false);
            this.keyPanel.ResumeLayout(false);
            this.keyPanel.PerformLayout();
            this.ResumeLayout(false);

        }
        #endregion

        private Label detectLabel;
    }
}
