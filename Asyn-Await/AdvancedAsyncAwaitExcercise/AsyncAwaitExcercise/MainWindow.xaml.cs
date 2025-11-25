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
using System.Threading;

namespace AsyncAwaitExcercise
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        CancellationTokenSource cts = new CancellationTokenSource();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void executeSync_Click(object sender, RoutedEventArgs e)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            var result = DemoMethods.RunReadParallelSync();
            ReportResult(result);
            stopwatch.Stop();
            resultsWindow.Text += $"Total Ellapsed Time: {stopwatch.ElapsedMilliseconds}";
        }

        private async void executeAsync_Click(object sender, RoutedEventArgs e)
        {
            Progress<ProgressModel> progress = new Progress<ProgressModel>();
            progress.ProgressChanged += ReportProgress;
            cts = new CancellationTokenSource();
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            try
            {
                var result = await DemoMethods.RunReadAsync(progress, cts.Token);
                ReportResult(result);
            }
            catch (OperationCanceledException)
            {
                resultsWindow.Text += $"{Environment.NewLine}Async operation was cancelled.{Environment.NewLine}";
            }
            stopwatch.Stop();
            resultsWindow.Text += $"Total Ellapsed Time: {stopwatch.ElapsedMilliseconds}";
        }

        private async void executeParallelAsync_Click(object sender, RoutedEventArgs e)
        {
            Progress<ProgressModel> progress = new Progress<ProgressModel>();
            progress.ProgressChanged += ReportProgress;
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            var result = await DemoMethods.RunReadParallelAsyncV2(progress);
            ReportResult(result);
            stopwatch.Stop();
            resultsWindow.Text += $"Total Ellapsed Time: {stopwatch.ElapsedMilliseconds}";
        }

        private void ReportProgress(object sender, ProgressModel e)
        {
            dashboardProgress.Value = e.PercentageRead;
            ReportResult(e.ReadCompletedFiles);
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
            cts.Cancel();
        }
    }
}
