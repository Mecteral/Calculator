using System;
using Calculator.Logic.ArgumentParsing;
using Calculator.Logic.Parsing.CalculationTokenizer;

namespace Calculator.Logic.Parsing.ConversionTokenizer
{
    public class MetricLengthToken : AConversionTokens, IConversionToken
    {
        public MetricLengthToken(string asText, ApplicationArguments arg) : base(asText, arg) {}

        public void Accept(IConversionTokenVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
