﻿using System.IO;
using Newtonsoft.Json;

namespace Calculator.Logic
{
    public class JSonSerializer : IJSonSerializer
    {
        const string FilePath = "C:\\Users\\Public\\config.json";
        const string JsonConfigStart = "{}";
        WpfApplicationStatics mStatics;

        public JSonSerializer(WpfApplicationStatics statics)
        {
            mStatics = statics;
        }

        public void Read()
        {
            if (File.Exists(FilePath))
                mStatics = JsonConvert.DeserializeObject<WpfApplicationStatics>(File.ReadAllText(FilePath));
        }

        public void Write()
        {
            File.WriteAllText(FilePath, File.Exists(FilePath) ? JsonConvert.SerializeObject(mStatics) : JsonConfigStart);
        }
    }
}