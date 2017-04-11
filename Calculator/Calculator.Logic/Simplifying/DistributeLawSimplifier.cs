using System.Collections.Generic;
using Calculator.Logic.Model;
using Calculator.Model;

namespace Calculator.Logic.Simplifying
{
    public class DistributeLawSimplifier : AVisitingTraversingReplacer
    {
        readonly IDistributeLawHelper mHelper;
        bool? mIsParenthesesLeftSided;
        List<IExpression> mListOfFactors = new List<IExpression>();
        List<IExpression> mListOfMultipliers = new List<IExpression>();

        public DistributeLawSimplifier(IDistributeLawHelper helper)
        {
            mHelper = helper;
        }

        protected override IExpression ReplaceMultiplication(Multiplication multiplication)
        {
            if (IsMultiplicationWithAtLeastOneSideBeingAParenthesedExpression(multiplication))
            {
                var result = mIsParenthesesLeftSided != null
                    ? ExecuteMultiplicationWithOneParentheses(multiplication)
                    : ExecuteMultiplicationWithTwoParentheses(multiplication);
                return result;
            }
            return multiplication;
        }

        IExpression ExecuteMultiplicationWithTwoParentheses(IArithmeticOperation multiplication)
            => mHelper.GetAllUnderLyingMultipliableExpressions(multiplication.Left).Count <=
               mHelper.GetAllUnderLyingMultipliableExpressions(multiplication.Right).Count
                ? ReplaceMultiplication(multiplication.Left, multiplication.Right)
                : ReplaceMultiplication(multiplication.Right, multiplication.Left);

        IExpression ExecuteMultiplicationWithOneParentheses(IArithmeticOperation multiplication)
            => mIsParenthesesLeftSided != null && mIsParenthesesLeftSided.Value
                ? ReplaceMultiplication(multiplication.Right, multiplication.Left)
                : ReplaceMultiplication(multiplication.Left, multiplication.Right);

        IExpression ReplaceMultiplication(IExpression multiplier, IExpression parenthesed)
        {
            mListOfMultipliers = mHelper.GetAllUnderLyingMultipliableExpressions(multiplier);
            mListOfFactors = mHelper.GetAllUnderLyingMultipliableExpressions(parenthesed);

            foreach (var singleMultiplier in mListOfMultipliers)
            {
                var valueHolder = singleMultiplier as IExpressionWithValue;
                var nameHolder = singleMultiplier as IExpressionWithName;
                var needsNegation = IsNegationNecessary(singleMultiplier);
                foreach (var factor in mListOfFactors)
                {
                    if (factor is Variable)
                        continue;
                    if (singleMultiplier is Constant)
                        ReplaceChildWithValue<Constant>(factor, valueHolder.Value, needsNegation);
                    if (singleMultiplier is Sinus)
                        ReplaceChildWithValue<Sinus>(factor, valueHolder.Value, needsNegation);
                    if (singleMultiplier is Cosine)
                        ReplaceChildWithValue<Cosine>(factor, valueHolder.Value, needsNegation);
                    if (singleMultiplier is Tangent)
                        ReplaceChildWithValue<Tangent>(factor, valueHolder.Value, needsNegation);
                    if (singleMultiplier is Variable)
                        ReplaceChildWithName<Variable>(factor, nameHolder.Name, needsNegation);
                }
            }
            var result = (ParenthesedExpression) parenthesed;
            return ExpressionCloner.Clone(result.Wrapped);
        }

        static bool IsNegationNecessary(IExpression expression) => expression.HasParent && expression.Parent is Subtraction;

        static void ReplaceChildWithValue<TSelf>(IExpression factor, decimal value, bool isNegationNecessary)
            where TSelf : IExpressionWithValue, new()
        {
            var parentAsParenthesesExpression = factor.Parent as ParenthesedExpression;
            var parentAsOperation = factor.Parent as IArithmeticOperation;
            var replacement = isNegationNecessary
                ? new Multiplication {Left = factor, Right = new TSelf {Value = value * -1}}
                : new Multiplication {Left = factor, Right = new TSelf {Value = value}};

            if (null != parentAsParenthesesExpression)
                parentAsParenthesesExpression.Wrapped = replacement;
            else if (parentAsOperation != null && parentAsOperation.Left == factor)
                parentAsOperation.Left = replacement;
            else if (parentAsOperation != null && parentAsOperation.Right == factor)
                parentAsOperation.Right = replacement;
        }

        static void ReplaceChildWithName<TSelf>(IExpression multiplier, string name, bool isNegationNecessary)
            where TSelf : IExpressionWithName, new()
        {
            var parentAsParenthesesExpression = multiplier.Parent as ParenthesedExpression;
            var parentAsOperation = multiplier.Parent as IArithmeticOperation;
            var replacement = isNegationNecessary
                ? new Multiplication
                {
                    Left = new Multiplication {Left = multiplier, Right = new Constant {Value = -1}},
                    Right = new TSelf {Name = name}
                }
                : new Multiplication {Left = multiplier, Right = new TSelf {Name = name}};

            if (null != parentAsParenthesesExpression)
                parentAsParenthesesExpression.Wrapped = replacement;
            else if (parentAsOperation != null && parentAsOperation.Left == multiplier)
                parentAsOperation.Left = replacement;
            else if (parentAsOperation != null && parentAsOperation.Right == multiplier)
                parentAsOperation.Right = replacement;
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