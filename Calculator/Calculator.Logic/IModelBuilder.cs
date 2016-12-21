using System.Collections.Generic;
using Calculator.Logic.Model;
using Calculator.Logic.Parsing;

namespace Calculator.Logic
{
    public interface IModelBuilder {
        IExpression BuildFrom(IEnumerable<IToken> tokens);
    }
}