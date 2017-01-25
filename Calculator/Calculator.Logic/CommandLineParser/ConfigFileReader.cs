using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using Microsoft.VisualBasic.ApplicationServices;
using ModernRonin.PraeterArtem.Functional;

namespace Calculator.Logic.CommandLineParser
{
    public static class ConfigFileReader
    {
        const string Path = @"C:\Users\Public\Calc\ConfigFileCalculator.txt";
        static readonly List<string> sReceivers = new List<string>();
        public static string[] ReadFile()
        {
            if (File.Exists(Path))
            {
                var config = File.ReadAllLines(Path);
                ExtractReceivers(config);
                CheckForInvalidArguments();
                return config;
            }
            return null;
        }

        static void ExtractReceivers(IEnumerable<string> config)
        {
            var temp = "";
            foreach (var s in config)
            {
                var start = 0;
                if (s.IsEmpty())
                {
                    continue;
                }
                if (s[0] == '-' && s[1] == '-')
                {
                    start = 2;
                }
                else
                {
                    start = 1;
                }
                for (var i = start; i < s.Length; i++)
                {
                    if (char.IsLetter(s[i]))
                    {
                        temp += s[i];
                    }
                    else if (s[i] == '=' || s[i] == '+' || s[i] == '-')
                    {
                        break;
                    }
                }
                sReceivers.Add(temp);
                temp = "";
            }
        }
        static void CheckForInvalidArguments()
        {
            var names = new ParserShortAndLongNames();
            var fieldValues = names.GetType()
            .GetFields()
            .Where(fi => fi.IsLiteral && !fi.IsInitOnly)
            .Select(field => field.GetValue(null))
            .ToList();
            if (sReceivers.Select(s => fieldValues.Select(fieldValue => fieldValue.ToString()).Any(toTest => s == toTest)).Any(isEqual => !isEqual))
            {
                throw new InvalidExpressionException();
            }

        }
    }
}