using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace CreateFolder
{
    public partial class Main : Form
    {
        [DllImport("shell32.dll", CharSet = CharSet.Unicode)]
        private static extern int SHCreateDirectoryEx(IntPtr hwnd, string pszPath, IntPtr psa);

        public Main()
        {
            InitializeComponent();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog())
            {
                DialogResult result = folderBrowserDialog.ShowDialog();
                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(folderBrowserDialog.SelectedPath))
                {
                    txtFolderPath.Text = folderBrowserDialog.SelectedPath;
                }
            }
        }

        private void btnCreateFolder_Click(object sender, EventArgs e)
        {
            string folderPath = txtFolderPath.Text;
            string folderName = txtFolderName.Text;

            if (!string.IsNullOrEmpty(folderPath) && !string.IsNullOrEmpty(folderName))
            {
                string fullPath = System.IO.Path.Combine(folderPath, folderName);
                CreateFolder(fullPath);
            }
            else
            {
                lblResult.Text = "Lütfen geçerli bir klasör yolu ve adı girin.";
            }
        }

        private void CreateFolder(string folderPath)
        {
            int result = SHCreateDirectoryEx(IntPtr.Zero, folderPath, IntPtr.Zero);

            if (result == 0)
            {
                MessageBox.Show($"'{folderPath}' klasörü başarıyla oluşturuldu.", "BAŞARILI", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                lblResult.Text = $"Klasör oluşturulamadı. Hata kodu: {result}";
            }
        }

        private TextBox txtFolderPath;
        private Button btnBrowse;
        private TextBox txtFolderName;
        private Button btnCreateFolder;
        private Label lblResult;


        private void InitializeComponent()
        {
            txtFolderPath = new TextBox();
            btnBrowse = new Button();
            txtFolderName = new TextBox();
            btnCreateFolder = new Button();
            lblResult = new Label();
            SuspendLayout();

            // txtFolderPath
            txtFolderPath.Location = new System.Drawing.Point(12, 12);
            txtFolderPath.Name = "txtFolderPath";
            txtFolderPath.Size = new System.Drawing.Size(260, 20);
            txtFolderPath.TabIndex = 0;

            // btnBrowse
            btnBrowse.Location = new System.Drawing.Point(278, 10);
            btnBrowse.Name = "btnBrowse";
            btnBrowse.Size = new System.Drawing.Size(75, 23);
            btnBrowse.TabIndex = 1;
            btnBrowse.Text = "Gözat";
            btnBrowse.UseVisualStyleBackColor = true;
            btnBrowse.Click += new EventHandler(btnBrowse_Click);

            // txtFolderName
            txtFolderName.Location = new System.Drawing.Point(12, 38);
            txtFolderName.Name = "txtFolderName";
            txtFolderName.Size = new System.Drawing.Size(260, 20);
            txtFolderName.TabIndex = 2;

            // btnCreateFolder
            btnCreateFolder.Location = new System.Drawing.Point(12, 64);
            btnCreateFolder.Name = "btnCreateFolder";
            btnCreateFolder.Size = new System.Drawing.Size(100, 23);
            btnCreateFolder.TabIndex = 3;
            btnCreateFolder.Text = "Klasör Oluştur";
            btnCreateFolder.UseVisualStyleBackColor = true;
            btnCreateFolder.Click += new EventHandler(btnCreateFolder_Click);

            // lblResult
            lblResult.AutoSize = true;
            lblResult.Location = new System.Drawing.Point(12, 90);
            lblResult.Name = "lblResult";
            lblResult.Size = new System.Drawing.Size(0, 13);
            lblResult.TabIndex = 4;

            // Main
            AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(380, 111);
            Controls.Add(lblResult);
            Controls.Add(btnCreateFolder);
            Controls.Add(txtFolderName);
            Controls.Add(btnBrowse);
            Controls.Add(txtFolderPath);
            Name = "Main";
            Text = "Klasör Oluşturma";
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
