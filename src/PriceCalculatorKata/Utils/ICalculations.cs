using System;

namespace PriceCalculatorKata
{
    interface ICalculations
    {
        Product Product { get; set; }

        double GetTotal();
    }
}