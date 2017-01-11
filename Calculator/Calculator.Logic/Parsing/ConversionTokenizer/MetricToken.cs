using System;
using Calculator.Logic.Parsing.CalculationTokenizer;

namespace Calculator.Logic.Parsing.ConversionTokenizer
{
    public class MetricToken : IToken
    {
        public decimal Value { get; }

        public MetricToken(string asText)
        {
            
        }
        public void Accept(ITokenVisitor visitor)
        {
            throw new NotImplementedException();
        }
    }
}
