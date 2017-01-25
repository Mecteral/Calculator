using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using Calculator.Logic.Parsing.ConversionTokenizer;
using Microsoft.VisualBasic.ApplicationServices;
using ModernRonin.PraeterArtem.Functional;

namespace Calculator.Logic.CommandLineParser
{
    public static class ConfigFileReader
    {
        const string Path = @"C:\Users\Public\Calc\ConfigFileCalculator.txt";
        static readonly List<string> sReceivers = new List<string>();
        static readonly List<string> sValues = new List<string>();
        public static string[] ReadFile()
        {
            if (File.Exists(Path))
            {
                var config = File.ReadAllLines(Path);
                ExtractReceiversAndValues(config);
                CheckForInvalidArguments();
                CheckForInvalidEntries();
                return config;
            }
            return null;
        }

        static void ExtractReceiversAndValues(IEnumerable<string> config)
        {
            var tempReceiver = "";
            var tempValue = "";
            var valueStart = 0;
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
                        tempReceiver += s[i];
                    }
                    else if (s[i] == '=' || s[i] == '+' || s[i] == '-')
                    {
                        valueStart = i;
                        break;
                    }
                }
                sReceivers.Add(tempReceiver);
                tempReceiver = "";
                for (var i = valueStart; i < s.Length; i++)
                {
                    if (s[i] != '\"' && s[i] != '=')
                    {
                        tempValue += s[i];
                    }

                }
                sValues.Add(tempValue);
                tempValue = "";
            }
        }

        static void CheckForInvalidEntries()
        {
            string usedUnit = null;
            string usedRad = null;
            var abbreviation = new UnitAbbreviations();
            var fieldValues = abbreviation.GetType()
                .GetFields()
                .Where(fi => fi.IsLiteral && !fi.IsInitOnly)
                .Select(field => field.GetValue(null))
                .ToList();
            for (var i = 0; i < sReceivers.Count; i++)
            {
                if (sReceivers[i] == "u" || sReceivers[i] == "unit")
                {
                    if (usedUnit == null)
                        usedUnit = sValues[i];
                    if (usedUnit != null && sValues[i] != usedUnit)
                        throw new InvalidExpressionException("Conflicting entries with unit or u");
                    if (!fieldValues.Contains(usedUnit))
                        throw new InvalidExpressionException("Wrong abbreviation in config file");
                }
                else if (sReceivers[i] == "d" || sReceivers[i] == "degree")
                {
                    if (usedRad == null)
                        usedRad = sValues[i];
                    if (usedRad != null && sValues[i] != usedRad)
                        throw new InvalidExpressionException("Conflicting entries with degree or d");
                }
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
                throw new InvalidExpressionException("There is an Argument defined in the config file that doesnt exist.");
            }
        }
    }
}