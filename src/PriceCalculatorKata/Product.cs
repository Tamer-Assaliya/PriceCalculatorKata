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
                bool IsValidPercentage = value >= 0 && value <= 100;
                if (IsValidPercentage) _taxPercentage = value / 100.0;
                else throw new Exception("Percentage is not valid");
            }
        }
        public void ReportProductPrice()
        {
            double PriceWithTax = Math.Round(Price * (1 + TaxPercentage), 2);
            Console.WriteLine($"${Price} before tax and ${PriceWithTax} after {TaxPercentage * 100}% tax");
        }
    }
}
