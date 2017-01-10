using Calculator.Logic.Model;
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
            if (IsCalculateable(operation))
            {
                var constant = new Constant {Value = EvaluatingExpressionVisitor.Evaluate(operation)};
                if (operation.HasParent)
                    operation.Parent.ReplaceChild(operation, constant);
                else mExpression = constant;
            }
            else if (HasAdditiveOperationAsLeft(operation) && operation.Right is Constant) CalculateRightHandAdditionAndSubtractions(operation);
        }
        static bool HasAdditiveOperationAsLeft(IArithmeticOperation operation)
        {
            return operation.Left is Addition || operation.Left is Subtraction;
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
        void VisitOperands(IArithmeticOperation operation)
        {
            operation.Left.Accept(this);
            operation.Right.Accept(this);
        }
    }
}