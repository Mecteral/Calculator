using System;
using System.Linq.Expressions;
using Calculator.Logic.Model;
using Calculator.Logic.Utilities;
using Calculator.Model;

namespace Calculator.Logic
{
    /// <summary>
    /// Removes all unnecessary Parentheses
    /// </summary>
    public class ParenthesesSimplifier : IExpressionVisitor
    {
        readonly IExpression mExpression;

        public ParenthesesSimplifier(IExpression expression)
        {
            mExpression = ExpressionCloner.Clone(expression);
        }

        public void Visit(Subtraction subtraction)
        {
            RemoveParenthesesIfPossible(subtraction);
        }

        public void Visit(Multiplication multiplication) {}

        public void Visit(Addition addition)
        {
            RemoveParenthesesIfPossible(addition);
        }

        public void Visit(Division division)
        {
            RemoveParenthesesIfPossible(division);
        }

        public void Visit(ParenthesedExpression parenthesed)
        {
            if (parenthesed.Wrapped is IArithmeticOperation)
            RemoveParenthesesIfPossible((IArithmeticOperation) parenthesed.Wrapped);
        }

        void RemoveParenthesesIfPossible(IArithmeticOperation operation)
        {
            VisitOperands(operation);
            if (operation.Left is ParenthesedExpression) {
                HandleParenthesis(operation, o => o.Left);
            }
            else if (operation.Right is ParenthesedExpression) { HandleParenthesis(operation, o => o.Right); }
        }

        public void Visit(Constant constant) {}
        public void Visit(Variable variable) {}

        public IExpression Simplify()
        {
            mExpression.Accept(this);
            return mExpression;
        }

        public  IExpression Simplify(IExpression input)
        {
            var simplifier = new ParenthesesSimplifier(input);
            return simplifier.Simplify();
        }
        static void HandleParenthesis(
            IArithmeticOperation operation,
            Expression<Func<IArithmeticOperation, IExpression>> propertySelector)
        {
            var parenthesis = (ParenthesedExpression)propertySelector.GetFrom(operation);
            if (parenthesis.Wrapped is Constant) { propertySelector.SetTo(operation, parenthesis.Wrapped); }
        }
        void VisitOperands(IArithmeticOperation operation)
        {
            operation.Left.Accept(this);
            operation.Right.Accept(this);
        }
    }
}