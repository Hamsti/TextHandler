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
        public readonly long fileSize;
        public readonly string fileName;
        public readonly string fileFullPath;
        public readonly Dictionary<char, int> punctuation;
        public int LinesCount { get; private set; }
        public Dictionary<int, int> Digits { get; private set; }
        public Dictionary<int, int> Numbers { get; private set; }
        public Dictionary<char, int> Letters { get; private set; }
        public Dictionary<string, int> Words { get; private set; }
        public Dictionary<string, int> WordsWithHyphen { get; private set; }
        public int DigitsCount => Digits.Sum(s => s.Value);
        public int NumbersCount => Numbers.Sum(s => s.Value);
        public int LettersCount => Letters.Sum(s => s.Value);
        public int PunctuationCount => punctuation.Sum(s => s.Value);
        public int WordsCount => Words.Sum(s => s.Value);
        public int WordsWithHyphenCount => WordsWithHyphen.Sum(s => s.Value);
        public int UniqueWordsCount => Words.Aggregate(0, (seed, word) => word.Value == 1 ? ++seed : seed);
        public string MostCommonWord => Words.Aggregate((x, y) => x.Value > y.Value ? x : y).Key;
        public string LongestWord => Words.Aggregate(string.Empty, (seed, word) => word.Key.Length > seed.Length ? word.Key : seed);

        public FileStructure(string fileName, long fileSize)
        {
            this.fileSize = fileSize < 0 ? throw new ArgumentOutOfRangeException(nameof(fileSize)) : fileSize;
            fileFullPath = Path.GetFullPath(fileName) ?? throw new ArgumentNullException(nameof(fileName));
            this.fileName = Path.GetFileName(fileFullPath) ?? throw new ArgumentNullException(nameof(fileFullPath));
            Digits = new Dictionary<int, int>();
            Numbers = new Dictionary<int, int>();
            Letters = new Dictionary<char, int>();
            punctuation = new Dictionary<char, int>();
            Words = new Dictionary<string, int>();
            WordsWithHyphen = new Dictionary<string, int>();
        }

        public void AnalazyLine(string dataLine)
        {
            if (dataLine is null)
            {
                throw new ArgumentNullException(nameof(dataLine));
            }

            LinesCount++;
            int wordLength = default;
            int wordLengthWithHypen = default;
            for (int index = default; index < dataLine.Length; index++)
            {
                char currentChar = dataLine[index];
                if (char.IsDigit(currentChar))
                {
                    IncreaseValue(Digits, (int)char.GetNumericValue(currentChar));
                    wordLength++;
                }
                else if (char.IsLetter(currentChar))
                {
                    IncreaseValue(Letters, currentChar);
                    wordLength++;
                }
                else
                {
                    IncreaseValue(punctuation, currentChar);
                    if (wordLength != 0)
                    {
                        AddWordNumber(dataLine.Substring(index - wordLength, wordLength));
                        wordLengthWithHypen = AddWordWithHypen(dataLine, index + 1, wordLength, wordLengthWithHypen);
                    }

                    wordLength = default;
                }
            }

            if (wordLength != 0)
            {
                AddWordNumber(dataLine.Substring(dataLine.Length - wordLength, wordLength));
                AddWordWithHypen(dataLine, dataLine.Length + 1, wordLength, wordLengthWithHypen);
            }
        }

        public FileStructure SortDictionaries()
        {
            Console.WriteLine("\nSorting results...");
            Digits = SortDictionary(Digits);
            Numbers = SortDictionary(Numbers);
            Letters = SortDictionary(Letters);
            Words = SortDictionary(Words);
            WordsWithHyphen = SortDictionary(WordsWithHyphen);
            return this;
        }

        private void AddWordNumber(string numberOrWord)
        {
            if (int.TryParse(numberOrWord, out int result))
            {
                IncreaseValue(Numbers, result);
            }
            else
            {
                IncreaseValue(Words, numberOrWord);
            }
        }

        private int AddWordWithHypen(string dataLine, int index, int wordLength, int wordLengthWithHypen)
        {
            if (index < dataLine.Length && char.IsLetterOrDigit(dataLine[index]) && dataLine[index - 1] == '-')
            {
                return wordLengthWithHypen + wordLength + 1;
            }

            if (wordLengthWithHypen > 0)
            {
                wordLengthWithHypen += wordLength;
                string result = dataLine.Substring(index - wordLengthWithHypen == 0 ? 0 : index - wordLengthWithHypen - 1,
                                                   wordLengthWithHypen);
                IncreaseValue(WordsWithHyphen, result);
            }

            return 0;
        }

        private void IncreaseValue<T>(Dictionary<T, int> dict, T key) => dict[key] = dict.TryGetValue(key, out int result) ? ++result : 1;
        private Dictionary<string, int> SortDictionary(Dictionary<string, int> dict) => dict.OrderByDescending(o => o.Value).ThenBy(o => o.Key).ToDictionary(k => k.Key, v => v.Value);
        private Dictionary<T, int> SortDictionary<T>(Dictionary<T, int> dict) where T : struct => dict.OrderByDescending(o => o.Value).ThenBy(o => o.Key).ToDictionary(k => k.Key, v => v.Value);
    }
}