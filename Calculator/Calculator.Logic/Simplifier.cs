using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Calculator.Logic.Model;

namespace Calculator.Logic
{
    public class Simplifier : IExpressionVisitor
    {
        readonly IExpression mExpression;
        IExpression mCurrent;
        IEnumerable<IExpression> mToEvaluate = new List<IExpression>();
        public Simplifier(IExpression expression)
        {
            mExpression = expression;
        }

        public string Simplify()
        {
            mCurrent = mExpression;
            mCurrent.Accept(this);
            return FormattingExpressionVisitor.Format(mCurrent);
        }
        static bool IsVariable(IExpression expression) => expression is Variable;
        public void Visit(ParenthesedExpression parenthesed)
        {
        }

        public void Visit(Subtraction subtraction)
        {
            CalculateResultIfPossible(subtraction);
        }

        public void Visit(Multiplication multiplication)
        {
            CalculateResultIfPossible(multiplication);
        }

        public void Visit(Addition addition)
        {
            CalculateResultIfPossible(addition);
        }

        public void Visit(Constant constant)
        {
        }

        public void Visit(Division division)
        {
            CalculateResultIfPossible(division);
        }

        public void Visit(Variable variable)
        {
        }

        static bool IsCalculateable(IArithmeticOperation operation) => (operation.Left is Constant && operation.Right is Constant) ? true : false;

        static void CalculateResultIfPossible(IArithmeticOperation operation)
        {
            if (IsCalculateable(operation))
            {
                var result = EvaluatingExpressionVisitor.Evaluate(operation);
            }
        }
    }
}
