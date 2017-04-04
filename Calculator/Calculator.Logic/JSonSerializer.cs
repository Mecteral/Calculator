using System.IO;
using Newtonsoft.Json;

namespace Calculator.Logic
{
    public class JSonSerializer : IJSonSerializer
    {
        const string FilePath = "C:\\Users\\Public\\config.json";
        const string JsonConfigStart = "{\"UsedWpfTheme\":\"Themes\\\\BureauBlack.xaml\"}";
        IAllSerializableSettings mSettings;

        public JSonSerializer(IAllSerializableSettings settings)
        {
            mSettings = settings;
        }

        public void Read()
        {
            if (File.Exists(FilePath))
                mSettings = JsonConvert.DeserializeObject<AllSerializableSettings>(File.ReadAllText(FilePath));
            else
            {
                File.WriteAllText(FilePath, JsonConfigStart);
                mSettings = JsonConvert.DeserializeObject<AllSerializableSettings>(File.ReadAllText(FilePath));
            }
        }

        public void Write() => File.WriteAllText(FilePath, JsonConvert.SerializeObject(mSettings));
    }
}