using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows;

namespace ChartPainter3D
{
    class CylinderPainter3D : INotifyPropertyChanged
    {

        Color fillingColor = Colors.Blue;
        /// <summary>
        /// Sets the Color which is used to draw the cylinder
        /// </summary>
        public Color FillingColor
        {
            get { return fillingColor; }
            set 
            {
                fillingColor = value;
                NotifyPropertyChanged("FillingColor");
            }
        }

        Color gridLineColor = Colors.Black;
        /// <summary>
        /// Sets the color which is used to draw the grid colors of the Cylinder.
        /// </summary>
        public Color GridLineColor
        {
            get { return gridLineColor; }
            set 
            { 
                gridLineColor = value;
                NotifyPropertyChanged("GridLineColor");
            }
        }

        bool drawGridLines = false;
        /// <summary>
        /// True if gridlines shall be drawn on the cyrcle - false if not.
        /// </summary>
        public bool DrawGridLines
        {
            get { return drawGridLines; }
            set 
            { 
                drawGridLines = value;
                NotifyPropertyChanged("DrawGridLines");
            }
        }

        /// <summary>
        /// Specifies the height of the cylinder
        /// </summary>
        private double cylinderHeight = 200;
        public double CylinderHeight
        {
            get { return this.cylinderHeight; }

            set
            {
                if (value != this.cylinderHeight)
                {
                    this.cylinderHeight = value;
                    NotifyPropertyChanged("CylinderHeight");
                }
            }
        }
        /// <summary>
        /// Specifies the radius of the cylinder
        /// </summary>
        private double cylinderRadius = 100;
        public double CylinderRadius
        {
            get { return this.cylinderRadius; }

            set
            {
                if (value != this.cylinderRadius)
                {
                    this.cylinderRadius = value;
                    NotifyPropertyChanged("CylinderRadius");
                }
            }
        }
        /// <summary>
        /// Decides how many divisions the cylinder will have
        /// </summary>
        private double cylinderDivisions = 16;
        public double CylinderDivisions
        {
            get { return this.cylinderDivisions; }

            set
            {
                if (value != this.cylinderDivisions)
                {
                    this.cylinderDivisions = value;
                    NotifyPropertyChanged("CylinderDivisions");
                }
            }
        }
        /// <summary>
        /// Demo-Usage only. Draws a Cylinder with an X/Y Offset of 100/80
        /// </summary>
        public void DrawCylinder(System.Windows.Controls.Canvas cnv)
        {
            DrawCylinder(cnv, 100, 80);
        }

