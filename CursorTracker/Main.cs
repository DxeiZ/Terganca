using System;
using System.Windows.Forms;

namespace CursorTracker
{
    public partial class Main : Form
    {
        private Label lblCoordinates;
        private Timer timer;

        public Main()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            lblCoordinates = new Label();
            timer = new Timer();

            SuspendLayout();

            // 
            // lblCoordinates
            // 
            lblCoordinates.AutoSize = true;
            lblCoordinates.Location = new System.Drawing.Point(12, 9);
            lblCoordinates.Name = "lblCoordinates";
            lblCoordinates.Size = new System.Drawing.Size(100, 23);
            lblCoordinates.TabIndex = 0;
            lblCoordinates.Text = "X: 0, Y: 0";

            // 
            // timer
            // 
            timer.Interval = 100; // 100 ms
            timer.Tick += new EventHandler(Timer_Tick);

            // 
            // Main
            // 
            ClientSize = new System.Drawing.Size(284, 41);
            Controls.Add(lblCoordinates);
            Name = "Main";
            Text = "Fare Koordinat İzleyici";
            Load += new EventHandler(MainForm_Load);
            StartPosition = FormStartPosition.CenterScreen;
            MaximizeBox = false;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            ResumeLayout(false);
            PerformLayout();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            var position = CursorTrackerFunction.GetCursorPosition();
            lblCoordinates.Text = $"X: {position.X}, Y: {position.Y}";
        }
    }
}
