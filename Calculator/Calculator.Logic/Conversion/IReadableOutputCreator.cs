using Calculator.Logic.Model.ConversionModel;

namespace Calculator.Logic.Conversion
{
    public interface IReadableOutputCreator
    {
        string MakeReadable(IConversionExpressionWithValue expression);
    }
}