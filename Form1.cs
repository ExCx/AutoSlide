using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace AutoSlide
{
    public partial class Form1 : Form
    {
        List<string> filePaths;
        List<int> randomSeq;
        string currFilePath;
        int currIndex = 0;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            filePaths = Directory.GetFiles(".").Where(file => file.EndsWith(".jpg") || file.EndsWith(".jpeg") || file.EndsWith(".png")).ToList();
            randomSeq = Enumerable.Range(0, filePaths.Count).ToList();
            randomSeq.Shuffle();
            AdvanceToNextImage();
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                AdvanceToNextImage();
            else if (e.Button == MouseButtons.Right) 
            {
                currIndex -= 2;
                if (currIndex < 0) 
                    currIndex = 0;
                AdvanceToNextImage();
            }
        }

        private void AdvanceToNextImage() 
        {
            if (currIndex == filePaths.Count)
            {
                Application.Exit();
                return;
            }
            currFilePath = filePaths[randomSeq[currIndex++]];
            label1.Text = Path.GetFileName(currFilePath);
            pictureBox1.Image = new Bitmap(currFilePath);
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                var pathToDelete = currFilePath;
                AdvanceToNextImage();
                GC.Collect();
                GC.WaitForPendingFinalizers();
                File.Delete(pathToDelete);
            }
            else if (e.KeyCode == Keys.Escape)
                Application.Exit();
        }
    }
}