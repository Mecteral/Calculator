using System.Collections.Generic;
using Calculator.Model;

namespace Calculator.Logic.Simplifying
{
    public interface IDistributeLawHelper
    {
        List<IExpression> GetAllUnderLyingMultipliableExpressions(IExpression multiplicatorRoot);
    }
}