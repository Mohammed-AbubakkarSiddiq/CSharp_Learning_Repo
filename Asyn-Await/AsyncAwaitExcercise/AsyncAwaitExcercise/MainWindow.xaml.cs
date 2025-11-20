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

        public List<string> PrepData()
        {
            List<string> filePaths = new List<string>();
            resultsWindow.Text = "";

            filePaths.Add(@"C:\Users\siddi\Documents\UpSkill\C#\CSharp_Learning_Repo\Asyn-Await\AsyncAwaitExcercise\AsyncAwaitExcercise\LoremIpsum_Paragraph_1.txt");
            filePaths.Add(@"C:\Users\siddi\Documents\UpSkill\C#\CSharp_Learning_Repo\Asyn-Await\AsyncAwaitExcercise\AsyncAwaitExcercise\LoremIpsum_Paragraph_2.txt");

            return filePaths;
        }

        private FileDataModel ReadFile(string filePath)
        {
            FileDataModel result = new FileDataModel();

            result.FileName = System.IO.Path.GetFileName(filePath);
            result.FileData = File.ReadAllText(filePath);

            return result;
        }

        private async Task<FileDataModel> ReadFileAsync(string filePath)
        {
            FileDataModel result = new FileDataModel();

            result.FileName = System.IO.Path.GetFileName(filePath);
            StreamReader reader = new StreamReader(filePath);
            result.FileData = await reader.ReadToEndAsync();

            return result;
        }

        private void ReportResult(FileDataModel result)
        {
            resultsWindow.Text += $"File Name: {result.FileName} File Data Length: {result.FileData.Length} {Environment.NewLine}";
        }

        private void RunReadSync()
        {
            List<string> filePathList = PrepData();
            
            foreach(string filePath in filePathList)
            {
                var output = ReadFile(filePath);
                ReportResult(output);
            }
        }

        private async Task RunReadAsync()
        {
            List<string> filePathList = PrepData();

            foreach (string filePath in filePathList)
            {
                var output = await Task.Run(() => ReadFile(filePath));
                ReportResult(output);
            }
        }

        private async Task RunReadParallelAsync()
        {
            List<string> filePathList = PrepData();
            List<Task<FileDataModel>> taskList = new List<Task<FileDataModel>>();
            
            foreach (string filePath in filePathList)
            {
                taskList.Add(Task.Run(() => ReadFile(filePath)));
            }

            var results = await Task.WhenAll(taskList);

            foreach (var result in results)
            {
                ReportResult(result);
            }
        }
    }
}
