using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace FarmOverallApp
{
    public abstract class Animal
    {
        public int id;

        public Animal(int id, double waterAmt, double dailyCost, double weight, int age, string color)
        {
            this.id = id;
            this.waterAmt = waterAmt;
            this.dailyCost = dailyCost;
            this.weight = weight;
            this.age = age;
            this.color = color; 
        }

        public double waterAmt { get ; set; }
        public double dailyCost { get; set; }
        public double weight { get; set; }
        public string color { get; set; }
        public int age { get; set; }    

        /// <summary>
        /// This method returns monthly tax calculated by tax * weight * 30
        /// </summary>
        /// <returns>double type</returns>
        public virtual double getDailyTax()
        {
            return(Form1.commodityPriceDict["Government tax per kg"] * weight);
        }

        public virtual double dailyWaterCost()
        {
            return Form1.commodityPriceDict["Water price"] * waterAmt;
        }

        /// <summary>
        /// returns the monthlyTotal Cost including water
        /// </summary>
        /// <returns></returns>
        public virtual double totalDailyCost()
        {
            return this.dailyCost + this.dailyWaterCost() + this.getDailyTax();
        }

        public virtual double netDailyProfit()
        {
            return this.grossDailyProfit() - this.totalDailyCost();
        }

        abstract public double grossDailyProfit();
        abstract public double getMilkAmt();

    }
}
