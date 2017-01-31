using Calculator.Logic.Model.ConversionModel;

namespace Calculator.Logic.Conversion
{
    public interface IConverters
    {
        IConversionExpressionWithValue Convert(IConversionExpression expression);
    }
}