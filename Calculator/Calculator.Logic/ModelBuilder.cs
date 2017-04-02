using System.Collections.Generic;
using Calculator.Logic.Parsing.CalculationTokenizer;
using Calculator.Model;

namespace Calculator.Logic
{
    /// <summary>
    /// Takes in a List of tokens to make an IExpression for Algorithmic Operations
    /// </summary>
    public class ModelBuilder : ITokenVisitor, IModelBuilder
    {
        IExpression mCurrent;
        IArithmeticOperation mCurrentOperation;
        bool mIsNegated;
        ParenthesedExpression mParenthesed;
        IExpression mResult;
        bool mWasMultiplication;
        bool mWasOpening;

        public IExpression BuildFrom(IEnumerable<IToken> tokens)
        {
            foreach (var token in tokens)
            {
                token.Accept(this);
            }
            mResult = mCurrent;
            return mResult;
        }

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
            mCurrent = new Constant {Value = numberToken.Value};
            HandleNonParenthesesAndOperation();
        }

        public void Visit(ParenthesesToken parenthesesToken)
        {
            if (parenthesesToken.IsOpening)
            {
                AddNewParenthesesExpression();
            }
            else
            {
                HandleClosingParenthesis();
            }
        }

        public void Visit(VariableToken variableToken)
        {
            mCurrent = new Variable {Variables = variableToken.Variable};
            HandleNonParenthesesAndOperation();
        }

        public void Visit(CosineToken cosineToken)
        {
            mCurrent = new Cosine {Value = cosineToken.Value};
            HandleNonParenthesesAndOperation();
        }

        public void Visit(TangentToken tangentToken)
        {
            mCurrent = new Tangent {Value = tangentToken.Value};
            HandleNonParenthesesAndOperation();
        }

        public void Visit(SinusToken sinusToken)
        {
            mCurrent = new Sinus {Value = sinusToken.Value};
            HandleNonParenthesesAndOperation();
        }

        public void Visit(SquareRootToken sqaureRootToken)
        {
            mCurrent = new SquareRoot {Value = sqaureRootToken.Value};
            HandleNonParenthesesAndOperation();
        }

        void HandleNonParenthesesAndOperation()
        {
            if (!mWasOpening)
            {
                FillOperation();
            }
            else
            {
                mWasOpening = false;
            }
        }

        void HandleClosingParenthesis()
        {
            if (mCurrent is IExpressionWithValue)
            {
                mParenthesed.Wrapped = mCurrent;
            }
            mCurrent = mParenthesed;
            mParenthesed = null;
            if (mCurrent.HasParent)
            {
                FindParentForParentheses();
            }
        }

        void AddNewParenthesesExpression()
        {
            var parenthesedExpression = new ParenthesedExpression();
            if (mCurrent is ParenthesedExpression)
            {
                var parent = (ParenthesedExpression) mCurrent;
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

        void FindParentForParentheses()
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

        void CreateOperatorExpression(OperatorToken operatorToken)
        {
            switch (operatorToken.Operator)
            {
                case Operator.Add:
                    mCurrentOperation = new Addition();
                    break;
                case Operator.Subtract:
                    HandleNegationIfNeeded();
                    break;
                case Operator.Multiply:
                    HandleMultiplicationOrDivisionBeforeAdditiveOperation<Multiplication>();
                    break;
                case Operator.Divide:
                    HandleMultiplicationOrDivisionBeforeAdditiveOperation<Division>();
                    break;
                case Operator.Square:
                    HandleMultiplicationOrDivisionBeforeAdditiveOperation<Square>();
                    break;
            }
        }

        void HandleNegationIfNeeded()
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
                var constant = new Constant {Value = 0};
                mCurrentOperation = new Subtraction {Left = constant};
                mIsNegated = true;
            }
        }

        void HandleMultiplicationOrDivisionBeforeAdditiveOperation<TSelf>() where TSelf : IArithmeticOperation, new()
        {
            if (mCurrentOperation?.Right is Variable)
            {
                if (mCurrentOperation.HasParent && !(mCurrentOperation.Parent is ParenthesedExpression))
                {
                    var parent = (IArithmeticOperation) mCurrentOperation.Parent;
                    var temp = parent.Right;
                    parent.Right = new TSelf() {Left = temp};
                    mCurrentOperation = (IArithmeticOperation) parent.Right;
                    mWasMultiplication = true;
                }
                else
                {
                    var temp = mCurrentOperation;
                    mCurrentOperation = new TSelf { Left = temp };
                }
            }
            else if (mCurrentOperation != null && !(mCurrent is ParenthesedExpression))
            {
                var temp = mCurrentOperation.Right;
                var multiplicationOrDivision = new TSelf {Left = temp};
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
                while (mCurrent.HasParent && !(mCurrent.Parent is ParenthesedExpression))
                {
                    mCurrent = mCurrent.HasParent ? mCurrent.Parent : mCurrentOperation;
                }
            }
            if (mParenthesed != null)
            {
                mParenthesed.Wrapped = mCurrent;
            }
        }
    }
}