﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Calculator.Logic.ArgumentParsing;
using CalculatorConsoleApplication;
using Fclp;

namespace Calculator.Logic.CommandLineParser
{
    public static class CommandLineParserCreator
    {
        public static FluentCommandLineParser ArgumentsSetup(ApplicationArguments args)
        {
            var help = new HelpText();
            var parser = new FluentCommandLineParser();
            parser.Setup<bool>(ParserShortAndLongNames.ToDegreeShort, ParserShortAndLongNames.ToDegreeLong).Callback(arg => args.ToDegree = arg).SetDefault(false);
            parser.Setup<string>(ParserShortAndLongNames.UnitConversionShort, ParserShortAndLongNames.UnitConversionLong).Callback(arg => args.UnitForConversion = arg).SetDefault(null);
            parser.SetupHelp(ParserShortAndLongNames.HelpShort, ParserShortAndLongNames.HelpLong).Callback(text => Console.WriteLine(help.HelpStrings.Aggregate("", (current, s) => current + s)));
            return parser;
        }
    }
}