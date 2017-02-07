namespace ImperialAndMetricConverter
{
    public class ImperialAreaToken : AConversionTokens, IConversionToken
    {
        public ImperialAreaToken(string asText) : base(asText) {}
        public void Accept(IConversionTokenVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
