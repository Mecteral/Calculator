namespace Calculator.Logic.Parsing
{
    /// <summary>
    /// Visitor for all Tokens ( OperatorToken, NumberToken, ParenthesesToken )
    /// </summary>
    public interface ITokenVisitor
    {
        void Visit(OperatorToken operatorToken);
        void Visit(NumberToken numberToken);
        void Visit(ParenthesesToken parenthesesToken);
        void Visit(VariableToken variableToken);
    }
}
