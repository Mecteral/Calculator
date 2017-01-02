using System;
using System.Linq.Expressions;
using Calculator.Logic.Model;
using Calculator.Logic.Utilities;
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
            if (operation.Left is ParenthesedExpression) {
                HandleParenthesis(operation, o => o.Left);
            }
            else if (operation.Right is ParenthesedExpression) { HandleParenthesis(operation, o => o.Right); }
            if (IsCalculateable(operation))
            {
                var constant = new Constant {Value = EvaluatingExpressionVisitor.Evaluate(operation)};
                if (operation.HasParent) ReplaceChild(operation, constant);
                else mExpression = constant;
            }
            else if (HasAdditiveOperationAsLeft(operation) && operation.Right is Constant) CalculateRightHandAdditionAndSubtractions(operation);
        }
        static bool HasAdditiveOperationAsLeft(IArithmeticOperation operation)
        {
            return operation.Left is Addition || operation.Left is Subtraction;
        }
        static void ReplaceChild(IExpression oldChild, IExpression newChild)
        {
            var arithmeticOperation = oldChild.Parent as IArithmeticOperation;
            if (arithmeticOperation != null)
            {
                ReplaceOperandIn(arithmeticOperation, oldChild, newChild);
                return;
            }
            var parenthesis = oldChild.Parent as ParenthesedExpression;
            if (null != parenthesis)
            {
                ReplaceWrappedInParenthesis(parenthesis, newChild);
                return;
            }
            throw new InvalidOperationException();
        }
        static void ReplaceWrappedInParenthesis(ParenthesedExpression parent, IExpression newChild)
        {
            parent.Wrapped = newChild;
        }
        static void ReplaceOperandIn(IArithmeticOperation parent, IExpression oldChild, IExpression newChild)
        {
            if (parent.Left == oldChild) parent.Left = newChild;
            else parent.Right = newChild;
        }
        void CalculateRightHandAdditionAndSubtractions(IArithmeticOperation operation)
        {
            var operationLeft = (IArithmeticOperation) operation.Left;
            if (!(operationLeft.Right is Constant)) return;

            if (operation.HasParent)
            {
                var parent = (IArithmeticOperation) operation.Parent;
                parent.Left = operationLeft;
            }
            operation.Left = operationLeft.Right;
            var constant = new Constant {Value = EvaluatingExpressionVisitor.Evaluate(operation)};
            operationLeft.Right = constant;
            if (!operation.HasParent) { mExpression = operationLeft; }
        }
        static void HandleParenthesis(
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
}