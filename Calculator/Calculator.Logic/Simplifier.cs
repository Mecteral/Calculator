using Calculator.Logic.Model;

namespace Calculator.Logic
{
    public class Simplifier : IExpressionVisitor
    {
        public static IExpression DirectCalculationExpression { get; set; }
        public static IExpression OriginalExpression { get; set; }
        public static IExpression Simplify(IExpression input)
        {
            var directSimplifier = DirectCalculationSimplifier.Simplify(input);
            OriginalExpression = input;
            DirectCalculationExpression = directSimplifier;
            return directSimplifier;
        }

        public void Visit(ParenthesedExpression parenthesed)
        {
            throw new System.NotImplementedException();
        }

        public void Visit(Subtraction subtraction)
        {
            throw new System.NotImplementedException();
        }

        public void Visit(Multiplication multiplication)
        {
            throw new System.NotImplementedException();
        }

        public void Visit(Addition addition)
        {
            throw new System.NotImplementedException();
        }

        public void Visit(Constant constant)
        {
            throw new System.NotImplementedException();
        }

        public void Visit(Division division)
        {
            throw new System.NotImplementedException();
        }

        public void Visit(Variable variable)
        {
            throw new System.NotImplementedException();
        }
    }
}