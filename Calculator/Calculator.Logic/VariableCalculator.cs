using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Calculator.Logic.Model;
using Calculator.Model;

namespace Calculator.Logic
{
    public class VariableCalculator : IExpressionVisitor
    {
        string mCurrentVariable;
        static IExpression sCalculatedExpression;
        bool mWasChanged;
        bool mIsRight;
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
                MakeMove(operation, FindSuitableAdditionOrSubtraction((IArithmeticOperation)operation.Left));
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

        void HandleDoubleMultiplicationInAddition(IArithmeticOperation operation)
        {
            var operationLeft = (IArithmeticOperation)operation.Left;
            var operationRight = (IArithmeticOperation)operation.Right;
            if (operationLeft.Right is Variable && operationRight.Right is Variable)
            {
                var variableOne = (Variable)operationLeft.Right;
                var variableTwo = (Variable)operationRight.Right;
                if (variableOne.Variables == variableTwo.Variables)
                {
                    var constantOne = (Constant)operationLeft.Left;
                    var constantTwo = (Constant)operationRight.Left;
                    if (operation.HasParent)
                    {
                        if (operation.Parent is ParenthesedExpression)
                        {
                            var parenthesed = (ParenthesedExpression)operation.Parent;
                            parenthesed.Wrapped = new Multiplication { Left = new Constant { Value = constantOne.Value + constantTwo.Value }, Right = variableOne };
                        }
                        else
                        {
                            var parent = (IArithmeticOperation)operation.Parent;
                            if (parent.Left.Equals(operation))
                            {
                                parent.Left = new Multiplication { Left = new Constant { Value = constantOne.Value + constantTwo.Value }, Right = variableOne };
                            }
                            else
                            {
                                parent.Right = new Multiplication { Left = new Constant { Value = constantOne.Value + constantTwo.Value }, Right = variableOne };
                            }
                        }
                    }
                    else
                    {
                        sCalculatedExpression = new Multiplication { Left = new Constant { Value = constantOne.Value + constantTwo.Value }, Right = variableOne };
                        mWasChanged = true;
                    }
                }
            }
        }

