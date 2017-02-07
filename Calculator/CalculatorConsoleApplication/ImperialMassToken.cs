namespace ImperialAndMetricConverter
{
    public class ImperialMassToken : AConversionTokens, IConversionToken
    {
        public ImperialMassToken(string asText) : base(asText) {}
        public void Accept(IConversionTokenVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
