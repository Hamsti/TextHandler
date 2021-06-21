using System;

namespace NumericalCalculations
{
    public class Program
    {
        static void Main()
        {
            InteractionInterface();
        }

        private static void InteractionInterface()
        { 
            Console.Write("Input first integer number:  ");
            string num1 = Console.ReadLine();
            
            Console.Write("Input second integer number: ");
            string num2 = Console.ReadLine();

            Console.WriteLine("Select the required operation\n" +
                              "[1] Addition (+)\n" +
                              "[2] Subtraction (-)\n" +
                              "[3] Multiplication (*)\n" +
                              "[4] Division (/)\n" +
                              "[5] Division with fractional part (/)\n" +
                              "[0] Exit\n" +
                              "Another key for reset");
            var keyInfo = Console.ReadKey();
            Console.WriteLine("\n");

            try
            {
                switch (keyInfo.KeyChar)
                {
                    case '1': Console.WriteLine($"{num1} + {num2} = {CalculationHandler.Add(num1, num2)}"); break;
                    case '2': Console.WriteLine($"{num1} - {num2} = {CalculationHandler.Subtract(num1, num2)}"); break;
                    case '3': Console.WriteLine($"{num1} * {num2} = {CalculationHandler.Multiply(num1, num2)}"); break;
                    case '4': Console.WriteLine($"{num1} / {num2} = {CalculationHandler.DivideInt(num1, num2)}"); break;
                    case '5': Console.WriteLine($"{num1} / {num2} = {CalculationHandler.Divide(num1, num2)}"); break;
                    case '0': return;
                    default: Console.WriteLine("Reset\n"); InteractionInterface(); return;
                }
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine(ex);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex);
            }

            Console.WriteLine("\n");
            InteractionInterface();
        }
    }
}
