namespace Calculator.Model
{
    /// <summary>
    /// Visitor for IExpressions
    /// </summary>
    public interface IExpressionVisitor
    {
        void Visit(ParenthesedExpression parenthesed);
        void Visit(Subtraction subtraction);
        void Visit(Multiplication multiplication);
        void Visit(Addition addition);
        void Visit(Constant constant);
        void Visit(Division division);
        void Visit(Variable variable);
        void Visit(CosineExpression cosineExpression);
        void Visit(TangentExpression tangentExpression);
        void Visit(SinusExpression sinusExpression);
        void Visit(SquareRootExpression squareRootExpression);
        void Visit(Square square);
    }
}