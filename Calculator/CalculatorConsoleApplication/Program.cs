using System;
using System.Linq;
using Calculator.Logic;
using Calculator.Logic.ArgumentParsing;
using Calculator.Logic.Model;
using Calculator.Logic.Model.ConversionModel;
using Calculator.Logic.Parsing;
using Calculator.Logic.Parsing.CalculationTokenizer;
using Calculator.Logic.Parsing.ConversionTokenizer;
using Calculator.Model;
using Fclp;

namespace CalculatorConsoleApplication
{
    // ReSharper disable once ClassNeverInstantiated.Global
    class Program
    {
        static ApplicationArguments sArgs;
        static void Main(string[] args)
        {
            sArgs = new ApplicationArguments();
            var parser = ArgumentsSetup();
            parser.Parse(args);
            var input = GetUserInput();
            if (input.Contains("=?"))
            {
                ConvertUnits(input);
            }
            else
            {
                var token = CreateTokens(input);
                if (IsSimplificationNecessary(token)) Console.WriteLine(UseFormattingExpressionVisitor(UseSimplifier(token)));
                else Console.WriteLine(UseEvaluationExpressionVisitor(token));
            }
            Console.ReadKey();
        }

        static FluentCommandLineParser ArgumentsSetup()
        {
            var help = new HelpText();
            var parser = new FluentCommandLineParser();
            parser.Setup<bool>('d', "degree").Callback(arg => sArgs.ToDegree = arg).SetDefault(false);
            parser.Setup<string>('u', "unit").Callback(arg => sArgs.UnitForConversion = arg).SetDefault(null);
            parser.SetupHelp("h", "help").Callback(text => Console.WriteLine(help.HelpStrings.Aggregate("", (current, s) => current + s)));
            return parser;
        }
        static Tokenizer CreateTokens(string input)
        {
            var token = new Tokenizer();
            token.Tokenize(input, sArgs);
            return token;
        }

        static ConversionTokenizer CreateConversionTokens(string input)
        {
            var token = new ConversionTokenizer();
            token.Tokenize(input, sArgs);
            return token;
        }

        static void ConvertUnits(string input)
        {
            Console.WriteLine("Do you want to convert to the metric system?");
            var userInput = Console.ReadLine();
            var toMetric = userInput == "y";
            var token = CreateConversionTokens(input);
            var converted = UseUnitConverter(CreateConversionInMemoryModel(token), toMetric);
            var output = new ReadableOutputCreator();
            Console.WriteLine(output.MakeReadable((IConversionExpressionWithValue)converted));
        }
        static IConversionExpression CreateConversionInMemoryModel(ConversionTokenizer token) => new ConversionModelBuilder().BuildFrom(token.Tokens);
        static IConversionExpression UseUnitConverter(IConversionExpression expression, bool toMetric) => new UnitConverter().Convert(expression, toMetric);
        static IExpression CreateInMemoryModel(ITokenizer token) => new ModelBuilder().BuildFrom(token.Tokens);
        static IExpression UseSimplifier(ITokenizer token) => new Simplifier().Simplify(CreateInMemoryModel(token));
        static string GetUserInput() => Console.ReadLine();
        static bool IsSimplificationNecessary(ITokenizer tokenized) => tokenized.Tokens.OfType<VariableToken>().Any();
        static decimal UseEvaluationExpressionVisitor(ITokenizer token)
            => EvaluatingExpressionVisitor.Evaluate(CreateInMemoryModel(token));
        static string UseFormattingExpressionVisitor(IExpression expression)
            => new FormattingExpressionVisitor().Format(expression);
    }
}