using System;
using System.Drawing;
using System.Windows.Forms;

namespace KeyboardHook
{
    public partial class Main : Form
    {
        private TextBox txtKeyLogger;
        private Button btnStart;
        private Button btnStop;
        private KeyboardHookFunction _keyboardHook;

        public Main()
        {
            InitializeComponent();
            _keyboardHook = new KeyboardHookFunction();
            _keyboardHook.KeyPressed += KeyboardHook_KeyPressed;
        }

        private void InitializeComponent()
        {
            txtKeyLogger = new TextBox();
            btnStart = new Button();
            btnStop = new Button();

            SuspendLayout();

            // 
            // txtKeyLogger
            // 
            txtKeyLogger.Location = new Point(12, 12);
            txtKeyLogger.Multiline = true;
            txtKeyLogger.Name = "txtKeyLogger";
            txtKeyLogger.Size = new Size(360, 199);
            txtKeyLogger.TabIndex = 0;

            // 
            // btnStart
            // 
            btnStart.Location = new Point(12, 217);
            btnStart.Name = "btnStart";
            btnStart.Size = new Size(75, 23);
            btnStart.TabIndex = 1;
            btnStart.Text = "Başlat";
            btnStart.UseVisualStyleBackColor = true;
            btnStart.Click += new EventHandler(BtnStart_Click);

            // 
            // btnStop
            // 
            btnStop.Location = new Point(93, 217);
            btnStop.Name = "btnStop";
            btnStop.Size = new Size(75, 23);
            btnStop.TabIndex = 2;
            btnStop.Text = "Durdur";
            btnStop.UseVisualStyleBackColor = true;
            btnStop.Click += new EventHandler(BtnStop_Click);

            // 
            // Main
            // 
            ClientSize = new Size(384, 251);
            Controls.Add(btnStop);
            Controls.Add(btnStart);
            Controls.Add(txtKeyLogger);
            Name = "Main";
            Text = "Tuş Kaydedici";
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            ResumeLayout(false);
            PerformLayout();
        }

        private void BtnStart_Click(object sender, EventArgs e)
        {
            _keyboardHook.HookKeyboard();
        }

        private void BtnStop_Click(object sender, EventArgs e)
        {
            _keyboardHook.UnhookKeyboard();
        }

        private void KeyboardHook_KeyPressed(object sender, Keys e)
        {
            txtKeyLogger.AppendText(e.ToString() + " ");
        }
    }
}
