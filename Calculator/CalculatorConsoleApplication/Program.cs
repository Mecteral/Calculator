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
            var token = GetStringAndCreateTokens();
            if (IsSimplificationIsNecessary(token))
            {
                
            }
            else
                GetStringAndPrintResult(token);
        }

        static void GetStringAndPrintResult(Tokenizer token)
        {
            GetStringAndCreateTokens();
            var model = new ModelBuilder();
            var evaluate = model.BuildFrom(token.Tokens);
            var result = EvaluatingExpressionVisitor.Evaluate(evaluate);
            Console.WriteLine(result);
            Console.ReadKey();
        }

        static Tokenizer GetStringAndCreateTokens()
        {
            var input = Console.ReadLine();
            var token = new Tokenizer(input);
            token.Tokenize();
            return token;
        }

        static bool IsSimplificationIsNecessary(Tokenizer tokenized)
        {
            var isSimplificationNeeded = false;
            foreach (var token in tokenized.Tokens)
            {
                if (token is VariableToken)
                    isSimplificationNeeded = true;
            }
            return isSimplificationNeeded;
        }
    }
}