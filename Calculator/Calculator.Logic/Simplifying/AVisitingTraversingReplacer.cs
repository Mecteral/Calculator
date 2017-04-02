using Calculator.Model;

namespace Calculator.Logic.Simplifying
{
    public abstract class AVisitingTraversingReplacer : ATraversingReplacer, IExpressionVisitor
    {
        IExpression mResult;
        void IExpressionVisitor.Visit(ParenthesedExpression parenthesed) => mResult = ReplaceParenthesed(parenthesed);
        void IExpressionVisitor.Visit(Subtraction subtraction) => mResult = ReplaceSubtraction(subtraction);
        void IExpressionVisitor.Visit(Multiplication multiplication) => mResult = ReplaceMultiplication(multiplication);
        void IExpressionVisitor.Visit(Addition addition) => mResult = ReplaceAddition(addition);
        void IExpressionVisitor.Visit(Constant constant) => mResult = ReplaceConstant(constant);
        void IExpressionVisitor.Visit(Division division) => mResult = ReplaceDivision(division);
        void IExpressionVisitor.Visit(Variable variable) => mResult = ReplaceVariable(variable);
        void IExpressionVisitor.Visit(Cosine cosineExpression) => mResult = ReplaceCosine(cosineExpression);
        void IExpressionVisitor.Visit(Tangent tangentExpression)
            => mResult = ReplaceTangent(tangentExpression);
        void IExpressionVisitor.Visit(Sinus sinus) => mResult = ReplaceSinus(sinus);
        void IExpressionVisitor.Visit(Power square) => mResult = ReplaceSquare(square);
        protected virtual IExpression ReplaceParenthesed(ParenthesedExpression parenthesed) => parenthesed;
        protected virtual IExpression ReplaceSubtraction(Subtraction subtraction) => subtraction;
        protected virtual IExpression ReplaceMultiplication(Multiplication multiplication) => multiplication;
        protected virtual IExpression ReplaceAddition(Addition addition) => addition;
        protected virtual IExpression ReplaceDivision(Division division) => division;
        protected virtual IExpression ReplaceConstant(Constant constant) => constant;
        protected virtual IExpression ReplaceCosine(Cosine cosine) => cosine;
        protected virtual IExpression ReplaceTangent(Tangent tangent) => tangent;
        protected virtual IExpression ReplaceSinus(Sinus sinus) => sinus;
        protected virtual IExpression ReplaceVariable(Variable variable) => variable;
        protected virtual IExpression ReplaceSquare(Power square) => square;
        protected override IExpression Replace(IExpression expression)
        {
            mResult = expression;
            expression.Accept(this);
            return mResult;
        }
    }
}