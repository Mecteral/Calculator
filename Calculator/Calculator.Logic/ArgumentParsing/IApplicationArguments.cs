namespace Calculator.Logic.ArgumentParsing
{
    public interface IApplicationArguments
    {
        bool ToDegree { get; set; }
        bool ShowSteps { get; set; }
        bool WriteSwitchesToDefault { get; set; }
        bool RevertConfig { get; set; }
        bool UseConversion { get; set; }
        bool? SaveAllOrIgnoreAllDifferingSwitches { get; set; }

        string UnitForConversion { get; set; }
        string ImportFromSpecificConfigFile { get; set; }
        string UseCustomConfigFile { get; set; }
    }
}