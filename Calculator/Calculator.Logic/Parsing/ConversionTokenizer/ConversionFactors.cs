namespace Calculator.Logic.Parsing.ConversionTokenizer
{
    public static class ConversionFactors
    {
        //Metric
        public const decimal MetricDivisionOneMillion = 1.0E-6M;
        public const decimal MetricDivisionOneThousand = 1.0E-3M;
        public const decimal MetricDivisionTenThousand = 1.0E-4M;
        public const decimal MetricDivisionOneHundred = 1.0E-2M;
        public const decimal MultiplicationByOne = 1;
        public const decimal MetricMultiplicationOneHundred = 1.0E2M;
        public const decimal MetricMultiplicationOneThousand = 1.0E3M;
        public const decimal MetricMultiplicationOneMillion = 1.0E6M;
        public const decimal MetricMultiplicationMeterToha = 1.0E4M;
        //Imperial Length
        public const decimal ThouToFeet = 1M/12000;
        public const decimal InchToFeet = 1M/12;
        public const decimal YardToFeet = 3;
        public const decimal ChainToFeet = 66;
        public const decimal FurlongToFeet = 660;
        public const decimal MileToFeet = 5280;
        public const decimal LeagueToFeet = 15840;
        public const decimal FathomToFeet = 608001;
        //Imperial Area
        public const decimal PerchToSquareFoot = 272.25M;
        public const decimal RoodToSquareFoot = 10890;
        public const decimal AcreToSquareFoot = 43560;
        //Imperial Volume
        public const decimal GillToFluidOunce = 5;
        public const decimal PintToFluidOunce = 20;
        public const decimal QuartToFluidOunce = 40;
        public const decimal GallonToFluidOunce = 160;
        //Imperial Mass
        public const decimal GrainToPound = 1M/7000;
        public const decimal DrachmToPound = 1M/256;
        public const decimal OunceToPound = 1M/16;
        public const decimal StoneToPound = 14;
        public const decimal HundredWeightToPound = 112;
        public const decimal ImperialTonToPound = 2240;
    }
}