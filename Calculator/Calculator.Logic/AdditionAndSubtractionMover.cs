using Calculator.Logic.Model;
using Calculator.Model;

namespace Calculator.Logic
{
    public class AdditionAndSubtractionMover : IExpressionVisitor, ISimplifier
    {
        static IExpression sMovedExpression;
        bool mIsRight;
        bool mWasChanged;
        public void Visit(ParenthesedExpression parenthesed)
        {
            parenthesed.Wrapped.Accept(this);
        }
        public void Visit(Subtraction subtraction)
        {
            CheckIfMoveIsAvailable(subtraction);
            VisitOperands(subtraction);
        }
        public void Visit(Multiplication multiplication)
        {
            VisitOperands(multiplication);
        }
        public void Visit(Addition addition)
        {
            CheckIfMoveIsAvailable(addition);
            VisitOperands(addition);
        }
        public void Visit(Constant constant) {}
        public void Visit(Division division)
        {
            VisitOperands(division);
        }
        public void Visit(Variable variable) {}
        public IExpression Simplify(IExpression input)
        {
            mWasChanged = false;
            sMovedExpression = ExpressionCloner.Clone(input);
            MoveAdditionsOrSubtractions(sMovedExpression);
            return sMovedExpression;
        }
        static void MoveAdditionsOrSubtractions(IExpression expression)
        {
            var mover = new AdditionAndSubtractionMover();
            expression.Accept(mover);
        }
        void CheckIfMoveIsAvailable(IArithmeticOperation operation)
        {
            if (operation.Right is Constant && (operation.Left is Addition || operation.Left is Subtraction)) {
                MakeMove(operation, FindMoveableExpression((IArithmeticOperation) operation.Left));
            }
        }
        IArithmeticOperation FindMoveableExpression(IArithmeticOperation operation)
        {
            while (!mWasChanged)
            {
                var current = operation;
                if (operation.Right is Multiplication && operation.Left is Constant)
                {
                    var constant = (Constant) operation.Left;
                    if (constant.Value == 0)
                    {
                        break;
                    }
                }
                if (operation.Right is Constant && !(operation.Left is Constant))
                {
                    mIsRight = true;
                    return current;
                }
                if (operation.Left is Constant && !(operation.Right is Constant))
                {
                    mIsRight = false;
                    return current;
                }
                if (operation.Left is Addition || operation.Left is Subtraction)
                {
                    operation = (IArithmeticOperation) current.Left;
                    continue;
                }
                if (operation.Right is Addition || operation.Right is Subtraction)
                {
                    operation = (IArithmeticOperation) current.Right;
                    continue;
                }
                break;
            }
            return null;
        }
        void MakeMove(IArithmeticOperation operation, IArithmeticOperation chainedOperation)
        {
            if (chainedOperation == null) return;
            if (operation is Addition)
            {
                HandleAdditionInAddition(operation, chainedOperation);
                HandleSubtractionInAddition(operation, chainedOperation);
            }
            if (operation is Subtraction)
            {
                HandelSubtractionInSubtraction(operation, chainedOperation);
                HandelAdditionInSubtraction(operation, chainedOperation);
            }
        }
        void HandleSubtractionInAddition(IArithmeticOperation operation, IArithmeticOperation chainedOperation)
        {
            if (chainedOperation is Subtraction)
            {
                var parent = (IArithmeticOperation) chainedOperation.Parent;
                if (mIsRight)
                {
                    parent.Left = chainedOperation.Left;
                    operation.Right = new Addition
                    {
                        Left = new Subtraction {Left = new Constant {Value = 0}, Right = chainedOperation.Right},
                        Right = operation.Right
                    };
                }
                else
                {
                    parent.Left = new Subtraction {Left = new Constant {Value = 0}, Right = chainedOperation.Right};
                    operation.Right = new Addition {Left = chainedOperation.Left, Right = operation.Right};
                }
            }
        }
        void HandleAdditionInAddition(IArithmeticOperation operation, IArithmeticOperation chainedOperation)
        {
            if (chainedOperation is Addition)
            {
                var parent = (IArithmeticOperation) chainedOperation.Parent;
                if (mIsRight)
                {
                    parent.Left = chainedOperation.Left;
                    operation.Right = new Addition {Left = chainedOperation.Right, Right = operation.Right};
                }
                else
                {
                    parent.Left = chainedOperation.Right;
                    operation.Right = new Addition {Left = chainedOperation.Left, Right = operation.Right};
                }
            }
        }
        void HandelSubtractionInSubtraction(IArithmeticOperation operation, IArithmeticOperation chainedOperation)
        {
            if (chainedOperation is Subtraction)
            {
                var parent = (IArithmeticOperation) chainedOperation.Parent;
                if (mIsRight)
                {
                    parent.Left = chainedOperation.Left;
                    operation.Right = new Subtraction
                    {
                        Left = new Subtraction {Left = new Constant {Value = 0}, Right = chainedOperation.Right},
                        Right = operation.Right
                    };
                    mWasChanged = true;
                }
                else
                {
                    parent.Left = chainedOperation.Right;
                    operation.Right = new Subtraction {Left = chainedOperation.Left, Right = operation.Right};
                    mWasChanged = true;
                }
            }
        }
        void HandelAdditionInSubtraction(IArithmeticOperation operation, IArithmeticOperation chainedOperation)
        {
            if (chainedOperation is Addition)
            {
                var parent = (IArithmeticOperation) chainedOperation.Parent;
                if (mIsRight)
                {
                    parent.Left = chainedOperation.Left;
                    var replacement = new Addition
                    {
                        Left = operation.Left,
                        Right = new Subtraction {Left = chainedOperation.Right, Right = operation.Right}
                    };
                    CheckForParent(operation, replacement);
                    mWasChanged = true;
                }
                else
                {
                    parent.Left = chainedOperation.Right;
                    var replacement = new Addition
                    {
                        Left = operation.Left,
                        Right = new Subtraction {Left = chainedOperation.Left, Right = operation.Right}
                    };
                    CheckForParent(operation, replacement);
                    mWasChanged = true;
                }
            }
        }
        static void CheckForParent(IArithmeticOperation operation, Addition replacement)
        {
            if (!operation.HasParent) sMovedExpression = replacement;
            else
            {
                HandleParent(operation, replacement);
            }
        }
        static void HandleParent(IArithmeticOperation operation, IExpression replacement)
        {
            if (operation.Parent is IArithmeticOperation)
            {
                var operationParent = (IArithmeticOperation) operation.Parent;
                if (operationParent.Left.Equals(operation)) {
                    operationParent.Left = replacement;
                }
                else
                {
                    operationParent.Right = replacement;
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