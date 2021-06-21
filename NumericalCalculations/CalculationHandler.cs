using System;
using System.Numerics;

namespace NumericalCalculations
{
    public static class CalculationHandler
    {
        public static BigInteger Add(BigInteger num1, BigInteger num2) => num1 + num2;
        public static BigInteger Subtract(BigInteger num1, BigInteger num2) => num1 - num2;
        public static BigInteger Multiply(BigInteger num1, BigInteger num2) => num1 * num2;
        public static BigInteger DivideInt(BigInteger numerator, BigInteger denominator) =>
            !denominator.IsZero ? numerator / denominator : throw new ArgumentException("Dividing a value by zero throw an exception", nameof(denominator));
        public static string Add(string num1, string num2) => Add(Parse(num1), Parse(num2)).ToString();
        public static string Subtract(string num1, string num2) => Subtract(Parse(num1), Parse(num2)).ToString();
        public static string Multiply(string num1, string num2) => Multiply(Parse(num1), Parse(num2)).ToString();
        public static string DivideInt(string numerator, string denominator) => DivideInt(Parse(numerator), Parse(denominator)).ToString();

        public static string Divide(string numeratorValue, string denominatorValue)
        {
            BigInteger numerator = Parse(numeratorValue);
            BigInteger denominator = Parse(denominatorValue);

            BigInteger integerPart = DivideInt(numerator, denominator);
            double fractionalPart = (double)(numerator % denominator) / (double)denominator;

            return fractionalPart == 0 ? integerPart.ToString() :
                                         integerPart.ToString() + fractionalPart.ToString().Substring(1);
        }

        private static BigInteger Parse(string num)
        {
            if (num is null)
            {
                throw new ArgumentNullException(nameof(num));
            }

            if (string.IsNullOrWhiteSpace(num) || !BigInteger.TryParse(num, out BigInteger numInt))
            {
                throw new ArgumentException($"Can't parse to BigInteger value: \"{num}\"", nameof(num));
            }

            return numInt;
        }
    }
}
