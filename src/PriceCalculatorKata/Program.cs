using System;
using System.Globalization;
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
                TaxPercentage = 20,
                ProductDiscountType = DiscountType.Additive,
                RegionInfo = new RegionInfo("GB"), //use region info like: US, GB, JP 


            };
            product.AssignCapAmount(ValueComputationType.Absolute, 0); //no discounts
            product.ReportProductPrice();
            Console.WriteLine("---");
            product.Price = 17.76;
            product.ReportProductPrice();
        }
    }
}
