using System.Collections.Generic;

namespace Calculator.Logic.Model
{
    public interface IExpression
    {
        IExpression Parent { get; set; }
        void Accept(IExpressionVisitor visitor);
    }
}