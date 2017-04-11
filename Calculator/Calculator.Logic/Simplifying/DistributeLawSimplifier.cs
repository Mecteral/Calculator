using Calculator.Model;

namespace Calculator.Logic.Simplifying
{
    public class DistributeLawSimplifier : AVisitingTraversingReplacer
    {
        bool? mIsParenthesesLeftSided;


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

        IExpression ExecuteMultiplicationWithOneParentheses(Multiplication multiplication)
            =>
                mIsParenthesesLeftSided.Value
                    ? GetExpressionTypeOfMultiplicator(multiplication.Right, multiplication.Left)
                    : GetExpressionTypeOfMultiplicator(multiplication.Left, multiplication.Right);

        IExpression GetExpressionTypeOfMultiplicator(IExpression multiplicator, IExpression parentheses)
        {
            if (multiplicator is Constant)
                return HandleChildrenOfParentheses<Constant>(parentheses);
            else if (multiplicator is Sinus)
                return HandleChildrenOfParentheses<Sinus>(parentheses);
            else if (multiplicator is Cosine)
                return HandleChildrenOfParentheses<Cosine>(parentheses);
            else if (multiplicator is Tangent)
                return HandleChildrenOfParentheses<Tangent>(parentheses);
            else if (multiplicator is Variable)
                return HandleChildrenOfParentheses<Variable>(parentheses);
            return null;

        }

        IExpression HandleChildrenOfParentheses<TSelf>(IExpression expression) where TSelf : IExpression
        {
            while (expression.Children != null)
            {
                foreach (var child in expression.Children)
                {
                    HandleSingleExpressionWithValue<TSelf>(child);
                }
            }
            return expression;
        }

        void HandleSingleExpressionWithValue<TSelf>(IExpression expression) where TSelf : IExpressionWithValue
        {
            expression.ReplaceChild(expression, new Multiplication() {Left = expression, });
        }

        void HandleSingleExpressionWithName<TSelf>(IExpression expression) where TSelf : IExpression
        {
            
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