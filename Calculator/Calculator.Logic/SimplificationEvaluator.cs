using System.Collections.Generic;
using System.Data;
using System.Linq;
using Calculator.Model;

namespace Calculator.Logic
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

        public IExpression FindSmallesExpressionInEnumerable(IEnumerable<IExpression> expressions)
        {
            if (expressions == null)
                throw new InvalidExpressionException();
            var result = expressions.First();
            foreach (var expression in expressions)
            {
                var currentExpression = expression;
                if (CountExpression(expression) < CountExpression(result))
                {
                    result = currentExpression;
                }
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