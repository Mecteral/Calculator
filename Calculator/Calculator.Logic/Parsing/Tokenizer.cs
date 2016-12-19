using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Calculator.Logic.Parsing
{
    /// <summary>
    /// Takes in a string and returns IEnumerable of ITokens
    /// </summary>
    public class Tokenizer
    {
        readonly string mInput;
        public IEnumerable<IToken> Tokens { get; private set; }

        public Tokenizer(string input)
        {
            mInput = input;
        }

        public void Tokenize()
        {
            Tokens = FillTokens();
        }
        IEnumerable<IToken> FillTokens()
        {
            var tempTokens = new List<IToken>();
            var wasNumber = false;
            string number = null;
            foreach (var c in mInput)
            {
                if (c == '+' || c == '-' || c == '*' || c == '/')
                {
                    if (wasNumber)
                    {
                        tempTokens.Add(new NumberToken(number));
                        number = null;
                        wasNumber = false;
                    }
                    tempTokens.Add(new OperatorToken(c.ToString()));
                }
                else if (c == '(' || c == ')')
                {
                    if (wasNumber)
                    {
                        tempTokens.Add(new NumberToken(number));
                        number = null;
                        wasNumber = false;
                    }
                    tempTokens.Add(new ParenthesesToken(c.ToString()));
                }
                else if (char.IsLetter(c))
                {
                    if (wasNumber)
                    {
                        tempTokens.Add(new NumberToken(number));
                        number = null;
                        wasNumber = false;
                    }
                    if (tempTokens.Count == 0 || !(tempTokens.Last() is NumberToken))
                        tempTokens.Add(new NumberToken("1"));
                    tempTokens.Add(new OperatorToken("*"));
                    tempTokens.Add(new VariableToken(c.ToString()));
                }
                else if (char.IsNumber(c) || c == '.' || c == ',')
                {
                    wasNumber = true;
                    number += c;
                }
            }
            if (number != null)
            {
                tempTokens.Add(new NumberToken(number));
            }
            return tempTokens;
        }
    }
}
