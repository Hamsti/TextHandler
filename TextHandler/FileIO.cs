using Newtonsoft.Json;
using System;
using System.IO;
using System.Text;

namespace TextHandler
{
    public static class FileIO
    {
        /// <summary>
        /// Buffer size that is better for reading a huge text file if rely on tests
        /// </summary>
        private const int READER_BUFFER_SIZE = 4096;

        public static FileStructure ReadFileData(string fileName)
        {
            FileStructure fileStructure;
            ProgressBar progressBar;

            CheckFileExists(fileName);
            using (var fileStream = File.OpenRead(fileName))
            {
                fileStructure = new FileStructure(fileName, fileStream.Length);
                progressBar = new ProgressBar(fileStructure.fileSize, 1);

                using (StreamReader reader = new StreamReader(fileStream, Encoding.UTF8, true, READER_BUFFER_SIZE))
                {
                    string dataLine;
                    while ((dataLine = reader.ReadLine()) != null)
                    {
                        progressBar.Update(FileStructure.AnalazyLine(fileStructure, dataLine));
                    }
                }
            }

            progressBar.Update(fileStructure.fileSize);
            return fileStructure;
        }

        public static string WriteFileData(FileStructure fileStructure)
        {
            if (fileStructure == null)
            {
                throw new ArgumentNullException(nameof(fileStructure));
            }

            if (string.IsNullOrWhiteSpace(fileStructure.fileName))
            {
                throw new ArgumentException(nameof(fileStructure.fileName) + " empty or contains only spaces");
            }

            if (string.IsNullOrWhiteSpace(fileStructure.fileFullPath))
            {
                throw new ArgumentException(nameof(fileStructure.fileFullPath) + " empty or contains only spaces");
            }

            string saveFullPath = fileStructure.fileFullPath.Remove(fileStructure.fileFullPath.Length - Path.GetExtension(fileStructure.fileName).Length) + ".json";
            string serializeObject = JsonConvert.SerializeObject(fileStructure,
                                                          Formatting.Indented,
                                                          new JsonSerializerSettings()
                                                          {
                                                              ContractResolver = new OrderedContractResolver()
                                                          });
            File.WriteAllText(saveFullPath, serializeObject);
            return serializeObject;
        }

        public static FileInfo[] GetTextFiles(string pathOfDirectory = null) => new DirectoryInfo(pathOfDirectory ?? Directory.GetCurrentDirectory()).GetFiles("*.txt");

        private static void CheckFileExists(string fileName)
        {
            if (!File.Exists(fileName))
            {
                throw new ArgumentException("File doesn't exist by path: " + fileName, nameof(fileName));
            }

            if (!Path.GetExtension(fileName).Equals(".txt"))
            {
                throw new ArgumentException("It isn't a text file by path: " + fileName, nameof(fileName));
            }
        }
    }
}
