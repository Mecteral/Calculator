using System.Collections.Generic;

namespace Calculator.Logic
{
    public class ArgumentParser
    {
        static IList<string> _moutputList = new List<string>();

        public static IEnumerable<string> Tokenize(IEnumerable<string> inputArrayStrings)
        {
            _moutputList = DeleteWhitespacesOfListAndAddBracketsAroundTheInput(inputArrayStrings);
            for (var operatorIndex = 0; operatorIndex < _moutputList.Count; operatorIndex++)
            {
                if (_moutputList[operatorIndex] == "*" ||
                    _moutputList[operatorIndex] == "/")
                {
                    if (_moutputList[operatorIndex - 1] == ")" &&
                        _moutputList[operatorIndex + 1] == "(")
                        continue;
                    CheckIfBracketsNeedToBeAdded(operatorIndex);
                    operatorIndex++;
                }
            }
            return _moutputList;
        }
        static IList<string> DeleteWhitespacesOfListAndAddBracketsAroundTheInput(IEnumerable<string> inputArrayStrings)
        {
            IList<string> outputList = new List<string>();
            foreach (var t in inputArrayStrings)
            {
                if (string.IsNullOrWhiteSpace(t)) {}
                else
                {
                    outputList.Add(t);
                }
            }
            outputList.Insert(0, "(");
            outputList.Add(")");
            return outputList;
        }

        static void CheckIfBracketsNeedToBeAdded(int operatorIndex)
        {
            if (_moutputList[operatorIndex + 1] == "(")
            {
                AddParathesesBeforeBracket(operatorIndex);
            }
            else if (_moutputList[operatorIndex - 1] == ")")
            {
                AddParathesesAfterBracket(operatorIndex);
            }

            else if (_moutputList[operatorIndex - 2] != "(" ||
                     _moutputList[operatorIndex + 2] != ")")
            {
                AddBracketsAroundOperatorValues(operatorIndex);
            }
        }

        static void AddParathesesBeforeBracket(int operatorIndex)
        {
            var bracketCount = 0;
            _moutputList.Insert(operatorIndex - 1, "(");
            operatorIndex++;
            for (var i = operatorIndex + 1; i < _moutputList.Count; i++)
            {
                if (_moutputList[i] == "(")
                    bracketCount++;
                else if (_moutputList[i] == ")")
                    bracketCount--;
                if (bracketCount != 0) continue;
                _moutputList.Insert(i, ")");
                break;
            }
        }

        static void AddParathesesAfterBracket(int operatorIndex)
        {
            var bracketCount = 0;
            _moutputList.Insert(operatorIndex + 2, ")");
            for (var i = operatorIndex - 1; i > 0; i--)
            {
                if (_moutputList[i] == ")")
                    bracketCount++;
                if (_moutputList[i] == "(")
                    bracketCount--;
                if (bracketCount != 0) continue;
                _moutputList.Insert(i, "(");
                break;
            }
        }

        static void AddBracketsAroundOperatorValues(int operatorIndex)
        {
            _moutputList.Insert(operatorIndex + 2, ")");
            _moutputList.Insert(operatorIndex - 1, "(");
        }
    }
}