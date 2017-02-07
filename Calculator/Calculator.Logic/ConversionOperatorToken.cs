namespace ImperialAndMetricConverter
{
    public class ConversionOperatorToken : IConversionToken
    {
        public ConversionOperatorToken(char asText)
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
            }
        }
        public Operator Operator { get; }
        public void Accept(IConversionTokenVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
