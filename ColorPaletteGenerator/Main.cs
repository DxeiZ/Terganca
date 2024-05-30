using System;
using System.Drawing;
using System.Windows.Forms;

namespace ColorPaletteGenerator
{
    public partial class Main : Form
    {
        private PictureBox pictureBox;
        private FlowLayoutPanel colorPanel;
        private Button btnLoadImage;
        private OpenFileDialog openFileDialog;

        public Main()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            pictureBox = new PictureBox();
            colorPanel = new FlowLayoutPanel();
            btnLoadImage = new Button();
            openFileDialog = new OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)(pictureBox)).BeginInit();
            SuspendLayout();
            // 
            // pictureBox
            // 
            pictureBox.BorderStyle = BorderStyle.FixedSingle;
            pictureBox.Location = new Point(12, 12);
            pictureBox.Name = "pictureBox";
            pictureBox.Size = new Size(360, 199);
            pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox.TabIndex = 0;
            pictureBox.TabStop = false;
            pictureBox.MouseClick += new MouseEventHandler(PictureBox_MouseClick);
            // 
            // colorPanel
            // 
            colorPanel.AutoScroll = true;
            colorPanel.Location = new Point(12, 220);
            colorPanel.Name = "colorPanel";
            colorPanel.Size = new Size(360, 50);
            colorPanel.TabIndex = 1;
            // 
            // btnLoadImage
            // 
            btnLoadImage.Location = new Point(12, 280);
            btnLoadImage.Name = "btnLoadImage";
            btnLoadImage.Size = new Size(100, 23);
            btnLoadImage.TabIndex = 2;
            btnLoadImage.Text = "Resim Yükle";
            btnLoadImage.UseVisualStyleBackColor = true;
            btnLoadImage.Click += new System.EventHandler(BtnLoadImage_Click);
            // 
            // openFileDialog
            // 
            openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif|All Files|*.*";
            // 
            // Main
            // 
            ClientSize = new Size(384, 311);
            Controls.Add(btnLoadImage);
            Controls.Add(colorPanel);
            Controls.Add(pictureBox);
            Name = "Main";
            Text = "Renk Paleti Oluşturucu";
            ((System.ComponentModel.ISupportInitialize)(pictureBox)).EndInit();
            ResumeLayout(false);

        }

        private void PictureBox_MouseClick(object sender, MouseEventArgs e)
        {
            if (pictureBox.Image != null)
            {
                Bitmap bmp = new Bitmap(pictureBox.Image);
                Color pixelColor = bmp.GetPixel(e.X, e.Y);
                AddColorToPalette(pixelColor);
            }
            else
            {
                MessageBox.Show("Renkleri seçmeden önce lütfen bir resim yükleyin.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AddColorToPalette(Color color)
        {
            Panel colorBox = new Panel
            {
                Size = new Size(30, 30),
                BackColor = color,
                Margin = new Padding(5)
            };

            colorPanel.Controls.Add(colorBox);
        }

        private void BtnLoadImage_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string imagePath = openFileDialog.FileName;
                pictureBox.Image = Image.FromFile(imagePath);
            }
        }
    }
}
