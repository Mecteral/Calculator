using System.IO;
using Calculator.Logic.ArgumentParsing;
using Fclp;

namespace Calculator.Logic.CommandLineParser
{
    public static class ConfigFileReader
    {
        const string Path = @"C:\Users\Public\Calc\ConfigFileCalculator.txt";

        public static string[] ReadFile()
        {
            if (File.Exists(Path))
            {
                var config = File.ReadAllLines(Path);
                return config;
            }
            return null;
        }
    }
}