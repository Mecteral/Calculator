using System.Collections.Generic;
using Calculator.Logic.Parsing;
using Calculator.Logic.Parsing.CalculationTokenizer;
using Calculator.Model;

namespace Calculator.Logic
{
    public interface IModelBuilder
    {
        IExpression BuildFrom(IEnumerable<IToken> tokens);
    }
}