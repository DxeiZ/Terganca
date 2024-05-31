using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Windows.Forms;

namespace Shortcut
{
    public partial class Main : Form
    {
        private Button btnSelectFile;
        private Button btnCreateShortcut;
        private TextBox txtFilePath;
        private TextBox txtShortcutName;
        private OpenFileDialog openFileDialog;

        public Main()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            btnSelectFile = new Button();
            btnCreateShortcut = new Button();
            txtFilePath = new TextBox();
            txtShortcutName = new TextBox();
            openFileDialog = new OpenFileDialog();

            SuspendLayout();

            // 
            // btnSelectFile
            // 
            btnSelectFile.Location = new System.Drawing.Point(12, 12);
            btnSelectFile.Name = "btnSelectFile";
            btnSelectFile.Size = new System.Drawing.Size(75, 23);
            btnSelectFile.TabIndex = 0;
            btnSelectFile.Text = "Dosya Seçiniz";
            btnSelectFile.UseVisualStyleBackColor = true;
            btnSelectFile.Click += new EventHandler(BtnSelectFile_Click);

            // 
            // btnCreateShortcut
            // 
            btnCreateShortcut.Location = new System.Drawing.Point(12, 70);
            btnCreateShortcut.Name = "btnCreateShortcut";
            btnCreateShortcut.Size = new System.Drawing.Size(100, 23);
            btnCreateShortcut.TabIndex = 1;
            btnCreateShortcut.Text = "Kısayol Oluştur";
            btnCreateShortcut.UseVisualStyleBackColor = true;
            btnCreateShortcut.Click += new EventHandler(BtnCreateShortcut_Click);

            // 
            // txtFilePath
            // 
            txtFilePath.Location = new System.Drawing.Point(93, 14);
            txtFilePath.Name = "txtFilePath";
            txtFilePath.Size = new System.Drawing.Size(300, 20);
            txtFilePath.TabIndex = 2;

            // 
            // txtShortcutName
            // 
            txtShortcutName.Location = new System.Drawing.Point(93, 43);
            txtShortcutName.Name = "txtShortcutName";
            txtShortcutName.Size = new System.Drawing.Size(300, 20);
            txtShortcutName.TabIndex = 3;
            txtShortcutName.Text = "Kısayol Adı";

            // 
            // Main
            // 
            ClientSize = new System.Drawing.Size(405, 105);
            Controls.Add(txtShortcutName);
            Controls.Add(txtFilePath);
            Controls.Add(btnCreateShortcut);
            Controls.Add(btnSelectFile);
            Name = "Main";
            Text = "Masaüstü Kısayol Oluşturucu";
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            ResumeLayout(false);
            PerformLayout();

        }

        private void BtnSelectFile_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                txtFilePath.Text = openFileDialog.FileName;
            }
        }

        private void BtnCreateShortcut_Click(object sender, EventArgs e)
        {
            string filePath = txtFilePath.Text;
            string shortcutName = txtShortcutName.Text;

            if (string.IsNullOrEmpty(filePath) || string.IsNullOrEmpty(shortcutName))
            {
                MessageBox.Show("Lütfen bir dosya seçin ve bir kısayol adı girin.");
                return;
            }

            CreateShortcut(filePath, shortcutName);
            MessageBox.Show("Kısayol başarıyla oluşturuldu!");
        }

        private void CreateShortcut(string filePath, string shortcutName)
        {
            IShellLink link = (IShellLink)new ShellLink();
            link.SetPath(filePath);
            link.SetDescription(shortcutName);

            IPersistFile file = (IPersistFile)link;
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            string shortcutPath = System.IO.Path.Combine(desktopPath, shortcutName + ".lnk");

            file.Save(shortcutPath, false);
        }

        [ComImport]
        [Guid("00021401-0000-0000-C000-000000000046")]
        internal class ShellLink
        {
        }

        [ComImport]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        [Guid("000214F9-0000-0000-C000-000000000046")]
        internal interface IShellLink
        {
            void GetPath([Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszFile, int cchMaxPath, out IntPtr pfd, int fFlags);
            void GetIDList(out IntPtr ppidl);
            void SetIDList(IntPtr pidl);
            void GetDescription([Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszName, int cchMaxName);
            void SetDescription([MarshalAs(UnmanagedType.LPWStr)] string pszName);
            void GetWorkingDirectory([Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszDir, int cchMaxPath);
            void SetWorkingDirectory([MarshalAs(UnmanagedType.LPWStr)] string pszDir);
            void GetArguments([Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszArgs, int cchMaxPath);
            void SetArguments([MarshalAs(UnmanagedType.LPWStr)] string pszArgs);
            void GetHotkey(out short pwHotkey);
            void SetHotkey(short wHotkey);
            void GetShowCmd(out int piShowCmd);
            void SetShowCmd(int iShowCmd);
            void GetIconLocation([Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszIconPath, int cchIconPath, out int piIcon);
            void SetIconLocation([MarshalAs(UnmanagedType.LPWStr)] string pszIconPath, int iIcon);
            void SetRelativePath([MarshalAs(UnmanagedType.LPWStr)] string pszPathRel, int dwReserved);
            void Resolve(IntPtr hwnd, int fFlags);
            void SetPath([MarshalAs(UnmanagedType.LPWStr)] string pszFile);
        }
    }
}
