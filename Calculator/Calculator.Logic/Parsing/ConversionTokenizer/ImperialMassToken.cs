using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Calculator.Logic.ArgumentParsing;

namespace Calculator.Logic.Parsing.ConversionTokenizer
{
    public class ImperialMassToken : AConversionTokens, IConversionToken
    {
        public ImperialMassToken(string asText, ApplicationArguments arg) : base(asText, arg) {}
        public void Accept(IConversionTokenVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
