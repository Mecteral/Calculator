namespace ImperialAndMetricConverter
{
    public class MetricVolumeToken : AConversionTokens, IConversionToken
    {
        public MetricVolumeToken(string asText) : base(asText) {}
        public void Accept(IConversionTokenVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
