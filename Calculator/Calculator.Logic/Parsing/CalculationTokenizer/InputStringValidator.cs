using System;
using System.Linq;
using ModernRonin.PraeterArtem.Functional;

namespace Calculator.Logic.Parsing.CalculationTokenizer
{
    public class InputStringValidator
    {
        static readonly Func<char, bool>[] sCharacterClassValidators =
        {
            IsConversionMarker, IsVariable, IsOperator,
            IsWhiteSpace, IsDigit, IsDecimal, IsParenthesis
        };
        int mFunctionEnd;
        string mInput;
        public void Validate(string input)
        {
            mInput = input;
            CheckForUnknownCharacters();
            CheckParanthesesCount();
            CheckForDoubleVariableAndFunction();
        }
        void CheckForUnknownCharacters() => mInput.UseIn(ValidateCharacter);
        static void ValidateCharacter(char c)
        {
            if (!IsValidCharacter(c)) throw new CalculationException("Unknown character in input", c);
        }
        static bool IsValidCharacter(char c) => sCharacterClassValidators.Any(v => v(c));
        static bool IsConversionMarker(char c) => c == '=' || c == '?';
        static bool IsParenthesis(char c) => c == '(' || c == ')';
        static bool IsDecimal(char c) => c == '.' || c == ',';
        static bool IsDigit(char c) => char.IsNumber(c);
        static bool IsWhiteSpace(char c) => char.IsWhiteSpace(c);
        static bool IsOperator(char c) => c == '-' || c == '+' || c == '*' || c == '/' || c == '^';
        static bool IsVariable(char c) => char.IsLetter(c);
        void CheckParanthesesCount()
        {
            var count = 0;
            var lastOpeningParenthesesIndex = 0;
            for (var i = 0; i < mInput.Length; i++)
            {
                var c = mInput[i];
                if (c == '(')
                {
                    count++;
                    lastOpeningParenthesesIndex = i;
                }
                else if (c == ')') { count--; }
            }
            if (count != 0) { throw new CalculationException("Uneven Parenthesescount", lastOpeningParenthesesIndex); }
        }
        string ExtractFuntionConditionsFromString(int start)
        {
            var result = "";
            if (mInput[start + 1] == ')') { throw new CalculationException("The Function has no Value", start); }
            for (var i = start + 1; i < mInput.Length; i++)
            {
                if (mInput[i] != ')') result += mInput[i];
                else
                {
                    mFunctionEnd = i;
                    break;
                }
            }
            return result;
        }
        static int CheckTrigonometricFunctions(string input)
        {
            var numberIsOver = false;
            var degOrRadDefined = false;
            for (var i = 0; i < input.Length; i++)
            {
                var c = input[i];
                if (char.IsLetter(c) && input.Length <= i + 2) { return i; }
                if (!degOrRadDefined && input.Length >= i + 2 && c == 'r' && input[i + 1] == 'a' && input[i + 2] == 'd' ||
                    c == 'd' && input[i + 1] == 'e' && input[i + 2] == 'g')
                {
                    numberIsOver = true;
                    degOrRadDefined = true;
                    i += 2;
                }
                else if (!numberIsOver && (char.IsNumber(c) || c == '.' || c == ',')) {}
                else if (degOrRadDefined) { return i; }
            }
            return 0;
        }
        static int CheckFunctionsWithValue(string input)
        {
            for (var i = 0; i < input.Length; i++)
            {
                var c = input[i];
                if (c != '.' && c != ',' && !char.IsNumber(c)) { return i; }
            }
            return 0;
        }
        void CheckForDoubleVariableAndFunction()
        {
            if (mInput.Length <= 1) return;
            for (var i = 1; i < mInput.Length; i++)
            {
                var c = mInput[i - 1];
                if (i + 2 < mInput.Length && c == 'c' && mInput[i] == 'o' && mInput[i + 1] == 's' &&
                    mInput[i + 2] == '(' || c == 's' && mInput[i] == 'i' && mInput[i + 1] == 'n' && mInput[i + 2] == '(' ||
                    c == 't' && mInput[i] == 'a' && mInput[i + 1] == 'n' && mInput[i + 2] == '(')
                {
                    var errorIndex = CheckTrigonometricFunctions(ExtractFuntionConditionsFromString(i + 2));
                    if (errorIndex != 0) {
                        throw new CalculationException("The Function was not properly defined", i + errorIndex);
                    }
                    i += mFunctionEnd;
                }
                else if (i + 3 < mInput.Length && c == 's' && mInput[i] == 'q' && mInput[i + 1] == 'r' &&
                         mInput[i + 2] == 't' && mInput[i + 3] == '(')
                {
                    var errorIndex = CheckFunctionsWithValue(ExtractFuntionConditionsFromString(i + 3));
                    if (errorIndex != 0) {
                        throw new CalculationException("The Function was not properly defined", i + errorIndex);
                    }
                    i += mFunctionEnd;
                }
                else if (char.IsLetter(c) && char.IsLetter(mInput[i])) {
                    throw new CalculationException("Variables need to be seperated with an Operator", i);
                }
            }
        }
    }
}