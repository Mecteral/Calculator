using System.Collections.Generic;
using Calculator.Logic.ArgumentParsing;

namespace Calculator.Logic.Parsing.CalculationTokenizer
{
    public interface ITokenizer
    {
        IEnumerable<IToken> Tokens { get; }
        void Tokenize(string input, IApplicationArguments args);
    }
}