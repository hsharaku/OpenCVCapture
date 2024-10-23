using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

using OpenCvSharp;

namespace OpenCVCapture
{
    public partial class Form1 : Form
    {
        Mat frame;
        VideoCapture capture;
        Bitmap bmp;
        Graphics graphic;

        public Form1()
        {
            InitializeComponent();

            //カメラ画像取得用のVideoCapture作成
            capture = new VideoCapture(0);
            if (!capture.IsOpened())
            {
                MessageBox.Show("camera was not found!");
                this.Close();
            }

            System.Diagnostics.Debug.WriteLine(capture.FrameWidth + ", " + capture.FrameHeight);

            pictureBox1.Width = capture.FrameWidth;
            pictureBox1.Height = capture.FrameHeight;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //取得先のMat作成
            frame = new Mat(capture.FrameHeight, capture.FrameWidth, MatType.CV_8UC3);
            capture.Grab();
            OpenCvSharp.Internal.NativeMethods.videoio_VideoCapture_operatorRightShift_Mat(capture.CvPtr, frame.CvPtr);

            //表示用のBitmap作成
            bmp = new Bitmap(frame.Cols, frame.Rows, (int)frame.Step(), System.Drawing.Imaging.PixelFormat.Format24bppRgb, frame.Data);
            pictureBox1.Refresh();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            if (bmp != null)
            {
                e.Graphics.DrawImage(bmp, 0, 0);
            }
        }
    }
}
