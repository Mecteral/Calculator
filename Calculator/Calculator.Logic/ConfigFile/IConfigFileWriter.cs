using System.Collections.Generic;

namespace Calculator.Logic.ConfigFile
{
    public interface IConfigFileWriter
    {
        void Write(string pathToConfig, IEnumerable<string> newConfig);
        bool IsSwitchToWrite(string configReceiver, string configValue, string switchReceiver, string switchValue);
    }
}