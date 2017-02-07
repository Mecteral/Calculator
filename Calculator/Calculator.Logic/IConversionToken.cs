namespace ImperialAndMetricConverter
{
    public interface IConversionToken
    {
        void Accept(IConversionTokenVisitor visitor);
    }
}
