using System.Collections.Generic;
using System.Data;
using System.Linq;
using Calculator.Model;

namespace Calculator.Logic.Simplifying
{
    public class SimplificationEvaluator : IExpressionVisitor
    {
        int mExpressionCount;
        public void Visit(ParenthesedExpression parenthesed)
        {
            parenthesed.Wrapped.Accept(this);
            mExpressionCount++;
        }
        public void Visit(Subtraction subtraction)
        {
            VisitOperands(subtraction);
            mExpressionCount++;
        }
        public void Visit(Multiplication multiplication)
        {
            VisitOperands(multiplication);
            mExpressionCount++;
        }
        public void Visit(Addition addition)
        {
            VisitOperands(addition);
            mExpressionCount++;
        }
        public void Visit(Constant constant)
        {
            mExpressionCount++;
        }
        public void Visit(Division division)
        {
            VisitOperands(division);
            mExpressionCount++;
        }
        public void Visit(Variable variable)
        {
            mExpressionCount++;
        }

        public void Visit(CosineExpression cosineExpression)
        {
            throw new System.NotImplementedException();
        }

        public void Visit(TangentExpression tangentExpression)
        {
            throw new System.NotImplementedException();
        }

        public void Visit(SinusExpression sinusExpression)
        {
            throw new System.NotImplementedException();
        }

        public IExpression FindSmallesExpressionInEnumerable(IEnumerable<IExpression> expressions)
        {
            if (expressions == null) throw new InvalidExpressionException();
            var frozen = expressions as IExpression[] ?? expressions.ToArray();
            var result = frozen.First();
            foreach (var expression in frozen)
            {
                var currentExpression = expression;
                if (CountExpression(expression) < CountExpression(result)) { result = currentExpression; }
            }
            return result;
        }
        public int CountExpression(IExpression expression)
        {
            mExpressionCount = 0;
            expression.Accept(this);
            return mExpressionCount;
        }
        void VisitOperands(IArithmeticOperation operation)
        {
            operation.Left.Accept(this);
            operation.Right.Accept(this);
        }
    }
}