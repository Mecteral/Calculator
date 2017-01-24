using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Calculator.Logic.ArgumentParsing;

namespace Calculator.Logic.Parsing.ConversionTokenizer
{
    public class MetricMassToken : AConversionTokens, IConversionToken
    {
        public MetricMassToken(string asText, ApplicationArguments arg) : base(asText, arg) {}
        public void Accept(IConversionTokenVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
