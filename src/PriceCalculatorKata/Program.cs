using System;
using System.Collections.Generic;
using System.Globalization;
namespace PriceCalculatorKata
{
    class Program
    {
        static void Main(string[] args)
        {
            var product = new Product()
            {
                Name = "The Little Prince",
                Price = 20.25,
                UPC = 12345,
                UPCDiscountPercentage = 7,
            };
            var discountCalculations = new DiscountCalculations()
            {
                Product = product,
                UniversalDiscountPercentage = 15,
                ProductDiscountType = DiscountType.Multiplicative,
            };
            // discountCalculations.AssignCapAmount(ValueComputationType.Absolute, 4);
            var taxCalculation = new TaxCalculation()
            {
                Product = product,
                TaxPercentage = 21,
                UniversalDiscountPrecedence = DiscountPrecedence.AfterTax,
                upcDiscountPrecedence = DiscountPrecedence.AfterTax,
                UniversalDiscountAmount = discountCalculations.GetUniversalDiscountAmount(),
                UPCDiscountAmount = discountCalculations.GetUPCDiscountAmount(),
            };
            var additionalCostsCalculations = new AdditionalCostsCalculations()
            {
                Product = product,
            };
            additionalCostsCalculations.AssignAdditionalCost(ValueComputationType.PriceRelative, "Transport", 3);
            var productReport = new ProductReport()
            {
                RegionInfo = new RegionInfo("US"), ////use region info like: US, GB, JP
            };

            var cost = product.Price;
            var tax = taxCalculation.GetTaxAmount();
            var discount = discountCalculations.GetTotalDiscountAmount();
            var totalAdditionalCost = additionalCostsCalculations.GetTotalAdditionalCost();
            Dictionary<string, double> additionalCosts = additionalCostsCalculations.AdditionalCosts;
            var totalCostCalculation = new TotalCostCalculation()
            {
                Price = cost,
                Tax = tax,
                TotalDiscount = discount,
                AdditionalCosts = totalAdditionalCost,
            };
            var totalCost = totalCostCalculation.GetTotalCost();

            productReport.ReportCostByName("Cost", cost);
            productReport.ReportCostByName("Tax", tax);
            productReport.ReportCostByName("Discounts", discount);
            foreach (KeyValuePair<string, double> KeyValue in additionalCosts)
                productReport.ReportCostByName(KeyValue.Key, KeyValue.Value);

            productReport.ReportCostByName("Total", totalCost);
        }
    }
}