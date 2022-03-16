using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace Capstone.Classes
{
    public class LogSheet
    {
        public LogSheet()
        {
            balance = 0;

        }
        public bool Audit(Food foodItem) //updates logsheet / tracks transactions.
        {
            string directory = Environment.CurrentDirectory;
            string file = "Log.txt";
            string fullPath = Path.Combine(directory, file);
            //making a path to logsheet

            try
            {
                using (StreamWriter sw = new StreamWriter(fullPath, true))
                {
                    sw.WriteLine($"{DateTime.UtcNow} {foodItem.Name} {this.Balance.ToString("C")} {(this.Balance - foodItem.Cost).ToString("C")}");
                    // adds a line to logsheet file with date/time, food purchased, current balance, and balance after transaction   
                }
                return true;
            }
            catch (IOException) //if for some reason we can't write a logsheet file...VERY WRONG
            {
                Console.WriteLine("The Log file was corrupted");
                return false;
            }
            catch (Exception) //catch all exception (I.E. corrupted foodItem)
            {

                Console.WriteLine("Vending machine self destructed!");
                return false;
            }
        }

        public bool Audit(decimal money) //tracks money added
        {
            string directory = Environment.CurrentDirectory;
            string file = "Log.txt";
            string fullPath = Path.Combine(directory, file);

            try
            {
                using (StreamWriter sw = new StreamWriter(fullPath, true))
                {
                    sw.WriteLine($"{DateTime.UtcNow} FEED MONEY: {this.Balance.ToString("C")} {(this.Balance + money).ToString("C")}"); //left off here
                }
                return true;
            }
            catch (IOException)
            {
                Console.WriteLine("The Log file was corrupted");
                return false;
            }
            catch (Exception)
            {

                Console.WriteLine("Vending machine self destructed!");
                return false;
            }
            //tracks money inserted


        }

        
        public bool AdjustBalance(decimal change) // adjusts balance and determines if cash deposit was successful
        {

            // if you adjust the balance and the amount is NOT negative it will change the Balance
            if (change == 0 || change == 1 || change == 2 || change == 5 || change == 10)
            {


                this.Audit(change);// also going to LOG cash inserted here.
                balance += change;
                Console.WriteLine($"Current Balance is: {balance.ToString("C")}");
                return true;

            }
            // else if money inserted is not a positive INT

            Console.WriteLine("Invalid dollar amount. We only accept $1, $2, $5, or $10 bills.");
            return false;

        }
        public bool AdjustBalance(Food foodItem)
        {

            // if you adjust the balance and the amount is NOT negative it will change the Balance

            if ((balance - foodItem.Cost) >= 0)
            {
                this.Audit(foodItem);// going to LOG food purchased here.
                balance -= foodItem.Cost;
                Console.WriteLine($"Current Balance is: {balance.ToString("C")}");
                return true;
            }
            // else user does not have sufficient funds
            else
            {
                Console.WriteLine("Insufficient Funds.");
                return false;
            }
        }

        public void GiveChange(VendingMachine vendingMachine)
        {
            // creating LOG file
            string directory = Environment.CurrentDirectory;
            string file = "Log.txt";
            string fullPath = Path.Combine(directory, file);
            // change is what is returning from the balance they had left after purchasing food.
            decimal change = balance;

            try
            {
                //  updates without deleting if it exists. if it does not exist it will create a new one.
                using (StreamWriter sw = new StreamWriter(fullPath, true))
                {
                    //              current date/time              balance displayer using currency formatter
                    sw.WriteLine($"{DateTime.UtcNow} GIVE CHANGE: {Balance.ToString("C")} $0.00");
                    balance = 0M;
                }
                // update sales report file 
                this.CreateSalesReport(vendingMachine);

                // creating new dictionary for giving correct change in only coins
                Dictionary<string, decimal> coins = new Dictionary<string, decimal>()
                {
                    ["Quarter"] = 0.25M,
                    ["Dime"] = 0.10M,
                    ["Nickel"] = 0.05M,
                    
                };
                
                foreach (KeyValuePair<string, decimal> coin in coins)
                {

                    while (change >= coin.Value)
                    {
                        // used to display coins dropping into change return...... slowly
                        Console.WriteLine($"Your change is: {change.ToString("C")}");
                        Thread.Sleep(800);
                        Console.Clear();
                        change -= coin.Value;
                        Console.WriteLine("**CLINK**");
                        Thread.Sleep(500);
                        Console.Clear();
                        Console.WriteLine($"Here's a {coin.Key}");
                        Thread.Sleep(800);
                        Console.Clear();
                    }
                }
            }
            catch (Exception)
            {

                Console.WriteLine("Vending machine self destructed!");
            }
            
        }
        static decimal balance;
        public decimal Balance
        {
            get

            { return balance; }
        }
        public bool PrintSalesReport()
        {
            // secret sales report if 4 is chosen on first screen
            string directory = Environment.CurrentDirectory;
            string file = "SalesReport.txt";
            string fullPath = Path.Combine(directory, file);
            // if sales report should fail, it will return false. wasnt used but could be in the future.
            bool passed = false;
            try
            {
                if (File.Exists(fullPath))
                {
                    using (StreamReader sr = new StreamReader(fullPath))
                    {
                        while (!sr.EndOfStream)
                        {
                            Console.WriteLine(sr.ReadLine());
                        }
                    }

                    passed = true;
                }
            }
            catch (Exception)
            {
                Console.WriteLine("There was an error with the sales report.");

            }
            Console.WriteLine("Please press enter to exit");
            Console.ReadLine();
            Console.Clear();
            return passed;

        }
        public bool CreateSalesReport(VendingMachine machine) //pull up our current vending machine being ran
        {
            string directory = Environment.CurrentDirectory;
            string file = "SalesReport.txt";
            string fullPath = Path.Combine(directory, file);

            try
            {
                Dictionary<string, int> salesReport = new Dictionary<string, int>();
                if (File.Exists(fullPath))
                {
                    using (StreamReader sr = new StreamReader(fullPath))
                    {
                        sr.ReadLine();
                        while (!sr.EndOfStream)
                        {
                            // created parameter for each line the streamreader reads within the file
                            string line = sr.ReadLine();
                            string[] splitLine = line.Split("|");
                            // if the sales report already exists, it will grab the line being used and add it to the dictionary we created
                            // if it does not already exist it will skip this
                            salesReport[splitLine[0]] = int.Parse(splitLine[1]);
                        }
                    }
                }
                // updates sales report
                foreach (Food item in machine.foodItems)
                {
                    if (salesReport.ContainsKey(item.Name))
                    {
                        int snacksSold = salesReport[item.Name] + (item.startingSnacks - item.SnacksLeft);
                        salesReport[item.Name] = snacksSold; // calculate how many snacks we sold, using our starting snacks and subtracting our snacks left
                    }
                    else
                    {
                        salesReport[item.Name] = (item.startingSnacks - item.SnacksLeft); //will create a new item in the sales report if it wasn't originally in our .csv
                    }
                }
                using (StreamWriter sw = new StreamWriter(fullPath, false)) //records the date/time of sales report being accessed
                {
                    sw.WriteLine($"{DateTime.UtcNow}");
                    foreach (KeyValuePair<string, int> item in salesReport)
                    {
                        sw.WriteLine($"{item.Key}|{item.Value}");
                    }
                }
                return true;
            }
            // if file does not exist or something blocking file from being accessed.
            catch (IOException)
            {
                Console.WriteLine("The Log file was corrupted");
                return false;
            }
            catch (Exception)
            {

                Console.WriteLine("Vending machine self destructed!");
                return false;
            }







        }
    }
}
