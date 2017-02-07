namespace ImperialAndMetricConverter
{
    public class MetricMassToken : AConversionTokens, IConversionToken
    {
        public MetricMassToken(string asText) : base(asText) {}
        public void Accept(IConversionTokenVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
