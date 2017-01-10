﻿using Calculator.Logic.Model;
using Calculator.Model;

namespace Calculator.Logic
{
    /// <summary>
    /// This Class calculates all possible calculations without variables
    /// </summary>
    public class DirectCalculationSimplifier : IExpressionVisitor
    {
        IExpression mExpression;
        public DirectCalculationSimplifier(IExpression expression)
        {
            mExpression = ExpressionCloner.Clone(expression);
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
            parenthesed.Wrapped.Accept(this);
        }
        public void Visit(Constant constant) {}
        public void Visit(Variable variable) {}
        public IExpression Simplify()
        {
            mExpression.Accept(this);
            return mExpression;
        }
        public static IExpression Simplify(IExpression input)
        {
            var simplifier = new DirectCalculationSimplifier(input);
            return simplifier.Simplify();
        }
        static bool IsCalculateable(IArithmeticOperation operation)
            => operation.Left is Constant && operation.Right is Constant;
        void CalculateResultIfPossible(IArithmeticOperation operation)
        {
            if (IsCalculateable(operation))
            {
                if (operation is Subtraction && operation.Left is Constant && operation.Right is Constant)
                {
                    operation = ChangeSubtractionIfRighthandsideIsNegative(operation);
                }
                var replacement = new Constant {Value = EvaluatingExpressionVisitor.Evaluate(operation)};
                if (operation.HasParent)
                    operation.Parent.ReplaceChild(operation, replacement);
                else mExpression = replacement;
            }
            else VisitOperands(operation);
        }

        IArithmeticOperation ChangeSubtractionIfRighthandsideIsNegative(IArithmeticOperation operation)
        {
            var constant = (Constant)operation.Right;
            if (constant.Value < 0)
            {
                var replaced = new Addition { Left = operation.Left, Right = operation.Right };
                if (operation.HasParent)
                {
                    operation.Parent.ReplaceChild(operation, replaced);
                    return replaced;
                }
                else
                {
                    mExpression = replaced;
                }
            }
            return operation;
        }

        void VisitOperands(IArithmeticOperation operation)
        {
            operation.Left.Accept(this);
            operation.Right.Accept(this);
        }
    }
}