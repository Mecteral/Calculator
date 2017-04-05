using System;
using System.Linq;

namespace Calculator.Logic.Parsing.CalculationTokenizer
{
    public static class CharacterClasses
    {
        static readonly Func<char, bool>[] sCharacterClassValidators =
        {
            IsConversionMarker, IsVariable, IsOperator,
            IsWhiteSpace, IsDigit, IsDecimal, IsParenthesis
        };
        public static void ValidateCharacter(char c, int index)
        {
            if (!IsValidCharacter(c)) throw new CalculationException($"Unknown character '{c}' in input", index);
        }
        static bool IsValidCharacter(char c) => sCharacterClassValidators.Any(v => v(c));
        static bool IsConversionMarker(char c) => c == '=' || c == '?';
        static bool IsParenthesis(char c) => c == '(' || c == ')';
        static bool IsDecimal(char c) => c == '.' || c == ',';
        static bool IsDigit(char c) => char.IsNumber(c);
        static bool IsWhiteSpace(char c) => char.IsWhiteSpace(c);
        static bool IsOperator(char c) => c == '-' || c == '+' || c == '*' || c == '/' || c == '^';
        static bool IsVariable(char c) => char.IsLetter(c);
    }
}