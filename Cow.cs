using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmOverallApp
{
    internal class Cow: Animal
    {

        public Cow (int id, double waterAmt, double dailyCost, double weight, int age, string color, double milkAmt) : 
            base(id, waterAmt, dailyCost, weight, age, color)
        {
            this.milkAmt = milkAmt; //daily milk amount
        }

        public double milkAmt { get; set; }

        override public double grossDailyProfit()
        {
            return milkAmt * Form1.commodityPriceDict["Cow milk price"];
        }

        public override double getMilkAmt()
        {
            return this.milkAmt;
        }
    }
}
