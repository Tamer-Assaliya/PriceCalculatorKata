using System;

namespace PriceCalculatorKata
{
    class TaxCalculation
    {
        private double _taxPercentage;
        public double TaxPercentage { get { return _taxPercentage; } set { _taxPercentage = value.CastPercentage(); } }
        public DiscountPrecedence UniversalDiscountPrecedence { get; set; } = DiscountPrecedence.AfterTax;
        public DiscountPrecedence UPCDiscountPrecedence { get; set; } = DiscountPrecedence.AfterTax;
        public Product Product { get; set; }
        public double UPCDiscountAmount { get; set; }
        public double UniversalDiscountAmount { get; set; }
        public double GetTaxAmount()
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