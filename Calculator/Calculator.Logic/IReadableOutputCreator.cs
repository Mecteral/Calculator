namespace ImperialAndMetricConverter
{
    public interface IReadableOutputCreator
    {
        string MakeReadable(IConversionExpressionWithValue expression);
    }
}