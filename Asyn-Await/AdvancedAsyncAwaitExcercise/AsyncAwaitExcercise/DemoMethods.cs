using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncAwaitExcercise
{
    public static class DemoMethods
    {
        public static List<string> PrepData()
        {
            List<string> filePaths = new List<string>();

            filePaths.Add(@"..\..\..\LoremIpsum_Paragraph_1.txt");
            filePaths.Add(@"..\..\..\LoremIpsum_Paragraph_2.txt");

            return filePaths;
        }

        public static List<FileDataModel> RunReadSync()
        {
            List<string> filePathList = PrepData();
            List<FileDataModel> output = new List<FileDataModel>();

            foreach (string filePath in filePathList)
            {
                output.Add(ReadFile(filePath));
            }

            return output;
        }

        public static async Task<List<FileDataModel>> RunReadAsync()
        {
            List<string> filePathList = PrepData();
            List<FileDataModel> output = new List<FileDataModel>();

            foreach (string filePath in filePathList)
            {
                output.Add(await Task.Run(() => ReadFile(filePath)));
            }

            return output;
        }

        public static async Task<List<FileDataModel>> RunReadParallelAsync()
        {
            List<string> filePathList = PrepData();
            List<Task<FileDataModel>> taskList = new List<Task<FileDataModel>>();

            foreach (string filePath in filePathList)
            {
                taskList.Add(Task.Run(() => ReadFile(filePath)));
            }

            var results = await Task.WhenAll(taskList);

            return new List<FileDataModel>(results);
        }

        public static FileDataModel ReadFile(string filePath)
        {
            FileDataModel result = new FileDataModel();

            result.FileName = System.IO.Path.GetFileName(filePath);
            result.FileData = File.ReadAllText(filePath);

            return result;
        }

        public static async Task<FileDataModel> ReadFileAsync(string filePath)
        {
            FileDataModel result = new FileDataModel();

            result.FileName = System.IO.Path.GetFileName(filePath);
            StreamReader reader = new StreamReader(filePath);
            result.FileData = await reader.ReadToEndAsync();

            return result;
        }
    }
}
