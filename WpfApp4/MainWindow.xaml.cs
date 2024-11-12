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
        Brush strokeBursh = Brushes.Black;
        Point start, dest;
        public MainWindow()
        {
            InitializeComponent();
            strokecolorpicker.SelectedColor = strokeColor;
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
        }
        private void myCanvas_MouseUp(object sender,
       MouseButtonEventArgs e)
        {
            var brush = new SolidColorBrush(strokeColor);
            Line line = new Line
            {
                X1 = start.X,
                Y1 = start.Y,
                X2 = dest.X,
                Y2 = dest.Y,
                Stroke = brush,
                StrokeThickness = 2
            };
            myCanvas.Children.Add(line);
        }
        private void strokeColorPicker_SelectedColorChanged(object sender,
       RoutedPropertyChangedEventArgs<Color?> e)
        {
            strokeColor = strokecolorpicker.SelectedColor.Value;
        }

        private void ShapeButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            string shape = button.Tag.ToString();
            MessageBox.Show(shape);
        }

        private void eraserButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void myCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            dest = e.GetPosition(myCanvas);
            statuspoint.Content = $"({Convert.ToInt32(start.X)}, {Convert.ToInt32(start.Y)}) - ({Convert.ToInt32(dest.X)}, {Convert.ToInt32(dest.Y)})";
        }

    }
}