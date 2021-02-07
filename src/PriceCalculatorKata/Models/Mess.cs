using System;
using System.Collections.Generic;
using System.Globalization;

namespace PriceCalculatorKata
{
    class Mess
    {
        public string Name { get; set; }
        public int UPC { get; set; }
        public RegionInfo RegionInfo { private get; set; }
        private double _price;
        public double Price
        {
            get { return _price; }
            set { _price = Math.Round(value, 4); }
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
        public DiscountType ProductDiscountType { get; set; } = DiscountType.Additive;
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
        private Dictionary<String, double> _additionalCosts = new Dictionary<string, double>();
        private double Cap = double.MaxValue;
        public void ReportProductPrice()
        {
            double universalDiscountAmount = GetUniversalDiscountAmount();
            double UPCDiscountAmount = GetUPCDiscountAmount();
            double totalDiscountAmount = Math.Clamp(universalDiscountAmount + UPCDiscountAmount, 0, Cap);
            double taxAmount = GetTaxAmount(Price);
            string regionCurrencySymbol = RegionInfo.CurrencySymbol;
            string regionISOCurrencySymbol = RegionInfo.ISOCurrencySymbol;
            Console.WriteLine($"Cost = {regionCurrencySymbol}{Math.Round(Price, 2)} {regionISOCurrencySymbol}");
            Console.WriteLine($"Tax = {regionCurrencySymbol}{Math.Round(taxAmount, 2)} {regionISOCurrencySymbol}");
            if (Cap > 0) Console.WriteLine($"Discounts = {regionCurrencySymbol}{Math.Round(totalDiscountAmount, 2)} {regionISOCurrencySymbol}");
            double additionalCosts = GetAdditionalCosts();
            double total = Math.Round(Price + taxAmount - totalDiscountAmount + additionalCosts, 2);
            Console.WriteLine($"Total = {regionCurrencySymbol}{total} {regionISOCurrencySymbol}");
        }
        private double GetTaxAmount(double price)
        {
            if (UPCDiscountPrecedence == DiscountPrecedence.BeforeTax)
                price -= GetUPCDiscountAmount();
            if (UniversalDiscountPrecedence == DiscountPrecedence.BeforeTax)
                price -= GetUniversalDiscountAmount();
            double taxAmount = Math.Round(price * TaxPercentage, 4);
            return taxAmount;
        }
        private double GetUniversalDiscountAmount()
        {
            double univarsalDiscountAmount = Price * UniversalDiscountPercentage;
            return Math.Round(univarsalDiscountAmount, 4);
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
            double UPCDiscountAmount = 0;
            if (ProductDiscountType == DiscountType.Additive)
                UPCDiscountAmount = _upcDiscountPercentage * Price;
            else if (ProductDiscountType == DiscountType.Multiplicative) UPCDiscountAmount = _upcDiscountPercentage * (Price - GetUniversalDiscountAmount());
            return Math.Round(UPCDiscountAmount, 4);
        }
        public void AssignAdditionalCost(ValueComputationType valueComputationType, String key, double value)
        {
            switch (valueComputationType)
            {
                case ValueComputationType.Absolute:
                    _additionalCosts[key] = Math.Round(value, 4);
                    break;
                case ValueComputationType.PriceRelative:
                    CheckPercentageValidation(value);
                    _additionalCosts[key] = Math.Round((value / 100.0) * Price, 4);
                    break;
                default: break;
            }
        }

        public double GetAdditionalCosts()
        {
            double totalCost = 0;
            foreach (KeyValuePair<string, double> KeyValue in _additionalCosts)
            {
                totalCost += KeyValue.Value;
                Console.WriteLine(KeyValue.Key + " = " + Math.Round(KeyValue.Value, 2), 2);
            }
            return totalCost;
        }
        public void AssignCapAmount(ValueComputationType valueComputationType, double value)
        {
            switch (valueComputationType)
            {
                case ValueComputationType.Absolute:
                    Cap = Math.Round(value, 4);
                    break;
                case ValueComputationType.PriceRelative:
                    CheckPercentageValidation(value);
                    Cap = Math.Round((value / 100.0) * Price, 4);
                    break;
                default: break;
            }
        }
    }
}
