namespace Calculator.Logic.Model
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
    }
}