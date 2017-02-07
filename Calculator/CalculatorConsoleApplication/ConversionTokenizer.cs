using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace ImperialAndMetricConverter
{
    public class ConversionTokenizer : IConversionTokenizer
    {
        readonly List<IConversionToken> mTempTokens = new List<IConversionToken>();
        string mInput;
        public IEnumerable<IConversionToken> Tokens { get; private set; }

        public void Tokenize(string input)
        {
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
            if (UnitAbbreviations.ImperialAreas.Any(input.Contains))
                mTempTokens.Add(new ImperialAreaToken(input));
            else if (UnitAbbreviations.ImperialLengths.Any(input.Contains))
                mTempTokens.Add(new ImperialLengthToken(input));
            else if (UnitAbbreviations.ImperialVolumes.Any(input.Contains))
                mTempTokens.Add(new ImperialVolumeToken(input));
            else if (UnitAbbreviations.ImperialMasses.Any(input.Contains))
                mTempTokens.Add(new ImperialMassToken(input));
            else if (UnitAbbreviations.MetricVolumes.Any(input.Contains))
                mTempTokens.Add(new MetricVolumeToken(input));
            else if (UnitAbbreviations.MetricMasses.Any(input.Contains))
                mTempTokens.Add(new MetricMassToken(input));
            else if (UnitAbbreviations.MetricAreas.Any(input.Contains))
                mTempTokens.Add(new MetricAreaToken(input));
            else if (UnitAbbreviations.MetricLengths.Any(input.Contains))
                mTempTokens.Add(new MetricLengthToken(input));
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