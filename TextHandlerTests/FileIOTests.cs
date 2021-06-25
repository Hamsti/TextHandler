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
    [TestFixture()]
    public class FileIOTests
    {
        [TestCase("1datas.txt")]
        [TestCase("Tolstoy Leo. War and Peace.txt")]
        public void ReadFileDataTest(string fileName)
        {
            Assert.IsNotNull(FileIO.ReadFileData(Directory.GetCurrentDirectory() + @"\TextHandler\bin\Debug\" + fileName));
        }

        [Test()]
        public void WriteFileDataTest()
        {
            Assert.IsNotNull(FileIO.WriteFileData(FileIO.ReadFileData(Directory.GetCurrentDirectory() + @"\TextHandler\bin\Debug\ShortTestFile.txt")));
        }
    }
}