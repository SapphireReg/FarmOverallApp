using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmOverallApp
{
    internal class Goat: Animal
    {

        public Goat(int id, double waterAmt, double dailyCost, double weight, int age, string color, double milkAmt) : base(id, waterAmt, dailyCost, weight, age, color)
        { 
            this.milkAmt = milkAmt;
        }

        public double milkAmt { get; set; }

        override public double grossDailyProfit()
        {
            return milkAmt * Form1.commodityPriceDict["Goat milk price"];
        }
        public override double getMilkAmt()
        {
            return this.milkAmt;
        }

    }
}
