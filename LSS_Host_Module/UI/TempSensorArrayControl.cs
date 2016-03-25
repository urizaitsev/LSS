using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace LSS_Host_Module.UI
{
    public partial class TempSensorArrayControl : UserControl
    {
        public TempSensorArrayControl()
        {
            InitializeComponent();
            _temperatures = new double[maxY * maxX];
            for (int i = 0; i < _temperatures.Length; i++)
                _temperatures[i] = double.NaN;
            _maxTemp = 30;
            _minTemp = 10;
        }

        private void UpdateView()
        {
            if (Temperatures == null)
                return;

            double lowTemp = MinTemp;
            double highTemp = MaxTemp;
            Bitmap bitmap = new Bitmap(cellSize * maxX, cellSize * maxY, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                g.Clear(Color.Black);
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                for (int x = 0; x < maxX; x++)
                { //go through all the rows
                    for (int y = 0; y < maxY; y++)
                    { //go through all the columns
                        int index = x + y * maxX;
                        double temp = _temperatures[index];
                        if (double.IsNaN(temp))
                        {
                            System.Drawing.SolidBrush brush = new System.Drawing.SolidBrush(Color.FromArgb(127, 127, 127));
                            g.FillRectangle(brush, new RectangleF(new PointF(x * cellSize, y * cellSize), new SizeF(cellSize, cellSize)));
                        }
                        else
                        {
                            temp = Math.Max(temp, lowTemp);
                            temp = Math.Min(temp, highTemp);
                            int bluePart = (int)(((highTemp - temp) / (highTemp - lowTemp)) * 255);
                            int redPart = (int)(((temp - lowTemp) / (highTemp - lowTemp)) * 255);
                            System.Drawing.SolidBrush brush = new System.Drawing.SolidBrush(Color.FromArgb(redPart, 0, bluePart));
                            g.FillRectangle(brush, new RectangleF(new PointF(x * cellSize, y * cellSize), new SizeF(cellSize, cellSize)));
                            g.DrawString(temp.ToString(), new Font("Tahoma", 8), Brushes.White, new PointF(x * cellSize + 5, y * cellSize + 5));
                        }
                    }
                }
                g.Flush();
            }
            pictureBox1.Image = bitmap;
        }

        //matrix size
        private int maxX = 4;
        private int maxY = 16;
        private int cellSize = 25;

        private double _maxTemp;
        public double MaxTemp
        {
            get
            {
                return _maxTemp;
            }

            set
            {
                _maxTemp = value;
                UpdateView();
            }
        }

        private double _minTemp;
        public double MinTemp
        {
            get
            {
                return _minTemp;
            }

            set
            {
                _minTemp = value;
                UpdateView();
            }
        }

        private double[] _temperatures;
        public double[] Temperatures
        {
            get
            {
                return _temperatures;
            }
            set
            {
                if ((value != null) && (value.Length == maxX*maxY))
                {
                    _temperatures = value;
                    UpdateView();
                } 
            }
        }

        public float FPS
        {
            set
            {
                labelFPS.Text = string.Format("FPS={0}", value.ToString());
            }
        }
    }
}
