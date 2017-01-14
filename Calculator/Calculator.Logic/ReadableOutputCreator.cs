using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Calculator.Logic.Model.ConversionModel;
using Calculator.Logic.Parsing.ConversionTokenizer;

namespace Calculator.Logic
{
    public class ReadableOutputCreator
    {
        public string Unit { get; set; }

        public string MakeReadable(IConversionExpressionWithValue expression)
        {
            var result = CreateUnitIfMetric(expression);
            if (result==null)
            {
                var readabilityCreator = new ImperialMassReadabilityCreator();
                result = readabilityCreator.MakeReadable(expression);
            }
            return result;
        }
        string CreateUnitIfMetric(IConversionExpressionWithValue expression)
        {
            if (expression is MetricLengthExpression)
            {
                if (expression.Value <= (decimal)0.01)
                {
                    expression.Value /= ConversionFactors.MetricDivisionOneThousand;
                    Unit = UnitAbbreviations.Millimeters;
                }
                else if (expression.Value <= (decimal)0.1)
                {
                    expression.Value /= ConversionFactors.MetricDivisionOneHundred;
                    Unit = UnitAbbreviations.Centimeters;
                }
                else if (expression.Value > 999)
                {
                    expression.Value /= ConversionFactors.MetricMultiplicationOneThousand;
                    Unit = UnitAbbreviations.Kilometers;
                }
                else
                {
                    Unit = UnitAbbreviations.Meters;
                }
            }
            else if (expression is MetricAreaExpression)
            {
                if (expression.Value <= (decimal)1E-5)
                {
                    expression.Value /= ConversionFactors.MetricDivisionOneMillion;
                    Unit = UnitAbbreviations.Squaremillimeters;
                }
                else if (expression.Value <= (decimal)1E-3)
                {
                    expression.Value /= ConversionFactors.MetricDivisionTenThousand;
                    Unit = UnitAbbreviations.Squarecentimeters;
                }
                else if (expression.Value >= (decimal)1E5)
                {
                    expression.Value /= ConversionFactors.MetricMultiplicationOneMillion;
                    Unit = UnitAbbreviations.Squarekilometers;
                }
                else if (expression.Value >= (decimal)1E3)
                {
                    expression.Value /= ConversionFactors.MetricMultiplicationMeterToha;
                    Unit = UnitAbbreviations.Hectas;
                }
                else
                {
                    Unit = UnitAbbreviations.Sqauremeters;
                }
            }
            else if (expression is MetricVolumeExpression)
            {
                if (expression.Value <= (decimal)0.01)
                {
                    expression.Value /= ConversionFactors.MetricDivisionOneThousand;
                    Unit = UnitAbbreviations.Milliliters;
                }
                else if (expression.Value <= (decimal)0.1)
                {
                    expression.Value /= ConversionFactors.MetricDivisionOneHundred;
                    Unit = UnitAbbreviations.Centiliters;
                }
                else if (expression.Value >= 10)
                {
                    expression.Value /= ConversionFactors.MetricDivisionOneHundred;
                    Unit = UnitAbbreviations.Hectoliters;
                }
                else
                {
                    Unit = UnitAbbreviations.Liters;
                }
            }
            else if (expression is MetricMassExpression)
            {
                if (expression.Value <= (decimal)0.01)
                {
                    expression.Value /= ConversionFactors.MetricDivisionOneThousand;
                    Unit = UnitAbbreviations.Milligram;
                }
                else if (expression.Value >= 999999)
                {
                    expression.Value /= ConversionFactors.MetricMultiplicationOneMillion;
                    Unit = UnitAbbreviations.Ton;
                }
                else if (expression.Value >= 999)
                {
                    expression.Value /= ConversionFactors.MetricMultiplicationOneThousand;
                    Unit = UnitAbbreviations.Kilogram;
                }
                else
                {
                    Unit = UnitAbbreviations.Gram;
                }
            }
            if (Unit != null)
            {
                return $"{expression.Value} {Unit}";
            }
            return null;
        }
    }
    

    public class ImperialMassReadabilityCreator
    {
        string mResult;
        decimal mValue;
        int mGrainCount;
        int mDrachmCount;
        int mOunzeCount;
        int mStoneCount;
        int mHundredWeightCount;
        int mTonCount;
        void CreateCountsFromValue(IConversionExpressionWithValue expression)
        {
            while (mValue > ConversionFactors.ImperialTonToPound)
            {
                mValue -= ConversionFactors.ImperialTonToPound;
                mTonCount += 1;
            }
            while (mValue > ConversionFactors.ImperialTonToPound)
            {
                mValue -= ConversionFactors.HundredWeightToPound;
                mHundredWeightCount += 1;
            }
            while (mValue > ConversionFactors.StoneToPound)
            {
                mValue -= ConversionFactors.StoneToPound;
                mStoneCount += 1;
            }
            while (mValue < ConversionFactors.GrainToPound)
            {
                mValue += ConversionFactors.GrainToPound;
                mGrainCount += 1;
            }
            while (mValue < ConversionFactors.DrachmToPound)
            {
                mValue += ConversionFactors.DrachmToPound;
                mDrachmCount += 1;
            }
            while (mValue < ConversionFactors.OunceToPound)
            {
                mValue += ConversionFactors.OunceToPound;
                mOunzeCount += 1;
            }
        }

        public string MakeReadable(IConversionExpressionWithValue expression)
        {
            mValue = expression.Value;
            CreateCountsFromValue(expression);
            if (mTonCount != 0)
            {
                mResult += $"{mTonCount}{UnitAbbreviations.ImperialTon}";
            }
            if (mHundredWeightCount != 0)
            {
                mResult += $" {mHundredWeightCount}{UnitAbbreviations.HundredWeight}";
            }
            if (mStoneCount != 0)
            {
                mResult += $" {mStoneCount}{UnitAbbreviations.Stone}";
            }
            if (mValue != 0)
            {
                mResult += $" {mValue}{UnitAbbreviations.Pound}";
            }
            if (mOunzeCount != 0)
            {
                mResult += $" {mOunzeCount}{UnitAbbreviations.Ounce}";
            }
            if (mDrachmCount != 0)
            {
                mResult += $" {mDrachmCount}{UnitAbbreviations.Drachm}";
            }
            if (mGrainCount!=0)
            {
                mResult += $" {mGrainCount}{UnitAbbreviations.Grain}";
            }
            return mResult;
        }
    }
}
