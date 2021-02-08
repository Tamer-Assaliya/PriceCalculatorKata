using System;
using System.Collections.Generic;

namespace PriceCalculatorKata
{
    class AdditionalCostsCalculations
    {
        private Dictionary<String, double> _additionalCosts = new Dictionary<string, double>();
        public Product Product { get; set; }
        public void AssignAdditionalCost(ValueComputationType valueComputationType, String key, double value)
        {
            switch (valueComputationType)
            {
                case ValueComputationType.Absolute:
                    _additionalCosts[key] = Math.Round(value, 4);
                    break;
                case ValueComputationType.PriceRelative:
                    var percentage = value.CastPercentage();
                    _additionalCosts[key] = Math.Round(percentage * Product.Price, 4);
                    break;
                default: break;
            }
        }

        public double GetTotalAdditionalCost()
        {
            double totalCost = 0;
            foreach (KeyValuePair<string, double> KeyValue in _additionalCosts)
            {
                totalCost += KeyValue.Value;
            }
            return totalCost;
        }

        public Dictionary<String, double> GetAdditionalCosts()
        {
            return _additionalCosts;
        }

    }
}