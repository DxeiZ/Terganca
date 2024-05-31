using System;
using System.Threading;
using System.Windows.Forms;

namespace Automation
{
    public partial class Main : Form
    {
        public Main()
        {
            AutomationFunction.SimulateMouseClick(100, 100);    // x:100, y:100 ve tıkla
            AutomationFunction.SimulateKeyPress(0x41);          // 'A'
            InitializeComponent();

            Thread.Sleep(3000);
            Environment.Exit(0);
        }

        private void InitializeComponent()
        {
            SuspendLayout();
            // 
            // Main
            // 
            ClientSize = new System.Drawing.Size(0, 0);
            ControlBox = false;
            Enabled = false;
            FormBorderStyle = FormBorderStyle.None;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Main";
            Opacity = 0D;
            ShowIcon = false;
            ShowInTaskbar = false;
            Text = "a";
            ResumeLayout(false);
        }
    }
}
