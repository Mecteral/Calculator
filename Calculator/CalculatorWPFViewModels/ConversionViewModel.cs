using System.Collections.Generic;
using System.Linq;
using Calculator.Logic.ArgumentParsing;
using Caliburn.Micro;
using Mecteral.UnitConversion;

namespace CalculatorWPFViewModels
{
    public class ConversionViewModel : Screen
    {
        public List<UnitAbbreviationsAndNames> MetricalMasses { get; set; } = new List<UnitAbbreviationsAndNames>();
        public List<UnitAbbreviationsAndNames> MetricalVolumes { get; set; } = new List<UnitAbbreviationsAndNames>();
        public List<UnitAbbreviationsAndNames> MetricalAreas { get; set; } = new List<UnitAbbreviationsAndNames>();
        public List<UnitAbbreviationsAndNames> MetricalLengths { get; set; } = new List<UnitAbbreviationsAndNames>();
        public List<UnitAbbreviationsAndNames> ImperialMasses { get; set; } = new List<UnitAbbreviationsAndNames>();
        public List<UnitAbbreviationsAndNames> ImperialVolumes { get; set; } = new List<UnitAbbreviationsAndNames>();
        public List<UnitAbbreviationsAndNames> ImperialAreas { get; set; } = new List<UnitAbbreviationsAndNames>();
        public List<UnitAbbreviationsAndNames> ImperialLengths { get; set; } = new List<UnitAbbreviationsAndNames>();

        readonly IApplicationArguments mArguments;

        public ConversionViewModel(IApplicationArguments arguments)
        {
            mArguments = arguments;
            SetListsForView();
        }
        protected string RadioButtonGroupName { get; set; }

        public void SetUnitAbbreviation(string abbreviation)
        {
            mArguments.UnitForConversion = abbreviation;
        }

        void SetListsForView()
        {
            var abbreviation = new UnitAbbreviations();
            var fieldValues = abbreviation.GetType()
                .GetFields()
                .Where(fi => fi.IsLiteral && !fi.IsInitOnly)
                .Select(field => field.GetValue(null))
                .ToList();
            var fieldNames = typeof(UnitAbbreviations).GetFields()
                .Where(fi => fi.IsLiteral && !fi.IsInitOnly)
                .Select(field => field.Name)
                .ToList();
            for (int i = 0; i < fieldValues.Count; i++)
            {
                var unitAbbreviationAndName = new UnitAbbreviationsAndNames();
                unitAbbreviationAndName.Name = fieldNames.ElementAt(i);
                unitAbbreviationAndName.Abbreviation = fieldValues.ElementAt(i).ToString();
                var value = fieldValues[i];
                if (UnitAbbreviations.MetricAreas.Contains(value))
                {
                    MetricalAreas.Add(unitAbbreviationAndName);
                }
                else if(UnitAbbreviations.MetricLengths.Contains(value))
                {
                    MetricalLengths.Add(unitAbbreviationAndName);
                }
                else if (UnitAbbreviations.MetricMasses.Contains(value))
                {
                    MetricalMasses.Add(unitAbbreviationAndName);
                }
                else if (UnitAbbreviations.MetricVolumes.Contains(value))
                {
                    MetricalVolumes.Add(unitAbbreviationAndName);
                }
                else if (UnitAbbreviations.ImperialAreas.Contains(value))
                {
                    ImperialAreas.Add(unitAbbreviationAndName);
                }
                else if (UnitAbbreviations.ImperialLengths.Contains(value))
                {
                    ImperialLengths.Add(unitAbbreviationAndName);
                }
                else if (UnitAbbreviations.ImperialMasses.Contains(value))
                {
                    ImperialMasses.Add(unitAbbreviationAndName);
                }
                else if (UnitAbbreviations.ImperialVolumes.Contains(value))
                {
                    ImperialVolumes.Add(unitAbbreviationAndName);
                }
            }
        }
    }

    public class UnitAbbreviationsAndNames
    {
        public  string Abbreviation { get; set; }
        public  string Name { get; set; }
    }

}