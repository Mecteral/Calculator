using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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

        public void Visit(Division division)
        {
            CalculateResultIfPossible(division);
        }

        public void Visit(ParenthesedExpression parenthesed)
        {
            if (parenthesed.Wrapped is IArithmeticOperation)
            {
                CalculateResultIfPossible((IArithmeticOperation)parenthesed.Wrapped);
            }
        }

        public void Visit(Constant constant)
        {
        }

        public void Visit(Variable variable)
        {
        }

        static bool IsCalculateable(IArithmeticOperation operation) => (operation.Left is Constant && operation.Right is Constant);

        void CalculateResultIfPossible(IArithmeticOperation operation)
        {
            operation.Left.Accept(this);
            operation.Right.Accept(this);
            if (IsCalculateable(operation))
            {
                var constant = new Constant {Value = EvaluatingExpressionVisitor.Evaluate(operation)};
                if (operation.Parent != null)
                {
                    if (operation.Parent is IArithmeticOperation)
                    {
                        var parent = (IArithmeticOperation)operation.Parent;
                        if (parent.Left == operation)
                            parent.Left = constant;
                        else
                            parent.Right = constant;
                    }
                    else
                    {
                        var parent = (ParenthesedExpression) operation.Parent;
                        parent.Wrapped = constant;
                    }
                }
                else
                {
                    mCurrent = constant;
                }
            }
        }
    }
}
