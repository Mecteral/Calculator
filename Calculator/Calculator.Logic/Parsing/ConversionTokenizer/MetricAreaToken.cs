using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Calculator.Logic.ArgumentParsing;
using Calculator.Logic.Parsing.CalculationTokenizer;

namespace Calculator.Logic.Parsing.ConversionTokenizer
{
    public class MetricAreaToken : AConversionTokens, IConversionToken
    {
        public MetricAreaToken(string asText, ApplicationArguments arg) : base(asText, arg) {}
        public void Accept(IConversionTokenVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
