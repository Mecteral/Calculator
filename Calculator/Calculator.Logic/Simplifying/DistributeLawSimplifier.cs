using System.Collections.Generic;
using Calculator.Logic.Model;
using Calculator.Model;

namespace Calculator.Logic.Simplifying
{
    public class DistributeLawSimplifier : AVisitingTraversingReplacer
    {
        readonly IDistributeLawHelper mHelper;
        bool? mIsParenthesesLeftSided;
        List<IExpression> mListOfMultiplicators = new List<IExpression>();

        public DistributeLawSimplifier(IDistributeLawHelper helper)
        {
            mHelper = helper;
        }

        protected override IExpression ReplaceMultiplication(Multiplication multiplication)
        {
            if (IsMultiplicationWithAtLeastOneSideBeingAParenthesedExpression(multiplication))
            {
                return mIsParenthesesLeftSided != null
                    ? ExecuteMultiplicationWithOneParentheses(multiplication)
                    : ExecuteMultiplicationWithTwoParentheses(multiplication);
            }
            return multiplication;
        }

        IExpression ExecuteMultiplicationWithTwoParentheses(Multiplication multiplication)
        {
            return null;
        }

        IExpression ExecuteMultiplicationWithOneParentheses(IArithmeticOperation multiplication)
        {
            return mIsParenthesesLeftSided != null && mIsParenthesesLeftSided.Value
                ? ExecuteMultiplication(multiplication.Right, multiplication.Left)
                : ExecuteMultiplication(multiplication.Left, multiplication.Right);
        }

        IExpression ExecuteMultiplication(IExpression multiplicator, IExpression parentheses)
        {
            mListOfMultiplicators = mHelper.GetAllUnderLyingMultipliableExpressions(multiplicator);
            foreach (var singleMultiplicator in mListOfMultiplicators)
            {
                var valueholder = singleMultiplicator as IExpressionWithValue;
                var nameholder = singleMultiplicator as IExpressionWithName;
                foreach (var expression in mHelper.GetAllUnderLyingMultipliableExpressions(parentheses))
                {
                    if (singleMultiplicator is Constant)
                        expression.Parent.ReplaceChild(expression,
                            new Multiplication {Left = expression, Right = new Constant {Value = valueholder.Value}});
                    if (singleMultiplicator is Sinus)
                        expression.Parent.ReplaceChild(expression,
                            new Multiplication {Left = expression, Right = new Sinus {Value = valueholder.Value}});
                    if (singleMultiplicator is Cosine)
                        expression.Parent.ReplaceChild(expression,
                            new Multiplication {Left = expression, Right = new Cosine {Value = valueholder.Value}});
                    if (singleMultiplicator is Tangent)
                        expression.Parent.ReplaceChild(expression,
                            new Multiplication {Left = expression, Right = new Tangent {Value = valueholder.Value}});
                    if (singleMultiplicator is Variable)
                        expression.Parent.ReplaceChild(expression,
                            new Multiplication {Left = expression, Right = new Variable {Name = nameholder.Name}});
                }
            }
            var result = (ParenthesedExpression) parentheses;
            return ExpressionCloner.Clone(result.Wrapped);
        }

        bool IsMultiplicationWithAtLeastOneSideBeingAParenthesedExpression(IArithmeticOperation multiplication)
        {
            if (multiplication.Left is ParenthesedExpression && multiplication.Right is ParenthesedExpression)
            {
                mIsParenthesesLeftSided = null;
                return true;
            }
            if (multiplication.Left is ParenthesedExpression)
            {
                mIsParenthesesLeftSided = true;
                return true;
            }
            if (multiplication.Right is ParenthesedExpression)
            {
                mIsParenthesesLeftSided = false;
                return true;
            }
            return false;
        }
    }
}