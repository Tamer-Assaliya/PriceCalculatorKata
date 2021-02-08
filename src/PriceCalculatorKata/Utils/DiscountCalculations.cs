using System;

namespace PriceCalculatorKata
{
    class DiscountCalculations
    {
        private double Cap = double.MaxValue;
        public Product Product { get; set; }
        public DiscountType ProductDiscountType { get; set; } = DiscountType.Additive;
        private double _universalDiscountPercentage { get; set; }

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
            double upcDiscountAmount = 0;
            if (ProductDiscountType == DiscountType.Additive)
                upcDiscountAmount = Product.UPCDiscountPercentage * Product.Price;
            else if (ProductDiscountType == DiscountType.Multiplicative)
                upcDiscountAmount = Product.UPCDiscountPercentage * (Product.Price - GetUniversalDiscountAmount());
            return Math.Round(upcDiscountAmount, 4);
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
            double upcDiscountAmount = GetUPCDiscountAmount();
            double totalDiscountAmount = Math.Clamp(universalDiscountAmount + upcDiscountAmount, 0, Cap);
            return totalDiscountAmount;
        }
    }
}