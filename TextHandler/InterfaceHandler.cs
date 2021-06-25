using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace TextHandler
{
    public static class InterfaceHandler
    {
        private static string pathOfDirectory = null;

        public static void DisplayMenu()
        {
            try
            {
                StringBuilder message = new StringBuilder();
                var menuItems = GetDirectoryFiles();
                foreach (var menuValue in menuItems)
                {
                    message.AppendLine(menuValue.Key + ". " + menuValue.Value);
                }

                Console.Write(message + "select a menu item: ");
                var fileStructure = SelectMenuItem(menuItems);
                if (fileStructure != null)
                {
                    WriteDisplayTextAnalysis(fileStructure);
                    DisplayMenu();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + '\n');
                DisplayMenu();
            }
        }

        public static void ExecuteAnalyseTextFile(string arg)
        {
            try
            {
                FileStructure fileStructure = FileIO.ReadFileData(arg);
                if (fileStructure != null)
                {
                    WriteDisplayTextAnalysis(fileStructure);
                    FileIO.WriteFileData(fileStructure);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + "\nPress any button to exit...");
                Console.ReadKey();
            }
        }

        public static void WriteDisplayTextAnalysis(FileStructure fileStructure)
        {
            if (fileStructure is null)
            {
                throw new ArgumentNullException(nameof(fileStructure), "An exception was thrown when displaying text analysis\n");
            }

            Console.WriteLine('\n' + FileIO.WriteFileData(fileStructure) + '\n');
        }

        private static FileStructure SelectMenuItem(Dictionary<int, string> menuItems)
        {
            if (int.TryParse(Console.ReadLine(), out int indexMenuItem) && indexMenuItem >= 0 && indexMenuItem < menuItems.Count)
            {
                if (indexMenuItem == menuItems.Count - 2)
                {
                    ChangeFolder();
                    DisplayMenu();
                }
                else if (indexMenuItem == menuItems.Count - 1)
                {
                    Console.Write("Input path to file: ");
                    return FileIO.ReadFileData(Console.ReadLine());
                }
                else if (indexMenuItem != default)
                {
                    return FileIO.ReadFileData((pathOfDirectory ?? string.Empty) + menuItems[indexMenuItem]);
                }

                return null;
            }
            
            throw new ArgumentException("Unknown menu item", nameof(indexMenuItem));
        }

        private static void ChangeFolder()
        {
            Console.Write("Input path to folder: ");
            pathOfDirectory = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(pathOfDirectory) || !Directory.Exists(pathOfDirectory))
            {
                Console.WriteLine("Unknown path to directory...\nchange on directory by default");
                pathOfDirectory = null;
            }
            else
            {
                pathOfDirectory += pathOfDirectory.EndsWith("\\") ? string.Empty : "\\";
            }

            Console.WriteLine();
        }

        private static Dictionary<int, string> GetDirectoryFiles()
        {
            int index = default;
            var directoryFiles = FileIO.GetTextFiles(pathOfDirectory);
            Dictionary<int, string> keyMenuValue = new Dictionary<int, string>();

            foreach (var file in directoryFiles)
            {
                keyMenuValue.Add(++index, file.Name);
            }

            keyMenuValue.Add(++index, "input custom path to folder");
            keyMenuValue.Add(++index, "input custom path to file");
            keyMenuValue.Add(default, "select to exit...");

            return keyMenuValue;
        }
    }
}
