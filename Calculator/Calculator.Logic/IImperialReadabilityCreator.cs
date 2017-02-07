using System.Collections.Generic;

namespace ImperialAndMetricConverter
{
    public interface IImperialReadabilityCreator
    {
        string CreateFrom(decimal value, string baseUnit, IList<decimal> conversionFactors, IList<string> abbreviations);
    }
}