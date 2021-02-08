using System;

namespace PriceCalculatorKata
{
    class DiscountCalculations
    {
        private double Cap = double.MaxValue;
        public Product Product { get; set; }
        public DiscountType ProductDiscountType { get; set; } = DiscountType.Additive;
        private double _universalDiscountPercentage { get; set; } = 0.0;
        public double UniversalDiscountPercentage
        {
            get { return _universalDiscountPercentage; }
            set { _universalDiscountPercentage = value.CastPercentage(); }
        }
        public double GetUniversalDiscountAmount()
        {
            double univarsalDiscountAmount = Product.Price * UniversalDiscountPercentage;
            return Math.Round(univarsalDiscountAmount, 4);
        }
        public double GetUPCDiscountAmount()
        {
            double UPCDiscountAmount = 0;
            if (ProductDiscountType == DiscountType.Additive)
                UPCDiscountAmount = Product.UPCDiscountPercentage * Product.Price;
            else if (ProductDiscountType == DiscountType.Multiplicative)
                UPCDiscountAmount = Product.UPCDiscountPercentage * (Product.Price - GetUniversalDiscountAmount());
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
        public double GetTotalDiscountAmount()
        {
            double universalDiscountAmount = GetUniversalDiscountAmount();
            double UPCDiscountAmount = GetUPCDiscountAmount();
            double totalDiscountAmount = Math.Clamp(universalDiscountAmount + UPCDiscountAmount, 0, Cap);
            return totalDiscountAmount;
        }
    }
}