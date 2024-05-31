using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace FileIconViewer
{
    public partial class Main : Form
    {
        [StructLayout(LayoutKind.Sequential)]
        private struct SHFILEINFO
        {
            public IntPtr hIcon;
            public int iIcon;
            public uint dwAttributes;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
            public string szDisplayName;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)]
            public string szTypeName;
        }

        private const uint SHGFI_ICON = 0x000000100;
        private const uint SHGFI_LARGEICON = 0x000000000;
        private const uint SHGFI_SMALLICON = 0x000000001;
        private const uint SHGFI_USEFILEATTRIBUTES = 0x000000010;

        [DllImport("shell32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr SHGetFileInfo(string pszPath, uint dwFileAttributes, ref SHFILEINFO psfi, uint cbFileInfo, uint uFlags);

        public Main()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            btnSelectFile = new Button();
            pictureBoxIcon = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)(pictureBoxIcon)).BeginInit();
            SuspendLayout();
            // 
            // btnSelectFile
            // 
            btnSelectFile.Location = new Point(16, 15);
            btnSelectFile.Margin = new Padding(4, 4, 4, 4);
            btnSelectFile.Name = "btnSelectFile";
            btnSelectFile.Size = new Size(200, 28);
            btnSelectFile.TabIndex = 0;
            btnSelectFile.Text = "Dosya Seçiniz";
            btnSelectFile.UseVisualStyleBackColor = true;
            btnSelectFile.Click += new System.EventHandler(btnSelectFile_Click);
            // 
            // pictureBoxIcon
            // 
            pictureBoxIcon.Location = new Point(16, 50);
            pictureBoxIcon.Margin = new Padding(4, 4, 4, 4);
            pictureBoxIcon.Name = "pictureBoxIcon";
            pictureBoxIcon.Size = new Size(32, 32);
            pictureBoxIcon.SizeMode = PictureBoxSizeMode.AutoSize;
            pictureBoxIcon.TabIndex = 1;
            pictureBoxIcon.TabStop = false;
            // 
            // Main
            // 
            AutoScaleDimensions = new SizeF(8F, 16F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(450, 110);
            Controls.Add(pictureBoxIcon);
            Controls.Add(btnSelectFile);
            Margin = new Padding(4, 4, 4, 4);
            Name = "Main";
            Text = "Dosya Simgesi Görüntüleyicisi";
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            ((System.ComponentModel.ISupportInitialize)(pictureBoxIcon)).EndInit();
            ResumeLayout(false);
            PerformLayout();

        }

        private Button btnSelectFile;
        private PictureBox pictureBoxIcon;

        private void btnSelectFile_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = openFileDialog.FileName;
                    DisplayFileIcon(filePath);
                }
            }
        }

        private void DisplayFileIcon(string filePath)
        {
            SHFILEINFO shinfo = new SHFILEINFO();
            IntPtr hImgSmall = SHGetFileInfo(filePath, 0, ref shinfo, (uint)Marshal.SizeOf(shinfo), SHGFI_ICON | SHGFI_LARGEICON);

            if (hImgSmall != IntPtr.Zero)
            {
                Icon icon = Icon.FromHandle(shinfo.hIcon);
                pictureBoxIcon.Image = icon.ToBitmap();
                DestroyIcon(shinfo.hIcon);
            }
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        extern static bool DestroyIcon(IntPtr handle);
    }
}
