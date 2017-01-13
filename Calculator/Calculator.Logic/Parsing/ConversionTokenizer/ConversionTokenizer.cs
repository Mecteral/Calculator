using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Calculator.Logic.Parsing.ConversionTokenizer
{
    public class ConversionTokenizer
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
            if (input.Contains("sft") || input.Contains("perch") || input.Contains("rood") || input.Contains("acre"))
                mTempTokens.Add(new ImperialAreaToken(input));
            else if (input.Contains("ft") || input.Contains("in") || input.Contains("yd") || input.Contains("mI") ||
                     input.Contains("th") || input.Contains("ch") || input.Contains("fur") || input.Contains("lea") ||
                     input.Contains("ftm"))
                mTempTokens.Add(new ImperialLengthToken(input));
            else if (input.Contains("floz") || input.Contains("gi") || input.Contains("pt") || input.Contains("qt") ||
                     input.Contains("gal"))
                mTempTokens.Add(new ImperialVolumeToken(input));
            else if (input.Contains("gr") || input.Contains("dr") || input.Contains("oz") || input.Contains("lb") ||
                     input.Contains("st") || input.Contains("cwt") || input.Contains("it"))
                mTempTokens.Add(new ImperialMassToken(input));
            else if (input.Contains("ml") || input.Contains("cl") || input.Contains("l") || input.Contains("hl"))
                mTempTokens.Add(new MetricVolumeToken(input));
            else if (input.Contains("mg") || input.Contains("g") || input.Contains("kg") || input.Contains("t"))
                mTempTokens.Add(new MetricMassToken(input));
            else if (input.Contains("qmm") || input.Contains("qcm") || input.Contains("qm") || input.Contains("qkm") ||
                     input.Contains("ha"))
                mTempTokens.Add(new MetricAreaToken(input));
            else if (input.Contains("mm") || input.Contains("cm") || input.Contains("m") || input.Contains("km"))
                mTempTokens.Add(new MetricLengthToken(input));
            else
                throw new InvalidExpressionException("The input didnt define which system it used.");
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