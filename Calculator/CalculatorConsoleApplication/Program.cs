using System;
using System.Collections.Generic;
using System.IO;
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
        const string DefaultConfig = "E:\\Programmieren\\ExercisesFromMax\\koans\\Calculator\\Calculator\\CalculatorConsoleApplication\\DefaultConfig.txt";
        const string CommandLineError = "Commandline Switches";
        static readonly ConfigFileReader sReader = new ConfigFileReader();
        static readonly ConfigFileValidator sValidator = new ConfigFileValidator();

        static readonly string sUserPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal) +
                                       $"\\CalculatorConfig\\ConfigFileCalculator.txt";

        static readonly ApplicationArguments sArgs = new ApplicationArguments();
        static readonly ContainerBuilder sBuilder = new ContainerBuilder();
        static readonly CommandLineParserCreator sCreator = new CommandLineParserCreator();

        static void Main(string[] args)
        {
            var defaultConfig = sReader.ReadFile(DefaultConfig);
            var defaultParser = sCreator.ArgumentsSetup(sArgs);
            defaultParser.Parse(defaultConfig);

            var userConfig = sReader.ReadFile(sUserPath);
            sValidator.CheckForValidation(userConfig, sUserPath);
            OutputErrorsIfExistent();

            sValidator.CheckForValidation(args, CommandLineError);
            OutputErrorsIfExistent();

            var fileParser = sCreator.ArgumentsSetup(sArgs);
            fileParser.Parse(userConfig);

            var parser = sCreator.ArgumentsSetup(sArgs);
            parser.Parse(args);

            if (sArgs.UseCustomConfigFile != "")
                RunCustomParser(args);
            if (sArgs.ImportFromSpecificConfigFile != "")
                ImportSpecificFile();
            if (sArgs.WriteSwitchesToDefault)
                WriteSwitchesToDefault();
            if (sArgs.RevertConfig)
                RevertConfig();


            sBuilder.RegisterAssemblyModules(typeof(ContainerModule).Assembly);
            var container = sBuilder.Build();
            var pipelineEvaluator = container.Resolve<IPipelineEvaluator>();

            var input = GetUserInput();
            Console.WriteLine(pipelineEvaluator.Evaluate(input, sArgs));
            Console.ReadKey();
        }

        static string GetUserInput() => Console.ReadLine();

        static void WriteSwitchesToDefault()
        {
            
        }
        static void RunCustomParser(string[] args)
        {
            var customConfig = sReader.ReadFile(sArgs.UseCustomConfigFile);
            sValidator.CheckForValidation(customConfig, sArgs.UseCustomConfigFile);
            OutputErrorsIfExistent();

            var specificFileParser = sCreator.ArgumentsSetup(sArgs);
            specificFileParser.Parse(customConfig);

            var afterSpecificParser = sCreator.ArgumentsSetup(sArgs);
            afterSpecificParser.Parse(args);
        }

        static void ImportSpecificFile()
        {
            if (File.Exists(sArgs.ImportFromSpecificConfigFile))
            {
                var specific = sReader.ReadFile(sArgs.ImportFromSpecificConfigFile);
                sValidator.CheckForValidation(specific,sArgs.ImportFromSpecificConfigFile);
                OutputErrorsIfExistent();
                File.Copy(sArgs.ImportFromSpecificConfigFile, sUserPath, true);
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
                    File.Copy(DefaultConfig, sUserPath, true);
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