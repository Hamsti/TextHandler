using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace TextHandler
{
    [Serializable]
    public class FileStructure
    {
        public int linesCount;
        public string longestWord;
        public readonly long fileSize;
        public readonly string fileName;
        public readonly string fileFullPath;
        public readonly Dictionary<byte, int> digits;
        public readonly Dictionary<int, int> numbers;
        public readonly Dictionary<char, int> letters;
        public readonly Dictionary<char, int> punctuation;
        public readonly Dictionary<string, int> words;
        public readonly Dictionary<string, int> wordsWithHyphen;
        public int DigitsCount => digits.Sum(s => s.Value); //цифры
        public int NumbersCount => numbers.Sum(s => s.Value); //числа
        public int LettersCount => letters.Sum(s => s.Value);
        public int PunctuationCount => punctuation.Sum(s => s.Value);
        public int WordsCount => words.Sum(s => s.Value);
        public int WordsWithHyphenCount => wordsWithHyphen.Sum(s => s.Value);

        public FileStructure(string fileName, long fileSize)
        {
            this.fileSize = fileSize;
            fileFullPath = Path.GetFullPath(fileName) ?? throw new ArgumentNullException(nameof(fileName));
            this.fileName = Path.GetFileName(fileFullPath) ?? throw new ArgumentNullException(nameof(fileFullPath));
            digits = new Dictionary<byte, int>();
            numbers = new Dictionary<int, int>();
            letters = new Dictionary<char, int>();
            punctuation = new Dictionary<char, int>();
            words = new Dictionary<string, int>();
            wordsWithHyphen = new Dictionary<string, int>();
        }

        public static long AnalazyLine(FileStructure fileStructure, string dataLine)
        {
            return Encoding.UTF8.GetByteCount(dataLine);
            
            throw new NotImplementedException();
        }
    }
}
