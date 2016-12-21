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
                Console.WriteLine(UseFormattingExpressionVisitor(UseSimplifier(token)));
            else
                Console.WriteLine(UseEvaluationExpressionVisitor(token));
            Console.ReadKey();
        }

        static Tokenizer GetStringAndCreateTokens()
        {
            var token = new Tokenizer();
            token.Tokenize(GetUserInput());
            return token;
        }

        static IExpression CreateInMemoryModel(Tokenizer token) => new ModelBuilder().BuildFrom(token.Tokens);
        static IExpression UseSimplifier(Tokenizer token) => new Simplifier().Simplify(CreateInMemoryModel(token));
        static string GetUserInput() => Console.ReadLine();
        static bool IsSimplificationNecessary(Tokenizer tokenized) => tokenized.Tokens.OfType<VariableToken>().Any();
        static double UseEvaluationExpressionVisitor(Tokenizer token) => EvaluatingExpressionVisitor.Evaluate(CreateInMemoryModel(token));

        static string UseFormattingExpressionVisitor(IExpression expression)
        {
            return new FormattingExpressionVisitor().Format(expression);
        }

    }
}