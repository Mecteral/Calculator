using System.Collections.Generic;
using System.Data;
using System.Linq;
using Calculator.Logic.ArgumentParsing;

namespace Calculator.Logic.Parsing.CalculationTokenizer
{
    /// <summary>
    /// Takes in a string and returns IEnumerable of ITokens
    /// </summary>
    public class Tokenizer : ITokenizer
    {
        ApplicationArguments mArgs;
        readonly List<IToken> mTempTokens = new List<IToken>();
        string mInput;
        string mNumber;
        string mFunctionString;
        bool mWasNumber;
        public IEnumerable<IToken> Tokens { get; private set; }

        public void Tokenize(string input, ApplicationArguments args)
        {
            mArgs = args;
            mTempTokens.Clear();
            input = RemoveWhitespaces(input);
            mInput = input;
            Tokens = FillTokens();
        }

        IEnumerable<IToken> FillTokens()
        {
            for (var i = 0; i < mInput.Length; i++)
            {
                var c = mInput[i];
                if (char.IsNumber(c) || c == '.' || c == ',' ||
                    (mInput.Length >= i + 1 && c == 'E' && char.IsNumber(mInput[i + 1])) ||
                    (mInput.Length >= i + 2 && c == 'E' && mInput[i + 1] == '-' && char.IsNumber(mInput[i + 2])) ||
                    (i - 1 > 0 && c == '-' && mInput[i - 1] == 'E'))
                {
                    mWasNumber = true;
                    mNumber += c;
                }
                else if (c == '+' || c == '-' || c == '*' || c == '/' || c== '^') AddOperatorToken(c);
                else if (c == '(' || c == ')') AddParenthesisToken(c);
                else if (i + 3 < mInput.Length && c == 'c' && mInput[i + 1] == 'o' && mInput[i + 2] == 's' && mInput[i + 3] == '(' ||
                         (c == 's' && mInput[i + 1] == 'i' && mInput[i + 2] == 'n' && mInput[i + 3] == '(') ||
                         (c == 't' && mInput[i + 1] == 'a' && mInput[i + 2] == 'n' && mInput[i + 3] == '(') ||
                         (i+4 < mInput.Length && c == 's' && mInput[i + 1] == 'q' && mInput[i + 2] == 'r' && mInput[i + 3] == 't' && mInput[i + 4] == '('))
                {
                    i = CreateTokenStringForFunctionTokensAndSetCounterAnew(i);
                    AddFunctionTokens();
                }
                else if (mTempTokens.Count == 0 || (!(mTempTokens.Last() is VariableToken) && char.IsLetter(c)))
                    AddVariableToken(c);
            }
            if (mNumber != null) mTempTokens.Add(new NumberToken(mNumber));
            return mTempTokens;
        }

        static string RemoveWhitespaces(string input)
        {
            return input.Where(c => !char.IsWhiteSpace(c)).Aggregate("", (current, c) => current + c);
        }

        int CreateTokenStringForFunctionTokensAndSetCounterAnew(int i)
        {
            do
            {
                mFunctionString += mInput[i];
                i++;
            } while (mInput[i] != ')');
            return i;
        }

        void AddFunctionTokens()
        {
            AddNumberTokenIfNecessary();
            if (mFunctionString.Contains("cos"))
            {
                mTempTokens.Add(new CosineToken(mFunctionString, mArgs));
            }
            else if (mFunctionString.Contains("sin"))
            {
                mTempTokens.Add(new SinusToken(mFunctionString, mArgs));
            }
            else if (mFunctionString.Contains("tan"))
            {
                mTempTokens.Add(new TangentToken(mFunctionString, mArgs));
            }
            else if (mFunctionString.Contains("sqrt"))
            {
                mTempTokens.Add(new SquareRootToken(mFunctionString));
            }
            mFunctionString = null;
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