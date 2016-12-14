namespace Calculator.Logic.Parsing
{
    /// <summary>
    /// ParenthesesToken if true => "(", else ")"
    /// </summary>
    public class ParenthesesToken : IToken
    {
        public bool IsOpening { get; private set; }
        public ParenthesesToken(string asText)
        {
            switch (asText)
            {
                case "(":
                    IsOpening = true;
                    break;
                case ")":
                    IsOpening = false;
                    break;
            }
        }
        public void Accept(ITokenVisitor visitor) { visitor.Visit(this); }
    }
}