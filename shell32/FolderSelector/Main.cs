using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace FolderSelector
{
    public partial class Main : Form
    {
        private Button btnSelectFolder;
        private TextBox txtSelectedPath;

        public Main()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            btnSelectFolder = new Button();
            txtSelectedPath = new TextBox();

            SuspendLayout();

            // 
            // btnSelectFolder
            // 
            btnSelectFolder.Location = new System.Drawing.Point(12, 12);
            btnSelectFolder.Name = "btnSelectFolder";
            btnSelectFolder.Size = new System.Drawing.Size(100, 23);
            btnSelectFolder.TabIndex = 0;
            btnSelectFolder.Text = "Klasör Seçin";
            btnSelectFolder.UseVisualStyleBackColor = true;
            btnSelectFolder.Click += new EventHandler(BtnSelectFolder_Click);

            // 
            // txtSelectedPath
            // 
            txtSelectedPath.Location = new System.Drawing.Point(12, 41);
            txtSelectedPath.Name = "txtSelectedPath";
            txtSelectedPath.Size = new System.Drawing.Size(260, 20);
            txtSelectedPath.TabIndex = 1;

            // 
            // FolderSelector
            // 
            ClientSize = new System.Drawing.Size(284, 71);
            Controls.Add(txtSelectedPath);
            Controls.Add(btnSelectFolder);
            Name = "FolderSelector";
            Text = "Klasör Seçici";
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            ResumeLayout(false);
            PerformLayout();
        }

        private void BtnSelectFolder_Click(object sender, EventArgs e)
        {
            StringBuilder path = new StringBuilder(260);
            BROWSEINFO bi = new BROWSEINFO
            {
                hwndOwner = Handle,
                lpszTitle = "D",
                ulFlags = BIF_RETURNONLYFSDIRS | BIF_NEWDIALOGSTYLE
            };

            IntPtr pidl = SHBrowseForFolder(ref bi);

            if (pidl != IntPtr.Zero)
            {
                if (SHGetPathFromIDList(pidl, path))
                {
                    txtSelectedPath.Text = path.ToString();
                    // Klasör ile ilgili işlemler
                }
            }
        }

        [DllImport("shell32.dll")]
        private static extern IntPtr SHBrowseForFolder(ref BROWSEINFO lpbi);

        [DllImport("shell32.dll", CharSet = CharSet.Auto)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool SHGetPathFromIDList(IntPtr pidl, StringBuilder pszPath);

        private const int BIF_RETURNONLYFSDIRS = 0x0001;
        private const int BIF_NEWDIALOGSTYLE = 0x0040;

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct BROWSEINFO
        {
            public IntPtr hwndOwner;
            public IntPtr pidlRoot;
            public IntPtr pszDisplayName;
            [MarshalAs(UnmanagedType.LPTStr)]
            public string lpszTitle;
            public uint ulFlags;
            public IntPtr lpfn;
            public IntPtr lParam;
            public int iImage;
        }
    }
}
