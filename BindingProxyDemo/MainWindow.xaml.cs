using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BindingProxyDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static readonly Random rnd = new Random();

        public MainWindow()
        {
            InitLogging();
            Log.Debug("Start App");

            InitializeComponent();
        }

        private static void InitLogging() =>
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Debug()
                .CreateLogger();

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Dispatcher is optional...
            Dispatcher.BeginInvoke(new Action(() =>
            {
                Color color = Color.FromRgb((byte)rnd.Next(255), (byte)rnd.Next(255), (byte)rnd.Next(255));
                SolidColorBrush brush = new SolidColorBrush(color);

                Log.Debug("Changing color brush: {newColor}", color);
                // 1. Try: set resource via global instance
                //Application.Current.Resources["myBrush"] = brush;

                // 2. Try: set resource in actual resource dictionary
                Application.Current.Resources.MergedDictionaries[0]["myBrush"] = brush;

                // 3. Try: set color on brush
                //(Application.Current.Resources["myBrush"] as SolidColorBrush).Color = color;
                // throws exception!
            }));

        }
    }
}
