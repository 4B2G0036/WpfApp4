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
namespace WpfApp4
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Color strokeColor = Colors.Black;
        Color fillColor = Colors.White;
        Brush strokeBursh = Brushes.Black;
        Brush fillBrush = Brushes.White;
        string shapeType = "line";
        int strokeThickness = 1;
        Point start, dest;
        string actionType = "draw";

        public MainWindow()
        {
            InitializeComponent();
            strokecolorpicker.SelectedColor = strokeColor;
            fillColorPicker.SelectedColor = fillColor;
        }
        private void myCanvas_MouseEnter(object sender, MouseEventArgs e)
        {
            myCanvas.Cursor = Cursors.Pen;
        }
        private void myCanvas_MouseLeftButtonDown(object sender,
       MouseButtonEventArgs e)
        {
            start = e.GetPosition(myCanvas);
            myCanvas.Cursor = Cursors.Cross;

            if (actionType == "draw")
            {
                switch (shapeType)
                {
                    case "line":
                        Line line = new Line
                        {
                            X1 = start.X,
                            Y1 = start.Y,
                            X2 = dest.X,
                            Y2 = dest.Y,
                            Stroke = Brushes.Gray,
                            StrokeThickness = 1
                        };
                        myCanvas.Children.Add(line);
                        break;
                    case "rectangle":
                        Rectangle rectangle = new Rectangle
                        {
                            Stroke = Brushes.Gray,
                            Fill = Brushes.LightGray
                        };
                        myCanvas.Children.Add(rectangle);
                        rectangle.SetValue(Canvas.LeftProperty, start.X);
                        rectangle.SetValue(Canvas.TopProperty, start.Y);
                        break;
                    case "ellipse":
                        Ellipse ellipse = new Ellipse
                        {
                            Stroke = Brushes.Gray,
                            Fill = Brushes.LightGray
                        };
                        myCanvas.Children.Add(ellipse);
                        ellipse.SetValue(Canvas.LeftProperty, start.X);
                        ellipse.SetValue(Canvas.TopProperty, start.Y);
                        break;
                    case "polyline":
                        Polyline polyline = new Polyline
                        {
                            Stroke = Brushes.Gray,
                            Fill = Brushes.LightGray
                        };
                        myCanvas.Children.Add(polyline);
                        break;
                }
            }
        }
        private void strokeColorPicker_SelectedColorChanged(object sender,
       RoutedPropertyChangedEventArgs<Color?> e)
        {
            strokeColor = strokecolorpicker.SelectedColor.Value;
        }

        private void ShapeButton_Click(object sender, RoutedEventArgs e)
        {
            var targetRadioButton = sender as RadioButton;
            shapeType = targetRadioButton.Tag.ToString();
            actionType = "draw";
        }

        private void EraseButton_Click(object sender, RoutedEventArgs e)
        {
            actionType = "erase";
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            actionType = "clear";
            myCanvas.Children.Clear();
        }

        private void fillColorPicker_SelectColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            fillColor = fillColorPicker.SelectedColor.Value;
        }

        private void myCanvas_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Brush strokeBrush = new SolidColorBrush(strokeColor);
            Brush fillBrush = new SolidColorBrush(fillColor);

            switch (shapeType)
            {
                case "line":
                    var line = myCanvas.Children.OfType<Line>().LastOrDefault();
                    line.Stroke = strokeBrush;
                    line.StrokeThickness = strokeThickness;
                    break;
                case "rectangle":
                    var rectangle = myCanvas.Children.OfType<Rectangle>().LastOrDefault();
                    rectangle.Stroke = strokeBrush;
                    rectangle.Fill = fillBrush;
                    rectangle.StrokeThickness = strokeThickness;
                    break;
                case "ellipse":
                    var ellipse = myCanvas.Children.OfType<Ellipse>().LastOrDefault();
                    ellipse.Stroke = strokeBrush;
                    ellipse.Fill = fillBrush;
                    ellipse.StrokeThickness = strokeThickness;
                    break;
                case "polyline":
                    var polyline = myCanvas.Children.OfType<Polyline>().LastOrDefault();
                    polyline.Stroke = strokeBrush;
                    polyline.Fill = fillBrush;
                    polyline.StrokeThickness = strokeThickness;
                    break;
            }
        }

        private void strokeThicknessSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            strokeThickness = (int)strokeThicknessSlider.Value;
        }

        

        private void myCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            dest = e.GetPosition(myCanvas);
            DisplayStatus();

            switch (actionType)
            {
                case "draw": //繪圖
                    if (e.LeftButton == MouseButtonState.Pressed)
                    {
                        Point origin;
                        origin.X = Math.Min(start.X, dest.X);
                        origin.Y = Math.Min(start.Y, dest.Y);
                        double width = Math.Abs(start.X - dest.X);
                        double height = Math.Abs(start.Y - dest.Y);

                        switch (shapeType)
                        {
                            case "line":
                                var line = myCanvas.Children.OfType<Line>().LastOrDefault();
                                line.X2 = dest.X;
                                line.Y2 = dest.Y;
                                break;
                            case "rectangle":
                                var rectangle = myCanvas.Children.OfType<Rectangle>().LastOrDefault();
                                rectangle.Width = width;
                                rectangle.Height = height;
                                rectangle.SetValue(Canvas.LeftProperty, origin.X);
                                rectangle.SetValue(Canvas.TopProperty, origin.Y);
                                break;
                            case "ellipse":
                                var ellipse = myCanvas.Children.OfType<Ellipse>().LastOrDefault();
                                ellipse.Width = width;
                                ellipse.Height = height;
                                ellipse.SetValue(Canvas.LeftProperty, origin.X);
                                ellipse.SetValue(Canvas.TopProperty, origin.Y);
                                break;
                            case "polyline":
                                var polyline = myCanvas.Children.OfType<Polyline>().LastOrDefault();
                                polyline.Points.Add(dest);
                                break;
                        }
                    }
                    break;
                case "erase": //擦除
                    var shape = e.OriginalSource as Shape;
                    myCanvas.Cursor = Cursors.Hand;
                    myCanvas.Children.Remove(shape);
                    if (myCanvas.Children.Count == 0)
                    {
                        myCanvas.Cursor = Cursors.Arrow;
                    }
                    break;
                case "clear": //清除
                    myCanvas.Children.Clear();
                    break;
            }


        }

        private void DisplayStatus()
        {
            statuspoint.Content = $"({Convert.ToInt32(start.X)}, {Convert.ToInt32(start.Y)}) - ({Convert.ToInt32(dest.X)}, {Convert.ToInt32(dest.Y)})";
        }

    }
}