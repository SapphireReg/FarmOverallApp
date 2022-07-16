using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmOverallApp
{
    internal class Sheep: Animal
    {

        public Sheep(int id, double waterAmt, double dailyCost, double weight, int age, string color, double woolAmt) : base(id, waterAmt, dailyCost, weight, age, color)
        {
            this.woolAmt = woolAmt;
        }

        public double woolAmt { get; set; }

        override public double grossDailyProfit()
        {
            return woolAmt * Form1.commodityPriceDict["Sheep wool price"];
        }

        public override double getMilkAmt()
        {
            return 0;
        }
    }
}
