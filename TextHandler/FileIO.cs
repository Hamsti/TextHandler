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
        public static long CountReadedBytesFile { get; private set; }

        public static FileStructure ReadFileData(string fileName)
        {
            FileStructure fileStructure;

            try
            {
                CheckFileExists(fileName);
                InterfaceHandler.ResetProgressBar();

                using (var fileStream = File.OpenRead(fileName))
                {
                    fileStructure = new FileStructure(fileStream.Length, fileName);
                    using (StreamReader reader = new StreamReader(fileStream, Encoding.UTF8, true, READER_BUFFER_SIZE))
                    {
                        string dataLine;
                        while ((dataLine = reader.ReadLine()) != null)
                        {
                            CountReadedBytesFile += FileStructure.AnalazyLine(fileStructure, dataLine);
                            InterfaceHandler.RefreshProgressBar(CountReadedBytesFile, fileStructure.fileSize);
                        }
                    }
                }

                CountReadedBytesFile = fileStructure.fileSize;
                InterfaceHandler.RefreshProgressBar(CountReadedBytesFile, fileStructure.fileSize);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }

            return fileStructure;
        }

        public static bool WriteFileData(FileStructure fileStructure)
        {
            try
            {
                if (fileStructure == null)
                {
                    throw new ArgumentNullException(nameof(fileStructure));
                }

                if (fileStructure.fileName is null)
                {
                    throw new ArgumentNullException(nameof(fileStructure.fileName));
                }

                if (string.IsNullOrWhiteSpace(fileStructure.fileName))
                {
                    throw new ArgumentException("It's empty or contains only spaces");
                }

                string saveFullPath = fileStructure.fileFullPath.Remove(fileStructure.fileFullPath.Length - Path.GetExtension(fileStructure.fileName).Length) + ".json";
                File.WriteAllText(saveFullPath, JsonConvert.SerializeObject(fileStructure));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }

            return true;
        }

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
