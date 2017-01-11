using System.Collections.Generic;

namespace Calculator.Logic.Parsing
{
    public interface ITokenizer
    {
        IEnumerable<IToken> Tokens { get; }
        void Tokenize(string input);
    }
}