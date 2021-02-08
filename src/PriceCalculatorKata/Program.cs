using System;
using System.Collections.Generic;
using System.Globalization;
namespace PriceCalculatorKata
{
    class Program
    {
        static void Main(string[] args)
        {
            Product product = new Product()
            {
                Name = "The Little Prince",
                Price = 20.25,
                UPC = 12345,
                UPCDiscountPercentage = 7,
            };
            DiscountCalculations discountCalculations = new DiscountCalculations()
            {
                Product = product,
                UniversalDiscountPercentage = 15,
                ProductDiscountType = DiscountType.Multiplicative,
            };
            // discountCalculations.AssignCapAmount(ValueComputationType.Absolute, 4);
            TaxCalculation taxCalculation = new TaxCalculation()
            {
                Product = product,
                TaxPercentage = 21,
                UniversalDiscountPrecedence = DiscountPrecedence.AfterTax,
                UPCDiscountPrecedence = DiscountPrecedence.AfterTax,
                UniversalDiscountAmount = discountCalculations.GetUniversalDiscountAmount(),
                UPCDiscountAmount = discountCalculations.GetUPCDiscountAmount(),
            };
            AdditionalCostsCalculations additionalCostsCalculations = new AdditionalCostsCalculations()
            {
                Product = product,
            };
            additionalCostsCalculations.AssignAdditionalCost(ValueComputationType.PriceRelative, "Transport", 3);
            ProductReport productReport = new ProductReport()
            {
                RegionInfo = new RegionInfo("US"), ////use region info like: US, GB, JP
            };

            double cost = product.Price;
            double tax = taxCalculation.GetTaxAmount();
            double discount = discountCalculations.GetTotalDiscountAmount();
            double totalAdditionalCost = additionalCostsCalculations.GetTotalAdditionalCost();
            Dictionary<string, double> additionalCosts = additionalCostsCalculations.GetAdditionalCosts();
            TotalCostCalculation totalCostCalculation = new TotalCostCalculation()
            {
                Price = cost,
                Tax = tax,
                TotalDiscount = discount,
                AdditionalCosts = totalAdditionalCost,
            };
            double totalCost = totalCostCalculation.GetTotalCost();

            productReport.ReportCostByName("Cost", cost);
            productReport.ReportCostByName("Tax", tax);
            productReport.ReportCostByName("Discounts", discount);
            foreach (KeyValuePair<string, double> KeyValue in additionalCosts)
                productReport.ReportCostByName(KeyValue.Key, KeyValue.Value);

            productReport.ReportCostByName("Total", totalCost);
        }
    }
}
