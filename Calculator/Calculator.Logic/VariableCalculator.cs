﻿using Calculator.Logic.Model;
using Calculator.Model;

namespace Calculator.Logic
{
    public class VariableCalculator : IExpressionVisitor, ISimplifier
    {
        static IExpression sCalculatedExpression;
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
        public IExpression Simplify(IExpression input)
        {
            mWasChanged = false;
            sCalculatedExpression = ExpressionCloner.Clone(input);
            CalculateVariables(sCalculatedExpression);
            return sCalculatedExpression;
        }
        static void CalculateVariables(IExpression expression)
        {
            var calculator = new VariableCalculator();
            expression.Accept(calculator);
        }
        public IExpression Calculate(IExpression expression)
        {
            mWasChanged = false;
            sCalculatedExpression = ExpressionCloner.Clone(expression);
            CalculateVariables(sCalculatedExpression);
            return sCalculatedExpression;
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
                if (operation is Addition) {
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
            var operationLeft = (IArithmeticOperation) operation.Left;
            var operationRight = (IArithmeticOperation) operation.Right;
            if (operationLeft.Right is Variable && operationRight.Right is Variable)
            {
                var variableOne = (Variable) operationLeft.Right;
                var variableTwo = (Variable) operationRight.Right;
                if (variableOne.Variables == variableTwo.Variables)
                {
                    var constantOne = (Constant) operationLeft.Left;
                    var constantTwo = (Constant) operationRight.Left;
                    if (operation.HasParent)
                    {
                        if (operation.Parent is ParenthesedExpression)
                        {
                            var parenthesed = (ParenthesedExpression) operation.Parent;
                            parenthesed.Wrapped = new Multiplication
                            {
                                Left = new Constant {Value = constantOne.Value + constantTwo.Value},
                                Right = variableOne
                            };
                        }
                        else
                        {
                            var parent = (IArithmeticOperation) operation.Parent;
                            if (parent.Left.Equals(operation))
                            {
                                parent.Left = new Multiplication
                                {
                                    Left = new Constant {Value = constantOne.Value + constantTwo.Value},
                                    Right = variableOne
                                };
                            }
                            else
                            {
                                parent.Right = new Multiplication
                                {
                                    Left = new Constant {Value = constantOne.Value + constantTwo.Value},
                                    Right = variableOne
                                };
                            }
                        }
                    }
                    else
                    {
                        sCalculatedExpression = new Multiplication
                        {
                            Left = new Constant {Value = constantOne.Value + constantTwo.Value},
                            Right = variableOne
                        };
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
                    if (operation.HasParent)
                    {
                        if (operation.Parent is ParenthesedExpression)
                        {
                            var parenthesed = (ParenthesedExpression) operation.Parent;
                            parenthesed.Wrapped = new Multiplication
                            {
                                Left = new Constant {Value = constantOne.Value - constantTwo.Value},
                                Right = variableOne
                            };
                        }
                        else
                        {
                            var parent = (IArithmeticOperation) operation.Parent;
                            if (parent.Left.Equals(operation))
                            {
                                parent.Left = new Multiplication
                                {
                                    Left = new Constant {Value = constantOne.Value - constantTwo.Value},
                                    Right = variableOne
                                };
                            }
                            else
                            {
                                parent.Right = new Multiplication
                                {
                                    Left = new Constant {Value = constantOne.Value - constantTwo.Value},
                                    Right = variableOne
                                };
                            }
                        }
                    }
                    else
                    {
                        sCalculatedExpression = new Multiplication
                        {
                            Left = new Constant {Value = constantOne.Value - constantTwo.Value},
                            Right = variableOne
                        };
                        mWasChanged = true;
                    }
                }
            }
        }
        void MakeMove(IArithmeticOperation operation, IArithmeticOperation chainedOperation)
        {
            if (chainedOperation == null) return;
            if (operation is Addition && chainedOperation is Addition)
            {
                if (mIsRight)
                {
                    chainedOperation.Parent.ReplaceChild(chainedOperation, chainedOperation.Left);
                    HandleAdditionOfVariables(operation, (IArithmeticOperation) chainedOperation.Right);
                }
                else
                {
                    chainedOperation.Parent.ReplaceChild(chainedOperation, chainedOperation.Right);
                    HandleAdditionOfVariables(operation, (IArithmeticOperation) chainedOperation.Left);
                }
            }
            if (operation is Subtraction && chainedOperation is Addition)
            {
                if (mIsRight)
                {
                    chainedOperation.Parent.ReplaceChild(chainedOperation, chainedOperation.Left);
                    HanldeSubtractionToAddition(operation, (IArithmeticOperation) chainedOperation.Right);
                }
                else
                {
                    chainedOperation.Parent.ReplaceChild(chainedOperation, chainedOperation.Right);
                    HanldeSubtractionToAddition(operation, (IArithmeticOperation) chainedOperation.Left);
                }
            }
            if (operation is Addition && chainedOperation is Subtraction)
            {
                if (mIsRight)
                {
                    HandleSubtractionToAddition(operation, (IArithmeticOperation) chainedOperation.Right);
                    chainedOperation.Right = new Constant {Value = 0};
                }
                else
                {
                    HandleAdditionOfVariables(operation, (IArithmeticOperation) chainedOperation.Left);
                    chainedOperation.Left = new Constant {Value = 0};
                }
            }
            if (operation is Subtraction && chainedOperation is Subtraction)
            {
                if (mIsRight)
                {
                    HandleSubtractionToSubtraction(operation, (IArithmeticOperation) chainedOperation.Right);
                    chainedOperation.Right = new Constant {Value = 0};
                }
                else
                {
                    HandleSubtractionToSubtraction(operation, (IArithmeticOperation) chainedOperation.Left);
                    chainedOperation.Left = new Constant {Value = 0};
                }
            }
        }
        void HandleReplacement(IArithmeticOperation operation, IArithmeticOperation replacement)
        {
            if (operation.HasParent)
            {
                if (operation.Parent is ParenthesedExpression) operation.Parent.ReplaceChild(operation, replacement);
                else operation.Parent.ReplaceChild(operation, replacement);
            }
            else sCalculatedExpression = replacement;
        }
        void HandleSubtractionToAddition(IArithmeticOperation operation, IArithmeticOperation multiplication)
        {
            var multiplicationOfOperation = (IArithmeticOperation) operation.Right;
            var constantOne = (Constant) multiplicationOfOperation.Left;
            var constantTwo = (Constant) multiplication.Left;
            var parenthesed = new Multiplication
            {
                Left = new Constant {Value = constantOne.Value - constantTwo.Value},
                Right = new Variable {Variables = mCurrentVariable}
            };
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
            var parenthesed = new Multiplication
            {
                Left = new Constant {Value = constantOne.Value + constantTwo.Value},
                Right = new Variable {Variables = mCurrentVariable}
            };
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
            var parenthesed = new Multiplication
            {
                Left = new Constant {Value = constantOne.Value + constantTwo.Value},
                Right = new Variable {Variables = mCurrentVariable}
            };
            HandleReplacement(operation, replacement);
            if (!operation.HasParent && !mIsRight)
            {
                sCalculatedExpression = new Addition
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
            var parenthesed = new Multiplication
            {
                Left = new Constant {Value = constantOne.Value + constantTwo.Value},
                Right = new Variable {Variables = mCurrentVariable}
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
                if (multiplicationLeft != null) { variableLeft = multiplicationLeft.Right as Variable; }
                if (multiplicationRight != null) { variableRight = multiplicationRight.Right as Variable; }
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