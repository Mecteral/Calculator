namespace Calculator.Logic.Parsing.CalculationTokenizer
{
    public class SinusToken : AnTrigonometricToken, IToken
    {
        public SinusToken(string input) : base(input) {}

        public void Accept(ITokenVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}