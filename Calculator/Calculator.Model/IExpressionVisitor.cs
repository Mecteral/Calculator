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
        void Visit(Cosine cosineExpression);
        void Visit(Tangent tangentExpression);
        void Visit(Sinus sinusExpression);
        void Visit(SquareRoot squareRootExpression);
        void Visit(Power power);
    }
}