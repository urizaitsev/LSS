using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace ChartPainter3D
{
    public static class CylinderChartPainter
    {
        public static void DrawChart(ChartDataContainer container, System.Windows.Controls.Canvas cnv)
        {
            //Draw the X-Axis line of the chart
            Line xLine = new Line();
            xLine.Stroke = Brushes.Black;
            xLine.X1 = container.Offset.X;
            xLine.X2 = container.Offset.X + container.MaxSize.X;
            xLine.Y1 = container.Offset.Y + container.MaxSize.Y;
            xLine.Y2 = container.Offset.Y + container.MaxSize.Y;

            xLine.StrokeThickness = 1;
            xLine.SnapsToDevicePixels = true;

            cnv.Children.Add(xLine);

            //Draw the Y-Axis Line of the chart.
            Line yLine = new Line();
            yLine.Stroke = Brushes.Black;
            yLine.X1 = container.Offset.X;
            yLine.Y1 = container.Offset.Y;
            yLine.X2 = container.Offset.X;
            yLine.Y2 = xLine.Y2;

            yLine.StrokeThickness = 1;
            yLine.SnapsToDevicePixels = true;

            cnv.Children.Add(yLine);

            //Add Y-Axis description text to the chart
            TextBlock yAxisDescriptionBlock = new TextBlock();
            yAxisDescriptionBlock.Text = container.YAxisText;
            Canvas.SetLeft(yAxisDescriptionBlock, yLine.X1 + 5);
            Canvas.SetRight(yAxisDescriptionBlock, xLine.X2);
            Canvas.SetTop(yAxisDescriptionBlock, container.Offset.Y);
            cnv.Children.Add(yAxisDescriptionBlock);

            //Add X-Axis description text to the chart
            TextBlock xAxisDescriptionBlock = new TextBlock();
            xAxisDescriptionBlock.Text = container.XAxisText;
            Canvas.SetTop(xAxisDescriptionBlock, yLine.Y2 - 12);
            Canvas.SetLeft(xAxisDescriptionBlock, xLine.X2 + 3);
            Canvas.SetRight(xAxisDescriptionBlock, xLine.X2 + container.YAxisText.Length);
            cnv.Children.Add(xAxisDescriptionBlock);

            
            //Add Cylinder graphics
            double offsetX = container.Offset.X + 25;//Offset on the X-Axis
            double scale =  container.MaxSize.Y / container.Data.Max(); //Scaling value - Needed to make the chart
                                                                        //more readable
            double radius = (container.MaxSize.X / (container.Data.Count + 2.5)) / 2;   //Radius of a single chart element
                                                                                        // = diameter/ 2
            CylinderPainter3D cylinderPainter = new CylinderPainter3D();
            cylinderPainter.CylinderRadius = radius;                    // Set the radius of the cylinder to be drawn
            cylinderPainter.FillingColor = container.ChartElementColor; // Set the Cylinder's color
            foreach(double dataelement in container.Data)
            {
                double topY = (container.Offset.Y + (container.MaxSize.Y - (dataelement*scale))) - 26;  //Calculate Y-Offset
                cylinderPainter.CylinderHeight = dataelement * scale;   //Apply scale and set height
                cylinderPainter.DrawCylinder(cnv, offsetX, topY);   //Draw the Cylinder
                offsetX += cylinderPainter.CylinderRadius * 2.5;    //Increase offset
            }

            
            if (container.DataCaptions.Count > 0)//If available, add captions to cylinder graphics
            {
                offsetX = container.Offset.X + 25; //Offset on the X-Axis is reset because captioning is started from the left
                short zipperOffset = 10;
                foreach (string caption in container.DataCaptions)
                {
                    zipperOffset *= (-1);
                    //Add line to associate a caption with a single chart element
                    Line captionLine = new Line();
                    captionLine.Stroke = Brushes.Black;
                    captionLine.X1 = offsetX;
                    captionLine.X2 = offsetX;
                    captionLine.Y1 = yLine.Y2;
                    captionLine.Y2 = yLine.Y2 + 40 + zipperOffset;

                    cnv.Children.Add(captionLine);

                    //Add chart element description text block
                    TextBlock chartElementDescriptionTextBlock = new TextBlock();
                    chartElementDescriptionTextBlock.Text = caption;
                    Canvas.SetTop(chartElementDescriptionTextBlock, yLine.Y2 + 47 + zipperOffset);
                    Canvas.SetLeft(chartElementDescriptionTextBlock, offsetX);
                    offsetX += radius * 2.5;
                    Canvas.SetRight(chartElementDescriptionTextBlock, offsetX);
                    cnv.Children.Add(chartElementDescriptionTextBlock);
                }
            }
        }
    }
}
