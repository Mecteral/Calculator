using Calculator.Logic.ArgumentParsing;

namespace Calculator.Logic.Parsing.CalculationTokenizer
{
    public class TangentToken : AnTrigonometricToken, IToken
    {
        public TangentToken(string input, ApplicationArguments args) : base(input, args) {}

        public void Accept(ITokenVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}