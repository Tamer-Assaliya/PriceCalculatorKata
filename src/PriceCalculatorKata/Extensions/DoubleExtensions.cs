using System;

namespace PriceCalculatorKata
{
    public static class DoubleExtensions
    {
        public static double CastPercentage(this double value)
        {
            if (value < 0 && value > 100)
                throw new Exception("Percentage is not valid");
            return value / 100.0;
        }
    }
}