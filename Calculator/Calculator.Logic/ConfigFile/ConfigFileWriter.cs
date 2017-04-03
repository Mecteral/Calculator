using System;
using System.Collections.Generic;
using System.IO;

namespace Calculator.Logic.ConfigFile
{
    public class ConfigFileWriter : IConfigFileWriter
    {
        public void Write(string pathToConfig, IEnumerable<string> newConfig)
        {
            File.WriteAllLines(pathToConfig, newConfig);
        }

        public bool IsSwitchToWrite(string configReceiver, string configValue, string switchReceiver, string switchValue)
        {
            Console.WriteLine(
                $"Do you want to replace \" {configReceiver}={configValue} \" with \" {switchReceiver}={switchValue} \" ? \n Type y or yes to overwrite Config with switch.");
            var answer = Console.ReadLine();
            return answer == "y" || answer == "yes";
        }
    }
}