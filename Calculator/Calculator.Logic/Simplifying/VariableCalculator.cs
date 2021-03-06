﻿using System;
using Calculator.Logic.Model;
using Calculator.Model;

namespace Calculator.Logic.Simplifying
{
    public class VariableCalculator : IVariableCalculator
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
            CheckOperation(multiplication);
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
            CheckOperation(division);
            VisitOperands(division);
        }

        public void Visit(Power square)
        {
            VisitOperands(square);
        }

        public void Visit(Variable variable) {}

        public void Visit(Cosine cosineExpression) {}

        public void Visit(Tangent tangentExpression) {}

        public void Visit(Sinus sinusExpression) {}


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
            var checkDoubleVariableInMultiplication = operation.Left as Multiplication;
            if (operation.Right is Multiplication && (operation.Left is Addition || operation.Left is Subtraction))
            {
                var multiplication = (IArithmeticOperation) operation.Right;
                if (multiplication.Right is Variable)
                {
                    var variable = (Variable) multiplication.Right;
                    mCurrentVariable = variable.Name;
                }
                MakeMove(operation, FindSuitableAdditionOrSubtraction((IArithmeticOperation) operation.Left));
            }
            else if (operation.Right is Multiplication && operation.Left is Multiplication)
            {
                if (operation is Addition)
                {
                    HandleDoubleMultiplicationInAddition(operation);
                }
                else if (operation is Subtraction)
                {
                    HandleDoubleMultiplicationInSubtraction(operation);
                }
            }
            else if (checkDoubleVariableInMultiplication?.Left is Variable && checkDoubleVariableInMultiplication.Right is Variable)
            {
            }
            else if (operation.Right is Constant && operation.Left is Multiplication && (operation is Multiplication || operation is Division))
            {
                var boundVariable = (IArithmeticOperation) operation.Left;
                if (boundVariable.Right is Variable)
                {
                    HandleLeftHandedVariableMultiplicationAndDivision(operation, boundVariable);
                }
            }
            else if (operation.Right is Multiplication && operation.Left is Constant && (operation is Multiplication || operation is Division))
            {
                var boundVariable = (IArithmeticOperation) operation.Right;
                if (boundVariable.Right is Variable)
                {
                    HandleRightHandedVariableMultiplicationAndDivision(operation, boundVariable);
                }
            }
        }

        void HandleRightHandedVariableMultiplicationAndDivision(IArithmeticOperation operation,
            IArithmeticOperation boundVariable)
        {
            var left = (IExpressionWithValue) boundVariable.Left;
            var right = (IExpressionWithValue) operation.Left;
            if (operation is Multiplication)
            {
                operation.Left = new Constant {Value = left.Value * right.Value};
            }
            else
            {
                if (operation.HasParent)
                {
                    operation = HandleParentedRightHandedVariableDivisionInChain(operation);
                }
                else
                {
                    mCalculatedExpression = new Division();
                    operation = (IArithmeticOperation) mCalculatedExpression;
                }
                operation.Left = new Constant {Value = right.Value / left.Value};
            }
            operation.Right = boundVariable.Right;
        }

        void HandleLeftHandedVariableMultiplicationAndDivision(IArithmeticOperation operation,
            IArithmeticOperation boundVariable)
        {
            var left = (IExpressionWithValue) boundVariable.Left;
            var right = (IExpressionWithValue) operation.Right;
            if (operation is Multiplication)
            {
                operation.Left = new Constant {Value = right.Value * left.Value};
            }
            else
            {
                if (operation.HasParent)
                {
                    operation = HandleParentedLeftHandedVariableDivisonInChain(operation);
                }
                else
                {
                    mCalculatedExpression = new Multiplication();
                    operation = (IArithmeticOperation) mCalculatedExpression;
                }
                operation.Left = new Constant {Value = left.Value / right.Value};
            }
            operation.Right = boundVariable.Right;
        }

        IArithmeticOperation HandleParentedLeftHandedVariableDivisonInChain(IArithmeticOperation operation)
        {
            var parent = (IArithmeticOperation)operation.Parent;
            if (parent.Left == operation)
            {
                parent.Left = new Multiplication();
                return (IArithmeticOperation)parent.Left;
            }
            parent.Right = new Multiplication();

            return (IArithmeticOperation)parent.Right;
        }

        IArithmeticOperation HandleParentedRightHandedVariableDivisionInChain(IArithmeticOperation operation)
        {
            var parent = (IArithmeticOperation) operation.Parent;
            if (parent.Left == operation)
            {
                parent.Left = new Division();
                return (IArithmeticOperation) parent.Left;
            }
            parent.Right = new Division();

            return (IArithmeticOperation) parent.Right;
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
                if (variableOne.Name == variableTwo.Name)
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
                        Right = new Variable {Name = mCurrentVariable}
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
                        Right = new Variable {Name = mCurrentVariable}
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
                        Right = new Variable {Name = mCurrentVariable}
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
                            Right = new Variable {Name = mCurrentVariable}
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
                        Right = new Variable {Name = mCurrentVariable}
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
                if (multiplicationRight != null && variableRight != null && variableRight.Name == mCurrentVariable)
                {
                    mIsRight = true;
                    return operation;
                }
                if (multiplicationLeft != null && variableLeft != null && variableLeft.Name == mCurrentVariable)
                {
                    mIsRight = false;
                    return operation;
                }
                if (operation.Left is Addition || operation.Left is Subtraction)
                {
                    operation = (IArithmeticOperation) operation.Left;
                    continue;
                }
                break;
            }
            return null;
        }
    }
}