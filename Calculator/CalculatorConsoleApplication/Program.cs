using System;
using System.Runtime.Remoting.Messaging;
using Calculator.Logic;
using Calculator.Logic.Model;
using Calculator.Logic.Parsing;

namespace CalculatorConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            GetStringAndPrintResult();
        }

        static void GetStringAndPrintResult()
        {
            var input = Console.ReadLine();
            var token = new Tokenizer(input);
            token.Tokenize();
            var model = new ModelBuilder();
            var evaluate = model.BuildFrom(token.Tokens);
            var result = EvaluatingExpressionVisitor.Evaluate(evaluate);
            Console.WriteLine(result);
            Console.ReadKey();
        }
    }
}