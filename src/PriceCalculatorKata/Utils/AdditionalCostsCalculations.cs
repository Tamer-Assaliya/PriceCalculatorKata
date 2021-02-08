using System;
using System.Collections.Generic;
using System.Linq;
namespace PriceCalculatorKata
{
    class AdditionalCostsCalculations
    {
        public Dictionary<String, double> AdditionalCosts { private set; get; } = new Dictionary<String, double>();
        public Product Product { get; set; }

        public void AssignAdditionalCost(ValueComputationType valueComputationType, String key, double value)
        {
            switch (valueComputationType)
            {
                case ValueComputationType.Absolute:
                    AdditionalCosts[key] = Math.Round(value, 4);
                    break;
                case ValueComputationType.PriceRelative:
                    var percentage = value.CastPercentage();
                    AdditionalCosts[key] = Math.Round(percentage * Product.Price, 4);
                    break;
                default: break;
            }
        }

        public double GetTotalAdditionalCost()
        {
            List<double> values = new List<double>(AdditionalCosts.Values);
            double totalCost = values.Sum();
            return totalCost;
        }
    }
}