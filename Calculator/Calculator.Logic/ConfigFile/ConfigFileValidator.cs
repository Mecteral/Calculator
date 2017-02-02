using System.Collections.Generic;
using System.Linq;
using Calculator.Logic.CommandLineParser;
using Calculator.Logic.Parsing.ConversionTokenizer;
using ModernRonin.PraeterArtem.Functional;

namespace Calculator.Logic.ConfigFile
{
    public class ConfigFileValidator
    {
        public string FilePath { get; private set; }
        public List<string> Errors { get; private set; }
        List<string> mReceivers;
        List<string> mValues;
        string mUsedUnit;
        string mUsedRad;
        string mShowSteps;
        string mWriter;
        string mRevert;
        string mImport;
        string mCustom;
        string mSaveAll;

        public void CheckForValidation(IEnumerable<string> input, string path)
        {
            SetDefault();
            FilePath = path;
            ExtractReceiversAndValues(input);
            CheckForInvalidEntries();
            CheckForInvalidArguments();
        }

        void SetDefault()
        {
            Errors = new List<string>();
            mReceivers = new List<string>();
            mValues = new List<string>();
            mUsedUnit = null;
            mUsedRad = null;
            mShowSteps = null;
            mWriter = null;
            mRevert = null;
            mImport = null;
            mCustom = null;
            mSaveAll = null;
        }
        void ExtractReceiversAndValues(IEnumerable<string> input)
        {
            var extractor = new ReceiverAndValueExtractor();
            extractor.ExtractReceiversAndValues(input);
            mReceivers = extractor.Receivers;
            mValues = extractor.Values;
        }

        void CheckForInvalidEntries()
        {
            
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
                    if (mUsedUnit == null)
                        mUsedUnit = mValues[i];
                    else if (mUsedUnit != null && mValues[i] != mUsedUnit)
                        Errors.Add($"Conflicting entries with {mReceivers[i]}");
                    else if (!fieldValues.Contains(mUsedUnit) && mUsedUnit != "")
                        Errors.Add("Wrong abbreviation in config file");
                }
                else if (mReceivers[i] == "d" || mReceivers[i] == "degree")
                {
                    if (mUsedRad == null)
                        mUsedRad = mValues[i];
                    else if (mUsedRad != null && mValues[i] != mUsedRad)
                        Errors.Add($"Conflicting entries with {mReceivers[i]}");
                }
                else if (mReceivers[i] == "s" || mReceivers[i] == "steps")
                {
                    if (mShowSteps == null)
                        mShowSteps = mValues[i];
                    else if (mShowSteps != null && mValues[i] != mShowSteps)
                        Errors.Add($"Conflicting entries with {mReceivers[i]}");
                }
                else if (mReceivers[i] == "w" || mReceivers[i] == "writer")
                {
                    if (mWriter == null)
                        mWriter = mValues[i];
                    else if (mWriter != null && mValues[i] != mWriter)
                        Errors.Add($"Conflicting entries with {mReceivers[i]}");
                }
                else if (mReceivers[i] == "r" || mReceivers[i] == "revert")
                {
                    if (mRevert == null)
                        mRevert = mValues[i];
                    else if (mRevert != null && mValues[i] != mRevert)
                        Errors.Add($"Conflicting entries with {mReceivers[i]}");
                }
                else if (mReceivers[i] == "i" || mReceivers[i] == "import")
                {
                    if (mImport == null)
                        mImport = mValues[i];
                    else if (mImport != null && mValues[i] != mImport)
                        Errors.Add($"Conflicting entries with {mReceivers[i]}");
                }
                else if (mReceivers[i] == "c" || mReceivers[i] == "custom")
                {
                    if (mCustom == null)
                        mCustom = mValues[i];
                    else if (mCustom != null && mValues[i] != mCustom)
                        Errors.Add($"Conflicting entries with {mReceivers[i]}");
                }
                else if (mReceivers[i] == "a" || mReceivers[i] == "saveAll")
                {
                    if (mSaveAll == null)
                        mSaveAll = mValues[i];
                    else if (mSaveAll != null && mValues[i] != mSaveAll)
                        Errors.Add($"Conflicting entries with {mReceivers[i]}");
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
                Errors.Add("There is an Argument defined that doesnt exist.");
            }
        }
    }
}