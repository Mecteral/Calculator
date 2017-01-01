using System;
using System.Linq;
using Calculator.Logic;
using Calculator.Logic.Model;
using Calculator.Logic.Parsing;
using Calculator.Model;

namespace CalculatorConsoleApplication
{
    // ReSharper disable once ClassNeverInstantiated.Global
    class Program
    {
        static void Main(string[] args)
        {
            var token = GetStringAndCreateTokens();
            if (IsSimplificationNecessary(token)) Console.WriteLine(UseFormattingExpressionVisitor(UseSimplifier(token)));
            else Console.WriteLine(UseEvaluationExpressionVisitor(token));
            Console.ReadKey();
        }
        static Tokenizer GetStringAndCreateTokens()
        {
            var token = new Tokenizer();
            token.Tokenize(GetUserInput());
            return token;
        }
        static IExpression CreateInMemoryModel(ITokenizer token) => new ModelBuilder().BuildFrom(token.Tokens);
        static IExpression UseSimplifier(ITokenizer token) => new Simplifier().Simplify(CreateInMemoryModel(token));
        static string GetUserInput() => Console.ReadLine();
        static bool IsSimplificationNecessary(ITokenizer tokenized) => tokenized.Tokens.OfType<VariableToken>().Any();
        static double UseEvaluationExpressionVisitor(ITokenizer token)
            => EvaluatingExpressionVisitor.Evaluate(CreateInMemoryModel(token));
        static string UseFormattingExpressionVisitor(IExpression expression)
            => new FormattingExpressionVisitor().Format(expression);
    }
}