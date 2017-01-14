namespace Calculator.Logic.Parsing.ConversionTokenizer
{
    public static class ConversionFactors
    {
        //Metric
        public const decimal MetricDivisionOneMillion = (decimal) 1.0E-6;
        public const decimal MetricDivisionOneThousand = (decimal) 1.0E-3;
        public const decimal MetricDivisionTenThousand = (decimal) 1.0E-4;
        public const decimal MetricDivisionOneHundred = (decimal) 1.0E-2;
        public const decimal MultiplicationByOne = 1;
        public const decimal MetricMultiplicationOneHundred = (decimal) 1.0E2;
        public const decimal MetricMultiplicationOneThousand = (decimal) 1.0E3;
        public const decimal MetricMultiplicationOneMillion = (decimal) 1.0E6;
        public const decimal MetricMultiplicationMeterToha = (decimal) 1.0E4;
        //Imperial Length
        public const decimal ThouToFeet = (decimal) 1/12000;
        public const decimal InchToFeet = (decimal) 1/12;
        public const decimal YardToFeet = 3;
        public const decimal ChainToFeet = 66;
        public const decimal FurlongToFeet = 660;
        public const decimal MileToFeet = 5280;
        public const decimal LeagueToFeet = 15840;
        public const decimal FathomToFeet = 608001;
        //Imperial Area
        public const decimal PerchToSquareFoot = (decimal) 272.25;
        public const decimal RoodToSquareFoot = 10890;
        public const decimal AcreToSquareFoot = 43560;
        //Imperial Volume
        public const decimal GillToFluidOunce = 5;
        public const decimal PintToFluidOunce = 20;
        public const decimal QuartToFluidOunce = 40;
        public const decimal GallonToFluidOunce = 160;
        //Imperial Mass
        public const decimal GrainToPound = (decimal) 1/7000;
        public const decimal DrachmToPound = (decimal) 1/256;
        public const decimal OunceToPound = (decimal) 1/16;
        public const decimal StoneToPound = 14;
        public const decimal HundredWeightToPound = 112;
        public const decimal ImperialTonToPound = 2240;
    }
}