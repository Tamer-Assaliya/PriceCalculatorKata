using System;

namespace PriceCalculatorKata
{
    abstract class Calculations
    {
        public Product Product { get; set; }

        public abstract double GetTotal();

        public double ComputeValueByType(ValueComputationType valueComputationType, double value)
        {
            double result = 0;
            switch (valueComputationType)
            {
                case ValueComputationType.Absolute:
                    result = value;
                    break;
                case ValueComputationType.PriceRelative:
                    var percentage = value.CastPercentage();
                    result = percentage * Product.Price;
                    break;
                default: break;
            }
            return Math.Round(result, 4);
        }
    }
}