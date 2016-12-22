using System;
using Calculator.Model;

namespace Calculator.Logic
{
    public class Simplifier : IExpressionVisitor
    {
        public static IExpression SimplifiedCalculationExpression { get; set; }
        public static IExpression DirectCalculationExpression { get; set; }
        public static IExpression OriginalExpression { get; set; }
        public void Visit(ParenthesedExpression parenthesed)
        {
            throw new NotImplementedException();
        }
        public void Visit(Subtraction subtraction)
        {
            throw new NotImplementedException();
        }
        public void Visit(Multiplication multiplication)
        {
            throw new NotImplementedException();
        }
        public void Visit(Addition addition)
        {
            throw new NotImplementedException();
        }
        public void Visit(Constant constant)
        {
            throw new NotImplementedException();
        }
        public void Visit(Division division)
        {
            throw new NotImplementedException();
        }
        public void Visit(Variable variable)
        {
            throw new NotImplementedException();
        }
        public static IExpression Simplify(IExpression input)
        {
            OriginalExpression = input;
            DirectCalculationExpression = DirectCalculationSimplifier.Simplify(input);
            return DirectCalculationExpression;
        }
    }
}