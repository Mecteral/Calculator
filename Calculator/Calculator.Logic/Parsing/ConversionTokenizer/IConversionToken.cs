using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Logic.Parsing.ConversionTokenizer
{
    public interface IConversionToken
    {
        void Accept(IConversionTokenVisitor visitor);
    }
}
