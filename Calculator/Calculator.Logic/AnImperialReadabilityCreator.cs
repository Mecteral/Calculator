using System;
using System.Collections.Generic;

namespace ImperialAndMetricConverter
{
    public abstract class AnImperialReadabilityCreator : IImperialReadabilityCreator
    {
        string mResult;
        int mUnitCount;
        decimal mValue;

        public string CreateFrom(decimal value, string baseUnit, IList<decimal> conversionFactors,
            IList<string> abbreviations)
        {
            mValue = value;
            for (var i = 0; i < conversionFactors.Count; i++)
            {
                if (abbreviations[i] == baseUnit)
                {
                    mResult += $" {Math.Truncate(mValue)} {baseUnit}";
                    mValue -= Math.Truncate(mValue);
                }
                else
                {
                    while (mValue >= conversionFactors[i])
                    {
                        mUnitCount++;
                        mValue -= conversionFactors[i];
                    }
                    if (mUnitCount > 0)
                    {
                        mResult += " " + mUnitCount + " " + abbreviations[i];
                    }
                    mUnitCount = 0;
                }
            }
            return mResult;
        }
    }
}