using System.Collections.Generic;

namespace ImperialAndMetricConverter
{
    public interface IConversionTokenizer
    {
        IEnumerable<IConversionToken> Tokens { get; }
        void Tokenize(string input, ApplicationArguments arg);
    }
}