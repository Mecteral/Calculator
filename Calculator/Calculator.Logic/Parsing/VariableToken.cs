namespace Calculator.Logic.Parsing
{
    /// <summary>
    /// Contains alphabetized <string> string </string> for Variables and double Value for calculations
    /// </summary>
    public class VariableToken : IToken
    {
        public VariableToken(char asText)
        {
            Variable += asText;
        }
        public string Variable { get; private set; }
        public void Accept(ITokenVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}