using System;
using Calculator.Logic;

namespace CalculatorConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            var result = ArgumentParser.Tokenize(args);
            foreach (var variable in result)
            {
                Console.Write(variable);
            }
            Console.ReadKey();
        }
    }
}