using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace ScreenCapture
{
    public partial class Main : Form
    {
        private PictureBox pictureBox;
        private Button btnCapture;
        private Button btnSave;
        private Button btnCopy;

        public Main()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            pictureBox = new PictureBox();
            btnCapture = new Button();
            btnSave = new Button();
            btnCopy = new Button();

            SuspendLayout();

            // 
            // pictureBox
            // 
            pictureBox.Location = new Point(12, 12);
            pictureBox.Name = "pictureBox";
            pictureBox.Size = new Size(360, 199);
            pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox.TabIndex = 0;
            pictureBox.TabStop = false;

            // 
            // btnCapture
            // 
            btnCapture.Location = new Point(12, 217);
            btnCapture.Name = "btnCapture";
            btnCapture.Size = new Size(75, 23);
            btnCapture.TabIndex = 1;
            btnCapture.Text = "Yakala";
            btnCapture.UseVisualStyleBackColor = true;
            btnCapture.Click += new EventHandler(BtnCapture_Click);

            // 
            // btnSave
            // 
            btnSave.Location = new Point(93, 217);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(75, 23);
            btnSave.TabIndex = 2;
            btnSave.Text = "Kaydet";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += new EventHandler(BtnSave_Click);

            //
            // btnCopy
            //
            btnCopy.Location = new Point(174, 217);
            btnCopy.Name = "btnCopy";
            btnCopy.Size = new Size(75, 23);
            btnCopy.TabIndex = 3;
            btnCopy.Text = "Kopyala";
            btnCopy.UseVisualStyleBackColor = true;
            btnCopy.Click += new EventHandler(BtnCopy_Click);

            // 
            // Main
            // 
            ClientSize = new Size(384, 251);
            Controls.Add(btnCopy);
            Controls.Add(btnSave);
            Controls.Add(btnCapture);
            Controls.Add(pictureBox);
            Name = "Main";
            Text = "Ekran Görüntüsü";
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            ResumeLayout(false);

        }

        private void BtnCapture_Click(object sender, EventArgs e)
        {
            Bitmap screenshot = ScreenCaptureFunction.CaptureScreen();
            pictureBox.Image = screenshot;
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (pictureBox.Image != null)
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Filter = "PNG Image|*.png|JPEG Image|*.jpg|Bitmap Image|*.bmp",
                    Title = "Ekran Görüntüsünü Kaydet"
                };

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = saveFileDialog.FileName;
                    ImageFormat format = ImageFormat.Png;

                    switch (System.IO.Path.GetExtension(filePath).ToLower())
                    {
                        case ".jpg":
                            format = ImageFormat.Jpeg;
                            break;
                        case ".bmp":
                            format = ImageFormat.Bmp;
                            break;
                    }

                    pictureBox.Image.Save(filePath, format);
                }
            }
            else
            {
                MessageBox.Show("Kaydedilecek görüntü yok.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnCopy_Click(object sender, EventArgs e)
        {
            if (pictureBox.Image != null)
            {
                Clipboard.SetImage(pictureBox.Image);
                MessageBox.Show("Ekran görüntüsü panoya kopyalandı.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Kopyalanacak görüntü yok.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
