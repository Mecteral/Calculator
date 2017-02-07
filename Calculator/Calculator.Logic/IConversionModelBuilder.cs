using System.Collections.Generic;

namespace ImperialAndMetricConverter
{
    public interface IConversionModelBuilder
    {
        IConversionExpression BuildFrom(IEnumerable<IConversionToken> tokens);
    }
}