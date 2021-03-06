﻿namespace Calculator.Logic.ArgumentParsing
{
    public class ApplicationArguments : IApplicationArguments
    {
        public bool ToMetric { get; set; }
        public bool ToDegree { get; set; }
        public bool ShowSteps { get; set; }
        public bool WriteSwitchesToDefault { get; set; }
        public bool RevertConfig { get; set; }
        public bool UseConversion { get; set; }
        public bool? SaveAllOrIgnoreAllDifferingSwitches { get; set; }

        public string UnitForConversion { get; set; }
        public string ImportFromSpecificConfigFile { get; set; }
        public string UseCustomConfigFile { get; set; }
    }
}