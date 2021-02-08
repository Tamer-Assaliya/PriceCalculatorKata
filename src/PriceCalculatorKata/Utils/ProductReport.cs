using System;
using System.Globalization;

namespace PriceCalculatorKata
{
    class ProductReport
    {
        public RegionInfo RegionInfo { private get; set; }

        public void ReportCostByName(string name, double cost)
        {
            double value = Math.Round(cost, 2);
            string output = $"{name} = {RegionInfo.CurrencySymbol}{value} {RegionInfo.ISOCurrencySymbol}";
            Console.WriteLine(output);
        }
    }
}