using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace FileInfo
{
    public partial class Main : Form
    {
        private Button btnSelectFile;
        private TextBox txtFilePath;
        private ListView listView;

        public Main()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            btnSelectFile = new Button();
            txtFilePath = new TextBox();
            listView = new ListView();

            SuspendLayout();

            // 
            // btnSelectFile
            // 
            btnSelectFile.Location = new System.Drawing.Point(12, 12);
            btnSelectFile.Name = "btnSelectFile";
            btnSelectFile.Size = new System.Drawing.Size(75, 23);
            btnSelectFile.TabIndex = 0;
            btnSelectFile.Text = "Select File";
            btnSelectFile.UseVisualStyleBackColor = true;
            btnSelectFile.Click += new EventHandler(BtnSelectFile_Click);

            // 
            // txtFilePath
            // 
            txtFilePath.Location = new System.Drawing.Point(93, 12);
            txtFilePath.Name = "txtFilePath";
            txtFilePath.Size = new System.Drawing.Size(400, 20);
            txtFilePath.TabIndex = 1;

            // 
            // listView
            // 
            listView.Location = new System.Drawing.Point(12, 41);
            listView.Name = "listView";
            listView.Size = new System.Drawing.Size(481, 200);
            listView.TabIndex = 2;
            listView.View = View.Details;

            // Add columns to the listView
            listView.Columns.Add("Property", 150);
            listView.Columns.Add("Value", 300);

            // 
            // MainForm
            // 
            ClientSize = new System.Drawing.Size(505, 253);
            Controls.Add(listView);
            Controls.Add(txtFilePath);
            Controls.Add(btnSelectFile);
            Name = "Main";
            Text = "Dosya Özellikleri";
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            ResumeLayout(false);
            PerformLayout();

        }

        private void BtnSelectFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                txtFilePath.Text = openFileDialog.FileName;
                ShowFileProperties(openFileDialog.FileName);
            }
        }

        private void ShowFileProperties(string filePath)
        {
            listView.Items.Clear();

            // Get the file info
            FileInfo fileInfo = new FileInfo(filePath);
            AddPropertyToListView("File Path", fileInfo.FullName);
            AddPropertyToListView("Size", fileInfo.Length.ToString() + " bytes");
            AddPropertyToListView("Creation Time", fileInfo.CreationTime.ToString());
            AddPropertyToListView("Last Access Time", fileInfo.LastAccessTime.ToString());
            AddPropertyToListView("Last Write Time", fileInfo.LastWriteTime.ToString());

            // Get the shell info
            Shell32.SHFILEINFO shinfo = new Shell32.SHFILEINFO();
            Shell32.SHGetFileInfo(filePath, 0, ref shinfo, (uint)Marshal.SizeOf(shinfo), Shell32.SHGFI_TYPENAME);
            AddPropertyToListView("Type", shinfo.szTypeName);
        }

        private void AddPropertyToListView(string property, string value)
        {
            ListViewItem item = new ListViewItem(property);
            item.SubItems.Add(value);
            listView.Items.Add(item);
        }
    }
}
