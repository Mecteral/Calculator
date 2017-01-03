using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Calculator.Model;

namespace Calculator.Logic
{
    public class SimplificationEvaluator : IExpressionVisitor
    {
        int mExpressionCount;
        public IExpression FindSmallesExpressionInEnumerable(IEnumerable<IExpression> expressions)
        {
            if (expressions == null)
                return null;
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

        int CountExpression(IExpression expression)
        {
            mExpressionCount = 0;
            expression.Accept(this);
            return mExpressionCount;
        }
        public void Visit(ParenthesedExpression parenthesed)
        {
            mExpressionCount++;
        }

        public void Visit(Subtraction subtraction)
        {
            mExpressionCount++;
        }

        public void Visit(Multiplication multiplication)
        {
            mExpressionCount++;
        }

        public void Visit(Addition addition)
        {
            mExpressionCount++;
        }

        public void Visit(Constant constant)
        {
            mExpressionCount++;
        }

        public void Visit(Division division)
        {
            mExpressionCount++;
        }

        public void Visit(Variable variable)
        {
            mExpressionCount++;
        }
    }
}