        void HandleDoubleMultiplicationInSubtraction(IArithmeticOperation operation)
        {
            var operationLeft = (IArithmeticOperation)operation.Left;
            var operationRight = (IArithmeticOperation)operation.Right;
            if (operationLeft.Right is Variable && operationRight.Right is Variable && operationLeft.Left is Constant && operationRight.Left is Constant)
            {
                var variableOne = (Variable)operationLeft.Right;
                var variableTwo = (Variable)operationRight.Right;
                if (variableOne.Variables == variableTwo.Variables)
                {
                    var constantOne = (Constant)operationLeft.Left;
                    var constantTwo = (Constant)operationRight.Left;
                    if (operation.HasParent)
                    {
                        if (operation.Parent is ParenthesedExpression)
                        {
                            var parenthesed = (ParenthesedExpression)operation.Parent;
                            parenthesed.Wrapped = new Multiplication { Left = new Constant { Value = constantOne.Value - constantTwo.Value }, Right = variableOne };
                        }
                        else
                        {
                            var parent = (IArithmeticOperation)operation.Parent;
                            if (parent.Left.Equals(operation))
                            {
                                parent.Left = new Multiplication { Left = new Constant { Value = constantOne.Value - constantTwo.Value }, Right = variableOne };
                            }
                            else
                            {
                                parent.Right = new Multiplication { Left = new Constant { Value = constantOne.Value - constantTwo.Value }, Right = variableOne };
                            }
                        }
                    }
                    else
                    {
                        sCalculatedExpression = new Multiplication { Left = new Constant { Value = constantOne.Value - constantTwo.Value }, Right = variableOne };
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
                if (chainedOperation.HasParent)
                {
                    var parent = (IArithmeticOperation)chainedOperation.Parent;
                    if (parent.Left.Equals(chainedOperation))
                    {
                        if (mIsRight)
                        {
                            parent.Left = chainedOperation.Left;
                            HandleAdditionOfVariables(operation, (IArithmeticOperation)chainedOperation.Right);
                        }
                        else
                        {
                            parent.Left = chainedOperation.Right;
                            HandleAdditionOfVariables(operation, (IArithmeticOperation)chainedOperation.Left);
                        }
                    }
                }
            }
            if ((operation is Subtraction && chainedOperation is Addition))
            {
                if (chainedOperation.HasParent)
                {
                    var parent = (IArithmeticOperation)chainedOperation.Parent;
                    if (parent.Left.Equals(chainedOperation))
                    {
                        if (mIsRight)
                        {
                            parent.Left = chainedOperation.Left;
                            HanldeSubtractionToAddition(operation, (IArithmeticOperation)chainedOperation.Right);
                        }
                        else
                        {
                            parent.Left = chainedOperation.Right;
                            HanldeSubtractionToAddition(operation, (IArithmeticOperation)chainedOperation.Left);
                        }
                    }
                }
            }
            if (operation is Addition && chainedOperation is Subtraction)
            {
                if (chainedOperation.HasParent)
                {
                    var parent = (IArithmeticOperation)chainedOperation.Parent;
                    if (parent.Left.Equals(chainedOperation))
                    {
                        if (mIsRight)
                        {
                            HandleSubtractionToAddition(operation, (IArithmeticOperation)chainedOperation.Right);
                            chainedOperation.Right = new Constant {Value = 0};
                        }
                        else
                        {
                            HandleAdditionOfVariables(operation, (IArithmeticOperation)chainedOperation.Left);
                            chainedOperation.Left = new Constant {Value = 0};
                        }
                    }
                }
            }
            if (operation is Subtraction && chainedOperation is Subtraction)
            {
                if (chainedOperation.HasParent)
                {
                    var parent = (IArithmeticOperation)chainedOperation.Parent;
                    if (parent.Left.Equals(chainedOperation))
                    {
                        if (mIsRight)
                        {
                            HandleSubtractionToSubtraction(operation, (IArithmeticOperation)chainedOperation.Right);
                            chainedOperation.Right = new Constant { Value = 0 };
                        }
                        else
                        {
                            HandleSubtractionToSubtraction(operation, (IArithmeticOperation)chainedOperation.Left);
                            chainedOperation.Left = new Constant { Value = 0 };
                        }
                    }
                }
            }
        }

        void HandleSubtractionToSubtraction(IArithmeticOperation operation, IArithmeticOperation multiplication)
        {
            var multiplicationOfOperation = (IArithmeticOperation)operation.Right;
            var variable = (Variable)multiplicationOfOperation.Right;
            var constantOne = (Constant)multiplication.Left;
            var constantTwo = (Constant)multiplicationOfOperation.Left;
            if (operation.HasParent)
            {
                if (operation.Parent is ParenthesedExpression)
                {
                    var parentheses = (ParenthesedExpression)operation.Parent;
                    parentheses.Wrapped = new Multiplication { Left = new Constant { Value = constantOne.Value + constantTwo.Value }, Right = new Variable { Variables = variable.Variables } };
                }
                else
                {
                    var parent = (IArithmeticOperation)operation.Parent;
                    if (parent.Left.Equals(operation))
                    {
                        parent.Left = new Multiplication { Left = new Constant { Value = constantOne.Value - constantTwo.Value }, Right = new Variable { Variables = variable.Variables } };
                    }
                    else
                    {
                        parent.Right = new Multiplication { Left = new Constant { Value = constantOne.Value - constantTwo.Value }, Right = new Variable { Variables = variable.Variables } };
                    }
                }
            }
            else
            {
                if (mIsRight)
                {
                    sCalculatedExpression = new Subtraction { Left = operation.Left, Right = new Multiplication { Left = new Constant { Value = constantOne.Value + constantTwo.Value }, Right = new Variable { Variables = variable.Variables } } };
                }
                else
                {
                    sCalculatedExpression = new Addition { Left = operation.Left, Right = new Multiplication { Left = new Constant { Value = constantOne.Value - constantTwo.Value }, Right = new Variable { Variables = variable.Variables } } };
                }
            }
        }
        void HanldeSubtractionToAddition(IArithmeticOperation operation, IArithmeticOperation multiplication)
        {
            var multiplicationOfOperation = (IArithmeticOperation)operation.Right;
            var variable = (Variable)multiplicationOfOperation.Right;
            var constantTwo = (Constant)multiplication.Left;
            var constantOne = (Constant)multiplicationOfOperation.Left;
            if (operation.HasParent)
            {
                if (operation.Parent is ParenthesedExpression)
                {
                    var parentheses = (ParenthesedExpression)operation.Parent;
                    parentheses.Wrapped = new Multiplication { Left = new Constant { Value = constantOne.Value + constantTwo.Value }, Right = new Variable { Variables = variable.Variables } };
                }
                else
                {
                    var parent = (IArithmeticOperation)operation.Parent;
                    if (parent.Left.Equals(operation))
                    {
                        parent.Left = new Multiplication { Left = new Constant { Value = constantOne.Value - constantTwo.Value }, Right = new Variable { Variables = variable.Variables } };
                    }
                    else
                    {
                        parent.Right = new Multiplication { Left = new Constant { Value = constantOne.Value - constantTwo.Value }, Right = new Variable { Variables = variable.Variables } };
                    }
                }
            }
            else
            {
                if (mIsRight)
                {
                    sCalculatedExpression = new Subtraction { Left = operation.Left, Right = new Multiplication { Left = new Constant { Value = constantOne.Value - constantTwo.Value }, Right = new Variable { Variables = variable.Variables } } };
                }
                else
                {
                    sCalculatedExpression = new Subtraction { Left = operation.Left, Right = new Multiplication { Left = new Constant { Value = constantOne.Value - constantTwo.Value }, Right = new Variable { Variables = variable.Variables } } };
                }

            }
        }

        void HandleSubtractionToAddition(IArithmeticOperation operation, IArithmeticOperation multiplication)
        {
            var multiplicationOfOperation = (IArithmeticOperation)operation.Right;
            var variable = (Variable)multiplicationOfOperation.Right;
            var constantOne = (Constant)multiplication.Left;
            var constantTwo = (Constant)multiplicationOfOperation.Left;
            if (operation.HasParent)
            {
                if (operation.Parent is ParenthesedExpression)
                {
                    var parentheses = (ParenthesedExpression)operation.Parent;
                    parentheses.Wrapped = new Multiplication { Left = new Constant { Value = constantOne.Value - constantTwo.Value }, Right = new Variable { Variables = variable.Variables } };
                }
                else
                {
                    var parent = (IArithmeticOperation)operation.Parent;
                    if (parent.Left.Equals(operation))
                    {
                        parent.Left = new Multiplication { Left = new Constant { Value = constantOne.Value - constantTwo.Value }, Right = new Variable { Variables = variable.Variables } };
                    }
                    else
                    {
                        parent.Right = new Multiplication { Left = new Constant { Value = constantOne.Value - constantTwo.Value }, Right = new Variable { Variables = variable.Variables } };
                    }
                }
            }
            else
            {
                sCalculatedExpression = new Addition
                {
                    Left = operation.Left,
                    Right = new Multiplication{
                        Left = new Constant { Value = constantTwo.Value  - constantOne.Value},
                        Right = new Variable { Variables = variable.Variables } }
                };
            }
        }
        void HandleAdditionOfVariables(IArithmeticOperation operation, IArithmeticOperation multiplication)
        {
            var multiplicationOfOperation = (IArithmeticOperation)operation.Right;
            var variable = (Variable) multiplicationOfOperation.Right;
            var constantOne = (Constant) multiplication.Left;
            var constantTwo = (Constant) multiplicationOfOperation.Left;
            if (operation.HasParent)
            {
                if (operation.Parent is ParenthesedExpression)
                {
                    var parentheses = (ParenthesedExpression)operation.Parent;
                    parentheses.Wrapped =  new Multiplication { Left = new Constant {Value = constantOne.Value+constantTwo.Value}, Right = new Variable {Variables = variable.Variables} };
                }
                else
                {
                    var parent = (IArithmeticOperation)operation.Parent;
                    if (parent.Left.Equals(operation))
                    {
                        parent.Left = new Multiplication { Left = new Constant { Value = constantOne.Value + constantTwo.Value }, Right = new Variable { Variables = variable.Variables } };
                    }
                    else
                    {
                        parent.Right = new Multiplication { Left = new Constant { Value = constantOne.Value + constantTwo.Value }, Right = new Variable { Variables = variable.Variables } };
                    }
                }
            }
            else
            {
                sCalculatedExpression = new Addition { Left = operation.Left, Right = new Multiplication { Left = new Constant { Value = constantOne.Value + constantTwo.Value }, Right = new Variable { Variables = variable.Variables } } };
            }
        }

        IArithmeticOperation FindSuitableAdditionOrSubtraction(IArithmeticOperation operation)
        {
            while (true && !mWasChanged)
            {
                var current = operation;
                if (operation.Right is Multiplication)
                {
                    var multiplication = (IArithmeticOperation)operation.Right;
                    if (multiplication.Right is Variable)
                    {
                        var variable = (Variable) multiplication.Right;
                        if (variable.Variables == mCurrentVariable)
                        {
                            mIsRight = true;
                            return current;
                        }
                    }
                }
                else if (operation.Left is Multiplication)
                {
                    var multiplication = (IArithmeticOperation)operation.Left;
                    if (multiplication.Right is Variable)
                    {
                        var variable = (Variable)multiplication.Right;
                        if (variable.Variables == mCurrentVariable)
                        {
                            mIsRight = false;
                            return current;
                        }
                    }
                }
                else if (operation.Left is Addition || operation.Left is Subtraction)
                {
                    operation = (IArithmeticOperation)current.Left;
                    continue;
                }
                else if (operation.Right is Addition || operation.Right is Subtraction)
                {
                    operation = (IArithmeticOperation)current.Right;
                    continue;
                }
                break;
            }
            return null;
        }
    }
}
