using System.Collections.Generic;
using System.Linq;
using Calculator.Logic.WpfApplicationProperties;
using Caliburn.Micro;
using Mecteral.UnitConversion;

namespace Calculator.WPF.ViewModels
{
    public class ConversionViewModel : PropertyChangedBase, IUnitsAndAbbreviationsSource
    {
        readonly IConversionProperties mConversionProperties;
        readonly IEventAggregator mEventAggregator;
        readonly IWindowProperties mWindowProperties;
        bool mToImperial;
        bool mToMetric;
        bool mUnitExpander;

        public ConversionViewModel(IEventAggregator eventAggregator, IWindowProperties windowProperties,
            IConversionProperties conversionProperties)
        {
            mEventAggregator = eventAggregator;
            mWindowProperties = windowProperties;
            mConversionProperties = conversionProperties;
            SetListsForView();
            AllUnitsAndAbbreviations = new List<List<UnitAbbreviationsAndNames>>
            {
                MetricalMasses,
                MetricalAreas,
                MetricalLengths,
                MetricalVolumes,
                ImperialMasses,
                ImperialAreas,
                ImperialLengths,
                ImperialVolumes
            };
            SetUnitOnStartup();
            mToMetric = mConversionProperties.DoUseMetricSystem;
            mUnitExpander = mWindowProperties.AreUnitsExpanded;
        }

        public bool UnitExpander
        {
            get { return mUnitExpander; }
            set
            {
                if (value == mUnitExpander) return;
                mUnitExpander = value;
                NotifyOfPropertyChange(() => UnitExpander);
                mWindowProperties.AreUnitsExpanded = value;
                mEventAggregator.PublishOnUIThread("Resize");
            }
        }

        public bool ToMetric
        {
            get { return mToMetric; }
            set
            {
                if (value == mToMetric) return;
                mToMetric = value;
                NotifyOfPropertyChange(() => ToMetric);
                SetUseMetric();
            }
        }

        public bool ToImperial
        {
            get { return mToImperial; }
            set
            {
                if (value == mToImperial) return;
                mToImperial = value;
                NotifyOfPropertyChange(() => ToImperial);
                SetUseMetric();
            }
        }

        public List<UnitAbbreviationsAndNames> MetricalMasses { get; set; } = new List<UnitAbbreviationsAndNames>();
        public List<UnitAbbreviationsAndNames> MetricalVolumes { get; set; } = new List<UnitAbbreviationsAndNames>();
        public List<UnitAbbreviationsAndNames> MetricalAreas { get; set; } = new List<UnitAbbreviationsAndNames>();
        public List<UnitAbbreviationsAndNames> MetricalLengths { get; set; } = new List<UnitAbbreviationsAndNames>();
        public List<UnitAbbreviationsAndNames> ImperialMasses { get; set; } = new List<UnitAbbreviationsAndNames>();
        public List<UnitAbbreviationsAndNames> ImperialVolumes { get; set; } = new List<UnitAbbreviationsAndNames>();
        public List<UnitAbbreviationsAndNames> ImperialAreas { get; set; } = new List<UnitAbbreviationsAndNames>();
        public List<UnitAbbreviationsAndNames> ImperialLengths { get; set; } = new List<UnitAbbreviationsAndNames>();

        protected string RadioButtonGroupName { get; set; }

        public List<List<UnitAbbreviationsAndNames>> AllUnitsAndAbbreviations { get; set; }

        void SetUseMetric()
        {
            mConversionProperties.DoUseMetricSystem = ToMetric;
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
            for (var i = 0; i < fieldValues.Count; i++)
            {
                var unitAbbreviationAndName = new UnitAbbreviationsAndNames();
                unitAbbreviationAndName.Name = fieldNames.ElementAt(i);
                unitAbbreviationAndName.Abbreviation = fieldValues.ElementAt(i).ToString();
                var value = fieldValues[i];
                if (UnitAbbreviations.MetricAreas.Contains(value))
                {
                    MetricalAreas.Add(unitAbbreviationAndName);
                }
                else if (UnitAbbreviations.MetricLengths.Contains(value))
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

        void SetUnitOnStartup()
        {
            if (mConversionProperties.LastPickedUnit == null) return;
            AllUnitsAndAbbreviations
                .SelectMany(l => l)
                .First(u => u.Abbreviation == mConversionProperties.LastPickedUnit)
                .IsSelected = true;
        }
    }
}