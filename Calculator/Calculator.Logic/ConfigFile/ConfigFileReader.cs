using System.Collections.Generic;
using System.IO;
using System.Linq;
using Calculator.Logic.CommandLineParser;
using Calculator.Logic.Parsing.ConversionTokenizer;
using ModernRonin.PraeterArtem.Functional;

namespace Calculator.Logic.ConfigFile
{
    public class ConfigFileReader
    {
        public string FilePath { get; set; }
        public List<string> Errors { get; private set; } = new List<string>();
        public string[] ReadFile(string path)
        {
            FilePath = path;
            if (File.Exists(path))
            {
                var config = File.ReadAllLines(path);
                var validator = new ConfigFileValidator();
                Errors = validator.CheckForValidation(config);
                return config;
            }
            return new string[0];
        }
    }
}