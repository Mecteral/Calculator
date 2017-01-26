using System;
using Calculator.Logic.Model;
using Calculator.Model;

namespace Calculator.Logic.Simplifying
{
    public class VariableCalculator : IExpressionVisitor, ISimplifier
    {
        IExpression mCalculatedExpression;
        string mCurrentVariable;
        bool mIsRight;
        bool mWasChanged;

        public void Visit(ParenthesedExpression parenthesed)
        {
            parenthesed.Wrapped.Accept(this);
        }

        public void Visit(Subtraction subtraction)
        {
            CheckOperation(subtraction);
            VisitOperands(subtraction);
        }

        public void Visit(Multiplication multiplication)
        {
            VisitOperands(multiplication);
        }

        public void Visit(Addition addition)
        {
            CheckOperation(addition);
            VisitOperands(addition);
        }

        public void Visit(Constant constant) {}

        public void Visit(Division division)
        {
            VisitOperands(division);
        }

        public void Visit(Variable variable) {}

        public void Visit(CosineExpression cosineExpression) {}

        public void Visit(TangentExpression tangentExpression) {}

        public void Visit(SinusExpression sinusExpression) {}

        public IExpression Simplify(IExpression input)
        {
            mWasChanged = false;
            mCalculatedExpression = ExpressionCloner.Clone(input);
            mCalculatedExpression.Accept(this);
            return mCalculatedExpression;
        }

        void VisitOperands(IArithmeticOperation operation)
        {
            operation.Left.Accept(this);
            operation.Right.Accept(this);
        }

        void CheckOperation(IArithmeticOperation operation)
        {
            if (operation.Right is Multiplication && (operation.Left is Addition || operation.Left is Subtraction))
            {
                var multiplication = (IArithmeticOperation) operation.Right;
                if (multiplication.Right is Variable)
                {
                    var variable = (Variable) multiplication.Right;
                    mCurrentVariable = variable.Variables;
                }
                MakeMove(operation, FindSuitableAdditionOrSubtraction((IArithmeticOperation) operation.Left));
            }
            else if (operation.Right is Multiplication && operation.Left is Multiplication)
            {
                if (operation is Addition)
                {
                    HandleDoubleMultiplicationInAddition(operation);
                }
                else
                {
                    HandleDoubleMultiplicationInSubtraction(operation);
                }
            }
        }

        /// <summary>
        /// Handles addition of Variables next to each other
        /// </summary>
        /// <param name="operation"></param>
        void HandleDoubleMultiplicationInAddition(IArithmeticOperation operation)
        {
            HandleDoubleMultiplicationInAdditiveOperation(operation, (l, r) => l + r);
        }

        void HandleDoubleMultiplicationInAdditiveOperation(
            IArithmeticOperation operation,
            Func<decimal, decimal, decimal> calculateLeftConstant)
        {
            var operationLeft = (IArithmeticOperation) operation.Left;
            var operationRight = (IArithmeticOperation) operation.Right;
            if (operationLeft.Right is Variable && operationRight.Right is Variable && operationLeft.Left is Constant &&
                operationRight.Left is Constant)
            {
                var variableOne = (Variable) operationLeft.Right;
                var variableTwo = (Variable) operationRight.Right;
                if (variableOne.Variables == variableTwo.Variables)
                {
                    var constantOne = (Constant) operationLeft.Left;
                    var constantTwo = (Constant) operationRight.Left;
                    var replacement = new Multiplication
                    {
                        Left = new Constant {Value = calculateLeftConstant(constantOne.Value, constantTwo.Value)},
                        Right = variableOne
                    };
                    if (operation.HasParent)
                    {
                        operation.Parent.ReplaceChild(operation, replacement);
                    }
                    else
                    {
                        mCalculatedExpression = replacement;
                        mWasChanged = true;
                    }
                }
            }
        }

        /// <summary>
        /// Handles subtraction of Variables next to each other
        /// </summary>
        /// <param name="operation"></param>
        void HandleDoubleMultiplicationInSubtraction(IArithmeticOperation operation)
        {
            HandleDoubleMultiplicationInAdditiveOperation(operation, (l, r) => l - r);
        }

        void ModifyAddition(
            IArithmeticOperation operation,
            IArithmeticOperation chainedOperation,
            Action<IArithmeticOperation, IArithmeticOperation> handler)
        {
            if (mIsRight)
            {
                chainedOperation.Parent.ReplaceChild(chainedOperation, chainedOperation.Left);
                handler(operation, (IArithmeticOperation) chainedOperation.Right);
            }
            else
            {
                chainedOperation.Parent.ReplaceChild(chainedOperation, chainedOperation.Right);
                handler(operation, (IArithmeticOperation) chainedOperation.Left);
            }
        }

        void ModifySubtraction(
            IArithmeticOperation operation,
            IArithmeticOperation chainedOperation,
            Action<IArithmeticOperation, IArithmeticOperation> handler)
        {
            ModifySubtraction(operation, chainedOperation, handler, handler);
        }

        void ModifySubtraction(
            IArithmeticOperation operation,
            IArithmeticOperation chainedOperation,
            Action<IArithmeticOperation, IArithmeticOperation> righthandler,
            Action<IArithmeticOperation, IArithmeticOperation> leftHandler)
        {
            if (mIsRight)
            {
                righthandler(operation, (IArithmeticOperation) chainedOperation.Right);
                chainedOperation.Right = new Constant {Value = 0};
            }
            else
            {
                leftHandler(operation, (IArithmeticOperation) chainedOperation.Left);
                chainedOperation.Left = new Constant {Value = 0};
            }
        }

