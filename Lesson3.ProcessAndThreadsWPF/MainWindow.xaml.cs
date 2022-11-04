using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
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

namespace Lesson3.ProcessAndThreadsWPF
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Task.Run(() =>
            {
                var total = CalculateTotal();
                Dispatcher.Invoke(() => { textBoxTotal.Text = total.ToString(); });
            });
        }

        private static long CalculateTotal()
        {
            long total = 0;
            var pathIncome = @"C:\Users\rodio\Downloads\income.txt";
            foreach (string line in File.ReadAllLines(pathIncome))
            {
                total += long.Parse(line);
            }

            //File.ReadAllLines(pathIncome).Where(it => it.Length > 10);

            var pathOutcome = @"C:\Users\rodio\Downloads\outcome.txt";
            foreach (string line in File.ReadAllLines(pathOutcome))
            {
                total -= long.Parse(line);
            }

            return total;
        }
    }
}