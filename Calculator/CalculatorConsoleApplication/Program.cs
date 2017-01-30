using System;
using Autofac;
using Calculator.Logic.ArgumentParsing;
using Calculator.Logic.CommandLineParser;
using Calculator.Logic.Pipelines;

namespace CalculatorConsoleApplication
{
    // ReSharper disable once ClassNeverInstantiated.Global
    class Program
    {
        static readonly ApplicationArguments sArgs = new ApplicationArguments();
        static readonly ContainerBuilder sBuilder = new ContainerBuilder();
        static void Main(string[] args)
        {
            var reader = new ConfigFileReader();
            var config = reader.ReadFile();
            if (reader.Errors.Count != 0)
            {
                foreach (var error in reader.Errors)
                {
                    Console.WriteLine(error);
                }
                Console.ReadKey();
                return;
            }
            var creator  = new CommandLineParserCreator();
            var fileParser = creator.ArgumentsSetup(sArgs);
            fileParser.Parse(config);
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
    }
}