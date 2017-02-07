namespace ImperialAndMetricConverter
{
    public class MetricAreaToken : AConversionTokens, IConversionToken
    {
        public MetricAreaToken(string asText) : base(asText) {}
        public void Accept(IConversionTokenVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
