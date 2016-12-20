using System;
using System.Linq;
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
            if (IsSimplificationNecessary(token))
                UseSimplifier(token);
            else
                Console.WriteLine(UseEvaluationExpressionVisitor(token));
            Console.ReadKey();
        }

        static Tokenizer GetStringAndCreateTokens()
        {
            var token = new Tokenizer(GetUserInput());
            token.Tokenize();
            return token;
        }

        static IExpression CreateInMemoryModel(Tokenizer token) => new ModelBuilder().BuildFrom(token.Tokens);

        static bool IsSimplificationNecessary(Tokenizer tokenized) => tokenized.Tokens.OfType<VariableToken>().Any();

        static void UseSimplifier(Tokenizer token) => new Simplifier(CreateInMemoryModel(token));

        static double UseEvaluationExpressionVisitor(Tokenizer token) => EvaluatingExpressionVisitor.Evaluate(CreateInMemoryModel(token));

        static string GetUserInput() => Console.ReadLine();
    }
}