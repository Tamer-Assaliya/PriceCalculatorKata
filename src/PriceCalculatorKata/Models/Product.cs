using System;

namespace PriceCalculatorKata
{
    class Product
    {
        public string Name { get; set; }
        public int UPC { get; set; }
        public double Price { get; set; }
        private double _upcDiscountPercentage;
        public double UPCDiscountPercentage
        {
            get { return _upcDiscountPercentage; }
            set { _upcDiscountPercentage = value.CastPercentage(); }
        }
    }
}