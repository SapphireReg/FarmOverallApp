using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Reflection;
using System.IO;


namespace FarmOverallApp
{
    public partial class Form1 : Form
    {
        static String fileName = "/FarmInformationFinal.accdb";
        String ConnStr = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source= " + Environment.CurrentDirectory + fileName;
        static OleDbConnection conn = null;
        public static Dictionary<string, double> commodityPriceDict = new Dictionary<string, double>();
        Dictionary<int, Animal> farmAnimalDict = new Dictionary<int, Animal>();
        double animalFarmCount = 0; //stores the total number of animals in the farm

        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// retrieves data from commodity price table and store in dictionary
        /// </summary>
        public void commodityPrices()
        {
            String comPrices = "SELECT * FROM [Commodity Prices]";
            OleDbCommand cmd = new OleDbCommand(comPrices, conn);

            using (OleDbDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    commodityPriceDict.Add(reader[0].ToString(), Convert.ToDouble(reader[1]));
                }
            }
        }

        /// <summary>
        /// retrieves data from the Cows table and stores into a dictionary
        /// </summary>
        private void cowDictionary()
        {
            String cow = "SELECT * FROM Cows";
            OleDbCommand cmd = new OleDbCommand(cow, conn);

            using (OleDbDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    if (reader[7].ToString() == "True") 
                    {
                        farmAnimalDict.Add(Int32.Parse(reader[0].ToString()), 
                            new JerseyCow(Int32.Parse(reader[0].ToString()), double.Parse(reader[1].ToString()), double.Parse(reader[2].ToString()), double.Parse(reader[3].ToString()), Int32.Parse(reader[4].ToString()), reader[5].ToString(), double.Parse(reader[6].ToString())));
                    }
                    else
                    {
                        farmAnimalDict.Add(Int32.Parse(reader[0].ToString()),
                           new Cow(Int32.Parse(reader[0].ToString()), double.Parse(reader[1].ToString()), double.Parse(reader[2].ToString()), double.Parse(reader[3].ToString()), Int32.Parse(reader[4].ToString()), reader[5].ToString(), double.Parse(reader[6].ToString())));
                    }
                }
            }
        }

        /// <summary>
        /// retrieves data from the Goats table and stores it into a dictionary
        /// </summary>
        private void goatDictionary()
        {
            String goat = "SELECT * FROM Goats";
            OleDbCommand cmd = new OleDbCommand(goat, conn);

            using (OleDbDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    farmAnimalDict.Add(Int32.Parse(reader[0].ToString()),
                           new Goat(Int32.Parse(reader[0].ToString()), double.Parse(reader[1].ToString()), double.Parse(reader[2].ToString()), double.Parse(reader[3].ToString()), Int32.Parse(reader[4].ToString()), reader[5].ToString(), double.Parse(reader[6].ToString())));
                }

            }
        }

        /// <summary>
        /// retrieves data from the Sheep table and stores it into a dictionary
        /// </summary>
        private void sheepDictionary()
        {
            String sheep = "SELECT * FROM Sheep";
            OleDbCommand cmd = new OleDbCommand(sheep, conn);

            using (OleDbDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    farmAnimalDict.Add(Int32.Parse(reader[0].ToString()),
                           new Sheep(Int32.Parse(reader[0].ToString()), double.Parse(reader[1].ToString()), double.Parse(reader[2].ToString()), double.Parse(reader[3].ToString()), Int32.Parse(reader[4].ToString()), reader[5].ToString(), double.Parse(reader[6].ToString())));
                }
            }
        }

        /// <summary>
        /// retrieves data from Dogs table and stores it into dictionary
        /// </summary>
        private void dogDictionary()
        {
            String dogs = "SELECT * FROM Dogs";
            OleDbCommand cmd = new OleDbCommand(dogs, conn);

            using (OleDbDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    farmAnimalDict.Add(Int32.Parse(reader[0].ToString()),
                            new Dog(Int32.Parse(reader[0].ToString()), double.Parse(reader[1].ToString()), double.Parse(reader[5].ToString()), double.Parse(reader[2].ToString()), Int32.Parse(reader[3].ToString()), reader[4].ToString()));
                }
            }
        }

        /// <summary>
        /// executes methods that retrives data from the database and stores it into separate dictionaries.
        /// </summary>
        /// 
        private void animalDictionary()
        {
            commodityPrices();
            cowDictionary();
            goatDictionary();
            dogDictionary();
            sheepDictionary();
        }

        /// <summary>
        /// counts the total number of animals
        /// </summary>
        private void countAnimals()
        {
            foreach (KeyValuePair<int, Animal> animal in farmAnimalDict)
            {
                animalFarmCount++;
            }
        }

        /// <summary>
        /// calculates the total daily NET profit (gross - cost - tax) 
        /// </summary>
        private double calcTotalNetProfit()
        {
            double totalProfit = 0;
            foreach (KeyValuePair<int, Animal> animal in farmAnimalDict)
            {
                totalProfit += animal.Value.grossDailyProfit() - animal.Value.totalDailyCost();
            }
            return totalProfit;
        }

        /// <summary>
        /// calculates and displays month's tax paid
        /// </summary>
        private double calcMonthlyTax()
        {
            double monthlyTax = 0;
            foreach (KeyValuePair<int, Animal> animal in farmAnimalDict)
            {
                monthlyTax += animal.Value.getDailyTax() * 30;
            }
            return monthlyTax;
        }

        /// <summary>
        /// calculates and displays total daily Milk amount of Cow and Goat
        /// </summary>
        private double calcMilkAmt()
        {
            double totalDailyMilkAmt = 0;
            foreach (KeyValuePair<int, Animal> animal in farmAnimalDict)
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
        private double calcAveAge()
        {
            double ave = 0;
            double sum = 0;
            double count = 0;
            foreach (KeyValuePair <int, Animal> animal in farmAnimalDict)
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
        private double ageRatio(int age)
        {
            double count = 0;
            double ratio = 0;
            
             foreach (KeyValuePair<int, Animal> animal in farmAnimalDict)
            {
                if (animal.Value.age > age)
                {
                    count++;
                }
            }
            ratio = count / animalFarmCount;
            return ratio;
        }

        /// <summary>
        /// using generics to get average of profitability by class
        /// </summary>
        /// <typeparam name="AnimalClass"></typeparam>
        /// <returns></returns>
        private double aveProf<AnimalClass>()
            where AnimalClass: Animal //constraint to only the Animal base class
        {
            double count = 0;
            double sum = 0;

            foreach (var animal in farmAnimalDict.Values.OfType<AnimalClass>())
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
        private double totalProf<AnimalClass>()
            where AnimalClass: Animal
        {
            double total = 0;

            foreach (var animal in farmAnimalDict.Values.OfType<AnimalClass>())
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
        private double totalTax<AnimalClass>()
            where AnimalClass: Animal
        {
            double tax = 0;

            foreach (var animal in farmAnimalDict.Values.OfType<AnimalClass>())
            {
                tax += animal.getDailyTax() * 30;
            }
            return tax;
        }

        /// <summary>
        /// calculates the ratio of the dog cost to the total cost
        /// </summary>
        /// <returns></returns>
        private double calcDogRatioCost()
        {
            double sum = 0;
            double dogSum = 0;
            foreach (KeyValuePair<int, Animal> animal in farmAnimalDict)
            {
                sum += animal.Value.totalDailyCost();

                if (animal.Value.GetType() == typeof(Dog))
                {
                    dogSum += animal.Value.totalDailyCost();
                }
            }
            totalCostOutput.Text = Math.Round(sum, 2).ToString();
            return dogSum / sum;
        }

        /// <summary>
        /// counts the number of red colored animals from the dictionary
        /// </summary>
        /// <returns></returns>
        private int countRed()
        {
            int count = 0;
            foreach (KeyValuePair<int, Animal> animal in farmAnimalDict)
            {
                if (animal.Value.color.ToLower() == "red")
                {
                    count++;
                }
            }
            return count;
        }


        /// <summary>
        /// displays the details of the Animal provided in the parameters via MessageBox
        /// </summary>
        /// <param name="animal"></param>
        private void displayAnimalDetails(Animal animal)
        {
            if (animal == null)
            {
                MessageBox.Show("We can't find an animal with the ID provided. Please try again.");
            }
            else 
            {
                string text = "";
                string colName = "";

                foreach (PropertyInfo prop in animal.GetType().GetProperties())
                {
                    colName += prop.Name + "\t";
                    text += prop.GetValue(animal, null).ToString() + "\t";
                }
                animalOutputBox.Text = "Animal Type: " + animal.GetType().Name + "\r\n\r\n" +  colName + "\r\n" +   text;
            }
        }

        /// <summary>
        /// swaps two elements in an array
        /// </summary>
        /// <param name="arr"></param>
        /// <param name="a"></param>
        /// <param name="b"></param>
        public void swap(List<List<double>> arr, int a, int b)
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
        public int partition(List<List<double>> arr, int first, int last)
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
        public void quickSort(List<List<double>> arr, int first, int last)
        {
            if (first < last)
            {
                int partInd = partition(arr, first, last); //partition index

                quickSort(arr, first, partInd - 1);
                quickSort(arr, partInd + 1, last);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //tooltips
            ageThresholdToolTip.SetToolTip(ageThresholdLabel, "Displays the ratio of animal farms above the threshold");


            conn = new OleDbConnection(ConnStr);
            conn.Open();
            animalDictionary();
            conn.Close();
            countAnimals();

            //displays output in the GUI
            totalProfOutput.Text = Math.Round(calcTotalNetProfit(), 2).ToString();
            totalMilkAmtOutput.Text = calcMilkAmt().ToString();
            totalTaxPaidOutput.Text = Math.Round(calcMonthlyTax(), 2).ToString();
            aveAgeOutput.Text = Math.Round(calcAveAge(), 2).ToString();
            totalCountOutput.Text = animalFarmCount.ToString();
            dogsCostOutput.Text = calcDogRatioCost().ToString("P");
            redCountOutput.Text = countRed().ToString();
            goatsCowsAveProfOutput.Text = Math.Round(aveProf<Cow>() + aveProf<JerseyCow>(), 2).ToString();
            sheepAveProfOutput.Text = Math.Round(aveProf<Sheep>(), 2).ToString();
            jerseyCowTotalProfOutput.Text = Math.Round(totalProf<JerseyCow>(), 2).ToString();
            jerseyCowTotalTaxPaidOutput.Text = Math.Round(totalTax<JerseyCow>(), 2).ToString();

        }

        /// <summary>
        /// displays the ratio of animals that have age more than the threshold input
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="FormatException"></exception>
        private void ageThresholdInput_TextChanged(object sender, EventArgs e)
        {
            int num = 0;
            if (ageThresholdInput.Text == "")
            {
                //ignores error for an empty input
                try
                {
                    ageThresholdRatioOutput.Text = ageRatio(num).ToString("P");
                }
                catch (FormatException)
                {
                    throw new FormatException("Empty");
                }
            }
            else if (Int32.TryParse(ageThresholdInput.Text, out num))
            {
                ageThresholdRatioOutput.Text = ageRatio(num).ToString("P");
            }
            else
            {
                MessageBox.Show("Please enter a numeric character", "Input Error"); //error for non-numeric input
            }

        }

        /// <summary>
        /// displays the details of the animal with the ID provided in a message box after pressing the Enter/Return Key
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="FormatException"></exception>
        private void idInput_KeyDown(object sender, KeyEventArgs e)
        {
            int num = 0;
            Animal animal = null;
            if (e.KeyCode == Keys.Enter)
            {
                if (idInput.Text == "")
                {
                    try
                    {
                        Int32.TryParse(idInput.Text, out num);
                    }
                    catch (FormatException)
                    {
                        throw new FormatException("Empty");
                    }
                }
                else if (Int32.TryParse(idInput.Text, out num))
                {
                    if (farmAnimalDict.TryGetValue(num, out animal)) //error checking for out of dictionary index range
                    {
                        displayAnimalDetails(animal);
                    }
                    else
                    {
                        MessageBox.Show("Cannot find ID. Please try again.", "ID Error"); //message box error for IDs not found
                    }

                }
                else
                {
                    MessageBox.Show("Please enter a numeric character", "Input Error"); //message error for non-numeric input
                }
            }
        }

        /// <summary>
        /// on click, produces a text file that contains a sorted list of IDs based on profitability
        /// </summary>
        private void genProfitFile_Click(object sender, EventArgs e)
        {
            List<List<double>> idProfitList = new List<List<double>>();

            //creates a list (exc dogs) of IDs and profit
            foreach (KeyValuePair<int, Animal> animal in farmAnimalDict)
            {
                if (animal.Value.GetType() != typeof(Dog)) //excludes dogs
                {
                    List<double> temp = new List<double>() { animal.Value.id, Math.Round(animal.Value.netDailyProfit(),2) };
                    idProfitList.Add(temp);
                }
            }

            quickSort(idProfitList, 0, idProfitList.Count -1 ); //sorts list via QuickSort

            //writes id and profitability on the textfile
            string fileName = String.Format("Sorted AnimalID List by Profitability {0}.txt", System.DateTime.Now.ToString("dd MMMM yyyy"));
            using (System.IO.StreamWriter writer = new System.IO.StreamWriter(fileName))
            {
                foreach (List<double> info in idProfitList)
                {
                    writer.WriteLine(String.Format("{0} - {1}", info[0], info[1]));
                }
            }
            MessageBox.Show(String.Format("{0} successfully created", fileName), "File Created");
        }

    }
}
