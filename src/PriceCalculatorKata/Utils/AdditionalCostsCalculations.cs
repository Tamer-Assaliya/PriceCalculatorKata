using System;
using System.Collections.Generic;
using System.Linq;
namespace PriceCalculatorKata
{
    class AdditionalCostsCalculations : Calculations
    {
        public Dictionary<String, double> AdditionalCosts { private set; get; } = new Dictionary<String, double>();

        public void AssignAdditionalCost(ValueComputationType valueComputationType, String key, double value)
        => AdditionalCosts[key] = ComputeValueByType(valueComputationType, value);

        public override double GetTotal() => AdditionalCosts.Values.Sum();
    }
}