namespace Calculator.Logic.Parsing.CalculationTokenizer
{
    public class TangentToken : AnTrigonometricToken, IToken
    {
        public TangentToken(string input) : base(input) {}

        public void Accept(ITokenVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}