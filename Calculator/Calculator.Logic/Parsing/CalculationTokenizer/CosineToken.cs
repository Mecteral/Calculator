namespace Calculator.Logic.Parsing.CalculationTokenizer
{
    public class CosineToken : AnTrigonometricToken, IToken
    {
        public CosineToken(string input) : base(input) {}

        public void Accept(ITokenVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}