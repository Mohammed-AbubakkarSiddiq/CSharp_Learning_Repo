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
            RunReadSync();
            stopwatch.Stop();
            resultsWindow.Text += $"Total Ellapsed Time: {stopwatch.ElapsedMilliseconds}";
        }

        private async void executeAsync_Click(object sender, RoutedEventArgs e)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            await RunReadAsync();
            stopwatch.Stop();
            resultsWindow.Text += $"Total Ellapsed Time: {stopwatch.ElapsedMilliseconds}";
        }

        private async void executeParallelAsync_Click(object sender, RoutedEventArgs e)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            await RunReadParallelAsync();
            stopwatch.Stop();
            resultsWindow.Text += $"Total Ellapsed Time: {stopwatch.ElapsedMilliseconds}";
        }

        private void ReportResult(FileDataModel result)
        {
            resultsWindow.Text += $"File Name: {result.FileName} File Data Length: {result.FileData.Length} {Environment.NewLine}";
        }
    }
}
