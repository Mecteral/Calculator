using System.Data;
using Calculator.Logic.Facades;
using Calculator.Logic.Model.ConversionModel;
using Calculator.Logic.Parsing.ConversionTokenizer;

namespace Calculator.Logic.Conversion
{
    public class ReadableOutputCreator : IReadableOutputCreator
    {
        string Unit { get; set; }

        public string MakeReadable(IConversionExpressionWithValue expression)
        {
            var result = CreateUnitIfMetric(expression);
            if (result != null) return result;
            result = CreateUnitIfImperial(expression);
            return result;
        }

        static string CreateUnitIfImperial(IConversionExpressionWithValue expression)
        {
            if (expression is ImperialLengthExpression)
            {
                var readabilityCreator = new ImperialLengthReadabilityCreator();
                return readabilityCreator.MakeReadable(expression);
            }
            if (expression is ImperialAreaExpression)
            {
                var readabilityCreator =
                    new ImperialAreaReadabilityCreator();
                return readabilityCreator.MakeReadable(expression);
            }
            if (expression is ImperialVolumeExpression)
            {
                var readabilityCreator = new ImperialVolumeReadabilityCreator();
                return readabilityCreator.MakeReadable(expression);
            }
            if (expression is ImperialMassExpression)
            {
                var readabilityCreator = new ImperialMassReadabilityCreator();
                return readabilityCreator.MakeReadable(expression);
            }
            throw new InvalidExpressionException();
        }

        string CreateUnitIfMetric(IConversionExpressionWithValue expression)
        {
            if (expression is MetricLengthExpression)
            {
                if (expression.Value <= (decimal) 0.01)
                {
                    expression.Value /= ConversionFactors.MetricDivisionOneThousand;
                    Unit = UnitAbbreviations.Millimeters;
                }
                else if (expression.Value <= (decimal) 0.1)
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
                if (expression.Value <= (decimal) 1E-5)
                {
                    expression.Value /= ConversionFactors.MetricDivisionOneMillion;
                    Unit = UnitAbbreviations.Squaremillimeters;
                }
                else if (expression.Value <= (decimal) 1E-3)
                {
                    expression.Value /= ConversionFactors.MetricDivisionTenThousand;
                    Unit = UnitAbbreviations.Squarecentimeters;
                }
                else if (expression.Value >= (decimal) 1E5)
                {
                    expression.Value /= ConversionFactors.MetricMultiplicationOneMillion;
                    Unit = UnitAbbreviations.Squarekilometers;
                }
                else if (expression.Value >= (decimal) 1E3)
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
                if (expression.Value <= (decimal) 0.01)
                {
                    expression.Value /= ConversionFactors.MetricDivisionOneThousand;
                    Unit = UnitAbbreviations.Milliliters;
                }
                else if (expression.Value <= (decimal) 0.1)
                {
                    expression.Value /= ConversionFactors.MetricDivisionOneHundred;
                    Unit = UnitAbbreviations.Centiliters;
                }
                else if (expression.Value >= 100)
                {
                    expression.Value /= ConversionFactors.MetricMultiplicationOneHundred;
                    Unit = UnitAbbreviations.Hectoliters;
                }
                else
                {
                    Unit = UnitAbbreviations.Liters;
                }
            }
            else if (expression is MetricMassExpression)
            {
                if (expression.Value <= (decimal) 0.01)
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
}