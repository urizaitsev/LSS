using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace ChartPainter3D
{
    /// <summary>
    /// A container which contains data and descriptive elements
    /// to draw a Cylinder Chart using a <see cref="CylinderChartPainer"/>
    /// </summary>
    public class ChartDataContainer
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

        List<string> dataCaptions = new List<string>();
        /// <summary>
        /// Each of the string elements in this list contains a caption for 
        /// a single data element - The list must contain either
        /// zero elements or the same amount of elements as <see cref="Data"/>.
        /// </summary>
        public List<string> DataCaptions
        {
            get { return dataCaptions; }
            set {
                if (value.Count != Data.Count)
                {
                    throw new ArgumentException("The container's data item caption list contains not the same amount of items as the container's data list.");
                }
                dataCaptions = value; 
            }
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

        Color chartElementColor = Colors.LightCoral;
        /// <summary>
        /// Gets or sets the System.Windows.Media.Color which is 
        /// used to colorize a chart element (e.g. a Cylinder)
        /// </summary>
        public Color ChartElementColor
        {
            get { return chartElementColor; }
            set { chartElementColor = value; }
        }
    }
}
