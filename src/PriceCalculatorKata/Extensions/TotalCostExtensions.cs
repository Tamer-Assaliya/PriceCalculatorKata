using System;

namespace PriceCalculatorKata
{
    public static class TotalCostExtensions
    {
        public static double GetTotalCost(this TotalCost totalCostObject)
        {
            double totalCost =
            totalCostObject.Price
            + totalCostObject.Tax
            + totalCostObject.AdditionalCosts
            - totalCostObject.TotalDiscount;
            return totalCost;
        }
    }
}