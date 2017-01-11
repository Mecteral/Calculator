using System;
using Calculator.Logic.Parsing.CalculationTokenizer;

namespace Calculator.Logic.Parsing.ConversionTokenizer
{
    public class ImperialToken : IToken
    {
        public decimal Value { get; }

        public ImperialToken(string asText)
        {

        }
        public void Accept(ITokenVisitor visitor)
        {
            throw new NotImplementedException();
        }
    }
}
