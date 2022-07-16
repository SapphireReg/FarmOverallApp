using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmOverallApp
{
    internal class JerseyCow: Cow
    {
        public JerseyCow (int id, double waterAmt, double dailyCost, double weight, int age, string color, double milkAmt) : 
            base(id, waterAmt, dailyCost, weight, age, color, milkAmt) {}

        /// <summary>
        /// This method returns monthly tax calculated by tax * weight * 30
        /// </summary>
        /// <returns>double type</returns>
        override public double getDailyTax()
        {
            return (Form1.commodityPriceDict["Jersy cow tax"] * weight);
        }
    }
}
