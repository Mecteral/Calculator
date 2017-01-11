namespace Calculator.Logic.Parsing.CalculationTokenizer
{
    /// <summary>
    /// ParenthesesToken if true => "(", else ")"
    /// </summary>
    public class ParenthesesToken : IToken
    {
        public ParenthesesToken(char asText)
        {
            switch (asText)
            {
                case '(':
                    IsOpening = true;
                    break;
                case ')':
                    IsOpening = false;
                    break;
            }
        }
        public bool IsOpening { get; private set; }
        public void Accept(ITokenVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}