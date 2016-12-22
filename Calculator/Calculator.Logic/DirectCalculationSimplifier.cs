using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Calculator.Logic.Model;

namespace Calculator.Logic
{
    public class DirectCalculationSimplifier : IExpressionVisitor
    {
        IExpression mExpression;

        public DirectCalculationSimplifier(IExpression expression)
        {
            mExpression = ExpressionCloner.Clone(expression);
        }

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
                CalculateResultIfPossible((IArithmeticOperation)parenthesed.Wrapped);
        }

        public void Visit(Constant constant) { }

        public void Visit(Variable variable) { }

        static bool IsCalculateable(IArithmeticOperation operation)
            => operation.Left is Constant && operation.Right is Constant;

        void CalculateResultIfPossible(IArithmeticOperation operation)
        {
            LoopthroughVisitors(operation);
            if (operation.Left is ParenthesedExpression)
                HandleParenthesesExpressionOnTheLeftSide(operation);
            else if (operation.Right is ParenthesedExpression)
                HandleParenthesesExpressionOnTheRightSide(operation);
            if (IsCalculateable(operation))
            {
                var constant = new Constant { Value = EvaluatingExpressionVisitor.Evaluate(operation) };
                if (operation.Parent != null)
                    HandleOperationWithParent(operation, constant);
                else
                    mExpression = constant;
            }
            else if ((operation.Left is Addition || operation.Left is Subtraction) && operation.Right is Constant)
                CalculateRightHandAdditionAndSubtractions(operation);
        }

        static void HandleOperationWithParent(IArithmeticOperation operation, Constant constant)
        {
            if (operation.Parent is IArithmeticOperation)
                HandleParentAsArithmeticOperation(operation, constant);
            else
            {
                var parent = (ParenthesedExpression)operation.Parent;
                parent.Wrapped = constant;
            }
        }

        static void HandleParentAsArithmeticOperation(IArithmeticOperation operation, Constant constant)
        {
            var parent = (IArithmeticOperation)operation.Parent;
            if (parent.Left == operation)
                parent.Left = constant;
            else
                parent.Right = constant;
        }

        void CalculateRightHandAdditionAndSubtractions(IArithmeticOperation operation)
        {
            var operationLeft = (IArithmeticOperation)operation.Left;
            if (operationLeft.Right is Constant)
            {
                if (operation.Parent != null)
                {
                    var parent = (IArithmeticOperation)operation.Parent;
                    parent.Left = operationLeft;
                }
                operation.Left.Parent = operation.Parent;
                operation.Left = operationLeft.Right;
                var constant = new Constant { Value = EvaluatingExpressionVisitor.Evaluate(operation) };
                operationLeft.Right = constant;
                operationLeft.Right.Parent = operationLeft;
                if (operation.Parent == null)
                {
                    mExpression = operationLeft;
                }
            }
        }

        static void HandleParenthesesExpressionOnTheLeftSide(IArithmeticOperation operation)
        {
            var parenthesis = (ParenthesedExpression)operation.Left;
            if (parenthesis.Wrapped is Constant)
            {
                operation.Left = parenthesis.Wrapped;
            }
        }

        static void HandleParenthesesExpressionOnTheRightSide(IArithmeticOperation operation)
        {
            var parenthesis = (ParenthesedExpression)operation.Right;
            if (parenthesis.Wrapped is Constant)
            {
                operation.Right = parenthesis.Wrapped;
            }
        }

        void LoopthroughVisitors(IArithmeticOperation operation)
        {
            operation.Left.Accept(this);
            operation.Right.Accept(this);
        }
    }
}
