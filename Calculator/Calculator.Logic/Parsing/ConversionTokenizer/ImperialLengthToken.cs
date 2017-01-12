using System;
using Calculator.Logic.Parsing.CalculationTokenizer;
using System.Globalization;

namespace Calculator.Logic.Parsing.ConversionTokenizer
{
    public class ImperialLengthToken : AConversionTokens, IConversionToken
    {
        public ImperialLengthToken(string asText) : base(asText) {}

        public void Accept(IConversionTokenVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
