using System;
using Calculator.Logic.Parsing.CalculationTokenizer;

namespace Calculator.Logic.Parsing.ConversionTokenizer
{
    public class MetricLengthToken : AConversionTokens, IConversionToken
    {
        public MetricLengthToken(string asText) : base(asText) {}

        public void Accept(IConversionTokenVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
