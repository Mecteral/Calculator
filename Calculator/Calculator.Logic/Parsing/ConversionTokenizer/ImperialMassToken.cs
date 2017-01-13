using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Logic.Parsing.ConversionTokenizer
{
    public class ImperialMassToken : AConversionTokens, IConversionToken
    {
        public ImperialMassToken(string asText) : base(asText) {}
        public void Accept(IConversionTokenVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
