namespace ImperialAndMetricConverter
{
    public class MetricAreaToken : AConversionTokens, IConversionToken
    {
        public MetricAreaToken(string asText, string arg) : base(asText, arg) {}
        public void Accept(IConversionTokenVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
