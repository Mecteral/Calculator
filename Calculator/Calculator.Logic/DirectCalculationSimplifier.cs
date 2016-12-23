using System;
using System.Linq.Expressions;
using Calculator.Logic.Model;
using Calculator.Logic.Utilities;
using Calculator.Model;

namespace Calculator.Logic
{
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
            if (parenthesed.Wrapped is IArithmeticOperation) CalculateResultIfPossible((IArithmeticOperation) parenthesed.Wrapped);
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
            VisitOperands(operation);
            if (operation.Left is ParenthesedExpression)
            {
                HandleParenthesis(operation, o => o.Left);
            }
            else if (operation.Right is ParenthesedExpression)
            {
                HandleParenthesis(operation, o => o.Right);
            }
            if (IsCalculateable(operation))
            {
                var constant = new Constant {Value = EvaluatingExpressionVisitor.Evaluate(operation)};
                if (operation.Parent != null) HandleOperationWithParent(operation, constant);
                else mExpression = constant;
            }
            else if ((operation.Left is Addition || operation.Left is Subtraction) && operation.Right is Constant) CalculateRightHandAdditionAndSubtractions(operation);
        }
        static void HandleOperationWithParent(IArithmeticOperation operation, Constant constant)
        {
            if (operation.Parent is IArithmeticOperation) HandleParentAsArithmeticOperation(operation, constant);
            else
            {
                var parent = (ParenthesedExpression) operation.Parent;
                parent.Wrapped = constant;
            }
        }
        static void HandleParentAsArithmeticOperation(IArithmeticOperation operation, Constant constant)
        {
            var parent = (IArithmeticOperation) operation.Parent;
            if (parent.Left == operation) parent.Left = constant;
            else parent.Right = constant;
        }
        void CalculateRightHandAdditionAndSubtractions(IArithmeticOperation operation)
        {
            var operationLeft = (IArithmeticOperation) operation.Left;
            if (operationLeft.Right is Constant)
            {
                if (operation.Parent != null)
                {
                    var parent = (IArithmeticOperation) operation.Parent;
                    parent.Left = operationLeft;
                }
                operation.Left = operationLeft.Right;
                var constant = new Constant {Value = EvaluatingExpressionVisitor.Evaluate(operation)};
                operationLeft.Right = constant;
                if (operation.Parent == null) { mExpression = operationLeft; }
            }
        }
        static void HandleParenthesis(
            IArithmeticOperation operation,
            Expression<Func<IArithmeticOperation, IExpression>>  propertySelector)
        {
            var parenthesis = (ParenthesedExpression)propertySelector.GetFrom(operation);
            if (parenthesis.Wrapped is Constant)
            {
                propertySelector.SetTo(operation, parenthesis.Wrapped);
            }
        }
        void VisitOperands(IArithmeticOperation operation)
        {
            operation.Left.Accept(this);
            operation.Right.Accept(this);
        }
    }
}