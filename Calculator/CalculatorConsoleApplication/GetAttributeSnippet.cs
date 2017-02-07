using System.Linq;

namespace Calculator.Logic.Parsing.ConversionTokenizer
{
    public static class GetAttributeSnippet
    {
        public static string Do()
        {
            var abbreviations = $"\n{"Units",4} {"Abbreviations",32}\n\n";
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
                abbreviations+= $"{fieldNames.ElementAt(i),-24}\"{fieldValues.ElementAt(i)}\"\n";
            }
            return abbreviations;
        }
    }
}