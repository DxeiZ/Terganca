using System;
using System.Windows.Forms;

namespace Terganca
{
    public partial class Main : Form
    {
        private ListBox lstWindows;
        private Button btnRefresh;
        private Button btnMinimize;
        private Button btnMaximize;
        private Button btnRestore;
        private Button btnBringToFront;

        public Main()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            lstWindows = new ListBox();
            btnRefresh = new Button();
            btnMinimize = new Button();
            btnMaximize = new Button();
            btnRestore = new Button();
            btnBringToFront = new Button();

            SuspendLayout();

            // 
            // lstWindows
            // 
            lstWindows.FormattingEnabled = true;
            lstWindows.Location = new System.Drawing.Point(12, 12);
            lstWindows.Name = "lstWindows";
            lstWindows.Size = new System.Drawing.Size(396, 199);
            lstWindows.TabIndex = 0;

            // 
            // btnRefresh
            // 
            btnRefresh.Location = new System.Drawing.Point(12, 217);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new System.Drawing.Size(75, 23);
            btnRefresh.TabIndex = 1;
            btnRefresh.Text = "Yenile";
            btnRefresh.UseVisualStyleBackColor = true;
            btnRefresh.Click += new EventHandler(BtnRefresh_Click);

            // 
            // btnMinimize
            // 
            btnMinimize.Location = new System.Drawing.Point(93, 217);
            btnMinimize.Name = "btnMinimize";
            btnMinimize.Size = new System.Drawing.Size(75, 23);
            btnMinimize.TabIndex = 2;
            btnMinimize.Text = "Minimize et";
            btnMinimize.UseVisualStyleBackColor = true;
            btnMinimize.Click += new EventHandler(BtnMinimize_Click);

            // 
            // btnMaximize
            // 
            btnMaximize.Location = new System.Drawing.Point(174, 217);
            btnMaximize.Name = "btnMaximize";
            btnMaximize.Size = new System.Drawing.Size(75, 23);
            btnMaximize.TabIndex = 3;
            btnMaximize.Text = "Maksimize Et";
            btnMaximize.UseVisualStyleBackColor = true;
            btnMaximize.Click += new EventHandler(BtnMaximize_Click);

            // 
            // btnRestore
            // 
            btnRestore.Location = new System.Drawing.Point(255, 217);
            btnRestore.Name = "btnRestore";
            btnRestore.Size = new System.Drawing.Size(75, 23);
            btnRestore.TabIndex = 4;
            btnRestore.Text = "Geri Yükle";
            btnRestore.UseVisualStyleBackColor = true;
            btnRestore.Click += new EventHandler(BtnRestore_Click);

            // 
            // btnBringToFront
            // 
            btnBringToFront.Location = new System.Drawing.Point(336, 217);
            btnBringToFront.Name = "btnBringToFront";
            btnBringToFront.Size = new System.Drawing.Size(75, 23);
            btnBringToFront.TabIndex = 5;
            btnBringToFront.Text = "Öne Getir";
            btnBringToFront.UseVisualStyleBackColor = true;
            btnBringToFront.Click += new EventHandler(BtnBringToFront_Click);

            // 
            // Main
            // 
            ClientSize = new System.Drawing.Size(424, 251);
            Controls.Add(btnBringToFront);
            Controls.Add(btnRestore);
            Controls.Add(btnMaximize);
            Controls.Add(btnMinimize);
            Controls.Add(btnRefresh);
            Controls.Add(lstWindows);
            Name = "Main";
            Text = "Pencere Yöneticisi";
            Load += new EventHandler(Main_Load);
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            ResumeLayout(false);
        }

        private void Main_Load(object sender, EventArgs e)
        {
            RefreshWindowList();
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            RefreshWindowList();
        }

        private void BtnMinimize_Click(object sender, EventArgs e)
        {
            if (lstWindows.SelectedItem is WindowManager.WindowInfo selectedWindow)
            {
                WindowManager.MinimizeWindow(selectedWindow.Handle);
            }
        }

        private void BtnMaximize_Click(object sender, EventArgs e)
        {
            if (lstWindows.SelectedItem is WindowManager.WindowInfo selectedWindow)
            {
                WindowManager.MaximizeWindow(selectedWindow.Handle);
            }
        }

        private void BtnRestore_Click(object sender, EventArgs e)
        {
            if (lstWindows.SelectedItem is WindowManager.WindowInfo selectedWindow)
            {
                WindowManager.RestoreWindow(selectedWindow.Handle);
            }
        }

        private void BtnBringToFront_Click(object sender, EventArgs e)
        {
            if (lstWindows.SelectedItem is WindowManager.WindowInfo selectedWindow)
            {
                WindowManager.BringToFront(selectedWindow.Handle);
            }
        }

        private void RefreshWindowList()
        {
            var windows = WindowManager.GetOpenWindows();
            lstWindows.DataSource = windows;
            lstWindows.DisplayMember = "Title";
        }
    }
}
