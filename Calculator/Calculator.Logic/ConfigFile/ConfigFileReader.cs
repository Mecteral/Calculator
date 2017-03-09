using System.IO;

namespace Calculator.Logic.ConfigFile
{
    public class ConfigFileReader
    {
        public string[] ReadFile(string path)
        {
            if (File.Exists(path))
            {
                var config = File.ReadAllLines(path);
                return config;
            }
            return new string[0];
        }
    }
}