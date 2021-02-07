using System;

namespace PriceCalculatorKata
{
    class Product
    {
        public string Name { get; set; }
        public int UPC { get; set; }
        private double _price;
        public double Price
        {
            get { return _price; }
            set { _price = Math.Round(value, 2); }
        }
        private double _taxPercentage = 0.2;
        public double TaxPercentage
        {
            get { return _taxPercentage; }
            set
            {
                CheckPercentageValidation(value);
                _taxPercentage = value / 100.0;
            }
        }
        private double _discountPercentage = 0.0;
        public double DiscountPercentage
        {
            get { return _discountPercentage; }
            set
            {
                CheckPercentageValidation(value);
                _discountPercentage = value / 100.0;
            }
        }

        public void CheckPercentageValidation(double value)
        {
            if (value < 0 && value > 100)
                throw new Exception("Percentage is not valid");
        }
        public void ReportProductPrice()
        {
            double DiscountAmount = Math.Round(Price * DiscountPercentage, 2);
            double PriceWithTaxAndDiscount = Math.Round(Price * (1 + TaxPercentage - DiscountPercentage), 2);
            Console.WriteLine($"${Price} before tax and discount and ${PriceWithTaxAndDiscount} after {TaxPercentage * 100}% tax and {DiscountPercentage * 100}% discount");
            if (DiscountAmount > 0)
                Console.WriteLine($"Discount amount = {DiscountAmount}");
        }

    }
}