        void MakeMove(IArithmeticOperation operation, IArithmeticOperation chainedOperation)
        {
            if (chainedOperation == null) return;

            new AdditionSubtractionDispatcher(operation, chainedOperation)
            {
                ForAddAdd = (op, chained) => ModifyAddition(op, chained, HandleAdditionOfVariables),
                ForSubAdd = (op, chained) => ModifyAddition(op, chained, HanldeSubtractionToAddition),
                ForSubSub = (op, chained) => ModifySubtraction(op, chained, HandleSubtractionToSubtraction),
                ForAddSub =
                    (op, chained) =>
                            ModifySubtraction(op, chained, HandleSubtractionToAddition, HandleAdditionOfVariables)
            }.Execute();
        }

        void HandleReplacement(IExpression operation, IExpression replacement)
        {
            if (operation.HasParent)
            {
                operation.Parent.ReplaceChild(operation, replacement);
            }
            else mCalculatedExpression = replacement;
        }

        void HandleSubtractionToAddition(IArithmeticOperation operation, IArithmeticOperation multiplication)
        {
            var multiplicationOfOperation = (IArithmeticOperation) operation.Right;
            var constantOne = (Constant) multiplicationOfOperation.Left;
            var constantTwo = (Constant) multiplication.Left;
            var replacement = new Addition
            {
                Left = operation.Left,
                Right =
                    new Multiplication
                    {
                        Left = new Constant {Value = constantOne.Value - constantTwo.Value},
                        Right = new Variable {Variables = mCurrentVariable}
                    }
            };
            HandleReplacement(operation, replacement);
        }

        void HandleAdditionOfVariables(IArithmeticOperation operation, IArithmeticOperation multiplication)
        {
            var multiplicationOfOperation = (IArithmeticOperation) operation.Right;
            var constantOne = (Constant) multiplication.Left;
            var constantTwo = (Constant) multiplicationOfOperation.Left;
            var replacement = new Addition
            {
                Left = operation.Left,
                Right =
                    new Multiplication
                    {
                        Left = new Constant {Value = constantOne.Value + constantTwo.Value},
                        Right = new Variable {Variables = mCurrentVariable}
                    }
            };
            HandleReplacement(operation, replacement);
        }

        void HandleSubtractionToSubtraction(IArithmeticOperation operation, IArithmeticOperation multiplication)
        {
            var multiplicationOfOperation = (IArithmeticOperation) operation.Right;
            var constantOne = (Constant) multiplication.Left;
            var constantTwo = (Constant) multiplicationOfOperation.Left;
            var replacement = new Subtraction
            {
                Left = operation.Left,
                Right =
                    new Multiplication
                    {
                        Left = new Constant {Value = constantOne.Value + constantTwo.Value},
                        Right = new Variable {Variables = mCurrentVariable}
                    }
            };
            HandleReplacement(operation, replacement);
            if (!operation.HasParent && !mIsRight)
            {
                mCalculatedExpression = new Addition
                {
                    Left = operation.Left,
                    Right =
                        new Multiplication
                        {
                            Left = new Constant {Value = constantOne.Value - constantTwo.Value},
                            Right = new Variable {Variables = mCurrentVariable}
                        }
                };
            }
        }

        void HanldeSubtractionToAddition(IArithmeticOperation operation, IArithmeticOperation multiplication)
        {
            var multiplicationOfOperation = (IArithmeticOperation) operation.Right;
            var constantTwo = (Constant) multiplication.Left;
            var constantOne = (Constant) multiplicationOfOperation.Left;
            var replacement = new Subtraction
            {
                Left = operation.Left,
                Right =
                    new Multiplication
                    {
                        Left = new Constant {Value = constantOne.Value - constantTwo.Value},
                        Right = new Variable {Variables = mCurrentVariable}
                    }
            };
            HandleReplacement(operation, replacement);
        }

        IArithmeticOperation FindSuitableAdditionOrSubtraction(IArithmeticOperation operation)
        {
            while (!mWasChanged)
            {
                var multiplicationLeft = operation.Left as Multiplication;
                var multiplicationRight = operation.Right as Multiplication;
                var variableRight = (Variable) null;
                var variableLeft = (Variable) null;
                if (multiplicationLeft != null)
                {
                    variableLeft = multiplicationLeft.Right as Variable;
                }
                if (multiplicationRight != null)
                {
                    variableRight = multiplicationRight.Right as Variable;
                }
                if (multiplicationRight != null && variableRight != null && variableRight.Variables == mCurrentVariable)
                {
                    mIsRight = true;
                    return operation;
                }
                if (multiplicationLeft != null && variableLeft != null && variableLeft.Variables == mCurrentVariable)
                {
                    mIsRight = false;
                    return operation;
                }
                if (operation.Left is Addition || operation.Left is Subtraction)
                {
                    operation = (IArithmeticOperation) operation.Left;
                    continue;
                }
                if (operation.Right is Addition || operation.Right is Subtraction)
                {
                    operation = (IArithmeticOperation) operation.Right;
                    continue;
                }
                break;
            }
            return null;
        }
    }
}