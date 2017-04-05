using System.Collections.Generic;

namespace Calculator.WPF.ViewModels
{
    public interface IUnitsAndAbbreviationsSource
    {
        List<List<UnitAbbreviationsAndNames>> AllUnitsAndAbbreviations { get; }
    }
}