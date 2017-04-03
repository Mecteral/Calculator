using System;
using System.Linq.Expressions;
using Calculator.Logic.Model;
using Calculator.Logic.Utilities;
using Calculator.Model;

namespace Calculator.Logic.Simplifying
{
    /// <summary>
    /// Removes all unnecessary Parentheses
    /// </summary>
    public class ParenthesesSimplifier : IParenthesesSimplifier
    {
        IExpression mExpression;
        public void Visit(Subtraction subtraction)
        {
            RemoveParenthesesIfPossible(subtraction);
        }
        public void Visit(Multiplication multiplication)
        {
            RemoveParenthesesIfPossible(multiplication);
        }
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
            var wrapped = parenthesed.Wrapped as IArithmeticOperation;
            if (wrapped != null) RemoveParenthesesIfPossible(wrapped);
        }
        public void Visit(Constant constant) {}
        public void Visit(Variable variable) {}
        public void Visit(Cosine cosineExpression) {}
        public void Visit(Tangent tangentExpression) {}
        public void Visit(Sinus sinusExpression) {}
        public IExpression Simplify(IExpression input)
        {
            mExpression = ExpressionCloner.Clone(input);
            mExpression.Accept(this);
            return mExpression;
        }
        public void Visit(Power power)
        {
            RemoveParenthesesIfPossible(power);
        }
        void RemoveParenthesesIfPossible(IArithmeticOperation operation)
        {
            VisitOperands(operation);
            if (operation.Left is ParenthesedExpression) {
                ReplaceParenthesedConstantWithConstantIn(operation, o => o.Left);
            }
            else if (operation.Right is ParenthesedExpression) {
                ReplaceParenthesedConstantWithConstantIn(operation, o => o.Right);
            }
        }
        static void ReplaceParenthesedConstantWithConstantIn(
            IArithmeticOperation operation,
            Expression<Func<IArithmeticOperation, IExpression>> propertySelector)
        {
            var parenthesis = (ParenthesedExpression) propertySelector.GetFrom(operation);
            if (parenthesis.Wrapped is Constant) { propertySelector.SetTo(operation, parenthesis.Wrapped); }
        }
        void VisitOperands(IArithmeticOperation operation)
        {
            operation.Left.Accept(this);
            operation.Right.Accept(this);
        }
    }

    public class ParenthesisAroundConstantsRemover : AVisitingTraversingReplacer
    {
        protected override IExpression ReplaceParenthesed(ParenthesedExpression parenthesed)
            => parenthesed.Wrapped is Constant ? parenthesed.Wrapped : parenthesed;
    }
}