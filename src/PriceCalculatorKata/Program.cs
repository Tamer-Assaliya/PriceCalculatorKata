﻿using System;

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
                Price = 20.253,
            };
            product.UniversalDiscountPercentage = 15;
            product.ReportProductPrice();
            product.UPC = 789;
            product.TaxPercentage = 21;
            Console.WriteLine("---");
            product.ReportProductPrice();
        }
    }
}
