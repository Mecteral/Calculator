using Calculator.Logic.Parsing.ConversionTokenizer;

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
        void Visit(MetricToken metricToken);
        void Visit(ImperialToken imperialToken);
    }
}