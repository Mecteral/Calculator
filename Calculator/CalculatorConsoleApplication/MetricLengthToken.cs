namespace ImperialAndMetricConverter
{
    public class MetricLengthToken : AConversionTokens, IConversionToken
    {
        public MetricLengthToken(string asText) : base(asText) {}

        public void Accept(IConversionTokenVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
