using System.Collections.Generic;
using System.Data;
using System.Linq;
using Calculator.Logic.Parsing.CalculationTokenizer;

namespace Calculator.Logic.Parsing.ConversionTokenizer
{
    public class ConversionTokenizer
    {
        readonly List<IToken> mTempTokens = new List<IToken>();
        string mInput;
        public IEnumerable<IToken> Tokens { get; private set; }

        public void Tokenize(string input)
        {
            mTempTokens.Clear();
            mInput = RemoveWhitespaceAndEqualSign(input);
            Tokens = FillTokens();
        }

        IEnumerable<IToken> FillTokens()
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
            if (input.Contains("ft") || input.Contains("in") || input.Contains("yd") || input.Contains("mI"))
                mTempTokens.Add(new ImperialToken(input));
            else if (input.Contains("mm") || input.Contains("cm") || input.Contains("m") || input.Contains("km") || input.Contains("ha"))
                mTempTokens.Add(new MetricToken(input));
            else
                throw new InvalidExpressionException("The input didnt define which system it used.");
        }

        void AddArithmeticToken(char input)
        {
            mTempTokens.Add(new OperatorToken(input));
        }
        static string RemoveWhitespaceAndEqualSign(string input)
        {
            return input.TakeWhile(c => c != '=').Where(c => !char.IsWhiteSpace(c)).Aggregate("", (current, c) => current + c);
        }
    }
}
