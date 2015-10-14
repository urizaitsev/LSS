using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ChartPainter3D;

namespace Chart3D_GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public void ClickButton(object sender, RoutedEventArgs e)
        {
            ChartDataContainer container = new ChartDataContainer();
            container.Data = new List<double>() {20, 33, 115 , 85, 65 };
            container.DataCaptions = new List<string>() {"1","2", "3", "4", "5"};
            container.MaxSize = new Point(700, 300);
            container.Offset = new Point(10, 150);
            container.ChartElementColor = Colors.SlateBlue;
            CylinderChartPainter.DrawChart(container, this.Canvas1);
        }
    }
}
