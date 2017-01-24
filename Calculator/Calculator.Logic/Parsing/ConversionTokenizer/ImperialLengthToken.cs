using System;
using Calculator.Logic.Parsing.CalculationTokenizer;
using System.Globalization;
using Calculator.Logic.ArgumentParsing;

namespace Calculator.Logic.Parsing.ConversionTokenizer
{
    public class ImperialLengthToken : AConversionTokens, IConversionToken
    {
        public ImperialLengthToken(string asText, ApplicationArguments arg) : base(asText, arg) {}

        public void Accept(IConversionTokenVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
