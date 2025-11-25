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
using System.IO;
using System.Diagnostics;

namespace AsyncAwaitExcercise
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

        private void executeSync_Click(object sender, RoutedEventArgs e)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            var result = DemoMethods.RunReadSync();
            ReportResult(result);
            stopwatch.Stop();
            resultsWindow.Text += $"Total Ellapsed Time: {stopwatch.ElapsedMilliseconds}";
        }

        private async void executeAsync_Click(object sender, RoutedEventArgs e)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            var result = await DemoMethods.RunReadAsync();
            ReportResult(result);
            stopwatch.Stop();
            resultsWindow.Text += $"Total Ellapsed Time: {stopwatch.ElapsedMilliseconds}";
        }

        private async void executeParallelAsync_Click(object sender, RoutedEventArgs e)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            var result = await DemoMethods.RunReadParallelAsync();
            ReportResult(result);
            stopwatch.Stop();
            resultsWindow.Text += $"Total Ellapsed Time: {stopwatch.ElapsedMilliseconds}";
        }

        private void ReportResult(List<FileDataModel> results)
        {
            resultsWindow.Text = "";
            foreach (FileDataModel result in results)
            {
                resultsWindow.Text += $"File Name: {result.FileName} File Data Length: {result.FileData.Length} {Environment.NewLine}";
            }
        }

        private void cancelOperation_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
