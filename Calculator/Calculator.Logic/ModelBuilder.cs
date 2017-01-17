using System;
using System.Collections.Generic;
using System.Linq;
using Calculator.Logic.Model;
using Calculator.Logic.Parsing;
using Calculator.Logic.Parsing.CalculationTokenizer;
using Calculator.Logic.Parsing.ConversionTokenizer;
using Calculator.Model;

namespace Calculator.Logic
{
    /// <summary>
    /// Takes in a List of tokens to make an IExpression for Algorithmic Operations
    /// </summary>
    public class ModelBuilder : ITokenVisitor, IModelBuilder
    {
        IExpression mCurrent;
        ParenthesedExpression mParenthesed;
        IArithmeticOperation mCurrentOperation;
        IExpression mResult;
        bool mIsNegated;
        bool mWasOpening;
        bool mWasMultiplication;

        public void Visit(OperatorToken operatorToken)
        {
            CreateOperatorExpression(operatorToken);
            if (!mIsNegated && !mWasMultiplication)
            {
                FillOperation();
            }
            else
            {
                mWasMultiplication = false;
                mIsNegated = false;
            }

        }

        public void Visit(NumberToken numberToken)
        {
            mCurrent = new Constant { Value = numberToken.Value };
            if (!mWasOpening)
            {
                FillOperation();
            }
            else
            {
                mWasOpening = false;
            }

        }

        public void Visit(ParenthesesToken parenthesesToken)
        {
            if (parenthesesToken.IsOpening)
            {
                var parenthesedExpression = new ParenthesedExpression();
                if (mCurrent is ParenthesedExpression)
                {
                    var parent = (ParenthesedExpression)mCurrent;
                    parent.Wrapped = parenthesedExpression;
                }
                mCurrent = parenthesedExpression;
                mParenthesed = (ParenthesedExpression) mCurrent;
                if (mCurrentOperation != null)
                {
                    mCurrentOperation.Right = mCurrent;
                    mWasOpening = true;
                    mCurrentOperation = null;
                }
            }
            else
            {
                mWasOpening = false;
                if (mCurrent is Constant)
                {
                    mParenthesed.Wrapped = mCurrent;
                }
                mCurrent = mParenthesed;
                mParenthesed = null;
                if (mCurrent.HasParent)
                {
                    var temp = mCurrent;
                    while (temp.HasParent)
                    {
                        temp = temp.Parent;
                        if (temp is ParenthesedExpression)
                        {
                            mParenthesed = (ParenthesedExpression) temp;
                            break;
                        }
                        mCurrent = temp;
                    }
                }
            }
        }

        public void Visit(VariableToken variableToken)
        {
            mCurrent = new Variable { Variables= variableToken.Variable};
            if (!mWasOpening)
            {
                FillOperation();
            }
            else
            {
                mWasOpening = false;
            }
        }

        public IExpression BuildFrom(IEnumerable<IToken> tokens)
        {
            foreach (var token in tokens)
            {
                token.Accept(this);
            }
            mResult = mCurrent;
            return mResult;
        }
        void CreateOperatorExpression(OperatorToken operatorToken)
        {
            switch (operatorToken.Operator)
            {
                case Operator.Add:
                    mCurrentOperation = new Addition();
                    break;
                case Operator.Subtract:
                    HandleNegation();
                    break;
                case Operator.Multiply:
                    HandleMultiplicationBeforeAdditiveOperation<Multiplication>();
                    break;
                case Operator.Divide:
                    
                    HandleMultiplicationBeforeAdditiveOperation<Division>();
                    break;
            }
        }

        void HandleNegation()
        {
            var parentheses = mCurrent as ParenthesedExpression;
            if (mCurrent != null && !(mCurrent is ParenthesedExpression))
                mCurrentOperation = new Subtraction();
            else if (parentheses?.Wrapped != null)
            {
                    mCurrentOperation = new Subtraction();
            }
            else
            {
                var constant = new Constant { Value = 0 };
                mCurrentOperation = new Subtraction { Left = constant };
                mIsNegated = true;
            }
        }
        void HandleMultiplicationBeforeAdditiveOperation<TSelf>() where TSelf : IArithmeticOperation, new()
        {
            if (mCurrentOperation is Subtraction || mCurrentOperation is Addition && !(mCurrent is ParenthesedExpression))
            {
                var temp = mCurrentOperation.Right;
                var multiplicationOrDivision = new TSelf { Left = temp };
                mCurrentOperation.Right = multiplicationOrDivision;
                mCurrentOperation = multiplicationOrDivision;
                mWasMultiplication = true;
            }
            else
                mCurrentOperation = new TSelf();
        }
        void FillOperation()
        {
            if (mCurrentOperation != null)
            {
                if (mCurrentOperation.Left == null)
                    mCurrentOperation.Left = mCurrent;
                else
                    mCurrentOperation.Right = mCurrent;
                mCurrent = mCurrentOperation;
            }
            if (mCurrentOperation != null && !(mCurrentOperation.Parent is ParenthesedExpression))
            {
                mCurrent = mCurrentOperation.HasParent ? mCurrentOperation.Parent : mCurrentOperation;
            }
            if (mParenthesed != null)
            {
                mParenthesed.Wrapped = mCurrent;
            }
        }
    }
}