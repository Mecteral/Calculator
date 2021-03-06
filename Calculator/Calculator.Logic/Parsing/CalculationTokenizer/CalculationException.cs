﻿using System;

namespace Calculator.Logic.Parsing.CalculationTokenizer
{
    public class CalculationException : Exception
    {
        public int Index { get; }
        public CalculationException(string error, int index) : base(error)
        {
            Index = index;
        }
    }
}