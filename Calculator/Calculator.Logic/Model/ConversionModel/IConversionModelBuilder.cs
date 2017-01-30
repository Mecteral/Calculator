using System.Collections.Generic;
using Calculator.Logic.Parsing.ConversionTokenizer;

namespace Calculator.Logic.Model.ConversionModel
{
    public interface IConversionModelBuilder
    {
        IConversionExpression BuildFrom(IEnumerable<IConversionToken> tokens);
    }
}