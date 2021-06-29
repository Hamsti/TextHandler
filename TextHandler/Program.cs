using System;
using System.Reflection;

namespace TextHandler
{
    public class Program
    {
        /// <summary>
        /// The main function of starting the program
        /// </summary>
        /// <param name="args">Args for help, name or full path of text file</param>
        static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                if (args.Length == 1 && (args[0] == "--help" || args[0] == "-h"))
                {
                    Console.WriteLine(Assembly.GetExecutingAssembly().GetName().Name + ".exe - text structure analyzer" +
                                      "\n-h, --help - get help" +
                                      "\n[args] - name of text file(-s) that doesn't contain space" +
                                      "\n[args] - name of text file(-s) that contain spaces and take each name into quotes)");
                    return;
                }

                foreach (string arg in args)
                {
                    InterfaceHandler.ExecuteAnalyseTextFile(arg);
                }
            }
            else
            {
                InterfaceHandler.DisplayMenu();
            }
        }
    }
}
