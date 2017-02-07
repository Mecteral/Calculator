namespace ImperialAndMetricConverter
{
    public class ImperialLengthToken : AConversionTokens, IConversionToken
    {
        public ImperialLengthToken(string asText) : base(asText) {}

        public void Accept(IConversionTokenVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
