namespace Calculator.Logic.Parsing
{
    /// <summary>
    /// Interface for all Tokens ( NumberToken, OperatorToken, ParenthesesToken)
    /// </summary>
    public interface IToken
    {
        void Accept(ITokenVisitor visitor);
    }
}