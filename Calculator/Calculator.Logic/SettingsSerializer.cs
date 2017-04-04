using System.IO;
using Newtonsoft.Json;

namespace Calculator.Logic
{
    public static class SettingsSerializer
    {
        const string FilePath = "C:\\Users\\Public\\config.json";
        const string JsonConfigStart = "{\"UsedWpfTheme\":\"Themes\\\\BureauBlack.xaml\"}";
        public static IAllSerializableSettings Read()
        {
            if (!File.Exists(FilePath)) File.WriteAllText(FilePath, JsonConfigStart);
            return JsonConvert.DeserializeObject<AllSerializableSettings>(File.ReadAllText(FilePath));
        }
        public static void Write(IAllSerializableSettings settings)
            => File.WriteAllText(FilePath, JsonConvert.SerializeObject(settings));
    }
}