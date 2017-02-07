using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace ImperialAndMetricConverter
{
    public class ConversionTokenizer : IConversionTokenizer
    {
        ApplicationArguments mArgs;
        readonly List<IConversionToken> mTempTokens = new List<IConversionToken>();
        string mInput;
        public IEnumerable<IConversionToken> Tokens { get; private set; }

        public void Tokenize(string input, ApplicationArguments arg)
        {
            mArgs = arg;
            mTempTokens.Clear();
            mInput = RemoveWhitespaceAndEqualSign(input);
            Tokens = FillTokens();
        }

        IEnumerable<IConversionToken> FillTokens()
        {
            string number = null;
            foreach (var c in mInput)
            {
                if (c == '+' || c == '-' || c == '*' || c == '/')
                {
                    AddToken(number);
                    number = null;
                    AddArithmeticToken(c);
                }
                else
                {
                    number += c;
                }
            }
            if (number != null)
                AddToken(number);
            return mTempTokens;
        }

        void AddToken(string input)
        {
            if (UnitAbbreviations.ImperialAreas.Any(input.Contains) || mArgs != null && !input.Any(char.IsLetter) && UnitAbbreviations.ImperialAreas.Contains(mArgs.UnitForConversion))
                mTempTokens.Add(new ImperialAreaToken(input, mArgs));
            else if (UnitAbbreviations.ImperialLengths.Any(input.Contains) || mArgs != null && !input.Any(char.IsLetter) && UnitAbbreviations.ImperialLengths.Contains(mArgs.UnitForConversion))
                mTempTokens.Add(new ImperialLengthToken(input, mArgs));
            else if (UnitAbbreviations.ImperialVolumes.Any(input.Contains) || mArgs != null && !input.Any(char.IsLetter) && UnitAbbreviations.ImperialVolumes.Contains(mArgs.UnitForConversion))
                mTempTokens.Add(new ImperialVolumeToken(input, mArgs));
            else if (UnitAbbreviations.ImperialMasses.Any(input.Contains) || mArgs != null && !input.Any(char.IsLetter) && UnitAbbreviations.ImperialMasses.Contains(mArgs.UnitForConversion))
                mTempTokens.Add(new ImperialMassToken(input, mArgs));
            else if (UnitAbbreviations.MetricVolumes.Any(input.Contains) || mArgs != null && !input.Any(char.IsLetter) && UnitAbbreviations.MetricVolumes.Contains(mArgs.UnitForConversion))
                mTempTokens.Add(new MetricVolumeToken(input, mArgs));
            else if (UnitAbbreviations.MetricMasses.Any(input.Contains) || mArgs != null && !input.Any(char.IsLetter) && UnitAbbreviations.MetricMasses.Contains(mArgs.UnitForConversion))
                mTempTokens.Add(new MetricMassToken(input, mArgs));
            else if (UnitAbbreviations.MetricAreas.Any(input.Contains) || mArgs != null && !input.Any(char.IsLetter) && UnitAbbreviations.MetricAreas.Contains(mArgs.UnitForConversion))
                mTempTokens.Add(new MetricAreaToken(input, mArgs));
            else if (UnitAbbreviations.MetricLengths.Any(input.Contains) || mArgs != null && !input.Any(char.IsLetter) && UnitAbbreviations.MetricLengths.Contains(mArgs.UnitForConversion))
                mTempTokens.Add(new MetricLengthToken(input, mArgs));
            else
                throw new InvalidExpressionException($"The input didnt define which system it used. Part that threw Exception: {input} ");
        }

        void AddArithmeticToken(char input)
        {
            mTempTokens.Add(new ConversionOperatorToken(input));
        }

        static string RemoveWhitespaceAndEqualSign(string input)
        {
            return input.TakeWhile(c => c != '=')
                .Where(c => !char.IsWhiteSpace(c))
                .Aggregate("", (current, c) => current + c);
        }
    }
}