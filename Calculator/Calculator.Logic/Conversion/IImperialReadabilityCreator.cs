using System.Collections.Generic;

namespace Calculator.Logic.Conversion
{
    public interface IImperialReadabilityCreator
    {
        string CreateFrom(decimal value, string baseUnit, IList<decimal> conversionFactors, IList<string> abbreviations);
    }
}