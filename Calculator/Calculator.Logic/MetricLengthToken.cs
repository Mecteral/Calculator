namespace ImperialAndMetricConverter
{
    public class MetricLengthToken : AConversionTokens, IConversionToken
    {
        public MetricLengthToken(string asText, ApplicationArguments arg) : base(asText, arg) {}

        public void Accept(IConversionTokenVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
