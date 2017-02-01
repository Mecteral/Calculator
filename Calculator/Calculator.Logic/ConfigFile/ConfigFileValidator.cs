using System.Collections.Generic;
using System.Linq;
using Calculator.Logic.CommandLineParser;
using Calculator.Logic.Parsing.ConversionTokenizer;
using ModernRonin.PraeterArtem.Functional;

namespace Calculator.Logic.ConfigFile
{
    public class ConfigFileValidator
    {
        readonly List<string> mErrors = new List<string>();
        readonly List<string> mReceivers = new List<string>();
        readonly List<string> mValues = new List<string>();

        public List<string> CheckForValidation(IEnumerable<string> input)
        {
            ExtractReceiversAndValues(input);
            CheckForInvalidEntries();
            CheckForInvalidArguments();
            return mErrors;
        }

        void ExtractReceiversAndValues(IEnumerable<string> config)
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
                mReceivers.Add(tempReceiver);
                tempReceiver = "";
                for (var i = valueStart; i < s.Length; i++)
                {
                    if (s[i] != '\"' && s[i] != '=')
                    {
                        tempValue += s[i];
                    }
                }
                mValues.Add(tempValue);
                tempValue = "";
            }
        }

        void CheckForInvalidEntries()
        {
            string usedUnit = null;
            string usedRad = null;
            string showSteps = null;
            string writer = null;
            string revert = null;
            string import = null;
            string custom = null;
            var abbreviation = new UnitAbbreviations();
            var fieldValues = abbreviation.GetType()
                .GetFields()
                .Where(fi => fi.IsLiteral && !fi.IsInitOnly)
                .Select(field => field.GetValue(null))
                .ToList();
            for (var i = 0; i < mReceivers.Count; i++)
            {
                if (mReceivers[i] == "u" || mReceivers[i] == "unit")
                {
                    if (usedUnit == null)
                        usedUnit = mValues[i];
                    if (usedUnit != null && mValues[i] != usedUnit)
                        mErrors.Add($"Conflicting entries with {mValues[i]}");
                    if (!fieldValues.Contains(usedUnit))
                        mErrors.Add("Wrong abbreviation in config file");
                }
                else if (mReceivers[i] == "d" || mReceivers[i] == "degree")
                {
                    if (usedRad == null)
                        usedRad = mValues[i];
                    if (usedRad != null && mValues[i] != usedRad)
                        mErrors.Add($"Conflicting entries with {mValues[i]}");
                }
                else if (mReceivers[i] == "s" || mReceivers[i] == "steps")
                {
                    if (showSteps == null)
                        showSteps = mValues[i];
                    if (showSteps != null && mValues[i] != showSteps)
                        mErrors.Add($"Conflicting entries with {mValues[i]}");
                }
                else if (mReceivers[i] == "w" || mReceivers[i] == "writer")
                {
                    if (writer == null)
                        writer = mValues[i];
                    if (writer != null && mValues[i] != writer)
                        mErrors.Add($"Conflicting entries with {mValues[i]}");
                }
                else if (mReceivers[i] == "r" || mReceivers[i] == "revert")
                {
                    if (revert == null)
                        revert = mValues[i];
                    if (revert != null && mValues[i] != revert)
                        mErrors.Add($"Conflicting entries with {mValues[i]}");
                }
                else if (mReceivers[i] == "i" || mReceivers[i] == "import")
                {
                    if (import == null)
                        import = mValues[i];
                    if (import != null && mValues[i] != import)
                        mErrors.Add($"Conflicting entries with {mValues[i]}");
                }
                else if (mReceivers[i] == "c" || mReceivers[i] == "custom")
                {
                    if (custom == null)
                        custom = mValues[i];
                    if (custom != null && mValues[i] != custom)
                        mErrors.Add($"Conflicting entries with {mValues[i]}");
                }
            }
        }

        void CheckForInvalidArguments()
        {
            var names = new ParserShortAndLongNames();
            var fieldValues = names.GetType()
                .GetFields()
                .Where(fi => fi.IsLiteral && !fi.IsInitOnly)
                .Select(field => field.GetValue(null))
                .ToList();
            if (
                mReceivers.Select(
                        s => fieldValues.Select(fieldValue => fieldValue.ToString()).Any(toTest => s == toTest))
                    .Any(isEqual => !isEqual))
            {
                mErrors.Add("There is an Argument defined in the config file that doesnt exist.");
            }
        }
    }
}