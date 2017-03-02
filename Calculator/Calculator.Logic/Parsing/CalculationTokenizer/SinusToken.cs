using Calculator.Logic.ArgumentParsing;

namespace Calculator.Logic.Parsing.CalculationTokenizer
{
    public class SinusToken : AnTrigonometricToken, IToken
    {
        public SinusToken(string input, IApplicationArguments args) : base(input, args) {}

        public void Accept(ITokenVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}