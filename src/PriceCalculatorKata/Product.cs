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
        private double _upcDiscountPercentage = 0.0;

        private double _universalDiscountPercentage = 0.0;
        public double UniversalDiscountPercentage
        {
            get { return _universalDiscountPercentage; }
            set
            {
                CheckPercentageValidation(value);
                _universalDiscountPercentage = value / 100.0;
            }
        }

        public void CheckPercentageValidation(double value)
        {
            if (value < 0 && value > 100)
                throw new Exception("Percentage is not valid");
        }
        public void ReportProductPrice()
        {
            double DiscountAmount = GetTotalDiscountAmount();
            double TaxAmount = Price * TaxPercentage;
            double PriceWithTaxAndDiscount = Math.Round(Price + TaxAmount - DiscountAmount, 2);
            Console.WriteLine($"${Price} before tax and discount and ${PriceWithTaxAndDiscount} after {TaxPercentage * 100}% tax and {UniversalDiscountPercentage * 100}% discount");
            if (DiscountAmount > 0)
                Console.WriteLine($"Total Discount amount = {DiscountAmount}");
        }
        private double GetTotalDiscountAmount()
        {
            double UnivarsalDiscountAmount = Math.Round(Price * UniversalDiscountPercentage, 2);
            Console.WriteLine($"Univarsal Discount amount = {UnivarsalDiscountAmount}");
            switch (UPC)
            {
                case 12345:
                    _upcDiscountPercentage = 0.07;
                    break;
                default:
                    _upcDiscountPercentage = 0.0;
                    break;
            }
            double UPCDiscountAmount = _upcDiscountPercentage * Price;
            Console.WriteLine($"UPCD Discount amount = {Math.Round(UPCDiscountAmount, 2)}");
            return Math.Round(UnivarsalDiscountAmount + UPCDiscountAmount, 2);

        }

    }
}
