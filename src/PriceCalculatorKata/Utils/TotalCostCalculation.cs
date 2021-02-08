using System;

namespace PriceCalculatorKata
{
    class TotalCostCalculation
    {
        public double Price { get; set; }
        public double Tax { get; set; }
        public double TotalDiscount { get; set; }
        public double AdditionalCosts { get; set; }
        public double GetTotalCost()
        {
            double totalCost = Price + Tax + AdditionalCosts - TotalDiscount;
            return totalCost;
        }

    }
}