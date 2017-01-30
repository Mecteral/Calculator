using System.Collections.Generic;
using Calculator.Logic.ArgumentParsing;

namespace Calculator.Logic.Parsing.ConversionTokenizer
{
    public interface IConversionTokenizer
    {
        IEnumerable<IConversionToken> Tokens { get; }
        void Tokenize(string input, ApplicationArguments arg);
    }
}