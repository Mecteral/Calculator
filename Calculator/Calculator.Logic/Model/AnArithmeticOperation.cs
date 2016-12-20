using System.Collections.Generic;

namespace Calculator.Logic.Model
{
    /// <summary>
    /// Abstract Class for all Operators ( Multiply , Divide , Minus , Plus )
    /// </summary>
    public abstract class AnArithmeticOperation : IArithmeticOperation
    {
        public IExpression Left { get; set; }
        public IExpression Right { get; set; }
        public IExpression Parent { get; set; }
        public abstract void Accept(IExpressionVisitor visitor);
    }
}