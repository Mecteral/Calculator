using System;
using System.Collections.Generic;
using System.Linq;
using Autofac;
using Calculator.Logic.ArgumentParsing;
using Calculator.Logic.CommandLineParser;
using Calculator.Logic.ConfigFile;
using Calculator.Logic.Pipelines;

namespace CalculatorConsoleApplication
{
    // ReSharper disable once ClassNeverInstantiated.Global
    class Program
    {
        static readonly ConfigFileReader sReader = new ConfigFileReader();
        const string Path = @"C:\Users\Public\Calc\ConfigFileCalculator.txt";
        static readonly ApplicationArguments sArgs = new ApplicationArguments();
        static readonly ContainerBuilder sBuilder = new ContainerBuilder();
        static void Main(string[] args)
        {
            var userConfig = sReader.ReadFile(Path);
            OutputErrorsIfExistent();

            var customConfig = sReader.ReadFile(FindCustomFilePath(args));
            OutputErrorsIfExistent();

            var creator  = new CommandLineParserCreator();
            var fileParser = creator.ArgumentsSetup(sArgs);
            fileParser.Parse(userConfig);

            var specificFileParser = creator.ArgumentsSetup(sArgs);
            specificFileParser.Parse(customConfig);

            var parser = creator.ArgumentsSetup(sArgs);
            parser.Parse(args);

            sBuilder.RegisterAssemblyModules(typeof(ContainerModule).Assembly);
            var container = sBuilder.Build();
            var pipelineEvaluator = container.Resolve<IPipelineEvaluator>();

            var input = GetUserInput();
            Console.WriteLine(pipelineEvaluator.Evaluate(input, sArgs));
            Console.ReadKey();
        }
        static string GetUserInput() => Console.ReadLine();
        static void OutputErrorsIfExistent()
        {
            if (sReader.Errors.Count != 0)
            {
                foreach (var error in sReader.Errors)
                {
                    Console.WriteLine($"Error in Path: \"{sReader.FilePath}\"");
                    Console.WriteLine(error);
                }
                Console.ReadKey();
                Environment.Exit(0);
            }
        }

        static string FindCustomFilePath(IEnumerable<string> args)
        {
            foreach (var s in args)
            {
                if (s.Contains("c=")||s.Contains("custom="))
                {
                    return ExtractCustomFilePath(s);
                }
            }
            return "";
        }

        static string ExtractCustomFilePath(string path)
        {
            var afterequal = false;
            var result = "";
            foreach (var c in path)
            {
                if (c == '=')
                {
                    afterequal = true;
                }
                else if (afterequal)
                {
                    result += c;
                }
            }
            return result;
        }
    }
}