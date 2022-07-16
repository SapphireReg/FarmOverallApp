using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmOverallApp
{
    public static class Reports
    {
        /// <summary>
        /// counts the total number of animals
        /// </summary>
        public static void countAnimals()
        {
            foreach (KeyValuePair<int, Animal> animal in Form1.farmAnimalDict)
            {
                Form1.animalFarmCount++; 
            }
        }

        /// <summary>
        /// calculates the total daily NET profit (gross - cost - tax) 
        /// </summary>
        public static double calcTotalNetProfit()
        {
            double totalProfit = 0;
            foreach (KeyValuePair<int, Animal> animal in Form1.farmAnimalDict)
            {
                totalProfit += animal.Value.grossDailyProfit() - animal.Value.totalDailyCost();
            }
            return totalProfit;
        }

        /// <summary>
        /// calculates and displays month's tax paid
        /// </summary>
        public static double calcMonthlyTax()
        {
            double monthlyTax = 0;
            foreach (KeyValuePair<int, Animal> animal in Form1.farmAnimalDict)
            {
                monthlyTax += animal.Value.getDailyTax() * 30;
            }
            return monthlyTax;
        }

        /// <summary>
        /// calculates and displays total daily Milk amount of Cow and Goat
        /// </summary>
        public static double calcMilkAmt()
        {
            double totalDailyMilkAmt = 0;
            foreach (KeyValuePair<int, Animal> animal in Form1.farmAnimalDict)
            {
                if (animal.Value.GetType() == typeof(Goat) || animal.Value.GetType() == typeof(Cow) || animal.Value.GetType() == typeof(JerseyCow))
                {
                    totalDailyMilkAmt += animal.Value.getMilkAmt();
                }
            }
            return totalDailyMilkAmt;
        }

        /// <summary>
        /// returns the average age of animals except dogs
        /// </summary>
        /// <returns>(type)double, average</returns>
        public static double calcAveAge()
        {
            double ave = 0;
            double sum = 0;
            double count = 0;
            foreach (KeyValuePair<int, Animal> animal in Form1.farmAnimalDict)
            {
                if (animal.Value.GetType() != typeof(Dog))
                {
                    count++;
                    sum += animal.Value.age;
                }
            }
            ave = sum / count;
            return ave;
        }

        /// <summary>
        /// returns the ratio (%) of farmAnimals higher than the input(age)
        /// </summary>
        /// <param name="age"></param>
        /// <returns></returns>
        public static double ageRatio(int age)
        {
            double count = 0;
            double ratio = 0;

            foreach (KeyValuePair<int, Animal> animal in Form1.farmAnimalDict)
            {
                if (animal.Value.age > age)
                {
                    count++;
                }
            }
            ratio = count / Form1.animalFarmCount;
            return ratio;
        }

        /// <summary>
        /// using generics to get average of profitability by class
        /// </summary>
        /// <typeparam name="AnimalClass"></typeparam>
        /// <returns></returns>
        public static double aveProf<AnimalClass>()
            where AnimalClass : Animal //constraint to only the Animal base class
        {
            double count = 0;
            double sum = 0;

            foreach (var animal in Form1.farmAnimalDict.Values.OfType<AnimalClass>())
            {
                sum += animal.grossDailyProfit();
                count++;
            }
            return sum / count;

        }

        /// <summary>
        /// using generics to get the total daily profitability
        /// </summary>
        /// <typeparam name="AnimalClass"></typeparam>
        /// <returns></returns>
        public static double totalProf<AnimalClass>()
            where AnimalClass : Animal
        {
            double total = 0;

            foreach (var animal in Form1.farmAnimalDict.Values.OfType<AnimalClass>())
            {
                total += animal.netDailyProfit();
            }
            return total;
        }

        /// <summary>
        /// using generics to get the total monthly taxes
        /// </summary>
        /// <typeparam name="AnimalClass"></typeparam>
        /// <returns></returns>
        public static double totalTax<AnimalClass>()
            where AnimalClass : Animal
        {
            double tax = 0;

            foreach (var animal in Form1.farmAnimalDict.Values.OfType<AnimalClass>())
            {
                tax += animal.getDailyTax() * 30;
            }
            return tax;
        }

        /// <summary>
        /// calculates the ratio of the dog cost to the total cost
        /// </summary>
        /// <returns></returns>
        public static double calcDogRatioCost()
        {
            double sum = 0;
            double dogSum = 0;
            foreach (KeyValuePair<int, Animal> animal in Form1.farmAnimalDict)
            {
                sum += animal.Value.totalDailyCost();

                if (animal.Value.GetType() == typeof(Dog))
                {
                    dogSum += animal.Value.totalDailyCost();
                }
            }
            Form1.totalCost = Math.Round(sum, 2);
            return dogSum / sum;
        }

        /// <summary>
        /// counts the number of red colored animals from the dictionary
        /// </summary>
        /// <returns></returns>
        public static int countRed()
        {
            int count = 0;
            foreach (KeyValuePair<int, Animal> animal in Form1.farmAnimalDict)
            {
                if (animal.Value.color.ToLower() == "red")
                {
                    count++;
                }
            }
            return count;
        }

        /// <summary>
        /// swaps two elements in an array
        /// </summary>
        /// <param name="arr"></param>
        /// <param name="a"></param>
        /// <param name="b"></param>
        public static void swap(List<List<double>> arr, int a, int b)
        {
            List<double> temp = arr[a];
            arr[a] = arr[b];
            arr[b] = temp;
        }

        /// <summary>
        /// This function takes the last element as a pivot point and places all numbers less than the pivot to the left and greater than to the right
        /// </summary>
        /// <param name="arr"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public static int partition(List<List<double>> arr, int first, int last)
        {
            List<double> pivot = arr[last]; //pivot

            int i = (first - 1);

            for (int j = first; j <= last - 1; j++)
            {
                if (arr[j][1] > pivot[1])
                {
                    i++;
                    swap(arr, i, j);
                }
            }
            swap(arr, i + 1, last);
            return (i + 1);
        }

        /// <summary>
        /// main QuickSort function
        /// </summary>
        /// <param name="arr">Array to be sorted</param>
        /// <param name="first">Starting index</param>
        /// <param name="last">Ending index</param>
        public static void quickSort(List<List<double>> arr, int first, int last)
        {
            if (first < last)
            {
                int partInd = partition(arr, first, last); //partition index

                quickSort(arr, first, partInd - 1);
                quickSort(arr, partInd + 1, last);
            }
        }
    }
}
