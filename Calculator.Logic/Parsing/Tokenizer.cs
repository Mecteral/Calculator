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
        static readonly char[] sCharactersNeedingWhitespace = {'+', '-', '*', '/', '(', ')'};
        readonly string mInput;

        public Tokenizer(string input)
        {
            mInput = input;
        }

        public IEnumerable<IToken> Tokens { get; private set; }

        public void Tokenize()
        {
            Tokens = SplitString(mInput).Select(GetTokenFor).ToList();
        }

        static IToken GetTokenFor(string text)
        {
            IToken result;
            switch (text)
            {
                case "(":
                case ")":
                    result = new ParenthesesToken(text);
                    break;
                case "*":
                case "/":
                case "+":
                case "-":
                    result = new OperatorToken(text);
                    break;
                default:
                    if (text.Any(char.IsLetter))
                        result = new VariableToken(text);
                    else
                        result = new NumberToken(text);
                    break;
            }
            return result;
        }
        static IEnumerable<string> WithoutEmptyEntries(IEnumerable<string> strings)
            => strings.Where(t => !string.IsNullOrWhiteSpace(t));

        static IEnumerable<string> SplitString(string input)
        {
            input = AddWhitespaceForSplit(input);
            const string pattern = @"\s+";
            return WithoutEmptyEntries(Regex.Split(input, pattern));
        }

        static string AddWhitespaceForSplit(string input)
            => string.Join(string.Empty, input.Select(SurroundWithSpacesIfNecessary));

        static string SurroundWithSpacesIfNecessary(char c)
            => sCharactersNeedingWhitespace.Contains(c) ? $" {c} " : c.ToString();
    }
}
