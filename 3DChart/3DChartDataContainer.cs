using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;

namespace ChartPainter3D
{
    class ChartDataContainer
    {
        List<double> data;
        /// <summary>
        /// Each of the elements in this list represent a single data 
        /// representation in the Chart
        /// </summary>
        public List<double> Data
        {
            get { return data; }
            set { data = value; }
        }

        Point maxSize;
        /// <summary>
        /// Size of the Chart, stored
        /// as X and Y coordinates.
        /// </summary>
        public Point MaxSize
        {
            get { return maxSize; }
            set { maxSize = value; }
        }

        Point offset;
        /// <summary>
        /// Offset from the top left corner
        /// of the Canvas on which the Chart is drawn
        /// on. Stored as X and Y coordinates.
        /// </summary>
        public Point Offset
        {
            get { return offset; }
            set { offset = value; }
        }

        string yAxisText = "Y-Axis";
        /// <summary>
        /// Text which is used to describe the X-Axis of 
        /// the chart.
        /// </summary>
        public string YAxisText
        {
            get { return yAxisText; }
            set { yAxisText = value; }
        }

        string xAxisText = "X-Axis";
        /// <summary>
        /// Text which is used to describe the Y-Axis of
        /// the chart.
        /// </summary>
        public string XAxisText
        {
            get { return xAxisText; }
            set { xAxisText = value; }
        }
    }
}
