using System.Text.RegularExpressions;

namespace Calculator.Logic.Utilities
{
    public static class StringExtensions
    {
        public static string WithoutAnyWhitespace(this string self)
        {
            return Regex.Replace(self, @"\s+", string.Empty);
        }
    }
}