using Calculator.Model;

namespace Calculator.Logic.Model
{
    public abstract class AnExpressionVisitorWithResult<TSelf, TResult> : IExpressionVisitor
        where TSelf : AnExpressionVisitorWithResult<TSelf, TResult>, new()
    {
        TResult Result { get; set; }
        void IExpressionVisitor.Visit(ParenthesedExpression parenthesed)
        {
            Result = UseParenthesed(GetResultFor(parenthesed.Wrapped));
        }
        void IExpressionVisitor.Visit(Subtraction subtraction)
        {
            var left = GetResultFor(subtraction.Left);
            var right = GetResultFor(subtraction.Right);
            Result = UseSubtraction(left, right);
        }
        void IExpressionVisitor.Visit(Multiplication multiplication)
        {
            var left = GetResultFor(multiplication.Left);
            var right = GetResultFor(multiplication.Right);
            Result = UseMultiplication(left, right);
        }
        void IExpressionVisitor.Visit(Addition addition)
        {
            var left = GetResultFor(addition.Left);
            var right = GetResultFor(addition.Right);
            Result = UseAddition(left, right);
        }
        void IExpressionVisitor.Visit(Division division)
        {
            var left = GetResultFor(division.Left);
            var right = GetResultFor(division.Right);
            Result = UseDivision(left, right);
        }
        public void Visit(Variable variable)
        {
            Result = UseVariable(variable.Variables);
        }
        void IExpressionVisitor.Visit(Constant constant)
        {
            Result = UseConstant(constant.Value);
        }
        protected static TResult GetResultFor(IExpression expression)
        {
            var visitor = new TSelf();
            expression.Accept(visitor);
            return visitor.Result;
        }
        protected abstract TResult UseParenthesed(TResult wrapped);
        protected abstract TResult UseSubtraction(TResult left, TResult right);
        protected abstract TResult UseMultiplication(TResult left, TResult right);
        protected abstract TResult UseAddition(TResult left, TResult right);
        protected abstract TResult UseDivision(TResult left, TResult right);
        protected abstract TResult UseConstant(decimal value);
        protected abstract TResult UseVariable(string variable);
    }
}