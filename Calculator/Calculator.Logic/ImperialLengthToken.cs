namespace ImperialAndMetricConverter
{
    public class ImperialLengthToken : AConversionTokens, IConversionToken
    {
        public ImperialLengthToken(string asText, ApplicationArguments arg) : base(asText, arg) {}

        public void Accept(IConversionTokenVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
