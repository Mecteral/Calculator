using System.Collections.Generic;
using Calculator.Logic.Model;
using Calculator.Logic.Utilities;
using Calculator.Model;

namespace Calculator.Logic.Simplifying
{
    public class DistributeLawSimplifier : AVisitingTraversingReplacer
    {
        readonly IExpressionEqualityChecker mEqualityChecker;
        readonly IDistributeLawHelper mHelper;
        bool? mIsParenthesesLeftSided;
        List<IExpression> mListOfEqualMultipliers = new List<IExpression>();
        List<IExpression> mListOfFactors = new List<IExpression>();
        List<IExpression> mListOfMultipliers = new List<IExpression>();

        public DistributeLawSimplifier(IDistributeLawHelper helper, IExpressionEqualityChecker equalityChecker)
        {
            mHelper = helper;
            mEqualityChecker = equalityChecker;
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
                ? CreateMultiplicationReplacement(multiplication.Left, multiplication.Right)
                : CreateMultiplicationReplacement(multiplication.Right, multiplication.Left);

        IExpression ExecuteMultiplicationWithOneParentheses(IArithmeticOperation multiplication)
            => mIsParenthesesLeftSided != null && mIsParenthesesLeftSided.Value
                ? CreateMultiplicationReplacement(multiplication.Right, multiplication.Left)
                : CreateMultiplicationReplacement(multiplication.Left, multiplication.Right);

        IExpression CreateMultiplicationReplacement(IExpression multiplier, IExpression parenthesed)
        {
            mListOfMultipliers = mHelper.GetAllUnderLyingMultipliableExpressions(multiplier);
            mListOfFactors = mHelper.GetAllUnderLyingMultipliableExpressions(parenthesed);
            foreach (var factor in mListOfFactors)
            {
                var variable = factor as Variable;
                if (variable?.Parent is Multiplication)
                    continue;
                CreateSingleFactorMultiplications(factor);
            }

            var result = (ParenthesedExpression) parenthesed;
            return ExpressionCloner.Clone(result.Wrapped);
        }

        void CreateSingleFactorMultiplications(IExpression factor)
        {
            foreach (var singleMultiplier in mListOfMultipliers)
            {
                var valueHolder = singleMultiplier as IExpressionWithValue;
                var nameHolder = singleMultiplier as IExpressionWithName;
                var needsNegation = IsNegationNecessary(singleMultiplier);

                if (singleMultiplier is Constant)
                    ReplaceChildWithValueExpression<Constant>(factor, valueHolder.Value, needsNegation);
                if (singleMultiplier is Sinus)
                    ReplaceChildWithValueExpression<Sinus>(factor, valueHolder.Value, needsNegation);
                if (singleMultiplier is Cosine)
                    ReplaceChildWithValueExpression<Cosine>(factor, valueHolder.Value, needsNegation);
                if (singleMultiplier is Tangent)
                    ReplaceChildWithValueExpression<Tangent>(factor, valueHolder.Value, needsNegation);
                if (singleMultiplier is Variable)
                    ReplaceChildWithNameExpression<Variable>(factor, nameHolder.Name, needsNegation);
            }
        }

        static bool IsNegationNecessary(IExpression expression)
            => expression.HasParent && expression.Parent is Subtraction;

        static void ReplaceChildWithValueExpression<TSelf>(IExpression factor, decimal value, bool isNegationNecessary)
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

        static void ReplaceChildWithNameExpression<TSelf>(IExpression multiplier, string name, bool isNegationNecessary)
            where TSelf : IExpressionWithName, new()
        {
            var parentAsParenthesesExpression = multiplier.Parent as ParenthesedExpression;
            var parentAsOperation = multiplier.Parent as IArithmeticOperation;
            var replacement = isNegationNecessary
                ? new Multiplication
                {
                    Left = new Multiplication {Left = new Constant {Value = -1}, Right = multiplier},
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