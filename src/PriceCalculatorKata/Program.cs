using System;

namespace PriceCalculatorKata
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Product product = new Product()
            {
                Name = "The Little Prince",
                UPC = 12345,
                Price = 20.25,
                UPCDiscountPrecedence = DiscountPrecedence.AfterTax,
                UniversalDiscountPrecedence = DiscountPrecedence.AfterTax,
                UniversalDiscountPercentage = 15,
                TaxPercentage = 21,
            };
            product.AssignAdditionalCost(AdditionalCostType.PriceRelative, "Packaging", 1);
            product.AssignAdditionalCost(AdditionalCostType.Absolute, "Transport", 2.2);
            product.ReportProductPrice();
            Console.WriteLine("---");
            product.ProductDiscountType = DiscountType.Multiplicative;
            product.ReportProductPrice();
        }
    }
}
