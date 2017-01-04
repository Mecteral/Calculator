using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Calculator.Logic.Model;
using Calculator.Model;

namespace Calculator.Logic
{
    public class AdditionAndSubtractionMover : IExpressionVisitor
    {
        bool isRight;
        public void MoveAdditionsOrSubtractions(IExpression expression)
        {
            expression.Accept(this);
        }

        void CheckIfMoveIsAvailable(IArithmeticOperation operation)
        {
            if (operation.Right is Constant && operation.Left is Addition || operation.Left is Subtraction)
            {
                MakeMove(operation, FindMoveableExpression((IArithmeticOperation)operation.Left));
            }
        }

        IArithmeticOperation FindMoveableExpression(IArithmeticOperation operation)
        {
            while (true)
            {
                var current = operation;
                if (operation.Right is Constant && !(operation.Left is Constant))
                {
                    isRight = true;
                    return current;
                }
                else if (operation.Left is Constant && !(operation.Right is Constant))
                {
                    isRight = false;
                    return current;
                }
                else if (operation.Left is Addition || operation.Left is Subtraction)
                {
                    operation = (IArithmeticOperation) current.Left;
                    continue;
                }
                else if (operation.Right is Addition || operation.Right is Subtraction)
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
                HandleSubtractionInAddition(operation,chainedOperation);
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
                var parent = (IArithmeticOperation)chainedOperation.Parent;
                if (isRight)
                {
                    parent.Left = chainedOperation.Left;
                    operation.Right = new Addition { Left = new Subtraction { Left = new Constant { Value = 0 }, Right = chainedOperation.Right }, Right = operation.Right };
                }
                else
                {
                    parent.Left = chainedOperation.Right;
                    operation.Right = new Addition { Left = new Subtraction { Left = new Constant { Value = 0 }, Right = chainedOperation.Left }, Right = operation.Right };
                }
            }
        }

        void HandleAdditionInAddition(IArithmeticOperation operation, IArithmeticOperation chainedOperation)
        {
            if (chainedOperation is Addition)
            {
                var parent = (IArithmeticOperation)chainedOperation.Parent;
                if (isRight)
                {
                    parent.Left = chainedOperation.Left;
                    operation.Right = new Addition { Left = chainedOperation.Right, Right = operation.Right };
                }
                else
                {
                    parent.Left = chainedOperation.Right;
                    operation.Right = new Addition { Left = chainedOperation.Left, Right = operation.Right };
                }
            }
        }
        void HandelSubtractionInSubtraction(IArithmeticOperation operation, IArithmeticOperation chainedOperation)
        {
            if (chainedOperation is Subtraction)
            {
                var parent = (IArithmeticOperation)chainedOperation.Parent;
                if (isRight)
                {
                    parent.Left = chainedOperation.Left;
                    operation.Right = new Subtraction { Left = chainedOperation.Right, Right = operation.Right };
                }
                else
                {
                    parent.Left = chainedOperation.Right;
                    operation.Right = new Subtraction { Left = chainedOperation.Left, Right = operation.Right };
                }
            }
        }

        void HandelAdditionInSubtraction(IArithmeticOperation operation, IArithmeticOperation chainedOperation)
        {
            if (chainedOperation is Addition)
            {
                var parent = (IArithmeticOperation)chainedOperation.Parent;
                if (isRight)
                {
                    parent.Left = chainedOperation.Left;
                    operation.Right = new Subtraction { Left = new Addition { Left = new Constant { Value = 0 }, Right = chainedOperation.Right }, Right = operation.Right };
                }
                else
                {
                    parent.Left = chainedOperation.Right;
                    operation.Right = new Subtraction { Left = new Addition { Left = new Constant { Value = 0 }, Right = chainedOperation.Left }, Right = operation.Right };
                }
            }
        }

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

        public void Visit(Constant constant)
        {
        }

        public void Visit(Division division)
        {
            VisitOperands(division);
        }

        public void Visit(Variable variable)
        {
        }

        void VisitOperands(IArithmeticOperation operation)
        {
            operation.Left.Accept(this);
            operation.Right.Accept(this);
        }
    }
}
