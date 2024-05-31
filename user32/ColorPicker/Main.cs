using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace ColorPaletteGenerator
{
    public partial class Main : Form
    {
        private Panel colorPanel;
        private ListBox colorListBox;
        private List<Color> colorPalette;
        private Timer timer;

        public Main()
        {
            InitializeComponent();
            colorPalette = new List<Color>();

            timer = new Timer();
            timer.Interval = 500;
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void InitializeComponent()
        {
            colorPanel = new Panel();
            colorListBox = new ListBox();

            SuspendLayout();

            // 
            // colorPanel
            // 
            colorPanel.Location = new Point(12, 12);
            colorPanel.Name = "colorPanel";
            colorPanel.Size = new Size(50, 50);
            colorPanel.TabIndex = 1;
            colorPanel.BackColor = Color.White;

            // 
            // colorListBox
            // 
            colorListBox.FormattingEnabled = true;
            colorListBox.Location = new Point(12, 70);
            colorListBox.Name = "colorListBox";
            colorListBox.Size = new Size(200, 147);
            colorListBox.TabIndex = 2;

            // 
            // Main
            // 
            ClientSize = new Size(284, 261);
            Controls.Add(colorListBox);
            Controls.Add(colorPanel);
            Name = "Main";
            Text = "Renk Seçici";
            ResumeLayout(false);

        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            Color color = ColorPicker.GetColorAtCursor();
            if (!colorPalette.Contains(color))
            {
                colorPanel.BackColor = color;
                colorPalette.Add(color);
                colorListBox.Items.Add(ColorTranslator.ToHtml(color));
            }
        }
    }
}
