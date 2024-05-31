using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Shell32;

namespace RecycleBinManager
{
    public partial class Main : Form
    {
        [DllImport("shell32.dll", CharSet = CharSet.Unicode)]
        public static extern uint SHEmptyRecycleBin(IntPtr hwnd, string pszRootPath, uint dwFlags);

        const uint SHERB_NOCONFIRMATION = 0x00000001;
        const uint SHERB_NOPROGRESSUI = 0x00000002;
        const uint SHERB_NOSOUND = 0x00000004;

        public Main()
        {
            InitializeComponent();
        }

        private void btnListItems_Click(object sender, EventArgs e)
        {
            listBoxRecycleBin.Items.Clear();

            Shell shell = new Shell();
            Folder recycleBin = shell.NameSpace(10);

            foreach (FolderItem2 item in recycleBin.Items())
            {
                listBoxRecycleBin.Items.Add(item.Name);
            }
        }

        private void btnRestoreItem_Click(object sender, EventArgs e)
        {
            if (listBoxRecycleBin.SelectedItem != null)
            {
                string selectedItemName = listBoxRecycleBin.SelectedItem.ToString();

                Shell shell = new Shell();
                Folder recycleBin = shell.NameSpace(10);

                foreach (FolderItem2 item in recycleBin.Items())
                {
                    if (item.Name == selectedItemName)
                    {
                        item.InvokeVerb("RESTORE");
                        MessageBox.Show($"{selectedItemName} geri yüklendi.");
                        listBoxRecycleBin.Items.Remove(selectedItemName);
                        break;
                    }
                }
            }
            else
            {
                MessageBox.Show("Lütfen geri yüklemek için bir öğe seçin.", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDeleteItem_Click(object sender, EventArgs e)
        {
            if (listBoxRecycleBin.SelectedItem != null)
            {
                string selectedItemName = listBoxRecycleBin.SelectedItem.ToString();

                Shell shell = new Shell();
                Folder recycleBin = shell.NameSpace(10);

                foreach (FolderItem2 item in recycleBin.Items())
                {
                    if (item.Name == selectedItemName)
                    {
                        item.InvokeVerb("DELETE");
                        MessageBox.Show($"{selectedItemName} kalıcı olarak silinmiştir.");
                        listBoxRecycleBin.Items.Remove(selectedItemName);
                        break;
                    }
                }
            }
            else
            {
                MessageBox.Show("Lütfen silmek için bir öğe seçin.", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEmptyRecycleBin_Click(object sender, EventArgs e)
        {
            uint result = SHEmptyRecycleBin(IntPtr.Zero, null, SHERB_NOCONFIRMATION);

            if (result == 0)
            {
                MessageBox.Show("Geri Dönüşüm Kutusu başarıyla boşaltıldı.");
                listBoxRecycleBin.Items.Clear();
            }
            else
            {
                MessageBox.Show($"Geri Dönüşüm Kutusu boşaltılırken bir hata oluştu. Hata kodu: {result}", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void InitializeComponent()
        {
            btnListItems = new Button();
            btnRestoreItem = new Button();
            btnDeleteItem = new Button();
            btnEmptyRecycleBin = new Button();
            listBoxRecycleBin = new ListBox();
            SuspendLayout();
            // 
            // btnListItems
            // 
            btnListItems.Location = new System.Drawing.Point(12, 12);
            btnListItems.Name = "btnListItems";
            btnListItems.Size = new System.Drawing.Size(180, 23);
            btnListItems.TabIndex = 0;
            btnListItems.Text = "Öğeleri Listele";
            btnListItems.UseVisualStyleBackColor = true;
            btnListItems.Click += new EventHandler(btnListItems_Click);
            // 
            // btnRestoreItem
            // 
            btnRestoreItem.Location = new System.Drawing.Point(12, 41);
            btnRestoreItem.Name = "btnRestoreItem";
            btnRestoreItem.Size = new System.Drawing.Size(180, 23);
            btnRestoreItem.TabIndex = 1;
            btnRestoreItem.Text = "Seçili Öğeyi Geri Yükle";
            btnRestoreItem.UseVisualStyleBackColor = true;
            btnRestoreItem.Click += new EventHandler(btnRestoreItem_Click);
            // 
            // btnDeleteItem
            // 
            btnDeleteItem.Location = new System.Drawing.Point(12, 70);
            btnDeleteItem.Name = "btnDeleteItem";
            btnDeleteItem.Size = new System.Drawing.Size(180, 23);
            btnDeleteItem.TabIndex = 2;
            btnDeleteItem.Text = "Seçili Öğeyi Sil";
            btnDeleteItem.UseVisualStyleBackColor = true;
            btnDeleteItem.Click += new EventHandler(btnDeleteItem_Click);
            // 
            // btnEmptyRecycleBin
            // 
            btnEmptyRecycleBin.Location = new System.Drawing.Point(12, 99);
            btnEmptyRecycleBin.Name = "btnEmptyRecycleBin";
            btnEmptyRecycleBin.Size = new System.Drawing.Size(180, 23);
            btnEmptyRecycleBin.TabIndex = 3;
            btnEmptyRecycleBin.Text = "Geri Dönüşüm Kutusunu Boşaltın";
            btnEmptyRecycleBin.UseVisualStyleBackColor = true;
            btnEmptyRecycleBin.Click += new EventHandler(btnEmptyRecycleBin_Click);
            // 
            // listBoxRecycleBin
            // 
            listBoxRecycleBin.FormattingEnabled = true;
            listBoxRecycleBin.Location = new System.Drawing.Point(198, 12);
            listBoxRecycleBin.Name = "listBoxRecycleBin";
            listBoxRecycleBin.Size = new System.Drawing.Size(260, 108);
            listBoxRecycleBin.TabIndex = 4;
            // 
            // Main
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(484, 141);
            Controls.Add(listBoxRecycleBin);
            Controls.Add(btnEmptyRecycleBin);
            Controls.Add(btnDeleteItem);
            Controls.Add(btnRestoreItem);
            Controls.Add(btnListItems);
            Name = "Main";
            Text = "Geri Dönüşüm Kutusu Yöneticisi";
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;

            ResumeLayout(false);

        }

        private Button btnListItems;
        private Button btnRestoreItem;
        private Button btnDeleteItem;
        private Button btnEmptyRecycleBin;
        private ListBox listBoxRecycleBin;
    }
}
