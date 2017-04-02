using Calculator.Logic.Model;
using Calculator.Model;
using November.MultiDispatch;

namespace Calculator.Logic.Simplifying
{
    public class ZeroBasedMultiplicationRemover : ISimplifier, IExpressionVisitor
    {
        IExpression mExpression;
        IExpression mNeedsToBeSetToZero;
        protected DoubleDispatcher<IExpression> Dispatcher { get; } = new DoubleDispatcher<IExpression>();

        public void Visit(ParenthesedExpression parenthesed)
        {
            parenthesed.Wrapped.Accept(this);
        }

        public void Visit(Subtraction subtraction)
        {
            VisitOperands(subtraction);
        }

        public void Visit(Multiplication multiplication)
        {
            var leftConstant = multiplication.Left as Constant;
            var rightConstant = multiplication.Right as Constant;
            if (leftConstant != null && leftConstant.Value == 0M)
            {
                mNeedsToBeSetToZero = multiplication;
            }
            else if (rightConstant != null && rightConstant.Value == 0M)
            {
                mNeedsToBeSetToZero = multiplication;
            }
            CheckForParentsAndReplaceIfNecessary();
        }

        public void Visit(Addition addition)
        {
            VisitOperands(addition);
        }

        public void Visit(Constant constant) {}

        public void Visit(Division division)
        {
            VisitOperands(division);
        }

        public void Visit(Variable variable) {}

        public void Visit(Cosine cosineExpression) {}

        public void Visit(Tangent tangentExpression) {}

        public void Visit(Sinus sinusExpression) {}

        public void Visit(SquareRoot squareRootExpression) {}

        public void Visit(Square square) {}

        public IExpression Simplify(IExpression input)
        {
            mExpression = ExpressionCloner.Clone(input);
            mExpression.Accept(this);
            return mExpression;
        }

        void CheckForParentsAndReplaceIfNecessary()
        {
            if (mNeedsToBeSetToZero == null) return;
            var replacement = new Constant {Value = 0M};
            if (mNeedsToBeSetToZero.HasParent)
            {
                HandleParentedMultiplication(replacement);
            }
            else
            {
                mExpression = replacement;
            }
            mNeedsToBeSetToZero = null;
        }

        void HandleParentedMultiplication(IExpression replacement)
        {
            if (mNeedsToBeSetToZero.Parent is ParenthesedExpression)
            {
                var parent = (ParenthesedExpression) mNeedsToBeSetToZero.Parent;
                parent.Wrapped = replacement;
            }
            else
            {
                var parent = (IArithmeticOperation) mNeedsToBeSetToZero.Parent;
                if (parent.Left == mNeedsToBeSetToZero)
                {
                    parent.Left = replacement;
                }
                else if (parent.Right == mNeedsToBeSetToZero)
                {
                    parent.Right = replacement;
                }
            }
        }

        void VisitOperands(IArithmeticOperation operation)
        {
            operation.Left.Accept(this);
            operation.Right.Accept(this);
        }
    }
}