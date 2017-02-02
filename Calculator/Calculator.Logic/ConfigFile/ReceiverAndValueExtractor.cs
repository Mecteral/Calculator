using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModernRonin.PraeterArtem.Functional;

namespace Calculator.Logic.ConfigFile
{
    public class ReceiverAndValueExtractor
    {
        public List<string> Receivers { get; private set; }
        public List<string> Values { get; private set; }
        public void ExtractReceiversAndValues(IEnumerable<string> config)
        {
            Receivers = new List<string>();
            Values = new List<string>();
            var tempReceiver = "";
            var tempValue = "";
            var valueStart = 0;
            foreach (var s in config)
            {
                var start = 0;
                if (s.IsEmpty())
                {
                    continue;
                }
                if (s[0] == '-' && s[1] == '-')
                {
                    start = 2;
                }
                else
                {
                    start = 1;
                }
                for (var i = start; i < s.Length; i++)
                {
                    if (char.IsLetter(s[i]))
                    {
                        tempReceiver += s[i];
                    }
                    else if (s[i] == '=' || s[i] == '+' || s[i] == '-')
                    {
                        valueStart = i;
                        break;
                    }
                }
                Receivers.Add(tempReceiver);
                tempReceiver = "";
                for (var i = valueStart; i < s.Length; i++)
                {
                    if (s[i] != '\"' && s[i] != '=')
                    {
                        tempValue += s[i];
                    }
                }
                Values.Add(tempValue);
                tempValue = "";
            }
        }
    }
}
