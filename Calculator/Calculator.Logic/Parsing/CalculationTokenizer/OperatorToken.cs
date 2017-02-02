namespace Calculator.Logic.Parsing.CalculationTokenizer
{
    /// <summary>
    /// Token of Operations ( Add, Multiply, Substract, Divide)
    /// </summary>
    public class OperatorToken : IToken
    {
        public OperatorToken(char asText)
        {
            switch (asText)
            {
                case '+':
                    Operator = Operator.Add;
                    break;
                case '-':
                    Operator = Operator.Subtract;
                    break;
                case '*':
                    Operator = Operator.Multiply;
                    break;
                case '/':
                    Operator = Operator.Divide;
                    break;
                case '^':
                    Operator = Operator.Square;
                    break;
            }
        }
        public Operator Operator { get; }
        public void Accept(ITokenVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}