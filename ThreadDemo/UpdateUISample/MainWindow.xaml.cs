using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace UpdateUISample
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

        private void OnCalculate(object sender, RoutedEventArgs e)
        {
            int x1 = int.Parse(text1.Text);
            int x2 = int.Parse(text2.Text);
            var calc = new Calculator();
            Task.Run(() =>
            {
                int result = calc.Add(x1, x2);

                textResult.Dispatcher.Invoke(() =>
                {
                    textResult.Text = result.ToString();
                });

            });


        }

        private async void OnCalculate2(object sender, RoutedEventArgs e)
        {
            int x1 = int.Parse(text1.Text);
            int x2 = int.Parse(text2.Text);
            int result = await AddAsync(x1, x2); // .ConfigureAwait(true);
            textResult.Text = result.ToString();
        }

        private Task<int> AddAsync(int x, int y)
        {
            int MyAdd()
            {
                var calc = new Calculator();
                int result = calc.Add(x, y);
                return result;
            }


            return Task<int>.Run<int>(MyAdd);
        }


    }
}
