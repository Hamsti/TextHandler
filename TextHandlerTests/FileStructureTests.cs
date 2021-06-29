using NUnit.Framework;
using TextHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace TextHandler.Tests
{
    [TestFixture]
    public class FileStructureTests
    {
        [Test]
        public void AnalazyLineFullTest()
        {
            FileStructure fileStructure = new FileStructure(Directory.GetCurrentDirectory() + @"\TextHandler\bin\Debug\test.txt", 0);
            fileStructure.AnalazyLine("for example, 24 hel-met not he-lmet; dis-abled not 666 disa-bled bla-bla-bla-blaaaaa-s");
            Dictionary<int, int> digits = new Dictionary<int, int>()
            {
                [2] = 1,
                [4] = 1,
                [6] = 3,
            };

            Dictionary<int, int> numbers = new Dictionary<int, int>()
            {
                [24] = 1,
                [666] = 1,
            };

            Dictionary<char, int> letters = new Dictionary<char, int>()
            {
                ['f'] = 1,
                ['o'] = 3,
                ['r'] = 1,
                ['e'] = 8,
                ['x'] = 1,
                ['a'] = 11,
                ['m'] = 3,
                ['p'] = 1,
                ['l'] = 9,
                ['h'] = 2,
                ['t'] = 4,
                ['n'] = 2,
                ['d'] = 4,
                ['i'] = 2,
                ['s'] = 3,
                ['b'] = 6,
            };

            Dictionary<char, int> punctuation = new Dictionary<char, int>()
            {
                [' '] = 10,
                [','] = 1,
                ['-'] = 8,
                [';'] = 1,
            };

            Dictionary<string, int> words = new Dictionary<string, int>()
            {
                ["for"] = 1,
                ["example"] = 1,
                ["hel"] = 1,
                ["met"] = 1,
                ["not"] = 2,
                ["he"] = 1,
                ["lmet"] = 1,
                ["dis"] = 1,
                ["abled"] = 1,
                ["disa"] = 1,
                ["bled"] = 1,
                ["bla"] = 3,
                ["blaaaaa"] = 1,
                ["s"] = 1,
            };

            Dictionary<string, int> wordsWithHyphen = new Dictionary<string, int>()
            {
                ["hel-met"] = 1,
                ["he-lmet"] = 1,
                ["dis-abled"] = 1,
                ["disa-bled"] = 1,
                ["bla-bla-bla-blaaaaa-s"] = 1,
            };

            Assert.AreEqual(1, fileStructure.LinesCount, nameof(fileStructure.LinesCount));
            Assert.AreEqual(5, fileStructure.DigitsCount, nameof(fileStructure.DigitsCount));
            Assert.AreEqual(2, fileStructure.NumbersCount, nameof(fileStructure.NumbersCount));
            Assert.AreEqual(61, fileStructure.LettersCount, nameof(fileStructure.LettersCount));
            Assert.AreEqual(17, fileStructure.WordsCount, nameof(fileStructure.WordsCount));
            Assert.AreEqual(5, fileStructure.WordsWithHyphenCount, nameof(fileStructure.WordsWithHyphenCount));
            Assert.AreEqual(20, fileStructure.PunctuationCount, nameof(fileStructure.PunctuationCount));
            Assert.AreEqual(12, fileStructure.UniqueWordsCount, nameof(fileStructure.UniqueWordsCount));
            Assert.AreEqual("bla", fileStructure.MostCommonWord, nameof(fileStructure.MostCommonWord));
            Assert.AreEqual("example", fileStructure.LongestWord, nameof(fileStructure.LongestWord));

            Assert.AreEqual(digits, fileStructure.Digits, nameof(fileStructure.Digits));
            Assert.AreEqual(numbers, fileStructure.Numbers, nameof(fileStructure.Numbers));
            Assert.AreEqual(letters, fileStructure.Letters, nameof(fileStructure.Letters));
            Assert.AreEqual(words, fileStructure.Words, nameof(fileStructure.Words));
            Assert.AreEqual(wordsWithHyphen, fileStructure.WordsWithHyphen, nameof(fileStructure.WordsWithHyphen));
            Assert.AreEqual(punctuation, fileStructure.punctuation, nameof(fileStructure.punctuation));
        }

        [TestCase("zero zero", 0, 2)]
        [TestCase("zero", 1, 1)]
        [TestCase("zero one", 2, 2)]
        [TestCase(" zero zero ", 0, 2)]
        [TestCase("zero ", 1, 1)]
        [TestCase(" zero one", 2, 2)]
        [TestCase(" zero, one", 2, 2)]
        [TestCase("zero,one ", 2, 2)]
        [TestCase(" zero, one ", 2, 2)]
        public void AnalazyUniqieTest(string text, int unique, int wordsCount)
        {
            FileStructure fileStructure = new FileStructure(Directory.GetCurrentDirectory() + @"\TextHandler\bin\Debug\test.txt", 0);
            fileStructure.AnalazyLine(text);

            Assert.AreEqual(unique, fileStructure.UniqueWordsCount, nameof(fileStructure.UniqueWordsCount));
            Assert.AreEqual(wordsCount, fileStructure.WordsCount, nameof(fileStructure.WordsCount));
        }
    }
}