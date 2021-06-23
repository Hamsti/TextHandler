using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace TextHandler
{
    public class Program
    {
        /// <summary>
        /// Прочитать текстовый файл (который может быть больших размеров), посчитать в нем:
        /// - количество букв всего; количества каждой буквы (A & a - different)
        /// - количество цифр; количество чисел
        /// - количество слов всего; количества каждого слова
        /// - количество строк
        /// - количество слов с дефисом
        /// - количество знаков препинания(.,:;'"-) (spaces count too)
        /// - самое длинное слово
        /// Сохранить вычисления в файле с именем: <имя исходного файла>.json в формате JSON.Причем результаты должны быть отсортированы по количеству в убывающем порядке.
        /// - Результаты в выходном файле должны быть определенным образом отсортированы
        ///
        /// Основные недостатки (и несоотвнетствия ТехЗаданию):
        /// - Обрабатывается всегда только файл с именем "test.txt". Нужно иметь возможность подать на вход любой файл. (Передать как входной аргумент командной строки)
        /// - Выходной файл всегда "serialized.json"
        /// - Результаты в выходном файле должны быть определенным образом отсортированы
        /// - Нет никакой защиты от ошибок
        /// - Текст обрабатывается несколько раз.Оптимальное решение - все подсчеты делаются за один проход.
        /// - Сначала весь текст считывается в память. В случае если будет файл большего размера чем объем памяти - обработки не произойдет, приложение завершится аварийно.
        /// Пример результата:
        /// {
        ///  "filename": "test.txt",
        ///  "fileSize": 33215,
        ///  "lettersCount": "5890",
        ///  "letters": {
        ///    "a": "2341",
        ///    "b": "1567",
        ///    "c": "1234"...
        ///  },
        ///  "wordsCount": "748",
        ///  "words": {
        ///    "hi": "45",
        ///    "bye": "37",
        ///    "my": "15",
        ///    ...
        ///  },
        ///  "linesCount": "123",
        ///  "digitsCount": "234",
        ///  "numbersCount": "34",
        ///  "longestWord": "independently",
        ///  "wordsWithHyphen": "15",
        ///  "punctuation": "65",
        ///  ...
        /// </summary>
        /// <param name="args">Args for help, name or full path of text file</param>
        static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                if (args.Length == 1 && (args[0] == "--help" || args[0] == "-h"))
                {
                    Console.WriteLine(System.Reflection.Assembly.GetExecutingAssembly().GetName().Name + ".exe - text structure analyzer" +
                                      "\n-h, --help - get help" +
                                      "\n[args] - name of text file(-s) that doesn't contain space" +
                                      "\n[args] - name of text file(-s) that contain spaces and take each name into quotes)");
                    return;
                }

                foreach (string arg in args)
                {
                    Console.WriteLine(FileIO.ReadFileData(arg)?.fileName ?? "Exception");
                }
            }
            else
            {
                InterfaceHandler.InteractionInterface();
            }

            Console.Write("\nPress any button to exit...");
            Console.ReadKey();
        }
    }
}
