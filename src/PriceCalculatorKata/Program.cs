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
                ProductDiscountType = DiscountType.Additive
            };
            // product.AssignCapAmount(ValueComputationType.PriceRelative, 20);
            product.ReportProductPrice();
            Console.WriteLine("---");
            // product.AssignCapAmount(ValueComputationType.Absolute, 4);
            product.ReportProductPrice();
        }
    }
}
