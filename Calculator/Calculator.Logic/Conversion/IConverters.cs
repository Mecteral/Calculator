using Calculator.Logic.Model.ConversionModel;

namespace Calculator.Logic.Conversion
{
    public interface IConverters
    {
        IConversionExpression Convert(IConversionExpression expression);
    }
}