namespace ImperialAndMetricConverter
{
    public class ImperialVolumeToken : AConversionTokens, IConversionToken
    {
        public ImperialVolumeToken(string asText) : base(asText) {}
        public void Accept(IConversionTokenVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
