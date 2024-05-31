using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace FileOperationManager
{
    public partial class Main : Form
    {
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        private struct SHFILEOPSTRUCT
        {
            public IntPtr hwnd;
            [MarshalAs(UnmanagedType.U4)]
            public int wFunc;
            public string pFrom;
            public string pTo;
            public short fFlags;
            [MarshalAs(UnmanagedType.Bool)]
            public bool fAnyOperationsAborted;
            public IntPtr hNameMappings;
            public string lpszProgressTitle;
        }

        private const int FO_COPY = 0x0002;
        private const int FO_MOVE = 0x0001;
        private const int FOF_NOCONFIRMATION = 0x0010;
        private const int FOF_SIMPLEPROGRESS = 0x0100;

        [DllImport("shell32.dll", CharSet = CharSet.Auto)]
        private static extern int SHFileOperation(ref SHFILEOPSTRUCT FileOp);

        public Main()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            btnSelectSource = new Button();
            btnSelectDestination = new Button();
            btnCopy = new Button();
            btnMove = new Button();
            txtSource = new TextBox();
            txtDestination = new TextBox();
            SuspendLayout();
            // 
            // btnSelectSource
            // 
            btnSelectSource.Location = new Point(12, 12);
            btnSelectSource.Name = "btnSelectSource";
            btnSelectSource.Size = new Size(150, 23);
            btnSelectSource.TabIndex = 0;
            btnSelectSource.Text = "Kaynak Seçiniz";
            btnSelectSource.UseVisualStyleBackColor = true;
            btnSelectSource.Click += new EventHandler(btnSelectSource_Click);
            // 
            // btnSelectDestination
            // 
            btnSelectDestination.Location = new Point(12, 41);
            btnSelectDestination.Name = "btnSelectDestination";
            btnSelectDestination.Size = new Size(150, 23);
            btnSelectDestination.TabIndex = 1;
            btnSelectDestination.Text = "Hedef Seçin";
            btnSelectDestination.UseVisualStyleBackColor = true;
            btnSelectDestination.Click += new EventHandler(btnSelectDestination_Click);
            // 
            // btnCopy
            // 
            btnCopy.Location = new Point(12, 70);
            btnCopy.Name = "btnCopy";
            btnCopy.Size = new Size(150, 23);
            btnCopy.TabIndex = 2;
            btnCopy.Text = "Kopyala";
            btnCopy.UseVisualStyleBackColor = true;
            btnCopy.Click += new EventHandler(btnCopy_Click);
            // 
            // btnMove
            // 
            btnMove.Location = new Point(12, 99);
            btnMove.Name = "btnMove";
            btnMove.Size = new Size(150, 23);
            btnMove.TabIndex = 3;
            btnMove.Text = "Taşı";
            btnMove.UseVisualStyleBackColor = true;
            btnMove.Click += new EventHandler(btnMove_Click);
            // 
            // txtSource
            // 
            txtSource.Location = new Point(168, 14);
            txtSource.Name = "txtSource";
            txtSource.ReadOnly = true;
            txtSource.Size = new Size(300, 20);
            txtSource.TabIndex = 4;
            // 
            // txtDestination
            // 
            txtDestination.Location = new Point(168, 43);
            txtDestination.Name = "txtDestination";
            txtDestination.ReadOnly = true;
            txtDestination.Size = new Size(300, 20);
            txtDestination.TabIndex = 5;
            // 
            // Main
            // 
            AutoScaleDimensions = new SizeF(6F, 13F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(484, 141);
            Controls.Add(txtDestination);
            Controls.Add(txtSource);
            Controls.Add(btnMove);
            Controls.Add(btnCopy);
            Controls.Add(btnSelectDestination);
            Controls.Add(btnSelectSource);
            Name = "Main";
            Text = "Dosya İşlem Yöneticisi";
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            ResumeLayout(false);
            PerformLayout();

        }

        private Button btnSelectSource;
        private Button btnSelectDestination;
        private Button btnCopy;
        private Button btnMove;
        private TextBox txtSource;
        private TextBox txtDestination;

        private void btnSelectSource_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    txtSource.Text = openFileDialog.FileName;
                }
            }
        }

        private void btnSelectDestination_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog())
            {
                if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                {
                    txtDestination.Text = folderBrowserDialog.SelectedPath;
                }
            }
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            PerformFileOperation(FO_COPY);
        }

        private void btnMove_Click(object sender, EventArgs e)
        {
            PerformFileOperation(FO_MOVE);
        }

        private void PerformFileOperation(int operation)
        {
            SHFILEOPSTRUCT fileOp = new SHFILEOPSTRUCT
            {
                wFunc = operation,
                pFrom = txtSource.Text + '\0' + '\0',
                pTo = txtDestination.Text + '\0' + '\0',
                fFlags = FOF_NOCONFIRMATION | FOF_SIMPLEPROGRESS
            };

            int result = SHFileOperation(ref fileOp);

            if (result == 0)
            {
                MessageBox.Show(operation == FO_COPY ? "Kopyalama işlemi başarıyla tamamlandı." : "Taşıma işlemi başarıyla tamamlanmıştır.");
            }
            else
            {
                MessageBox.Show($"İşlem sırasında bir hata oluştu. Hata kodu: {result}", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