        /// <summary>
        /// Routine used to draw the cylinder.
        /// </summary>
        public void DrawCylinder(System.Windows.Controls.Canvas cnv, double xOffset, double yOffset)
        {
            double ellipseHeight = 12;
                //Doesn't work with ChatPainter: Convert.ToInt32(Math.Floor(cylinderRadius * 0.3));
            Point ptUpperLeft;
            Point ptUpperRight;
            Point ptLowerLeft;
            Point ptLowerRight;
            Point ptC;

            
            ptUpperLeft = new Point(xOffset, ellipseHeight * 2);
            ptUpperRight = new Point(xOffset + (cylinderRadius * 2), ptUpperLeft.Y);
            ptLowerLeft = new Point(xOffset, ptUpperLeft.Y + cylinderHeight);
            ptLowerRight = new Point(ptUpperLeft.X + (cylinderRadius * 2), ptUpperLeft.Y + cylinderHeight);
            ptC = new Point(xOffset + cylinderRadius, ptUpperLeft.Y);

            ptUpperLeft.Y += yOffset;
            ptUpperRight.Y += yOffset;
            ptLowerLeft.Y += yOffset;
            ptLowerRight.Y += yOffset;
            ptC.Y += yOffset;

            Path pth = new Path();

            //Draw cylinder body.
            LineSegment ln = new LineSegment(ptLowerLeft, true);
            ArcSegment arc = new ArcSegment(ptLowerRight, new Size(cylinderRadius, ellipseHeight), 0, false, System.Windows.Media.SweepDirection.Counterclockwise, true);

            PathFigure pf = new PathFigure();
            pf.StartPoint = ptUpperLeft;
            //Add left side of cylinder.
            pf.Segments.Add(ln);
            //Add bottom arc of cylinder.
            pf.Segments.Add(arc);

            ln = new LineSegment(ptUpperRight, true);
            //Add right side of cylinder.
            pf.Segments.Add(ln);

            PathGeometry pg = new PathGeometry();
            pg.Figures.Add(pf);

            pth.Stroke = new SolidColorBrush(gridLineColor);
            pth.StrokeThickness = 2;
            pth.Fill = new SolidColorBrush(fillingColor);
            pth.Data = pg;
            cnv.Children.Add(pth);

            //Add top ellipse.
            pth = new Path();
            pth.Stroke = new SolidColorBrush(gridLineColor);
            pth.StrokeThickness = 2;
            pth.Fill = new SolidColorBrush(fillingColor);
            pg = new PathGeometry();
            pg.AddGeometry(new EllipseGeometry(ptC, cylinderRadius, ellipseHeight));
            pth.Data = pg;
            cnv.Children.Add(pth);

            #region Gridlines
            if (drawGridLines) { //If drawing grid lines within the Cylinder's body is enabled
                //Add gridlines on cylinder body.
                double i;
                double HorizontalPostion;
                double TopOfLine;
                double BottomOfLine;

                pth = new Path();
                pth.Stroke = new SolidColorBrush(gridLineColor);
                pth.StrokeThickness = 1;
                pg = new PathGeometry();

                for (i = 0; i <= cylinderDivisions / 2; i++)
                {
                    HorizontalPostion = Convert.ToInt32(cylinderRadius - (Math.Cos(Math.PI * 2 / cylinderDivisions * i) * cylinderRadius) + ptUpperLeft.X);
                    TopOfLine = Convert.ToInt32(Math.Sin(Math.PI * 2 / cylinderDivisions * i) * ellipseHeight + ptUpperLeft.Y);
                    BottomOfLine = TopOfLine + cylinderHeight;

                    pg.AddGeometry(new LineGeometry(new Point(HorizontalPostion, TopOfLine), new Point(HorizontalPostion, BottomOfLine)));

                }
                pth.Data = pg;
                cnv.Children.Add(pth);

                //Add radial gridlines on top of cylinder.
                pth = new Path();
                pth.Stroke = new SolidColorBrush(gridLineColor);
                pth.StrokeThickness = 1;
                pg = new PathGeometry();

                for (i = 0; i <= cylinderDivisions; i++)
                {
                    HorizontalPostion = Convert.ToInt32(cylinderRadius - (Math.Cos(Math.PI * 2 / cylinderDivisions * i) * cylinderRadius) + ptUpperLeft.X);
                    TopOfLine = Convert.ToInt32(Math.Sin(Math.PI * 2 / cylinderDivisions * i) * ellipseHeight + ptUpperLeft.Y);

                    pg.AddGeometry(new LineGeometry(new Point(HorizontalPostion, TopOfLine), ptC));

                }
                pth.Data = pg;
                cnv.Children.Add(pth);

                //Add circumferential gridlines on top of cylinder.
                pth = new Path();
                pth.Stroke = new SolidColorBrush(gridLineColor);
                pth.StrokeThickness = 1;
                pg = new PathGeometry();

                double circDivisions = 4;
                double multiplier;

                for (i = 1; i <= circDivisions - 1; i++)
                {
                    multiplier = i / Convert.ToDouble(circDivisions);

                    pg.AddGeometry(new EllipseGeometry(ptC, cylinderRadius * multiplier, ellipseHeight * multiplier));

                }
                pth.Data = pg;
                cnv.Children.Add(pth);

                double positionAdder;
                Point ptStart;
                Point ptEnd;

                //Add circumferential gridlines on body of cylinder.
                pth = new Path();
                pth.Stroke = new SolidColorBrush(gridLineColor);
                pth.StrokeThickness = 1;
                pg = new PathGeometry();

                for (i = 1; i <= cylinderDivisions / 2; i++)
                {
                    positionAdder = i * (cylinderHeight / (cylinderDivisions / 2));
                    ptStart = new Point(ptUpperLeft.X, ptUpperLeft.Y + positionAdder);
                    ptEnd = new Point(ptUpperRight.X, ptUpperRight.Y + positionAdder);

                    arc = new ArcSegment(ptEnd, new Size(cylinderRadius, ellipseHeight), 0, false, System.Windows.Media.SweepDirection.Counterclockwise, true);

                    pf = new PathFigure();
                    pf.StartPoint = ptStart;
                    //Add arc.
                    pf.Segments.Add(arc);

                    pg.Figures.Add(pf);

                }

                pth.Data = pg;
                cnv.Children.Add(pth);
            }
            #endregion
        }



        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }
    }
}
