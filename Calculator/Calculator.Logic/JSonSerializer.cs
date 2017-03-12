using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Calculator.Logic
{
    public class JSonSerializer : IJSonSerializer
    {
        WpfApplicationStatics mStatics;

        public JSonSerializer(WpfApplicationStatics statics)
        {
            mStatics = statics;
        }
        const string FilePath = "C:\\Users\\Public\\Calc\\config.json";
        public void Read()
        {
            if (File.Exists(FilePath))
                mStatics = JsonConvert.DeserializeObject<WpfApplicationStatics>(File.ReadAllText(FilePath));
        }

        public void Write()
        {
            if (File.Exists(FilePath))
                File.WriteAllText(FilePath, JsonConvert.SerializeObject(mStatics));
            else
                File.Create(FilePath);
        }
    }

    public interface IJSonSerializer
    {
        void Read();
        void Write();
    }
}
