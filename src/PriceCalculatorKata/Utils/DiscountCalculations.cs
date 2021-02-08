using System;

namespace PriceCalculatorKata
{
    class DiscountCalculations
    {
        private double Cap = double.MaxValue;
        public Product Product { get; set; }
        public DiscountType ProductDiscountType { get; set; } = DiscountType.Additive;
        public double UniversalDiscountPercentage { get; set; } = 0.0;
        private double GetUniversalDiscountAmount()
        {
            double univarsalDiscountAmount = Product.Price * UniversalDiscountPercentage;
            return Math.Round(univarsalDiscountAmount, 4);
        }
        private double GetUPCDiscountAmount(int UPC)
        {
            double UPCDiscountAmount = 0;
            if (ProductDiscountType == DiscountType.Additive)
                UPCDiscountAmount = Product.UPCDiscountPercentage * Product.Price;
            else if (ProductDiscountType == DiscountType.Multiplicative) UPCDiscountAmount = Product.UPCDiscountPercentage * (Product.Price - GetUniversalDiscountAmount());
            return Math.Round(UPCDiscountAmount, 4);
        }
        public void AssignCapAmount(ValueComputationType valueComputationType, double value)
        {
            switch (valueComputationType)
            {
                case ValueComputationType.Absolute:
                    Cap = Math.Round(value, 4);
                    break;
                case ValueComputationType.PriceRelative:
                    var percentage = value.CastPercentage();
                    Cap = Math.Round(percentage * Product.Price, 4);
                    break;
                default: break;
            }
        }
        public double GetTotalDiscountAmount(int UPC)
        {
            double universalDiscountAmount = GetUniversalDiscountAmount();
            double UPCDiscountAmount = GetUPCDiscountAmount(UPC);
            double totalDiscountAmount = Math.Clamp(universalDiscountAmount + UPCDiscountAmount, 0, Cap);
            return totalDiscountAmount;
        }
    }
}