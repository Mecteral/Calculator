using System.Collections.Generic;
using System.Linq;

namespace Calculator.Logic.Parsing.CalculationTokenizer
{
    /// <summary>
    /// Takes in a string and returns IEnumerable of ITokens
    /// </summary>
    public class Tokenizer : ITokenizer
    {
        readonly List<IToken> mTempTokens = new List<IToken>();
        string mInput;
        string mNumber;
        bool mWasNumber;
        public IEnumerable<IToken> Tokens { get; private set; }
        public void Tokenize(string input)
        {
            mTempTokens.Clear();
            mInput = input;
            Tokens = FillTokens();
        }
        IEnumerable<IToken> FillTokens()
        {
            for (var i = 0; i < mInput.Length; i++)
            {
                var c = mInput[i];
                if (char.IsNumber(c) || c == '.' || c == ',' || (mInput.Length >= i + 1 && c == 'E' && char.IsNumber(mInput[i + 1])) || (mInput.Length >= i + 2 && c == 'E' && mInput[i + 1] == '-' && char.IsNumber(mInput[i + 2])) || (i-1 > 0 && c == '-' && mInput[i - 1] == 'E'))
                {
                    mWasNumber = true;
                    mNumber += c;
                }
                else if (c == '+' || c == '-' || c == '*' || c == '/') AddOperatorToken(c);
                else if (c == '(' || c == ')') AddParenthesisToken(c);
                else if (char.IsLetter(c)) AddVariableToken(c);
            }
            if (mNumber != null) mTempTokens.Add(new NumberToken(mNumber));
            return mTempTokens;
        }
        void AddNumberTokenIfNecessary()
        {
            if (mWasNumber)
            {
                mTempTokens.Add(new NumberToken(mNumber));
                mNumber = null;
                mWasNumber = false;
            }
        }
        void AddOperatorToken(char c)
        {
            AddNumberTokenIfNecessary();
            mTempTokens.Add(new OperatorToken(c));
        }
        void AddParenthesisToken(char c)
        {
            AddNumberTokenIfNecessary();
            mTempTokens.Add(new ParenthesesToken(c));
        }
        void AddVariableToken(char c)
        {
            AddNumberTokenIfNecessary();
            if (mTempTokens.Count == 0 || !(mTempTokens.Last() is NumberToken)) mTempTokens.Add(new NumberToken("1"));
            mTempTokens.Add(new OperatorToken('*'));
            mTempTokens.Add(new VariableToken(c));
        }
    }
}