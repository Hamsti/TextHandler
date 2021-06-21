using NUnit.Framework;
using NumericalCalculations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace NumericalCalculations.Tests
{
    [TestFixture()]
    public class CalculationHandlerTests
    {
        private static readonly (string, string, Type)[] StringsEmptyNullData =
        {
            (string.Empty, string.Empty, typeof(ArgumentException)),
            ("   ", string.Empty, typeof(ArgumentException)),
            (string.Empty, null, typeof(ArgumentException)),
            (null, null, typeof(ArgumentNullException)),
            (null, "   ", typeof(ArgumentNullException)),
            (null, string.Empty, typeof(ArgumentNullException)),
        };

        private static readonly (string, string, string ExpectedResult)[] AddData =
        {
            ("2", "10", "12"),
            ("120", "50", "170"),
            ("607898337645645643444623958739854763845098390486503476837498263984732759837495732987492873498723984723",
                "32453463452147983425628376487263918724983745628736487263473984759862386549847659834523874843573849283",
                "640351801097793626870252335227118682570082136115239964100972248744595146387343392822016748342297834006"),
            ("324534634521479834256283764872639187249837456287364872634739847598623865498476598345238748435738492830000000000000000000000000000",
                "6078983376456456434446239587398547638450983904865034768374982639847327598374957329874928734987239847230000000000000000000000000000",
                "6403518010977936268702523352271186825700821361152399641009722487445951463873433928220167483422978340060000000000000000000000000000")
        };

        private static readonly (string, string, string ExpectedResult)[] SubtractData =
        {
            ("2", "10", "-8"),
            ("120", "50", "70"),
            ("607898337645645643444623958739854763845098390486503476837498263984732759837495732987492873498723984723",
                "32453463452147983425628376487263918724983745628736487263473984759862386549847659834523874843573849283",
                "575444874193497660018995582252590845120114644857766989574024279224870373287648073152968998655150135440"),
            ("324534634521479834256283764872639187249837456287364872634739847598623865498476598345238748435738492830000000000000000000000000000",
                "6078983376456456434446239587398547638450983904865034768374982639847327598374957329874928734987239847230000000000000000000000000000",
                "-5754448741934976600189955822525908451201146448577669895740242792248703732876480731529689986551501354400000000000000000000000000000")
        };

        private static readonly (string, string, string ExpectedResult)[] MultiplyData =
        {
            ("2", "10", "20"),
            ("120", "50", "6000"),
            ("607898337645645643444623958739854763845098390486503476837498263984732759837495732987492873498723984723",
                "32453463452147983425628376487263918724983745628736487263473984759862386549847659834523874843573849283",
                "19728406483404475494911725131524043219952138673932257015487428961033881477961345624510645669360548709050702332830292507642222558256029204661098738834109112452203902883258935987311837603948483537196503609"),
            ("324534634521479834256283764872639187249837456287364872634739847598623865498476598345238748435738492830000000000000000000000000000",
                "6078983376456456434446239587398547638450983904865034768374982639847327598374957329874928734987239847230000000000000000000000000000",
                "1972840648340447549491172513152404321995213867393225701548742896103388147796134562451064566936054870905070233283029250764222255825602920466109873883410911245220390288325893598731183760394848353719650360900000000000000000000000000000000000000000000000000000000")
        };

        private static readonly (string, string, string ExpectedResult)[] DivideIntData =
        {
            ("2", "10", "0"),
            ("120", "50", "2"),
            ("607898337645645643444623958739854763845098390486503476837498263984732759837495732987492873498723984723",
                "32453463452147983425628376487263918724983745628736487263473984759862386549847659834523874843573849283",
                "18"),
            ("324534634521479834256283764872639187249837456287364872634739847598623865498476598345238748435738492830000000000000000000000000000",
                "6078983376456456434446239587398547638450983904865034768374982639847327598374957329874928734987239847230000000000000000000000000000",
                "0")
        };

        [TestCaseSource(nameof(StringsEmptyNullData))]
        public void AddTest_NullOrWhiteSpace_IsThrowExeption((string, string, Type) data)
        {
            // Act
            Assert.Throws(data.Item3, () => CalculationHandler.Add(data.Item1, data.Item2));
        }

        [TestCaseSource(nameof(StringsEmptyNullData))]
        public void SubtractTest_NullOrWhiteSpace_IsThrowExeption((string, string, Type) data)
        {
            // Act
            Assert.Throws(data.Item3, () => CalculationHandler.Subtract(data.Item1, data.Item2));
        }

        [TestCaseSource(nameof(StringsEmptyNullData))]
        public void MultiplyTest_NullOrWhiteSpace_IsThrowExeption((string, string, Type) data)
        {
            // Act
            Assert.Throws(data.Item3, () => CalculationHandler.Multiply(data.Item1, data.Item2));
        }

        [TestCaseSource(nameof(StringsEmptyNullData))]
        public void DivideTest_NullOrWhiteSpace_IsThrowExeption((string, string, Type) data)
        {
            // Act
            Assert.Throws(data.Item3, () => CalculationHandler.Divide(data.Item1, data.Item2));
        }

        [TestCaseSource(nameof(StringsEmptyNullData))]
        public void DivideIntTest_NullOrWhiteSpace_IsThrowExeption((string, string, Type) data)
        {
            // Act
            Assert.Throws(data.Item3, () => CalculationHandler.DivideInt(data.Item1, data.Item2));
        }

        [Test]
        public void DivideTest_DividingByZero_IsThrowExeption()
        {
            // Act
            Assert.Throws<ArgumentException>(() => CalculationHandler.Divide("1", "0"));
        }

        [Test]
        public void DivideIntTest_DividingByZero_IsThrowExeption()
        {
            // Act
            Assert.Throws<ArgumentException>(() => CalculationHandler.DivideInt("1", "0"));
        }


        [TestCaseSource(nameof(AddData))]
        public void AddTest_BigInteger((string, string, string ExpectedResult) data)
        {
            // Averange
            BigInteger num1 = BigInteger.Parse(data.Item1);
            BigInteger num2 = BigInteger.Parse(data.Item2);

            // Act
            Assert.AreEqual(data.ExpectedResult, CalculationHandler.Add(num1, num2).ToString());
        }

        [TestCaseSource(nameof(SubtractData))]
        public void SubtractTest_BigInteger((string, string, string ExpectedResult) data)
        {
            // Averange
            BigInteger num1 = BigInteger.Parse(data.Item1);
            BigInteger num2 = BigInteger.Parse(data.Item2);

            // Act
            Assert.AreEqual(data.ExpectedResult, CalculationHandler.Subtract(num1, num2).ToString());
        }

        [TestCaseSource(nameof(MultiplyData))]
        public void MultiplyTest_BigInteger((string, string, string ExpectedResult) data)
        {
            // Averange
            BigInteger num1 = BigInteger.Parse(data.Item1);
            BigInteger num2 = BigInteger.Parse(data.Item2);

            // Act
            Assert.AreEqual(data.ExpectedResult, CalculationHandler.Multiply(num1, num2).ToString());
        }

        [TestCaseSource(nameof(DivideIntData))]
        public void DivideIntTest_BigInteger((string, string, string ExpectedResult) data)
        {
            // Averange
            BigInteger num1 = BigInteger.Parse(data.Item1);
            BigInteger num2 = BigInteger.Parse(data.Item2);

            // Act
            Assert.AreEqual(data.ExpectedResult, CalculationHandler.DivideInt(num1, num2).ToString());
        }


        [TestCaseSource(nameof(AddData))]
        public void AddTest((string, string, string ExpectedResult) data) =>
            Assert.AreEqual(data.ExpectedResult, CalculationHandler.Add(data.Item1, data.Item2));

        [TestCaseSource(nameof(SubtractData))]
        public void SubtractTest((string, string, string ExpectedResult) data) => 
            Assert.AreEqual(data.ExpectedResult, CalculationHandler.Subtract(data.Item1, data.Item2));

        [TestCaseSource(nameof(MultiplyData))]
        public void MultiplyTest((string, string, string ExpectedResult) data) =>
            Assert.AreEqual(data.ExpectedResult, CalculationHandler.Multiply(data.Item1, data.Item2));

        [TestCaseSource(nameof(DivideIntData))]
        public void DivideIntTest((string, string, string ExpectedResult) data) =>
            Assert.AreEqual(data.ExpectedResult, CalculationHandler.DivideInt(data.Item1, data.Item2));


        [TestCase("2", "10", ExpectedResult = "0.2")]
        [TestCase("120", "50", ExpectedResult = "2.4")]
        [TestCase("607898337645645643444623958739854763845098390486503476837498263984732759837495732987492873498723984723",
                  "32453463452147983425628376487263918724983745628736487263473984759862386549847659834523874843573849283",
                  ExpectedResult = "18.731385589768569")]
        [TestCase("324534634521479834256283764872639187249837456287364872634739847598623865498476598345238748435738492830000000000000000000000000000",
                  "6078983376456456434446239587398547638450983904865034768374982639847327598374957329874928734987239847230000000000000000000000000000",
                  ExpectedResult = "0.0533863336061065")]
        public string DivideTest(string numeratorValue, string denominatorValue) => CalculationHandler.Divide(numeratorValue, denominatorValue);
    }
}