using System;
using System.Collections.Generic;
using System.IO;
using Calculator.Logic.ArgumentParsing;

namespace Calculator.Logic.ConfigFile
{
    public class SwitchesToConfigFileWriter
    {
        readonly List<string> mNewConfig = new List<string>();
        ApplicationArguments mArgs;
        List<string> mConfigReceiver = new List<string>();
        List<string> mConfigValues = new List<string>();
        List<string> mSwitchReceiver = new List<string>();
        List<string> mSwitchValues = new List<string>();

        public void WriteToConfigFile(IEnumerable<string> switches, IEnumerable<string> userConfigFile,
            ApplicationArguments args, string pathToUserFile)
        {
            mArgs = args;
            FillReceiversAndValues(switches, userConfigFile);
            FindDifferingArgumentsAndReplaceThemInValues();
            CreateNewConfig();
            File.WriteAllLines(pathToUserFile, mNewConfig);
        }

        void FillReceiversAndValues(IEnumerable<string> switches, IEnumerable<string> configFile)
        {
            var extractor = new ReceiverAndValueExtractor();
            extractor.ExtractReceiversAndValues(switches);
            mSwitchReceiver = extractor.Receivers;
            mSwitchValues = extractor.Values;
            extractor.ExtractReceiversAndValues(configFile);
            mConfigReceiver = extractor.Receivers;
            mConfigValues = extractor.Values;
        }

        void FindDifferingArgumentsAndReplaceThemInValues()
        {
            for (var i = 0; i < mSwitchReceiver.Count; i++)
            {
                for (var j = 0; j < mConfigReceiver.Count; j++)
                {
                    if (mSwitchReceiver[i] != mConfigReceiver[j]) continue;
                    if (mSwitchValues[i] == mConfigValues[j]) continue;
                    if (mArgs.SaveAllOrIgnoreAllDifferingSwitches == null)
                    {
                        Console.WriteLine(
                            $"Do you want to replace \" {mConfigReceiver[j]}={mConfigValues[j]} \" with \" {mSwitchReceiver[i]}={mSwitchValues[i]} \" ? \n Type y or yes to overwrite Config with switch.");
                        var answer = Console.ReadLine();
                        if (answer == "y" || answer == "yes")
                        {
                            mConfigValues[j] = mSwitchValues[i];
                        }
                    }
                    else if (mArgs.SaveAllOrIgnoreAllDifferingSwitches == true)
                    {
                        mConfigValues[j] = mSwitchValues[i];
                    }
                }
            }
        }

        void CreateNewConfig()
        {
            var temp = "";
            for (var i = 0; i < mConfigReceiver.Count; i++)
            {
                if (mConfigReceiver[i].Length > 1)
                {
                    temp += $"--{mConfigValues[i]} = \"{mConfigValues[i]}\"";
                }
                else
                {
                    temp += $"-{mConfigValues[i]} = \"{mConfigValues[i]}\"";
                }
                mNewConfig.Add(temp);
                temp = "";
            }
        }
    }
}