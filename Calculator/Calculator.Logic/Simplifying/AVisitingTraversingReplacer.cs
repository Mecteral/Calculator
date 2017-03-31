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
        void IExpressionVisitor.Visit(CosineExpression cosineExpression) => mResult = ReplaceCosine(cosineExpression);
        void IExpressionVisitor.Visit(TangentExpression tangentExpression)
            => mResult = ReplaceTangent(tangentExpression);
        void IExpressionVisitor.Visit(SinusExpression sinus) => mResult = ReplaceSinus(sinus);
        void IExpressionVisitor.Visit(SquareRootExpression squareRootExpression)
            => mResult = ReplaceSquareRoot(squareRootExpression);
        void IExpressionVisitor.Visit(Square square) => mResult = ReplaceSquare(square);
        protected virtual IExpression ReplaceParenthesed(ParenthesedExpression parenthesed) => parenthesed;
        protected virtual IExpression ReplaceSubtraction(Subtraction subtraction) => subtraction;
        protected virtual IExpression ReplaceMultiplication(Multiplication multiplication) => multiplication;
        protected virtual IExpression ReplaceAddition(Addition addition) => addition;
        protected virtual IExpression ReplaceDivision(Division division) => division;
        protected virtual IExpression ReplaceConstant(Constant constant) => constant;
        protected virtual IExpression ReplaceCosine(CosineExpression cosine) => cosine;
        protected virtual IExpression ReplaceTangent(TangentExpression tangent) => tangent;
        protected virtual IExpression ReplaceSinus(SinusExpression sinus) => sinus;
        protected virtual IExpression ReplaceSquareRoot(SquareRootExpression squareRoot) => squareRoot;
        protected virtual IExpression ReplaceVariable(Variable variable) => variable;
        protected virtual IExpression ReplaceSquare(Square square) => square;
        protected override IExpression Replace(IExpression expression)
        {
            mResult = expression;
            expression.Accept(this);
            return mResult;
        }
    }
}