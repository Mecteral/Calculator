using System.Collections.Generic;
using System.Linq;
using Calculator.Logic.Parsing.CalculationTokenizer;

namespace Calculator.Logic.Parsing.ConversionTokenizer
{
    public class ConversionTokenizer
    {
        readonly List<IToken> mTempTokens = new List<IToken>();
        string mInput;
        string mNumber;
        bool mWasNumber;
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
                }
                else
                {
                    number += c;
                }
            }
            return mTempTokens;
        }

        void AddToken(string input)
        {
            if (input.Contains("ft"))
            {
                mTempTokens.Add(new ImperialToken(input));
            }
            else
            {
                mTempTokens.Add(new MetricToken(input));
            }
        }
        static string RemoveWhitespaceAndEqualSign(string input)
        {
            return input.TakeWhile(c => c != '=').Where(c => !char.IsWhiteSpace(c)).Aggregate("", (current, c) => current + c);
        }
    }
}
