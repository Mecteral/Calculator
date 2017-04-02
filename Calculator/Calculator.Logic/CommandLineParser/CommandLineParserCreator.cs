using System;
using System.Linq;
using Calculator.Logic.ArgumentParsing;
using Fclp;

namespace Calculator.Logic.CommandLineParser
{
    public class CommandLineParserCreator : ICommandLineParserCreator
    {
        public FluentCommandLineParser ArgumentsSetup(ApplicationArguments args)
        {
            var help = new HelpText();
            var parser = new FluentCommandLineParser();
            parser.SetupHelp(ParserShortAndLongNames.HelpShort, ParserShortAndLongNames.HelpLong)
                .Callback(text => Console.WriteLine(help.HelpStrings.Aggregate("", (current, s) => current + s)));
            parser.Setup<bool>(ParserShortAndLongNames.ToDegreeShort, ParserShortAndLongNames.ToDegreeLong)
                .Callback(arg => args.ToDegree = arg);
            parser.Setup<bool>(ParserShortAndLongNames.ShowStepsShort, ParserShortAndLongNames.ShowStepsLong)
                .Callback(arg => args.ShowSteps = arg);
            parser.Setup<bool>(ParserShortAndLongNames.WriteToConfigShort, ParserShortAndLongNames.WriteToConfigLong)
                .Callback(arg => args.WriteSwitchesToDefault = arg);
            parser.Setup<bool>(ParserShortAndLongNames.RevertShort, ParserShortAndLongNames.RevertLong)
                .Callback(arg => args.RevertConfig = arg);
            parser.Setup<bool>(ParserShortAndLongNames.SaveAllSwitchesShort, ParserShortAndLongNames.SaveAllSwitchesLong)
                .Callback(arg => args.SaveAllOrIgnoreAllDifferingSwitches = arg);
            parser.Setup<string>(ParserShortAndLongNames.UnitConversionShort, ParserShortAndLongNames.UnitConversionLong)
                .Callback(arg => args.UnitForConversion = arg);
            parser.Setup<string>(ParserShortAndLongNames.ImportShort, ParserShortAndLongNames.ImportLong)
                .Callback(arg => args.ImportFromSpecificConfigFile = arg);
            parser.Setup<string>(ParserShortAndLongNames.CustomShort, ParserShortAndLongNames.CustomLong)
                .Callback(arg => args.UseCustomConfigFile = arg);
            return parser;
        }
    }
}