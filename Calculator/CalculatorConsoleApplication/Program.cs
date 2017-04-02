using System;
using System.Collections.Generic;
using System.IO;
using Autofac;
using Calculator.Logic.ArgumentParsing;
using Calculator.Logic.CommandLineParser;
using Calculator.Logic.ConfigFile;
using Calculator.Logic.Parsing.CalculationTokenizer;
using Calculator.Logic.Pipelines;

namespace CalculatorConsoleApplication
{
    // ReSharper disable once ClassNeverInstantiated.Global
    class Program
    {
        const string DefaultConfig =
            "E:\\Programmieren\\ExercisesFromMax\\koans\\Calculator\\Calculator\\CalculatorConsoleApplication\\DefaultConfig.txt";

        const string CommandLineError = "Commandline Switches";
        static readonly ConfigFileReader sReader = new ConfigFileReader();
        static readonly ConfigFileValidator sValidator = new ConfigFileValidator();

        static readonly string sPathToUserFile = Environment.GetFolderPath(Environment.SpecialFolder.Personal) +
                                                 $"\\CalculatorConfig\\ConfigFileCalculator.txt";

        static readonly ApplicationArguments sArguments = new ApplicationArguments();
        static readonly ContainerBuilder sBuilder = new ContainerBuilder();
        static readonly CommandLineParserCreator sCreator = new CommandLineParserCreator();
        static readonly InputStringValidator sStringValidator = new InputStringValidator();

        static void Main(string[] args)
        {
            var defaultConfig = sReader.ReadFile(DefaultConfig);
            var defaultParser = sCreator.ArgumentsSetup(sArguments);
            defaultParser.Parse(defaultConfig);

            var userConfig = sReader.ReadFile(sPathToUserFile);
            sValidator.CheckForValidation(userConfig, sPathToUserFile);
            OutputErrorsIfExistent();

            sValidator.CheckForValidation(args, CommandLineError);
            OutputErrorsIfExistent();

            var fileParser = sCreator.ArgumentsSetup(sArguments);
            fileParser.Parse(userConfig);

            var parser = sCreator.ArgumentsSetup(sArguments);
            parser.Parse(args);

            if (sArguments.UseCustomConfigFile != "")
                RunCustomParser(args);
            if (sArguments.ImportFromSpecificConfigFile != "")
                ImportSpecificFile();
            if (sArguments.WriteSwitchesToDefault)
                WriteSwitchesToDefault(args, userConfig);
            if (sArguments.RevertConfig)
                RevertConfig();

            var input = GetUserInput();
            try
            {
                sStringValidator.Validate(input);
            }
            catch (CalculationException x)
            {
                OnStringError(x, input);
            }


            sBuilder.RegisterAssemblyModules(typeof(Calculator.Logic.LogicModule).Assembly);
            var container = sBuilder.Build();
            var pipelineEvaluator = container.Resolve<IPipelineEvaluator>();

            Console.WriteLine(pipelineEvaluator.Evaluate(input, sArguments));
            Console.ReadKey();
        }

        static string GetUserInput() => Console.ReadLine();

        static void OnStringError(CalculationException x, string input)
        {
            Console.WriteLine($"{x.Message} \n{input}");
            for (var i = 0; i < x.Index; i++)
            {
                Console.Write(".");
            }
            Console.Write("^");
            Console.ReadKey();
            Environment.Exit(-1);
        }
        static void WriteSwitchesToDefault(IEnumerable<string> args, IEnumerable<string> userConfig)
        {
            var writer = new SwitchesToConfigFileWriter();
            writer.WriteToConfigFile(args, userConfig, sArguments, sPathToUserFile);
        }
        static void RunCustomParser(string[] args)
        {
            var customConfig = sReader.ReadFile(sArguments.UseCustomConfigFile);
            sValidator.CheckForValidation(customConfig, sArguments.UseCustomConfigFile);
            OutputErrorsIfExistent();

            var specificFileParser = sCreator.ArgumentsSetup(sArguments);
            specificFileParser.Parse(customConfig);

            var afterSpecificParser = sCreator.ArgumentsSetup(sArguments);
            afterSpecificParser.Parse(args);
        }

        static void ImportSpecificFile()
        {
            if (File.Exists(sArguments.ImportFromSpecificConfigFile))
            {
                var specific = sReader.ReadFile(sArguments.ImportFromSpecificConfigFile);
                sValidator.CheckForValidation(specific,sArguments.ImportFromSpecificConfigFile);
                OutputErrorsIfExistent();
                File.Copy(sArguments.ImportFromSpecificConfigFile, sPathToUserFile, true);
            }
            else
            {
                Console.WriteLine("The Specific Config File does not exist");
                Console.ReadKey();
                Environment.Exit(-1);
            }
        }
        static void RevertConfig()
        {
                if (File.Exists(DefaultConfig))
                {
                    File.Copy(DefaultConfig, sPathToUserFile, true);
                }
                else
                {
                    Console.WriteLine("The Default Config File does not exist anymore!");
                    Console.ReadKey();
                    Environment.Exit(-1);
                }
        }

        static void OutputErrorsIfExistent()
        {
            if (sValidator.Errors.Count != 0)
            {
                foreach (var error in sValidator.Errors)
                {
                    Console.WriteLine($"Error in: \"{sValidator.FilePath}\"\n");
                    Console.WriteLine(error);
                }
                Console.ReadKey();
                Environment.Exit(-1);
            }
        }
    }
}