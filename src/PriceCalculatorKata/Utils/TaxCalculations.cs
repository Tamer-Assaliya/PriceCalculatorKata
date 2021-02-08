using System;

namespace PriceCalculatorKata
{
    class TaxCalculation
    {
        public double TaxPercentage { get; set; } = 0.2;
        public DiscountPrecedence UniversalDiscountPrecedence { get; set; } = DiscountPrecedence.AfterTax;
        public DiscountPrecedence UPCDiscountPrecedence { get; set; } = DiscountPrecedence.AfterTax;
        public Product Product { get; set; }
        public double UPCDiscountAmount { get; set; }
        public double UniversalDiscountAmount { get; set; }
        private double GetTaxAmount()
        {
            double productPrice = Product.Price;
            if (UPCDiscountPrecedence == DiscountPrecedence.BeforeTax)
                productPrice -= UPCDiscountAmount;
            if (UniversalDiscountPrecedence == DiscountPrecedence.BeforeTax)
                productPrice -= UniversalDiscountAmount;
            double taxAmount = Math.Round(productPrice * TaxPercentage, 4);
            return taxAmount;
        }
    }
}