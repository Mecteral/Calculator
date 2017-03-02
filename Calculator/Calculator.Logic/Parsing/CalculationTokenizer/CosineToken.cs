using Calculator.Logic.ArgumentParsing;

namespace Calculator.Logic.Parsing.CalculationTokenizer
{
    public class CosineToken : AnTrigonometricToken, IToken
    {
        public CosineToken(string input, IApplicationArguments args) : base(input, args) {}

        public void Accept(ITokenVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}