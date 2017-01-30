using Calculator.Logic.Model.ConversionModel;

namespace Calculator.Logic.Facades
{
    public interface IReadableOutputCreator
    {
        string MakeReadable(IConversionExpressionWithValue expression);
    }
}