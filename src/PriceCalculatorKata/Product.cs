using System;

namespace PriceCalculatorKata
{
    enum DiscountPrecedence
    {
        BeforeTax,
        AfterTax,
    }
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
        public DiscountPrecedence UPCDiscountPrecedence { get; set; } = DiscountPrecedence.AfterTax;
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
        public DiscountPrecedence UniversalDiscountPrecedence { get; set; } = DiscountPrecedence.AfterTax;
        public void CheckPercentageValidation(double value)
        {
            if (value < 0 && value > 100)
                throw new Exception("Percentage is not valid");
        }
        public void ReportProductPrice()
        {
            double TotalDiscountAmount = GetUniversalDiscountAmount() + GetUPCDiscountAmount();
            double TaxAmount = GetTaxAmount(Price);
            double PriceWithTaxAndDiscount = Math.Round(Price + TaxAmount - TotalDiscountAmount, 2);
            Console.WriteLine($"${Price} before tax and discount and ${PriceWithTaxAndDiscount} after {TaxPercentage * 100}% tax and {UniversalDiscountPercentage * 100}% discount");
            if (TotalDiscountAmount > 0)
                Console.WriteLine($"Total Discount amount = {TotalDiscountAmount}");
        }
        private double GetTaxAmount(double Price)
        {
            if (UPCDiscountPrecedence == DiscountPrecedence.BeforeTax)
                Price -= GetUPCDiscountAmount();
            if (UniversalDiscountPrecedence == DiscountPrecedence.BeforeTax)
                Price -= GetUniversalDiscountAmount();
            double TaxAmount = Math.Round(Price * TaxPercentage, 2);
            Console.WriteLine($"Tax amount = {TaxAmount}");
            return TaxAmount;
        }
        private double GetUniversalDiscountAmount()
        {
            double UnivarsalDiscountAmount = Math.Round(Price * UniversalDiscountPercentage, 2);
            Console.WriteLine($"Univarsal Discount amount = {UnivarsalDiscountAmount}");
            return Math.Round(UnivarsalDiscountAmount, 2);
        }
        private double GetUPCDiscountAmount()
        {
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
            return Math.Round(UPCDiscountAmount, 2);
        }
    }
}
