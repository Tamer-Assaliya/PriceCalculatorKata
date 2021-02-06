using System;
using System.Collections.Generic;
namespace PriceCalculatorKata
{
    enum DiscountPrecedence
    {
        BeforeTax,
        AfterTax,
    }
    enum AdditionalCostType
    {
        Absolute,
        PriceRelative,
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
        private Dictionary<String, double> _additionalAbsoluteCosts = new Dictionary<string, double>();
        private Dictionary<String, double> _additionalPriceRelativeCosts = new Dictionary<string, double>();
        public void ReportProductPrice()
        {
            double TotalDiscountAmount = GetUniversalDiscountAmount() + GetUPCDiscountAmount();
            double TaxAmount = GetTaxAmount(Price);
            Console.WriteLine($"Cost = ${Price}");
            Console.WriteLine($"Tax = ${TaxAmount}");
            Console.WriteLine($"Discounts = ${TotalDiscountAmount}");
            double AdditionalCosts = GetAdditionalCosts();
            double Total = Math.Round(Price + TaxAmount - TotalDiscountAmount + AdditionalCosts, 2);
            Console.WriteLine($"Total = ${Total}");
        }
        private double GetTaxAmount(double Price)
        {
            if (UPCDiscountPrecedence == DiscountPrecedence.BeforeTax)
                Price -= GetUPCDiscountAmount();
            if (UniversalDiscountPrecedence == DiscountPrecedence.BeforeTax)
                Price -= GetUniversalDiscountAmount();
            double TaxAmount = Math.Round(Price * TaxPercentage, 2);
            return TaxAmount;
        }
        private double GetUniversalDiscountAmount()
        {
            double UnivarsalDiscountAmount = Math.Round(Price * UniversalDiscountPercentage, 2);
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
            return Math.Round(UPCDiscountAmount, 2);
        }
        public void AssignAdditionalCost(AdditionalCostType additionalCostType, String Key, double value)
        {
            switch (additionalCostType)
            {
                case AdditionalCostType.Absolute:
                    _additionalAbsoluteCosts[Key] = value;
                    break;
                case AdditionalCostType.PriceRelative:
                    CheckPercentageValidation(value);
                    _additionalPriceRelativeCosts[Key] = (value / 100.0) * Price;
                    break;
                default: break;
            }
        }

        public double GetAdditionalCosts()
        {
            double totalCost = 0;
            foreach (KeyValuePair<string, double> KeyValue in _additionalAbsoluteCosts)
            {
                totalCost += KeyValue.Value;
                Console.WriteLine(KeyValue.Key + " = " + Math.Round(KeyValue.Value, 2));
            }
            foreach (KeyValuePair<string, double> KeyValue in _additionalPriceRelativeCosts)
            {
                totalCost += KeyValue.Value;
                Console.WriteLine(KeyValue.Key + " = " + Math.Round(KeyValue.Value, 2));
            }
            return Math.Round(totalCost, 2);
        }
    }
}
