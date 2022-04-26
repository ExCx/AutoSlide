using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoSlide
{
    public partial class Form1 : Form
    {
        List<string> filePaths;
        List<int> randomSeq;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            filePaths = Directory.GetFiles(".").Where(file => file.EndsWith(".jpg") || file.EndsWith(".jpeg") || file.EndsWith(".png")).ToList();
            randomSeq = Enumerable.Range(0, filePaths.Count).ToList();
            randomSeq.Shuffle();
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (randomSeq.Count == 0)
                Application.Exit();
            label1.Text = randomSeq[0].ToString();
            pictureBox1.Image = new Bitmap(filePaths[randomSeq[0]]);
            randomSeq.RemoveAt(0);
        }
    }
}