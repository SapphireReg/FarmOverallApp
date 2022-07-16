using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmOverallApp
{
    internal class Dog: Animal
    {

        public Dog(int id, double waterAmt, double dailyCost, double weight, int age, string color) : base(id, waterAmt, dailyCost, weight, age, color)
        {
        }

        override public double totalDailyCost()
        {
            return (dailyCost + (Form1.commodityPriceDict["Water price"] * waterAmt));
        }

        public override double grossDailyProfit()
        {
            return 0;
        }

        public override double getMilkAmt()
        {
            return 0;
        }

        public override double getDailyTax()
        {
            return 0;
        }

    }
}
