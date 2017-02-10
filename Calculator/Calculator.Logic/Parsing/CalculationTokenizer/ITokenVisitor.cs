
namespace Calculator.Logic.Parsing.CalculationTokenizer
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
        void Visit(CosineToken cosineToken);
        void Visit(TangentToken tangentToken);
        void Visit(SinusToken sinusToken);
        void Visit(SquareRootToken sqaureRootToken);
    }
}